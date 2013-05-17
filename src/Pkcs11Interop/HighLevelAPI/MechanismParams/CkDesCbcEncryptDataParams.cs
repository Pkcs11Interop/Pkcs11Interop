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
    /// Parameters for the CKM_DES_CBC_ENCRYPT_DATA and CKM_DES3_CBC_ENCRYPT_DATA mechanisms
    /// </summary>
    public class CkDesCbcEncryptDataParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkDesCbcEncryptDataParams class.
        /// </summary>
        /// <param name='iv'>IV value (8 bytes)</param>
        /// <param name='data'>Data to encrypt</param>
        public CkDesCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            _lowLevelStruct.Iv = new byte[8];
            _lowLevelStruct.Data = IntPtr.Zero;
            _lowLevelStruct.Length = 0;

            if (iv == null)
                throw new ArgumentNullException("iv");
            
            if (iv.Length != 8)
                throw new ArgumentOutOfRangeException("iv", "Array has to be 8 bytes long");

            Array.Copy(iv, _lowLevelStruct.Iv, iv.Length);

            if (data != null)
            {
                _lowLevelStruct.Data = LowLevelAPI.UnmanagedMemory.Allocate(data.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Data, data);
                _lowLevelStruct.Length = (uint)data.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Data);
                _lowLevelStruct.Length = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkDesCbcEncryptDataParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
