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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;
using NativeULong = System.UInt64;
using NativeLong = System.Int32;

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Attribute of cryptoki object (CK_ATTRIBUTE alternative)
    /// </summary>
    public class ObjectAttribute : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level attribute structure
        /// </summary>
        private CK_ATTRIBUTE _ckAttribute;

        /// <summary>
        /// Low level attribute structure
        /// </summary>
        internal CK_ATTRIBUTE CkAttribute
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _ckAttribute;
            }
        }

        /// <summary>
        /// Attribute type
        /// </summary>
        public NativeULong Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _ckAttribute.type;
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
        /// Creates attribute defined by low level CK_ATTRIBUTE structure
        /// </summary>
        /// <param name="attribute">CK_ATTRIBUTE structure</param>
        internal ObjectAttribute(CK_ATTRIBUTE attribute)
        {
            _ckAttribute = attribute;
        }

        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(NativeULong type)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type);
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

        #region Attribute with NativeULong value

        /// <summary>
        /// Creates attribute of given type with NativeULong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(NativeULong type, NativeULong value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with NativeULong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, NativeULong value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
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
        /// Reads value of attribute and returns it as NativeULong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public NativeULong GetValueAsNativeUlong()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                NativeULong value = 0;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return value;
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
        public ObjectAttribute(NativeULong type, bool value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
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
        public ObjectAttribute(NativeULong type, string value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
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
        public ObjectAttribute(NativeULong type, byte[] value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
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
        public ObjectAttribute(NativeULong type, DateTime value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
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
        public ObjectAttribute(NativeULong type, List<ObjectAttribute> value)
        {
            CK_ATTRIBUTE[] attributes = null;

            if (value != null)
            {
                attributes = new CK_ATTRIBUTE[value.Count];
                for (int i = 0; i < value.Count; i++)
                    attributes[i] = value[i].CkAttribute;
            }

            // Note: Each attribute in the input list still owns unmanaged memory used by its value and will free it when disposed.
            _ckAttribute = CkaUtils.CreateAttribute(type, attributes);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ObjectAttribute> value)
        {
            CK_ATTRIBUTE[] attributes = null;
            
            if (value != null)
            {
                attributes = new CK_ATTRIBUTE[value.Count];
                for (int i = 0; i < value.Count; i++)
                    attributes[i] = value[i].CkAttribute;
            }

            // Note: Each attribute in the input list still owns unmanaged memory used by its value and will free it when disposed.
            _ckAttribute = CkaUtils.CreateAttribute(type, attributes);
        }

        /// <summary>
        /// Reads value of attribute and returns it as attribute array
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ObjectAttribute> GetValueAsObjectAttributeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                CK_ATTRIBUTE[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);

                List<ObjectAttribute> attributes = null;

                if (value != null)
                {
                    attributes = new List<ObjectAttribute>();
                    for (int i = 0; i < value.Length; i++)
                        attributes.Add(new ObjectAttribute(value[i]));
                }

                return attributes;
            }
            catch (Exception ex)
            {
                throw new AttributeValueException(this.Type, ex);
            }
        }

        #endregion

        #region Attribute with NativeULong array value

        /// <summary>
        /// Creates attribute of given type with NativeULong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(NativeULong type, List<NativeULong> value)
        {
            NativeULong[] array = null;
            
            if (value != null)
                array = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, array);
        }

        /// <summary>
        /// Creates attribute of given type with NativeULong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<NativeULong> value)
        {
            NativeULong[] array = null;

            if (value != null)
                array = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, array);
        }

        /// <summary>
        /// Reads value of attribute and returns it as list of NativeULong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<NativeULong> GetValueAsNativeULongList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (this.CannotBeRead)
                throw new AttributeValueException(this.Type);

            try
            {
                NativeULong[] value = null;
                CkaUtils.ConvertValue(ref _ckAttribute, out value);
                return (value == null) ? null : new List<NativeULong>(value);
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
        public ObjectAttribute(NativeULong type, List<CKM> value)
        {
            CKM[] mechanisms = null;
            
            if (value != null)
                mechanisms = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, mechanisms);
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
                Common.UnmanagedMemory.Free(ref _ckAttribute.value);
                _ckAttribute.valueLen = 0;

                _disposed = true;
            }
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
