/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI40;
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
{
    /// <summary>
    /// Pkcs11UriUtils tests
    /// </summary>
    [TestFixture()]
    public partial class _28_Pkcs11UriUtilsTest
    {
        /// <summary>
        /// Demonstration of PKCS#11 URI usage in a signature creation application
        /// </summary>
        [Test()]
        public void _01_Pkcs11UriInSignatureCreationApplication()
        {
            Helpers.CheckPlatform();

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
            CKR rv = CKR.CKR_OK;

            using (Pkcs11Library pkcs11Library = new Pkcs11Library(pkcs11Uri.ModulePath, true))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());

                // Obtain a list of all slots with tokens that match URI
                NativeULong[] slots = null;
                
                rv = Pkcs11UriUtils.GetMatchingSlotList(pkcs11Uri, pkcs11Library, true, out slots);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                if ((slots == null) || (slots.Length == 0))
                    throw new Exception("None of the slots matches PKCS#11 URI");

                // Open read only session with first token that matches URI
                NativeULong session = CK.CK_INVALID_HANDLE;

                rv = pkcs11Library.C_OpenSession(slots[0], (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Login as normal user with PIN acquired from URI
                byte[] pinValue = ConvertUtils.Utf8StringToBytes(pkcs11Uri.PinValue);

                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, pinValue, ConvertUtils.UInt32FromInt32(pinValue.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get list of object attributes for the private key specified by URI
                CK_ATTRIBUTE[] attributes = null;

                Pkcs11UriUtils.GetObjectAttributes(pkcs11Uri, out attributes);

                // Find private key specified by URI
                NativeULong foundObjectCount = 0;
                NativeULong[] foundObjectIds = new NativeULong[] { CK.CK_INVALID_HANDLE };

                rv = pkcs11Library.C_FindObjectsInit(session, attributes, ConvertUtils.UInt32FromInt32(attributes.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_FindObjects(session, foundObjectIds, ConvertUtils.UInt32FromInt32(foundObjectIds.Length), ref foundObjectCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_FindObjectsFinal(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                if ((foundObjectCount == 0) || (foundObjectIds[0] == CK.CK_INVALID_HANDLE))
                    throw new Exception("None of the private keys match PKCS#11 URI");

                // Create signature with the private key specified by URI
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                rv = pkcs11Library.C_SignInit(session, ref mechanism, foundObjectIds[0]);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                NativeULong signatureLen = 0;

                rv = pkcs11Library.C_Sign(session, data, ConvertUtils.UInt32FromInt32(data.Length), null, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(signatureLen > 0);

                byte[] signature = new byte[signatureLen];

                rv = pkcs11Library.C_Sign(session, data, ConvertUtils.UInt32FromInt32(data.Length), signature, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                if (signature.Length != ConvertUtils.UInt32ToInt32(signatureLen))
                    Array.Resize(ref signature, ConvertUtils.UInt32ToInt32(signatureLen));

                // Release PKCS#11 resources
                rv = pkcs11Library.C_Logout(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                return signature;
            }
        }
    }
}
