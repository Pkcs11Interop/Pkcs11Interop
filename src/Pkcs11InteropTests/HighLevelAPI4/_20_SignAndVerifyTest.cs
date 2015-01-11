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

using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI4;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI4
{
    /// <summary>
    /// Signing and verification (where the signature is an appendix to the data) tests.
    /// </summary>
    [TestFixture()]
    public class _20_SignAndVerifyTest
    {
        /// <summary>
        /// Single-part signing and verification test.
        /// </summary>
        [Test()]
        public void _01_SignAndVerifySinglePartTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

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
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");

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
        public void _02_SignAndVerifyMultiPartTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

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

                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
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

