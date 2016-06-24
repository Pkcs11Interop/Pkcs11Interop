/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps to manage unmanaged dynamic libraries
    /// </summary>
    public static class UnmanagedLibrary
    {
        /// <summary>
        /// Loads the dynamic library
        /// </summary>
        /// <param name='fileName'>Library filename</param>
        /// <returns>Dynamic library handle</returns>
        public static IntPtr Load(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            IntPtr libraryHandle = IntPtr.Zero;

            if (Platform.IsLinux || Platform.IsMacOsX)
            {
                int flags = 0;

                if (Platform.IsLinux)
                    flags = NativeMethods.RTLD_NOW_LINUX | NativeMethods.RTLD_LOCAL_LINUX;
                else
                    flags = NativeMethods.RTLD_NOW_MACOSX | NativeMethods.RTLD_LOCAL_MACOSX;

                libraryHandle = NativeMethods.dlopen(fileName, flags);
                if (libraryHandle == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new UnmanagedException(string.Format("Unable to load library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new UnmanagedException("Unable to load library");
                }
            }
            else
            {
                libraryHandle = NativeMethods.LoadLibrary(fileName);
                if (libraryHandle == IntPtr.Zero)
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    if (win32Error == NativeMethods.ERROR_BAD_EXE_FORMAT)
                        throw new LibraryArchitectureException();
                    else
                        throw new UnmanagedException("Unable to load library", win32Error);
                }
            }

            return libraryHandle;
        }

        /// <summary>
        /// Unloads the dynamic library
        /// </summary>
        /// <param name='libraryHandle'>Dynamic library handle</param>
        public static void Unload(IntPtr libraryHandle)
        {
            if (libraryHandle == IntPtr.Zero)
                throw new ArgumentNullException("libraryHandle");

            if (Platform.IsLinux || Platform.IsMacOsX)
            {
                if (0 != NativeMethods.dlclose(libraryHandle))
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new UnmanagedException(string.Format("Unable to unload library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new UnmanagedException("Unable to unload library");
                }
            }
            else
            {
                if (false == NativeMethods.FreeLibrary(libraryHandle))
                    throw new UnmanagedException("Unable to unload library", Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Returns function pointer
        /// </summary>
        /// <param name='libraryHandle'>Dynamic library handle</param>
        /// <param name='function'>Function name</param>
        /// <returns>The function pointer</returns>
        public static IntPtr GetFunctionPointer(IntPtr libraryHandle, string function)
        {
            if (libraryHandle == IntPtr.Zero)
                throw new ArgumentNullException("libraryHandle");

            if (function == null)
                throw new ArgumentNullException("function");

            IntPtr functionPointer = IntPtr.Zero;

            if (Platform.IsLinux || Platform.IsMacOsX)
            {
                functionPointer = NativeMethods.dlsym(libraryHandle, function);
                if (functionPointer == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new UnmanagedException(string.Format("Unable to get function pointer: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new UnmanagedException("Unable to get function pointer");
                }
            }
            else
            {
                functionPointer = NativeMethods.GetProcAddress(libraryHandle, function);
                if (functionPointer == IntPtr.Zero)
                    throw new UnmanagedException("Unable to get function pointer", Marshal.GetLastWin32Error());
            }

            return functionPointer;
        }
    }
}