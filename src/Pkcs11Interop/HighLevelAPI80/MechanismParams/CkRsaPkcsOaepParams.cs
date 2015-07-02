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
        private CK_RSA_PKCS_OAEP_PARAMS _lowLevelStruct = new CK_RSA_PKCS_OAEP_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsOaepParams class.
        /// </summary>
        /// <param name='hashAlg'>Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='source'>Source of the encoding parameter (CKZ)</param>
        /// <param name='sourceData'>Data used as the input for the encoding parameter source</param>
        public CkRsaPkcsOaepParams(ulong hashAlg, ulong mgf, ulong source, byte[] sourceData)
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
                _lowLevelStruct.SourceData = UnmanagedMemory.Allocate(sourceData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SourceData, sourceData);
                _lowLevelStruct.SourceDataLen = Convert.ToUInt64(sourceData.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SourceData);
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

