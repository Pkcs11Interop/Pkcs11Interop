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
        private HighLevelAPI40.MechanismParams.CkWtlsKeyMatOut _params40 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatOut
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkWtlsKeyMatOut _params41 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatOut
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkWtlsKeyMatOut _params80 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatOut
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkWtlsKeyMatOut _params81 = null;
        
        /// <summary>
        /// Key handle for the resulting MAC secret key
        /// </summary>
        public ObjectHandle MacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? new ObjectHandle(_params40.MacSecret) : new ObjectHandle(_params41.MacSecret);
                else
                    return (Platform.StructPackingSize == 0) ? new ObjectHandle(_params80.MacSecret) : new ObjectHandle(_params81.MacSecret);
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

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? new ObjectHandle(_params40.Key) : new ObjectHandle(_params41.Key);
                else
                    return (Platform.StructPackingSize == 0) ? new ObjectHandle(_params80.Key) : new ObjectHandle(_params81.Key);
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

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _params40.IV : _params41.IV;
                else
                    return (Platform.StructPackingSize == 0) ? _params80.IV : _params81.IV;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI40.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params40 = ckWtlsKeyMatOut;
        }

        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI41.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params41 = ckWtlsKeyMatOut;
        }

        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI80.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params80 = ckWtlsKeyMatOut;
        }

        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatOut class.
        /// </summary>
        /// <param name='ckWtlsKeyMatOut'>Platform specific CkWtlsKeyMatOut</param>
        internal CkWtlsKeyMatOut(HighLevelAPI81.MechanismParams.CkWtlsKeyMatOut ckWtlsKeyMatOut)
        {
            if (ckWtlsKeyMatOut == null)
                throw new ArgumentNullException("ckWtlsKeyMatOut");

            _params81 = ckWtlsKeyMatOut;
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
        ~CkWtlsKeyMatOut()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

