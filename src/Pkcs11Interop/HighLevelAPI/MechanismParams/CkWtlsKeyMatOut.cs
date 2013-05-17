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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
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
        internal LowLevelAPI.MechanismParams.CK_WTLS_KEY_MAT_OUT _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_WTLS_KEY_MAT_OUT();
        
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

                return (_ivLength < 1) ? null : LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.IV, (int)_ivLength);
            }
        }
        
        /// <summary>
        /// The length of initialization vector
        /// </summary>
        private uint _ivLength = 0;
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ivLength'>Length of initialization vector or 0 if IV is not required</param>
        internal CkWtlsKeyMatOut(uint ivLength)
        {
            _lowLevelStruct.MacSecret = 0;
            _lowLevelStruct.Key = 0;
            _lowLevelStruct.IV = IntPtr.Zero;

            _ivLength = ivLength;
            
            if (_ivLength > 0)
            {
                _lowLevelStruct.IV = LowLevelAPI.UnmanagedMemory.Allocate((int)_ivLength);
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.IV);

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

