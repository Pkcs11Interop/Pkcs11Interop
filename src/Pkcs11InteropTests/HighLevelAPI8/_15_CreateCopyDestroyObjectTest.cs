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
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
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
            if (UnmanagedLong.Size != 8)
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
            if (UnmanagedLong.Size != 8)
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
            if (UnmanagedLong.Size != 8)
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
                    
                    // Create object
                    ObjectHandle objectHandle = Helpers.CreateDataObject(session);

                    // Determine object size
                    ulong objectSize = session.GetObjectSize(objectHandle);

                    // Do something interesting with object size
                    Assert.IsTrue(objectSize > 0);
                    
                    session.DestroyObject(objectHandle);
                    session.Logout();
                }
            }
        }
    }
}
