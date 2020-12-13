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
    /// Parameters for the CKM_KIP_DERIVE, CKM_KIP_WRAP and CKM_KIP_MAC mechanisms
    /// </summary>
    public class CkKipParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkKipParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkKipParams _params40 = null;

        /// <summary>
        /// Platform specific CkKipParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkKipParams _params41 = null;

        /// <summary>
        /// Platform specific CkKipParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkKipParams _params80 = null;

        /// <summary>
        /// Platform specific CkKipParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkKipParams _params81 = null;

        /// <summary>
        /// Initializes a new instance of the CkKipParams class.
        /// </summary>
        /// <param name='mechanism'>Underlying cryptographic mechanism (CKM)</param>
        /// <param name='key'>Handle to a key that will contribute to the entropy of the derived key (CKM_KIP_DERIVE) or will be used in the MAC operation (CKM_KIP_MAC)</param>
        /// <param name='seed'>Input seed</param>
        public CkKipParams(ulong? mechanism, ObjectHandle key, byte[] seed)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                uint? uintMechanism = (mechanism == null) ? null : (uint?)Convert.ToUInt32(mechanism.Value);

                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkKipParams(uintMechanism, key.ObjectHandle40, seed);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkKipParams(uintMechanism, key.ObjectHandle41, seed);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkKipParams(mechanism, key.ObjectHandle80, seed);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkKipParams(mechanism, key.ObjectHandle81, seed);
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
        ~CkKipParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
