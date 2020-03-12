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
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_AES_CCM mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_CCM_PARAMS
    {
        /// <summary>
        /// Length of the data
        /// </summary>
        public NativeULong DataLen;

        /// <summary>
        /// Pointer to the nonce
        /// </summary>
        public IntPtr Nonce;

        /// <summary>
        /// Length of the nonce
        /// </summary>
        public NativeULong NonceLen;

        /// <summary>
        /// Pointer to additional authentication data
        /// </summary>
        public IntPtr AAD;

        /// <summary>
        /// Length of additional authentication data
        /// </summary>
        public NativeULong AADLen;

        /// <summary>
        /// Length of the MAC (output following cipher text) in bytes
        /// </summary>
        public NativeULong MACLen;
    }
}
