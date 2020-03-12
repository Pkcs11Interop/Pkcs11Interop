/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Utility class connecting PKCS#11 URI and Pkcs11Interop types
    /// </summary>
    public static class Pkcs11UriUtils
    {
        /// <summary>
        /// Checks whether PKCS#11 library information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="libraryInfo">PKCS#11 library information</param>
        /// <returns>True if PKCS#11 library information matches PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, CK_INFO libraryInfo)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            string manufacturer = ConvertUtils.BytesToUtf8String(libraryInfo.ManufacturerId, true);
            string description = ConvertUtils.BytesToUtf8String(libraryInfo.LibraryDescription, true);
            string version = libraryInfo.LibraryVersion.ToString();

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, manufacturer, description, version);
        }

        /// <summary>
        /// Checks whether slot information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="slotInfo">Slot information</param>
        /// <param name="slotId">Slot identifier</param>
        /// <returns>True if slot information matches PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, CK_SLOT_INFO slotInfo, NativeULong? slotId)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            string manufacturer = ConvertUtils.BytesToUtf8String(slotInfo.ManufacturerId, true);
            string description = ConvertUtils.BytesToUtf8String(slotInfo.SlotDescription, true);

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, manufacturer, description, slotId);
        }

        /// <summary>
        /// Checks whether token information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="tokenInfo">Token information</param>
        /// <returns>True if token information matches PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, CK_TOKEN_INFO tokenInfo)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            string token = ConvertUtils.BytesToUtf8String(tokenInfo.Label, true);
            string manufacturer = ConvertUtils.BytesToUtf8String(tokenInfo.ManufacturerId, true);
            string serial = ConvertUtils.BytesToUtf8String(tokenInfo.SerialNumber, true);
            string model = ConvertUtils.BytesToUtf8String(tokenInfo.Model, true);

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, token, manufacturer, serial, model);
        }

        /// <summary>
        /// Checks whether object attributes match PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="objectAttributes">Object attributes</param>
        /// <returns>True if object attributes match PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, List<CK_ATTRIBUTE> objectAttributes)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (objectAttributes == null)
                throw new ArgumentNullException("objectAttributes");

            NativeULong ckaClassType = ConvertUtils.UInt32FromCKA(CKA.CKA_CLASS);
            CKO? ckaClassValue = null;
            bool ckaClassFound = false;

            NativeULong ckaLabelType = ConvertUtils.UInt32FromCKA(CKA.CKA_LABEL);
            string ckaLabelValue = null;
            bool ckaLabelFound = false;

            NativeULong ckaIdType = ConvertUtils.UInt32FromCKA(CKA.CKA_ID);
            byte[] ckaIdValue = null;
            bool ckaIdFound = false;

            foreach (CK_ATTRIBUTE objectAttribute in objectAttributes)
            {
                CK_ATTRIBUTE attribute = objectAttribute;

                if (attribute.type == ckaClassType)
                {
                    NativeULong nativeUlongValue = 0;
                    CkaUtils.ConvertValue(ref attribute, out nativeUlongValue);
                    ckaClassValue = ConvertUtils.UInt32ToCKO(nativeUlongValue);
                    ckaClassFound = true;
                }
                else if (attribute.type == ckaLabelType)
                {
                    CkaUtils.ConvertValue(ref attribute, out ckaLabelValue);
                    ckaLabelFound = true;
                }
                else if (objectAttribute.type == ckaIdType)
                {
                    CkaUtils.ConvertValue(ref attribute, out ckaIdValue);
                    ckaIdFound = true;
                }

                if (ckaClassFound && ckaLabelFound && ckaIdFound)
                    break;
            }

            if ((!ckaClassFound) && (pkcs11Uri.Type != null))
                throw new Pkcs11UriException("CKA_CLASS attribute is not present in the list of object attributes");

            if ((!ckaLabelFound) && (pkcs11Uri.Object != null))
                throw new Pkcs11UriException("CKA_LABEL attribute is not present in the list of object attributes");

            if ((!ckaIdFound) && (pkcs11Uri.Id != null))
                throw new Pkcs11UriException("CKA_ID attribute is not present in the list of object attributes");

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, ckaClassValue, ckaLabelValue, ckaIdValue);
        }

        /// <summary>
        /// Obtains a list of all slots where token that matches PKCS#11 URI is present
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="pkcs11Library">Low level PKCS#11 wrapper</param>
        /// <param name="tokenPresent">Flag indicating whether the list obtained includes only those slots with a token present (true), or all slots (false)</param>
        /// <param name="slotList">List of slots matching PKCS#11 URI</param>
        /// <returns>CKR_OK if successful; any other value otherwise</returns>
        public static CKR GetMatchingSlotList(Pkcs11Uri pkcs11Uri, Pkcs11Library pkcs11Library, bool tokenPresent, out NativeULong[] slotList)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Library == null)
                throw new ArgumentNullException("pkcs11Library");

            List<NativeULong> matchingSlots = new List<NativeULong>();

            // Get library information
            CK_INFO libraryInfo = new CK_INFO();
            CKR rv = pkcs11Library.C_GetInfo(ref libraryInfo);
            if (rv != CKR.CKR_OK)
            {
                slotList = new NativeULong[0];
                return rv;
            }

            // Check whether library matches URI
            if (!Matches(pkcs11Uri, libraryInfo))
            {
                slotList = new NativeULong[0];
                return CKR.CKR_OK;
            }

            // Get number of slots in first call
            NativeULong slotCount = 0;
            rv = pkcs11Library.C_GetSlotList(false, null, ref slotCount);
            if (rv != CKR.CKR_OK)
            {
                slotList = new NativeULong[0];
                return rv;
            }

            if (slotCount < 1)
            {
                slotList = new NativeULong[0];
                return CKR.CKR_OK;
            }

            // Allocate array for slot IDs
            NativeULong[] slots = new NativeULong[slotCount];

            // Get slot IDs in second call
            rv = pkcs11Library.C_GetSlotList(tokenPresent, slots, ref slotCount);
            if (rv != CKR.CKR_OK)
            {
                slotList = new NativeULong[0];
                return rv;
            }

            // Shrink array if needed
            if (slots.Length != ConvertUtils.UInt32ToInt32(slotCount))
                Array.Resize(ref slots, ConvertUtils.UInt32ToInt32(slotCount));

            // Match slots with Pkcs11Uri
            foreach (NativeULong slot in slots)
            {
                CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
                rv = pkcs11Library.C_GetSlotInfo(slot, ref slotInfo);
                if (rv != CKR.CKR_OK)
                {
                    slotList = new NativeULong[0];
                    return rv;
                }

                // Check whether slot matches URI
                if (Matches(pkcs11Uri, slotInfo, slot))
                {
                    if ((slotInfo.Flags & CKF.CKF_TOKEN_PRESENT) == CKF.CKF_TOKEN_PRESENT)
                    {
                        CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                        rv = pkcs11Library.C_GetTokenInfo(slot, ref tokenInfo);
                        if (rv != CKR.CKR_OK)
                        {
                            slotList = new NativeULong[0];
                            return rv;
                        }

                        // Check whether token matches URI
                        if (Matches(pkcs11Uri, tokenInfo))
                            matchingSlots.Add(slot);
                    }
                    else
                    {
                        if (!tokenPresent && Pkcs11UriSharedUtils.Matches(pkcs11Uri, null, null, null, null))
                            matchingSlots.Add(slot);
                    }
                }
            }

            slotList = matchingSlots.ToArray();
            return CKR.CKR_OK;
        }

        /// <summary>
        /// Returns list of object attributes defined by PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="objectAttributes">List of object attributes defined by PKCS#11 URI</param>
        public static void GetObjectAttributes(Pkcs11Uri pkcs11Uri, out CK_ATTRIBUTE[] objectAttributes)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            List<CK_ATTRIBUTE> attributes = new List<CK_ATTRIBUTE>();

            if (pkcs11Uri.DefinesObject)
            {
                if (pkcs11Uri.Type != null)
                    attributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, pkcs11Uri.Type.Value));
                if (pkcs11Uri.Object != null)
                    attributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, pkcs11Uri.Object));
                if (pkcs11Uri.Id != null)
                    attributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, pkcs11Uri.Id));
            }

            objectAttributes = attributes.ToArray();
        }
    }
}
