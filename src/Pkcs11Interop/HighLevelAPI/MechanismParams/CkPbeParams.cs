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
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using System;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism
    /// </summary>
    public class CkPbeParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_PBE_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_PBE_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkPbeParams class.
        /// </summary>
        /// <param name='initVector'>8-byte initialization vector (IV), if an IV is required</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <param name='salt'>Salt to be used in the PBE key generation</param>
        /// <param name='iteration'>Number of iterations required for the generation</param>
        public CkPbeParams(byte[] initVector, byte[] password, byte[] salt, uint iteration)
        {
            _lowLevelStruct.InitVector = IntPtr.Zero;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PasswordLen = 0;
            _lowLevelStruct.Salt = IntPtr.Zero;
            _lowLevelStruct.SaltLen = 0;
            _lowLevelStruct.Iteration = 0;

            if (initVector != null)
            {
                if (initVector.Length != 8)
                    throw new ArgumentOutOfRangeException("initVector", "Array has to be 8 bytes long");

                _lowLevelStruct.InitVector = LowLevelAPI.UnmanagedMemory.Allocate(initVector.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.InitVector, initVector);
            }

            if (password != null)
            {
                _lowLevelStruct.Password = LowLevelAPI.UnmanagedMemory.Allocate(password.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = (uint)password.Length;
            }

            if (salt != null)
            {
                _lowLevelStruct.Salt = LowLevelAPI.UnmanagedMemory.Allocate(salt.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Salt, salt);
                _lowLevelStruct.SaltLen = (uint)salt.Length;
            }

            _lowLevelStruct.Iteration = iteration;
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.InitVector);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Salt);
                _lowLevelStruct.SaltLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkPbeParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
