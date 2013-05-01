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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// WrapKey and UnwrapKey tests.
    /// </summary>
    [TestFixture()]
    public class WrapAndUnwrapKeyTest
    {
        /// <summary>
        /// Basic WrapKey and UnwrapKey test.
        /// </summary>
        [Test()]
        public void BasicWrapAndUnwrapKeyTest()
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
                    
                    // Generate asymetric key pair
                    ObjectHandle publicKey = null;
                    ObjectHandle privateKey = null;
                    Helpers.GenerateKeyPair(session, out publicKey, out privateKey);
                    
                    // Generate symetric key
                    ObjectHandle secretKey = Helpers.GenerateKey(session);

                    // Specify wrapping mechanism
                    Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);

                    // Wrap key
                    byte[] wrappedKey = session.WrapKey(mechanism, publicKey, secretKey);

                    // Do something interesting with wrapped key
                    Assert.IsNotNull(wrappedKey);

                    // Unwrap key
                    ObjectHandle unwrappedKey = session.UnwrapKey(mechanism, privateKey, wrappedKey, null);

                    // Do something interesting with unwrapped key
                    Assert.IsTrue(unwrappedKey.ObjectId != CK.CK_INVALID_HANDLE);

                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.DestroyObject(secretKey);
                    session.Logout();
                }
            }
        }
    }
}

