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
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_WTLS_PRF mechanism
    /// </summary>
    public class CkWtlsPrfParams : ICkWtlsPrfParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_WTLS_PRF_PARAMS _lowLevelStruct = new CK_WTLS_PRF_PARAMS();
        
        /// <summary>
        /// Output of the operation
        /// </summary>
        public byte[] Output
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                int nativeULongSize = UnmanagedMemory.SizeOf(typeof(NativeULong));
                byte[] outputLenBytes = UnmanagedMemory.Read(_lowLevelStruct.OutputLen, nativeULongSize);
                NativeULong outputLen = ConvertUtils.UInt64FromBytes(outputLenBytes);
                return UnmanagedMemory.Read(_lowLevelStruct.Output, ConvertUtils.UInt64ToInt32(outputLen));
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsPrfParams class.
        /// </summary>
        /// <param name='digestMechanism'>Digest mechanism to be used (CKM)</param>
        /// <param name='seed'>Input seed</param>
        /// <param name='label'>Identifying label</param>
        /// <param name='outputLen'>Length in bytes that the output to be created shall have</param>
        public CkWtlsPrfParams(NativeULong digestMechanism, byte[] seed, byte[] label, NativeULong outputLen)
        {
            _lowLevelStruct.DigestMechanism = 0;
            _lowLevelStruct.Seed = IntPtr.Zero;
            _lowLevelStruct.SeedLen = 0;
            _lowLevelStruct.Label = IntPtr.Zero;
            _lowLevelStruct.LabelLen = 0;
            _lowLevelStruct.Output = IntPtr.Zero;
            _lowLevelStruct.OutputLen = IntPtr.Zero;

            _lowLevelStruct.DigestMechanism = digestMechanism;

            if (seed != null)
            {
                _lowLevelStruct.Seed = UnmanagedMemory.Allocate(seed.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Seed, seed);
                _lowLevelStruct.SeedLen = ConvertUtils.UInt64FromInt32(seed.Length);
            }
            
            if (label != null)
            {
                _lowLevelStruct.Label = UnmanagedMemory.Allocate(label.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Label, label);
                _lowLevelStruct.LabelLen = ConvertUtils.UInt64FromInt32(label.Length);
            }
            
            if (outputLen < 1)
                throw new ArgumentException("Value has to be positive number", "outputLen");
            
            _lowLevelStruct.Output = UnmanagedMemory.Allocate(ConvertUtils.UInt64ToInt32(outputLen));
            
            byte[] outputLenBytes = ConvertUtils.UInt64ToBytes(outputLen);
            _lowLevelStruct.OutputLen = UnmanagedMemory.Allocate(outputLenBytes.Length);
            UnmanagedMemory.Write(_lowLevelStruct.OutputLen, outputLenBytes);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Seed);
                _lowLevelStruct.SeedLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Label);
                _lowLevelStruct.LabelLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Output);
                UnmanagedMemory.Free(ref _lowLevelStruct.OutputLen);
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkWtlsPrfParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
