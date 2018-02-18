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

using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI80;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI80
{
    /// <summary>
    /// Digesting tests.
    /// </summary>
    [TestFixture()]
    public class _12_DigestTest
    {
        /// <summary>
        /// Single-part digesting test.
        /// </summary>
        [Test()]
        public void _01_DigestSinglePartTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO session
                using (Session session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    
                    // Digest data
                    byte[] digest = session.Digest(mechanism, sourceData);
                    
                    // Do something interesting with digest value
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(digest) == "e1AsOh9IyGCa4hLN+2Od7jlnP14=");
                }
            }
        }

        /// <summary>
        /// Multi-part digesting test.
        /// </summary>
        [Test()]
        public void _02_DigestMultiPartTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO session
                using (Session session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    byte[] digest = null;
                    
                    // Multipart digesting can be used i.e. for digesting of streamed data
                    using (MemoryStream inputStream = new MemoryStream(sourceData))
                    {
                        // Digest data
                        digest = session.Digest(mechanism, inputStream);
                    }

                    // Do something interesting with digest value
                    Assert.IsTrue(ConvertUtils.BytesToBase64String(digest) == "e1AsOh9IyGCa4hLN+2Od7jlnP14=");
                }
            }
        }
        
        /// <summary>
        /// DigestKey test.
        /// </summary>
        [Test()]
        public void _03_DigestKeyTest()
        {
            Helpers.CheckPlatform();

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate symetric key
                    ObjectHandle generatedKey = Helpers.GenerateKey(session);

                    // Specify digesting mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA_1);
                    
                    // Digest key
                    byte[] digest = session.DigestKey(mechanism, generatedKey);
                    
                    // Do something interesting with digest value
                    Assert.IsNotNull(digest);

                    session.DestroyObject(generatedKey);
                    session.Logout();
                }
            }
        }
    }
}

