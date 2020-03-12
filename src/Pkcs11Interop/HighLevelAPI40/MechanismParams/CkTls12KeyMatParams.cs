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
    /// Parameters for the CKM_TLS12_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public class CkTls12KeyMatParams : ICkTls12KeyMatParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_TLS12_KEY_MAT_PARAMS _lowLevelStruct = new CK_TLS12_KEY_MAT_PARAMS();

        /// <summary>
        /// Flag indicating whether object with returned key material has left this instance
        /// </summary>
        private bool _returnedKeyMaterialLeftInstance = false;

        /// <summary>
        /// Resulting key handles and initialization vectors
        /// </summary>
        private CkSsl3KeyMatOut _returnedKeyMaterial = null;

        /// <summary>
        /// Resulting key handles and initialization vectors
        /// </summary>
        public ICkSsl3KeyMatOut ReturnedKeyMaterial
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                // Abrakadabra :)
                UnmanagedMemory.Read(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);

                // Since now it is the caller's responsibility to dispose returned key material
                _returnedKeyMaterialLeftInstance = true;

                return _returnedKeyMaterial;
            }
        }

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private ICkSsl3RandomData _randomInfo = null;

        /// <summary>
        /// Initializes a new instance of the CkTls12KeyMatParams class.
        /// </summary>
        /// <param name="macSizeInBits">The length (in bits) of the MACing keys agreed upon during the protocol handshake phase</param>
        /// <param name="keySizeInBits">The length (in bits) of the secret keys agreed upon during the protocol handshake phase</param>
        /// <param name="ivSizeInBits">The length (in bits) of the IV agreed upon during the protocol handshake phase</param>
        /// <param name="isExport">Flag which must be set to false because export cipher suites must not be used in TLS 1.1 and later</param>
        /// <param name="randomInfo">Client's and server's random data information</param>
        /// <param name="prfHashMechanism">Base hash used in the underlying TLS1.2 PRF operation used to derive the master key (CKM)</param>
        public CkTls12KeyMatParams(NativeULong macSizeInBits, NativeULong keySizeInBits, NativeULong ivSizeInBits, bool isExport, ICkSsl3RandomData randomInfo, NativeULong prfHashMechanism)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");

            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            // GC will not free ReturnedKeyMaterial while this object exists
            _returnedKeyMaterial = new CkSsl3KeyMatOut(ivSizeInBits / 8);

            _lowLevelStruct.MacSizeInBits = macSizeInBits;
            _lowLevelStruct.KeySizeInBits = keySizeInBits;
            _lowLevelStruct.IVSizeInBits = ivSizeInBits;
            _lowLevelStruct.IsExport = isExport;
            _lowLevelStruct.RandomInfo = (CK_SSL3_RANDOM_DATA)_randomInfo.ToMarshalableStructure();

            // Abrakadabra :)
            _lowLevelStruct.ReturnedKeyMaterial = UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_SSL3_KEY_MAT_OUT)));
            UnmanagedMemory.Write(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);

            _lowLevelStruct.PrfHashMechanism = prfHashMechanism;
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
                    if (_returnedKeyMaterialLeftInstance == false)
                    {
                        if (_returnedKeyMaterial != null)
                        {
                            _returnedKeyMaterial.Dispose();
                            _returnedKeyMaterial = null;
                        }
                    }

                    // Release the reference to randomInfo so GC knows this object doesn't need it anymore
                    _randomInfo = null;
                }

                // Dispose unmanaged objects
                UnmanagedMemory.Free(ref _lowLevelStruct.ReturnedKeyMaterial);

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkTls12KeyMatParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
