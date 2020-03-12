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

// Note: Code in this file is maintained manually.

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
        /// MD5 HMAC key
        /// </summary>
        CKK_MD5_HMAC = 0x00000027,

        /// <summary>
        /// SHA-1 HMAC key
        /// </summary>
        CKK_SHA_1_HMAC = 0x00000028,

        /// <summary>
        /// RIPE-MD 128 HMAC key
        /// </summary>
        CKK_RIPEMD128_HMAC = 0x00000029,

        /// <summary>
        /// RIPE-MD 160 HMAC key
        /// </summary>
        CKK_RIPEMD160_HMAC = 0x0000002A,

        /// <summary>
        /// SHA-256 HMAC key
        /// </summary>
        CKK_SHA256_HMAC = 0x0000002B,

        /// <summary>
        /// SHA-384 HMAC key
        /// </summary>
        CKK_SHA384_HMAC = 0x0000002C,

        /// <summary>
        /// SHA-512 HMAC key
        /// </summary>
        CKK_SHA512_HMAC = 0x0000002D,

        /// <summary>
        /// SHA-224 HMAC key
        /// </summary>
        CKK_SHA224_HMAC = 0x0000002E,

        /// <summary>
        /// SEED secret key
        /// </summary>
        CKK_SEED = 0x0000002F,

        /// <summary>
        /// GOST R 34.10-2001 key
        /// </summary>
        CKK_GOSTR3410 = 0x00000030,

        /// <summary>
        /// GOST R 34.11-94 key or domain parameter
        /// </summary>
        CKK_GOSTR3411 = 0x00000031,

        /// <summary>
        /// GOST 28147-89 key or domain parameter
        /// </summary>
        CKK_GOST28147 = 0x00000032,

        /// <summary>
        /// Permanently reserved for token vendors
        /// </summary>
        CKK_VENDOR_DEFINED = 0x80000000
    }
}
