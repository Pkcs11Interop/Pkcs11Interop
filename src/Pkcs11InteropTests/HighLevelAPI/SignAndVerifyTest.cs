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
using Net.Pkcs11Interop.Common;
using System.Text;
using System.IO;
using Net.Pkcs11Interop.HighLevelAPI;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Signing and verification (where the signature is an appendix to the data) tests.
    /// </summary>
    [TestFixture()]
    public class SignAndVerifyTest
    {
        /// <summary>
        /// Single-part signing and verification test.
        /// </summary>
        [Test()]
        public void SignAndVerifySinglePartTest()
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
                    
                    // Specify signing mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA1_RSA_PKCS);
                    
                    byte[] sourceData = UTF8Encoding.UTF8.GetBytes("Hello world");

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
        public void SignAndVerifyMultiPartTest()
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
                    
                    // Specify signing mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_SHA1_RSA_PKCS);

                    byte[] sourceData = UTF8Encoding.UTF8.GetBytes("Hello world");
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

