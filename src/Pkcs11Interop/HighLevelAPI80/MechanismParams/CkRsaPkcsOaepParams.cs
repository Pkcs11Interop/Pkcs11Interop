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
    /// Parameters for the CKM_RSA_PKCS_OAEP mechanism
    /// </summary>
    public class CkRsaPkcsOaepParams : ICkRsaPkcsOaepParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RSA_PKCS_OAEP_PARAMS _lowLevelStruct = new CK_RSA_PKCS_OAEP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsOaepParams class.
        /// </summary>
        /// <param name='hashAlg'>Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='source'>Source of the encoding parameter (CKZ)</param>
        /// <param name='sourceData'>Data used as the input for the encoding parameter source</param>
        public CkRsaPkcsOaepParams(NativeULong hashAlg, NativeULong mgf, NativeULong source, byte[] sourceData)
        {
            _lowLevelStruct.HashAlg = 0;
            _lowLevelStruct.Mgf = 0;
            _lowLevelStruct.Source = 0;
            _lowLevelStruct.SourceData = IntPtr.Zero;
            _lowLevelStruct.SourceDataLen = 0;

            _lowLevelStruct.HashAlg = hashAlg;
            _lowLevelStruct.Mgf = mgf;
            _lowLevelStruct.Source = source;

            if (sourceData != null)
            {
                _lowLevelStruct.SourceData = UnmanagedMemory.Allocate(sourceData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SourceData, sourceData);
                _lowLevelStruct.SourceDataLen = ConvertUtils.UInt64FromInt32(sourceData.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SourceData);
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

