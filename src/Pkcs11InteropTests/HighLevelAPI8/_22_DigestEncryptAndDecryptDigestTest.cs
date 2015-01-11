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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
