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
using Net.Pkcs11Interop.Common;
using NativeLong = System.UInt32;

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Utility class that helps with NativeLong conversions
    /// </summary>
    public static class NativeLongUtils
    {

#if true // DO NOT REMOVE - UInt32 conversions managed by code generation script

        #region Byte array

        /// <summary>
        /// Converts NativeLong to byte array
        /// </summary>
        /// <param name='value'>NativeLong that should be converted</param>
        /// <returns>Byte array with NativeLong value</returns>
        public static byte[] ConvertToByteArray(NativeLong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(NativeLong));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of NativeLong ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to NativeLong
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>NativeLong with value from byte array</returns>
        public static NativeLong ConvertFromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(NativeLong))))
                throw new Exception("Unable to convert bytes to NativeLong");

            return BitConverter.ToUInt32(value, 0);
        }

        #endregion

        #region CKA

        /// <summary>
        /// Converts CKA to NativeLong
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>NativeLong with value from CKA</returns>
        public static NativeLong ConvertFromCKA(CKA value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to NativeLong
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>NativeLong with value from CKC</returns>
        public static NativeLong ConvertFromCKC(CKC value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to NativeLong
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>NativeLong with value from CKG</returns>
        public static NativeLong ConvertFromCKG(CKG value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to NativeLong
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>NativeLong with value from CKK</returns>
        public static NativeLong ConvertFromCKK(CKK value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts NativeLong to CKM
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKM with NativeLong value</returns>
        public static CKM ConvertToCKM(NativeLong value)
        {
            return (CKM)value;
        }

        /// <summary>
        /// Converts CKM to NativeLong
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>NativeLong with value from CKM</returns>
        public static NativeLong ConvertFromCKM(CKM value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts NativeLong to CKO
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKO with NativeLong value</returns>
        public static CKO ConvertToCKO(NativeLong value)
        {
            return (CKO)value;
        }

        /// <summary>
        /// Converts CKO to NativeLong
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>NativeLong with value from CKO</returns>
        public static NativeLong ConvertFromCKO(CKO value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts NativeLong to CKR
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKR with NativeLong value</returns>
        public static CKR ConvertToCKR(NativeLong value)
        {
            return (CKR)value;
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to NativeLong
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>NativeLong with value from CKU</returns>
        public static NativeLong ConvertFromCKU(CKU value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts NativeLong to Int32
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>Int32 with NativeLong value</returns>
        public static Int32 ConvertToInt32(NativeLong value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to NativeLong
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>NativeLong with value from Int32</returns>
        public static NativeLong ConvertFromInt32(Int32 value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts NativeLong to UInt32
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>UInt32 with NativeLong value</returns>
        public static UInt32 ConvertToUInt32(NativeLong value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt32 to NativeLong
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>NativeLong with value from UInt32</returns>
        public static NativeLong ConvertFromUInt32(UInt32 value)
        {
            return value;
        }

        #endregion

#endif // DO NOT REMOVE - UInt32 conversions managed by code generation script

#if false // DO NOT REMOVE - UInt64 conversions managed by code generation script

        #region Byte array

        /// <summary>
        /// Converts NativeLong to byte array
        /// </summary>
        /// <param name='value'>NativeLong that should be converted</param>
        /// <returns>Byte array with NativeLong value</returns>
        public static byte[] ConvertToByteArray(NativeLong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(NativeLong));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of NativeLong ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to NativeLong
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>NativeLong with value from byte array</returns>
        public static NativeLong ConvertFromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(NativeLong))))
                throw new Exception("Unable to convert bytes to NativeLong");

            return BitConverter.ToUInt64(value, 0);
        }

        #endregion

        #region CKA

        /// <summary>
        /// Converts CKA to NativeLong
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>NativeLong with value from CKA</returns>
        public static NativeLong ConvertFromCKA(CKA value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to NativeLong
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>NativeLong with value from CKC</returns>
        public static NativeLong ConvertFromCKC(CKC value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to NativeLong
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>NativeLong with value from CKG</returns>
        public static NativeLong ConvertFromCKG(CKG value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to NativeLong
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>NativeLong with value from CKK</returns>
        public static NativeLong ConvertFromCKK(CKK value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts NativeLong to CKM
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKM with NativeLong value</returns>
        public static CKM ConvertToCKM(NativeLong value)
        {
            return (CKM)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKM to NativeLong
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>NativeLong with value from CKM</returns>
        public static NativeLong ConvertFromCKM(CKM value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts NativeLong to CKO
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKO with NativeLong value</returns>
        public static CKO ConvertToCKO(NativeLong value)
        {
            return (CKO)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKO to NativeLong
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>NativeLong with value from CKO</returns>
        public static NativeLong ConvertFromCKO(CKO value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts NativeLong to CKR
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>CKR with NativeLong value</returns>
        public static CKR ConvertToCKR(NativeLong value)
        {
            return (CKR)Convert.ToUInt32(value);
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to NativeLong
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>NativeLong with value from CKU</returns>
        public static NativeLong ConvertFromCKU(CKU value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts NativeLong to Int32
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>Int32 with NativeLong value</returns>
        public static Int32 ConvertToInt32(NativeLong value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to NativeLong
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>NativeLong with value from Int32</returns>
        public static NativeLong ConvertFromInt32(Int32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts NativeLong to UInt32
        /// </summary>
        /// <param name="value">NativeLong that should be converted</param>
        /// <returns>UInt32 with NativeLong value</returns>
        public static UInt32 ConvertToUInt32(NativeLong value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt32 to NativeLong
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>NativeLong with value from UInt32</returns>
        public static NativeLong ConvertFromUInt32(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

#endif // DO NOT REMOVE - UInt64 conversions managed by code generation script

    }
}
