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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Information about the random data of a client and a server in an SSL context
    /// </summary>
    public class CkSsl3RandomData : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_SSL3_RANDOM_DATA _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_SSL3_RANDOM_DATA();

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
                _lowLevelStruct.ClientRandom = LowLevelAPI.UnmanagedMemory.Allocate(clientRandom.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.ClientRandom, clientRandom);
                _lowLevelStruct.ClientRandomLen = (uint)clientRandom.Length;
            }

            if (serverRandom != null)
            {
                _lowLevelStruct.ServerRandom = LowLevelAPI.UnmanagedMemory.Allocate(serverRandom.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.ServerRandom, serverRandom);
                _lowLevelStruct.ServerRandomLen = (uint)serverRandom.Length;
            }
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ClientRandom);
                _lowLevelStruct.ClientRandomLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ServerRandom);
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
