/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI81;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI81
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
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
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
                    
                    // Let's create two objects so we can find something
                    ObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    ObjectHandle objectHandle2 = Helpers.CreateDataObject(session);

                    // Prepare attribute template that defines search criteria
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA));
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
        public void _02_FindAllObjectsTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
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
                    
                    // Let's create two objects so we can find something
                    ObjectHandle objectHandle1 = Helpers.CreateDataObject(session);
                    ObjectHandle objectHandle2 = Helpers.CreateDataObject(session);
                    
                    // Prepare attribute template that defines search criteria
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA));
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
