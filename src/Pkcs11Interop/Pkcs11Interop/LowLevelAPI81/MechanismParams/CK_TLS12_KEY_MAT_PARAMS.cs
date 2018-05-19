﻿/*
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
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_TLS12_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_TLS12_KEY_MAT_PARAMS
    {
        /// <summary>
        /// The length (in bits) of the MACing keys agreed upon during the protocol handshake phase
        /// </summary>
        public NativeULong MacSizeInBits;

        /// <summary>
        /// The length (in bits) of the secret keys agreed upon during the protocol handshake phase
        /// </summary>
        public NativeULong KeySizeInBits;

        /// <summary>
        /// The length (in bits) of the IV agreed upon during the protocol handshake phase
        /// </summary>
        public NativeULong IVSizeInBits;

        /// <summary>
        /// Flag which must be set to false because export cipher suites must not be used in TLS 1.1 and later
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool IsExport;

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        public CK_SSL3_RANDOM_DATA RandomInfo;

        /// <summary>
        /// Points to a CK_SSL3_KEY_MAT_OUT structure which receives the handles for the keys generated and the IVs
        /// </summary>
        public IntPtr ReturnedKeyMaterial;

        /// <summary>
        /// Base hash used in the underlying TLS1.2 PRF operation used to derive the master key (CKM)
        /// </summary>
        public NativeULong PrfHashMechanism;
    }
}
