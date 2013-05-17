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
    /// Key types
    /// </summary>
    public enum CKK : uint
    {
        /// <summary>
        /// RSA key
        /// </summary>
        CKK_RSA = 0x00000000,

        /// <summary>
        /// DSA key
        /// </summary>
        CKK_DSA = 0x00000001,

        /// <summary>
        /// DH (Diffie-Hellman) key
        /// </summary>
        CKK_DH = 0x00000002,
        
        /// <summary>
        /// EC (Elliptic Curve) key
        /// </summary>
        CKK_ECDSA = 0x00000003,

        /// <summary>
        /// EC (Elliptic Curve) key
        /// </summary>
        CKK_EC = 0x00000003,

        /// <summary>
        /// X9.42 Diffie-Hellman public keys
        /// </summary>
        CKK_X9_42_DH = 0x00000004,

        /// <summary>
        /// KEA keys
        /// </summary>
        CKK_KEA = 0x00000005,

        /// <summary>
        /// Generic secret key
        /// </summary>
        CKK_GENERIC_SECRET = 0x00000010,

        /// <summary>
        /// RC2 key
        /// </summary>
        CKK_RC2 = 0x00000011,

        /// <summary>
        /// RC4 key
        /// </summary>
        CKK_RC4 = 0x00000012,

        /// <summary>
        /// Single-length DES key
        /// </summary>
        CKK_DES = 0x00000013,

        /// <summary>
        /// Double-length DES key
        /// </summary>
        CKK_DES2 = 0x00000014,

        /// <summary>
        /// Triple-length DES key
        /// </summary>
        CKK_DES3 = 0x00000015,
        
        /// <summary>
        /// CAST key
        /// </summary>
        CKK_CAST = 0x00000016,

        /// <summary>
        /// CAST3 key
        /// </summary>
        CKK_CAST3 = 0x00000017,

        /// <summary>
        /// CAST128 key
        /// </summary>
        CKK_CAST5 = 0x00000018,

        /// <summary>
        /// CAST128 key
        /// </summary>
        CKK_CAST128 = 0x00000018,
        
        /// <summary>
        /// RC5 key
        /// </summary>
        CKK_RC5 = 0x00000019,

        /// <summary>
        /// IDEA key
        /// </summary>
        CKK_IDEA = 0x0000001A,

        /// <summary>
        /// Single-length MEK or a TEK
        /// </summary>
        CKK_SKIPJACK = 0x0000001B,

        /// <summary>
        /// Single-length BATON key
        /// </summary>
        CKK_BATON = 0x0000001C,

        /// <summary>
        /// Single-length JUNIPER key
        /// </summary>
        CKK_JUNIPER = 0x0000001D,

        /// <summary>
        /// Single-length CDMF key
        /// </summary>
        CKK_CDMF = 0x0000001E,

        /// <summary>
        /// AES key
        /// </summary>
        CKK_AES = 0x0000001F,

        /// <summary>
        /// Blowfish key
        /// </summary>
        CKK_BLOWFISH = 0x00000020,

        /// <summary>
        /// Twofish key
        /// </summary>
        CKK_TWOFISH = 0x00000021,

        /// <summary>
        /// RSA SecurID secret key
        /// </summary>
        CKK_SECURID = 0x00000022,

        /// <summary>
        /// Generic secret key and associated counter value
        /// </summary>
        CKK_HOTP = 0x00000023,

        /// <summary>
        /// ActivIdentity ACTI secret key
        /// </summary>
        CKK_ACTI = 0x00000024,

        /// <summary>
        /// Camellia key
        /// </summary>
        CKK_CAMELLIA = 0x00000025,
        
        /// <summary>
        /// ARIA key
        /// </summary>
        CKK_ARIA = 0x00000026,

        /// <summary>
        /// Permanently reserved for token vendors
        /// </summary>
        CKK_VENDOR_DEFINED = 0x80000000
    }
}
