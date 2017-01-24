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

namespace Net.Pkcs11Interop.Tests.LowLevelAPI81
{
    /// <summary>
    /// C_SignRecoverInit, C_SignRecover, C_VerifyRecoverInit and C_VerifyRecover tests.
    /// </summary>
    [TestFixture()]
    public class _22_SignAndVerifyRecoverTest
    {
        /// <summary>
        /// Basic C_SignRecoverInit, C_SignRecover, C_VerifyRecoverInit and C_VerifyRecover test.
        /// </summary>
        [Test()]
        public void _01_BasicSignAndVerifyRecoverTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs81);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                ulong slotId = Helpers.GetUsableSlot(pkcs11);
                
                ulong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, Convert.ToUInt64(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                ulong pubKeyId = CK.CK_INVALID_HANDLE;
                ulong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS);
                
                // Initialize signing operation
                rv = pkcs11.C_SignRecoverInit(session, ref mechanism, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                
                // Get length of signature in first call
                ulong signatureLen = 0;
                rv = pkcs11.C_SignRecover(session, sourceData, Convert.ToUInt64(sourceData.Length), null, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(signatureLen > 0);
                
                // Allocate array for signature
                byte[] signature = new byte[signatureLen];
                
                // Get signature in second call
                rv = pkcs11.C_SignRecover(session, sourceData, Convert.ToUInt64(sourceData.Length), signature, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with signature
                
                // Initialize verification operation
                rv = pkcs11.C_VerifyRecoverInit(session, ref mechanism, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get length of recovered data in first call
                ulong recoveredDataLen = 0;
                rv = pkcs11.C_VerifyRecover(session, signature, Convert.ToUInt64(signature.Length), null, ref recoveredDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(recoveredDataLen > 0);
                
                // Allocate array for recovered data
                byte[] recoveredData = new byte[recoveredDataLen];

                // Verify signature and get recovered data in second call
                rv = pkcs11.C_VerifyRecover(session, signature, Convert.ToUInt64(signature.Length), recoveredData, ref recoveredDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with verification result and recovered data
                Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(recoveredData));

                rv = pkcs11.C_DestroyObject(session, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_DestroyObject(session, pubKeyId);
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
