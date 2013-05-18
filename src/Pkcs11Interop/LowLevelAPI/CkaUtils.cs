/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;
using System.Text;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Utility class that helps to manage CK_ATTRIBUTE structure
    /// </summary>
    public class CkaUtils
    {
        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of given type structure with no value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type)
        {
            return CreateAttribute((uint)type);
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of given type structure with no value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type)
        {
            return _CreateAttribute(type, null);
        }

        #endregion

        #region Attribute with uint value

        /// <summary>
        /// Creates attribute of given type with uint value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with uint value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, uint value)
        {
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with uint value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with uint value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, uint value)
        {
            return _CreateAttribute(type, ConvertUtils.UintToBytes(value));
        }

        /// <summary>
        /// Reads value of attribute and returns it as uint
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out uint value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            value = ConvertUtils.BytesToUint(bytes);
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
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with bool value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, bool value)
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
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with string value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, string value)
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
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with byte array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, byte[] value)
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
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with DateTime value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, DateTime value)
        {
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
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out DateTime value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            if ((bytes == null) || (bytes.Length != 8))
                throw new Pkcs11InteropException("Unable to convert attribute value to DateTime");

            string year = ConvertUtils.BytesToUtf8String(bytes, 0, 4);
            string month = ConvertUtils.BytesToUtf8String(bytes, 4, 2);
            string day = ConvertUtils.BytesToUtf8String(bytes, 6, 2);

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
            return CreateAttribute((uint)type, value);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with attribute array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, CK_ATTRIBUTE[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if ((value != null) && (value.Length > 0))
            {
                int ckAttributeSize = UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE));
                attribute.value = UnmanagedMemory.Allocate(ckAttributeSize * value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt32() + (i * ckAttributeSize));
                    UnmanagedMemory.Write(tempPointer, value[i]);
                }
                attribute.valueLen = (uint)(ckAttributeSize * value.Length);
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
            int attrCount = (int)attribute.valueLen / ckAttributeSize;
            int attrCountMod = (int)attribute.valueLen % ckAttributeSize;

            if (attrCountMod != 0)
                throw new Pkcs11InteropException("Unable to convert attribute value to attribute array");

            if (attrCount == 0)
            {
                value = null;
            }
            else
            {
                CK_ATTRIBUTE[] attrs = new CK_ATTRIBUTE[attrCount];

                for (int i = 0; i < attrCount; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt32() + (i * ckAttributeSize));
                    attrs[i] = (CK_ATTRIBUTE)UnmanagedMemory.Read(tempPointer, typeof(CK_ATTRIBUTE));
                }

                value = attrs;
            }
        }

        #endregion

        #region Attribute with uint array value

        /// <summary>
        /// Creates attribute of given type with uint array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with uint array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, uint[] value)
        {
            return CreateAttribute((uint)type, value);
        }
        
        /// <summary>
        /// Creates attribute of given type with uint array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with uint array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, uint[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if ((value != null) && (value.Length > 0))
            {
                int ckmSize = UnmanagedMemory.SizeOf(typeof(uint));
                attribute.value = UnmanagedMemory.Allocate(ckmSize * value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt32() + (i * ckmSize));
                    UnmanagedMemory.Write(tempPointer, ConvertUtils.UintToBytes((uint)value[i]));
                }
                attribute.valueLen = (uint)(ckmSize * value.Length);
            }
            else
            {
                attribute.value = IntPtr.Zero;
                attribute.valueLen = 0;
            }
            
            return attribute;
        }

        /// <summary>
        /// Reads value of attribute and returns it as uint array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out uint[] value)
        {
            int ckmSize = UnmanagedMemory.SizeOf(typeof(uint));
            int attrCount = (int)attribute.valueLen / ckmSize;
            int attrCountMod = (int)attribute.valueLen % ckmSize;
            
            if (attrCountMod != 0)
                throw new Pkcs11InteropException("Unable to convert attribute value to uint array");
            
            if (attrCount == 0)
            {
                value = null;
            }
            else
            {
                uint[] attrs = new uint[attrCount];
                
                for (int i = 0; i < attrCount; i++)
                {
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt32() + (i * ckmSize));
                    attrs[i] = ConvertUtils.BytesToUint(UnmanagedMemory.Read(tempPointer, ckmSize));
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
            uint[] uintArray = null;
            if (value != null)
            {
                uintArray = new uint[value.Length];
                for (int i = 0; i < value.Length; i++)
                    uintArray[i] = (uint)value[i];
            }

            return CreateAttribute(type, uintArray);
        }

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with mechanism array value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, CKM[] value)
        {
            uint[] uintArray = null;
            if (value != null)
            {
                uintArray = new uint[value.Length];
                for (int i = 0; i < value.Length; i++)
                    uintArray[i] = (uint)value[i];
            }

            return CreateAttribute(type, uintArray);
        }

        /// <summary>
        /// Reads value of attribute and returns it as mechanism array
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out CKM[] value)
        {
            uint[] uintArray = null;
            ConvertValue(ref attribute, out uintArray);

            CKM[] ckmArray = null;
            if (uintArray != null)
            {
                ckmArray = new CKM[uintArray.Length];
                for (int i = 0; i < uintArray.Length; i++)
                    ckmArray[i] = (CKM)uintArray[i];
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
        private static CK_ATTRIBUTE _CreateAttribute(uint type, byte[] value)
        {
            CK_ATTRIBUTE attribute = new CK_ATTRIBUTE();
            attribute.type = type;
            if (value != null)
            {
                attribute.value = UnmanagedMemory.Allocate(value.Length);
                UnmanagedMemory.Write(attribute.value, value);
                attribute.valueLen = (uint)value.Length;
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
                value = UnmanagedMemory.Read(attribute.value, (int)attribute.valueLen);

            return value;
        }

        #endregion
    }
}
