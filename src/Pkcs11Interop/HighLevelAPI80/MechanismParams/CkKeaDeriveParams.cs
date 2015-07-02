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
        private CK_KEA_DERIVE_PARAMS _lowLevelStruct = new CK_KEA_DERIVE_PARAMS();

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
                _lowLevelStruct.RandomA = UnmanagedMemory.Allocate(randomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RandomA, randomA);
                _lowLevelStruct.RandomLen = Convert.ToUInt64(randomA.Length);
            }
            
            if (randomB != null)
            {
                _lowLevelStruct.RandomB = UnmanagedMemory.Allocate(randomB.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RandomB, randomB);
                _lowLevelStruct.RandomLen = Convert.ToUInt64(randomB.Length);
            }

            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = Convert.ToUInt64(publicData.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.RandomA);
                UnmanagedMemory.Free(ref _lowLevelStruct.RandomB);
                _lowLevelStruct.RandomLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
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
