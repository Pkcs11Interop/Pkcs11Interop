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
using System.Runtime.InteropServices;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_AES_GCM mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_GCM_PARAMS
    {
        /// <summary>
        /// Pointer to initialization vector
        /// </summary>
        public IntPtr Iv;

        /// <summary>
        /// Length of initialization vector in bytes
        /// </summary>
        public NativeULong IvLen;

        /// <summary>
        /// Member is defined in PKCS#11 v2.40e1 headers but the description is not present in the specification
        /// </summary>
        public NativeULong IvBits; // TODO - Fix description when fixed in PKCS#11 specification

        /// <summary>
        /// Pointer to additional authentication data
        /// </summary>
        public IntPtr AAD;

        /// <summary>
        /// Length of additional authentication data in bytes
        /// </summary>
        public NativeULong AADLen;

        /// <summary>
        /// Length of authentication tag (output following cipher text) in bits
        /// </summary>
        public NativeULong TagBits;
    }
}
