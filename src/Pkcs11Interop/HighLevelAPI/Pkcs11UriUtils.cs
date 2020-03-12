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
using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, ILibraryInfo libraryInfo)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, libraryInfo.ManufacturerId, libraryInfo.LibraryDescription, libraryInfo.LibraryVersion);
        }

        /// <summary>
        /// Checks whether slot information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="slotInfo">Slot information</param>
        /// <returns>True if slot information matches PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, ISlotInfo slotInfo)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, slotInfo.ManufacturerId, slotInfo.SlotDescription, slotInfo.SlotId);
        }

        /// <summary>
        /// Checks whether token information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="tokenInfo">Token information</param>
        /// <returns>True if token information matches PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, ITokenInfo tokenInfo)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            return Pkcs11UriSharedUtils.Matches(pkcs11Uri, tokenInfo.Label, tokenInfo.ManufacturerId, tokenInfo.SerialNumber, tokenInfo.Model);
        }

        /// <summary>
        /// Checks whether object attributes match PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="objectAttributes">Object attributes</param>
        /// <returns>True if object attributes match PKCS#11 URI</returns>
        public static bool Matches(Pkcs11Uri pkcs11Uri, List<IObjectAttribute> objectAttributes)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (objectAttributes == null)
                throw new ArgumentNullException("objectAttributes");

            ulong ckaClassType = Convert.ToUInt64(CKA.CKA_CLASS);
            CKO? ckaClassValue = null;
            bool ckaClassFound = false;

            ulong ckaLabelType = Convert.ToUInt64(CKA.CKA_LABEL);
            string ckaLabelValue = null;
            bool ckaLabelFound = false;

            ulong ckaIdType = Convert.ToUInt64(CKA.CKA_ID);
            byte[] ckaIdValue = null;
            bool ckaIdFound = false;

            foreach (IObjectAttribute objectAttribute in objectAttributes)
            {
                if (objectAttribute == null)
                    continue;

                if (objectAttribute.Type == ckaClassType)
                {
                    ckaClassValue = (CKO)Convert.ToUInt32(objectAttribute.GetValueAsUlong());
                    ckaClassFound = true;
                }
                else if (objectAttribute.Type == ckaLabelType)
                {
                    ckaLabelValue = objectAttribute.GetValueAsString();
                    ckaLabelFound = true;
                }
                else if (objectAttribute.Type == ckaIdType)
                {
                    ckaIdValue = objectAttribute.GetValueAsByteArray();
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
        /// Obtains a list of all PKCS#11 URI matching slots
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="pkcs11Library">High level PKCS#11 wrapper</param>
        /// <param name="slotsType">Type of slots to be obtained</param>
        /// <returns>List of slots matching PKCS#11 URI</returns>
        public static List<ISlot> GetMatchingSlotList(Pkcs11Uri pkcs11Uri, IPkcs11Library pkcs11Library, SlotsType slotsType)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Library == null)
                throw new ArgumentNullException("pkcs11Library");

            List<ISlot> matchingSlots = new List<ISlot>();

            ILibraryInfo libraryInfo = pkcs11Library.GetInfo();
            if (!Matches(pkcs11Uri, libraryInfo))
                return matchingSlots;

            List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);
            if ((slots == null) || (slots.Count == 0))
                return matchingSlots;

            foreach (ISlot slot in slots)
            {
                ISlotInfo slotInfo = slot.GetSlotInfo();
                if (Matches(pkcs11Uri, slotInfo))
                {
                    if (slotInfo.SlotFlags.TokenPresent)
                    {
                        ITokenInfo tokenInfo = slot.GetTokenInfo();
                        if (Matches(pkcs11Uri, tokenInfo))
                            matchingSlots.Add(slot);
                    }
                    else
                    {
                        if (slotsType == SlotsType.WithOrWithoutTokenPresent && Pkcs11UriSharedUtils.Matches(pkcs11Uri, null, null, null, null))
                            matchingSlots.Add(slot);
                    }
                }
            }

            return matchingSlots;
        }

        /// <summary>
        /// Returns list of object attributes defined by PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="objectAttributeFactory">Factory for creation of IObjectAttribute instances</param>
        /// <returns>List of object attributes defined by PKCS#11 URI</returns>
        public static List<IObjectAttribute> GetObjectAttributes(Pkcs11Uri pkcs11Uri, IObjectAttributeFactory objectAttributeFactory)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (objectAttributeFactory == null)
                throw new ArgumentNullException("objectAttributeFactory");

            List<IObjectAttribute> attributes = null;

            if (pkcs11Uri.DefinesObject)
            {
                attributes = new List<IObjectAttribute>();
                if (pkcs11Uri.Type != null)
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_CLASS, pkcs11Uri.Type.Value));
                if (pkcs11Uri.Object != null)
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_LABEL, pkcs11Uri.Object));
                if (pkcs11Uri.Id != null)
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_ID, pkcs11Uri.Id));
            }

            return attributes;
        }
    }
}
