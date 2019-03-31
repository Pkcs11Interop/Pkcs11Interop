﻿/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Imported native methods
    /// </summary>
    internal static class NativeMethods
    {
        #region Windows

        /// <summary>
        /// Error indicating an attempt to load unmanaged library designated for a different architecture
        /// </summary>
        internal const int ERROR_BAD_EXE_FORMAT = 0xC1;

        /// <summary>
        /// Loads the specified module into the address space of the calling process.
        /// </summary>
        /// <param name="lpFileName">The name of the module.</param>
        /// <returns>If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL.</returns>
#if SILVERLIGHT
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
#else
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
#endif

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule">A handle to the DLL module that contains the function or variable.</param>
        /// <param name="lpProcName">The function or variable name, or the function's ordinal value.</param>
        /// <returns>If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL.</returns>
#if SILVERLIGHT
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);
#else
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);
#endif

#if SILVERLIGHT

        /// <summary>
        /// Allocates fixed memory
        /// </summary>
        internal const int LMEM_FIXED = 0x0000;

        /// <summary>
        /// Initializes memory contents to zero
        /// </summary>
        internal const int LMEM_ZEROINIT = 0x0040;

        /// <summary>
        /// Allocates the specified number of bytes from the heap
        /// </summary>
        /// <param name="uFlags">The memory allocation attributes</param>
        /// <param name="uBytes">The number of bytes to allocate</param>
        /// <returns>If the function succeeds, the return value is a handle to the newly allocated memory object. If the function fails, the return value is NULL.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        internal static extern IntPtr LocalAlloc(int uFlags, UIntPtr uBytes);

        /// <summary>
        /// Frees the specified local memory object and invalidates its handle
        /// </summary>
        /// <param name="hMem">A handle to the local memory object</param>
        /// <returns>If the function succeeds, the return value is NULL. If the function fails, the return value is equal to a handle to the local memory object.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        internal static extern IntPtr LocalFree(IntPtr hMem);

#endif

        #endregion

        #region Unix

#if XAMARINMAC2_0
        private const string _dllName = "/usr/lib/libSystem.dylib";
#else
        private const string _dllName = "libdl";
#endif

        /// <summary>
        /// Immediately resolve all symbols
        /// </summary>
        internal const int RTLD_NOW_LINUX = 0x2;

        /// <summary>
        /// Resolved symbols are not available for subsequently loaded libraries
        /// </summary>
        internal const int RTLD_LOCAL_LINUX = 0;

        /// <summary>
        /// Immediately resolve all symbols
        /// </summary>
        internal const int RTLD_NOW_MACOSX = 0x2;

        /// <summary>
        /// Resolved symbols are not available for subsequently loaded libraries
        /// </summary>
        internal const int RTLD_LOCAL_MACOSX = 0x4;

        /// <summary>
        /// Human readable string describing the most recent error that occurred from dlopen(), dlsym() or dlclose() since the last call to dlerror().
        /// </summary>
        /// <returns>Human readable string describing the most recent error or NULL if no errors have occurred since initialization or since it was last called.</returns>
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlerror();
        
        /// <summary>
        /// Loads the dynamic library
        /// </summary>
        /// <param name='filename'>Library filename.</param>
        /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
        /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
#if SILVERLIGHT
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern IntPtr dlopen(string filename, int flag);
#else
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr dlopen(string filename, int flag);
#endif

        /// <summary>
        /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int dlclose(IntPtr handle);
        
        /// <summary>
        /// Returns the address where the symbol is loaded into memory.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <param name='symbol'>Name of symbol that should be addressed.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
#if SILVERLIGHT
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
#else
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
#endif

        #endregion
    }
}
