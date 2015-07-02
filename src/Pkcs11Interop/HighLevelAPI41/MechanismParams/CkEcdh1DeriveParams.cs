/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_ECDH1_DERIVE and CKM_ECDH1_COFACTOR_DERIVE key derivation mechanisms
    /// </summary>
    public class CkEcdh1DeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_ECDH1_DERIVE_PARAMS _lowLevelStruct = new CK_ECDH1_DERIVE_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkEcdh1DeriveParams class.
        /// </summary>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='sharedData'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's EC public key value</param>
        public CkEcdh1DeriveParams(uint kdf, byte[] sharedData, byte[] publicData)
        {
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.SharedDataLen = 0;
            _lowLevelStruct.SharedData = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;

            _lowLevelStruct.Kdf = kdf;

            if (sharedData != null)
            {
                _lowLevelStruct.SharedData = UnmanagedMemory.Allocate(sharedData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SharedData, sharedData);
                _lowLevelStruct.SharedDataLen = Convert.ToUInt32(sharedData.Length);
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = Convert.ToUInt32(publicData.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SharedData);
                _lowLevelStruct.SharedDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkEcdh1DeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
