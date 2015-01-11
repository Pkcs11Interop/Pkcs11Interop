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
        /// Platform specific CkRsaPkcsOaepParams
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkRsaPkcsOaepParams _params4 = null;

        /// <summary>
        /// Platform specific CkRsaPkcsOaepParams
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkRsaPkcsOaepParams _params8 = null;

        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsOaepParams class.
        /// </summary>
        /// <param name='hashAlg'>Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='source'>Source of the encoding parameter (CKZ)</param>
        /// <param name='sourceData'>Data used as the input for the encoding parameter source</param>
        public CkRsaPkcsOaepParams(ulong hashAlg, ulong mgf, ulong source, byte[] sourceData)
        {
            if (UnmanagedLong.Size == 4)
                _params4 = new HighLevelAPI4.MechanismParams.CkRsaPkcsOaepParams(Convert.ToUInt32(hashAlg), Convert.ToUInt32(mgf), Convert.ToUInt32(source), sourceData);
            else
                _params8 = new HighLevelAPI8.MechanismParams.CkRsaPkcsOaepParams(hashAlg, mgf, source, sourceData);
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

            if (UnmanagedLong.Size == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
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
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects

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

