/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI40;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI40
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
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Specify signing mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA1_RSA_PKCS);
                    
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
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Specify signing mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA1_RSA_PKCS);

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

