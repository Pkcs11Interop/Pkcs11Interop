/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.LowLevelAPI;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// Mechanism tests.
    /// </summary>
    [TestFixture()]
    public class MechanismTest
    {
        /// <summary>
        /// Mechanism with empty parameter test.
        /// </summary>
        [Test()]
        public void EmptyParameterTest()
        {
            // Create mechanism without the parameter
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void ByteArrayParameterTest()
        {
            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);

            // Create mechanism with the byte array parameter
            // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == parameter.Length);

            // Free unmanaged memory taken by mechanism parameter
            UnmanagedMemory.Free(ref mechanism.Parameter);
            mechanism.ParameterLen = 0;
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);

            parameter = null;

            // Create mechanism with null byte array parameter
            mechanism = CkmUtils.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_AES_CBC);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with structure as parameter test.
        /// </summary>
        [Test()]
        public void StructureParameterTest()
        {
            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            // Note that we are allocating unmanaged memory that will have to be freed later
            CK_KEY_DERIVATION_STRING_DATA parameter = new CK_KEY_DERIVATION_STRING_DATA();
            parameter.Data = UnmanagedMemory.Allocate(data.Length);
            UnmanagedMemory.Write(parameter.Data, data);
            parameter.Len = (uint)data.Length;
            
            // Create mechanism with the structure as parameter
            // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(mechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == UnmanagedMemory.SizeOf(typeof(CK_KEY_DERIVATION_STRING_DATA)));

            // Free all unmanaged memory we previously allocated
            UnmanagedMemory.Free(ref parameter.Data);
            parameter.Len = 0;
            
            // Free unmanaged memory taken by mechanism parameter
            UnmanagedMemory.Free(ref mechanism.Parameter);
            mechanism.ParameterLen = 0;
            Assert.IsTrue(mechanism.Mechanism == (uint)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(mechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(mechanism.ParameterLen == 0);
        }
    }
}

