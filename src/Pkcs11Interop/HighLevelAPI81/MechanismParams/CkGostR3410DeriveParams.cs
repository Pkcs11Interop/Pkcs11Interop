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
    /// Parameters for the CKM_GOSTR3410_DERIVE mechanism
    /// </summary>
    public class CkGostR3410DeriveParams : ICkGostR3410DeriveParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_GOSTR3410_DERIVE_PARAMS _lowLevelStruct = new CK_GOSTR3410_DERIVE_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkGostR3410DeriveParams class.
        /// </summary>
        /// <param name="kdf">Additional key diversification algorithm (CKD)</param>
        /// <param name="publicData">Data with public key of a receiver</param>
        /// <param name="ukm">UKM data</param>
        public CkGostR3410DeriveParams(NativeULong kdf, byte[] publicData, byte[] ukm)
        {
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.UKM = IntPtr.Zero;
            _lowLevelStruct.UKMLen = 0;

            if (publicData == null)
                throw new ArgumentNullException("publicData");

            if (publicData.Length != 64)
                throw new ArgumentOutOfRangeException("publicData", "Array has to be 64 bytes long");

            if (ukm == null)
                throw new ArgumentNullException("ukm");

            if (ukm.Length != 8)
                throw new ArgumentOutOfRangeException("ukm", "Array has to be 8 bytes long");

            _lowLevelStruct.Kdf = kdf;

            _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
            UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
            _lowLevelStruct.PublicDataLen = ConvertUtils.UInt64FromInt32(publicData.Length);

            _lowLevelStruct.UKM = UnmanagedMemory.Allocate(ukm.Length);
            UnmanagedMemory.Write(_lowLevelStruct.UKM, ukm);
            _lowLevelStruct.UKMLen = ConvertUtils.UInt64FromInt32(ukm.Length);
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
                _lowLevelStruct.Kdf = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.UKM);
                _lowLevelStruct.UKMLen = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkGostR3410DeriveParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
