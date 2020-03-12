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
    /// Parameters for the CKM_AES_CCM mechanism
    /// </summary>
    public class CkCcmParams : ICkCcmParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_CCM_PARAMS _lowLevelStruct = new CK_CCM_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkCcmParams class.
        /// </summary>
        /// <param name="dataLen">Length of the data</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="aad">Additional authentication data</param>
        /// <param name="macLen">Length of the MAC (output following cipher text) in bytes</param>
        public CkCcmParams(NativeULong dataLen, byte[] nonce, byte[] aad, NativeULong macLen)
        {
            _lowLevelStruct.DataLen = 0;
            _lowLevelStruct.Nonce = IntPtr.Zero;
            _lowLevelStruct.NonceLen = 0;
            _lowLevelStruct.AAD = IntPtr.Zero;
            _lowLevelStruct.AADLen = 0;
            _lowLevelStruct.MACLen = 0;

            _lowLevelStruct.DataLen = dataLen;

            if (nonce != null)
            {
                _lowLevelStruct.Nonce = UnmanagedMemory.Allocate(nonce.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Nonce, nonce);
                _lowLevelStruct.NonceLen = ConvertUtils.UInt64FromInt32(nonce.Length);
            }

            if (aad != null)
            {
                _lowLevelStruct.AAD = UnmanagedMemory.Allocate(aad.Length);
                UnmanagedMemory.Write(_lowLevelStruct.AAD, aad);
                _lowLevelStruct.AADLen = ConvertUtils.UInt64FromInt32(aad.Length);
            }

            _lowLevelStruct.MACLen = macLen;
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
                _lowLevelStruct.DataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Nonce);
                _lowLevelStruct.NonceLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.AAD);
                _lowLevelStruct.AADLen = 0;
                _lowLevelStruct.MACLen = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkCcmParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
