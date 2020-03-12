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
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms
    /// </summary>
    public class CkRc5CbcParams : ICkRc5CbcParams
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
        public CkRc5CbcParams(NativeULong wordsize, NativeULong rounds, byte[] iv)
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
                _lowLevelStruct.IvLen = ConvertUtils.UInt64FromInt32(iv.Length);
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
