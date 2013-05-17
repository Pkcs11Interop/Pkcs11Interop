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
using System.Collections.Generic;
using System.Text;
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
        /// Low level attribute structure
        /// </summary>
        private LowLevelAPI.CK_ATTRIBUTE _ckAttribute;

        /// <summary>
        /// Low level attribute structure
        /// </summary>
        internal LowLevelAPI.CK_ATTRIBUTE CkAttribute
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
        public uint Type
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

                return ((int)_ckAttribute.valueLen == -1);
            }
        }

        /// <summary>
        /// Creates attribute defined by low level CK_ATTRIBUTE structure
        /// </summary>
        /// <param name="attribute">CK_ATTRIBUTE structure</param>
        internal ObjectAttribute(LowLevelAPI.CK_ATTRIBUTE attribute)
        {
            _ckAttribute = attribute;
        }

        #region Attribute with no value

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(uint type)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type);
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        public ObjectAttribute(CKA type)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type);
        }

        #endregion

        #region Attribute with uint value

        /// <summary>
        /// Creates attribute of given type with uint value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, uint value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with uint value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, uint value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as uint
        /// </summary>
        /// <returns>Value of attribute</returns>
        public uint GetValueAsUint()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint value = 0;
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
        }

        #endregion

        #region Attribute with bool value

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, bool value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, bool value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
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
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
        }

        #endregion

        #region Attribute with string value

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, string value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, string value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
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
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
        }

        #endregion

        #region Attribute with byte array value

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, byte[] value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, byte[] value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
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
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
        }

        #endregion

        #region Attribute with DateTime value

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, DateTime value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, DateTime value)
        {
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, value);
        }

        /// <summary>
        /// Reads value of attribute and returns it as DateTime
        /// </summary>
        /// <returns>Value of attribute</returns>
        public DateTime GetValueAsDateTime()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            DateTime value = DateTime.UtcNow;
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return value;
        }

        #endregion

        #region Attribute with attribute array value

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, List<ObjectAttribute> value)
        {
            LowLevelAPI.CK_ATTRIBUTE[] attributes = null;

            if (value != null)
            {
                attributes = new LowLevelAPI.CK_ATTRIBUTE[value.Count];
                for (int i = 0; i < value.Count; i++)
                    attributes[i] = value[i].CkAttribute;
            }

            // Note: Each attribute in the input list still owns unmanaged memory used by its value and will free it when disposed.
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, attributes);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<ObjectAttribute> value)
        {
            LowLevelAPI.CK_ATTRIBUTE[] attributes = null;
            
            if (value != null)
            {
                attributes = new LowLevelAPI.CK_ATTRIBUTE[value.Count];
                for (int i = 0; i < value.Count; i++)
                    attributes[i] = value[i].CkAttribute;
            }

            // Note: Each attribute in the input list still owns unmanaged memory used by its value and will free it when disposed.
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, attributes);
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

        #region Attribute with uint array value

        /// <summary>
        /// Creates attribute of given type with uint array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, List<uint> value)
        {
            uint[] uints = null;
            
            if (value != null)
                uints = value.ToArray();
            
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, uints);
        }

        /// <summary>
        /// Creates attribute of given type with uint array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(CKA type, List<uint> value)
        {
            uint[] uints = null;

            if (value != null)
                uints = value.ToArray();
            
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, uints);
        }

        /// <summary>
        /// Reads value of attribute and returns it as list of uints
        /// </summary>
        /// <returns>Value of attribute</returns>
        public List<uint> GetValueAsUintList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint[] value = null;
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
            return (value == null) ? null : new List<uint>(value);
        }

        #endregion

        #region Attribute with mechanism array value

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        public ObjectAttribute(uint type, List<CKM> value)
        {
            CKM[] mechanisms = null;
            
            if (value != null)
                mechanisms = value.ToArray();
            
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, mechanisms);
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
            
            _ckAttribute = LowLevelAPI.CkaUtils.CreateAttribute(type, mechanisms);
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
            LowLevelAPI.CkaUtils.ConvertValue(ref _ckAttribute, out value);
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
                LowLevelAPI.UnmanagedMemory.Free(ref _ckAttribute.value);
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
