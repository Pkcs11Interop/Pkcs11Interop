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
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_TLS_KDF mechanism
    /// </summary>
    public class CkTlsKdfParams : ICkTlsKdfParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_TLS_KDF_PARAMS _lowLevelStruct = new CK_TLS_KDF_PARAMS();

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private ICkSsl3RandomData _randomInfo = null;

        /// <summary>
        /// Initializes a new instance of the CkTlsKdfParams class.
        /// </summary>
        /// <param name="prfMechanism">Hash mechanism used in the TLS 1.2 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)</param>
        /// <param name="label">Label for this key derivation</param>
        /// <param name="randomInfo">Random data for the key derivation</param>
        /// <param name="contextData">Context data for this key derivation</param>
        public CkTlsKdfParams(NativeULong prfMechanism, byte[] label, ICkSsl3RandomData randomInfo, byte[] contextData)
        {
            _lowLevelStruct.Label = IntPtr.Zero;
            _lowLevelStruct.LabelLength = 0;
            _lowLevelStruct.ContextData = IntPtr.Zero;
            _lowLevelStruct.ContextDataLength = 0;

            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");

            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            _lowLevelStruct.PrfMechanism = prfMechanism;

            if (label != null)
            {
                _lowLevelStruct.Label = UnmanagedMemory.Allocate(label.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Label, label);
                _lowLevelStruct.LabelLength = ConvertUtils.UInt64FromInt32(label.Length);
            }

            _lowLevelStruct.RandomInfo = (CK_SSL3_RANDOM_DATA)_randomInfo.ToMarshalableStructure();

            if (contextData != null)
            {
                _lowLevelStruct.ContextData = UnmanagedMemory.Allocate(contextData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ContextData, contextData);
                _lowLevelStruct.ContextDataLength = ConvertUtils.UInt64FromInt32(contextData.Length);
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

                    // Release the reference to randomInfo so GC knows this object doesn't need it anymore
                    _randomInfo = null;
                }

                // Dispose unmanaged objects
                UnmanagedMemory.Free(ref _lowLevelStruct.Label);
                _lowLevelStruct.LabelLength = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.ContextData);
                _lowLevelStruct.ContextDataLength = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkTlsKdfParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
