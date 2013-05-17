/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RSA_PKCS_OAEP mechanism
    /// </summary>
    public class CkRsaPkcsOaepParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsOaepParams class.
        /// </summary>
        /// <param name='hashAlg'>Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='source'>Source of the encoding parameter (CKZ)</param>
        /// <param name='sourceData'>Data used as the input for the encoding parameter source</param>
        public CkRsaPkcsOaepParams(uint hashAlg, uint mgf, uint source, byte[] sourceData)
        {
            _lowLevelStruct.HashAlg = 0;
            _lowLevelStruct.Mgf = 0;
            _lowLevelStruct.Source = 0;
            _lowLevelStruct.SourceData = IntPtr.Zero;
            _lowLevelStruct.SourceDataLen = 0;

            _lowLevelStruct.HashAlg = hashAlg;
            _lowLevelStruct.Mgf = mgf;
            _lowLevelStruct.Source = source;

            if (sourceData != null)
            {
                _lowLevelStruct.SourceData = LowLevelAPI.UnmanagedMemory.Allocate(sourceData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SourceData, sourceData);
                _lowLevelStruct.SourceDataLen = (uint)sourceData.Length;
            }
        }

        #region IMechanismParams

        /// <summary>
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SourceData);
                _lowLevelStruct.SourceDataLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkRsaPkcsOaepParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}

