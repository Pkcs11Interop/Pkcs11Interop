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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// FindObjectsInit, FindObjects, FindObjectsFinal and FindAllObjects tests.
    /// </summary>
    [TestFixture()]
    public class _17_ObjectFindingTest
    {
        /// <summary>
        /// Basic FindObjectsInit, FindObjects and FindObjectsFinal test.
        /// </summary>
        [Test()]
        public void _01_BasicObjectFindingTest()
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
                    
                    // Let's create two objects so we can find something
                    IObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    IObjectHandle objectHandle2 = Helpers.CreateDataObject(session);

                    // Prepare attribute template that defines search criteria
                    List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, true));

                    // Initialize searching
                    session.FindObjectsInit(objectAttributes);

                    // Get search results
                    List<IObjectHandle> foundObjects = session.FindObjects(2);

                    // Terminate searching
                    session.FindObjectsFinal();

                    // Do something interesting with found objects
                    Assert.IsTrue(foundObjects.Count == 2);
                    Assert.IsTrue((foundObjects[0].ObjectId != CK.CK_INVALID_HANDLE) && (foundObjects[1].ObjectId != CK.CK_INVALID_HANDLE));

                    session.DestroyObject(objectHandle2);
                    session.DestroyObject(objectHandle1);
                    session.Logout();
                }
            }
        }
    
        /// <summary>
        /// FindAllObjects test.
        /// </summary>
        [Test()]
        public void _02_FindAllObjectsTest()
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
                    
                    // Let's create two objects so we can find something
                    IObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    IObjectHandle objectHandle2 = Helpers.CreateDataObject(session);
                    
                    // Prepare attribute template that defines search criteria
                    List<IObjectAttribute> objectAttributes = new List<IObjectAttribute>();
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA));
                    objectAttributes.Add(Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, true));
                    
                    // Find all objects that match provided attributes
                    List<IObjectHandle> foundObjects = session.FindAllObjects(objectAttributes);
                                        
                    // Do something interesting with found objects
                    Assert.IsTrue(foundObjects.Count >= 2);

                    session.DestroyObject(objectHandle2);
                    session.DestroyObject(objectHandle1);
                    session.Logout();
                }
            }
        }
    }
}
