/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
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
