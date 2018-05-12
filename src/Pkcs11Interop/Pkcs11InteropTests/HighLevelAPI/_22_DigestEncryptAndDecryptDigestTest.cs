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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// DigestEncrypt and DecryptDigest tests.
    /// </summary>
    [TestFixture()]
    public class _22_DigestEncryptAndDecryptDigestTest
    {
        /// <summary>
        /// Basic DigestEncrypt and DecryptDigest test.
        /// </summary>
        [Test()]
        public void _01_BasicDigestEncryptAndDecryptDigestTest()
        {
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate symetric key
                    IObjectHandle generatedKey = Helpers.GenerateKey(session);
                    
                    // Generate random initialization vector
                    byte[] iv = session.GenerateRandom(8);

                    // Specify encryption mechanism with initialization vector as parameter
                    IMechanism encryptionMechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_DES3_CBC, iv);

                    // Specify digesting mechanism
                    IMechanism digestingMechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_SHA_1);

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");

                    // Encrypt and digest data
                    byte[] digest1 = null;
                    byte[] encryptedData = null;
                    session.DigestEncrypt(digestingMechanism, encryptionMechanism, generatedKey, sourceData, out digest1, out encryptedData);

                    // Do something interesting with encrypted data and digest

                    // Decrypt and digest data
                    byte[] digest2 = null;
                    byte[] decryptedData = null;
                    session.DecryptDigest(digestingMechanism, encryptionMechanism, generatedKey, encryptedData, out digest2, out decryptedData);

                    // Do something interesting with decrypted data and digest
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));
                    Assert.IsTrue(Convert.ToBase64String(digest1) == Convert.ToBase64String(digest2));

                    session.DestroyObject(generatedKey);
                    session.Logout();
                }
            }
        }
    }
}
