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
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

// Note: Code in this file is maintained manually.

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
                // Determine flags for LoadLibraryEx function
                // Note: If no flags are specified, the behavior LoadLibraryEx function is identical to that of the LoadLibrary function.
                //       If LOAD_WITH_ALTERED_SEARCH_PATH is used and fileName specifies an absolute path, the system uses the alternate file search strategy 
                //       to find associated executable modules that the specified module causes to be loaded.
                //       If LOAD_WITH_ALTERED_SEARCH_PATH is used and fileName specifies a relative path, the behavior is undefined.
                int flags = IsAbsolutePath(fileName) ? NativeMethods.LOAD_WITH_ALTERED_SEARCH_PATH : 0;
                
                libraryHandle = NativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, flags);
                if (libraryHandle == IntPtr.Zero)
                {
                    int win32Error = Marshal.GetLastWin32Error();
                    if (win32Error == NativeMethods.ERROR_BAD_EXE_FORMAT)
                        throw new LibraryArchitectureException();
                    else
                        throw new UnmanagedException(string.Format("Unable to load library. Error code: 0x{0:X8}. Error detail: {1}", win32Error, new Win32Exception(win32Error).Message), win32Error);
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
                    throw new UnmanagedException(string.Format("Unable to unload library. Error code: 0x{0:X8}. Error detail: {1}", win32Error, new Win32Exception(win32Error).Message), win32Error);
                }
            }
        }

        /// <summary>
        /// Gets function pointer for specified unmanaged function
        /// </summary>
        /// <param name='libraryHandle'>Dynamic library handle</param>
        /// <param name='function'>Function name</param>
        /// <returns>The function pointer for specified unmanaged function</returns>
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
                    throw new UnmanagedException(string.Format("Unable to get pointer for {0} function. Error code: 0x{1:X8}. Error detail: {2}", function, win32Error, new Win32Exception(win32Error).Message), win32Error);
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
            return (T)(object)Marshal.GetDelegateForFunctionPointer(functionPointer, typeof(T));
        }

        /// <summary>
        /// Gets delegate for specified unmanaged function
        /// </summary>
        /// <typeparam name="T">Type of delegate</typeparam>
        /// <param name="libraryHandle">Dynamic library handle</param>
        /// <param name="function">Function name</param>
        /// <returns>Delegate for specified unmanaged function</returns>
        public static T GetFunctionDelegate<T>(IntPtr libraryHandle, string function)
        {
            IntPtr functionPtr = GetFunctionPointer(libraryHandle, function);
            return GetDelegateForFunctionPointer<T>(functionPtr);
        }

        /// <summary>
        /// Checks whether path is absolute
        /// </summary>
        /// <param name="path">Path that should be checked</param>
        /// <returns>True if path is aboslute false otherwise</returns>
        private static bool IsAbsolutePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            if (Platform.IsWindows)
            {
                if (path.Length < 3)
                    return false;

                char firstChar = path[0];
                char secondChar = path[1];
                char thirdChar = path[2];

                // First character must be valid drive character
                if ((firstChar < 'A' || firstChar > 'Z') && (firstChar < 'a' || firstChar > 'z'))
                    return false;

                // Second character must be valid volume separator character
                if (secondChar != Path.VolumeSeparatorChar)
                    return false;

                // Third character must be valid directory separator character
                if (thirdChar != Path.DirectorySeparatorChar && thirdChar != Path.AltDirectorySeparatorChar)
                    return false;

                return true;
            }
            else
            {
                if (path.Length < 1)
                    return false;

                return (path[0] == '/');
            }
        }
    }
}