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
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.LowLevelAPI40.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_GOSTR3410_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_GOSTR3410_DERIVE_PARAMS
    {
        /// <summary>
        /// Additional key diversification algorithm (CKD)
        /// </summary>
        public NativeULong Kdf;

        /// <summary>
        /// Pointer to data with public key of a receiver
        /// </summary>
        public IntPtr PublicData;

        /// <summary>
        /// Length of data with public key of a receiver. Must be 64.
        /// </summary>
        public NativeULong PublicDataLen;

        /// <summary>
        /// Pointer to a UKM data
        /// </summary>
        public IntPtr UKM;

        /// <summary>
        /// Length of UKM data in bytes. Must be 8.
        /// </summary>
        public NativeULong UKMLen;
    }
}
