/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
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
        /// Platform specific ObjectAttribute
        /// </summary>
        private HighLevelAPI4.ObjectAttribute _objectAttribute4 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI4.ObjectAttribute ObjectAttribute4
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute4;
            }
        }

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        private HighLevelAPI8.ObjectAttribute _objectAttribute8 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI8.ObjectAttribute ObjectAttribute8
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute8;
            }
        }

        /// <summary>
        /// Attribute type
        /// </summary>
        public ulong Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? _objectAttribute4.Type : _objectAttribute8.Type;
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

                return (UnmanagedLong.Size == 4) ? _objectAttribute4.CannotBeRead : _objectAttribute8.CannotBeRead;
            }
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI4.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute4 = objectAttribute;
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI8.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute8 = objectAttribute;
        }

        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(ulong type)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type));
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type);
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(CKA type)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type);
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), Convert.ToUInt32(value));
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, ulong value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, Convert.ToUInt32(value));
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKC value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKK value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKO value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public ulong GetValueAsUlong()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsUint();
            else
                return _objectAttribute8.GetValueAsUlong();
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, bool value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as bool
        /// </summary>
        /// <returns>Value of attribute</returns>
        public bool GetValueAsBool()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsBool();
            else
                return _objectAttribute8.GetValueAsBool();
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, string value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as string
        /// </summary>
        /// <returns>Value of attribute</returns>
        public string GetValueAsString()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsString();
            else
                return _objectAttribute8.GetValueAsString();
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, byte[] value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as byte array
        /// </summary>
        /// <returns>Value of attribute</returns>
        public byte[] GetValueAsByteArray()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsByteArray();
            else
                return _objectAttribute8.GetValueAsByteArray();
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, DateTime value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as DateTime
        /// </summary>
        /// <returns>Value of attribute</returns>
        public DateTime? GetValueAsDateTime()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsDateTime();
            else
                return _objectAttribute8.GetValueAsDateTime();
        }

        #endregion

        #region Attribute with attribute array value

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, List<ObjectAttribute> value)
        {
            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> attributes = ConvertToHighLevelAPI4List(value);
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), attributes);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> attributes = ConvertToHighLevelAPI8List(value);
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, attributes);
            }
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ObjectAttribute> value)
        {
            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> attributes = ConvertToHighLevelAPI4List(value);
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, attributes);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> attributes = ConvertToHighLevelAPI8List(value);
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, attributes);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as attribute array (CURRENTLY NOT IMPLEMENTED)
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ObjectAttribute> GetValueAsObjectAttributeList()
        {
            // TODO : In order to implement and properly test this tricky method it is crucial to find PKCS#11 implementation that supports CKA.CKA_WRAP_TEMPLATE and CKA.CKA_UNWRAP_TEMPLATE
            throw new NotImplementedException();
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
            if (UnmanagedLong.Size == 4)
            {
                List<uint> uintList = null;

                if (value != null)
                {
                    uintList = new List<uint>();
                    for (int i = 0; i < value.Count; i++)
                        uintList.Add(Convert.ToUInt32(value[i]));
                }

                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), uintList);
            }
            else
            {
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ulong> value)
        {
            if (UnmanagedLong.Size == 4)
            {
                List<uint> uintList = null;

                if (value != null)
                {
                    uintList = new List<uint>();
                    for (int i = 0; i < value.Count; i++)
                        uintList.Add(Convert.ToUInt32(value[i]));
                }

                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, uintList);
            }
            else
            {
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as list of ulongs
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ulong> GetValueAsUlongList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
            {
                List<uint> uintList = _objectAttribute4.GetValueAsUintList();

                List<ulong> ulongList = null;

                if (uintList != null)
                {
                    ulongList = new List<ulong>();
                    for (int i = 0; i < uintList.Count; i++)
                        ulongList.Add(Convert.ToUInt64(uintList[i]));
                }

                return ulongList;
            }
            else
            {
                return _objectAttribute8.GetValueAsUlongList();
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
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(Convert.ToUInt32(type), value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }
        
        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<CKM> value)
        {
            if (UnmanagedLong.Size == 4)
                _objectAttribute4 = new HighLevelAPI4.ObjectAttribute(type, value);
            else
                _objectAttribute8 = new HighLevelAPI8.ObjectAttribute(type, value);
        }
        
        /// <summary>
        /// Reads value of attribute and returns it as list of mechanisms
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<CKM> GetValueAsCkmList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _objectAttribute4.GetValueAsCkmList();
            else
                return _objectAttribute8.GetValueAsCkmList();
        }

        #endregion

        #region Conversions

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI4.ObjectAttribute> ConvertToHighLevelAPI4List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI4.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI4.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute4);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI4List(List<HighLevelAPI4.ObjectAttribute> hlaAttributes)
        {
            List<ObjectAttribute> attributes = null;

            if (hlaAttributes != null)
            {
                attributes = new List<ObjectAttribute>();
                for (int i = 0; i < hlaAttributes.Count; i++)
                    attributes.Add(new ObjectAttribute(hlaAttributes[i]));
            }

            return attributes;
        }

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI8.ObjectAttribute> ConvertToHighLevelAPI8List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI8.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI8.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute8);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI8List(List<HighLevelAPI8.ObjectAttribute> hlaAttributes)
        {
            List<ObjectAttribute> attributes = null;

            if (hlaAttributes != null)
            {
                attributes = new List<ObjectAttribute>();
                for (int i = 0; i < hlaAttributes.Count; i++)
                    attributes.Add(new ObjectAttribute(hlaAttributes[i]));
            }

            return attributes;
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
                    if (_objectAttribute4 != null)
                    {
                        _objectAttribute4.Dispose();
                        _objectAttribute4 = null;
                    }

                    if (_objectAttribute8 != null)
                    {
                        _objectAttribute8.Dispose();
                        _objectAttribute8 = null;
                    }
                }

                // Dispose unmanaged objects

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
