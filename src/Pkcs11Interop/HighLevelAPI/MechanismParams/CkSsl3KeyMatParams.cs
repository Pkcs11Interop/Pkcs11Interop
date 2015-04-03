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
    /// Parameters for the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public class CkSsl3KeyMatParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkSsl3KeyMatParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkSsl3KeyMatParams _params4 = null;

        /// <summary>
        /// Platform specific CkSsl3KeyMatParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkSsl3KeyMatParams _params8 = null;

        /// <summary>
        /// Flag indicating whether object with returned key material has left this instance
        /// </summary>
        private bool _returnedKeyMaterialLeftInstance = false;

        /// <summary>
        /// Resulting key handles and initialization vectors after performing a DeriveKey method
        /// </summary>
        private CkSsl3KeyMatOut _returnedKeyMaterial = null;

        /// <summary>
        /// Resulting key handles and initialization vectors after performing a DeriveKey method
        /// </summary>
        public CkSsl3KeyMatOut ReturnedKeyMaterial
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (_returnedKeyMaterial == null)
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _returnedKeyMaterial = new CkSsl3KeyMatOut(_params4.ReturnedKeyMaterial);
                    else
                        _returnedKeyMaterial = new CkSsl3KeyMatOut(_params8.ReturnedKeyMaterial);

                    // Since now it is the caller's responsibility to dispose returned key material
                    _returnedKeyMaterialLeftInstance = true;
                }

                return _returnedKeyMaterial;
            }
        }

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private CkSsl3RandomData _randomInfo = null;

        /// <summary>
        /// Initializes a new instance of the CkSsl3KeyMatParams class.
        /// </summary>
        /// <param name='macSizeInBits'>The length (in bits) of the MACing keys agreed upon during the protocol handshake phase</param>
        /// <param name='keySizeInBits'>The length (in bits) of the secret keys agreed upon during the protocol handshake phase</param>
        /// <param name='ivSizeInBits'>The length (in bits) of the IV agreed upon during the protocol handshake phase or if no IV is required, the length should be set to 0</param>
        /// <param name='isExport'>Flag indicating whether the keys have to be derived for an export version of the protocol</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        public CkSsl3KeyMatParams(ulong macSizeInBits, ulong keySizeInBits, ulong ivSizeInBits, bool isExport, CkSsl3RandomData randomInfo)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");

            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            if (Platform.UnmanagedLongSize == 4)
                _params4 = new HighLevelAPI41.MechanismParams.CkSsl3KeyMatParams(Convert.ToUInt32(macSizeInBits), Convert.ToUInt32(keySizeInBits), Convert.ToUInt32(ivSizeInBits), isExport, _randomInfo._params4);
            else
                _params8 = new HighLevelAPI81.MechanismParams.CkSsl3KeyMatParams(macSizeInBits, keySizeInBits, ivSizeInBits, isExport, _randomInfo._params8);
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

                    if (_returnedKeyMaterialLeftInstance == false)
                    {
                        if (_returnedKeyMaterial != null)
                        {
                            _returnedKeyMaterial.Dispose();
                            _returnedKeyMaterial = null;
                        }
                    }
                }
                
                // Dispose unmanaged objects
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSsl3KeyMatParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
