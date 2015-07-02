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
    /// Resulting key handles and initialization vectors after performing a DeriveKey method with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public class CkSsl3KeyMatOut : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level structure
        /// </summary>
        internal CK_SSL3_KEY_MAT_OUT _lowLevelStruct = new CK_SSL3_KEY_MAT_OUT();

        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        public ObjectHandle ClientMacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.ClientMacSecret);
            }
        }

        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        public ObjectHandle ServerMacSecret
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.ServerMacSecret);
            }
        }

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        public ObjectHandle ClientKey
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.ClientKey);
            }
        }

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        public ObjectHandle ServerKey
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return new ObjectHandle(_lowLevelStruct.ServerKey);
            }
        }

        /// <summary>
        /// Initialization vector (IV) created for the client
        /// </summary>
        public byte[] IVClient
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_ivLength < 1) ? null : UnmanagedMemory.Read(_lowLevelStruct.IVClient, Convert.ToInt32(_ivLength));
            }
        }

        /// <summary>
        /// Initialization vector (IV) created for the server
        /// </summary>
        public byte[] IVServer
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_ivLength < 1) ? null : UnmanagedMemory.Read(_lowLevelStruct.IVServer, Convert.ToInt32(_ivLength));
            }
        }

        /// <summary>
        /// The length of initialization vectors
        /// </summary>
        private ulong _ivLength = 0;

        /// <summary>
        /// Initializes a new instance of the CkSsl3KeyMatOut class.
        /// </summary>
        /// <param name='ivLength'>Length of initialization vectors or 0 if IVs are not required</param>
        internal CkSsl3KeyMatOut(ulong ivLength)
        {
            _lowLevelStruct.ClientMacSecret = 0;
            _lowLevelStruct.ServerMacSecret = 0;
            _lowLevelStruct.ClientKey = 0;
            _lowLevelStruct.ServerKey = 0;
            _lowLevelStruct.IVClient = IntPtr.Zero;
            _lowLevelStruct.IVServer = IntPtr.Zero;

            _ivLength = ivLength;

            if (_ivLength > 0)
            {
                _lowLevelStruct.IVClient = UnmanagedMemory.Allocate(Convert.ToInt32(_ivLength));
                _lowLevelStruct.IVServer = UnmanagedMemory.Allocate(Convert.ToInt32(_ivLength));
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
                UnmanagedMemory.Free(ref _lowLevelStruct.IVClient);
                UnmanagedMemory.Free(ref _lowLevelStruct.IVServer);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSsl3KeyMatOut()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

