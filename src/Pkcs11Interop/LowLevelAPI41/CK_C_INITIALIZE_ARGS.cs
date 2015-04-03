/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using System.Runtime.InteropServices;

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
        public uint Flags = 0;

        /// <summary>
        /// Reserved for future use
        /// </summary>
        public IntPtr Reserved = IntPtr.Zero;
    }
}
