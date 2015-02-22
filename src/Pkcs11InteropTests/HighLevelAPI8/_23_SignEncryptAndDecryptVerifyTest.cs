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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
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
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));
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
