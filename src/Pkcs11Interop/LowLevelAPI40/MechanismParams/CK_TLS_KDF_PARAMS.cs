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

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI40.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_TLS_KDF mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_TLS_KDF_PARAMS
    {
        /// <summary>
        /// Hash mechanism used in the TLS 1.2 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)
        /// </summary>
        public NativeULong PrfMechanism;

        /// <summary>
        /// Pointer to the label for this key derivation
        /// </summary>
        public IntPtr Label;

        /// <summary>
        /// Length of the label in bytes
        /// </summary>
        public NativeULong LabelLength;

        /// <summary>
        /// Random data for the key derivation
        /// </summary>
        public CK_SSL3_RANDOM_DATA RandomInfo;

        /// <summary>
        /// Pointer to the context data for this key derivation
        /// </summary>
        public IntPtr ContextData;

        /// <summary>
        /// Length of the context data in bytes
        /// </summary>
        public NativeULong ContextDataLength;
    }
}
