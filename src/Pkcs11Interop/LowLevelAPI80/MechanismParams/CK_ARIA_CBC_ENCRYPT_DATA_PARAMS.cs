﻿/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.LowLevelAPI80.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_ARIA_CBC_ENCRYPT_DATA mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_ARIA_CBC_ENCRYPT_DATA_PARAMS
    {
        /// <summary>
        /// 16-octet initialization vector
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Iv;

        /// <summary>
        /// Pointer to data to encrypt
        /// </summary>
        public IntPtr Data;

        /// <summary>
        /// Length of data to encrypt
        /// </summary>
        public NativeULong Length;
    }
}
