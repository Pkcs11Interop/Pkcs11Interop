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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// DeriveKey tests.
    /// </summary>
    [TestFixture()]
    public class DeriveKeyTest
    {
        /// <summary>
        /// DeriveKey test.
        /// </summary>
        [Test()]
        public void BasicDeriveKeyTest()
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
                    
                    // Generate symetric key
                    ObjectHandle baseKey = Helpers.GenerateKey(session);

                    // Generate random data needed for key derivation
                    byte[] data = session.GenerateRandom(24);

                    // Specify mechanism parameters
                    CkKeyDerivationStringData mechanismParams = new CkKeyDerivationStringData(data);

                    // Specify derivation mechanism with parameters
                    Mechanism mechanism = new Mechanism(CKM.CKM_XOR_BASE_AND_DATA, mechanismParams);
                    
                    // Derive key
                    ObjectHandle derivedKey = session.DeriveKey(mechanism, baseKey, null);

                    // Do something interesting with derived key
                    Assert.IsTrue(derivedKey.ObjectId != CK.CK_INVALID_HANDLE);

                    session.DestroyObject(baseKey);
                    session.DestroyObject(derivedKey);
                    session.Logout();
                }
            }
        }
    }
}

