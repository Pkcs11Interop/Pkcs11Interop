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
    /// Object class
    /// </summary>
    public enum CKO : uint
    {
        /// <summary>
        /// Data object that holds information defined by an application.
        /// </summary>
        CKO_DATA = 0x00000000,

        /// <summary>
        /// Certificate object that holds public-key or attribute certificates.
        /// </summary>
        CKO_CERTIFICATE = 0x00000001,

        /// <summary>
        /// Public key object that holds public keys.
        /// </summary>
        CKO_PUBLIC_KEY = 0x00000002,

        /// <summary>
        /// Private key object that holds private keys.
        /// </summary>
        CKO_PRIVATE_KEY = 0x00000003,

        /// <summary>
        /// Secret key object that holds secret keys.
        /// </summary>
        CKO_SECRET_KEY = 0x00000004,

        /// <summary>
        /// Hardware feature object that represent features of the device.
        /// </summary>
        CKO_HW_FEATURE = 0x00000005,

        /// <summary>
        /// Domain parameter object that holds public domain parameters.
        /// </summary>
        CKO_DOMAIN_PARAMETERS = 0x00000006,

        /// <summary>
        /// Mechanism object that provides information about mechanisms supported by a device beyond that given by the CK_MECHANISM_INFO structure.
        /// </summary>
        CKO_MECHANISM = 0x00000007,

        /// <summary>
        /// OTP key object that holds secret keys used by OTP tokens.
        /// </summary>
        CKO_OTP_KEY = 0x00000008,

        /// <summary>
        /// Reserved for token vendors.
        /// </summary>
        CKO_VENDOR_DEFINED = 0x80000000
    }
}
