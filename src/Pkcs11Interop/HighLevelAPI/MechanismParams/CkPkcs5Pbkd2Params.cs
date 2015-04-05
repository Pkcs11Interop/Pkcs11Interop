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
    /// Parameters for the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    public class CkPkcs5Pbkd2Params : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkPkcs5Pbkd2Params
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkPkcs5Pbkd2Params _params40 = null;

        /// <summary>
        /// Platform specific CkPkcs5Pbkd2Params
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkPkcs5Pbkd2Params _params41 = null;

        /// <summary>
        /// Platform specific CkPkcs5Pbkd2Params
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkPkcs5Pbkd2Params _params80 = null;

        /// <summary>
        /// Platform specific CkPkcs5Pbkd2Params
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkPkcs5Pbkd2Params _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkPkcs5Pbkd2Params class.
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        public CkPkcs5Pbkd2Params(ulong saltSource, byte[] saltSourceData, ulong iterations, ulong prf, byte[] prfData, byte[] password)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkPkcs5Pbkd2Params(Convert.ToUInt32(saltSource), saltSourceData, Convert.ToUInt32(iterations), Convert.ToUInt32(prf), prfData, password);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkPkcs5Pbkd2Params(Convert.ToUInt32(saltSource), saltSourceData, Convert.ToUInt32(iterations), Convert.ToUInt32(prf), prfData, password);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkPkcs5Pbkd2Params(saltSource, saltSourceData, iterations, prf, prfData, password);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkPkcs5Pbkd2Params(saltSource, saltSourceData, iterations, prf, prfData, password);
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

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _params40.ToMarshalableStructure() : _params41.ToMarshalableStructure();
            else
                return (Platform.StructPackingSize == 0) ? _params80.ToMarshalableStructure() : _params81.ToMarshalableStructure();
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
                    if (_params40 != null)
                    {
                        _params40.Dispose();
                        _params40 = null;
                    }

                    if (_params41 != null)
                    {
                        _params41.Dispose();
                        _params41 = null;
                    }

                    if (_params80 != null)
                    {
                        _params80.Dispose();
                        _params80 = null;
                    }

                    if (_params81 != null)
                    {
                        _params81.Dispose();
                        _params81 = null;
                    }
                }
                
                // Dispose unmanaged objects

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkPkcs5Pbkd2Params()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
