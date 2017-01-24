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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI40;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI40
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
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)
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

