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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41
{
    /// <summary>
    /// Specifies a particular mechanism and any parameters it requires
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_MECHANISM
    {
        /// <summary>
        /// The type of mechanism
        /// </summary>
        public NativeULong Mechanism;

        /// <summary>
        /// Pointer to the parameter if required by the mechanism
        /// </summary>
        public IntPtr Parameter;

        /// <summary>
        /// Length of the parameter in bytes
        /// </summary>
        public NativeULong ParameterLen;
    }
}
