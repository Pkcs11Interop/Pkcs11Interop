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
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using System.Runtime.InteropServices;

// Note: Code in this file is maintained manually.

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
        /// Enables alternate file search strategy to find associated executable modules that the specified module causes to be loaded.
        /// Alternate search strategy uses "the directory the specified module was loaded from" 
        /// instead of "the directory the calling process was loaded from" that is used by the standard search strategy.
        /// </summary>
        internal const int LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="lpFileName">A string that specifies the file name of the module to load.</param>
        /// <param name="hFile">This parameter is reserved for future use. It must be NULL.</param>
        /// <param name="dwFlags">The action to be taken when loading the module. If no flags are specified, the behavior of this function is identical to that of the LoadLibrary function.</param>
        /// <returns>If the function succeeds, the return value is a handle to the loaded module. If the function fails, the return value is NULL.</returns>
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr LoadLibraryEx([MarshalAs(UnmanagedType.LPStr)] string lpFileName, IntPtr hFile, int dwFlags);

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
        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        #endregion

        #region Unix

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
        internal static IntPtr dlerror()
        {
            if (Platform.IsMono)
                return UnixMono.dlerror();
            if (Platform.IsNetCore)
                return UnixNetCore.dlerror();

            return Unix.dlerror();
        }

        /// <summary>
        /// Loads the dynamic library
        /// </summary>
        /// <param name='filename'>Library filename.</param>
        /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
        /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
        internal static IntPtr dlopen(string filename, int flag)
        {
            if (Platform.IsMono)
                return UnixMono.dlopen(filename, flag);
            if (Platform.IsNetCore)
                return UnixNetCore.dlopen(filename, flag);

            return Unix.dlopen(filename, flag);
        }

        /// <summary>
        /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
        internal static int dlclose(IntPtr handle)
        {
            if (Platform.IsMono)
                return UnixMono.dlclose(handle);
            if (Platform.IsNetCore)
                return UnixNetCore.dlclose(handle);

            return Unix.dlclose(handle);
        }

        /// <summary>
        /// Returns the address where the symbol is loaded into memory.
        /// </summary>
        /// <param name='handle'>Handle for the dynamic library.</param>
        /// <param name='symbol'>Name of symbol that should be addressed.</param>
        /// <returns>Returns 0 on success, and nonzero on error.</returns>
        internal static IntPtr dlsym(IntPtr handle, string symbol)
        {
            if (Platform.IsMono)
                return UnixMono.dlsym(handle, symbol);
            if (Platform.IsNetCore)
                return UnixNetCore.dlsym(handle, symbol);

            return Unix.dlsym(handle, symbol);
        }

        /// <summary>
        /// On Linux systems, using dlopen and dlsym results in
        /// DllNotFoundException("libdl.so not found") if libc6-dev
        /// is not installed. As a workaround, we load symbols for
        /// dlopen and dlsym from the current process as on Linux
        /// Mono sure is linked against these symbols.
        /// </summary>
        static class UnixMono
        {
            /// <summary>
            /// Human readable string describing the most recent error that occurred from dlopen(), dlsym() or dlclose() since the last call to dlerror().
            /// </summary>
            /// <returns>Human readable string describing the most recent error or NULL if no errors have occurred since initialization or since it was last called.</returns>
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr dlerror();

            /// <summary>
            /// Loads the dynamic library
            /// </summary>
            /// <param name='filename'>Library filename.</param>
            /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
            /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlopen(string filename, int flag);

            /// <summary>
            /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int dlclose(IntPtr handle);

            /// <summary>
            /// Returns the address where the symbol is loaded into memory.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <param name='symbol'>Name of symbol that should be addressed.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        /// <summary>
        /// Similarly as for Mono on Linux, we load symbols for
        /// dlopen and dlsym from the "libcoreclr.so",
        /// to avoid the dependency on libc-dev Linux.
        /// </summary>
        static class UnixNetCore
        {
            /// <summary>
            /// Human readable string describing the most recent error that occurred from dlopen(), dlsym() or dlclose() since the last call to dlerror().
            /// </summary>
            /// <returns>Human readable string describing the most recent error or NULL if no errors have occurred since initialization or since it was last called.</returns>
            [DllImport("libcoreclr", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr dlerror();

            /// <summary>
            /// Loads the dynamic library
            /// </summary>
            /// <param name='filename'>Library filename.</param>
            /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
            /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
            [DllImport("libcoreclr", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlopen(string filename, int flag);

            /// <summary>
            /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("libcoreclr", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int dlclose(IntPtr handle);

            /// <summary>
            /// Returns the address where the symbol is loaded into memory.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <param name='symbol'>Name of symbol that should be addressed.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("libcoreclr", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        static class Unix
        {
            /// <summary>
            /// Human readable string describing the most recent error that occurred from dlopen(), dlsym() or dlclose() since the last call to dlerror().
            /// </summary>
            /// <returns>Human readable string describing the most recent error or NULL if no errors have occurred since initialization or since it was last called.</returns>
            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr dlerror();

            /// <summary>
            /// Loads the dynamic library
            /// </summary>
            /// <param name='filename'>Library filename.</param>
            /// <param name='flag'>RTLD_LAZY for lazy function call binding or RTLD_NOW immediate function call binding.</param>
            /// <returns>Handle for the dynamic library if successful, IntPtr.Zero otherwise.</returns>
            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlopen(string filename, int flag);

            /// <summary>
            /// Decrements the reference count on the dynamic library handle. If the reference count drops to zero and no other loaded libraries use symbols in it, then the dynamic library is unloaded.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int dlclose(IntPtr handle);

            /// <summary>
            /// Returns the address where the symbol is loaded into memory.
            /// </summary>
            /// <param name='handle'>Handle for the dynamic library.</param>
            /// <param name='symbol'>Name of symbol that should be addressed.</param>
            /// <returns>Returns 0 on success, and nonzero on error.</returns>
            [DllImport("libdl", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        #endregion
    }
}
