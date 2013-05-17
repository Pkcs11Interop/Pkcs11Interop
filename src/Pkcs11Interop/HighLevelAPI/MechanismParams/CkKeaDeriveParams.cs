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
    /// Parameters for the CKM_KEA_DERIVE mechanism
    /// </summary>
    public class CkKeaDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_KEA_DERIVE_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_KEA_DERIVE_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkKeaDeriveParams class.
        /// </summary>
        /// <param name='isSender'>Option for generating the key (called a TEK). True if the sender (originator) generates the TEK, false if the recipient is regenerating the TEK.</param>
        /// <param name='randomA'>Ra data</param>
        /// <param name='randomB'>Rb data</param>
        /// <param name='publicData'>Other party's KEA public key value</param>
        public CkKeaDeriveParams(bool isSender, byte[] randomA, byte[] randomB, byte[] publicData)
        {
            _lowLevelStruct.IsSender = false;
            _lowLevelStruct.RandomLen = 0;
            _lowLevelStruct.RandomA = IntPtr.Zero;
            _lowLevelStruct.RandomB = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;

            _lowLevelStruct.IsSender = isSender;

            if ((randomA != null) && (randomB != null))
            {
                if (randomA.Length != randomB.Length)
                    throw new ArgumentException("Length of randomA has to be the same as length of randomB");
            }
            
            if (randomA != null)
            {
                _lowLevelStruct.RandomA = LowLevelAPI.UnmanagedMemory.Allocate(randomA.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RandomA, randomA);
                _lowLevelStruct.RandomLen = (uint)randomA.Length;
            }
            
            if (randomB != null)
            {
                _lowLevelStruct.RandomB = LowLevelAPI.UnmanagedMemory.Allocate(randomB.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RandomB, randomB);
                _lowLevelStruct.RandomLen = (uint)randomB.Length;
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = LowLevelAPI.UnmanagedMemory.Allocate(publicData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = (uint)publicData.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RandomA);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RandomB);
                _lowLevelStruct.RandomLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkKeaDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
