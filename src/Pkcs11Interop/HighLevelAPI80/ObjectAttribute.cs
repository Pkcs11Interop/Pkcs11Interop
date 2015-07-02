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
using Net.Pkcs11Interop.LowLevelAPI80;

namespace Net.Pkcs11Interop.HighLevelAPI80
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
        public ulong Type
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
                return ((long)_ckAttribute.valueLen == -1);
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
        public ObjectAttribute(ulong type)
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

        #region Attribute with ulong value

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, ulong value)
        {
            _ckAttribute = CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, ulong value)
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
        /// Reads value of attribute and returns it as ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        public ulong GetValueAsUlong()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            ulong value = 0;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
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

            bool value = false;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
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

            string value = null;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
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

            byte[] value = null;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
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

            DateTime? value = null;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
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
        /// Reads value of attribute and returns it as attribute array (CURRENTLY NOT IMPLEMENTED)
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ObjectAttribute> GetValueAsObjectAttributeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

        #endregion

        #region Attribute with ulong array value

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(ulong type, List<ulong> value)
        {
            ulong[] ulongs = null;
            
            if (value != null)
                ulongs = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, ulongs);
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ulong> value)
        {
            ulong[] ulongs = null;

            if (value != null)
                ulongs = value.ToArray();
            
            _ckAttribute = CkaUtils.CreateAttribute(type, ulongs);
        }

        /// <summary>
        /// Reads value of attribute and returns it as list of ulongs
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<ulong> GetValueAsUlongList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            ulong[] value = null;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return (value == null) ? null : new List<ulong>(value);
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

            CKM[] value = null;
            CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return (value == null) ? null : new List<CKM>(value);
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
