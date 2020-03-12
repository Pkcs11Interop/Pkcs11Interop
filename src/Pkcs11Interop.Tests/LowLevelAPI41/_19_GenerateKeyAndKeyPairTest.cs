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
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
{
    /// <summary>
    /// C_GenerateKey and C_GenerateKeyPair tests.
    /// </summary>
    [TestFixture()]
    public class _19_GenerateKeyAndKeyPairTest
    {
        /// <summary>
        /// C_GenerateKey test.
        /// </summary>
        [Test()]
        public void _01_GenerateKeyTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, ConvertUtils.UInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare attribute template of new key
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[4];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY);
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3);
                template[2] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
                template[3] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);

                // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_KEY_GEN);
                
                // Generate key
                NativeULong keyId = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_GenerateKey(session, ref mechanism, template, ConvertUtils.UInt32FromInt32(template.Length), ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // In LowLevelAPI we have to free unmanaged memory taken by attributes
                for (int i = 0; i < template.Length; i++)
                {
                    UnmanagedMemory.Free(ref template[i].value);
                    template[i].valueLen = 0;
                }

                // Do something interesting with generated key

                // Destroy object
                rv = pkcs11Library.C_DestroyObject(session, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_Logout(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_GenerateKeyPair test.
        /// </summary>
        [Test()]
        public void _02_GenerateKeyPairTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, ConvertUtils.UInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // The CKA_ID attribute is intended as a means of distinguishing multiple key pairs held by the same subject
                byte[] ckaId = new byte[20];
                rv = pkcs11Library.C_GenerateRandom(session, ckaId, ConvertUtils.UInt32FromInt32(ckaId.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare attribute template of new public key
                CK_ATTRIBUTE[] pubKeyTemplate = new CK_ATTRIBUTE[10];
                pubKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
                pubKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, false);
                pubKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
                pubKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
                pubKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
                pubKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY, true);
                pubKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY_RECOVER, true);
                pubKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_WRAP, true);
                pubKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_MODULUS_BITS, 1024);
                pubKeyTemplate[9] = CkaUtils.CreateAttribute(CKA.CKA_PUBLIC_EXPONENT, new byte[] { 0x01, 0x00, 0x01 });

                // Prepare attribute template of new private key
                CK_ATTRIBUTE[] privKeyTemplate = new CK_ATTRIBUTE[9];
                privKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
                privKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, true);
                privKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
                privKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
                privKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_SENSITIVE, true);
                privKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
                privKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_SIGN, true);
                privKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_SIGN_RECOVER, true);
                privKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_UNWRAP, true);

                // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS_KEY_PAIR_GEN);
                
                // Generate key pair
                NativeULong pubKeyId = CK.CK_INVALID_HANDLE;
                NativeULong privKeyId = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_GenerateKeyPair(session, ref mechanism, pubKeyTemplate, ConvertUtils.UInt32FromInt32(pubKeyTemplate.Length), privKeyTemplate, ConvertUtils.UInt32FromInt32(privKeyTemplate.Length), ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // In LowLevelAPI we have to free unmanaged memory taken by attributes
                for (int i = 0; i < privKeyTemplate.Length; i++)
                {
                    UnmanagedMemory.Free(ref privKeyTemplate[i].value);
                    privKeyTemplate[i].valueLen = 0;
                }

                for (int i = 0; i < pubKeyTemplate.Length; i++)
                {
                    UnmanagedMemory.Free(ref pubKeyTemplate[i].value);
                    pubKeyTemplate[i].valueLen = 0;
                }

                // Do something interesting with generated key pair
                
                // Destroy object
                rv = pkcs11Library.C_DestroyObject(session, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_DestroyObject(session, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_Logout(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

