/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI41;
using Net.Pkcs11Interop.HighLevelAPI41.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
{
    /// <summary>
    /// DeriveKey tests.
    /// </summary>
    [TestFixture()]
    public class _25_DeriveKeyTest
    {
        /// <summary>
        /// DeriveKey test.
        /// </summary>
        [Test()]
        public void _01_BasicDeriveKeyTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
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

