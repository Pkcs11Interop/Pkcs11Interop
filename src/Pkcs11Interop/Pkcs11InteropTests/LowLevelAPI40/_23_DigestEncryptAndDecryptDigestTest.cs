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
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI40;
using NUnit.Framework;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
{
    /// <summary>
    /// C_DigestEncryptUpdate and C_DecryptDigestUpdate tests.
    /// </summary>
    [TestFixture()]
    public class _23_DigestEncryptAndDecryptDigestTest
    {
        /// <summary>
        /// Basic C_DigestEncryptUpdate and C_DecryptDigestUpdate test.
        /// </summary>
        [Test()]
        public void _01_BasicDigestEncryptAndDecryptDigestTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeULongUtils.ConvertUInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                NativeULong keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate random initialization vector
                byte[] iv = new byte[8];
                rv = pkcs11.C_GenerateRandom(session, iv, NativeULongUtils.ConvertUInt32FromInt32(iv.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify encryption mechanism with initialization vector as parameter.
                // Note that CkmUtils.CreateMechanism() automaticaly copies iv into newly allocated unmanaged memory.
                CK_MECHANISM encryptionMechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_CBC, iv);

                // Specify digesting mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM digestingMechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA_1);
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");
                byte[] encryptedData = null;
                byte[] digest1 = null;
                byte[] decryptedData = null;
                byte[] digest2 = null;
                
                // Multipart digesting and encryption function C_DigestEncryptUpdate can be used i.e. for digesting and encryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData), outputStream = new MemoryStream())
                {
                    // Initialize digesting operation
                    rv = pkcs11.C_DigestInit(session, ref digestingMechanism);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Initialize encryption operation
                    rv = pkcs11.C_EncryptInit(session, ref encryptionMechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    
                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    NativeULong encryptedPartLen = NativeULongUtils.ConvertUInt32FromInt32(encryptedPart.Length);
                    
                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        encryptedPartLen = NativeULongUtils.ConvertUInt32FromInt32(encryptedPart.Length);
                        rv = pkcs11.C_DigestEncryptUpdate(session, part, NativeULongUtils.ConvertUInt32FromInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append encrypted data part to the output stream
                        outputStream.Write(encryptedPart, 0, NativeULongUtils.ConvertUInt32ToInt32(encryptedPartLen));
                    }

                    // Get length of digest value in first call
                    NativeULong digestLen = 0;
                    rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    Assert.IsTrue(digestLen > 0);
                    
                    // Allocate array for digest value
                    digest1 = new byte[digestLen];
                    
                    // Get digest value in second call
                    rv = pkcs11.C_DigestFinal(session, digest1, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Get the length of last encrypted data part in first call
                    byte[] lastEncryptedPart = null;
                    NativeULong lastEncryptedPartLen = 0;
                    rv = pkcs11.C_EncryptFinal(session, null, ref lastEncryptedPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Allocate array for the last encrypted data part
                    lastEncryptedPart = new byte[lastEncryptedPartLen];
                    
                    // Get the last encrypted data part in second call
                    rv = pkcs11.C_EncryptFinal(session, lastEncryptedPart, ref lastEncryptedPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Append the last encrypted data part to the output stream
                    outputStream.Write(lastEncryptedPart, 0, NativeULongUtils.ConvertUInt32ToInt32(lastEncryptedPartLen));
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    encryptedData = outputStream.ToArray();
                }
                
                // Do something interesting with encrypted data and digest
                
                // Multipart decryption and digesting function C_DecryptDigestUpdate can be used i.e. for digesting and decryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(encryptedData), outputStream = new MemoryStream())
                {
                    // Initialize decryption operation
                    rv = pkcs11.C_DecryptInit(session, ref encryptionMechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Initialize digesting operation
                    rv = pkcs11.C_DigestInit(session, ref digestingMechanism);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    
                    // Prepare buffer for decrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    NativeULong partLen = NativeULongUtils.ConvertUInt32FromInt32(part.Length);
                    
                    // Read input stream with encrypted data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
                    {
                        // Process each individual encrypted data part
                        partLen = NativeULongUtils.ConvertUInt32FromInt32(part.Length);
                        rv = pkcs11.C_DecryptDigestUpdate(session, encryptedPart, NativeULongUtils.ConvertUInt32FromInt32(bytesRead), part, ref partLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append decrypted data part to the output stream
                        outputStream.Write(part, 0, NativeULongUtils.ConvertUInt32ToInt32(partLen));
                    }
                    
                    // Get the length of last decrypted data part in first call
                    byte[] lastPart = null;
                    NativeULong lastPartLen = 0;
                    rv = pkcs11.C_DecryptFinal(session, null, ref lastPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Allocate array for the last decrypted data part
                    lastPart = new byte[lastPartLen];
                    
                    // Get the last decrypted data part in second call
                    rv = pkcs11.C_DecryptFinal(session, lastPart, ref lastPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Append the last decrypted data part to the output stream
                    outputStream.Write(lastPart, 0, NativeULongUtils.ConvertUInt32ToInt32(lastPartLen));
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    decryptedData = outputStream.ToArray();

                    // Get length of digest value in first call
                    NativeULong digestLen = 0;
                    rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    Assert.IsTrue(digestLen > 0);
                    
                    // Allocate array for digest value
                    digest2 = new byte[digestLen];
                    
                    // Get digest value in second call
                    rv = pkcs11.C_DigestFinal(session, digest2, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }
                
                // Do something interesting with decrypted data and digest
                Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));
                Assert.IsTrue(ConvertUtils.BytesToBase64String(digest1) == ConvertUtils.BytesToBase64String(digest2));
                
                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter (iv in this case)
                UnmanagedMemory.Free(ref encryptionMechanism.Parameter);
                encryptionMechanism.ParameterLen = 0;
                
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

