/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// C_SignInit, C_Sign, C_SignUpdate, C_SignFinal, C_VerifyInit, C_Verify, C_VerifyUpdate and C_VerifyFinal tests.
    /// </summary>
    [TestFixture()]
    public class _21_SignAndVerifyTest
    {
        /// <summary>
        /// C_SignInit, C_Sign, C_VerifyInit and C_Verify test with CKM_RSA_PKCS mechanism.
        /// </summary>
        [Test()]
        public void _01_SignAndVerifySinglePartTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, ConvertUtils.UInt64FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                NativeULong pubKeyId = CK.CK_INVALID_HANDLE;
                NativeULong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11Library, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                // Initialize signing operation
                rv = pkcs11Library.C_SignInit(session, ref mechanism, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");

                // Get length of signature in first call
                NativeULong signatureLen = 0;
                rv = pkcs11Library.C_Sign(session, sourceData, ConvertUtils.UInt64FromInt32(sourceData.Length), null, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(signatureLen > 0);
                
                // Allocate array for signature
                byte[] signature = new byte[signatureLen];

                // Get signature in second call
                rv = pkcs11Library.C_Sign(session, sourceData, ConvertUtils.UInt64FromInt32(sourceData.Length), signature, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with signature

                // Initialize verification operation
                rv = pkcs11Library.C_VerifyInit(session, ref mechanism, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Verify signature
                rv = pkcs11Library.C_Verify(session, sourceData, ConvertUtils.UInt64FromInt32(sourceData.Length), signature, ConvertUtils.UInt64FromInt32(signature.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with verification result

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

        /// <summary>
        /// C_SignInit, C_SignUpdate, C_SignFinal, C_VerifyInit, C_VerifyUpdate and C_VerifyFinal test.
        /// </summary>
        [Test()]
        public void _02_SignAndVerifyMultiPartTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, ConvertUtils.UInt64FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                NativeULong pubKeyId = CK.CK_INVALID_HANDLE;
                NativeULong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11Library, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                byte[] signature = null;

                // Multipart signature functions C_SignUpdate and C_SignFinal can be used i.e. for signing of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData))
                {
                    // Initialize signing operation
                    rv = pkcs11Library.C_SignInit(session, ref mechanism, privKeyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];

                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        rv = pkcs11Library.C_SignUpdate(session, part, ConvertUtils.UInt64FromInt32(bytesRead));
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                    }

                    // Get the length of signature in first call
                    NativeULong signatureLen = 0;
                    rv = pkcs11Library.C_SignFinal(session, null, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    Assert.IsTrue(signatureLen > 0);
                    
                    // Allocate array for signature
                    signature = new byte[signatureLen];

                    // Get signature in second call
                    rv = pkcs11Library.C_SignFinal(session, signature, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }

                // Do something interesting with signature

                // Multipart verification functions C_VerifyUpdate and C_VerifyFinal can be used i.e. for signature verification of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData))
                {
                    // Initialize verification operation
                    rv = pkcs11Library.C_VerifyInit(session, ref mechanism, pubKeyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    
                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        rv = pkcs11Library.C_VerifyUpdate(session, part, ConvertUtils.UInt64FromInt32(bytesRead));
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                    }

                    // Verify signature
                    rv = pkcs11Library.C_VerifyFinal(session, signature, ConvertUtils.UInt64FromInt32(signature.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }

                // Do something interesting with verification result

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

