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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI41
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
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKC value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKC value)
        {
            return CreateAttribute((uint)type, (uint)value);
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKK value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKK value)
        {
            return CreateAttribute((uint)type, (uint)value);
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with CKO value</returns>
        public static CK_ATTRIBUTE CreateAttribute(CKA type, CKO value)
        {
            return CreateAttribute((uint)type, (uint)value);
        }
        
        /// <summary>
        /// Creates attribute of given type with uint value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of given type with uint value</returns>
        public static CK_ATTRIBUTE CreateAttribute(uint type, uint value)
        {
            return _CreateAttribute(type, ConvertUtils.UIntToBytes(value));
        }

        /// <summary>
        /// Reads value of attribute and returns it as uint
        /// </summary>
        /// <param name="attribute">Attribute whose value should be read</param>
        /// <param name="value">Location that receives attribute value</param>
        public static void ConvertValue(ref CK_ATTRIBUTE attribute, out uint value)
        {
            byte[] bytes = ConvertValue(ref attribute);
            value = ConvertUtils.BytesToUInt(bytes);
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
            // Possible TODO - Implement with nullable DateTime

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

            if (bytes.Length == 0)
            {
                // PKCS#11 v2.20:
                // When a Cryptoki object carries an attribute of this type, and the default value of the
                // attribute is specified to be "empty," then Cryptoki libraries shall set the attribute's ulValueLen to 0.
                value = null;
                return;
            }

            if ((bytes == null) || (bytes.Length != 8))
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
                attribute.valueLen = Convert.ToUInt32(ckAttributeSize * value.Length);
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
            int attrCount = Convert.ToInt32(attribute.valueLen) / ckAttributeSize;
            int attrCountMod = Convert.ToInt32(attribute.valueLen) % ckAttributeSize;

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
                    IntPtr tempPointer = new IntPtr(attribute.value.ToInt32() + (i * ckAttributeSize));
#if SILVERLIGHT
                    CK_ATTRIBUTE_CLASS attrClass = (CK_ATTRIBUTE_CLASS)UnmanagedMemory.Read(tempPointer, typeof(CK_ATTRIBUTE_CLASS));
                    attrClass.ToCkAttributeStruct(ref attrs[i]);
#else
                    attrs[i] = (CK_ATTRIBUTE)UnmanagedMemory.Read(tempPointer, typeof(CK_ATTRIBUTE));
#endif
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
                    UnmanagedMemory.Write(tempPointer, ConvertUtils.UIntToBytes(value[i]));
                }
                attribute.valueLen = Convert.ToUInt32(ckmSize * value.Length);
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
            int attrCount = Convert.ToInt32(attribute.valueLen) / ckmSize;
            int attrCountMod = Convert.ToInt32(attribute.valueLen) % ckmSize;
            
            if (attrCountMod != 0)
                throw new Exception("Unable to convert attribute value to uint array");
            
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
                    attrs[i] = ConvertUtils.BytesToUInt(UnmanagedMemory.Read(tempPointer, ckmSize));
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
                attribute.valueLen = Convert.ToUInt32(value.Length);
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
                value = UnmanagedMemory.Read(attribute.value, Convert.ToInt32(attribute.valueLen));

            return value;
        }

        #endregion
    }
}
