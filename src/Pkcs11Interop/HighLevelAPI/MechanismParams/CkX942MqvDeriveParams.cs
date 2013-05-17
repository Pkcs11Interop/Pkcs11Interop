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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
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
        private LowLevelAPI.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS();
        
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
                _lowLevelStruct.OtherInfo = LowLevelAPI.UnmanagedMemory.Allocate(otherInfo.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.OtherInfo, otherInfo);
                _lowLevelStruct.OtherInfoLen = (uint)otherInfo.Length;
            }
            
            if (publicData != null)
            {
                _lowLevelStruct.PublicData = LowLevelAPI.UnmanagedMemory.Allocate(publicData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData, publicData);
                _lowLevelStruct.PublicDataLen = (uint)publicData.Length;
            }
            
            _lowLevelStruct.PrivateDataLen = privateDataLen;
            
            if (privateData == null)
                throw new ArgumentNullException("privateData");
            
            _lowLevelStruct.PrivateData = privateData.ObjectId;
            
            if (publicData2 != null)
            {
                _lowLevelStruct.PublicData2 = LowLevelAPI.UnmanagedMemory.Allocate(publicData2.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.PublicData2, publicData2);
                _lowLevelStruct.PublicDataLen2 = (uint)publicData2.Length;
            }
            
            if (publicKey == null)
                throw new ArgumentNullException("publicKey");
            
            _lowLevelStruct.PublicKey = publicKey.ObjectId;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.OtherInfo);
                _lowLevelStruct.OtherInfoLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData);
                _lowLevelStruct.PublicDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.PublicData2);
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
