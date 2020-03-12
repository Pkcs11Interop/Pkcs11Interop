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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_CAMELLIA_CBC_ENCRYPT_DATA mechanism
    /// </summary>
    public class CkCamelliaCbcEncryptDataParams : ICkCamelliaCbcEncryptDataParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS _lowLevelStruct = new CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkCamelliaCbcEncryptDataParams class.
        /// </summary>
        /// <param name='iv'>IV value (16 bytes)</param>
        /// <param name='data'>Data to encrypt</param>
        public CkCamelliaCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            _lowLevelStruct.Iv = new byte[16];
            _lowLevelStruct.Data = IntPtr.Zero;
            _lowLevelStruct.Length = 0;

            if (iv == null)
                throw new ArgumentNullException("iv");
            
            if (iv.Length != 16)
                throw new ArgumentOutOfRangeException("iv", "Array has to be 16 bytes long");

            Array.Copy(iv, _lowLevelStruct.Iv, iv.Length);

            if (data != null)
            {
                _lowLevelStruct.Data = UnmanagedMemory.Allocate(data.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Data, data);
                _lowLevelStruct.Length = ConvertUtils.UInt32FromInt32(data.Length);
            }
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Data);
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
