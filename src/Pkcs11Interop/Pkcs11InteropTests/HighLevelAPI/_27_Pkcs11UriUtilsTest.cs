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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Pkcs11UriUtils tests
    /// </summary>
    [TestFixture()]
    public partial class _27_Pkcs11UriUtilsTest
    {
        /// <summary>
        /// Demonstration of PKCS#11 URI usage in a signature creation application
        /// </summary>
        [Test()]
        public void _01_Pkcs11UriInSignatureCreationApplication()
        {
            // PKCS#11 URI can be acquired i.e. from configuration file as a simple string...
            string uri = @"<pkcs11:serial=7BFF2737350B262C;
                            type=private;
                            object=John%20Doe
                            ?module-path=pkcs11.dll&
                            pin-value=11111111>";

            Assert.IsNotNull(uri);

            // ...or it can be easily constructed with Pkcs11UriBuilder
            Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
            pkcs11UriBuilder.Serial = "7BFF2737350B262C";
            pkcs11UriBuilder.Type = CKO.CKO_PRIVATE_KEY;
            pkcs11UriBuilder.Object = "John Doe";
            pkcs11UriBuilder.ModulePath = "pkcs11.dll";
            pkcs11UriBuilder.PinValue = "11111111";
            uri = pkcs11UriBuilder.ToString();

            Assert.IsNotNull(uri);

            // Warning: Please note that PIN stored in PKCS#11 URI can pose a security risk and therefore other options
            //          should be carefully considered. For example an application may ask for a PIN with a GUI dialog etc.

            // Use PKCS#11 URI acquired from Settings class to identify private key in signature creation method
            byte[] signature = SignData(ConvertUtils.Utf8StringToBytes("Hello world"), Settings.PrivateKeyUri);

            // Do something interesting with the signature
            Assert.IsNotNull(signature);
        }

        /// <summary>
        /// Creates the PKCS#1 v1.5 RSA signature with SHA-1 mechanism
        /// </summary>
        /// <param name="data">Data that should be signed</param>
        /// <param name="uri">PKCS#11 URI identifying PKCS#11 library, token and private key</param>
        /// <returns>PKCS#1 v1.5 RSA signature</returns>
        private byte[] SignData(byte[] data, string uri)
        {
            // Verify input parameters
            if (data == null)
                throw new ArgumentNullException("data");

            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri");

            // Parse PKCS#11 URI
            Pkcs11Uri pkcs11Uri = new Pkcs11Uri(uri);

            // Verify that URI contains all information required to perform this operation
            if (pkcs11Uri.ModulePath == null)
                throw new Exception("PKCS#11 URI does not specify PKCS#11 library");

            if (pkcs11Uri.PinValue == null)
                throw new Exception("PKCS#11 URI does not specify PIN");

            if (!pkcs11Uri.DefinesObject || pkcs11Uri.Type != CKO.CKO_PRIVATE_KEY)
                throw new Exception("PKCS#11 URI does not specify private key");

            // Load and initialize PKCS#11 library specified by URI
            using (IPkcs11 pkcs11 = Pkcs11Factory.Instance.CreatePkcs11(pkcs11Uri.ModulePath, AppType.MultiThreaded))
            {
                // Obtain a list of all slots with tokens that match URI
                List<ISlot> slots = Pkcs11UriUtils.GetMatchingSlotList(pkcs11Uri, pkcs11, SlotsType.WithTokenPresent);
                if ((slots == null) || (slots.Count == 0))
                    throw new Exception("None of the slots matches PKCS#11 URI");

                // Open read only session with first token that matches URI
                using (ISession session = slots[0].OpenSession(SessionType.ReadOnly))
                {
                    // Login as normal user with PIN acquired from URI
                    session.Login(CKU.CKU_USER, pkcs11Uri.PinValue);

                    // Get list of object attributes for the private key specified by URI
                    List<IObjectAttribute> searchTemplate = Pkcs11UriUtils.GetObjectAttributes(pkcs11Uri, ObjectAttributeFactory.Instance);

                    // Find private key specified by URI
                    List<IObjectHandle> foundObjects = session.FindAllObjects(searchTemplate);
                    if ((foundObjects == null) || (foundObjects.Count == 0))
                        throw new Exception("None of the private keys match PKCS#11 URI");

                    // Create signature with the private key specified by URI
                    return session.Sign(MechanismFactory.Instance.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS), foundObjects[0], data);
                }
            }
        }
    }
}
