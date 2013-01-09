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
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;
using System.Collections.Generic;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// CreateObject, DestroyObject, CopyObject and GetObjectSize tests.
    /// </summary>
    [TestFixture()]
    public class CreateCopyDestroyObjectTest
    {
        /// <summary>
        /// CreateObject and DestroyObject test.
        /// </summary>
        [Test()]
        public void CreateDestroyObjectTest()
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
                    
                    // Prepare attribute template of new data object
                    List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
                    objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA));
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
        public void CopyObjectTest()
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
        public void GetObjectSizeTest()
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
