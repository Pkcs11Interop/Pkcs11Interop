/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
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
