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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;
using NUnit.Framework;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI81
{
    /// <summary>
    /// C_WrapKey and C_UnwrapKey tests.
    /// </summary>
    [TestFixture()]
    public class _25_WrapAndUnwrapKeyTest
    {
        /// <summary>
        /// Basic C_WrapKey and C_UnwrapKey test.
        /// </summary>
        [Test()]
        public void _01_BasicWrapAndUnwrapKeyTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs81);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeULongUtils.ConvertUInt64FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                NativeULong pubKeyId = CK.CK_INVALID_HANDLE;
                NativeULong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                NativeULong keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify wrapping mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS);

                // Get length of wrapped key in first call
                NativeULong wrappedKeyLen = 0;
                rv = pkcs11.C_WrapKey(session, ref mechanism, pubKeyId, keyId, null, ref wrappedKeyLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(wrappedKeyLen > 0);
                
                // Allocate array for wrapped key
                byte[] wrappedKey = new byte[wrappedKeyLen];

                // Get wrapped key in second call
                rv = pkcs11.C_WrapKey(session, ref mechanism, pubKeyId, keyId, wrappedKey, ref wrappedKeyLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with wrapped key

                // Define attributes for unwrapped key
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[6];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY);
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3);
                template[2] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
                template[3] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
                template[4] = CkaUtils.CreateAttribute(CKA.CKA_DERIVE, true);
                template[5] = CkaUtils.CreateAttribute(CKA.CKA_EXTRACTABLE, true);

                // Unwrap key
                NativeULong unwrappedKeyId = 0;
                rv = pkcs11.C_UnwrapKey(session, ref mechanism, privKeyId, wrappedKey, wrappedKeyLen, template, NativeULongUtils.ConvertUInt64FromInt32(template.Length), ref unwrappedKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with unwrapped key

                rv = pkcs11.C_DestroyObject(session, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_DestroyObject(session, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_DestroyObject(session, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Logout(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

