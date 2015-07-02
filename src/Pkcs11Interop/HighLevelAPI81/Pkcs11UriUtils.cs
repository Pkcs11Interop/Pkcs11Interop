/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI81
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, LibraryInfo libraryInfo)
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, SlotInfo slotInfo)
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, TokenInfo tokenInfo)
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, List<ObjectAttribute> objectAttributes)
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

            foreach (ObjectAttribute objectAttribute in objectAttributes)
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
        /// <param name="pkcs11">High level PKCS#11 wrapper</param>
        /// <param name="tokenPresent">Flag indicating whether the list obtained includes only those slots with a token present (true), or all slots (false)</param>
        /// <returns>List of slots matching PKCS#11 URI</returns>
        public static List<Slot> GetMatchingSlotList(Pkcs11Uri pkcs11Uri, Pkcs11 pkcs11, bool tokenPresent)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11 == null)
                throw new ArgumentNullException("pkcs11");

            List<Slot> matchingSlots = new List<Slot>();

            LibraryInfo libraryInfo = pkcs11.GetInfo();
            if (!Matches(pkcs11Uri, libraryInfo))
                return matchingSlots;

            List<Slot> slots = pkcs11.GetSlotList(false);
            if ((slots == null) || (slots.Count == 0))
                return slots;

            foreach (Slot slot in slots)
            {
                SlotInfo slotInfo = slot.GetSlotInfo();
                if (Matches(pkcs11Uri, slotInfo))
                {
                    if (slotInfo.SlotFlags.TokenPresent)
                    {
                        TokenInfo tokenInfo = slot.GetTokenInfo();
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

            return matchingSlots;
        }

        /// <summary>
        /// Returns list of object attributes defined by PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="objectAttributes">List of object attributes defined by PKCS#11 URI</param>
        public static void GetObjectAttributes(Pkcs11Uri pkcs11Uri, out List<ObjectAttribute> objectAttributes)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            List<ObjectAttribute> attributes = null;

            if (pkcs11Uri.DefinesObject)
            {
                attributes = new List<ObjectAttribute>();
                if (pkcs11Uri.Type != null)
                    attributes.Add(new ObjectAttribute(CKA.CKA_CLASS, pkcs11Uri.Type.Value));
                if (pkcs11Uri.Object != null)
                    attributes.Add(new ObjectAttribute(CKA.CKA_LABEL, pkcs11Uri.Object));
                if (pkcs11Uri.Id != null)
                    attributes.Add(new ObjectAttribute(CKA.CKA_ID, pkcs11Uri.Id));
            }

            objectAttributes = attributes;
        }
    }
}
