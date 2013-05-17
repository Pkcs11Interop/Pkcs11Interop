/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI
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
