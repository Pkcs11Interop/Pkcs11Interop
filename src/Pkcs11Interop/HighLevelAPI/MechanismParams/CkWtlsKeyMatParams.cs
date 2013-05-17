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
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.LowLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI.MechanismParams;

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
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_WTLS_KEY_MAT_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_WTLS_KEY_MAT_PARAMS();
        
        /// <summary>
        /// Resulting key handles and initialization vector after performing a DeriveKey method
        /// </summary>
        public CkWtlsKeyMatOut ReturnedKeyMaterial
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                // Abrakadabra :)
                UnmanagedMemory.Read(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);
                return _returnedKeyMaterial;
            }
        }
        
        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        private CkWtlsRandomData _randomInfo = null;
        
        /// <summary>
        /// Handles for the keys generated and the IV
        /// </summary>
        private CkWtlsKeyMatOut _returnedKeyMaterial = null;
        
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
        public CkWtlsKeyMatParams(uint digestMechanism, uint macSizeInBits, uint keySizeInBits, uint ivSizeInBits, uint sequenceNumber, bool isExport, CkWtlsRandomData randomInfo)
        {
            if (randomInfo == null)
                throw new ArgumentNullException("randomInfo");
            
            // Keep reference to randomInfo so GC will not free it while this object exists
            _randomInfo = randomInfo;
            
            if (ivSizeInBits % 8 != 0)
                throw new ArgumentException("Value has to be a multiple of 8", "ivSizeInBits");
            
            // GC will not free ReturnedKeyMaterial while this object exists
            _returnedKeyMaterial = new CkWtlsKeyMatOut(ivSizeInBits / 8);

            _lowLevelStruct.DigestMechanism = digestMechanism;
            _lowLevelStruct.MacSizeInBits = macSizeInBits;
            _lowLevelStruct.KeySizeInBits = keySizeInBits;
            _lowLevelStruct.IVSizeInBits = ivSizeInBits;
            _lowLevelStruct.SequenceNumber = sequenceNumber;
            _lowLevelStruct.IsExport = isExport;
            _lowLevelStruct.RandomInfo = (CK_WTLS_RANDOM_DATA)_randomInfo.ToLowLevelParams();
            
            // Abrakadabra :)
            _lowLevelStruct.ReturnedKeyMaterial = UnmanagedMemory.Allocate(UnmanagedMemory.SizeOf(typeof(CK_WTLS_KEY_MAT_OUT)));
            UnmanagedMemory.Write(_lowLevelStruct.ReturnedKeyMaterial, _returnedKeyMaterial._lowLevelStruct);
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ReturnedKeyMaterial);
                
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
