/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Common;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Mock.LowLevelAPI80
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetUnmanagedStructSizeListDelegate(NativeULong[] sizeList, ref NativeULong count);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_EjectTokenDelegate(NativeULong slotId);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_InteractiveLoginDelegate(NativeULong session);

    /// <summary>
    /// Holds delegates for vendor specific unmanaged functions of PKCS11-MOCK module.
    /// </summary>
    internal class MockDelegates
    {
        /// <summary>
        /// Definitions of vendor specific unmanaged methods
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("Pkcs11Library", EntryPoint = "C_GetUnmanagedStructSizeList", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetUnmanagedStructSizeList(NativeULong[] sizeList, ref NativeULong count);

            [DllImport("Pkcs11Library", EntryPoint = "C_EjectToken", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_EjectToken(NativeULong slotId);

            [DllImport("Pkcs11Library", EntryPoint = "C_InteractiveLogin", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_InteractiveLogin(NativeULong session);
        }

        /// <summary>
        /// Delegate for C_Initialize
        /// </summary>
        internal C_GetUnmanagedStructSizeListDelegate C_GetUnmanagedStructSizeList = null;

        /// <summary>
        /// Delegate for C_EjectToken
        /// </summary>
        internal C_EjectTokenDelegate C_EjectToken = null;

        /// <summary>
        /// Delegate for C_InteractiveLogin
        /// </summary>
        internal C_InteractiveLoginDelegate C_InteractiveLogin = null;

        /// <summary>
        /// Initializes new instance of MockDelegates class
        /// </summary>
        /// <param name="libraryHandle">Handle to the PKCS#11 library</param>
        internal MockDelegates(IntPtr libraryHandle)
        {
            // Get delegates from the dynamically loaded shared PKCS#11 library
            if (libraryHandle != IntPtr.Zero)
            {
                this.C_GetUnmanagedStructSizeList = UnmanagedLibrary.GetFunctionDelegate<C_GetUnmanagedStructSizeListDelegate>(libraryHandle, "C_GetUnmanagedStructSizeList");
                this.C_EjectToken = UnmanagedLibrary.GetFunctionDelegate<C_EjectTokenDelegate>(libraryHandle, "C_EjectToken");
                this.C_InteractiveLogin = UnmanagedLibrary.GetFunctionDelegate<C_InteractiveLoginDelegate>(libraryHandle, "C_InteractiveLogin");
            }
            // Get delegates from the statically linked PKCS#11 library
            else
            {
                this.C_GetUnmanagedStructSizeList = NativeMethods.C_GetUnmanagedStructSizeList;
                this.C_EjectToken = NativeMethods.C_EjectToken;
                this.C_InteractiveLogin = NativeMethods.C_InteractiveLogin;
            }
        }
    }
}
