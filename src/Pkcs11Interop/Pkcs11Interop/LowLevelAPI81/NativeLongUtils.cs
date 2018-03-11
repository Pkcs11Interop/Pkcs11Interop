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
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    /// <summary>
    /// Utility class that helps with NativeULong conversions
    /// </summary>
    public static class NativeLongUtils
    {

#if false // DO NOT REMOVE - UInt32 conversions managed by code generation script

        #region Byte array

        /// <summary>
        /// Converts NativeULong to byte array
        /// </summary>
        /// <param name='value'>NativeULong that should be converted</param>
        /// <returns>Byte array with NativeULong value</returns>
        public static byte[] ConvertToByteArray(NativeULong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(NativeULong));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of NativeULong ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to NativeULong
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>NativeULong with value from byte array</returns>
        public static NativeULong ConvertFromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(NativeULong))))
                throw new Exception("Unable to convert bytes to NativeULong");

            return BitConverter.ToUInt32(value, 0);
        }

        #endregion

        #region CKA

        /// <summary>
        /// Converts CKA to NativeULong
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>NativeULong with value from CKA</returns>
        public static NativeULong ConvertFromCKA(CKA value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to NativeULong
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>NativeULong with value from CKC</returns>
        public static NativeULong ConvertFromCKC(CKC value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to NativeULong
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>NativeULong with value from CKG</returns>
        public static NativeULong ConvertFromCKG(CKG value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to NativeULong
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>NativeULong with value from CKK</returns>
        public static NativeULong ConvertFromCKK(CKK value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts NativeULong to CKM
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKM with NativeULong value</returns>
        public static CKM ConvertToCKM(NativeULong value)
        {
            return (CKM)value;
        }

        /// <summary>
        /// Converts CKM to NativeULong
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>NativeULong with value from CKM</returns>
        public static NativeULong ConvertFromCKM(CKM value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts NativeULong to CKO
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKO with NativeULong value</returns>
        public static CKO ConvertToCKO(NativeULong value)
        {
            return (CKO)value;
        }

        /// <summary>
        /// Converts CKO to NativeULong
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>NativeULong with value from CKO</returns>
        public static NativeULong ConvertFromCKO(CKO value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts NativeULong to CKR
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKR with NativeULong value</returns>
        public static CKR ConvertToCKR(NativeULong value)
        {
            return (CKR)value;
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to NativeULong
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>NativeULong with value from CKU</returns>
        public static NativeULong ConvertFromCKU(CKU value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts NativeULong to Int32
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>Int32 with NativeULong value</returns>
        public static Int32 ConvertToInt32(NativeULong value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to NativeULong
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>NativeULong with value from Int32</returns>
        public static NativeULong ConvertFromInt32(Int32 value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts NativeULong to UInt32
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>UInt32 with NativeULong value</returns>
        public static UInt32 ConvertToUInt32(NativeULong value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt32 to NativeULong
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>NativeULong with value from UInt32</returns>
        public static NativeULong ConvertFromUInt32(UInt32 value)
        {
            return value;
        }

        #endregion

        #region UInt64

        /// <summary>
        /// Converts NativeULong to UInt64
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>UInt64 with NativeULong value</returns>
        public static UInt64 ConvertToUInt64(NativeULong value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to NativeULong
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>NativeULong with value from UInt64</returns>
        public static NativeULong ConvertFromUInt64(UInt64 value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

#endif // DO NOT REMOVE - UInt32 conversions managed by code generation script

#if true // DO NOT REMOVE - UInt64 conversions managed by code generation script

        #region Byte array

        /// <summary>
        /// Converts NativeULong to byte array
        /// </summary>
        /// <param name='value'>NativeULong that should be converted</param>
        /// <returns>Byte array with NativeULong value</returns>
        public static byte[] ConvertToByteArray(NativeULong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(NativeULong));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of NativeULong ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to NativeULong
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>NativeULong with value from byte array</returns>
        public static NativeULong ConvertFromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(NativeULong))))
                throw new Exception("Unable to convert bytes to NativeULong");

            return BitConverter.ToUInt64(value, 0);
        }

        #endregion

        #region CKA

        /// <summary>
        /// Converts CKA to NativeULong
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>NativeULong with value from CKA</returns>
        public static NativeULong ConvertFromCKA(CKA value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to NativeULong
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>NativeULong with value from CKC</returns>
        public static NativeULong ConvertFromCKC(CKC value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to NativeULong
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>NativeULong with value from CKG</returns>
        public static NativeULong ConvertFromCKG(CKG value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to NativeULong
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>NativeULong with value from CKK</returns>
        public static NativeULong ConvertFromCKK(CKK value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts NativeULong to CKM
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKM with NativeULong value</returns>
        public static CKM ConvertToCKM(NativeULong value)
        {
            return (CKM)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKM to NativeULong
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>NativeULong with value from CKM</returns>
        public static NativeULong ConvertFromCKM(CKM value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts NativeULong to CKO
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKO with NativeULong value</returns>
        public static CKO ConvertToCKO(NativeULong value)
        {
            return (CKO)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKO to NativeULong
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>NativeULong with value from CKO</returns>
        public static NativeULong ConvertFromCKO(CKO value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts NativeULong to CKR
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>CKR with NativeULong value</returns>
        public static CKR ConvertToCKR(NativeULong value)
        {
            return (CKR)Convert.ToUInt32(value);
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to NativeULong
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>NativeULong with value from CKU</returns>
        public static NativeULong ConvertFromCKU(CKU value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts NativeULong to Int32
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>Int32 with NativeULong value</returns>
        public static Int32 ConvertToInt32(NativeULong value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to NativeULong
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>NativeULong with value from Int32</returns>
        public static NativeULong ConvertFromInt32(Int32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts NativeULong to UInt32
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>UInt32 with NativeULong value</returns>
        public static UInt32 ConvertToUInt32(NativeULong value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt32 to NativeULong
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>NativeULong with value from UInt32</returns>
        public static NativeULong ConvertFromUInt32(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt64

        /// <summary>
        /// Converts NativeULong to UInt64
        /// </summary>
        /// <param name="value">NativeULong that should be converted</param>
        /// <returns>UInt64 with NativeULong value</returns>
        public static UInt64 ConvertToUInt64(NativeULong value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to NativeULong
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>NativeULong with value from UInt64</returns>
        public static NativeULong ConvertFromUInt64(UInt64 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

#endif // DO NOT REMOVE - UInt64 conversions managed by code generation script

    }
}
