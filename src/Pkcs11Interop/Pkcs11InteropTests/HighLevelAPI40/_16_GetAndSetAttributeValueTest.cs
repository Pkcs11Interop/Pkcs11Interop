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
    /// GetAttributeValue and SetAttributeValue tests.
    /// </summary>
    [TestFixture()]
    public class _16_GetAndSetAttributeValueTest
    {
        /// <summary>
        /// GetAttributeValue test.
        /// </summary>
        [Test()]
        public void _01_GetAttributeValueTest()
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
                    
                    // Create object
                    ObjectHandle objectHandle = Helpers.CreateDataObject(session);

                    // Prepare list of empty attributes we want to read
                    List<CKA> attributes = new List<CKA>();
                    attributes.Add(CKA.CKA_LABEL);
                    attributes.Add(CKA.CKA_VALUE);

                    // Get value of specified attributes
                    List<ObjectAttribute> objectAttributes = session.GetAttributeValue(objectHandle, attributes);

                    // Do something interesting with attribute value
                    Assert.IsTrue(objectAttributes[0].GetValueAsString() == Settings.ApplicationName);

                    session.DestroyObject(objectHandle);
                    session.Logout();
                }
            }
        }

        /// <summary>
        /// GetAttributeValue test for invalid type of attribute.
        /// </summary>
        [Test()]
        public void _02_GetInvalidAttributeValueTest()
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
                    
                    // Prepare list of empty attributes we want to read
                    List<CKA> attributes = new List<CKA>();
                    attributes.Add(CKA.CKA_LABEL);
                    attributes.Add(CKA.CKA_VALUE);
                    
                    // Get value of specified attributes
                    List<ObjectAttribute> objectAttributes = session.GetAttributeValue(privateKey, attributes);
                    
                    // Do something interesting with attribute value
                    Assert.IsTrue(objectAttributes[0].GetValueAsString() == Settings.ApplicationName);
                    Assert.IsTrue(objectAttributes[1].CannotBeRead == true);
                    
                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
        
        /// <summary>
        /// SetAttributeValue test.
        /// </summary>
        [Test()]
        public void _03_SetAttributeValueTest()
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
                    
                    // Create object
                    ObjectHandle objectHandle = Helpers.CreateDataObject(session);

                    // Prepare list of attributes we want to set
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, Settings.ApplicationName + "_2"));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_VALUE, "New data object content"));

                    // Set attributes
                    session.SetAttributeValue(objectHandle, objectAttributes);

                    // Do something interesting with modified object

                    session.DestroyObject(objectHandle);
                    session.Logout();
                }
            }
        }
    }
}

