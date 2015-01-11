/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI8;
using Net.Pkcs11Interop.LowLevelAPI8.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI8
{
    /// <summary>
    /// Mechanism tests.
    /// </summary>
    [TestFixture()]
    public class _15_MechanismTest
    {
        /// <summary>
        /// Mechanism with empty parameter test.
        /// </summary>
        [Test()]
        public void _01_EmptyParameterTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            // Create mechanism without the parameter
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void _02_ByteArrayParameterTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);

            // Create mechanism with the byte array parameter
            // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == Convert.ToUInt64(parameter.Length));

            // Free unmanaged memory taken by mechanism parameter
            UnmanagedMemory.Free(ref mechanism.Parameter);
            mechanism.ParameterLen = 0;
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);

            parameter = null;

            // Create mechanism with null byte array parameter
            mechanism = CkmUtils.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with structure as parameter test.
        /// </summary>
        [Test()]
        public void _03_StructureParameterTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            // Note that we are allocating unmanaged memory that will have to be freed later
            CK_KEY_DERIVATION_STRING_DATA parameter = new CK_KEY_DERIVATION_STRING_DATA();
            parameter.Data = UnmanagedMemory.Allocate(data.Length);
            UnmanagedMemory.Write(parameter.Data, data);
            parameter.Len = Convert.ToUInt64(data.Length);
            
            // Create mechanism with the structure as parameter
            // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(mechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(CK_KEY_DERIVATION_STRING_DATA))));

            // Free all unmanaged memory we previously allocated
            UnmanagedMemory.Free(ref parameter.Data);
            parameter.Len = 0;
            
            // Free unmanaged memory taken by mechanism parameter
            UnmanagedMemory.Free(ref mechanism.Parameter);
            mechanism.ParameterLen = 0;
            Assert.IsTrue(mechanism.Mechanism == (ulong)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }
    }
}

