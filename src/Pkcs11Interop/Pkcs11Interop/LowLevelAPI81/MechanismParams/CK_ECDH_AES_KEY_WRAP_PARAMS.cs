/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using NativeLong = System.UInt64;

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_ECDH_AES_KEY_WRAP mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_ECDH_AES_KEY_WRAP_PARAMS
    {
        /// <summary>
        /// Length of the temporary AES key in bits
        /// </summary>
        public NativeLong AESKeyBits;

        /// <summary>
        /// Key derivation function used on the shared secret value to generate AES key (CKD)
        /// </summary>
        public NativeLong Kdf;

        /// <summary>
        /// Length in bytes of the shared info
        /// </summary>
        public NativeLong SharedDataLen;

        /// <summary>
        /// Data shared between the two parties
        /// </summary>
        public IntPtr SharedData;
    }
}
