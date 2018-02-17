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
using Net.Pkcs11Interop.LowLevelAPI80;
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;
using NUnit.Framework;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// C_EncryptInit, C_Encrypt, C_EncryptUpdate, C_EncryptFinish, C_DecryptInit, C_Decrypt, C_DecryptUpdate and C_DecryptFinish tests.
    /// </summary>
    [TestFixture()]
    public class _20_EncryptAndDecryptTest
    {
        /// <summary>
        /// C_EncryptInit, C_Encrypt, C_DecryptInit and C_Decrypt test.
        /// </summary>
        [Test()]
        public void _01_EncryptAndDecryptSinglePartTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeLongUtils.ConvertFromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                NativeULong keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Generate random initialization vector
                byte[] iv = new byte[8];
                rv = pkcs11.C_GenerateRandom(session, iv, NativeLongUtils.ConvertFromInt32(iv.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify encryption mechanism with initialization vector as parameter.
                // Note that CkmUtils.CreateMechanism() automaticaly copies iv into newly allocated unmanaged memory.
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_CBC, iv);
                
                // Initialize encryption operation
                rv = pkcs11.C_EncryptInit(session, ref mechanism, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");

                // Get length of encrypted data in first call
                NativeULong encryptedDataLen = 0;
                rv = pkcs11.C_Encrypt(session, sourceData, NativeLongUtils.ConvertFromInt32(sourceData.Length), null, ref encryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(encryptedDataLen > 0);

                // Allocate array for encrypted data
                byte[] encryptedData = new byte[encryptedDataLen];

                // Get encrypted data in second call
                rv = pkcs11.C_Encrypt(session, sourceData, NativeLongUtils.ConvertFromInt32(sourceData.Length), encryptedData, ref encryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with encrypted data

                // Initialize decryption operation
                rv = pkcs11.C_DecryptInit(session, ref mechanism, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get length of decrypted data in first call
                NativeULong decryptedDataLen = 0;
                rv = pkcs11.C_Decrypt(session, encryptedData, NativeLongUtils.ConvertFromInt32(encryptedData.Length), null, ref decryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(decryptedDataLen > 0);
                
                // Allocate array for decrypted data
                byte[] decryptedData = new byte[decryptedDataLen];

                // Get decrypted data in second call
                rv = pkcs11.C_Decrypt(session, encryptedData, NativeLongUtils.ConvertFromInt32(encryptedData.Length), decryptedData, ref decryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with decrypted data
                Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));

                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter (iv in this case)
                UnmanagedMemory.Free(ref mechanism.Parameter);
                mechanism.ParameterLen = 0;

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

        /// <summary>
        /// C_EncryptInit, C_EncryptUpdate, C_EncryptFinish, C_DecryptInit, C_DecryptUpdate and C_DecryptFinish test.
        /// </summary>
        [Test()]
        public void _02_EncryptAndDecryptMultiPartTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeLongUtils.ConvertFromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                NativeULong keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate random initialization vector
                byte[] iv = new byte[8];
                rv = pkcs11.C_GenerateRandom(session, iv, NativeLongUtils.ConvertFromInt32(iv.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify encryption mechanism with initialization vector as parameter.
                // Note that CkmUtils.CreateMechanism() automaticaly copies iv into newly allocated unmanaged memory.
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_CBC, iv);
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");
                byte[] encryptedData = null;
                byte[] decryptedData = null;

                // Multipart encryption functions C_EncryptUpdate and C_EncryptFinal can be used i.e. for encryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData), outputStream = new MemoryStream())
                {
                    // Initialize encryption operation
                    rv = pkcs11.C_EncryptInit(session, ref mechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];

                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    NativeULong encryptedPartLen = NativeLongUtils.ConvertFromInt32(encryptedPart.Length);

                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Encrypt each individual source data part
                        encryptedPartLen = NativeLongUtils.ConvertFromInt32(encryptedPart.Length);
                        rv = pkcs11.C_EncryptUpdate(session, part, NativeLongUtils.ConvertFromInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());

                        // Append encrypted data part to the output stream
                        outputStream.Write(encryptedPart, 0, NativeLongUtils.ConvertToInt32(encryptedPartLen));
                    }

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
                    outputStream.Write(lastEncryptedPart, 0, NativeLongUtils.ConvertToInt32(lastEncryptedPartLen));

                    // Read whole output stream to the byte array so we can compare results more easily
                    encryptedData = outputStream.ToArray();
                }

                // Do something interesting with encrypted data

                // Multipart decryption functions C_DecryptUpdate and C_DecryptFinal can be used i.e. for decryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(encryptedData), outputStream = new MemoryStream())
                {
                    // Initialize decryption operation
                    rv = pkcs11.C_DecryptInit(session, ref mechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];

                    // Prepare buffer for decrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    NativeULong partLen = NativeLongUtils.ConvertFromInt32(part.Length);

                    // Read input stream with encrypted data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
                    {
                        // Decrypt each individual encrypted data part
                        partLen = NativeLongUtils.ConvertFromInt32(part.Length);
                        rv = pkcs11.C_DecryptUpdate(session, encryptedPart, NativeLongUtils.ConvertFromInt32(bytesRead), part, ref partLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());

                        // Append decrypted data part to the output stream
                        outputStream.Write(part, 0, NativeLongUtils.ConvertToInt32(partLen));
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
                    outputStream.Write(lastPart, 0, NativeLongUtils.ConvertToInt32(lastPartLen));

                    // Read whole output stream to the byte array so we can compare results more easily
                    decryptedData = outputStream.ToArray();
                }

                // Do something interesting with decrypted data
                Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));

                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter (iv in this case)
                UnmanagedMemory.Free(ref mechanism.Parameter);
                mechanism.ParameterLen = 0;
                
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
        
        /// <summary>
        /// C_EncryptInit, C_Encrypt, C_DecryptInit and C_Decrypt test with CKM_RSA_PKCS_OAEP mechanism.
        /// </summary>
        [Test()]
        public void _03_EncryptAndDecryptSinglePartOaepTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeLongUtils.ConvertFromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                NativeULong pubKeyId = CK.CK_INVALID_HANDLE;
                NativeULong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify mechanism parameters
                CK_RSA_PKCS_OAEP_PARAMS mechanismParams = new CK_RSA_PKCS_OAEP_PARAMS();
                mechanismParams.HashAlg = NativeLongUtils.ConvertFromCKM(CKM.CKM_SHA_1);
                mechanismParams.Mgf = NativeLongUtils.ConvertFromCKG(CKG.CKG_MGF1_SHA1);
                mechanismParams.Source = NativeLongUtils.ConvertFromUInt32(CKZ.CKZ_DATA_SPECIFIED);
                mechanismParams.SourceData = IntPtr.Zero;
                mechanismParams.SourceDataLen = 0;
                
                // Specify encryption mechanism with parameters
                // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory.
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS_OAEP, mechanismParams);
                
                // Initialize encryption operation
                rv = pkcs11.C_EncryptInit(session, ref mechanism, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                
                // Get length of encrypted data in first call
                NativeULong encryptedDataLen = 0;
                rv = pkcs11.C_Encrypt(session, sourceData, NativeLongUtils.ConvertFromInt32(sourceData.Length), null, ref encryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(encryptedDataLen > 0);
                
                // Allocate array for encrypted data
                byte[] encryptedData = new byte[encryptedDataLen];
                
                // Get encrypted data in second call
                rv = pkcs11.C_Encrypt(session, sourceData, NativeLongUtils.ConvertFromInt32(sourceData.Length), encryptedData, ref encryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with encrypted data
                
                // Initialize decryption operation
                rv = pkcs11.C_DecryptInit(session, ref mechanism, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Get length of decrypted data in first call
                NativeULong decryptedDataLen = 0;
                rv = pkcs11.C_Decrypt(session, encryptedData, NativeLongUtils.ConvertFromInt32(encryptedData.Length), null, ref decryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(decryptedDataLen > 0);
                
                // Allocate array for decrypted data
                byte[] decryptedData = new byte[decryptedDataLen];
                
                // Get decrypted data in second call
                rv = pkcs11.C_Decrypt(session, encryptedData, NativeLongUtils.ConvertFromInt32(encryptedData.Length), decryptedData, ref decryptedDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Array may need to be shrinked
                if (decryptedData.Length != NativeLongUtils.ConvertToInt32(decryptedDataLen))
                    Array.Resize(ref decryptedData, NativeLongUtils.ConvertToInt32(decryptedDataLen));

                // Do something interesting with decrypted data
                Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));

                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter
                UnmanagedMemory.Free(ref mechanism.Parameter);
                mechanism.ParameterLen = 0;
                
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
