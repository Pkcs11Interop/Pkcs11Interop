/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI80;
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;
using NativeULong = System.UInt64;

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

                return (_ivLength < 1) ? null : UnmanagedMemory.Read(_lowLevelStruct.IVClient, NativeLongUtils.ConvertToInt32(_ivLength));
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

                return (_ivLength < 1) ? null : UnmanagedMemory.Read(_lowLevelStruct.IVServer, NativeLongUtils.ConvertToInt32(_ivLength));
            }
        }

        /// <summary>
        /// The length of initialization vectors
        /// </summary>
        private NativeULong _ivLength = 0;

        /// <summary>
        /// Initializes a new instance of the CkSsl3KeyMatOut class.
        /// </summary>
        /// <param name='ivLength'>Length of initialization vectors or 0 if IVs are not required</param>
        internal CkSsl3KeyMatOut(NativeULong ivLength)
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
                _lowLevelStruct.IVClient = UnmanagedMemory.Allocate(NativeLongUtils.ConvertToInt32(_ivLength));
                _lowLevelStruct.IVServer = UnmanagedMemory.Allocate(NativeLongUtils.ConvertToInt32(_ivLength));
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

