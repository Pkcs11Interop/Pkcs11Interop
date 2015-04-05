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
    /// Parameters for the CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE and the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanisms
    /// </summary>
    public class CkWtlsKeyMatParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkWtlsKeyMatParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkWtlsKeyMatParams _params40 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkWtlsKeyMatParams _params41 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkWtlsKeyMatParams _params80 = null;

        /// <summary>
        /// Platform specific CkWtlsKeyMatParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkWtlsKeyMatParams _params81 = null;

        /// <summary>
        /// Flag indicating whether object with returned key material has left this instance
        /// </summary>
        private bool _returnedKeyMaterialLeftInstance = false;

        /// <summary>
        /// Resulting key handles and initialization vector after performing a DeriveKey method
        /// </summary>
        private CkWtlsKeyMatOut _returnedKeyMaterial = null;

        /// <summary>
        /// Resulting key handles and initialization vector after performing a DeriveKey method
        /// </summary>
        public CkWtlsKeyMatOut ReturnedKeyMaterial
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (_returnedKeyMaterial == null)
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _returnedKeyMaterial = (Platform.StructPackingSize == 0) ? new CkWtlsKeyMatOut(_params40.ReturnedKeyMaterial) : new CkWtlsKeyMatOut(_params41.ReturnedKeyMaterial);
                    else
                        _returnedKeyMaterial = (Platform.StructPackingSize == 0) ? new CkWtlsKeyMatOut(_params80.ReturnedKeyMaterial) : new CkWtlsKeyMatOut(_params81.ReturnedKeyMaterial);

                    // Since now it is the caller's responsibility to dispose returned key material
                    _returnedKeyMaterialLeftInstance = true;
                }
                
                return _returnedKeyMaterial;
            }
        }
        
        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private CkWtlsRandomData _randomInfo = null;
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsKeyMatParams class.
        /// </summary>
        /// <param name='digestMechanism'>The digest mechanism to be used (CKM)</param>
        /// <param name='macSizeInBits'>The length (in bits) of the MACing key agreed upon during the protocol handshake phase</param>
        /// <param name='keySizeInBits'>The length (in bits) of the secret key agreed upon during the handshake phase</param>
        /// <param name='ivSizeInBits'>The length (in bits) of the IV agreed upon during the handshake phase or if no IV is required, the length should be set to 0</param>
        /// <param name='sequenceNumber'>The current sequence number used for records sent by the client and server respectively</param>
        /// <param name='isExport'>Flag indicating whether the keys have to be derived for an export version of the protocol</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        public CkWtlsKeyMatParams(ulong digestMechanism, ulong macSizeInBits, ulong keySizeInBits, ulong ivSizeInBits, ulong sequenceNumber, bool isExport, CkWtlsRandomData randomInfo)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkWtlsKeyMatParams(Convert.ToUInt32(digestMechanism), Convert.ToUInt32(macSizeInBits), Convert.ToUInt32(keySizeInBits), Convert.ToUInt32(ivSizeInBits), Convert.ToUInt32(sequenceNumber), isExport, _randomInfo._params40);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkWtlsKeyMatParams(Convert.ToUInt32(digestMechanism), Convert.ToUInt32(macSizeInBits), Convert.ToUInt32(keySizeInBits), Convert.ToUInt32(ivSizeInBits), Convert.ToUInt32(sequenceNumber), isExport, _randomInfo._params41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkWtlsKeyMatParams(digestMechanism, macSizeInBits, keySizeInBits, ivSizeInBits, sequenceNumber, isExport, _randomInfo._params80);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkWtlsKeyMatParams(digestMechanism, macSizeInBits, keySizeInBits, ivSizeInBits, sequenceNumber, isExport, _randomInfo._params81);
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
        ~CkWtlsKeyMatParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
