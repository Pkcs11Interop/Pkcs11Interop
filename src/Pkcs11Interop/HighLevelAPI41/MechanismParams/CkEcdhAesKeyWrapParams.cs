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
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_ECDH_AES_KEY_WRAP mechanism
    /// </summary>
    public class CkEcdhAesKeyWrapParams : ICkEcdhAesKeyWrapParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_ECDH_AES_KEY_WRAP_PARAMS _lowLevelStruct = new CK_ECDH_AES_KEY_WRAP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkEcdhAesKeyWrapParams class.
        /// </summary>
        /// <param name="aesKeyBits">Length of the temporary AES key in bits</param>
        /// <param name="kdf">Key derivation function used on the shared secret value to generate AES key (CKD)</param>
        /// <param name="sharedData">Data shared between the two parties</param>
        public CkEcdhAesKeyWrapParams(NativeULong aesKeyBits, NativeULong kdf, byte[] sharedData)
        {
            _lowLevelStruct.AESKeyBits = 0;
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.SharedData = IntPtr.Zero;
            _lowLevelStruct.SharedDataLen = 0;

            _lowLevelStruct.AESKeyBits = aesKeyBits;
            _lowLevelStruct.Kdf = kdf;

            if (sharedData != null)
            {
                _lowLevelStruct.SharedData = UnmanagedMemory.Allocate(sharedData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SharedData, sharedData);
                _lowLevelStruct.SharedDataLen = ConvertUtils.UInt32FromInt32(sharedData.Length);
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
                _lowLevelStruct.AESKeyBits = 0;
                _lowLevelStruct.Kdf = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.SharedData);
                _lowLevelStruct.SharedDataLen = 0;
                
                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkEcdhAesKeyWrapParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
