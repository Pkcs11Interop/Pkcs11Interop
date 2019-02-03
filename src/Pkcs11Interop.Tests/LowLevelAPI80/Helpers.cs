/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// Helper methods for LowLevelAPI tests.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Checks whether test can be executed on this platform
        /// </summary>
        public static void CheckPlatform()
        {
            if (Platform.NativeULongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");
        }

        /// <summary>
        /// Finds slot containing the token that matches criteria specified in Settings class
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <returns>Slot containing the token that matches criteria</returns>
        public static NativeULong GetUsableSlot(Pkcs11 pkcs11)
        {
            CKR rv = CKR.CKR_OK;

            // Get list of available slots with token present
            NativeULong slotCount = 0;
            rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
            if (rv != CKR.CKR_OK)
                Assert.Fail(rv.ToString());

            Assert.IsTrue(slotCount > 0);

            NativeULong[] slotList = new NativeULong[slotCount];

            rv = pkcs11.C_GetSlotList(true, slotList, ref slotCount);
            if (rv != CKR.CKR_OK)
                Assert.Fail(rv.ToString());

            // Return first slot with token present when both TokenSerial and TokenLabel are null...
            if (Settings.TokenSerial == null && Settings.TokenLabel == null)
                return slotList[0];

            // First slot with token present is OK...
            NativeULong? matchingSlot = slotList[0];

            // ...unless there are matching criteria specified in Settings class
            if (Settings.TokenSerial != null || Settings.TokenLabel != null)
            {
                matchingSlot = null;

                foreach (NativeULong slot in slotList)
                {
                    CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                    rv = pkcs11.C_GetTokenInfo(slot, ref tokenInfo);
                    if (rv != CKR.CKR_OK)
                    {
                        if (rv == CKR.CKR_TOKEN_NOT_RECOGNIZED || rv == CKR.CKR_TOKEN_NOT_PRESENT)
                            continue;
                        else
                            Assert.Fail(rv.ToString());
                    }

                    if (!string.IsNullOrEmpty(Settings.TokenSerial))
                        if (0 != string.Compare(Settings.TokenSerial, ConvertUtils.BytesToUtf8String(tokenInfo.SerialNumber, true), StringComparison.Ordinal))
                            continue;

                    if (!string.IsNullOrEmpty(Settings.TokenLabel))
                        if (0 != string.Compare(Settings.TokenLabel, ConvertUtils.BytesToUtf8String(tokenInfo.Label, true), StringComparison.Ordinal))
                            continue;

                    matchingSlot = slot;
                    break;
                }
            }

            Assert.IsTrue(matchingSlot != null, "Token matching criteria specified in Settings class is not present");
            return matchingSlot.Value;
        }

        /// <summary>
        /// Creates the data object.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='objectId'>Output parameter for data object handle</param>
        /// <returns>Return value of C_CreateObject</returns>
        public static CKR CreateDataObject(Pkcs11 pkcs11, NativeULong session, ref NativeULong objectId)
        {
            CKR rv = CKR.CKR_OK;

            // Prepare attribute template of new data object
            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[5];
            template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);
            template[1] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            template[2] = CkaUtils.CreateAttribute(CKA.CKA_APPLICATION, Settings.ApplicationName);
            template[3] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            template[4] = CkaUtils.CreateAttribute(CKA.CKA_VALUE, "Data object content");
            
            // Create object
            rv = pkcs11.C_CreateObject(session, template, ConvertUtils.UInt64FromInt32(template.Length), ref objectId);

            // In LowLevelAPI caller has to free unmanaged memory taken by attributes
            for (int i = 0; i < template.Length; i++)
            {
                UnmanagedMemory.Free(ref template[i].value);
                template[i].valueLen = 0;
            }

            return rv;
        }

        /// <summary>
        /// Generates symetric key.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='keyId'>Output parameter for key object handle</param>
        /// <returns>Return value of C_GenerateKey</returns>
        public static CKR GenerateKey(Pkcs11 pkcs11, NativeULong session, ref NativeULong keyId)
        {
            CKR rv = CKR.CKR_OK;

            // Prepare attribute template of new key
            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[6];
            template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_SECRET_KEY);
            template[1] = CkaUtils.CreateAttribute(CKA.CKA_KEY_TYPE, CKK.CKK_DES3);
            template[2] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
            template[3] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
            template[4] = CkaUtils.CreateAttribute(CKA.CKA_DERIVE, true);
            template[5] = CkaUtils.CreateAttribute(CKA.CKA_EXTRACTABLE, true);
            
            // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_KEY_GEN);
            
            // Generate key
            rv = pkcs11.C_GenerateKey(session, ref mechanism, template, ConvertUtils.UInt64FromInt32(template.Length), ref keyId);

            // In LowLevelAPI we have to free unmanaged memory taken by attributes
            for (int i = 0; i < template.Length; i++)
            {
                UnmanagedMemory.Free(ref template[i].value);
                template[i].valueLen = 0;
            }

            return rv;
        }

        /// <summary>
        /// Generates asymetric key pair.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='pubKeyId'>Output parameter for public key object handle</param>
        /// <param name='privKeyId'>Output parameter for private key object handle</param>
        /// <returns>Return value of C_GenerateKeyPair</returns>
        public static CKR GenerateKeyPair(Pkcs11 pkcs11, NativeULong session, ref NativeULong pubKeyId, ref NativeULong privKeyId)
        {
            CKR rv = CKR.CKR_OK;

            // The CKA_ID attribute is intended as a means of distinguishing multiple key pairs held by the same subject
            byte[] ckaId = new byte[20];
            rv = pkcs11.C_GenerateRandom(session, ckaId, ConvertUtils.UInt64FromInt32(ckaId.Length));
            if (rv != CKR.CKR_OK)
                return rv;
            
            // Prepare attribute template of new public key
            CK_ATTRIBUTE[] pubKeyTemplate = new CK_ATTRIBUTE[10];
            pubKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            pubKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, false);
            pubKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            pubKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
            pubKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
            pubKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY, true);
            pubKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY_RECOVER, true);
            pubKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_WRAP, true);
            pubKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_MODULUS_BITS, 1024);
            pubKeyTemplate[9] = CkaUtils.CreateAttribute(CKA.CKA_PUBLIC_EXPONENT, new byte[] { 0x01, 0x00, 0x01 });
            
            // Prepare attribute template of new private key
            CK_ATTRIBUTE[] privKeyTemplate = new CK_ATTRIBUTE[9];
            privKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            privKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, true);
            privKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            privKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
            privKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_SENSITIVE, true);
            privKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
            privKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_SIGN, true);
            privKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_SIGN_RECOVER, true);
            privKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_UNWRAP, true);
            
            // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS_KEY_PAIR_GEN);
            
            // Generate key pair
            rv = pkcs11.C_GenerateKeyPair(session, ref mechanism, pubKeyTemplate, ConvertUtils.UInt64FromInt32(pubKeyTemplate.Length), privKeyTemplate, ConvertUtils.UInt64FromInt32(privKeyTemplate.Length), ref pubKeyId, ref privKeyId);

            // In LowLevelAPI we have to free unmanaged memory taken by attributes
            for (int i = 0; i < privKeyTemplate.Length; i++)
            {
                UnmanagedMemory.Free(ref privKeyTemplate[i].value);
                privKeyTemplate[i].valueLen = 0;
            }
            
            for (int i = 0; i < pubKeyTemplate.Length; i++)
            {
                UnmanagedMemory.Free(ref pubKeyTemplate[i].value);
                pubKeyTemplate[i].valueLen = 0;
            }

            return rv;
        }
    }
}

