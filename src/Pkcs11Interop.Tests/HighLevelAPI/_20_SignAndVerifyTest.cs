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

using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Signing and verification (where the signature is an appendix to the data) tests.
    /// </summary>
    [TestFixture()]
    public class _20_SignAndVerifyTest
    {
        /// <summary>
        /// Single-part signing and verification test.
        /// </summary>
        [Test()]
        public void _01_SignAndVerifySinglePartTest()
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
                    
                    // Generate key pair
                    IObjectHandle publicKey = null;
                    IObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Specify signing mechanism
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");

                    // Sign data
                    byte[] signature = session.Sign(mechanism, privateKey, sourceData);

                    // Do something interesting with signature

                    // Verify signature
                    bool isValid = false;
                    session.Verify(mechanism, publicKey, sourceData, signature, out isValid);

                    // Do something interesting with verification result
                    Assert.IsTrue(isValid);

                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
        
        /// <summary>
        /// Multi-part signing and verification test.
        /// </summary>
        [Test()]
        public void _02_SignAndVerifyMultiPartTest()
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
                    
                    // Generate key pair
                    IObjectHandle publicKey = null;
                    IObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Specify signing mechanism
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    byte[] signature = null;
                    bool isValid = false;
                    
                    // Multipart signing can be used i.e. for signing of streamed data
                    using (MemoryStream inputStream = new MemoryStream(sourceData))
                    {
                        // Sign data
                        // Note that in real world application we would rather use bigger read buffer i.e. 4096
                        signature = session.Sign(mechanism, privateKey, inputStream, 8);
                    }

                    // Do something interesting with signature

                    // Multipart verification can be used i.e. for signature verification of streamed data
                    using (MemoryStream inputStream = new MemoryStream(sourceData))
                    {
                        // Verify signature
                        // Note that in real world application we would rather use bigger read buffer i.e. 4096
                        session.Verify(mechanism, publicKey, inputStream, signature, out isValid, 8);
                    }

                    // Do something interesting with verification result
                    Assert.IsTrue(isValid);
                    
                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
    }
}
