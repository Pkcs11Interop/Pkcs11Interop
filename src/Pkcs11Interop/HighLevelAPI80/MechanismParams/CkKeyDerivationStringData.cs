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
    /// Parameters for the CKM_CONCATENATE_BASE_AND_DATA, CKM_CONCATENATE_DATA_AND_BASE and CKM_XOR_BASE_AND_DATA mechanisms
    /// </summary>
    public class CkKeyDerivationStringData : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_KEY_DERIVATION_STRING_DATA _lowLevelStruct = new CK_KEY_DERIVATION_STRING_DATA();

        /// <summary>
        /// Initializes a new instance of the CkKeyDerivationStringData class.
        /// </summary>
        /// <param name='data'>Byte string used as the input for derivation mechanism</param>
        public CkKeyDerivationStringData(byte[] data)
        {
            _lowLevelStruct.Data = IntPtr.Zero;
            _lowLevelStruct.Len = 0;

            if (data != null)
            {
                _lowLevelStruct.Data = UnmanagedMemory.Allocate(data.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Data, data);
                _lowLevelStruct.Len = Convert.ToUInt64(data.Length);
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
                _lowLevelStruct.Len = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkKeyDerivationStringData()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
