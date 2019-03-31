/*
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
using System.IO;
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
                // Perform Linux specific library compatibility checks
                // Note: If filename contains a slash ("/"), then it is interpreted by dlopen() as a (relative or absolute) pathname.
                //       Otherwise, the dynamic linker searches for the library following complicated rules.
                if (Platform.IsLinux && fileName.Contains("/") && File.Exists(fileName))
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        // Note: Each ELF file is made up of one ELF header.
                        //       First byte 0x7F is followed by ELF(45 4c 46) in ASCII; these four bytes constitute the magic number.
                        //       Fifth byte is set to either 1 or 2 to signify 32-bit or 64-bit format.
                        byte[] elfHeader = new byte[5];
                        if (fs.Read(elfHeader, 0, 5) == 5 && elfHeader[0] == 0x7F && elfHeader[1] == 0x45 && elfHeader[2] == 0x4c && elfHeader[3] == 0x46)
                        {
                            if ((Platform.Uses64BitRuntime && elfHeader[4] == 0x01) || (Platform.Uses32BitRuntime && elfHeader[4] == 0x02))
                                throw new LibraryArchitectureException();
                        }
                    }
                }

                // Note: On Mac OS X there's dlopen_preflight function that checks whether the library is compatible with current process or not but it cannot be used here
                //       as it just returns the same errors as dlopen function and does not allow us to distinguish between invalid path, invalid architecture and other errors.

                // Load library
                int flags = Platform.IsLinux ? (NativeMethods.RTLD_NOW_LINUX | NativeMethods.RTLD_LOCAL_LINUX) : (NativeMethods.RTLD_NOW_MACOSX | NativeMethods.RTLD_LOCAL_MACOSX);
                libraryHandle = NativeMethods.dlopen(fileName, flags);
                if (libraryHandle == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error == IntPtr.Zero)
                        throw new UnmanagedException("Unable to load library");
                    else
                        throw new UnmanagedException(string.Format("Unable to load library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
                }
            }
            else
            {
                // Load library
                libraryHandle = NativeMethods.LoadLibrary(fileName);
                if (libraryHandle == IntPtr.Zero)
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    if (win32Error == NativeMethods.ERROR_BAD_EXE_FORMAT)
                        throw new LibraryArchitectureException();
                    else
                        throw new UnmanagedException(string.Format("Unable to load library. Error code: 0x{0:X8}", win32Error), win32Error);
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
                    if (error == IntPtr.Zero)
                        throw new UnmanagedException("Unable to unload library");
                    else
                        throw new UnmanagedException(string.Format("Unable to unload library. Error detail: {0}", Marshal.PtrToStringAnsi(error)));
                }
            }
            else
            {
                if (!NativeMethods.FreeLibrary(libraryHandle))
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    throw new UnmanagedException(string.Format("Unable to unload library. Error code: 0x{0:X8}", win32Error), win32Error);
                }
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

            if (string.IsNullOrEmpty(function))
                throw new ArgumentNullException("function");

            IntPtr functionPointer = IntPtr.Zero;

            if (Platform.IsLinux || Platform.IsMacOsX)
            {
                functionPointer = NativeMethods.dlsym(libraryHandle, function);
                if (functionPointer == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error == IntPtr.Zero)
                        throw new UnmanagedException(string.Format("Unable to get pointer for {0} function", function));
                    else
                        throw new UnmanagedException(string.Format("Unable to get pointer for {0} function. Error detail: {1}", function, Marshal.PtrToStringAnsi(error)));
                }
            }
            else
            {
                functionPointer = NativeMethods.GetProcAddress(libraryHandle, function);
                if (functionPointer == IntPtr.Zero)
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    throw new UnmanagedException(string.Format("Unable to get pointer for {0} function. Error code: 0x{1:X8}", function, win32Error), win32Error);
                }
            }

            return functionPointer;
        }

        /// <summary>
        /// Converts function pointer to a delegate
        /// </summary>
        /// <typeparam name="T">Type of delegate</typeparam>
        /// <param name="functionPointer">Function pointer</param>
        /// <returns>Delegate</returns>
        public static T GetDelegateForFunctionPointer<T>(IntPtr functionPointer)
        {
#if !NETSTANDARD1_3
            return (T)(object)Marshal.GetDelegateForFunctionPointer(functionPointer, typeof(T));
#else
            return Marshal.GetDelegateForFunctionPointer<T>(functionPointer);
#endif
        }
    }
}