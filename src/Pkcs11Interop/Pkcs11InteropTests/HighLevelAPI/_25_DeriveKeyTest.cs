/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
                {
                    // Login as normal user
                    session.Login(CKU.CKU_USER, Settings.NormalUserPin);
                    
                    // Generate symetric key
                    IObjectHandle baseKey = Helpers.GenerateKey(session);

                    // Generate random data needed for key derivation
                    byte[] data = session.GenerateRandom(24);

                    // Specify mechanism parameters
                    ICkKeyDerivationStringData mechanismParams = Settings.Factories.MechanismParamsFactory.CreateCkKeyDerivationStringData(data);

                    // Specify derivation mechanism with parameters
                    IMechanism mechanism = Settings.Factories.MechanismFactory.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, mechanismParams);
                    
                    // Derive key
                    IObjectHandle derivedKey = session.DeriveKey(mechanism, baseKey, null);

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
