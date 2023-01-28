/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
 *  KhaledGomaa <khaledgomaa670@gmail.com>
 */

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using System;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    internal class CkEciesParams : ICkEciesParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_ECIES_PARAMS _lowLevelStruct = new CK_ECIES_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkEciesParams class.
        /// </summary>
        /// <param name="dhPrimitive">Diffie-Hellman Primitive (CKDHP) used to derive the shared secret value</param>
        /// <param name="kdf">Key derivation function used on the shared secret value (CKD)</param>
        /// <param name="sharedData1">The key derivation padding data shared between the two parties</param>
        /// <param name="encScheme">The encryption scheme (CKES) used to transform the input data</param>
        /// <param name="encKeyLenInBits">The bit length of the key to use for the encryption scheme</param>
        /// <param name="macScheme">The MAC scheme (CKMS) used for MAC generation or validation</param>
        /// <param name="macKeyLenInBits">The bit length of the key to use for the MAC scheme</param>
        /// <param name="macLenInBits">The bit length of the MAC scheme output</param>
        /// <param name="sharedData2">The MAC padding data shared between the two parties</param>
        public CkEciesParams(NativeULong dhPrimitive, NativeULong kdf, byte[] sharedData1, NativeULong encScheme, NativeULong encKeyLenInBits, NativeULong macScheme, NativeULong macKeyLenInBits, NativeULong macLenInBits, byte[] sharedData2)
        {
            _lowLevelStruct.DhPrimitive = 0;
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.SharedDataLen1 = 0;
            _lowLevelStruct.SharedData1 = IntPtr.Zero;
            _lowLevelStruct.EncScheme = 0;
            _lowLevelStruct.EncKeyLenInBits = 0;
            _lowLevelStruct.MacScheme = 0;
            _lowLevelStruct.MacKeyLenInBits = 0;
            _lowLevelStruct.MacLenInBits = 0;
            _lowLevelStruct.SharedDataLen2 = 0;
            _lowLevelStruct.SharedData2 = IntPtr.Zero;

            _lowLevelStruct.DhPrimitive = dhPrimitive;
            _lowLevelStruct.Kdf = kdf;
            _lowLevelStruct.EncScheme = encScheme;
            _lowLevelStruct.EncKeyLenInBits = encKeyLenInBits;
            _lowLevelStruct.MacScheme = macScheme;
            _lowLevelStruct.MacKeyLenInBits = macKeyLenInBits;
            _lowLevelStruct.MacLenInBits = macLenInBits;

            if (sharedData1 != null)
            {
                _lowLevelStruct.SharedData1 = UnmanagedMemory.Allocate(sharedData1.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SharedData1, sharedData1);
                _lowLevelStruct.SharedDataLen1 = ConvertUtils.UInt64FromInt32(sharedData1.Length);
            }

            if (sharedData2 != null)
            {
                _lowLevelStruct.SharedData2 = UnmanagedMemory.Allocate(sharedData2.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SharedData2, sharedData2);
                _lowLevelStruct.SharedDataLen2 = ConvertUtils.UInt64FromInt32(sharedData2.Length);
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

        #endregion IMechanismParams

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
                UnmanagedMemory.Free(ref _lowLevelStruct.SharedData1);
                _lowLevelStruct.SharedDataLen1 = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.SharedData2);
                _lowLevelStruct.SharedDataLen2 = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkEciesParams()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}