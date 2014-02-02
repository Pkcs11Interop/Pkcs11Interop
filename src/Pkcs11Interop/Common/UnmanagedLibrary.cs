/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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

            PlatformID platformId = System.Environment.OSVersion.Platform;
            if ((platformId == PlatformID.Unix) || (platformId == PlatformID.MacOSX))
            {
                libraryHandle = NativeMethods.dlopen(fileName, NativeMethods.RTLD_NOW | NativeMethods.RTLD_LOCAL);
                if (libraryHandle == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new Exception(string.Format("Unable to load library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new Exception("Unable to load library");
                }
            }
            else
            {
                libraryHandle = NativeMethods.LoadLibrary(fileName);
                if (libraryHandle == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
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

            PlatformID platformId = System.Environment.OSVersion.Platform;
            if ((platformId == PlatformID.Unix) || (platformId == PlatformID.MacOSX))
            {
                if (0 != NativeMethods.dlclose(libraryHandle))
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new Exception(string.Format("Unable to unload library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new Exception("Unable to unload library");
                }
            }
            else
            {
                if (false == NativeMethods.FreeLibrary(libraryHandle))
                    throw new Win32Exception(Marshal.GetLastWin32Error());
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

            PlatformID platformId = System.Environment.OSVersion.Platform;
            if ((platformId == PlatformID.Unix) || (platformId == PlatformID.MacOSX))
            {
                NativeMethods.dlerror();
                functionPointer = NativeMethods.dlsym(libraryHandle, function);
                IntPtr error = NativeMethods.dlerror();
                if (error != IntPtr.Zero)
                    throw new Exception(string.Format("Unable to get function pointer: {0}", Marshal.PtrToStringAnsi(error)));
            }
            else
            {
                functionPointer = NativeMethods.GetProcAddress(libraryHandle, function);
                if (functionPointer == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return functionPointer;
        }
    }
}