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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;
using HLA40 = Net.Pkcs11Interop.HighLevelAPI40;
using HLA41 = Net.Pkcs11Interop.HighLevelAPI41;
using HLA80 = Net.Pkcs11Interop.HighLevelAPI80;
using HLA81 = Net.Pkcs11Interop.HighLevelAPI81;
using LLA40 = Net.Pkcs11Interop.LowLevelAPI40;
using LLA41 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA80 = Net.Pkcs11Interop.LowLevelAPI80;
using LLA81 = Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Extends Session classes with the ability to call C_InteractiveLogin function.
    /// C_InteractiveLogin function is vendor specific function of PKCS11-MOCK module.
    /// </summary>
    public static class SessionClassExtensions
    {
        #region HighLevelAPI extensions

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public static void InteractiveLogin(this Session session)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    session.HLA40Session.InteractiveLogin();
                }
                else
                {
                    session.HLA41Session.InteractiveLogin();
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    session.HLA80Session.InteractiveLogin();
                }
                else
                {
                    session.HLA81Session.InteractiveLogin();
                }
            }
        }

        #endregion

        #region HighLevelAPI4x and HighLevelAPI8x extensions

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public static void InteractiveLogin(this HLA40.Session session)
        {
            CKR rv = session.LowLevelPkcs11.C_InteractiveLogin(session.SessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public static void InteractiveLogin(this HLA41.Session session)
        {
            CKR rv = session.LowLevelPkcs11.C_InteractiveLogin(session.SessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public static void InteractiveLogin(this HLA80.Session session)
        {
            CKR rv = session.LowLevelPkcs11.C_InteractiveLogin(session.SessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public static void InteractiveLogin(this HLA81.Session session)
        {
            CKR rv = session.LowLevelPkcs11.C_InteractiveLogin(session.SessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }

        #endregion

        #region LowLevelAPI4x and LowLevelAPI8x extensions

        /// <summary>
        /// Delegates for vendor specific unmanaged method
        /// </summary>
        private static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate uint C_InteractiveLogin4x(uint session);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate ulong C_InteractiveLogin8x(ulong session);
        }

        /// <summary>
        /// Definitions of vendor specific unmanaged method
        /// </summary>
        private static class NativeMethods
        {
#if XAMARINMAC2_0
            private const string _dllName = "NonExistingLibrary";
#else
            private const string _dllName = "__Internal";
#endif

            [DllImport(_dllName, EntryPoint = "C_InteractiveLogin", CallingConvention = CallingConvention.Cdecl)]
            internal static extern uint C_InteractiveLogin4x(uint session);

            [DllImport(_dllName, EntryPoint = "C_InteractiveLogin", CallingConvention = CallingConvention.Cdecl)]
            internal static extern ulong C_InteractiveLogin8x(ulong session);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SESSION_HANDLE_INVALID, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_OK</returns>
        public static CKR C_InteractiveLogin(this LLA40.Pkcs11 pkcs11, uint session)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_InteractiveLogin4x cInteractiveLogin = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cInteractiveLoginPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_InteractiveLogin");
                cInteractiveLogin = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_InteractiveLogin4x>(cInteractiveLoginPtr);
            }
            else
            {
                cInteractiveLogin = NativeMethods.C_InteractiveLogin4x;
            }

            uint rv = cInteractiveLogin(session);
            return (CKR)rv;
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SESSION_HANDLE_INVALID, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_OK</returns>
        public static CKR C_InteractiveLogin(this LLA41.Pkcs11 pkcs11, uint session)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_InteractiveLogin4x cInteractiveLogin = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cInteractiveLoginPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_InteractiveLogin");
                cInteractiveLogin = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_InteractiveLogin4x>(cInteractiveLoginPtr);
            }
            else
            {
                cInteractiveLogin = NativeMethods.C_InteractiveLogin4x;
            }

            uint rv = cInteractiveLogin(session);
            return (CKR)rv;
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SESSION_HANDLE_INVALID, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_OK</returns>
        public static CKR C_InteractiveLogin(this LLA80.Pkcs11 pkcs11, ulong session)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_InteractiveLogin8x cInteractiveLogin = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cInteractiveLoginPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_InteractiveLogin");
                cInteractiveLogin = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_InteractiveLogin8x>(cInteractiveLoginPtr);
            }
            else
            {
                cInteractiveLogin = NativeMethods.C_InteractiveLogin8x;
            }

            ulong rv = cInteractiveLogin(session);
            return (CKR)Convert.ToUInt32(rv);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SESSION_HANDLE_INVALID, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_OK</returns>
        public static CKR C_InteractiveLogin(this LLA81.Pkcs11 pkcs11, ulong session)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_InteractiveLogin8x cInteractiveLogin = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cInteractiveLoginPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_InteractiveLogin");
                cInteractiveLogin = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_InteractiveLogin8x>(cInteractiveLoginPtr);
            }
            else
            {
                cInteractiveLogin = NativeMethods.C_InteractiveLogin8x;
            }

            ulong rv = cInteractiveLogin(session);
            return (CKR)Convert.ToUInt32(rv);
        }

        #endregion
    }

    /// <summary>
    /// Tests the possibility to extend Session classes with a vendor specific function
    /// </summary>
    [TestFixture()]
    public class _30_SessionClassExtensionTest
    {
        /// <summary>
        /// C_InteractiveLogin vendor specific function test
        /// </summary>
        [Test()]
        public void _01_InteractiveLoginTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                LibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                // Open RO session
                using (Session session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Login interactively via vendor specific function C_EjectToken
                    session.InteractiveLogin();
                }
            }
        }
    }
}
