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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RSA_PKCS_OAEP mechanism
    /// </summary>
    public class CkRsaPkcsOaepParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS();

        /// <summary>
        /// Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)
        /// </summary>
        public uint HashAlg
        {
            get
            {
                return _lowLevelStruct.HashAlg;
            }
            set
            {
                _lowLevelStruct.HashAlg = value;
            }
        }

        /// <summary>
        /// Mask generation function to use on the encoded block (CKG)
        /// </summary>
        public uint Mgf
        {
            get
            {
                return _lowLevelStruct.Mgf;
            }
            set
            {
                _lowLevelStruct.Mgf = value;
            }
        }

        /// <summary>
        /// Source of the encoding parameter (CKZ)
        /// </summary>
        public uint Source
        {
            get
            {
                return _lowLevelStruct.Source;
            }
            set
            {
                _lowLevelStruct.Source = value;
            }
        }

        /// <summary>
        /// Data used as the input for the encoding parameter source
        /// </summary>
        public byte[] SourceData
        {
            get
            {
                byte[] rv = null;

                if (_lowLevelStruct.SourceDataLen > 0)
                    rv = LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.SourceData, (int)_lowLevelStruct.SourceDataLen);

                return rv;
            }
            set
            {
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SourceData);
                _lowLevelStruct.SourceDataLen = 0;

                if (value != null)
                {
                    _lowLevelStruct.SourceData = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                    LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SourceData, value);
                    _lowLevelStruct.SourceDataLen = (uint)value.Length;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsOaepParams class.
        /// </summary>
        public CkRsaPkcsOaepParams()
        {
            _lowLevelStruct.HashAlg = 0;
            _lowLevelStruct.Mgf = 0;
            _lowLevelStruct.Source = 0;
            _lowLevelStruct.SourceData = IntPtr.Zero;
            _lowLevelStruct.SourceDataLen = 0;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SourceData);
                _lowLevelStruct.SourceDataLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkRsaPkcsOaepParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

