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
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SKIPJACK_PRIVATE_WRAP mechanism
    /// </summary>
    public class CkSkipjackPrivateWrapParams : ICkSkipjackPrivateWrapParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_SKIPJACK_PRIVATE_WRAP_PARAMS _lowLevelStruct = new CK_SKIPJACK_PRIVATE_WRAP_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkSkipjackPrivateWrapParams class.
        /// </summary>
        /// <param name='password'>User-supplied password</param>
        /// <param name='publicData'>Other party's key exchange public key value</param>
        /// <param name='randomA'>Ra data</param>
        /// <param name='primeP'>Prime, p, value</param>
        /// <param name='baseG'>Base, g, value</param>
        /// <param name='subprimeQ'>Subprime, q, value</param>
        public CkSkipjackPrivateWrapParams(byte[] password, byte[] publicData, byte[] randomA, byte[] primeP, byte[] baseG, byte[] subprimeQ)
        {
            _lowLevelStruct.PasswordLen = 0;
            _lowLevelStruct.Password = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;
            _lowLevelStruct.PAndGLen = 0;
            _lowLevelStruct.QLen = 0;
            _lowLevelStruct.RandomLen = 0;
            _lowLevelStruct.RandomA = IntPtr.Zero;
            _lowLevelStruct.PrimeP = IntPtr.Zero;
            _lowLevelStruct.BaseG = IntPtr.Zero;
            _lowLevelStruct.SubprimeQ = IntPtr.Zero;

            if (password != null)
            {
                _lowLevelStruct.Password = UnmanagedMemory.Allocate(password.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = ConvertUtils.UInt64FromInt32(password.Length);
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = ConvertUtils.UInt64FromInt32(publicData.Length);
            }

            if (randomA != null)
            {
                _lowLevelStruct.RandomA = UnmanagedMemory.Allocate(randomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RandomA, randomA);
                _lowLevelStruct.RandomLen = ConvertUtils.UInt64FromInt32(randomA.Length);
            }

            if ((primeP != null) && (baseG != null))
            {
                if (primeP.Length != baseG.Length)
                    throw new ArgumentException("Length of primeP has to be the same as length of baseG");
            }

            if (primeP != null)
            {
                _lowLevelStruct.PrimeP = UnmanagedMemory.Allocate(primeP.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PrimeP, primeP);
                _lowLevelStruct.PAndGLen = ConvertUtils.UInt64FromInt32(primeP.Length);
            }

            if (baseG != null)
            {
                _lowLevelStruct.BaseG = UnmanagedMemory.Allocate(baseG.Length);
                UnmanagedMemory.Write(_lowLevelStruct.BaseG, baseG);
                _lowLevelStruct.PAndGLen = ConvertUtils.UInt64FromInt32(baseG.Length);
            }

            if (subprimeQ != null)
            {
                _lowLevelStruct.SubprimeQ = UnmanagedMemory.Allocate(subprimeQ.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SubprimeQ, subprimeQ);
                _lowLevelStruct.QLen = ConvertUtils.UInt64FromInt32(subprimeQ.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.RandomA);
                _lowLevelStruct.RandomLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PrimeP);
                UnmanagedMemory.Free(ref _lowLevelStruct.BaseG);
                _lowLevelStruct.PAndGLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.SubprimeQ);
                _lowLevelStruct.QLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSkipjackPrivateWrapParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
