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

using Net.Pkcs11Interop.Common;
using System;
using System.Collections.Generic;

namespace Net.Pkcs11Interop.LowLevelAPI80
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
        public static bool Matches(Pkcs11Uri pkcs11Uri, CK_SLOT_INFO slotInfo, ulong? slotId)
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

            uint ckaClassType = (uint)CKA.CKA_CLASS;
            CKO? ckaClassValue = null;
            bool ckaClassFound = false;

            uint ckaLabelType = (uint)CKA.CKA_LABEL;
            string ckaLabelValue = null;
            bool ckaLabelFound = false;

            uint ckaIdType = (uint)CKA.CKA_ID;
            byte[] ckaIdValue = null;
            bool ckaIdFound = false;

            foreach (CK_ATTRIBUTE objectAttribute in objectAttributes)
            {
                CK_ATTRIBUTE attribute = objectAttribute;

                if (attribute.type == ckaClassType)
                {
                    ulong ulongValue = 0;
                    CkaUtils.ConvertValue(ref attribute, out ulongValue);
                    ckaClassValue = (CKO)Convert.ToUInt32(ulongValue);
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
        /// <param name="pkcs11">Low level PKCS#11 wrapper</param>
        /// <param name="tokenPresent">Flag indicating whether the list obtained includes only those slots with a token present (true), or all slots (false)</param>
        /// <param name="slotList">List of slots matching PKCS#11 URI</param>
        /// <returns>CKR_OK if successful; any other value otherwise</returns>
        public static CKR GetMatchingSlotList(Pkcs11Uri pkcs11Uri, Pkcs11 pkcs11, bool tokenPresent, out ulong[] slotList)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11 == null)
                throw new ArgumentNullException("pkcs11");

            List<ulong> matchingSlots = new List<ulong>();

            // Get library information
            CK_INFO libraryInfo = new CK_INFO();
            CKR rv = pkcs11.C_GetInfo(ref libraryInfo);
            if (rv != CKR.CKR_OK)
            {
                slotList = new ulong[0];
                return rv;
            }

            // Check whether library matches URI
            if (!Matches(pkcs11Uri, libraryInfo))
            {
                slotList = new ulong[0];
                return CKR.CKR_OK;
            }

            // Get number of slots in first call
            ulong slotCount = 0;
            rv = pkcs11.C_GetSlotList(false, null, ref slotCount);
            if (rv != CKR.CKR_OK)
            {
                slotList = new ulong[0];
                return rv;
            }

            if (slotCount < 1)
            {
                slotList = new ulong[0];
                return CKR.CKR_OK;
            }

            // Allocate array for slot IDs
            ulong[] slots = new ulong[slotCount];

            // Get slot IDs in second call
            rv = pkcs11.C_GetSlotList(tokenPresent, slots, ref slotCount);
            if (rv != CKR.CKR_OK)
            {
                slotList = new ulong[0];
                return rv;
            }

            // Shrink array if needed
            if (slots.Length != Convert.ToInt32(slotCount))
                Array.Resize(ref slots, Convert.ToInt32(slotCount));

            // Match slots with Pkcs11Uri
            foreach (ulong slot in slots)
            {
                CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
                rv = pkcs11.C_GetSlotInfo(slot, ref slotInfo);
                if (rv != CKR.CKR_OK)
                {
                    slotList = new ulong[0];
                    return rv;
                }

                // Check whether slot matches URI
                if (Matches(pkcs11Uri, slotInfo, slot))
                {
                    if ((slotInfo.Flags & CKF.CKF_TOKEN_PRESENT) == CKF.CKF_TOKEN_PRESENT)
                    {
                        CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                        rv = pkcs11.C_GetTokenInfo(slot, ref tokenInfo);
                        if (rv != CKR.CKR_OK)
                        {
                            slotList = new ulong[0];
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

            List<CK_ATTRIBUTE> attributes = null;

            if (pkcs11Uri.DefinesObject)
            {
                attributes = new List<CK_ATTRIBUTE>();
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
