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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps with UInt32 and UInt64 conversions
    /// </summary>
    public static class NativeULongUtils
    {
        #region Byte array

        /// <summary>
        /// Converts UInt32 to byte array
        /// </summary>
        /// <param name='value'>UInt32 that should be converted</param>
        /// <returns>Byte array with UInt32 value</returns>
        public static byte[] ConvertUInt32ToByteArray(UInt32 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(UInt32));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of UInt32 ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to UInt32
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>UInt32 with value from byte array</returns>
        public static UInt32 ConvertUInt32FromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(UInt32))))
                throw new Exception("Unable to convert bytes to UInt32");

            return BitConverter.ToUInt32(value, 0);
        }

        /// <summary>
        /// Converts UInt64 to byte array
        /// </summary>
        /// <param name='value'>UInt64 that should be converted</param>
        /// <returns>Byte array with UInt64 value</returns>
        public static byte[] ConvertUInt64ToByteArray(UInt64 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(UInt64));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of UInt64 ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to UInt64
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>UInt64 with value from byte array</returns>
        public static UInt64 ConvertUInt64FromByteArray(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(UInt64))))
                throw new Exception("Unable to convert bytes to UInt64");

            return BitConverter.ToUInt64(value, 0);
        }

        #endregion

        #region CKA

        /// <summary>
        /// Converts CKA to UInt32
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>UInt32 with value from CKA</returns>
        public static UInt32 ConvertUInt32FromCKA(CKA value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKA to UInt64
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>UInt64 with value from CKA</returns>
        public static UInt64 ConvertUInt64FromCKA(CKA value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to UInt32
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>UInt32 with value from CKC</returns>
        public static UInt32 ConvertUInt32FromCKC(CKC value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKC to UInt64
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>UInt64 with value from CKC</returns>
        public static UInt64 ConvertUInt64FromCKC(CKC value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to UInt32
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>UInt32 with value from CKG</returns>
        public static UInt32 ConvertUInt32FromCKG(CKG value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKG to UInt64
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>UInt64 with value from CKG</returns>
        public static UInt64 ConvertUInt64FromCKG(CKG value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to UInt32
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>UInt32 with value from CKK</returns>
        public static UInt32 ConvertUInt32FromCKK(CKK value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKK to UInt64
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>UInt64 with value from CKK</returns>
        public static UInt64 ConvertUInt64FromCKK(CKK value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts UInt32 to CKM
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKM with UInt32 value</returns>
        public static CKM ConvertUInt32ToCKM(UInt32 value)
        {
            return (CKM)value;
        }

        /// <summary>
        /// Converts CKM to UInt32
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>UInt32 with value from CKM</returns>
        public static UInt32 ConvertUInt32FromCKM(CKM value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to CKM
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKM with UInt64 value</returns>
        public static CKM ConvertUInt64ToCKM(UInt64 value)
        {
            return (CKM)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKM to UInt64
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>UInt64 with value from CKM</returns>
        public static UInt64 ConvertUInt64FromCKM(CKM value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts UInt32 to CKO
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKO with UInt32 value</returns>
        public static CKO ConvertUInt32ToCKO(UInt32 value)
        {
            return (CKO)value;
        }

        /// <summary>
        /// Converts CKO to UInt32
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>UInt32 with value from CKO</returns>
        public static UInt32 ConvertUInt32FromCKO(CKO value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to CKO
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKO with UInt64 value</returns>
        public static CKO ConvertUInt64ToCKO(UInt64 value)
        {
            return (CKO)Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKO to UInt64
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>UInt64 with value from CKO</returns>
        public static UInt64 ConvertUInt64FromCKO(CKO value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts UInt32 to CKR
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKR with UInt32 value</returns>
        public static CKR ConvertUInt32ToCKR(UInt32 value)
        {
            return (CKR)value;
        }

        /// <summary>
        /// Converts UInt64 to CKR
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKR with UInt64 value</returns>
        public static CKR ConvertUInt64ToCKR(UInt64 value)
        {
            return (CKR)Convert.ToUInt32(value);
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to UInt32
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>UInt32 with value from CKU</returns>
        public static UInt32 ConvertUInt32FromCKU(CKU value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKU to UInt64
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>UInt64 with value from CKU</returns>
        public static UInt64 ConvertUInt64FromCKU(CKU value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts UInt32 to Int32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>Int32 with UInt32 value</returns>
        public static Int32 ConvertUInt32ToInt32(UInt32 value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to UInt32
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>UInt32 with value from Int32</returns>
        public static UInt32 ConvertUInt32FromInt32(Int32 value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to Int32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>Int32 with UInt64 value</returns>
        public static Int32 ConvertUInt64ToInt32(UInt64 value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts Int32 to UInt64
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>UInt64 with value from Int32</returns>
        public static UInt64 ConvertUInt64FromInt32(Int32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts UInt32 to UInt32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt32 with UInt32 value</returns>
        public static UInt32 ConvertUInt32ToUInt32(UInt32 value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt32 to UInt32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt32 with value from UInt32</returns>
        public static UInt32 ConvertUInt32FromUInt32(UInt32 value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt64 to UInt32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt32 with UInt64 value</returns>
        public static UInt32 ConvertUInt64ToUInt32(UInt64 value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt32 to UInt64
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt64 with value from UInt32</returns>
        public static UInt64 ConvertUInt64FromUInt32(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt64

        /// <summary>
        /// Converts UInt32 to UInt64
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt64 with UInt32 value</returns>
        public static UInt64 ConvertUInt32ToUInt64(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt32 with value from UInt64</returns>
        public static UInt32 ConvertUInt32FromUInt64(UInt64 value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt32 with value from UInt64</returns>
        public static UInt32? ConvertUInt32FromUInt64(UInt64? value)
        {
            return (value == null) ? null : ConvertUInt32FromUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with UInt64 value</returns>
        public static UInt64 ConvertUInt64ToUInt64(UInt64 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with value from UInt64</returns>
        public static UInt64 ConvertUInt64FromUInt64(UInt64 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with value from UInt64</returns>
        public static UInt64? ConvertUInt64FromUInt64(UInt64? value)
        {
            return (value == null) ? null : ConvertUInt64FromUInt64(value);
        }

        #endregion
    }
}
