/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;
using System.Collections.Generic;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// FindObjectsInit, FindObjects, FindObjectsFinal and FindAllObjects tests.
    /// </summary>
    [TestFixture()]
    public class ObjectFindingTest
    {
        /// <summary>
        /// Basic FindObjectsInit, FindObjects and FindObjectsFinal test.
        /// </summary>
        [Test()]
        public void BasicObjectFindingTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Let's create two objects so we can find something
                    ObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    ObjectHandle objectHandle2 = Helpers.CreateDataObject(session);

                    // Prepare attribute template that defines search criteria
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));

                    // Initialize searching
                    session.FindObjectsInit(objectAttributes);

                    // Get search results
                    List<ObjectHandle> foundObjects = session.FindObjects(2);

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
        public void FindAllObjectsTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (Session session = slot.OpenSession(false))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Let's create two objects so we can find something
                    ObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    ObjectHandle objectHandle2 = Helpers.CreateDataObject(session);
                    
                    // Prepare attribute template that defines search criteria
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA));
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));
                    
                    // Find all objects that match provided attributes
                    List<ObjectHandle> foundObjects = session.FindAllObjects(objectAttributes);
                                        
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
