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
    /// Parameters for the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism
    /// </summary>
    public class CkPbeParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_PBE_PARAMS _lowLevelStruct = new CK_PBE_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkPbeParams class.
        /// </summary>
        /// <param name='initVector'>8-byte initialization vector (IV), if an IV is required</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <param name='salt'>Salt to be used in the PBE key generation</param>
        /// <param name='iteration'>Number of iterations required for the generation</param>
        public CkPbeParams(byte[] initVector, byte[] password, byte[] salt, uint iteration)
        {
            _lowLevelStruct.InitVector = IntPtr.Zero;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PasswordLen = 0;
            _lowLevelStruct.Salt = IntPtr.Zero;
            _lowLevelStruct.SaltLen = 0;
            _lowLevelStruct.Iteration = 0;

            if (initVector != null)
            {
                if (initVector.Length != 8)
                    throw new ArgumentOutOfRangeException("initVector", "Array has to be 8 bytes long");

                _lowLevelStruct.InitVector = UnmanagedMemory.Allocate(initVector.Length);
                UnmanagedMemory.Write(_lowLevelStruct.InitVector, initVector);
            }

            if (password != null)
            {
                _lowLevelStruct.Password = UnmanagedMemory.Allocate(password.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = Convert.ToUInt32(password.Length);
            }

            if (salt != null)
            {
                _lowLevelStruct.Salt = UnmanagedMemory.Allocate(salt.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Salt, salt);
                _lowLevelStruct.SaltLen = Convert.ToUInt32(salt.Length);
            }

            _lowLevelStruct.Iteration = iteration;
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
                UnmanagedMemory.Free(ref _lowLevelStruct.InitVector);
                UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Salt);
                _lowLevelStruct.SaltLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkPbeParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
