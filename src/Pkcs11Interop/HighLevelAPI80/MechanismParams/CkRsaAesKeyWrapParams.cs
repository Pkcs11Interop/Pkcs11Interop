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
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RSA_AES_KEY_WRAP mechanism
    /// </summary>
    public class CkRsaAesKeyWrapParams : ICkRsaAesKeyWrapParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RSA_AES_KEY_WRAP_PARAMS _lowLevelStruct = new CK_RSA_AES_KEY_WRAP_PARAMS();

        /// <summary>
        /// Parameters of the temporary AES key wrapping
        /// </summary>
        private ICkRsaPkcsOaepParams _oaepParams = null;

        /// <summary>
        /// Initializes a new instance of the CkAesCbcEncryptDataParams class.
        /// </summary>
        /// <param name='aesKeyBits'>Length of the temporary AES key in bits</param>
        /// <param name='oaepParams'>Parameters of the temporary AES key wrapping</param>
        public CkRsaAesKeyWrapParams(NativeULong aesKeyBits, ICkRsaPkcsOaepParams oaepParams)
        {
            _lowLevelStruct.AESKeyBits = 0;
            _lowLevelStruct.OAEPParams = IntPtr.Zero;

            if (oaepParams == null)
                throw new ArgumentNullException("oaepParams");

            // Keep the reference to OAEP params so GC will not free it while this object exists
            _oaepParams = oaepParams;

            _lowLevelStruct.AESKeyBits = aesKeyBits;

            _lowLevelStruct.OAEPParams = UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_RSA_PKCS_OAEP_PARAMS)));
            UnmanagedMemory.Write(_lowLevelStruct.OAEPParams, oaepParams.ToMarshalableStructure());
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

                    // Release the reference to OAEP params so GC knows this object doesn't need it anymore
                    _oaepParams = null;
                }

                // Dispose unmanaged objects
                _lowLevelStruct.AESKeyBits = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.OAEPParams);

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkRsaAesKeyWrapParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
