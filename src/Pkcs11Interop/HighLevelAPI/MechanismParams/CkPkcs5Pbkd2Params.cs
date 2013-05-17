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
        private LowLevelAPI.MechanismParams.CK_PKCS5_PBKD2_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_PKCS5_PBKD2_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkPkcs5Pbkd2Params class.
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        public CkPkcs5Pbkd2Params(uint saltSource, byte[] saltSourceData, uint iterations, uint prf, byte[] prfData, byte[] password)
        {
            _lowLevelStruct.SaltSource = 0;
            _lowLevelStruct.SaltSourceData = IntPtr.Zero;
            _lowLevelStruct.SaltSourceDataLen = 0;
            _lowLevelStruct.Iterations = 0;
            _lowLevelStruct.Prf = 0;
            _lowLevelStruct.PrfData = IntPtr.Zero;
            _lowLevelStruct.PrfDataLen = 0;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PasswordLen = 0;

            _lowLevelStruct.SaltSource = saltSource;

            if (saltSourceData != null)
            {
                _lowLevelStruct.SaltSourceData = LowLevelAPI.UnmanagedMemory.Allocate(saltSourceData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SaltSourceData, saltSourceData);
                _lowLevelStruct.SaltSourceDataLen = (uint)saltSourceData.Length;
            }

            _lowLevelStruct.Iterations = iterations;

            _lowLevelStruct.Prf = prf;

            if (prfData != null)
            {
                _lowLevelStruct.PrfData = LowLevelAPI.UnmanagedMemory.Allocate(prfData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PrfData, prfData);
                _lowLevelStruct.PrfDataLen = (uint)prfData.Length;
            }

            if (password != null)
            {
                _lowLevelStruct.Password = LowLevelAPI.UnmanagedMemory.Allocate(password.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = (uint)password.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SaltSourceData);
                _lowLevelStruct.SaltSourceDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PrfData);
                _lowLevelStruct.PrfDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;

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
