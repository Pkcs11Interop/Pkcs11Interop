/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_CMS_SIG mechanism
    /// </summary>
    public class CkCmsSigParams : ICkCmsSigParams
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_CMS_SIG_PARAMS _lowLevelStruct = new CK_CMS_SIG_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkCmsSigParams class.
        /// </summary>
        /// <param name='certificateHandle'>Object handle for a certificate associated with the signing key</param>
        /// <param name='signingMechanism'>Mechanism to use when signing a constructed CMS SignedAttributes value</param>
        /// <param name='digestMechanism'>Mechanism to use when digesting the data</param>
        /// <param name='contentType'>String indicating complete MIME Content-type of message to be signed or null if the message is a MIME object</param>
        /// <param name='requestedAttributes'>DER-encoded list of CMS Attributes the caller requests to be included in the signed attributes</param>
        /// <param name='requiredAttributes'>DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes</param>
        public CkCmsSigParams(IObjectHandle certificateHandle, NativeULong? signingMechanism, NativeULong? digestMechanism, string contentType, byte[] requestedAttributes, byte[] requiredAttributes)
        {
            _lowLevelStruct.CertificateHandle = CK.CK_INVALID_HANDLE;
            _lowLevelStruct.SigningMechanism = IntPtr.Zero;
            _lowLevelStruct.DigestMechanism = IntPtr.Zero;
            _lowLevelStruct.ContentType = IntPtr.Zero;
            _lowLevelStruct.RequestedAttributes = IntPtr.Zero;
            _lowLevelStruct.RequestedAttributesLen = 0;
            _lowLevelStruct.RequiredAttributes = IntPtr.Zero;
            _lowLevelStruct.RequiredAttributesLen = 0;

            if (certificateHandle == null)
                throw new ArgumentNullException("certificateHandle");

            _lowLevelStruct.CertificateHandle = ConvertUtils.UInt64FromUInt64(certificateHandle.ObjectId);

            if (signingMechanism != null)
            {
                byte[] bytes = ConvertUtils.UInt64ToBytes(signingMechanism.Value);
                _lowLevelStruct.SigningMechanism = UnmanagedMemory.Allocate(bytes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SigningMechanism, bytes);
            }

            if (digestMechanism != null)
            {
                byte[] bytes = ConvertUtils.UInt64ToBytes(digestMechanism.Value);
                _lowLevelStruct.DigestMechanism = UnmanagedMemory.Allocate(bytes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.DigestMechanism, bytes);
            }

            if (contentType != null)
            {
                byte[] bytes = ConvertUtils.Utf8StringToBytes(contentType);
                Array.Resize(ref bytes, bytes.Length + 1);
                bytes[bytes.Length - 1] = 0;
                
                _lowLevelStruct.ContentType = UnmanagedMemory.Allocate(bytes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.ContentType, bytes);
            }

            if (requestedAttributes != null)
            {
                _lowLevelStruct.RequestedAttributes = UnmanagedMemory.Allocate(requestedAttributes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RequestedAttributes, requestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = ConvertUtils.UInt64FromInt32(requestedAttributes.Length);
            }

            if (requiredAttributes != null)
            {
                _lowLevelStruct.RequiredAttributes = UnmanagedMemory.Allocate(requiredAttributes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RequiredAttributes, requiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = ConvertUtils.UInt64FromInt32(requiredAttributes.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.SigningMechanism);
                UnmanagedMemory.Free(ref _lowLevelStruct.DigestMechanism);
                UnmanagedMemory.Free(ref _lowLevelStruct.ContentType);
                UnmanagedMemory.Free(ref _lowLevelStruct.RequestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.RequiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkCmsSigParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
