/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
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
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate asymetric key pair
                    IObjectHandle publicKey = null;
                    IObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Generate symetric key
                    IObjectHandle secretKey = Helpers.GenerateKey(session);

                    // Specify wrapping mechanism
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_RSA_PKCS);

                    // Wrap key
                    byte[] wrappedKey = session.WrapKey(mechanism, publicKey, secretKey);

                    // Do something interesting with wrapped key
                    Assert.IsNotNull(wrappedKey);

                    // Define attributes for unwrapped key
                    List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ENCRYPT, true));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_DECRYPT, true));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_DERIVE, true));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_EXTRACTABLE, true));

                    // Unwrap key
                    IObjectHandle unwrappedKey = session.UnwrapKey(mechanism, privateKey, wrappedKey, objectAttributes);

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
