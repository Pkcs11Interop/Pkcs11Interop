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
using Net.Pkcs11Interop.HighLevelAPI41;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
{
    /// <summary>
    /// CreateObject, DestroyObject, CopyObject and GetObjectSize tests.
    /// </summary>
    [TestFixture()]
    public class _15_CreateCopyDestroyObjectTest
    {
        /// <summary>
        /// CreateObject and DestroyObject test.
        /// </summary>
        [Test()]
        public void _01_CreateDestroyObjectTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
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
                    
                    // Prepare attribute template of new data object
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_APPLICATION, Settings.ApplicationName));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, Settings.ApplicationName));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_VALUE, "Data object content"));

                    // Create object
                    ObjectHandle objectHandle = session.CreateObject(objectAttributes);

                    // Do something interesting with new object

                    // Destroy object
                    session.DestroyObject(objectHandle);

                    session.Logout();
                }
            }
        }
        
        /// <summary>
        /// CopyObject test.
        /// </summary>
        [Test()]
        public void _02_CopyObjectTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
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
                    
                    // Create object that can be copied
                    ObjectHandle objectHandle = Helpers.CreateDataObject(session);

                    // Copy object
                    ObjectHandle copiedObjectHandle = session.CopyObject(objectHandle, null);

                    // Do something interesting with new object

                    session.DestroyObject(copiedObjectHandle);
                    session.DestroyObject(objectHandle);
                    session.Logout();
                }
            }
        }
        
        /// <summary>
        /// GetObjectSize test.
        /// </summary>
        [Test()]
        public void _03_GetObjectSizeTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
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

                    // Determine object size
                    uint objectSize = session.GetObjectSize(objectHandle);

                    // Do something interesting with object size
                    Assert.IsTrue(objectSize > 0);
                    
                    session.DestroyObject(objectHandle);
                    session.Logout();
                }
            }
        }
    }
}
