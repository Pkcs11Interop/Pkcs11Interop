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
    /// Optional arguments for the C_Initialize function
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CK_C_INITIALIZE_ARGS
    {
        /// <summary>
        /// Pointer to a function to use for creating mutex objects (not supported by Pkcs11Interop)
        /// </summary>
        public IntPtr CreateMutex = IntPtr.Zero;

        /// <summary>
        /// Pointer to a function to use for destroying mutex objects (not supported by Pkcs11Interop)
        /// </summary>
        public IntPtr DestroyMutex = IntPtr.Zero;

        /// <summary>
        /// Pointer to a function to use for locking mutex objects (not supported by Pkcs11Interop)
        /// </summary>
        public IntPtr LockMutex = IntPtr.Zero;

        /// <summary>
        /// Pointer to a function to use for unlocking mutex objects (not supported by Pkcs11Interop)
        /// </summary>
        public IntPtr UnlockMutex = IntPtr.Zero;

        /// <summary>
        /// Bit flags specifying options
        /// </summary>
        public NativeULong Flags = 0;

        /// <summary>
        /// Reserved for future use
        /// </summary>
        public IntPtr Reserved = IntPtr.Zero;
    }
}
