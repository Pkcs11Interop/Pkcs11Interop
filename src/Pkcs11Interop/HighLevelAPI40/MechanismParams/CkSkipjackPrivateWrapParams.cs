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
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
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
                _lowLevelStruct.PasswordLen = Convert.ToUInt32(password.Length);
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = Convert.ToUInt32(publicData.Length);
            }

            if (randomA != null)
            {
                _lowLevelStruct.RandomA = UnmanagedMemory.Allocate(randomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RandomA, randomA);
                _lowLevelStruct.RandomLen = Convert.ToUInt32(randomA.Length);
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
                _lowLevelStruct.PAndGLen = Convert.ToUInt32(primeP.Length);
            }

            if (baseG != null)
            {
                _lowLevelStruct.BaseG = UnmanagedMemory.Allocate(baseG.Length);
                UnmanagedMemory.Write(_lowLevelStruct.BaseG, baseG);
                _lowLevelStruct.PAndGLen = Convert.ToUInt32(baseG.Length);
            }

            if (subprimeQ != null)
            {
                _lowLevelStruct.SubprimeQ = UnmanagedMemory.Allocate(subprimeQ.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SubprimeQ, subprimeQ);
                _lowLevelStruct.QLen = Convert.ToUInt32(subprimeQ.Length);
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
