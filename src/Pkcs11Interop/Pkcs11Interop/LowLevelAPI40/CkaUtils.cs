﻿/*
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
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Utility class that helps to manage CK_ATTRIBUTE structure
    /// </summary>
    public static class CkaUtils
    {
        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of given type structure with no value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type));
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of given type structure with no value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type)
        {
            return _CreateAttribute(type, null);
        }

        #endregion

        #region Attribute with NativeULong value

        /// <summary>
        /// Creates attribute of given type with NativeULong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with NativeULong value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, NativeULong value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKC value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKC value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), NativeLongUtils.ConvertFromCKC(value));
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKK value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKK value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), NativeLongUtils.ConvertFromCKK(value));
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKO value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKO value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), NativeLongUtils.ConvertFromCKO(value));
        }

        /// <summary>
        /// Creates attribute of given type with NativeULong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with NativeULong value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, NativeULong value)
        {
            return _CreateAttribute(type, NativeLongUtils.ConvertToByteArray(value));
        }

        /// <summary>
        /// Reads value of attribute and returns it as NativeULong
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out NativeULong value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            value = NativeLongUtils.ConvertFromByteArray(bytes);
        }

        #endregion

        #region Attribute with bool value

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with bool value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, bool value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with bool value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, bool value)
        {
            return _CreateAttribute(type, ConvertUtils.BoolToBytes(value));
        }

        /// <summary>
        /// Reads value of attribute and returns it as bool
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out bool value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            value = ConvertUtils.BytesToBool(bytes);
        }

        #endregion

        #region Attribute with string value

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with string value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, string value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with string value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, string value)
        {
            return _CreateAttribute(type, ConvertUtils.Utf8StringToBytes(value));
        }

        /// <summary>
        /// Reads value of attribute and returns it as string
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out string value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            value = ConvertUtils.BytesToUtf8String(bytes);
        }

        #endregion

        #region Attribute with byte array value

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with byte array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, byte[] value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with byte array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, byte[] value)
        {
            return _CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as byte array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out byte[] value)
        {
            value = ConvertValue(ref attribute);
        }

        #endregion

        #region Attribute with DateTime value

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with DateTime value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, DateTime value)
        {
            // Possible TODO - Implement with nullable DateTime

            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with DateTime value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, DateTime value)
        {
            // Possible TODO - Implement with nullable DateTime

            byte[] year = ConvertUtils.Utf8StringToBytes(value.Date.Year.ToString());
            byte[] month = (value.Date.Month < 10) ? ConvertUtils.Utf8StringToBytes("0" + value.Date.Month.ToString()) : ConvertUtils.Utf8StringToBytes(value.Date.Month.ToString());
            byte[] day = (value.Date.Day < 10) ? ConvertUtils.Utf8StringToBytes("0" + value.Date.Day.ToString()) : ConvertUtils.Utf8StringToBytes(value.Date.Day.ToString());

            byte[] date = new byte[8];
            Array.Copy(year, 0, date, 0, 4);
            Array.Copy(month, 0, date, 4, 2);
            Array.Copy(day, 0, date, 6, 2);
            
            return _CreateAttribute(type, date);
        }

        /// <summary>
        /// Reads value of attribute and returns it as DateTime (CK_DATE)
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out DateTime? value)
        {
            byte[] bytes = ConvertValue(ref attribute);

            if ((bytes == null) || (bytes.Length == 0))
            {
                // PKCS#11 v2.20:
                // When a Cryptoki object carries an attribute of this type, and the default value of the
                // attribute is specified to be "empty," then Cryptoki libraries shall set the attribute's ulValueLen to 0.
                value = null;
                return;
            }

            if (bytes.Length != 8)
                throw new Exception("Unable to convert attribute value to DateTime");

            string year = ConvertUtils.BytesToUtf8String(bytes, 0, 4);
            string month = ConvertUtils.BytesToUtf8String(bytes, 4, 2);
            string day = ConvertUtils.BytesToUtf8String(bytes, 6, 2);

            if ((year == "0000") && (month == "00") && (day == "00"))
            {
                // PKCS#11 v2.20:
                // Note that implementations of previous versions of Cryptoki may have used other
                // methods to identify an "empty" attribute of type CK_DATE, and that applications that needs to
                // interoperate with these libraries therefore have to be flexible in what they accept as an empty value.
                value = null;
                return;
            }

            value = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day), 0, 0, 0, DateTimeKind.Utc);
        }

        #endregion

        #region Attribute with attribute array value

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with attribute array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CK_ATTRIBUTE[] value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with attribute array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, CK_ATTRIBUTE[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if ((value != null) && (value.Length > 0))
            {
                int ckAttributeSize = UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE));
                attribute.value = UnmanagedMemory.Allocate(ckAttributeSize * value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt64() + (i * ckAttributeSize));
                    UnmanagedMemory.Write(tempPointer, value[i]);
                }
                attribute.valueLen = NativeLongUtils.ConvertFromInt32(ckAttributeSize * value.Length);
            }
            else
            {
                attribute.value = IntPtr.Zero;
                attribute.valueLen = 0;
            }

            return attribute;
        }

        /// <summary>
        /// Reads value of attribute and returns it as attribute array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out CK_ATTRIBUTE[] value)
        {
            int ckAttributeSize = UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE));
            int attrCount = NativeLongUtils.ConvertToInt32(attribute.valueLen) / ckAttributeSize;
            int attrCountMod = NativeLongUtils.ConvertToInt32(attribute.valueLen) % ckAttributeSize;

            if (attrCountMod != 0)
                throw new Exception("Unable to convert attribute value to attribute array");

            if (attrCount == 0)
            {
                value = null;
            }
            else
            {
                CK_ATTRIBUTE[] attrs = new CK_ATTRIBUTE[attrCount];

                for (int i = 0; i < attrCount; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt64() + (i * ckAttributeSize));
                    attrs[i] = (CK_ATTRIBUTE)UnmanagedMemory.Read(tempPointer, typeof(CK_ATTRIBUTE));
                }

                value = attrs;
            }
        }

        #endregion

        #region Attribute with NativeULong array value

        /// <summary>
        /// Creates attribute of given type with NativeULong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with NativeULong array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, NativeULong[] value)
        {
            return CreateAttribute(NativeLongUtils.ConvertFromCKA(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with NativeULong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with NativeULong array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, NativeULong[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if ((value != null) && (value.Length > 0))
            {
                int ckmSize = UnmanagedMemory.SizeOf(typeof(NativeULong));
                attribute.value = UnmanagedMemory.Allocate(ckmSize * value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt64() + (i * ckmSize));
                    UnmanagedMemory.Write(tempPointer, NativeLongUtils.ConvertToByteArray(value[i]));
                }
                attribute.valueLen = NativeLongUtils.ConvertFromInt32(ckmSize * value.Length);
            }
            else
            {
                attribute.value = IntPtr.Zero;
                attribute.valueLen = 0;
            }
            
            return attribute;
        }

        /// <summary>
        /// Reads value of attribute and returns it as NativeULong array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out NativeULong[] value)
        {
            int ckmSize = UnmanagedMemory.SizeOf(typeof(NativeULong));
            int attrCount = NativeLongUtils.ConvertToInt32(attribute.valueLen) / ckmSize;
            int attrCountMod = NativeLongUtils.ConvertToInt32(attribute.valueLen) % ckmSize;
            
            if (attrCountMod != 0)
                throw new Exception("Unable to convert attribute value to NativeULong array");
            
            if (attrCount == 0)
            {
                value = null;
            }
            else
            {
                NativeULong[] attrs = new NativeULong[attrCount];
                
                for (int i = 0; i < attrCount; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt64() + (i * ckmSize));
                    attrs[i] = NativeLongUtils.ConvertFromByteArray(UnmanagedMemory.Read(tempPointer, ckmSize));
                }
                
                value = attrs;
            }
        }

        #endregion

        #region Attribute with mechanism array value

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with mechanism array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKM[] value)
        {
            NativeULong[] nativeULongArray = null;
            if (value != null)
            {
                nativeULongArray = new NativeULong[value.Length];
                for (int i = 0; i < value.Length; i++)
                    nativeULongArray[i] = NativeLongUtils.ConvertFromCKM(value[i]);
            }

            return CreateAttribute(type, nativeULongArray);
        }

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with mechanism array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(NativeULong type, CKM[] value)
        {
            NativeULong[] nativeULongArray = null;
            if (value != null)
            {
                nativeULongArray = new NativeULong[value.Length];
                for (int i = 0; i < value.Length; i++)
                    nativeULongArray[i] = NativeLongUtils.ConvertFromCKM(value[i]);
            }

            return CreateAttribute(type, nativeULongArray);
        }

        /// <summary>
        /// Reads value of attribute and returns it as mechanism array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out CKM[] value)
        {
            NativeULong[] nativeULongArray = null;
            ConvertValue(ref attribute, out nativeULongArray);

            CKM[] ckmArray = null;
            if (nativeULongArray != null)
            {
                ckmArray = new CKM[nativeULongArray.Length];
                for (int i = 0; i < nativeULongArray.Length; i++)
                    ckmArray[i] = NativeLongUtils.ConvertToCKM(nativeULongArray[i]);
            }
            value = ckmArray;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates attribute of given type with value copied from managed byte array to the newly allocated unmanaged memory
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with specified value</returns>
        private static CK_ATTRIBUTE _CreateAttribute(NativeULong type, byte[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if (value != null)
            {
                attribute.value = UnmanagedMemory.Allocate(value.Length);
                UnmanagedMemory.Write(attribute.value, value);
                attribute.valueLen = NativeLongUtils.ConvertFromInt32(value.Length);
            }
            else
            {
                attribute.value = IntPtr.Zero;
                attribute.valueLen = 0;
            }

            return attribute;
        }

        /// <summary>
        /// Copies attribute value from unmanaged memory to managed byte array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <returns>Managed copy of attribute value</returns>
        private static byte[] ConvertValue(ref CK_ATTRIBUTE attribute)
        {
            byte[] value = null;

            if (attribute.value != IntPtr.Zero)
                value = UnmanagedMemory.Read(attribute.value, NativeLongUtils.ConvertToInt32(attribute.valueLen));

            return value;
        }

        #endregion
    }
}
