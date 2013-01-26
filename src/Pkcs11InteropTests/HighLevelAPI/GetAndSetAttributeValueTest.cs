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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetAttributeValue and SetAttributeValue tests.
    /// </summary>
    [TestFixture()]
    public class GetAndSetAttributeValueTest
    {
        /// <summary>
        /// GetAttributeValue test.
        /// </summary>
        [Test()]
        public void GetAttributeValueTest()
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
        public void GetInvalidAttributeValueTest()
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
        public void SetAttributeValueTest()
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

