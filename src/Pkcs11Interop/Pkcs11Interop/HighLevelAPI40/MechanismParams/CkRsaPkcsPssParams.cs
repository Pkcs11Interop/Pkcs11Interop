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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RSA_PKCS_PSS mechanism
    /// </summary>
    public class CkRsaPkcsPssParams : ICkRsaPkcsPssParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RSA_PKCS_PSS_PARAMS _lowLevelStruct = new CK_RSA_PKCS_PSS_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsPssParams class.
        /// </summary>
        /// <param name='hashAlg'>Hash algorithm used in the PSS encoding (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='len'>Length, in bytes, of the salt value used in the PSS encoding</param>
        public CkRsaPkcsPssParams(NativeULong hashAlg, NativeULong mgf, NativeULong len)
        {
            _lowLevelStruct.HashAlg = hashAlg;
            _lowLevelStruct.Mgf = mgf;
            _lowLevelStruct.Len = len;
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
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

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkRsaPkcsPssParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
