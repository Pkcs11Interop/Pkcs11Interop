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

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Signing and verification (where the data can be recovered from the signature) tests.
    /// </summary>
    [TestFixture()]
    public class _21_SignAndVerifyRecoverTest
    {
        /// <summary>
        /// Signing and verification test.
        /// </summary>
        [Test()]
        public void _01_BasicSignAndVerifyRecoverTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
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
                    Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);
                    
                    byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                    
                    // Sign data
                    byte[] signature = session.SignRecover(mechanism, privateKey, sourceData);
                    
                    // Do something interesting with signature

                    // Verify signature
                    bool isValid = false;
                    byte[] recoveredData = session.VerifyRecover(mechanism, publicKey, signature, out isValid);

                    // Do something interesting with verification result and recovered data
                    Assert.IsTrue(isValid);
                    Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(recoveredData));

                    session.DestroyObject(privateKey);
                    session.DestroyObject(publicKey);
                    session.Logout();
                }
            }
        }
    }
}
