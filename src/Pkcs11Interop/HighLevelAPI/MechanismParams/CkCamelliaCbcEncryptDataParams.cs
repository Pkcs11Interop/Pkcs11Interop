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
    /// Parameters for the CKM_CAMELLIA_CBC_ENCRYPT_DATA mechanism
    /// </summary>
    public class CkCamelliaCbcEncryptDataParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS();
        
        /// <summary>
        /// IV value (16 bytes)
        /// </summary>
        public byte[] Iv
        {
            get
            {
                return _lowLevelStruct.Iv;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Iv");
                
                if (value.Length != 16)
                    throw new ArgumentOutOfRangeException("Iv", "Array has to be 16 bytes long");
                
                _lowLevelStruct.Iv = value;
            }
        }
        
        /// <summary>
        /// Data to encrypt
        /// </summary>
        public byte[] Data
        {
            get
            {
                byte[] rv = null;
                
                if (_lowLevelStruct.Length > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.Data, (int)_lowLevelStruct.Length);
                
                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Data);
                _lowLevelStruct.Length = 0;
                
                if (value != null)
                {
                    _lowLevelStruct.Data = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Data, value);
                    _lowLevelStruct.Length = (uint)value.Length;
                }
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkCamelliaCbcEncryptDataParams class.
        /// </summary>
        public CkCamelliaCbcEncryptDataParams()
        {
            _lowLevelStruct.Iv = new byte[16];
            _lowLevelStruct.Data = IntPtr.Zero;
            _lowLevelStruct.Length = 0;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Data);
                _lowLevelStruct.Length = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkCamelliaCbcEncryptDataParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
