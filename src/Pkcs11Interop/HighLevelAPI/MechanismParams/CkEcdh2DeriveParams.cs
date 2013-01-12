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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_ECMQV_DERIVE mechanism
    /// </summary>
    public class CkEcdh2DeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_ECDH2_DERIVE_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_ECDH2_DERIVE_PARAMS();
        
        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public uint Kdf
        {
            get
            {
                return _lowLevelStruct.Kdf;
            }
            set
            {
                _lowLevelStruct.Kdf = value;
            }
        }
        
        /// <summary>
        /// Some data shared between the two parties
        /// </summary>
        public byte[] SharedData
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.SharedDataLen > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.SharedData, (int)_lowLevelStruct.SharedDataLen);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SharedData);
                _lowLevelStruct.SharedDataLen = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.SharedData = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SharedData, value);
                    _lowLevelStruct.SharedDataLen = (uint)value.Length;
                }
            }
        }
        
        /// <summary>
        /// Other party's first EC public key value
        /// </summary>
        public byte[] PublicData
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.PublicDataLen > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.PublicData, (int)_lowLevelStruct.PublicDataLen);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.PublicData = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData, value);
                    _lowLevelStruct.PublicDataLen = (uint)value.Length;
                }
            }
        }

        
        /// <summary>
        /// The length in bytes of the second EC private key
        /// </summary>
        public uint PrivateDataLen
        {
            get
            {
                return _lowLevelStruct.PrivateDataLen;
            }
            set
            {
                _lowLevelStruct.PrivateDataLen = value;
            }
        }
        
        /// <summary>
        /// Key handle for second EC private key value
        /// </summary>
        public ObjectHandle PrivateData
        {
            get
            {
                return new ObjectHandle(_lowLevelStruct.PrivateData);
            }
            set
            {
                _lowLevelStruct.PrivateData = value.ObjectId;
            }
        }
        
        /// <summary>
        /// Other party's second EC public key value
        /// </summary>
        public byte[] PublicData2
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.PublicDataLen2 > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.PublicData2, (int)_lowLevelStruct.PublicDataLen2);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData2);
                _lowLevelStruct.PublicDataLen2 = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.PublicData2 = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData2, value);
                    _lowLevelStruct.PublicDataLen2 = (uint)value.Length;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkEcdh2DeriveParams class.
        /// </summary>
        public CkEcdh2DeriveParams()
        {
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.SharedDataLen = 0;
            _lowLevelStruct.SharedData = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;
            _lowLevelStruct.PrivateDataLen = 0;
            _lowLevelStruct.PrivateData = 0;
            _lowLevelStruct.PublicDataLen2 = 0;
            _lowLevelStruct.PublicData2 = IntPtr.Zero;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SharedData);
                _lowLevelStruct.SharedDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData2);
                _lowLevelStruct.PublicDataLen2 = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkEcdh2DeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
