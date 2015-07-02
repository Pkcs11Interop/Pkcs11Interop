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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
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
        /// Low level mechanism parameters
        /// </summary>
        private CK_SSL3_KEY_MAT_PARAMS _lowLevelStruct = new CK_SSL3_KEY_MAT_PARAMS();

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

                // Abrakadabra :)
                UnmanagedMemory.Read(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);

                // Since now it is the caller's responsibility to dispose returned key material
                _returnedKeyMaterialLeftInstance = true;

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
        public CkSsl3KeyMatParams(uint macSizeInBits, uint keySizeInBits, uint ivSizeInBits, bool isExport, CkSsl3RandomData randomInfo)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;

            if (ivSizeInBits % 8 != 0)
                throw new ArgumentException("Value has to be a multiple of 8", "ivSizeInBits");

            // GC will not free ReturnedKeyMaterial while this object exists
            _returnedKeyMaterial = new CkSsl3KeyMatOut(ivSizeInBits / 8);

            _lowLevelStruct.MacSizeInBits = macSizeInBits;
            _lowLevelStruct.KeySizeInBits = keySizeInBits;
            _lowLevelStruct.IVSizeInBits = ivSizeInBits;
            _lowLevelStruct.IsExport = isExport;
            _lowLevelStruct.RandomInfo = (CK_SSL3_RANDOM_DATA)_randomInfo.ToMarshalableStructure();

            // Abrakadabra :)
            _lowLevelStruct.ReturnedKeyMaterial = UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_SSL3_KEY_MAT_OUT)));
            UnmanagedMemory.Write(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.ReturnedKeyMaterial);
                
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
