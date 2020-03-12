/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism
    /// </summary>
    public class CkPbeParams : ICkPbeParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_PBE_PARAMS _lowLevelStruct = new CK_PBE_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkPbeParams class.
        /// </summary>
        /// <param name='initVector'>8-byte initialization vector (IV), if an IV is required</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <param name='salt'>Salt to be used in the PBE key generation</param>
        /// <param name='iteration'>Number of iterations required for the generation</param>
        public CkPbeParams(byte[] initVector, byte[] password, byte[] salt, NativeULong iteration)
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

                _lowLevelStruct.InitVector = UnmanagedMemory.Allocate(initVector.Length);
                UnmanagedMemory.Write(_lowLevelStruct.InitVector, initVector);
            }

            if (password != null)
            {
                _lowLevelStruct.Password = UnmanagedMemory.Allocate(password.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = ConvertUtils.UInt32FromInt32(password.Length);
            }

            if (salt != null)
            {
                _lowLevelStruct.Salt = UnmanagedMemory.Allocate(salt.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Salt, salt);
                _lowLevelStruct.SaltLen = ConvertUtils.UInt32FromInt32(salt.Length);
            }

            _lowLevelStruct.Iteration = iteration;
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
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
                UnmanagedMemory.Free(ref _lowLevelStruct.InitVector);
                UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Salt);
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
