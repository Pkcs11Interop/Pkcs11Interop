/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI41;
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
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
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

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
        public void _02_ByteArrayParameterTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

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
        public void _03_StructureParameterTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            // Note that we are allocating unmanaged memory that will have to be freed later
            CK_KEY_DERIVATION_STRING_DATA parameter = new CK_KEY_DERIVATION_STRING_DATA();
            parameter.Data = UnmanagedMemory.Allocate(data.Length);
            UnmanagedMemory.Write(parameter.Data, data);
            parameter.Len = Convert.ToUInt32(data.Length);
            
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

