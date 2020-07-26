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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI40;
using NativeLong = System.Int32;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Attribute of cryptoki object (CK_ATTRIBUTE alternative)
    /// </summary>
    public class ObjectAttribute : IObjectAttribute
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        protected bool _disposed = false;

        /// <summary>
        /// Low level attribute structure
        /// </summary>
        protected CK_ATTRIBUTE _ckAttribute;

        /// <summary>
        /// Attribute type
        /// </summary>
        public ulong Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return ConvertUtils.UInt32ToUInt64(_ckAttribute.type);
            }
        }

        /// <summary>
        /// Flag indicating whether attribute value cannot be read either because object is sensitive or unextractable or because specified attribute for the object is invalid.
        /// </summary>
        public bool CannotBeRead
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                // PKCS#11 v2.20 page 133:
                // If the specified attribute (i.e., the attribute specified by the type field) for the object
                // cannot be revealed because the object is sensitive or unextractable, then the
                // ulValueLen field in that triple is modified to hold the value -1 (i.e., when it is cast to a
                // CK_LONG, it holds -1).
                return ((NativeLong)_ckAttribute.valueLen == -1);
            }
        }

        /// <summary>
        /// Returns managed object corresponding to CK_ATTRIBUTE structure that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _ckAttribute;
        }

        /// <summary>
        /// Creates attribute defined by low level CK_ATTRIBUTE structure
        /// </summary>
        /// <param name="attribute">CK_ATTRIBUTE structure</param>
        protected internal ObjectAttribute(CK_ATTRIBUTE attribute)
        {
            _ckAttribute = attribute;
        }

        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(ulong type)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type));
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(CKA type)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type);
        }

        #endregion

        #region Attribute with ulong value

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, ulong value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), ConvertUtils.UInt32FromUInt64(value));
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, ulong value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, ConvertUtils.UInt32FromUInt64(value));
        }

        /// <summary>
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKC value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKK value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKO value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public ulong GetValueAsUlong()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                NativeULong value = 0;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return ConvertUtils.UInt32ToUInt64(value);
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with bool value

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, bool value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, bool value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as bool
        /// </summary>
        /// <returns>Value of attribute</returns>
        public bool GetValueAsBool()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                bool value = false;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return value;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with string value

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, string value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, string value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as string
        /// </summary>
        /// <returns>Value of attribute</returns>
        public string GetValueAsString()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                string value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return value;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with byte array value

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, byte[] value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, byte[] value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as byte array
        /// </summary>
        /// <returns>Value of attribute</returns>
        public byte[] GetValueAsByteArray()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                byte[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return value;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with DateTime value

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, DateTime value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, DateTime value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as DateTime
        /// </summary>
        /// <returns>Value of attribute</returns>
        public DateTime? GetValueAsDateTime()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                DateTime? value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return value;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with attribute array value

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, List<IObjectAttribute> value)
        {
            CK_ATTRIBUTE[] attributes = null;

            if (value != null)
            {
                attributes = new CK_ATTRIBUTE[value.Count];

                try
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        CK_ATTRIBUTE attribute = (CK_ATTRIBUTE)value[i].ToMarshalableStructure();
                        attributes[i] = DuplicateAttribute(ref attribute);
                    }
                }
                catch
                {
                    for (int i = 0; i < value.Count; i++)
                        FreeAttribute(ref attributes[i]);

                    throw;
                }
            }

            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), attributes);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<IObjectAttribute> value)
        {
            CK_ATTRIBUTE[] attributes = null;
            
            if (value != null)
            {
                attributes = new CK_ATTRIBUTE[value.Count];

                try
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        CK_ATTRIBUTE attribute = (CK_ATTRIBUTE)value[i].ToMarshalableStructure();
                        attributes[i] = DuplicateAttribute(ref attribute);
                    }
                }
                catch
                {
                    for (int i = 0; i < value.Count; i++)
                        FreeAttribute(ref attributes[i]);

                    throw;
                }
            }

            _ckAttribute = CkaUtils.CreateAttribute(type, attributes);
        }

        /// <summary>
        /// Reads value of attribute and returns it as attribute array
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<IObjectAttribute> GetValueAsObjectAttributeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                CK_ATTRIBUTE[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);

                List<IObjectAttribute> attributes = null;

                if (value != null)
                {
                    attributes = new List<IObjectAttribute>();
                    for (int i = 0; i < value.Length; i++)
                    {
                        CK_ATTRIBUTE copy = DuplicateAttribute(ref value[i]);
                        attributes.Add(new ObjectAttribute(copy));
                    }
                }

                return attributes;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        /// <summary>
        /// Creates copy of low level attribute
        /// </summary>
        /// <param name="attribute">Attribute to be copied</param>
        /// <returns>Copy of low level attribute</returns>
        protected CK_ATTRIBUTE DuplicateAttribute(ref CK_ATTRIBUTE attribute)
        {
            if (!MiscSettings.AttributesWithNestedAttributes.ContainsKey(ConvertUtils.UInt32ToUInt64(attribute.type)))
            {
                byte[] value = null;
                CkaUtils.ConvertValue(ref attribute, out value);
                return CkaUtils.CreateAttribute(attribute.type, value);
            }
            else
            {
                CK_ATTRIBUTE[] srcNestedAttrs = null;
                CkaUtils.ConvertValue(ref attribute, out srcNestedAttrs);

                CK_ATTRIBUTE[] dstNestedAttrs = new CK_ATTRIBUTE[srcNestedAttrs.Length];
                for (int i = 0; i < srcNestedAttrs.Length; i++)
                    dstNestedAttrs[i] = DuplicateAttribute(ref srcNestedAttrs[i]);

                return CkaUtils.CreateAttribute(attribute.type, dstNestedAttrs);
            }
        }

        #endregion

        #region Attribute with ulong array value

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, List<ulong> value)
        {
            NativeULong[] array = null;
            
            if (value != null)
            {
                array = new NativeULong[value.Count];
                for (int i = 0; i < value.Count; i++)
                    array[i] = ConvertUtils.UInt32FromUInt64(value[i]);
            }
            
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), array);
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ulong> value)
        {
            NativeULong[] array = null;

            if (value != null)
            {
                array = new NativeULong[value.Count];
                for (int i = 0; i < value.Count; i++)
                    array[i] = ConvertUtils.UInt32FromUInt64(value[i]);
            }

            _ckAttribute = CkaUtils.CreateAttribute(type, array);
        }

        /// <summary>
        /// Reads value of attribute and returns it as list of ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ulong> GetValueAsULongList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                NativeULong[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);

                List<ulong> ulongs = null;
                if (value != null)
                {
                    ulongs = new List<ulong>();
                    for (int i = 0; i < value.Length; i++)
                        ulongs.Add(ConvertUtils.UInt32ToUInt64(value[i]));
                }

                return ulongs;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with mechanism array value

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, List<CKM> value)
        {
            CKM[] mechanisms = null;
            
            if (value != null)
                mechanisms = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(type), mechanisms);
        }
        
        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<CKM> value)
        {
            CKM[] mechanisms = null;
            
            if (value != null)
                mechanisms = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, mechanisms);
        }
        
        /// <summary>
        /// Reads value of attribute and returns it as list of mechanisms
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<CKM> GetValueAsCkmList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                CKM[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return (value == null) ? null : new List<CKM>(value);
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                }

                // Dispose unmanaged objects

                FreeAttribute(ref _ckAttribute);

                _disposed = true;
            }
        }

        /// <summary>
        /// Frees low level attribute
        /// </summary>
        /// <param name="attribute">Attribute to be freed</param>
        protected void FreeAttribute(ref CK_ATTRIBUTE attribute)
        {
            if (MiscSettings.AttributesWithNestedAttributes.ContainsKey(ConvertUtils.UInt32ToUInt64(attribute.type)))
            {
                CK_ATTRIBUTE[] nestedAttributes = null;

                if (!CannotBeRead)
                    CkaUtils.ConvertValue(ref attribute, out nestedAttributes);

                if (nestedAttributes != null)
                {
                    for (int i = 0; i < nestedAttributes.Length; i++)
                        FreeAttribute(ref nestedAttributes[i]);
                }
            }

            Common.UnmanagedMemory.Free(ref attribute.value);
            attribute.valueLen = 0;
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~ObjectAttribute()
        {
            Dispose(false);
        }

        #endregion
    }
}
