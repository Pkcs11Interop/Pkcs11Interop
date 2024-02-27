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

using System;
using System.Runtime.InteropServices;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters for the CKM_ECIES mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_ECIES_PARAMS
    {
        /// <summary>
        /// Diffie-Hellman Primitive (CKDHP) used to derive the shared secret value
        /// </summary>
        public NativeULong DhPrimitive;

        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public NativeULong Kdf;

        /// <summary>
        /// The length in bytes of the key derivation shared data
        /// </summary>
        public NativeULong SharedDataLen1;

        /// <summary>
        /// The key derivation padding data shared between the two parties
        /// </summary>
        public IntPtr SharedData1;

        /// <summary>
        /// The encryption scheme (CKES) used to transform the input data
        /// </summary>
        public NativeULong EncScheme;

        /// <summary>
        /// The bit length of the key to use for the encryption scheme
        /// </summary>
        public NativeULong EncKeyLenInBits;

        /// <summary>
        /// The MAC scheme (CKMS) used for MAC generation or validation
        /// </summary>
        public NativeULong MacScheme;

        /// <summary>
        /// The bit length of the key to use for the MAC scheme
        /// </summary>
        public NativeULong MacKeyLenInBits;

        /// <summary>
        /// The bit length of the MAC scheme output
        /// </summary>
        public NativeULong MacLenInBits;

        /// <summary>
        /// The length in bytes of the MAC shared data
        /// </summary>
        public NativeULong SharedDataLen2;

        /// <summary>
        /// The MAC padding data shared between the two parties
        /// </summary>
        public IntPtr SharedData2;
    }
}