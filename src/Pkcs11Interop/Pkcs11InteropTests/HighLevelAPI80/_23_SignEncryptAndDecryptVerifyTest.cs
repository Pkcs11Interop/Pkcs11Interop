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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI80;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI80
{
    /// <summary>
    /// SignEncrypt and DecryptVerify tests.
    /// </summary>
    [TestFixture()]
    public class _23_SignEncryptAndDecryptVerifyTest
    {
        /// <summary>
        /// Basic SignEncrypt and DecryptVerify test.
        /// </summary>
        [Test()]
        public void _01_BasicSignEncryptAndDecryptVerifyTest()
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

                    // Generate asymetric key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Specify signing mechanism
                    Mechanism signingMechanism = new Mechanism(CKM.CKM_SHA1_RSA_PKCS);

                    // Generate symetric key
                    ObjectHandle secretKey = Helpers.GenerateKey(session);

                    // Generate random initialization vector
                    byte[] iv = session.GenerateRandom(8);
                    
                    // Specify encryption mechanism with initialization vector as parameter
                    Mechanism encryptionMechanism = new Mechanism(CKM.CKM_DES3_CBC, iv);

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Passw0rd");

                    // Sign and encrypt data
                    byte[] signature = null;
                    byte[] encryptedData = null;
                    session.SignEncrypt(signingMechanism, privateKey, encryptionMechanism, secretKey, sourceData, out signature, out encryptedData);
                    
                    // Do something interesting with signature and encrypted data
                    
                    // Decrypt data and verify signature of data
                    byte[] decryptedData = null;
                    bool isValid = false;
                    session.DecryptVerify(signingMechanism, publicKey, encryptionMechanism, secretKey, encryptedData, signature, out decryptedData, out isValid);

                    // Do something interesting with decrypted data and verification result
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(sourceData) == ConvertUtils.BytesToBase64String(decryptedData));
                    Assert.IsTrue(isValid);

                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.DestroyObject(secretKey);
                    session.Logout();
                }
            }
        }
    }
}
