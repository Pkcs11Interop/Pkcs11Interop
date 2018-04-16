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
    /*
    /// <summary>
    /// Extends Slot classes with the ability to call C_EjectToken function.
    /// C_EjectToken function is vendor specific function of PKCS11-MOCK module.
    /// </summary>
    public static class SlotClassExtensions
    {
        #region HighLevelAPI extensions

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="slot">Instance of the extended class</param>
        public static void EjectToken(this Slot slot)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    slot.HLA40Slot.EjectToken();
                }
                else
                {
                    slot.HLA41Slot.EjectToken();
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    slot.HLA80Slot.EjectToken();
                }
                else
                {
                    slot.HLA81Slot.EjectToken();
                }
            }
        }

        #endregion

        #region HighLevelAPI4x and HighLevelAPI8x extensions

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="slot">Instance of the extended class</param>
        public static void EjectToken(this HLA40.Slot slot)
        {
            CKR rv = slot.LowLevelPkcs11.C_EjectToken(slot.SlotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EjectToken", rv);
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="slot">Instance of the extended class</param>
        public static void EjectToken(this HLA41.Slot slot)
        {
            CKR rv = slot.LowLevelPkcs11.C_EjectToken(slot.SlotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EjectToken", rv);
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="slot">Instance of the extended class</param>
        public static void EjectToken(this HLA80.Slot slot)
        {
            CKR rv = slot.LowLevelPkcs11.C_EjectToken(slot.SlotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EjectToken", rv);
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="slot">Instance of the extended class</param>
        public static void EjectToken(this HLA81.Slot slot)
        {
            CKR rv = slot.LowLevelPkcs11.C_EjectToken(slot.SlotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EjectToken", rv);
        }

        #endregion

        #region LowLevelAPI4x and LowLevelAPI8x extensions

        /// <summary>
        /// Delegates for vendor specific unmanaged method
        /// </summary>
        private static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate uint C_EjectToken4x(uint slotId);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate ulong C_EjectToken8x(ulong slotId);
        }

        /// <summary>
        /// Definitions of vendor specific unmanaged method
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("__Internal", EntryPoint = "C_EjectToken", CallingConvention = CallingConvention.Cdecl)]
            internal static extern uint C_EjectToken4x(uint slotId);

            [DllImport("__Internal", EntryPoint = "C_EjectToken", CallingConvention = CallingConvention.Cdecl)]
            internal static extern ulong C_EjectToken8x(ulong slotId);
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SLOT_ID_INVALID, CKR_OK</returns>
        public static CKR C_EjectToken(this LLA40.Pkcs11 pkcs11, uint slotId)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_EjectToken4x cEjectToken = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cEjectTokenPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_EjectToken");
                cEjectToken = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_EjectToken4x>(cEjectTokenPtr);
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken4x;
            }

            uint rv = cEjectToken(slotId);
            return (CKR)rv;
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SLOT_ID_INVALID, CKR_OK</returns>
        public static CKR C_EjectToken(this LLA41.Pkcs11 pkcs11, uint slotId)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_EjectToken4x cEjectToken = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cEjectTokenPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_EjectToken");
                cEjectToken = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_EjectToken4x>(cEjectTokenPtr);
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken4x;
            }

            uint rv = cEjectToken(slotId);
            return (CKR)rv;
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SLOT_ID_INVALID, CKR_OK</returns>
        public static CKR C_EjectToken(this LLA80.Pkcs11 pkcs11, ulong slotId)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_EjectToken8x cEjectToken = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cEjectTokenPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_EjectToken");
                cEjectToken = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_EjectToken8x>(cEjectTokenPtr);
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken8x;
            }

            ulong rv = cEjectToken(slotId);
            return (CKR)Convert.ToUInt32(rv);
        }

        /// <summary>
        /// Ejects token from slot.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SLOT_ID_INVALID, CKR_OK</returns>
        public static CKR C_EjectToken(this LLA81.Pkcs11 pkcs11, ulong slotId)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_EjectToken8x cEjectToken = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cEjectTokenPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_EjectToken");
                cEjectToken = UnmanagedLibrary.GetDelegateForFunctionPointer<Delegates.C_EjectToken8x>(cEjectTokenPtr);
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken8x;
            }

            ulong rv = cEjectToken(slotId);
            return (CKR)Convert.ToUInt32(rv);
        }

        #endregion
    }
    */

    /// <summary>
    /// Tests the possibility to extend Slot classes with a vendor specific function
    /// </summary>
    [TestFixture()]
    public class _29_SlotClassExtensionTest
    {
        /// <summary>
        /// C_EjectToken vendor specific function test
        /// </summary>
        [Test()]
        public void _01_EjectTokenTest()
        {
            throw new NotImplementedException();

            /*
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                LibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                // Eject token via vendor specific function C_EjectToken
                slot.EjectToken();
            }
            */
        }
    }
}
