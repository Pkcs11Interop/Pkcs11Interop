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
using System.Collections.Generic;
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
    /// Extends Pkcs11 classes with the ability to call C_GetUnmanagedStructSizeList function.
    /// C_GetUnmanagedStructSizeList function is vendor specific function of PKCS11-MOCK module.
    /// </summary>
    public static class Pkcs11ClassExtensions
    {
        #region HighLevelAPI extensions

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <returns>List of unmanaged struct sizes</returns>
        public static List<ulong> GetUnmanagedStructSizeList(this Pkcs11 pkcs11)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                List<ulong> sizeList = new List<ulong>();

                if (Platform.StructPackingSize == 0)
                {
                    List<uint> hlaSizeList = pkcs11.HLA40Pkcs11.GetUnmanagedStructSizeList();
                    foreach (uint size in hlaSizeList)
                        sizeList.Add(Convert.ToUInt64(size));
                }
                else
                {
                    List<uint> hlaSizeList = pkcs11.HLA41Pkcs11.GetUnmanagedStructSizeList();
                    foreach (uint size in hlaSizeList)
                        sizeList.Add(Convert.ToUInt64(size));
                }

                return sizeList;
            }
            else
            {
                List<ulong> sizeList = new List<ulong>();

                if (Platform.StructPackingSize == 0)
                {
                    sizeList = pkcs11.HLA80Pkcs11.GetUnmanagedStructSizeList();
                }
                else
                {
                    sizeList = pkcs11.HLA81Pkcs11.GetUnmanagedStructSizeList();
                }

                return sizeList;
            }
        }

        #endregion

        #region HighLevelAPI4x and HighLevelAPI8x extensions

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <returns>List of unmanaged struct sizes</returns>
        public static List<uint> GetUnmanagedStructSizeList(this HLA40.Pkcs11 pkcs11)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            uint sizeCount = 0;
            CKR rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            uint[] sizeList = new uint[sizeCount];
            rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != sizeCount)
                Array.Resize(ref sizeList, Convert.ToInt32(sizeCount));

            return new List<uint>(sizeList);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <returns>List of unmanaged struct sizes</returns>
        public static List<uint> GetUnmanagedStructSizeList(this HLA41.Pkcs11 pkcs11)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            uint sizeCount = 0;
            CKR rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            uint[] sizeList = new uint[sizeCount];
            rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != sizeCount)
                Array.Resize(ref sizeList, Convert.ToInt32(sizeCount));

            return new List<uint>(sizeList);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <returns>List of unmanaged struct sizes</returns>
        public static List<ulong> GetUnmanagedStructSizeList(this HLA80.Pkcs11 pkcs11)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            ulong sizeCount = 0;
            CKR rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            ulong[] sizeList = new ulong[sizeCount];
            rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != Convert.ToInt32(sizeCount))
                Array.Resize(ref sizeList, Convert.ToInt32(sizeCount));

            return new List<ulong>(sizeList);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <returns>List of unmanaged struct sizes</returns>
        public static List<ulong> GetUnmanagedStructSizeList(this HLA81.Pkcs11 pkcs11)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            ulong sizeCount = 0;
            CKR rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            ulong[] sizeList = new ulong[sizeCount];
            rv = pkcs11.LowLevelPkcs11.C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != Convert.ToInt32(sizeCount))
                Array.Resize(ref sizeList, Convert.ToInt32(sizeCount));

            return new List<ulong>(sizeList);
        }

        #endregion

        #region LowLevelAPI4x and LowLevelAPI8x extensions

        /// <summary>
        /// Delegates for vendor specific unmanaged method
        /// </summary>
        private static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate CKR C_GetUnmanagedStructSizeListDelegate4x(uint[] sizeList, ref uint count);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate CKR C_GetUnmanagedStructSizeListDelegate8x(ulong[] sizeList, ref ulong count);
        }

        /// <summary>
        /// Definitions of vendor specific unmanaged method
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern CKR C_GetUnmanagedStructSizeList4x(uint[] sizeList, ref uint count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern CKR C_GetUnmanagedStructSizeList8x(ulong[] sizeList, ref ulong count);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="sizeList">
        /// If set to null then the number of sizes is returned in "count" parameter, without actually returning a list of sizes.
        /// If not set to null then "count" parameter must contain the lenght of sizeList array and size list is returned in "sizeList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of sizes</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_OK</returns>
        public static CKR C_GetUnmanagedStructSizeList(this LLA40.Pkcs11 pkcs11, uint[] sizeList, ref uint count)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_GetUnmanagedStructSizeListDelegate4x cGetUnmanagedStructSizeList = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cGetUnmanagedStructSizeListPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_GetUnmanagedStructSizeList");
                cGetUnmanagedStructSizeList = (Delegates.C_GetUnmanagedStructSizeListDelegate4x)Marshal.GetDelegateForFunctionPointer(cGetUnmanagedStructSizeListPtr, typeof(Delegates.C_GetUnmanagedStructSizeListDelegate4x));
            }
            else
            {
                cGetUnmanagedStructSizeList = NativeMethods.C_GetUnmanagedStructSizeList4x;
            }

            return cGetUnmanagedStructSizeList(sizeList, ref count);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="sizeList">
        /// If set to null then the number of sizes is returned in "count" parameter, without actually returning a list of sizes.
        /// If not set to null then "count" parameter must contain the lenght of sizeList array and size list is returned in "sizeList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of sizes</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_OK</returns>
        public static CKR C_GetUnmanagedStructSizeList(this LLA41.Pkcs11 pkcs11, uint[] sizeList, ref uint count)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_GetUnmanagedStructSizeListDelegate4x cGetUnmanagedStructSizeList = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cGetUnmanagedStructSizeListPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_GetUnmanagedStructSizeList");
                cGetUnmanagedStructSizeList = (Delegates.C_GetUnmanagedStructSizeListDelegate4x)Marshal.GetDelegateForFunctionPointer(cGetUnmanagedStructSizeListPtr, typeof(Delegates.C_GetUnmanagedStructSizeListDelegate4x));
            }
            else
            {
                cGetUnmanagedStructSizeList = NativeMethods.C_GetUnmanagedStructSizeList4x;
            }

            return cGetUnmanagedStructSizeList(sizeList, ref count);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="sizeList">
        /// If set to null then the number of sizes is returned in "count" parameter, without actually returning a list of sizes.
        /// If not set to null then "count" parameter must contain the lenght of sizeList array and size list is returned in "sizeList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of sizes</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_OK</returns>
        public static CKR C_GetUnmanagedStructSizeList(this LLA80.Pkcs11 pkcs11, ulong[] sizeList, ref ulong count)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_GetUnmanagedStructSizeListDelegate8x cGetUnmanagedStructSizeList = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cGetUnmanagedStructSizeListPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_GetUnmanagedStructSizeList");
                cGetUnmanagedStructSizeList = (Delegates.C_GetUnmanagedStructSizeListDelegate8x)Marshal.GetDelegateForFunctionPointer(cGetUnmanagedStructSizeListPtr, typeof(Delegates.C_GetUnmanagedStructSizeListDelegate8x));
            }
            else
            {
                cGetUnmanagedStructSizeList = NativeMethods.C_GetUnmanagedStructSizeList8x;
            }

            return cGetUnmanagedStructSizeList(sizeList, ref count);
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// This method should be used only for testing purposes with PKCS11-MOCK module.
        /// </summary>
        /// <param name="pkcs11">Instance of the extended class</param>
        /// <param name="sizeList">
        /// If set to null then the number of sizes is returned in "count" parameter, without actually returning a list of sizes.
        /// If not set to null then "count" parameter must contain the lenght of sizeList array and size list is returned in "sizeList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of sizes</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_OK</returns>
        public static CKR C_GetUnmanagedStructSizeList(this LLA81.Pkcs11 pkcs11, ulong[] sizeList, ref ulong count)
        {
            if (pkcs11.Disposed)
                throw new ObjectDisposedException(pkcs11.GetType().FullName);

            Delegates.C_GetUnmanagedStructSizeListDelegate8x cGetUnmanagedStructSizeList = null;

            if (pkcs11.LibraryHandle != IntPtr.Zero)
            {
                IntPtr cGetUnmanagedStructSizeListPtr = UnmanagedLibrary.GetFunctionPointer(pkcs11.LibraryHandle, "C_GetUnmanagedStructSizeList");
                cGetUnmanagedStructSizeList = (Delegates.C_GetUnmanagedStructSizeListDelegate8x)Marshal.GetDelegateForFunctionPointer(cGetUnmanagedStructSizeListPtr, typeof(Delegates.C_GetUnmanagedStructSizeListDelegate8x));
            }
            else
            {
                cGetUnmanagedStructSizeList = NativeMethods.C_GetUnmanagedStructSizeList8x;
            }

            return cGetUnmanagedStructSizeList(sizeList, ref count);
        }

        #endregion
    }

    /// <summary>
    /// Tests the possibility to extend Pkcs11 classes with a vendor specific function
    /// </summary>
    [TestFixture()]
    public partial class _28_Pkcs11ClassExtensionTest
    {
        /// <summary>
        /// C_GetUnmanagedStructSizeList vendor specific function test
        /// </summary>
        [Test()]
        public void _01_LibraryInfoMatches()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                LibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Obtain a list of unmanaged struct sizes via vendor specific function C_GetUnmanagedStructSizeList
                List<ulong> unmanagedSizes = pkcs11.GetUnmanagedStructSizeList();

                // Obtain a list of managed struct sizes
                List<ulong> managedSizes = GetManagedStructSizeList();

                // Compare sizes of unmanaged and managed structs
                Assert.IsTrue(unmanagedSizes.Count == managedSizes.Count);
                for (int i = 0; i < unmanagedSizes.Count; i++)
                    Assert.IsTrue(unmanagedSizes[i] == managedSizes[i]);
            }
        }

        /// <summary>
        /// Obtains a list of managed struct sizes.
        /// </summary>
        /// <returns>List of managed struct sizes</returns>
        private List<ulong> GetManagedStructSizeList()
        {
            List<ulong> sizeList = new List<ulong>();

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_C_INITIALIZE_ARGS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI40.CK_FUNCTION_LIST"))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
                else
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_C_INITIALIZE_ARGS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI41.CK_FUNCTION_LIST"))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_C_INITIALIZE_ARGS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI80.CK_FUNCTION_LIST"))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
                else
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_C_INITIALIZE_ARGS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI81.CK_FUNCTION_LIST"))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
            }

            return sizeList;
        }
    }
}
