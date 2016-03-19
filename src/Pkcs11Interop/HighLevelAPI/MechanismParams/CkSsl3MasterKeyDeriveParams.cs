/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SSL3_MASTER_KEY_DERIVE and CKM_SSL3_MASTER_KEY_DERIVE_DH mechanisms
    /// </summary>
    public class CkSsl3MasterKeyDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkSsl3MasterKeyDeriveParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkSsl3MasterKeyDeriveParams _params40 = null;

        /// <summary>
        /// Platform specific CkSsl3MasterKeyDeriveParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkSsl3MasterKeyDeriveParams _params41 = null;

        /// <summary>
        /// Platform specific CkSsl3MasterKeyDeriveParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkSsl3MasterKeyDeriveParams _params80 = null;

        /// <summary>
        /// Platform specific CkSsl3MasterKeyDeriveParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkSsl3MasterKeyDeriveParams _params81 = null;

        /// <summary>
        /// SSL protocol version information
        /// </summary>
        public CkVersion Version
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (Platform.UnmanagedLongSize == 4)
                {
                    if (Platform.StructPackingSize == 0)
                        return (_params40.Version == null) ? null : new CkVersion(_params40.Version);
                    else
                        return (_params41.Version == null) ? null : new CkVersion(_params41.Version);
                }
                else
                {
                    if (Platform.StructPackingSize == 0)
                        return (_params80.Version == null) ? null : new CkVersion(_params80.Version);
                    else
                        return (_params81.Version == null) ? null : new CkVersion(_params81.Version);
                }
            }
        }

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private CkSsl3RandomData _randomInfo = null;

        /// <summary>
        /// Initializes a new instance of the CkSsl3MasterKeyDeriveParams class.
        /// </summary>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <param name='dh'>Set to false for CKM_SSL3_MASTER_KEY_DERIVE mechanism and to true for CKM_SSL3_MASTER_KEY_DERIVE_DH mechanism</param>
        public CkSsl3MasterKeyDeriveParams(CkSsl3RandomData randomInfo, bool dh)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkSsl3MasterKeyDeriveParams(_randomInfo._params40, dh);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkSsl3MasterKeyDeriveParams(_randomInfo._params41, dh);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkSsl3MasterKeyDeriveParams(_randomInfo._params80, dh);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkSsl3MasterKeyDeriveParams(_randomInfo._params81, dh);
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

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _params40.ToMarshalableStructure() : _params41.ToMarshalableStructure();
            else
                return (Platform.StructPackingSize == 0) ? _params80.ToMarshalableStructure() : _params81.ToMarshalableStructure();
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
                    if (_params40 != null)
                    {
                        _params40.Dispose();
                        _params40 = null;
                    }

                    if (_params41 != null)
                    {
                        _params41.Dispose();
                        _params41 = null;
                    }

                    if (_params80 != null)
                    {
                        _params80.Dispose();
                        _params80 = null;
                    }

                    if (_params81 != null)
                    {
                        _params81.Dispose();
                        _params81 = null;
                    }
                }
                
                // Dispose unmanaged objects

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
