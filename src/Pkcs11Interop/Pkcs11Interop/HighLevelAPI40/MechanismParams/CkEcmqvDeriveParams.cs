/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI40;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_ECMQV_DERIVE mechanism
    /// </summary>
    public class CkEcmqvDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_ECMQV_DERIVE_PARAMS _lowLevelStruct = new CK_ECMQV_DERIVE_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkEcmqvDeriveParams class.
        /// </summary>>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='sharedData'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first EC public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second EC private key</param>
        /// <param name='privateData'>Key handle for second EC private key value</param>
        /// <param name='publicData2'>Other party's second EC public key value</param>
        /// <param name='publicKey'>Handle to the first party's ephemeral public key</param>
        public CkEcmqvDeriveParams(NativeULong kdf, byte[] sharedData, byte[] publicData, NativeULong privateDataLen, ObjectHandle privateData, byte[] publicData2, ObjectHandle publicKey)
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
            _lowLevelStruct.PublicKey = 0;

            _lowLevelStruct.Kdf = kdf;
            
            if (sharedData != null)
            {
                _lowLevelStruct.SharedData = UnmanagedMemory.Allocate(sharedData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SharedData, sharedData);
                _lowLevelStruct.SharedDataLen = NativeLongUtils.ConvertFromInt32(sharedData.Length);
            }
            
            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = NativeLongUtils.ConvertFromInt32(publicData.Length);
            }
            
            _lowLevelStruct.PrivateDataLen = privateDataLen;
            
            if (privateData == null)
                throw new ArgumentNullException("privateData");
            
            _lowLevelStruct.PrivateData = privateData.ObjectId;
            
            if (publicData2 != null)
            {
                _lowLevelStruct.PublicData2 = UnmanagedMemory.Allocate(publicData2.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData2, publicData2);
                _lowLevelStruct.PublicDataLen2 = NativeLongUtils.ConvertFromInt32(publicData2.Length);
            }

            if (publicKey == null)
                throw new ArgumentNullException("publicKey");
            
            _lowLevelStruct.PublicKey = publicKey.ObjectId;
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SharedData);
                _lowLevelStruct.SharedDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData2);
                _lowLevelStruct.PublicDataLen2 = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkEcmqvDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
