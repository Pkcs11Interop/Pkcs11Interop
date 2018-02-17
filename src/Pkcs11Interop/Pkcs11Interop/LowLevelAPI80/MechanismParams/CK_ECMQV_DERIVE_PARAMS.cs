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

namespace Net.Pkcs11Interop.LowLevelAPI80.MechanismParams
{
    /// <summary>
    ///  Structure that provides the parameters to the CKM_ECMQV_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_ECMQV_DERIVE_PARAMS
    {
        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public NativeULong Kdf;

        /// <summary>
        /// The length in bytes of the shared info
        /// </summary>
        public NativeULong SharedDataLen;

        /// <summary>
        /// Some data shared between the two parties
        /// </summary>
        public IntPtr SharedData;

        /// <summary>
        /// The length in bytes of the other party's first EC public key
        /// </summary>
        public NativeULong PublicDataLen;

        /// <summary>
        /// Pointer to other party's first EC public key value
        /// </summary>
        public IntPtr PublicData;

        /// <summary>
        /// The length in bytes of the second EC private key
        /// </summary>
        public NativeULong PrivateDataLen;

        /// <summary>
        /// Key handle for second EC private key value
        /// </summary>
        public NativeULong PrivateData;

        /// <summary>
        /// The length in bytes of the other party's second EC public key
        /// </summary>
        public NativeULong PublicDataLen2;

        /// <summary>
        /// Pointer to other party's second EC public key value
        /// </summary>
        public IntPtr PublicData2;

        /// <summary>
        /// Handle to the first party's ephemeral public key
        /// </summary>
        public NativeULong PublicKey;
    }
}
