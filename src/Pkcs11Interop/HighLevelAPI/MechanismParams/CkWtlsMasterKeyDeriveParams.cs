/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.LowLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_WTLS_MASTER_KEY_DERIVE and CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC mechanisms
    /// </summary>
    public class CkWtlsMasterKeyDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS();
        
        /// <summary>
        /// WTLS protocol version information
        /// </summary>
        public CkVersion Version
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
        private CkWtlsRandomData _randomInfo = null;
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsMasterKeyDeriveParams class.
        /// </summary>
        /// <param name='digestMechanism'>Digest mechanism to be used (CKM)</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <param name='dh'>Set to false for CKM_WTLS_MASTER_KEY_DERIVE mechanism and to true for CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC mechanism</param>
        public CkWtlsMasterKeyDeriveParams(uint digestMechanism, CkWtlsRandomData randomInfo, bool dh)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            _lowLevelStruct.DigestMechanism = digestMechanism;
            _lowLevelStruct.RandomInfo = (CK_WTLS_RANDOM_DATA)_randomInfo.ToLowLevelParams();
            _lowLevelStruct.Version = (dh) ? IntPtr.Zero : UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_VERSION)));
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Version);
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkWtlsMasterKeyDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
