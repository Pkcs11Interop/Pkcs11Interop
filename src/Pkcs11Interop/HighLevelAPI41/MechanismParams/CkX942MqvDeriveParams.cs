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
    /// Parameters for the CKM_X9_42_MQV_DERIVE key derivation mechanism
    /// </summary>
    public class CkX942MqvDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_X9_42_MQV_DERIVE_PARAMS _lowLevelStruct = new CK_X9_42_MQV_DERIVE_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkX942MqvDeriveParams class.
        /// </summary>>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='otherInfo'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first X9.42 Diffie-Hellman public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second X9.42 Diffie-Hellman private key</param>
        /// <param name='privateData'>Key handle for second X9.42 Diffie-Hellman private key value</param>
        /// <param name='publicData2'>Other party's second X9.42 Diffie-Hellman public key value</param>
        /// <param name='publicKey'>Handle to the first party's ephemeral public key</param>
        public CkX942MqvDeriveParams(uint kdf, byte[] otherInfo, byte[] publicData, uint privateDataLen, ObjectHandle privateData, byte[] publicData2, ObjectHandle publicKey)
        {
            _lowLevelStruct.Kdf = 0;
            _lowLevelStruct.OtherInfoLen = 0;
            _lowLevelStruct.OtherInfo = IntPtr.Zero;
            _lowLevelStruct.PublicDataLen = 0;
            _lowLevelStruct.PublicData = IntPtr.Zero;
            _lowLevelStruct.PrivateDataLen = 0;
            _lowLevelStruct.PrivateData = 0;
            _lowLevelStruct.PublicDataLen2 = 0;
            _lowLevelStruct.PublicData2 = IntPtr.Zero;
            _lowLevelStruct.PublicKey = 0;
            
            _lowLevelStruct.Kdf = kdf;
            
            if (otherInfo != null)
            {
                _lowLevelStruct.OtherInfo = UnmanagedMemory.Allocate(otherInfo.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OtherInfo, otherInfo);
                _lowLevelStruct.OtherInfoLen = Convert.ToUInt32(otherInfo.Length);
            }
            
            if (publicData != null)
            {
                _lowLevelStruct.PublicData = UnmanagedMemory.Allocate(publicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = Convert.ToUInt32(publicData.Length);
            }
            
            _lowLevelStruct.PrivateDataLen = privateDataLen;
            
            if (privateData == null)
                throw new ArgumentNullException("privateData");
            
            _lowLevelStruct.PrivateData = privateData.ObjectId;
            
            if (publicData2 != null)
            {
                _lowLevelStruct.PublicData2 = UnmanagedMemory.Allocate(publicData2.Length);
                UnmanagedMemory.Write(_lowLevelStruct.PublicData2, publicData2);
                _lowLevelStruct.PublicDataLen2 = Convert.ToUInt32(publicData2.Length);
            }
            
            if (publicKey == null)
                throw new ArgumentNullException("publicKey");
            
            _lowLevelStruct.PublicKey = publicKey.ObjectId;
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
                UnmanagedMemory.Free(ref _lowLevelStruct.OtherInfo);
                _lowLevelStruct.OtherInfoLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.PublicData2);
                _lowLevelStruct.PublicDataLen2 = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkX942MqvDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
