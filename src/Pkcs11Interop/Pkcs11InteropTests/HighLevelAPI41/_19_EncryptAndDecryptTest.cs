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

using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI41;
using Net.Pkcs11Interop.HighLevelAPI41.MechanismParams;
using NUnit.Framework;
using NativeLongUtils = Net.Pkcs11Interop.LowLevelAPI41.NativeLongUtils;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
{
    /// <summary>
    /// Encryption and decryption tests.
    /// </summary>
    [TestFixture()]
    public class _19_EncryptAndDecryptTest
    {
        /// <summary>
        /// Single-part encryption and decryption test.
        /// </summary>
        [Test()]
        public void _01_EncryptAndDecryptSinglePartTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);

                    // Generate symetric key
                    ObjectHandle generatedKey = Helpers.GenerateKey(session);

                    // Generate random initialization vector
                    byte[] iv = session.GenerateRandom(8);

                    // Specify encryption mechanism with initialization vector as parameter
                    Mechanism mechanism = new Mechanism(CKM.CKM_DES3_CBC, iv);

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");

                    // Encrypt data
                    byte[] encryptedData = session.Encrypt(mechanism, generatedKey, sourceData);

                    // Do something interesting with encrypted data
                    
                    // Decrypt data
                    byte[] decryptedData = session.Decrypt(mechanism, generatedKey, encryptedData);

                    // Do something interesting with decrypted data
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));

                    session.DestroyObject(generatedKey);
                    session.Logout();
                }
            }
        }

        /// <summary>
        /// Multi-part encryption and decryption test.
        /// </summary>
        [Test()]
        public void _02_EncryptAndDecryptMultiPartTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate symetric key
                    ObjectHandle generatedKey = Helpers.GenerateKey(session);
                    
                    // Generate random initialization vector
                    byte[] iv = session.GenerateRandom(8);
                    
                    // Specify encryption mechanism with initialization vector as parameter
                    Mechanism mechanism = new Mechanism(CKM.CKM_DES3_CBC, iv);

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");
                    byte[] encryptedData = null;
                    byte[] decryptedData = null;
                    
                    // Multipart encryption can be used i.e. for encryption of streamed data
                    using (MemoryStream inputStream = new MemoryStream(sourceData), outputStream = new MemoryStream())
                    {
                        // Encrypt data
                        // Note that in real world application we would rather use bigger read buffer i.e. 4096
                        session.Encrypt(mechanism, generatedKey, inputStream, outputStream, 8);

                        // Read whole output stream to the byte array so we can compare results more easily
                        encryptedData = outputStream.ToArray();
                    }
                    
                    // Do something interesting with encrypted data
                    
                    // Multipart decryption can be used i.e. for decryption of streamed data
                    using (MemoryStream inputStream = new MemoryStream(encryptedData), outputStream = new MemoryStream())
                    {
                        // Decrypt data
                        // Note that in real world application we would rather use bigger read buffer i.e. 4096
                        session.Decrypt(mechanism, generatedKey, inputStream, outputStream, 8);

                        // Read whole output stream to the byte array so we can compare results more easily
                        decryptedData = outputStream.ToArray();
                    }
                    
                    // Do something interesting with decrypted data
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));

                    session.DestroyObject(generatedKey);
                    session.Logout();
                }
            }
        }

        /// <summary>
        /// Single-part encryption and decryption test with CKM_RSA_PKCS_OAEP mechanism.
        /// </summary>
        [Test()]
        public void _03_EncryptAndDecryptSinglePartOaepTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);

                    // Generate key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);

                    // Specify mechanism parameters
                    CkRsaPkcsOaepParams mechanismParams = new CkRsaPkcsOaepParams(NativeLongUtils.ConvertFromCKM(CKM.CKM_SHA_1), NativeLongUtils.ConvertFromCKG(CKG.CKG_MGF1_SHA1), NativeLongUtils.ConvertFromUInt32(CKZ.CKZ_DATA_SPECIFIED), null);

                    // Specify encryption mechanism with parameters
                    Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS_OAEP, mechanismParams);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    
                    // Encrypt data
                    byte[] encryptedData = session.Encrypt(mechanism, publicKey, sourceData);
                    
                    // Do something interesting with encrypted data
                    
                    // Decrypt data
                    byte[] decryptedData = session.Decrypt(mechanism, privateKey, encryptedData);
                    
                    // Do something interesting with decrypted data
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));
                    
                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
    }
}
