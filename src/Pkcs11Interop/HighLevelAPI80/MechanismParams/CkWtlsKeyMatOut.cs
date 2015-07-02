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
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
{
    /// <summary>
    /// Resulting key handles and initialization vectors after performing a DeriveKey method with the CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE or with the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public class CkWtlsKeyMatOut : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level structure
        /// </summary>
        internal CK_WTLS_KEY_MAT_OUT _lowLevelStruct = new CK_WTLS_KEY_MAT_OUT();
        
        /// <summary>
        /// Key handle for the resulting MAC secret key
        /// </summary>
        public ObjectHandle MacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.MacSecret);
            }
        }

        /// <summary>
        /// Key handle for the resulting Secret key
        /// </summary>
        public ObjectHandle Key
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.Key);
            }
        }

        /// <summary>
        /// Initialization vector (IV)
        /// </summary>
        public byte[] IV
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_ivLength < 1) ? null : UnmanagedMemory.Read(_lowLevelStruct.IV, Convert.ToInt32(_ivLength));
            }
        }
        
        /// <summary>
        /// The length of initialization vector
        /// </summary>
        private ulong _ivLength = 0;
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ivLength'>Length of initialization vector or 0 if IV is not required</param>
        internal CkWtlsKeyMatOut(ulong ivLength)
        {
            _lowLevelStruct.MacSecret = 0;
            _lowLevelStruct.Key = 0;
            _lowLevelStruct.IV = IntPtr.Zero;

            _ivLength = ivLength;
            
            if (_ivLength > 0)
            {
                _lowLevelStruct.IV = UnmanagedMemory.Allocate(Convert.ToInt32(_ivLength));
            }
        }
        
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
                UnmanagedMemory.Free(ref _lowLevelStruct.IV);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkWtlsKeyMatOut()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

