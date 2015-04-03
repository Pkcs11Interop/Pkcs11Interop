/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using Net.Pkcs11Interop.HighLevelAPI8.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
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
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
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
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));

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
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
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
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));

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
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);

                    // Generate key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);

                    // Specify mechanism parameters
                    CkRsaPkcsOaepParams mechanismParams = new CkRsaPkcsOaepParams((ulong)CKM.CKM_SHA_1, (ulong)CKG.CKG_MGF1_SHA1, (ulong)CKZ.CKZ_DATA_SPECIFIED, null);

                    // Specify encryption mechanism with parameters
                    Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS_OAEP, mechanismParams);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    
                    // Encrypt data
                    byte[] encryptedData = session.Encrypt(mechanism, publicKey, sourceData);
                    
                    // Do something interesting with encrypted data
                    
                    // Decrypt data
                    byte[] decryptedData = session.Decrypt(mechanism, privateKey, encryptedData);
                    
                    // Do something interesting with decrypted data
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));
                    
                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
    }
}
