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
    /// Structure, which provides the parameters to the CKM_TLS_PRF mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_TLS_PRF_PARAMS
    {
        /// <summary>
        /// Pointer to the input seed
        /// </summary>
        public IntPtr Seed;

        /// <summary>
        /// Length in bytes of the input seed
        /// </summary>
        public NativeULong SeedLen;

        /// <summary>
        /// Pointer to the identifying label
        /// </summary>
        public IntPtr Label;

        /// <summary>
        /// Length in bytes of the identifying label
        /// </summary>
        public NativeULong LabelLen;

        /// <summary>
        /// Pointer receiving the output of the operation
        /// </summary>
        public IntPtr Output;

        /// <summary>
        /// Pointer to the length in bytes that the output to be created shall have, has to hold the desired length as input and will receive the calculated length as output
        /// </summary>
        public IntPtr OutputLen;
    }
}
