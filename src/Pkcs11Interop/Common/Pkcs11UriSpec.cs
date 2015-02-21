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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Definitions from the PKCS#11 URI scheme specification
    /// </summary>
    internal static class Pkcs11UriSpec
    {
        /// <summary>
        /// Characters allowed in value of path attribute
        /// </summary>
        internal static readonly char[] Pk11PathAttrValueChars = null;

        /// <summary>
        /// Characters allowed in name of vendor specific attribute
        /// </summary>
        internal static readonly char[] Pk11VendorAttrNameChars = null;

        /// <summary>
        /// Characters allowed in value of query attribute
        /// </summary>
        internal static readonly char[] Pk11QueryAttrValueChars = null;

        /// <summary>
        /// PKCS#11 URI scheme name
        /// </summary>
        internal const string Pk11UriSchemeName = "pkcs11";
        
        /// <summary>
        /// Character that always follows after PKCS#11 URI scheme name
        /// </summary>
        internal const string Pk11UriAndPathSeparator = ":";
        
        /// <summary>
        /// Character that separates path attributes
        /// </summary>
        internal const string Pk11PathAttributesSeparator = ";";
        
        /// <summary>
        /// Character that separates name and value of path attribute
        /// </summary>
        internal const string Pk11PathAttributeNameAndValueSeparator = "=";
        
        /// <summary>
        /// Character that separates path and query parts
        /// </summary>
        internal const string Pk11PathAndQuerySeparator = "?";

        /// <summary>
        /// Character that separates query attributes
        /// </summary>
        internal const string Pk11QueryAttributesSeparator = "&";

        /// <summary>
        /// Character that separates name and value of query attribute
        /// </summary>
        internal const string Pk11QueryAttributeNameAndValueSeparator = "=";

        /// <summary>
        /// Name of "token" path attribute
        /// </summary>
        internal const string Pk11Token = "token";

        /// <summary>
        /// Max length of "token" path attribute in bytes
        /// </summary>
        internal const int Pk11TokenMaxLength = 32;

        /// <summary>
        /// Name of "manufacturer" path attribute
        /// </summary>
        internal const string Pk11Manuf = "manufacturer";

        /// <summary>
        /// Max length of "manufacturer" path attribute in bytes
        /// </summary>
        internal const int Pk11ManufMaxLength = 32;

        /// <summary>
        /// Name of "serial" path attribute
        /// </summary>
        internal const string Pk11Serial = "serial";

        /// <summary>
        /// Max length of "serial" path attribute in bytes
        /// </summary>
        internal const int Pk11SerialMaxLength = 16;

        /// <summary>
        /// Name of "model" path attribute
        /// </summary>
        internal const string Pk11Model = "model";

        /// <summary>
        /// Max length of "model" path attribute in bytes
        /// </summary>
        internal const int Pk11ModelMaxLength = 16;

        /// <summary>
        /// Name of "library-manufacturer" path attribute
        /// </summary>
        internal const string Pk11LibManuf = "library-manufacturer";

        /// <summary>
        /// Max length of "library-manufacturer" path attribute in bytes
        /// </summary>
        internal const int Pk11LibManufMaxLength = 32;

        /// <summary>
        /// Name of "library-description" path attribute
        /// </summary>
        internal const string Pk11LibDesc = "library-description";

        /// <summary>
        /// Max length of "library-description" path attribute in bytes
        /// </summary>
        internal const int Pk11LibDescMaxLength = 32;

        /// <summary>
        /// Name of "library-version" path attribute
        /// </summary>
        internal const string Pk11LibVer = "library-version";

        /// <summary>
        /// Name of "object" path attribute
        /// </summary>
        internal const string Pk11Object = "object";

        /// <summary>
        /// Name of "type" path attribute
        /// </summary>
        internal const string Pk11Type = "type";

        /// <summary>
        /// Value of "type" path attribute for public key
        /// </summary>
        internal const string Pk11TypePublic = "public";

        /// <summary>
        /// Value of "type" path attribute for private key
        /// </summary>
        internal const string Pk11TypePrivate = "private";

        /// <summary>
        /// Value of "type" path attribute for certificate
        /// </summary>
        internal const string Pk11TypeCert = "cert";

        /// <summary>
        /// Value of "type" path attribute for secret key
        /// </summary>
        internal const string Pk11TypeSecretKey = "secret-key";

        /// <summary>
        /// Value of "type" path attribute for data object
        /// </summary>
        internal const string Pk11TypeData = "data";

        /// <summary>
        /// Name of "id" path attribute
        /// </summary>
        internal const string Pk11Id = "id";

        /// <summary>
        /// Name of "slot-manufacturer" path attribute
        /// </summary>
        internal const string Pk11SlotManuf = "slot-manufacturer";

        /// <summary>
        /// Max length of "slot-manufacturer" path attribute in bytes
        /// </summary>
        internal const int Pk11SlotManufMaxLength = 32;

        /// <summary>
        /// Name of "slot-description" path attribute
        /// </summary>
        internal const string Pk11SlotDesc = "slot-description";

        /// <summary>
        /// Max length of "slot-description" path attribute in bytes
        /// </summary>
        internal const int Pk11SlotDescMaxLength = 64;

        /// <summary>
        /// Name of "slot-id" path attribute
        /// </summary>
        internal const string Pk11SlotId = "slot-id";

        /// <summary>
        /// Name of "pin-source" query attribute
        /// </summary>
        internal const string Pk11PinSource = "pin-source";

        /// <summary>
        /// Name of "pin-value" query attribute
        /// </summary>
        internal const string Pk11PinValue = "pin-value";

        /// <summary>
        /// Name of "module-name" query attribute
        /// </summary>
        internal const string Pk11ModuleName = "module-name";

        /// <summary>
        /// Name of "module-path" query attribute
        /// </summary>
        internal const string Pk11ModulePath = "module-path";

        /// <summary>
        /// Static constructor for Net.Pkcs11Interop.URI.Pkcs11UriSpec class
        /// </summary>
        static Pkcs11UriSpec()
        {
            // Set characters allowed in value of path attribute
            Pk11PathAttrValueChars = new char[] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', // RFC 3986 unreserved
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', // RFC 3986 unreserved
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', // RFC 3986 unreserved
                '-', '.', '_', '~',  // RFC 3986 unreserved
                ':', '[', ']', '@', '!', '$', '\'', '(', ')', '*', '+', ',', '=', '&' // pk11-path-res-avail
                // pct-encoded characters are handled in Pkcs11Uri class
            };

            // Set characters allowed in name of vendor specific attribute
            Pk11VendorAttrNameChars = new char[] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', // RFC 2234 ALPHA
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', // RFC 2234 ALPHA
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', // RFC 2234 DIGIT
                '-', '_' // pk11-v-attr-nm-char
            };

            // Set characters allowed in value of query attribute
            Pk11QueryAttrValueChars = new char[] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', // RFC 3986 unreserved
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', // RFC 3986 unreserved
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', // RFC 3986 unreserved
                '-', '.', '_', '~',  // RFC 3986 unreserved
                ':', '[', ']', '@', '!', '$', '\'', '(', ')', '*', '+', ',', '=', '/', '?', '|' // pk11-query-res-avail
                // pct-encoded characters are handled in Pkcs11Uri class
            };
        }
    }
}
