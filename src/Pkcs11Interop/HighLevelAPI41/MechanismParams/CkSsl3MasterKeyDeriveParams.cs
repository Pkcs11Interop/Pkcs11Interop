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
using Net.Pkcs11Interop.LowLevelAPI41;
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SSL3_MASTER_KEY_DERIVE and CKM_SSL3_MASTER_KEY_DERIVE_DH mechanisms
    /// </summary>
    public class CkSsl3MasterKeyDeriveParams : ICkSsl3MasterKeyDeriveParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_SSL3_MASTER_KEY_DERIVE_PARAMS _lowLevelStruct = new CK_SSL3_MASTER_KEY_DERIVE_PARAMS();

        /// <summary>
        /// SSL protocol version information
        /// </summary>
        public ICkVersion Version
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                CkVersion version = null;

                if (_lowLevelStruct.Version != IntPtr.Zero)
                {
                    CK_VERSION ckVersion = new CK_VERSION();
                    UnmanagedMemory.Read(_lowLevelStruct.Version, ckVersion);
                    version = new CkVersion(ckVersion.Major[0], ckVersion.Minor[0]);
                }

                return version;
            }
        }

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private ICkSsl3RandomData _randomInfo = null;

        /// <summary>
        /// Initializes a new instance of the CkSsl3MasterKeyDeriveParams class.
        /// </summary>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <param name='dh'>Set to false for CKM_SSL3_MASTER_KEY_DERIVE mechanism and to true for CKM_SSL3_MASTER_KEY_DERIVE_DH mechanism</param>
        public CkSsl3MasterKeyDeriveParams(ICkSsl3RandomData randomInfo, bool dh)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            _lowLevelStruct.RandomInfo = (CK_SSL3_RANDOM_DATA)_randomInfo.ToMarshalableStructure();
            _lowLevelStruct.Version = (dh) ? IntPtr.Zero : UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_VERSION)));
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Version);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSsl3MasterKeyDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
