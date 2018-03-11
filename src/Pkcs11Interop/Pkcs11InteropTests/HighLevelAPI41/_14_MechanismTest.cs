/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using System.Reflection;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI41;
using Net.Pkcs11Interop.HighLevelAPI41.MechanismParams;
using NUnit.Framework;
using CK_KEY_DERIVATION_STRING_DATA = Net.Pkcs11Interop.LowLevelAPI41.MechanismParams.CK_KEY_DERIVATION_STRING_DATA;
using CK_MECHANISM = Net.Pkcs11Interop.LowLevelAPI41.CK_MECHANISM;
using NativeLongUtils = Net.Pkcs11Interop.LowLevelAPI41.NativeLongUtils;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
{
    /// <summary>
    /// Mechanism tests.
    /// </summary>
    [TestFixture()]
    public class _14_MechanismTest
    {
        /// <summary>
        /// Mechanism dispose test.
        /// </summary>
        [Test()]
        public void _01_DisposeMechanismTest()
        {
            Helpers.CheckPlatform();

            byte[] parameter = new byte[8];
            System.Random rng = new Random();
            rng.NextBytes(parameter);
            
            // Unmanaged memory for mechanism parameter stored in low level CK_MECHANISM struct
            // is allocated by constructor of Mechanism class.
            Mechanism mechanism1 = new Mechanism(CKM.CKM_DES_CBC, parameter);
            
            // Do something interesting with mechanism
            
            // This unmanaged memory is freed by Dispose() method.
            mechanism1.Dispose();
            
            
            // Mechanism class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (Mechanism mechanism2 = new Mechanism(CKM.CKM_DES_CBC, parameter))
            {
                // Do something interesting with mechanism
            }


            #pragma warning disable 0219

            // Explicit calling of Dispose() method can also be ommitted
            // and this is the prefered way how to use Mechanism class.
            Mechanism mechanism3 = new Mechanism(CKM.CKM_DES_CBC, parameter);
            
            // Do something interesting with mechanism
            
            // Dispose() method will be called (and unmanaged memory freed) by GC eventually
            // but we cannot be sure when will this occur.

            #pragma warning restore 0219
        }

        /// <summary>
        /// Mechanism with empty parameter test.
        /// </summary>
        [Test()]
        public void _02_EmptyParameterTest()
        {
            Helpers.CheckPlatform();

            // Create mechanism without the parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Type == NativeLongUtils.ConvertFromCKM(CKM.CKM_RSA_PKCS));

            // We access private Mechanism member just for the testing purposes
            CK_MECHANISM ckMechanism = (CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == NativeLongUtils.ConvertFromCKM(CKM.CKM_RSA_PKCS));
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void _03_ByteArrayParameterTest()
        {
            Helpers.CheckPlatform();

            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);
            
            // Create mechanism with the byte array parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == NativeLongUtils.ConvertFromCKM(CKM.CKM_AES_CBC));

            // We access private Mechanism member here just for the testing purposes
            CK_MECHANISM ckMechanism = (CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == NativeLongUtils.ConvertFromCKM(CKM.CKM_AES_CBC));
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == NativeLongUtils.ConvertFromInt32(parameter.Length));

            parameter = null;
            
            // Create mechanism with null byte array parameter
            mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == NativeLongUtils.ConvertFromCKM(CKM.CKM_AES_CBC));

            // We access private Mechanism member here just for the testing purposes
            ckMechanism = (CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == NativeLongUtils.ConvertFromCKM(CKM.CKM_AES_CBC));
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with object as parameter test.
        /// </summary>
        [Test()]
        public void _04_ObjectParameterTest()
        {
            Helpers.CheckPlatform();

            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            CkKeyDerivationStringData parameter = new CkKeyDerivationStringData(data);

            // Create mechanism with the object as parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Type == NativeLongUtils.ConvertFromCKM(CKM.CKM_XOR_BASE_AND_DATA));

            // We access private Mechanism member here just for the testing purposes
            CK_MECHANISM ckMechanism = (CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == NativeLongUtils.ConvertFromCKM(CKM.CKM_XOR_BASE_AND_DATA));
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(NativeLongUtils.ConvertToInt32(ckMechanism.ParameterLen) == UnmanagedMemory.SizeOf(typeof(CK_KEY_DERIVATION_STRING_DATA)));
        }
    }
}
