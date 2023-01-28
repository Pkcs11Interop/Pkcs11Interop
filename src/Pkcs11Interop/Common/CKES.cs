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
    /// Elliptic curve encryption
    /// </summary>
    public enum CKES : uint
    {
        /// <summary>
        /// Elliptic curve encryption based on XOR
        /// </summary>
        CKES_XOR = 0x00000001,

        /// <summary>
        /// Elliptic curve encryption based on DES3_CBC_PAD
        /// </summary>
        CKES_DES3_CBC_PAD = 0x00000002,

        /// <summary>
        /// Elliptic curve encryption based on AES_CBC_PAD
        /// </summary>
        CKES_AES_CBC_PAD = 0x00000003,

        /// <summary>
        /// Elliptic curve encryption based on DES3_CBC
        /// </summary>
        CKES_DES3_CBC = 0x00000004,

        /// <summary>
        /// Elliptic curve encryption based on AES_CBC
        /// </summary>
        CKES_AES_CBC = 0x00000005
    }
}