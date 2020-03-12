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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_AES_GCM mechanism
    /// </summary>
    public class CkGcmParams : ICkGcmParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_GCM_PARAMS _lowLevelStruct = new CK_GCM_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkGcmParams class.
        /// </summary>
        /// <param name="iv">Initialization vector</param>
        /// <param name="ivBits">Member is defined in PKCS#11 v2.40e1 headers but the description is not present in the specification</param>
        /// <param name="aad">Additional authentication data</param>
        /// <param name="tagBits">Length of authentication tag (output following cipher text) in bits</param>
        public CkGcmParams(byte[] iv, NativeULong ivBits, byte[] aad, NativeULong tagBits)
        {
            _lowLevelStruct.Iv = IntPtr.Zero;
            _lowLevelStruct.IvLen = 0;
            _lowLevelStruct.IvBits = 0; // TODO - Fix description when fixed in PKCS#11 specification
            _lowLevelStruct.AAD = IntPtr.Zero;
            _lowLevelStruct.AADLen = 0;
            _lowLevelStruct.TagBits = 0;

            if (iv != null)
            {
                _lowLevelStruct.Iv = UnmanagedMemory.Allocate(iv.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Iv, iv);
                _lowLevelStruct.IvLen = ConvertUtils.UInt32FromInt32(iv.Length);
            }

            _lowLevelStruct.IvBits = ivBits;

            if (aad != null)
            {
                _lowLevelStruct.AAD = UnmanagedMemory.Allocate(aad.Length);
                UnmanagedMemory.Write(_lowLevelStruct.AAD, aad);
                _lowLevelStruct.AADLen = ConvertUtils.UInt32FromInt32(aad.Length);
            }

            _lowLevelStruct.TagBits = tagBits;
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
                _lowLevelStruct.IvBits = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.AAD);
                _lowLevelStruct.AADLen = 0;
                _lowLevelStruct.TagBits = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkGcmParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
