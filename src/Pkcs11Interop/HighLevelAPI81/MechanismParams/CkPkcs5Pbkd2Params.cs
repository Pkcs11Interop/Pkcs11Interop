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
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    public class CkPkcs5Pbkd2Params : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_PKCS5_PBKD2_PARAMS _lowLevelStruct = new CK_PKCS5_PBKD2_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkPkcs5Pbkd2Params class.
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        public CkPkcs5Pbkd2Params(ulong saltSource, byte[] saltSourceData, ulong iterations, ulong prf, byte[] prfData, byte[] password)
        {
            _lowLevelStruct.SaltSource = 0;
            _lowLevelStruct.SaltSourceData = IntPtr.Zero;
            _lowLevelStruct.SaltSourceDataLen = 0;
            _lowLevelStruct.Iterations = 0;
            _lowLevelStruct.Prf = 0;
            _lowLevelStruct.PrfData = IntPtr.Zero;
            _lowLevelStruct.PrfDataLen = 0;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PasswordLen = IntPtr.Zero;

            _lowLevelStruct.SaltSource = saltSource;

            if (saltSourceData != null)
            {
                _lowLevelStruct.SaltSourceData = UnmanagedMemory.Allocate(saltSourceData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SaltSourceData, saltSourceData);
                _lowLevelStruct.SaltSourceDataLen = Convert.ToUInt64(saltSourceData.Length);
            }

            _lowLevelStruct.Iterations = iterations;

            _lowLevelStruct.Prf = prf;

            if (prfData != null)
            {
                _lowLevelStruct.PrfData = UnmanagedMemory.Allocate(prfData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PrfData, prfData);
                _lowLevelStruct.PrfDataLen = Convert.ToUInt64(prfData.Length);
            }

            if (password != null)
            {
                _lowLevelStruct.Password = UnmanagedMemory.Allocate(password.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Password, password);

                ulong passwordLength = Convert.ToUInt32(password.Length);
                _lowLevelStruct.PasswordLen = UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(ulong)));
                UnmanagedMemory.Write(_lowLevelStruct.PasswordLen, BitConverter.GetBytes(passwordLength));
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SaltSourceData);
                _lowLevelStruct.SaltSourceDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PrfData);
                _lowLevelStruct.PrfDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                UnmanagedMemory.Free(ref _lowLevelStruct.PasswordLen);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkPkcs5Pbkd2Params()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
