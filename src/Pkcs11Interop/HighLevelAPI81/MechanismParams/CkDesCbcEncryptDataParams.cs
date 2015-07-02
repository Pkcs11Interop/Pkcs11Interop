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
        private CK_DES_CBC_ENCRYPT_DATA_PARAMS _lowLevelStruct = new CK_DES_CBC_ENCRYPT_DATA_PARAMS();
        
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
                _lowLevelStruct.Data = UnmanagedMemory.Allocate(data.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Data, data);
                _lowLevelStruct.Length = Convert.ToUInt64(data.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Data);
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
