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
using Net.Pkcs11Interop.LowLevelAPI41;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
{
    /// <summary>
    /// C_SignEncryptUpdate and C_DecryptVerifyUpdate tests.
    /// </summary>
    [TestFixture()]
    public class _24_SignEncryptAndDecryptVerifyTest
    {
        /// <summary>
        /// Basic C_SignEncryptUpdate and C_DecryptVerifyUpdate test.
        /// </summary>
        [Test()]
        public void _01_BasicSignEncryptAndDecryptVerifyTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, Convert.ToUInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Generate asymetric key pair
                uint pubKeyId = CK.CK_INVALID_HANDLE;
                uint privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM signingMechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                // Generate symetric key
                uint keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Generate random initialization vector
                byte[] iv = new byte[8];
                rv = pkcs11.C_GenerateRandom(session, iv, Convert.ToUInt32(iv.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify encryption mechanism with initialization vector as parameter.
                // Note that CkmUtils.CreateMechanism() automaticaly copies iv into newly allocated unmanaged memory.
                CK_MECHANISM encryptionMechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_CBC, iv);

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Passw0rd");
                byte[] signature = null;
                byte[] encryptedData = null;
                byte[] decryptedData = null;
                
                // Multipart signing and encryption function C_SignEncryptUpdate can be used i.e. for signing and encryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData), outputStream = new MemoryStream())
                {
                    // Initialize signing operation
                    rv = pkcs11.C_SignInit(session, ref signingMechanism, privKeyId);
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
                    uint encryptedPartLen = Convert.ToUInt32(encryptedPart.Length);
                    
                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        encryptedPartLen = Convert.ToUInt32(encryptedPart.Length);
                        rv = pkcs11.C_SignEncryptUpdate(session, part, Convert.ToUInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append encrypted data part to the output stream
                        outputStream.Write(encryptedPart, 0, Convert.ToInt32(encryptedPartLen));
                    }

                    // Get the length of signature in first call
                    uint signatureLen = 0;
                    rv = pkcs11.C_SignFinal(session, null, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    Assert.IsTrue(signatureLen > 0);
                    
                    // Allocate array for signature
                    signature = new byte[signatureLen];
                    
                    // Get signature in second call
                    rv = pkcs11.C_SignFinal(session, signature, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Get the length of last encrypted data part in first call
                    byte[] lastEncryptedPart = null;
                    uint lastEncryptedPartLen = 0;
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
                    outputStream.Write(lastEncryptedPart, 0, Convert.ToInt32(lastEncryptedPartLen));
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    encryptedData = outputStream.ToArray();
                }
                
                // Do something interesting with signature and encrypted data

                // Multipart decryption and verification function C_DecryptVerifyUpdate can be used i.e. for decryption and signature verification of streamed data
                using (MemoryStream inputStream = new MemoryStream(encryptedData), outputStream = new MemoryStream())
                {
                    // Initialize decryption operation
                    rv = pkcs11.C_DecryptInit(session, ref encryptionMechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Initialize verification operation
                    rv = pkcs11.C_VerifyInit(session, ref signingMechanism, pubKeyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    
                    // Prepare buffer for decrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    uint partLen = Convert.ToUInt32(part.Length);
                    
                    // Read input stream with encrypted data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
                    {
                        // Process each individual encrypted data part
                        partLen = Convert.ToUInt32(part.Length);
                        rv = pkcs11.C_DecryptVerifyUpdate(session, encryptedPart, Convert.ToUInt32(bytesRead), part, ref partLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append decrypted data part to the output stream
                        outputStream.Write(part, 0, Convert.ToInt32(partLen));
                    }

                    // Get the length of last decrypted data part in first call
                    byte[] lastPart = null;
                    uint lastPartLen = 0;
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
                    outputStream.Write(lastPart, 0, Convert.ToInt32(lastPartLen));
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    decryptedData = outputStream.ToArray();

                    // Verify signature
                    rv = pkcs11.C_VerifyFinal(session, signature, Convert.ToUInt32(signature.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }
                
                // Do something interesting with decrypted data and verification result
                Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));
                
                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter (iv in this case)
                UnmanagedMemory.Free(ref encryptionMechanism.Parameter);
                encryptionMechanism.ParameterLen = 0;

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

