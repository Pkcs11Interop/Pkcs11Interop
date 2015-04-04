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
        private HighLevelAPI40.ObjectAttribute _objectAttribute40 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI40.ObjectAttribute ObjectAttribute40
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute40;
            }
        }

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        private HighLevelAPI41.ObjectAttribute _objectAttribute41 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI41.ObjectAttribute ObjectAttribute41
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute41;
            }
        }

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        private HighLevelAPI80.ObjectAttribute _objectAttribute80 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI80.ObjectAttribute ObjectAttribute80
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute80;
            }
        }

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        private HighLevelAPI81.ObjectAttribute _objectAttribute81 = null;

        /// <summary>
        /// Platform specific ObjectAttribute
        /// </summary>
        internal HighLevelAPI81.ObjectAttribute ObjectAttribute81
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _objectAttribute81;
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

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _objectAttribute40.Type : _objectAttribute41.Type;
                else
                    return (Platform.StructPackingSize == 0) ? _objectAttribute80.Type : _objectAttribute81.Type;
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

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _objectAttribute40.CannotBeRead : _objectAttribute41.CannotBeRead;
                else
                    return (Platform.StructPackingSize == 0) ? _objectAttribute80.CannotBeRead : _objectAttribute81.CannotBeRead;
            }
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI40.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute40 = objectAttribute;
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI41.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute41 = objectAttribute;
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI80.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute80 = objectAttribute;
        }

        /// <summary>
        /// Converts platform specific ObjectAttribute to platfrom neutral ObjectAttribute
        /// </summary>
        /// <param name="objectAttribute">Platform specific ObjectAttribute</param>
        internal ObjectAttribute(HighLevelAPI81.ObjectAttribute objectAttribute)
        {
            if (objectAttribute == null)
                throw new ArgumentNullException("objectAttribute");

            _objectAttribute81 = objectAttribute;
        }

        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(ulong type)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type));
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type);
            }
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(CKA type)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type);
            }
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), Convert.ToUInt32(value));
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), Convert.ToUInt32(value));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, ulong value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), Convert.ToUInt32(value));
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), Convert.ToUInt32(value));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKC value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKK value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, CKO value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public ulong GetValueAsUlong()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _objectAttribute40.GetValueAsUint() : _objectAttribute41.GetValueAsUint();
            else
                return (Platform.StructPackingSize == 0) ? _objectAttribute80.GetValueAsUlong() : _objectAttribute81.GetValueAsUlong();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, bool value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as bool
        /// </summary>
        /// <returns>Value of attribute</returns>
        public bool GetValueAsBool()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _objectAttribute40.GetValueAsBool() : _objectAttribute41.GetValueAsBool();
            else
                return (Platform.StructPackingSize == 0) ? _objectAttribute80.GetValueAsBool() : _objectAttribute81.GetValueAsBool();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, string value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as string
        /// </summary>
        /// <returns>Value of attribute</returns>
        public string GetValueAsString()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _objectAttribute40.GetValueAsString() : _objectAttribute41.GetValueAsString();
            else
                return (Platform.StructPackingSize == 0) ? _objectAttribute80.GetValueAsString() : _objectAttribute81.GetValueAsString();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, byte[] value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as byte array
        /// </summary>
        /// <returns>Value of attribute</returns>
        public byte[] GetValueAsByteArray()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _objectAttribute40.GetValueAsByteArray() : _objectAttribute41.GetValueAsByteArray();
            else
                return (Platform.StructPackingSize == 0) ? _objectAttribute80.GetValueAsByteArray() : _objectAttribute81.GetValueAsByteArray();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, DateTime value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as DateTime
        /// </summary>
        /// <returns>Value of attribute</returns>
        public DateTime? GetValueAsDateTime()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _objectAttribute40.GetValueAsDateTime() : _objectAttribute41.GetValueAsDateTime();
            else
                return (Platform.StructPackingSize == 0) ? _objectAttribute80.GetValueAsDateTime() : _objectAttribute81.GetValueAsDateTime();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), ConvertToHighLevelAPI40List(value));
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), ConvertToHighLevelAPI41List(value));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, ConvertToHighLevelAPI80List(value));
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, ConvertToHighLevelAPI81List(value));
            }
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ObjectAttribute> value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, ConvertToHighLevelAPI40List(value));
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, ConvertToHighLevelAPI41List(value));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, ConvertToHighLevelAPI80List(value));
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, ConvertToHighLevelAPI81List(value));
            }
        }

        /// <summary>
        /// Reads value of attribute and returns it as attribute array (CURRENTLY NOT IMPLEMENTED)
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ObjectAttribute> GetValueAsObjectAttributeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return ConvertFromHighLevelAPI40List(_objectAttribute40.GetValueAsObjectAttributeList());
                else
                    return ConvertFromHighLevelAPI41List(_objectAttribute41.GetValueAsObjectAttributeList());
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return ConvertFromHighLevelAPI80List(_objectAttribute80.GetValueAsObjectAttributeList());
                else
                    return ConvertFromHighLevelAPI81List(_objectAttribute81.GetValueAsObjectAttributeList());
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
            if (Platform.UnmanagedLongSize == 4)
            {
                List<uint> uintList = null;

                if (value != null)
                {
                    uintList = new List<uint>();
                    for (int i = 0; i < value.Count; i++)
                        uintList.Add(Convert.ToUInt32(value[i]));
                }

                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), uintList);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), uintList);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ulong> value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                List<uint> uintList = null;

                if (value != null)
                {
                    uintList = new List<uint>();
                    for (int i = 0; i < value.Count; i++)
                        uintList.Add(Convert.ToUInt32(value[i]));
                }

                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, uintList);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, uintList);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
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

            if (Platform.UnmanagedLongSize == 4)
            {
                List<uint> uintList = null;

                if (Platform.StructPackingSize == 0)
                    uintList = _objectAttribute40.GetValueAsUintList();
                else
                    uintList = _objectAttribute41.GetValueAsUintList();

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
                if (Platform.StructPackingSize == 0)
                    return _objectAttribute80.GetValueAsUlongList();
                else
                    return _objectAttribute81.GetValueAsUlongList();
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
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(Convert.ToUInt32(type), value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(Convert.ToUInt32(type), value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }
        
        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<CKM> value)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute40 = new HighLevelAPI40.ObjectAttribute(type, value);
                else
                    _objectAttribute41 = new HighLevelAPI41.ObjectAttribute(type, value);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectAttribute80 = new HighLevelAPI80.ObjectAttribute(type, value);
                else
                    _objectAttribute81 = new HighLevelAPI81.ObjectAttribute(type, value);
            }
        }
        
        /// <summary>
        /// Reads value of attribute and returns it as list of mechanisms
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<CKM> GetValueAsCkmList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _objectAttribute40.GetValueAsCkmList();
                else
                    return _objectAttribute41.GetValueAsCkmList();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _objectAttribute80.GetValueAsCkmList();
                else
                    return _objectAttribute81.GetValueAsCkmList();
            }
        }

        #endregion

        #region Conversions

        #region HighLevelAPI40

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI40.ObjectAttribute> ConvertToHighLevelAPI40List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI40.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI40.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute40);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI40List(List<HighLevelAPI40.ObjectAttribute> hlaAttributes)
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

        #region HighLevelAPI41

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI41.ObjectAttribute> ConvertToHighLevelAPI41List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI41.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI41.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute41);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI41List(List<HighLevelAPI41.ObjectAttribute> hlaAttributes)
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

        #region HighLevelAPI80

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI80.ObjectAttribute> ConvertToHighLevelAPI80List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI80.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI80.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute80);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI80List(List<HighLevelAPI80.ObjectAttribute> hlaAttributes)
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

        #region HighLevelAPI81

        /// <summary>
        /// Converts platfrom neutral ObjectAttributes to platform specific ObjectAttributes
        /// </summary>
        /// <param name="attributes">Platfrom neutral ObjectAttributes</param>
        /// <returns>Platform specific ObjectAttributes</returns>
        internal static List<HighLevelAPI81.ObjectAttribute> ConvertToHighLevelAPI81List(List<ObjectAttribute> attributes)
        {
            List<HighLevelAPI81.ObjectAttribute> hlaAttributes = null;

            if (attributes != null)
            {
                hlaAttributes = new List<HighLevelAPI81.ObjectAttribute>();
                for (int i = 0; i < attributes.Count; i++)
                    hlaAttributes.Add(attributes[i].ObjectAttribute81);
            }

            return hlaAttributes;
        }

        /// <summary>
        /// Converts platform specific ObjectAttributes to platfrom neutral ObjectAttributes
        /// </summary>
        /// <param name="hlaAttributes">Platform specific ObjectAttributes</param>
        /// <returns>Platfrom neutral ObjectAttributes</returns>
        internal static List<ObjectAttribute> ConvertFromHighLevelAPI81List(List<HighLevelAPI81.ObjectAttribute> hlaAttributes)
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
                    if (_objectAttribute40 != null)
                    {
                        _objectAttribute40.Dispose();
                        _objectAttribute40 = null;
                    }

                    if (_objectAttribute41 != null)
                    {
                        _objectAttribute41.Dispose();
                        _objectAttribute41 = null;
                    }

                    if (_objectAttribute80 != null)
                    {
                        _objectAttribute80.Dispose();
                        _objectAttribute80 = null;
                    }

                    if (_objectAttribute81 != null)
                    {
                        _objectAttribute81.Dispose();
                        _objectAttribute81 = null;
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
