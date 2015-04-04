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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI81;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI81
{
    /// <summary>
    /// WrapKey and UnwrapKey tests.
    /// </summary>
    [TestFixture()]
    public class _24_WrapAndUnwrapKeyTest
    {
        /// <summary>
        /// Basic WrapKey and UnwrapKey test.
        /// </summary>
        [Test()]
        public void _01_BasicWrapAndUnwrapKeyTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
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
                    
                    // Generate asymetric key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Generate symetric key
                    ObjectHandle secretKey = Helpers.GenerateKey(session);

                    // Specify wrapping mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);

                    // Wrap key
                    byte[] wrappedKey = session.WrapKey(mechanism, publicKey, secretKey);

                    // Do something interesting with wrapped key
                    Assert.IsNotNull(wrappedKey);

                    // Define attributes for unwrapped key
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_ENCRYPT, true));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_DECRYPT, true));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_DERIVE, true));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_EXTRACTABLE, true));

                    // Unwrap key
                    ObjectHandle unwrappedKey = session.UnwrapKey(mechanism, privateKey, wrappedKey, objectAttributes);

                    // Do something interesting with unwrapped key
                    Assert.IsTrue(unwrappedKey.ObjectId != CK.CK_INVALID_HANDLE);

                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.DestroyObject(secretKey);
                    session.Logout();
                }
            }
        }
    }
}

