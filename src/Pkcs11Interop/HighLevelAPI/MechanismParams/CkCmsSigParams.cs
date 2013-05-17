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
using Net.Pkcs11Interop.Common;
using System.Text;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_CMS_SIG mechanism
    /// </summary>
    public class CkCmsSigParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_CMS_SIG_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_CMS_SIG_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkCmsSigParams class.
        /// </summary>
        /// <param name='certificateHandle'>Object handle for a certificate associated with the signing key</param>
        /// <param name='signingMechanism'>Mechanism to use when signing a constructed CMS SignedAttributes value</param>
        /// <param name='digestMechanism'>Mechanism to use when digesting the data</param>
        /// <param name='contentType'>String indicating complete MIME Content-type of message to be signed or null if the message is a MIME object</param>
        /// <param name='requestedAttributes'>DER-encoded list of CMS Attributes the caller requests to be included in the signed attributes</param>
        /// <param name='requiredAttributes'>DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes</param>
        public CkCmsSigParams(ObjectHandle certificateHandle, uint? signingMechanism, uint? digestMechanism, string contentType, byte[] requestedAttributes, byte[] requiredAttributes)
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

            _lowLevelStruct.CertificateHandle = certificateHandle.ObjectId;

            if (signingMechanism != null)
            {
                byte[] bytes = Common.ConvertUtils.UintToBytes((uint)signingMechanism);
                _lowLevelStruct.SigningMechanism = LowLevelAPI.UnmanagedMemory.Allocate(bytes.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.SigningMechanism, bytes);
            }

            if (digestMechanism != null)
            {
                byte[] bytes = Common.ConvertUtils.UintToBytes((uint)digestMechanism);
                _lowLevelStruct.DigestMechanism = LowLevelAPI.UnmanagedMemory.Allocate(bytes.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.DigestMechanism, bytes);
            }

            if (contentType != null)
            {
                byte[] bytes = ConvertUtils.Utf8StringToBytes(contentType);
                Array.Resize(ref bytes, bytes.Length + 1);
                bytes[bytes.Length - 1] = 0;
                
                _lowLevelStruct.ContentType = LowLevelAPI.UnmanagedMemory.Allocate(bytes.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.ContentType, bytes);
            }

            if (requestedAttributes != null)
            {
                _lowLevelStruct.RequestedAttributes = LowLevelAPI.UnmanagedMemory.Allocate(requestedAttributes.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RequestedAttributes, requestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = (uint)requestedAttributes.Length;
            }

            if (requiredAttributes != null)
            {
                _lowLevelStruct.RequiredAttributes = LowLevelAPI.UnmanagedMemory.Allocate(requiredAttributes.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.RequiredAttributes, requiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = (uint)requiredAttributes.Length;
            }
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.SigningMechanism);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.DigestMechanism);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.ContentType);
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequestedAttributes);
                _lowLevelStruct.RequestedAttributesLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.RequiredAttributes);
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
