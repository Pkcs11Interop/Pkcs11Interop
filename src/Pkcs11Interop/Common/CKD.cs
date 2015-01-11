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
    /// Key derivation functions
    /// </summary>
    public enum CKD : uint
    {
        /// <summary>
        /// Produces a raw shared secret value without applying any key derivation function
        /// </summary>
        CKD_NULL = 0x00000001,

        /// <summary>
        /// Derives keying data from the shared secret value as defined in ANSI X9.63
        /// </summary>
        CKD_SHA1_KDF = 0x00000002,

        /// <summary>
        /// Derives keying data from the shared secret value as defined in the ANSI X9.42 standard
        /// </summary>
        CKD_SHA1_KDF_ASN1 = 0x00000003,

        /// <summary>
        /// Derives keying data from the shared secret value as defined in the ANSI X9.42 standard
        /// </summary>
        CKD_SHA1_KDF_CONCATENATE = 0x00000004
    }
}
