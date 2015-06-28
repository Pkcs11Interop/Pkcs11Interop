/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
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
            internal delegate CKR C_EjectToken4x(uint slotId);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate CKR C_EjectToken8x(ulong slotId);
        }

        /// <summary>
        /// Definitions of vendor specific unmanaged method
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern CKR C_EjectToken4x(uint slotId);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern CKR C_EjectToken8x(ulong slotId);
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
                cEjectToken = (Delegates.C_EjectToken4x)Marshal.GetDelegateForFunctionPointer(cEjectTokenPtr, typeof(Delegates.C_EjectToken4x));
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken4x;
            }

            return cEjectToken(slotId);
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
                cEjectToken = (Delegates.C_EjectToken4x)Marshal.GetDelegateForFunctionPointer(cEjectTokenPtr, typeof(Delegates.C_EjectToken4x));
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken4x;
            }

            return cEjectToken(slotId);
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
                cEjectToken = (Delegates.C_EjectToken8x)Marshal.GetDelegateForFunctionPointer(cEjectTokenPtr, typeof(Delegates.C_EjectToken8x));
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken8x;
            }

            return cEjectToken(slotId);
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
                cEjectToken = (Delegates.C_EjectToken8x)Marshal.GetDelegateForFunctionPointer(cEjectTokenPtr, typeof(Delegates.C_EjectToken8x));
            }
            else
            {
                cEjectToken = NativeMethods.C_EjectToken8x;
            }

            return cEjectToken(slotId);
        }

        #endregion
    }

    /// <summary>
    /// Tests the possibility to extend Slot classes with a vendor specific function
    /// </summary>
    [TestFixture()]
    public class _29_SlotClassExtensionsTest
    {
        /// <summary>
        /// C_EjectToken vendor specific function test
        /// </summary>
        [Test()]
        public void _01_EjectTokenTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                LibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                // Eject token via vendor specific function C_EjectToken
                slot.EjectToken();
            }
        }
    }
}
