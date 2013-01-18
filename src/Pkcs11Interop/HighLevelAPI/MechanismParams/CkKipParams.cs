/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_KIP_DERIVE, CKM_KIP_WRAP and CKM_KIP_MAC mechanisms
    /// </summary>
    public class CkKipParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_KIP_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_KIP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkKipParams class.
        /// </summary>
        /// <param name='mechanism'>Underlying cryptographic mechanism (CKM)</param>
        /// <param name='key'>Handle to a key that will contribute to the entropy of the derived key (CKM_KIP_DERIVE) or will be used in the MAC operation (CKM_KIP_MAC)</param>
        /// <param name='seed'>Input seed</param>
        public CkKipParams(uint? mechanism, ObjectHandle key, byte[] seed)
        {
            _lowLevelStruct.Mechanism = IntPtr.Zero;
            _lowLevelStruct.Key = 0;
            _lowLevelStruct.Seed = IntPtr.Zero;
            _lowLevelStruct.SeedLen = 0;

            if (mechanism != null)
            {
                int uintSize = LowLevelAPI.UnmanagedMemory.SizeOf(typeof(uint));
                _lowLevelStruct.Mechanism = LowLevelAPI.UnmanagedMemory.Allocate(uintSize);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Mechanism, BitConverter.GetBytes((uint)mechanism));
            }

            if (key == null)
                throw new ArgumentNullException("key");
            
            _lowLevelStruct.Key = key.ObjectId;

            if (seed != null)
            {
                _lowLevelStruct.Seed = LowLevelAPI.UnmanagedMemory.Allocate(seed.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Seed, seed);
                _lowLevelStruct.SeedLen = (uint)seed.Length;
            }
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
        {
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Mechanism);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Seed);
                _lowLevelStruct.SeedLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkKipParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
