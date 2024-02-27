/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
 *  KhaledGomaa <khaledgomaa670@gmail.com>
 */

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Elliptic curve encryption, MAC Scheme to be used.
    /// </summary>
    public enum CKMS : uint
    {
        /// <summary>
        /// MAC Scheme based on HMAC_SHA1
        /// </summary>
        CKMS_HMAC_SHA1 = 0x00000001,

        /// <summary>
        /// MAC Scheme based on SHA1
        /// </summary>
        CKMS_SHA1 = 0x00000002,

        /// <summary>
        /// MAC Scheme based on HMAC_SHA224
        /// </summary>
        CKMS_HMAC_SHA224 = 0x00000003,

        /// <summary>
        /// MAC Scheme based on SHA224
        /// </summary>
        CKMS_SHA224 = 0x00000004,

        /// <summary>
        /// MAC Scheme based on HMAC_SHA256
        /// </summary>
        CKMS_HMAC_SHA256 = 0x00000005,

        /// <summary>
        /// MAC Scheme based on SHA256
        /// </summary>
        CKMS_SHA256 = 0x00000006,

        /// <summary>
        /// MAC Scheme based on HMAC_SHA384
        /// </summary>
        CKMS_HMAC_SHA384 = 0x00000007,

        /// <summary>
        /// MAC Scheme based on SHA384
        /// </summary>
        CKMS_SHA384 = 0x00000008,

        /// <summary>
        /// MAC Scheme based on HMAC_SHA512
        /// </summary>
        CKMS_HMAC_SHA512 = 0x00000009,

        /// <summary>
        /// MAC Scheme based on SHA512
        /// </summary>
        CKMS_SHA512 = 0x0000000A,

        /// <summary>
        /// MAC Scheme based on HMAC_RIPEMD160
        /// </summary>
        CKMS_HMAC_RIPEMD160 = 0x0000000B,

        /// <summary>
        /// MAC Scheme based on RIPEMD160
        /// </summary>
        CKMS_RIPEMD160 = 0x0000000C
    }
}