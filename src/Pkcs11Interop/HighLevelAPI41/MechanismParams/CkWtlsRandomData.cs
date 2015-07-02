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
    /// Information about the random data of a client and a server in a WTLS context
    /// </summary>
    public class CkWtlsRandomData : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_WTLS_RANDOM_DATA _lowLevelStruct = new CK_WTLS_RANDOM_DATA();
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsRandomData class.
        /// </summary>
        /// <param name='clientRandom'>Client's random data</param>
        /// <param name='serverRandom'>Server's random data</param>
        public CkWtlsRandomData(byte[] clientRandom, byte[] serverRandom)
        {
            _lowLevelStruct.ClientRandom = IntPtr.Zero;
            _lowLevelStruct.ClientRandomLen = 0;
            _lowLevelStruct.ServerRandom = IntPtr.Zero;
            _lowLevelStruct.ServerRandomLen = 0;
            
            if (clientRandom != null)
            {
                _lowLevelStruct.ClientRandom = UnmanagedMemory.Allocate(clientRandom.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ClientRandom, clientRandom);
                _lowLevelStruct.ClientRandomLen = Convert.ToUInt32(clientRandom.Length);
            }
            
            if (serverRandom != null)
            {
                _lowLevelStruct.ServerRandom = UnmanagedMemory.Allocate(serverRandom.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ServerRandom, serverRandom);
                _lowLevelStruct.ServerRandomLen = Convert.ToUInt32(serverRandom.Length);
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
        ~CkWtlsRandomData()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
