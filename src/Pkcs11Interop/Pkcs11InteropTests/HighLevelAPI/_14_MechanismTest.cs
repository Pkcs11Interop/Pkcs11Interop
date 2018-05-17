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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using NUnit.Framework;
using HLA40 = Net.Pkcs11Interop.HighLevelAPI40;
using HLA41 = Net.Pkcs11Interop.HighLevelAPI41;
using HLA80 = Net.Pkcs11Interop.HighLevelAPI80;
using HLA81 = Net.Pkcs11Interop.HighLevelAPI81;
using LLA40 = Net.Pkcs11Interop.LowLevelAPI40;
using LLA41 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA80 = Net.Pkcs11Interop.LowLevelAPI80;
using LLA81 = Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
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
            byte[] parameter = new byte[8];
            System.Random rng = new Random();
            rng.NextBytes(parameter);

            // Unmanaged memory for mechanism parameter stored in low level CK_MECHANISM struct
            // is allocated by constructor of Mechanism class.
            IMechanism mechanism1 = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_DES_CBC, parameter);

            // Do something interesting with mechanism

            // This unmanaged memory is freed by Dispose() method.
            mechanism1.Dispose();


            // Mechanism class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (IMechanism mechanism2 = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_DES_CBC, parameter))
            {
                // Do something interesting with mechanism
            }


            #pragma warning disable 0219

            // Explicit calling of Dispose() method can also be ommitted
            // and this is the prefered way how to use Mechanism class.
            IMechanism mechanism3 = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_DES_CBC, parameter);

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
            // Create mechanism without the parameter
            IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Type == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_RSA_PKCS));

            // We access private Mechanism member just for the testing purposes
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA40.Mechanism mechanism40 = (HLA40.Mechanism)mechanism;
                    LLA40.CK_MECHANISM ckMechanism40 = (LLA40.CK_MECHANISM)typeof(HLA40.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism40);
                    Assert.IsTrue(ckMechanism40.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_RSA_PKCS));
                    Assert.IsTrue(ckMechanism40.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism40.ParameterLen == 0);
                }
                else
                {
                    HLA41.Mechanism mechanism41 = (HLA41.Mechanism)mechanism;
                    LLA41.CK_MECHANISM ckMechanism41 = (LLA41.CK_MECHANISM)typeof(HLA41.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism41);
                    Assert.IsTrue(ckMechanism41.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_RSA_PKCS));
                    Assert.IsTrue(ckMechanism41.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism41.ParameterLen == 0);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA80.Mechanism mechanism80 = (HLA80.Mechanism)mechanism;
                    LLA80.CK_MECHANISM ckMechanism80 = (LLA80.CK_MECHANISM)typeof(HLA80.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism80);
                    Assert.IsTrue(ckMechanism80.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_RSA_PKCS));
                    Assert.IsTrue(ckMechanism80.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism80.ParameterLen == 0);
                }
                else
                {
                    HLA81.Mechanism mechanism81 = (HLA81.Mechanism)mechanism;
                    LLA81.CK_MECHANISM ckMechanism81 = (LLA81.CK_MECHANISM)typeof(HLA81.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism81);
                    Assert.IsTrue(ckMechanism81.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_RSA_PKCS));
                    Assert.IsTrue(ckMechanism81.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism81.ParameterLen == 0);
                }
            }
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void _03_ByteArrayParameterTest()
        {
            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);

            // Create mechanism with the byte array parameter
            IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));

            // We access private members here just for the testing purposes
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA40.Mechanism mechanism40 = (HLA40.Mechanism)mechanism;
                    LLA40.CK_MECHANISM ckMechanism40 = (LLA40.CK_MECHANISM)typeof(HLA40.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism40);
                    Assert.IsTrue(ckMechanism40.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism40.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism40.ParameterLen) == parameter.Length);
                }
                else
                {
                    HLA41.Mechanism mechanism41 = (HLA41.Mechanism)mechanism;
                    LLA41.CK_MECHANISM ckMechanism41 = (LLA41.CK_MECHANISM)typeof(HLA41.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism41);
                    Assert.IsTrue(ckMechanism41.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism41.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism41.ParameterLen) == parameter.Length);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA80.Mechanism mechanism80 = (HLA80.Mechanism)mechanism;
                    LLA80.CK_MECHANISM ckMechanism80 = (LLA80.CK_MECHANISM)typeof(HLA80.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism80);
                    Assert.IsTrue(ckMechanism80.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism80.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism80.ParameterLen) == parameter.Length);
                }
                else
                {
                    HLA81.Mechanism mechanism81 = (HLA81.Mechanism)mechanism;
                    LLA81.CK_MECHANISM ckMechanism81 = (LLA81.CK_MECHANISM)typeof(HLA81.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism81);
                    Assert.IsTrue(ckMechanism81.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism81.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism81.ParameterLen) == parameter.Length);
                }
            }

            parameter = null;

            // Create mechanism with null byte array parameter
            mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));

            // We access private members here just for the testing purposes
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA40.Mechanism mechanism40 = (HLA40.Mechanism)mechanism;
                    LLA40.CK_MECHANISM ckMechanism40 = (LLA40.CK_MECHANISM)typeof(HLA40.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism40);
                    Assert.IsTrue(ckMechanism40.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism40.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism40.ParameterLen == 0);
                }
                else
                {
                    HLA41.Mechanism mechanism41 = (HLA41.Mechanism)mechanism;
                    LLA41.CK_MECHANISM ckMechanism41 = (LLA41.CK_MECHANISM)typeof(HLA41.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism41);
                    Assert.IsTrue(ckMechanism41.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism41.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism41.ParameterLen == 0);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA80.Mechanism mechanism80 = (HLA80.Mechanism)mechanism;
                    LLA80.CK_MECHANISM ckMechanism80 = (LLA80.CK_MECHANISM)typeof(HLA80.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism80);
                    Assert.IsTrue(ckMechanism80.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism80.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism80.ParameterLen == 0);
                }
                else
                {
                    HLA81.Mechanism mechanism81 = (HLA81.Mechanism)mechanism;
                    LLA81.CK_MECHANISM ckMechanism81 = (LLA81.CK_MECHANISM)typeof(HLA81.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism81);
                    Assert.IsTrue(ckMechanism81.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_AES_CBC));
                    Assert.IsTrue(ckMechanism81.Parameter == IntPtr.Zero);
                    Assert.IsTrue(ckMechanism81.ParameterLen == 0);
                }
            }
        }

        /// <summary>
        /// Mechanism with object as parameter test.
        /// </summary>
        [Test()]
        public void _04_ObjectParameterTest()
        {
            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            ICkKeyDerivationStringData parameter = Settings.Factories.MechanismParamsFactory.CreateCkKeyDerivationStringData(data);

            // Create mechanism with the object as parameter
            IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Type == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_XOR_BASE_AND_DATA));

            // We access private Mechanism member here just for the testing purposes
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA40.Mechanism mechanism40 = (HLA40.Mechanism)mechanism;
                    LLA40.CK_MECHANISM ckMechanism40 = (LLA40.CK_MECHANISM)typeof(HLA40.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism40);
                    Assert.IsTrue(ckMechanism40.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_XOR_BASE_AND_DATA));
                    Assert.IsTrue(ckMechanism40.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism40.ParameterLen) == UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
                }
                else
                {
                    HLA41.Mechanism mechanism41 = (HLA41.Mechanism)mechanism;
                    LLA41.CK_MECHANISM ckMechanism41 = (LLA41.CK_MECHANISM)typeof(HLA41.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism41);
                    Assert.IsTrue(ckMechanism41.Mechanism == NativeULongUtils.ConvertUInt32FromCKM(CKM.CKM_XOR_BASE_AND_DATA));
                    Assert.IsTrue(ckMechanism41.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism41.ParameterLen) == UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    HLA80.Mechanism mechanism80 = (HLA80.Mechanism)mechanism;
                    LLA80.CK_MECHANISM ckMechanism80 = (LLA80.CK_MECHANISM)typeof(HLA80.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism80);
                    Assert.IsTrue(ckMechanism80.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_XOR_BASE_AND_DATA));
                    Assert.IsTrue(ckMechanism80.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism80.ParameterLen) == UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
                }
                else
                {
                    HLA81.Mechanism mechanism81 = (HLA81.Mechanism)mechanism;
                    LLA81.CK_MECHANISM ckMechanism81 = (LLA81.CK_MECHANISM)typeof(HLA81.Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism81);
                    Assert.IsTrue(ckMechanism81.Mechanism == NativeULongUtils.ConvertUInt64FromCKM(CKM.CKM_XOR_BASE_AND_DATA));
                    Assert.IsTrue(ckMechanism81.Parameter != IntPtr.Zero);
                    Assert.IsTrue(Convert.ToInt32(ckMechanism81.ParameterLen) == UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
                }
            }
        }
    }
}
