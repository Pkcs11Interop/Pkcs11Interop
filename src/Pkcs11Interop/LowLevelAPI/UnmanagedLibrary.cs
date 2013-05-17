/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using Net.Pkcs11Interop.LowLevelAPI;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Utility class that helps to manage unmanaged dynamic libraries
    /// </summary>
    internal class UnmanagedLibrary
    {
        /// <summary>
        /// Loads the dynamic library
        /// </summary>
        /// <param name='fileName'>Library filename</param>
        /// <returns>Dynamic library handle</returns>
        internal static IntPtr Load(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            IntPtr libraryHandle = IntPtr.Zero;

            PlatformID platformId = System.Environment.OSVersion.Platform;
            if ((platformId == PlatformID.Unix) || (platformId == PlatformID.MacOSX))
            {
                libraryHandle = NativeMethods.dlopen(fileName, NativeMethods.RTLD_NOW);
                if (libraryHandle == IntPtr.Zero)
                {
                    IntPtr error = NativeMethods.dlerror();
                    if (error != IntPtr.Zero)
                        throw new Pkcs11InteropException(string.Format("Unable to load library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new Pkcs11InteropException("Unable to load library");
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
        internal static void Unload(IntPtr libraryHandle)
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
                        throw new Pkcs11InteropException(string.Format("Unable to unload library: {0}", Marshal.PtrToStringAnsi(error)));
                    else
                        throw new Pkcs11InteropException("Unable to unload library");
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
        internal static IntPtr GetFunctionPointer(IntPtr libraryHandle, string function)
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
                    throw new Pkcs11InteropException(string.Format("Unable to get function pointer: {0}", Marshal.PtrToStringAnsi(error)));
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