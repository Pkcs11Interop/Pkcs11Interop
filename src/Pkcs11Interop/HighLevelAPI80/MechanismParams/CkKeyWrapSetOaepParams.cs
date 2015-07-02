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
    /// Parameters for the CKM_KEY_WRAP_SET_OAEP mechanism
    /// </summary>
    public class CkKeyWrapSetOaepParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_KEY_WRAP_SET_OAEP_PARAMS _lowLevelStruct = new CK_KEY_WRAP_SET_OAEP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkKeyWrapSetOaepParams class.
        /// </summary>
        /// <param name='bc'>Block contents byte</param>
        /// <param name='x'>Concatenation of hash of plaintext data (if present) and extra data (if present)</param>
        public CkKeyWrapSetOaepParams(byte bc, byte[] x)
        {
            _lowLevelStruct.BC = 0;
            _lowLevelStruct.X = IntPtr.Zero;
            _lowLevelStruct.XLen = 0;

            _lowLevelStruct.BC = bc;

            if (x != null)
            {
                _lowLevelStruct.X = UnmanagedMemory.Allocate(x.Length);
                UnmanagedMemory.Write(_lowLevelStruct.X, x);
                _lowLevelStruct.XLen = Convert.ToUInt64(x.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.X);
                _lowLevelStruct.XLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkKeyWrapSetOaepParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
