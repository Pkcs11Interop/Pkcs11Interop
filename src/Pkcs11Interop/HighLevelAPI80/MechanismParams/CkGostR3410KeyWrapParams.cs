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
    /// Parameters for the CKM_GOSTR3410_KEY_WRAP mechanism
    /// </summary>
    public class CkGostR3410KeyWrapParams : ICkGostR3410KeyWrapParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_GOSTR3410_KEY_WRAP_PARAMS _lowLevelStruct = new CK_GOSTR3410_KEY_WRAP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkGostR3410KeyWrapParams class.
        /// </summary>
        /// <param name="wrapOID">Data with DER-encoding of the object identifier indicating the data object type of GOST 28147-89</param>
        /// <param name="ukm">Data with UKM</param>
        /// <param name="key">Key handle of a sender for wrapping operation or key handle of a receiver for unwrapping operation</param>
        public CkGostR3410KeyWrapParams(byte[] wrapOID, byte[] ukm, NativeULong key)
        {
            _lowLevelStruct.WrapOID = IntPtr.Zero;
            _lowLevelStruct.WrapOIDLen = 0;
            _lowLevelStruct.UKM = IntPtr.Zero;
            _lowLevelStruct.UKMLen = 0;
            _lowLevelStruct.Key = 0;

            if (wrapOID != null)
            {
                _lowLevelStruct.WrapOID = UnmanagedMemory.Allocate(wrapOID.Length);
                UnmanagedMemory.Write(_lowLevelStruct.WrapOID, wrapOID);
                _lowLevelStruct.WrapOIDLen = ConvertUtils.UInt64FromInt32(wrapOID.Length);
            }

            if (ukm != null)
            {
                _lowLevelStruct.UKM = UnmanagedMemory.Allocate(ukm.Length);
                UnmanagedMemory.Write(_lowLevelStruct.UKM, ukm);
                _lowLevelStruct.UKMLen = ConvertUtils.UInt64FromInt32(ukm.Length);
            }

            _lowLevelStruct.Key = key;
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
                UnmanagedMemory.Free(ref _lowLevelStruct.WrapOID);
                _lowLevelStruct.WrapOIDLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.UKM);
                _lowLevelStruct.UKMLen = 0;
                _lowLevelStruct.Key = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkGostR3410KeyWrapParams()
        {
            Dispose(false);
        }

        #endregion
    }
}
