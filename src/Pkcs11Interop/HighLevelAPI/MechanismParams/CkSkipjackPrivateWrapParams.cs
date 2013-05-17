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
    /// Parameters for the CKM_SKIPJACK_PRIVATE_WRAP mechanism
    /// </summary>
    public class CkSkipjackPrivateWrapParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS();
        
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
                _lowLevelStruct.Password = LowLevelAPI.UnmanagedMemory.Allocate(password.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Password, password);
                _lowLevelStruct.PasswordLen = (uint)password.Length;
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = LowLevelAPI.UnmanagedMemory.Allocate(publicData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = (uint)publicData.Length;
            }

            if (randomA != null)
            {
                _lowLevelStruct.RandomA = LowLevelAPI.UnmanagedMemory.Allocate(randomA.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RandomA, randomA);
                _lowLevelStruct.RandomLen = (uint)randomA.Length;
            }

            if ((primeP != null) && (baseG != null))
            {
                if (primeP.Length != baseG.Length)
                    throw new ArgumentException("Length of primeP has to be the same as length of baseG");
            }

            if (primeP != null)
            {
                _lowLevelStruct.PrimeP = LowLevelAPI.UnmanagedMemory.Allocate(primeP.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PrimeP, primeP);
                _lowLevelStruct.PAndGLen = (uint)primeP.Length;
            }

            if (baseG != null)
            {
                _lowLevelStruct.BaseG = LowLevelAPI.UnmanagedMemory.Allocate(baseG.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.BaseG, baseG);
                _lowLevelStruct.PAndGLen = (uint)baseG.Length;
            }

            if (subprimeQ != null)
            {
                _lowLevelStruct.SubprimeQ = LowLevelAPI.UnmanagedMemory.Allocate(subprimeQ.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SubprimeQ, subprimeQ);
                _lowLevelStruct.QLen = (uint)subprimeQ.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Password);
                _lowLevelStruct.PasswordLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RandomA);
                _lowLevelStruct.RandomLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PrimeP);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.BaseG);
                _lowLevelStruct.PAndGLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SubprimeQ);
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
