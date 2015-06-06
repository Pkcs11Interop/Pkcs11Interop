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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class connecting PKCS#11 URI and Pkcs11Interop types
    /// </summary>
    internal static class Pkcs11UriSharedUtils
    {
        /// <summary>
        /// Checks whether PKCS#11 library information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="libraryManufacturer">PKCS#11 library manufacturer</param>
        /// <param name="libraryDescription">PKCS#11 library description</param>
        /// <param name="libraryVersion">PKCS#11 library version</param>
        /// <returns>True if PKCS#11 library information matches PKCS#11 URI</returns>
        internal static bool Matches(Pkcs11Uri pkcs11Uri, string libraryManufacturer, string libraryDescription, string libraryVersion)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Uri.UnknownPathAttributes.Count > 0)
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.LibraryManufacturer, libraryManufacturer))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.LibraryDescription, libraryDescription))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.LibraryVersion, libraryVersion))
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether slot information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="slotManufacturer">Slot manufacturer</param>
        /// <param name="slotDescription">Slot description</param>
        /// <param name="slotId">Slot identifier</param>
        /// <returns>True if slot information matches PKCS#11 URI</returns>
        internal static bool Matches(Pkcs11Uri pkcs11Uri, string slotManufacturer, string slotDescription, ulong? slotId)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Uri.UnknownPathAttributes.Count > 0)
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.SlotManufacturer, slotManufacturer))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.SlotDescription, slotDescription))
                return false;

            if (!SlotIdsMatch(pkcs11Uri.SlotId, slotId))
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether token information matches PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="tokenLabel">Token label</param>
        /// <param name="tokenManufacturer">Token manufacturer</param>
        /// <param name="tokenSerial">Token serial number</param>
        /// <param name="tokenModel">Token model</param>
        /// <returns>True if token information matches PKCS#11 URI</returns>
        internal static bool Matches(Pkcs11Uri pkcs11Uri, string tokenLabel, string tokenManufacturer, string tokenSerial, string tokenModel)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Uri.UnknownPathAttributes.Count > 0)
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.Token, tokenLabel))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.Manufacturer, tokenManufacturer))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.Serial, tokenSerial))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.Model, tokenModel))
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether object attributes match PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI</param>
        /// <param name="ckaClass">Value of CKA_CLASS object attribute</param>
        /// <param name="ckaLabel">Value of CKA_LABEL object attribute</param>
        /// <param name="ckaId">Value of CKA_ID object attribute</param>
        /// <returns>True if object attributes match PKCS#11 URI</returns>
        internal static bool Matches(Pkcs11Uri pkcs11Uri, CKO? ckaClass, string ckaLabel, byte[] ckaId)
        {
            if (pkcs11Uri == null)
                throw new ArgumentNullException("pkcs11Uri");

            if (pkcs11Uri.UnknownPathAttributes.Count > 0)
                return false;

            if (!ObjectTypesMatch(pkcs11Uri.Type, ckaClass))
                return false;

            if (!SimpleStringsMatch(pkcs11Uri.Object, ckaLabel))
                return false;

            if (!ByteArraysMatch(pkcs11Uri.Id, ckaId))
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether string matches the value of string attribute
        /// </summary>
        /// <param name="uriString">Value of string attribute present (or not) in PKCS#11 URI</param>
        /// <param name="inputString">String that should be compared with the value of string attribute</param>
        /// <returns>True if string matches the value of string attribute</returns>
        private static bool SimpleStringsMatch(string uriString, string inputString)
        {
            if (inputString == null)
            {
                if (uriString != null)
                    return false;
            }
            else
            {
                if (uriString != null)
                {
                    // No characters should be percent-encoded so there is no need to apply
                    // the case and the percent-encoding normalization specified in RFC3986
                    if (0 != string.Compare(uriString, inputString, StringComparison.Ordinal))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether type matches the value of "type" path attribute
        /// </summary>
        /// <param name="uriType">Value of "type" path attribute present (or not) in PKCS#11 URI</param>
        /// <param name="inputType">Type that should be compared with the value of "type" path attribute</param>
        /// <returns>True if type matches the value of "type" path attribute</returns>
        private static bool ObjectTypesMatch(CKO? uriType, CKO? inputType)
        {
            if (inputType == null)
            {
                if (uriType != null)
                    return false;
            }
            else
            {
                if (uriType != null)
                {
                    if (uriType.Value != inputType.Value)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether byte array matches the value of "id" path attribute
        /// </summary>
        /// <param name="uriArray">Value of "id" path attribute present (or not) in PKCS#11 URI</param>
        /// <param name="inputArray">Byte array that should be compared with the value of "id" path attribute</param>
        /// <returns>True if byte array matches the value of "id" path attribute</returns>
        private static bool ByteArraysMatch(byte[] uriArray, byte[] inputArray)
        {
            if (inputArray == null)
            {
                if (uriArray != null)
                    return false;
            }
            else
            {
                if (uriArray != null)
                {
                    if (uriArray.Length != inputArray.Length)
                        return false;

                    for (int i = 0; i < uriArray.Length; i++)
                    {
                        if (uriArray[i] != inputArray[i])
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether id matches the value of "slot-id" path attribute
        /// </summary>
        /// <param name="uriId">Value of "slot-id" path attribute present (or not) in PKCS#11 URI</param>
        /// <param name="inputId">Id that should be compared with the value of "slot-id" path attribute</param>
        /// <returns>True if id matches the value of "slot-id" path attribute</returns>
        private static bool SlotIdsMatch(ulong? uriId, ulong? inputId)
        {
            if (inputId == null)
            {
                if (uriId != null)
                    return false;
            }
            else
            {
                if (uriId != null)
                {
                    if (uriId.Value != inputId.Value)
                        return false;
                }
            }

            return true;
        }
    }
}
