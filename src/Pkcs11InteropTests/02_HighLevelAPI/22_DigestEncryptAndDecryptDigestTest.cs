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
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using System.Text;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// DigestEncrypt and DecryptDigest tests.
    /// </summary>
    [TestFixture()]
    public class DigestEncryptAndDecryptDigestTest
    {
        /// <summary>
        /// Basic DigestEncrypt and DecryptDigest test.
        /// </summary>
        [Test()]
        public void BasicDigestEncryptAndDecryptDigestTest()
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
                    
                    // Generate symetric key
                    ObjectHandle generatedKey = Helpers.GenerateKey(session);
                    
                    // Generate random initialization vector
                    byte[] iv = session.GenerateRandom(8);

                    // Specify encryption mechanism with initialization vector as parameter
                    Mechanism encryptionMechanism = new Mechanism(CKM.CKM_DES3_CBC, iv);

                    // Specify digesting mechanism
                    Mechanism digestingMechanism = new Mechanism(CKM.CKM_SHA_1);

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
