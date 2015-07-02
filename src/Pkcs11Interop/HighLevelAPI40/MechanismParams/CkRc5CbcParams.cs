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
    /// Parameters for the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms
    /// </summary>
    public class CkRc5CbcParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RC5_CBC_PARAMS _lowLevelStruct = new CK_RC5_CBC_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRc5CbcParams class.
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <param name='iv'>Initialization vector (IV) for CBC encryption</param>
        public CkRc5CbcParams(uint wordsize, uint rounds, byte[] iv)
        {
            _lowLevelStruct.Wordsize = 0;
            _lowLevelStruct.Rounds = 0;
            _lowLevelStruct.Iv = IntPtr.Zero;
            _lowLevelStruct.IvLen = 0;

            _lowLevelStruct.Wordsize = wordsize;

            _lowLevelStruct.Rounds = rounds;

            if (iv != null)
            {
                _lowLevelStruct.Iv = UnmanagedMemory.Allocate(iv.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Iv, iv);
                _lowLevelStruct.IvLen = Convert.ToUInt32(iv.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Iv);
                _lowLevelStruct.IvLen = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkRc5CbcParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
