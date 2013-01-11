/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using Net.Pkcs11Interop.Common;
using System.Text;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_CMS_SIG mechanism
    /// </summary>
    public class CkCmsSigParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_CMS_SIG_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_CMS_SIG_PARAMS();

        /// <summary>
        /// Object handle for a certificate associated with the signing key
        /// </summary>
        public uint CertificateHandle
        {
            get
            {
                return _lowLevelStruct.CertificateHandle;
            }
            set
            {
                _lowLevelStruct.CertificateHandle = value;
            }
        }
        
        /// <summary>
        /// Mechanism to use when signing a constructed CMS SignedAttributes value
        /// </summary>
        public uint? SigningMechanism
        {
            get
            {
                if (_lowLevelStruct.SigningMechanism == IntPtr.Zero)
                    return null;

                int uintSize = LowLevelAPI.UnmanagedMemory.SizeOf(typeof(uint));
                byte[] uintValue = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.SigningMechanism, uintSize);
                return BitConverter.ToUInt32(uintValue, 0);
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SigningMechanism);

                if (value != null)
                {
                    int uintSize = LowLevelAPI.UnmanagedMemory.SizeOf(typeof(uint));
                    _lowLevelStruct.SigningMechanism = LowLevelAPI.UnmanagedMemory.Allocate(uintSize);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SigningMechanism, BitConverter.GetBytes((uint)value));
                }
            }
        }
        
        /// <summary>
        /// Mechanism to use when digesting the data
        /// </summary>
        public uint? DigestMechanism
        {
            get
            {
                if (_lowLevelStruct.DigestMechanism == IntPtr.Zero)
                    return null;
                
                int uintSize = LowLevelAPI.UnmanagedMemory.SizeOf(typeof(uint));
                byte[] uintValue = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.DigestMechanism, uintSize);
                return BitConverter.ToUInt32(uintValue, 0);
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.DigestMechanism);
                
                if (value != null)
                {
                    int uintSize = LowLevelAPI.UnmanagedMemory.SizeOf(typeof(uint));
                    _lowLevelStruct.DigestMechanism = LowLevelAPI.UnmanagedMemory.Allocate(uintSize);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.DigestMechanism, BitConverter.GetBytes((uint)value));
                }
            }
        }

        /// <summary>
        /// Length of ContentType string
        /// </summary>
        private int _contentTypeLength = 0;

        /// <summary>
        /// String indicating complete MIME Content-type of message to be signed or null if the message is a MIME object
        /// </summary>
        public string ContentType
        {
            get
            {
                if (_lowLevelStruct.ContentType == IntPtr.Zero)
                    return null;

                byte[] bytes = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.ContentType, _contentTypeLength);
                return Encoding.UTF8.GetString(bytes, 0, _contentTypeLength);
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ContentType);
                _contentTypeLength = 0;

                if (value != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    Array.Resize(ref bytes, bytes.Length + 1);
                    bytes[bytes.Length - 1] = 0;

                    _lowLevelStruct.ContentType = LowLevelAPI.UnmanagedMemory.Allocate(bytes.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.ContentType, bytes);
                    _contentTypeLength = bytes.Length - 1;
                }
            }
        }

        /// <summary>
        /// DER-encoded list of CMS Attributes the caller requests to be included in the signed attributes
        /// </summary>
        public byte[] RequestedAttributes
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.RequestedAttributesLen > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.RequestedAttributes, (int)_lowLevelStruct.RequestedAttributesLen);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.RequestedAttributes = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RequestedAttributes, value);
                    _lowLevelStruct.RequestedAttributesLen = (uint)value.Length;
                }
            }
        }

        /// <summary>
        /// DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes
        /// </summary>
        public byte[] RequiredAttributes
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.RequiredAttributesLen > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.RequiredAttributes, (int)_lowLevelStruct.RequiredAttributesLen);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.RequiredAttributes = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RequiredAttributes, value);
                    _lowLevelStruct.RequiredAttributesLen = (uint)value.Length;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkCmsSigParams class.
        /// </summary>
        public CkCmsSigParams()
        {
            _lowLevelStruct.CertificateHandle = CK.CK_INVALID_HANDLE;
            _lowLevelStruct.SigningMechanism = IntPtr.Zero;
            _lowLevelStruct.DigestMechanism = IntPtr.Zero;
            _lowLevelStruct.ContentType = IntPtr.Zero;
            _lowLevelStruct.RequestedAttributes = IntPtr.Zero;
            _lowLevelStruct.RequestedAttributesLen = 0;
            _lowLevelStruct.RequiredAttributes = IntPtr.Zero;
            _lowLevelStruct.RequiredAttributesLen = 0;
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
        {
            return _lowLevelStruct;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SigningMechanism);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.DigestMechanism);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ContentType);
                this._contentTypeLength = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkCmsSigParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
