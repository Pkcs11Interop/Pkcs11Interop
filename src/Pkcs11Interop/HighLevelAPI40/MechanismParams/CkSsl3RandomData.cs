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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Information about the random data of a client and a server in an SSL context
    /// </summary>
    public class CkSsl3RandomData : ICkSsl3RandomData
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_SSL3_RANDOM_DATA _lowLevelStruct = new CK_SSL3_RANDOM_DATA();

        /// <summary>
        /// Initializes a new instance of the CkSsl3RandomData class.
        /// </summary>
        /// <param name='clientRandom'>Client's random data</param>
        /// <param name='serverRandom'>Server's random data</param>
        public CkSsl3RandomData(byte[] clientRandom, byte[] serverRandom)
        {
            _lowLevelStruct.ClientRandom = IntPtr.Zero;
            _lowLevelStruct.ClientRandomLen = 0;
            _lowLevelStruct.ServerRandom = IntPtr.Zero;
            _lowLevelStruct.ServerRandomLen = 0;

            if (clientRandom != null)
            {
                _lowLevelStruct.ClientRandom = UnmanagedMemory.Allocate(clientRandom.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ClientRandom, clientRandom);
                _lowLevelStruct.ClientRandomLen = ConvertUtils.UInt32FromInt32(clientRandom.Length);
            }

            if (serverRandom != null)
            {
                _lowLevelStruct.ServerRandom = UnmanagedMemory.Allocate(serverRandom.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ServerRandom, serverRandom);
                _lowLevelStruct.ServerRandomLen = ConvertUtils.UInt32FromInt32(serverRandom.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.ClientRandom);
                _lowLevelStruct.ClientRandomLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.ServerRandom);
                _lowLevelStruct.ServerRandomLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSsl3RandomData()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
