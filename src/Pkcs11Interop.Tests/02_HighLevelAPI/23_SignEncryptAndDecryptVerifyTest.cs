/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using System.Text;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// SignEncrypt and DecryptVerify tests.
    /// </summary>
    [TestFixture()]
    public class SignEncryptAndDecryptVerifyTest
    {
        /// <summary>
        /// Basic SignEncrypt and DecryptVerify test.
        /// </summary>
        [Test()]
        public void BasicSignEncryptAndDecryptVerifyTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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
