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
        /// Platform specific CkWtlsKeyMatOut
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkWtlsKeyMatOut _params4 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatOut
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkWtlsKeyMatOut _params8 = null;
        
        /// <summary>
        /// Key handle for the resulting MAC secret key
        /// </summary>
        public ObjectHandle MacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.MacSecret) : new ObjectHandle(_params8.MacSecret);
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

                return (UnmanagedLong.Size == 4) ? new ObjectHandle(_params4.Key) : new ObjectHandle(_params8.Key);
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

                return (UnmanagedLong.Size == 4) ? _params4.IV : _params8.IV;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI4.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params4 = ckWtlsKeyMatOut;
        }

        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI8.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params8 = ckWtlsKeyMatOut;
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
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects

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

