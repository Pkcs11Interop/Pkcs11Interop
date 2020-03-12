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
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    public class CkPkcs5Pbkd2Params2 : ICkPkcs5Pbkd2Params2
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_PKCS5_PBKD2_PARAMS2 _lowLevelStruct = new CK_PKCS5_PBKD2_PARAMS2();

        /// <summary>
        /// Initializes a new instance of the CkPkcs5Pbkd2Params2 class.
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        public CkPkcs5Pbkd2Params2(NativeULong saltSource, byte[] saltSourceData, NativeULong iterations, NativeULong prf, byte[] prfData, byte[] password)
        {
            _lowLevelStruct.SaltSource = 0;
            _lowLevelStruct.SaltSourceData = IntPtr.Zero;
            _lowLevelStruct.SaltSourceDataLen = 0;
            _lowLevelStruct.Iterations = 0;
            _lowLevelStruct.Prf = prf;
            _lowLevelStruct.PrfData = IntPtr.Zero;
            _lowLevelStruct.PrfDataLen = 0;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PasswordLen = 0;

            _lowLevelStruct.SaltSource = saltSource;

            if (saltSourceData != null)
            {
                _lowLevelStruct.SaltSourceData = UnmanagedMemory.Allocate(saltSourceData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SaltSourceData, saltSourceData);
                _lowLevelStruct.SaltSourceDataLen = ConvertUtils.UInt64FromInt32(saltSourceData.Length);
            }

            _lowLevelStruct.Iterations = iterations;

            _lowLevelStruct.Prf = prf;

            if (prfData != null)
            {
                _lowLevelStruct.PrfData = UnmanagedMemory.Allocate(prfData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PrfData, prfData);
                _lowLevelStruct.PrfDataLen = ConvertUtils.UInt64FromInt32(prfData.Length);
            }

            if (password != null)
            {
                _lowLevelStruct.Password = UnmanagedMemory.Allocate(password.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = ConvertUtils.UInt64FromInt32(password.Length);
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
                _lowLevelStruct.PasswordLen = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkPkcs5Pbkd2Params2()
        {
            Dispose(false);
        }

        #endregion
    }
}
