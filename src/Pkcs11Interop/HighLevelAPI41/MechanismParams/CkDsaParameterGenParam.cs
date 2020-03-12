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
    /// Parameters for the CKM_DSA_PROBABLISTIC_PARAMETER_GEN, CKM_DSA_SHAWE_TAYLOR_PARAMETER_GEN a CKM_DSA_FIPS_G_GEN mechanisms
    /// </summary>
    public class CkDsaParameterGenParam : ICkDsaParameterGenParam
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_DSA_PARAMETER_GEN_PARAM _lowLevelStruct = new CK_DSA_PARAMETER_GEN_PARAM();

        /// <summary>
        /// Seed value used to generate PQ and G
        /// </summary>
        public byte[] Seed
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_lowLevelStruct.Seed == IntPtr.Zero) ? null : UnmanagedMemory.Read(_lowLevelStruct.Seed, ConvertUtils.UInt32ToInt32(_lowLevelStruct.SeedLen));
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkDsaParameterGenParam class
        /// </summary>
        /// <param name="hash">Mechanism value for the base hash used in PQG generation (CKM)</param>
        /// <param name="seed">Seed value used to generate PQ and G</param>
        /// <param name="index">Index value for generating G</param>
        public CkDsaParameterGenParam(NativeULong hash, byte[] seed, NativeULong index)
        {
            _lowLevelStruct.Hash = 0;
            _lowLevelStruct.Seed = IntPtr.Zero;
            _lowLevelStruct.SeedLen = 0;
            _lowLevelStruct.Index = 0;

            _lowLevelStruct.Hash = hash;

            if (seed != null)
            {
                _lowLevelStruct.Seed = UnmanagedMemory.Allocate(seed.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Seed, seed);
                _lowLevelStruct.SeedLen = ConvertUtils.UInt32FromInt32(seed.Length);
            }

            _lowLevelStruct.Index = index;
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
                _lowLevelStruct.Hash = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Seed);
                _lowLevelStruct.SeedLen = 0;
                _lowLevelStruct.Index = 0;

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkDsaParameterGenParam()
        {
            Dispose(false);
        }

        #endregion
    }
}
