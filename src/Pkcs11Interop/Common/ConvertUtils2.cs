/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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

// This file holds the code for NativeULong (UInt32 and UInt64) conversions.
// LowLevelAPI4* and HighLevelAPI4* APIs use ConvertUtils.UInt32* methods.
// Code generation script replaces them with ConvertUtils.UInt64* methods in LowLevelAPI8* and HighLevelAPI8* APIs.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps with data type conversions
    /// </summary>
    public static partial class ConvertUtils
    {
        #region CKA

        /// <summary>
        /// Converts CKA to UInt32
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>UInt32 with value from CKA</returns>
        public static UInt32 UInt32FromCKA(CKA value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKA to UInt64
        /// </summary>
        /// <param name="value">CKA that should be converted</param>
        /// <returns>UInt64 with value from CKA</returns>
        public static UInt64 UInt64FromCKA(CKA value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKA
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKA with UInt32 value</returns>
        public static CKA UInt32ToCKA(UInt32 value)
        {
            return (CKA)value;
        }

        /// <summary>
        /// Converts UInt64 to CKA
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKA with UInt64 value</returns>
        public static CKA UInt64ToCKA(UInt64 value)
        {
            return (CKA)Convert.ToUInt32(value);
        }

        #endregion

        #region CKC

        /// <summary>
        /// Converts CKC to UInt32
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>UInt32 with value from CKC</returns>
        public static UInt32 UInt32FromCKC(CKC value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKC to UInt64
        /// </summary>
        /// <param name="value">CKC that should be converted</param>
        /// <returns>UInt64 with value from CKC</returns>
        public static UInt64 UInt64FromCKC(CKC value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKC
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKC with UInt32 value</returns>
        public static CKC UInt32ToCKC(UInt32 value)
        {
            return (CKC)value;
        }

        /// <summary>
        /// Converts UInt64 to CKC
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKC with UInt64 value</returns>
        public static CKC UInt64ToCKC(UInt64 value)
        {
            return (CKC)Convert.ToUInt32(value);
        }

        #endregion

        #region CKD

        /// <summary>
        /// Converts CKD to UInt32
        /// </summary>
        /// <param name="value">CKD that should be converted</param>
        /// <returns>UInt32 with value from CKD</returns>
        public static UInt32 UInt32FromCKD(CKD value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKD to UInt64
        /// </summary>
        /// <param name="value">CKD that should be converted</param>
        /// <returns>UInt64 with value from CKD</returns>
        public static UInt64 UInt64FromCKD(CKD value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKD
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKD with UInt32 value</returns>
        public static CKD UInt32ToCKD(UInt32 value)
        {
            return (CKD)value;
        }

        /// <summary>
        /// Converts UInt64 to CKD
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKD with UInt64 value</returns>
        public static CKD UInt64ToCKD(UInt64 value)
        {
            return (CKD)Convert.ToUInt32(value);
        }

        #endregion

        #region CKG

        /// <summary>
        /// Converts CKG to UInt32
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>UInt32 with value from CKG</returns>
        public static UInt32 UInt32FromCKG(CKG value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKG to UInt64
        /// </summary>
        /// <param name="value">CKG that should be converted</param>
        /// <returns>UInt64 with value from CKG</returns>
        public static UInt64 UInt64FromCKG(CKG value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKG
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKG with UInt32 value</returns>
        public static CKG UInt32ToCKG(UInt32 value)
        {
            return (CKG)value;
        }

        /// <summary>
        /// Converts UInt64 to CKG
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKG with UInt64 value</returns>
        public static CKG UInt64ToCKG(UInt64 value)
        {
            return (CKG)Convert.ToUInt32(value);
        }

        #endregion

        #region CKH

        /// <summary>
        /// Converts CKH to UInt32
        /// </summary>
        /// <param name="value">CKH that should be converted</param>
        /// <returns>UInt32 with value from CKH</returns>
        public static UInt32 UInt32FromCKH(CKH value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKH to UInt64
        /// </summary>
        /// <param name="value">CKH that should be converted</param>
        /// <returns>UInt64 with value from CKH</returns>
        public static UInt64 UInt64FromCKH(CKH value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKH
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKH with UInt32 value</returns>
        public static CKH UInt32ToCKH(UInt32 value)
        {
            return (CKH)value;
        }

        /// <summary>
        /// Converts UInt64 to CKH
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKH with UInt64 value</returns>
        public static CKH UInt64ToCKH(UInt64 value)
        {
            return (CKH)Convert.ToUInt32(value);
        }

        #endregion

        #region CKK

        /// <summary>
        /// Converts CKK to UInt32
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>UInt32 with value from CKK</returns>
        public static UInt32 UInt32FromCKK(CKK value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKK to UInt64
        /// </summary>
        /// <param name="value">CKK that should be converted</param>
        /// <returns>UInt64 with value from CKK</returns>
        public static UInt64 UInt64FromCKK(CKK value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKK
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKK with UInt32 value</returns>
        public static CKK UInt32ToCKK(UInt32 value)
        {
            return (CKK)value;
        }

        /// <summary>
        /// Converts UInt64 to CKK
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKK with UInt64 value</returns>
        public static CKK UInt64ToCKK(UInt64 value)
        {
            return (CKK)Convert.ToUInt32(value);
        }

        #endregion

        #region CKM

        /// <summary>
        /// Converts CKM to UInt32
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>UInt32 with value from CKM</returns>
        public static UInt32 UInt32FromCKM(CKM value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKM to UInt64
        /// </summary>
        /// <param name="value">CKM that should be converted</param>
        /// <returns>UInt64 with value from CKM</returns>
        public static UInt64 UInt64FromCKM(CKM value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKM
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKM with UInt32 value</returns>
        public static CKM UInt32ToCKM(UInt32 value)
        {
            return (CKM)value;
        }

        /// <summary>
        /// Converts UInt64 to CKM
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKM with UInt64 value</returns>
        public static CKM UInt64ToCKM(UInt64 value)
        {
            return (CKM)Convert.ToUInt32(value);
        }

        #endregion

        #region CKN

        /// <summary>
        /// Converts CKN to UInt32
        /// </summary>
        /// <param name="value">CKN that should be converted</param>
        /// <returns>UInt32 with value from CKN</returns>
        public static UInt32 UInt32FromCKN(CKN value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKN to UInt64
        /// </summary>
        /// <param name="value">CKN that should be converted</param>
        /// <returns>UInt64 with value from CKN</returns>
        public static UInt64 UInt64FromCKN(CKN value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKN
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKN with UInt32 value</returns>
        public static CKN UInt32ToCKN(UInt32 value)
        {
            return (CKN)value;
        }

        /// <summary>
        /// Converts UInt64 to CKN
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKN with UInt64 value</returns>
        public static CKN UInt64ToCKN(UInt64 value)
        {
            return (CKN)Convert.ToUInt32(value);
        }

        #endregion

        #region CKO

        /// <summary>
        /// Converts CKO to UInt32
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>UInt32 with value from CKO</returns>
        public static UInt32 UInt32FromCKO(CKO value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKO to UInt64
        /// </summary>
        /// <param name="value">CKO that should be converted</param>
        /// <returns>UInt64 with value from CKO</returns>
        public static UInt64 UInt64FromCKO(CKO value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKO
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKO with UInt32 value</returns>
        public static CKO UInt32ToCKO(UInt32 value)
        {
            return (CKO)value;
        }

        /// <summary>
        /// Converts UInt64 to CKO
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKO with UInt64 value</returns>
        public static CKO UInt64ToCKO(UInt64 value)
        {
            return (CKO)Convert.ToUInt32(value);
        }

        #endregion

        #region CKP

        /// <summary>
        /// Converts CKP to UInt32
        /// </summary>
        /// <param name="value">CKP that should be converted</param>
        /// <returns>UInt32 with value from CKP</returns>
        public static UInt32 UInt32FromCKP(CKP value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKP to UInt64
        /// </summary>
        /// <param name="value">CKP that should be converted</param>
        /// <returns>UInt64 with value from CKP</returns>
        public static UInt64 UInt64FromCKP(CKP value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKP
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKP with UInt32 value</returns>
        public static CKP UInt32ToCKP(UInt32 value)
        {
            return (CKP)value;
        }

        /// <summary>
        /// Converts UInt64 to CKP
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKP with UInt64 value</returns>
        public static CKP UInt64ToCKP(UInt64 value)
        {
            return (CKP)Convert.ToUInt32(value);
        }

        #endregion

        #region CKR

        /// <summary>
        /// Converts CKR to UInt32
        /// </summary>
        /// <param name="value">CKR that should be converted</param>
        /// <returns>UInt32 with value from CKR</returns>
        public static UInt32 UInt32FromCKR(CKR value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKR to UInt64
        /// </summary>
        /// <param name="value">CKR that should be converted</param>
        /// <returns>UInt64 with value from CKR</returns>
        public static UInt64 UInt64FromCKR(CKR value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKR
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKR with UInt32 value</returns>
        public static CKR UInt32ToCKR(UInt32 value)
        {
            return (CKR)value;
        }

        /// <summary>
        /// Converts UInt64 to CKR
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKR with UInt64 value</returns>
        public static CKR UInt64ToCKR(UInt64 value)
        {
            return (CKR)Convert.ToUInt32(value);
        }

        #endregion

        #region CKS

        /// <summary>
        /// Converts CKS to UInt32
        /// </summary>
        /// <param name="value">CKS that should be converted</param>
        /// <returns>UInt32 with value from CKS</returns>
        public static UInt32 UInt32FromCKS(CKS value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKS to UInt64
        /// </summary>
        /// <param name="value">CKS that should be converted</param>
        /// <returns>UInt64 with value from CKS</returns>
        public static UInt64 UInt64FromCKS(CKS value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKS
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKS with UInt32 value</returns>
        public static CKS UInt32ToCKS(UInt32 value)
        {
            return (CKS)value;
        }

        /// <summary>
        /// Converts UInt64 to CKS
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKS with UInt64 value</returns>
        public static CKS UInt64ToCKS(UInt64 value)
        {
            return (CKS)Convert.ToUInt32(value);
        }

        #endregion

        #region CKU

        /// <summary>
        /// Converts CKU to UInt32
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>UInt32 with value from CKU</returns>
        public static UInt32 UInt32FromCKU(CKU value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts CKU to UInt64
        /// </summary>
        /// <param name="value">CKU that should be converted</param>
        /// <returns>UInt64 with value from CKU</returns>
        public static UInt64 UInt64FromCKU(CKU value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to CKU
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>CKU with UInt32 value</returns>
        public static CKU UInt32ToCKU(UInt32 value)
        {
            return (CKU)value;
        }

        /// <summary>
        /// Converts UInt64 to CKU
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>CKU with UInt64 value</returns>
        public static CKU UInt64ToCKU(UInt64 value)
        {
            return (CKU)Convert.ToUInt32(value);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Converts Int32 to UInt32
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>UInt32 with value from Int32</returns>
        public static UInt32 UInt32FromInt32(Int32 value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts Int32 to UInt64
        /// </summary>
        /// <param name="value">Int32 that should be converted</param>
        /// <returns>UInt64 with value from Int32</returns>
        public static UInt64 UInt64FromInt32(Int32 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to Int32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>Int32 with UInt32 value</returns>
        public static Int32 UInt32ToInt32(UInt32 value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to Int32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>Int32 with UInt64 value</returns>
        public static Int32 UInt64ToInt32(UInt64 value)
        {
            return Convert.ToInt32(value);
        }

        #endregion

        #region UInt32

        /// <summary>
        /// Converts UInt32 to UInt32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt32 with value from UInt32</returns>
        public static UInt32 UInt32FromUInt32(UInt32 value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt32 to UInt64
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt64 with value from UInt32</returns>
        public static UInt64 UInt64FromUInt32(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to UInt32
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt32 with UInt32 value</returns>
        public static UInt32 UInt32ToUInt32(UInt32 value)
        {
            return value;
        }

        /// <summary>
        /// Converts UInt64 to UInt32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt32 with UInt64 value</returns>
        public static UInt32 UInt64ToUInt32(UInt64 value)
        {
            return Convert.ToUInt32(value);
        }

        #endregion

        #region UInt64

        /// <summary>
        /// Converts UInt64 to UInt32
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt32 with value from UInt64</returns>
        public static UInt32 UInt32FromUInt64(UInt64 value)
        {
            return Convert.ToUInt32(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with value from UInt64</returns>
        public static UInt64 UInt64FromUInt64(UInt64 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt32 to UInt64
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt64 with UInt32 value</returns>
        public static UInt64 UInt32ToUInt64(UInt32 value)
        {
            return Convert.ToUInt64(value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with UInt64 value</returns>
        public static UInt64 UInt64ToUInt64(UInt64 value)
        {
            return Convert.ToUInt64(value);
        }

        #endregion

        #region UInt64?

        /// <summary>
        /// Converts UInt64? to UInt32?
        /// </summary>
        /// <param name="value">UInt64? that should be converted</param>
        /// <returns>UInt32? with value from UInt64?</returns>
        public static UInt32? UInt32FromUInt64(UInt64? value)
        {
            return (value == null) ? null : (UInt32?)UInt32FromUInt64(value.Value);
        }

        /// <summary>
        /// Converts UInt64? to UInt64?
        /// </summary>
        /// <param name="value">UInt64? that should be converted</param>
        /// <returns>UInt64? with value from UInt64?</returns>
        public static UInt64? UInt64FromUInt64(UInt64? value)
        {
            return (value == null) ? null : (UInt64?)UInt64FromUInt64(value.Value);
        }

        /// <summary>
        /// Converts UInt32 to UInt64
        /// </summary>
        /// <param name="value">UInt32 that should be converted</param>
        /// <returns>UInt64 with UInt32 value</returns>
        public static UInt64? UInt32ToUInt64(UInt32? value)
        {
            return (value == null) ? null : (UInt64?)UInt32ToUInt64(value.Value);
        }

        /// <summary>
        /// Converts UInt64 to UInt64
        /// </summary>
        /// <param name="value">UInt64 that should be converted</param>
        /// <returns>UInt64 with UInt64 value</returns>
        public static UInt64? UInt64ToUInt64(UInt64? value)
        {
            return (value == null) ? null : (UInt64?)UInt64ToUInt64(value.Value);
        }

        #endregion

        #region Byte array

        /// <summary>
        /// Converts byte array to UInt32
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>UInt32 with value from byte array</returns>
        public static UInt32 UInt32FromBytes(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(UInt32))))
                throw new Exception("Unable to convert bytes to UInt32");

            return BitConverter.ToUInt32(value, 0);
        }

        /// <summary>
        /// Converts byte array to UInt64
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>UInt64 with value from byte array</returns>
        public static UInt64 UInt64FromBytes(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(UInt64))))
                throw new Exception("Unable to convert bytes to UInt64");

            return BitConverter.ToUInt64(value, 0);
        }

        /// <summary>
        /// Converts UInt32 to byte array
        /// </summary>
        /// <param name='value'>UInt32 that should be converted</param>
        /// <returns>Byte array with UInt32 value</returns>
        public static byte[] UInt32ToBytes(UInt32 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(UInt32));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of UInt32 ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts UInt64 to byte array
        /// </summary>
        /// <param name='value'>UInt64 that should be converted</param>
        /// <returns>Byte array with UInt64 value</returns>
        public static byte[] UInt64ToBytes(UInt64 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(UInt64));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of UInt64 ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        #endregion
    }
}
