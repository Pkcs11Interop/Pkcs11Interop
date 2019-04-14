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
    /// GenerateKey and GenerateKeyPair tests.
    /// </summary>
    [TestFixture()]
    public class _18_GenerateKeyAndKeyPairTest
    {
        /// <summary>
        /// GenerateKey test.
        /// </summary>
        [Test()]
        public void _01_GenerateKeyTest()
        {
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11LibraryFactory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);

                    // Prepare attribute template of new key
                    List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ENCRYPT, true));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_DECRYPT, true));

                    // Specify key generation mechanism
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_DES3_KEY_GEN);

                    // Generate key
                    IObjectHandle objectHandle = session.GenerateKey(mechanism, objectAttributes);

                    // Do something interesting with generated key

                    // Destroy object
                    session.DestroyObject(objectHandle);
                    
                    session.Logout();
                }
            }
        }
        
        /// <summary>
        /// GenerateKeyPair test.
        /// </summary>
        [Test()]
        public void _02_GenerateKeyPairTest()
        {
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11LibraryFactory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);

                    // The CKA_ID attribute is intended as a means of distinguishing multiple key pairs held by the same subject
                    byte[] ckaId = session.GenerateRandom(20);

                    // Prepare attribute template of new public key
                    List<IObjectAttribute> publicKeyAttributes = new List<IObjectAttribute>();
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, true));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_PRIVATE, false));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_LABEL, Settings.ApplicationName));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ID, ckaId));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ENCRYPT, true));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_VERIFY, true));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_VERIFY_RECOVER, true));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_WRAP, true));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_MODULUS_BITS, 1024));
                    publicKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_PUBLIC_EXPONENT, new byte[] { 0x01, 0x00, 0x01 }));

                    // Prepare attribute template of new private key
                    List<IObjectAttribute> privateKeyAttributes = new List<IObjectAttribute>();
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_PRIVATE, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_LABEL, Settings.ApplicationName));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ID, ckaId));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_SENSITIVE, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_DECRYPT, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_SIGN, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_SIGN_RECOVER, true));
                    privateKeyAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_UNWRAP, true));

                    // Specify key generation mechanism
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_RSA_PKCS_KEY_PAIR_GEN);

                    // Generate key pair
                    IObjectHandle publicKeyHandle = null;
                    IObjectHandle privateKeyHandle = null;
                    session.GenerateKeyPair(mechanism, publicKeyAttributes, privateKeyAttributes, out publicKeyHandle, out privateKeyHandle);

                    // Do something interesting with generated key pair
                    
                    // Destroy keys
                    session.DestroyObject(privateKeyHandle);
                    session.DestroyObject(publicKeyHandle);

                    session.Logout();
                }
            }
        }
    }
}
