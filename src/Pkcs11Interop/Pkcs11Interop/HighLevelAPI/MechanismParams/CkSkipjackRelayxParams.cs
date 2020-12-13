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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SKIPJACK_RELAYX mechanism
    /// </summary>
    public class CkSkipjackRelayxParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkSkipjackRelayxParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkSkipjackRelayxParams _params40 = null;

        /// <summary>
        /// Platform specific CkSkipjackRelayxParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkSkipjackRelayxParams _params41 = null;

        /// <summary>
        /// Platform specific CkSkipjackRelayxParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkSkipjackRelayxParams _params80 = null;

        /// <summary>
        /// Platform specific CkSkipjackRelayxParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkSkipjackRelayxParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkSkipjackRelayxParams class.
        /// </summary>
        /// <param name='oldWrappedX'>Old wrapper key</param>
        /// <param name='oldPassword'>Old user-supplied password</param>
        /// <param name='oldPublicData'>Old key exchange public key value</param>
        /// <param name='oldRandomA'>Old Ra data</param>
        /// <param name='newPassword'>New user-supplied password</param>
        /// <param name='newPublicData'>New key exchange public key value</param>
        /// <param name='newRandomA'>New Ra data</param>
        public CkSkipjackRelayxParams(byte[] oldWrappedX, byte[] oldPassword, byte[] oldPublicData, byte[] oldRandomA, byte[] newPassword, byte[] newPublicData, byte[] newRandomA)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkSkipjackRelayxParams(oldWrappedX, oldPassword, oldPublicData, oldRandomA, newPassword, newPublicData, newRandomA);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkSkipjackRelayxParams(oldWrappedX, oldPassword, oldPublicData, oldRandomA, newPassword, newPublicData, newRandomA);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkSkipjackRelayxParams(oldWrappedX, oldPassword, oldPublicData, oldRandomA, newPassword, newPublicData, newRandomA);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkSkipjackRelayxParams(oldWrappedX, oldPassword, oldPublicData, oldRandomA, newPassword, newPublicData, newRandomA);
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
        ~CkSkipjackRelayxParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
