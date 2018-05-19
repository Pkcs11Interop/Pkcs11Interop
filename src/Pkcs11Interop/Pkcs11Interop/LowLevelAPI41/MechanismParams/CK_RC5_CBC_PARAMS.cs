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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_RC5_CBC_PARAMS
    {
        /// <summary>
        /// Wordsize of RC5 cipher in bytes
        /// </summary>
        public NativeULong Wordsize;

        /// <summary>
        /// Number of rounds of RC5 encipherment
        /// </summary>
        public NativeULong Rounds;

        /// <summary>
        /// Pointer to initialization vector (IV) for CBC encryption
        /// </summary>
        public IntPtr Iv;

        /// <summary>
        /// Length of initialization vector (must be same as blocksize)
        /// </summary>
        public NativeULong IvLen;
    }
}
