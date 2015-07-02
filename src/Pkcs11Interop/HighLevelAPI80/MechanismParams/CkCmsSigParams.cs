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
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
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
        public CkCmsSigParams(ObjectHandle certificateHandle, ulong? signingMechanism, ulong? digestMechanism, string contentType, byte[] requestedAttributes, byte[] requiredAttributes)
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
                byte[] bytes = ConvertUtils.ULongToBytes(signingMechanism.Value);
                _lowLevelStruct.SigningMechanism = UnmanagedMemory.Allocate(bytes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.SigningMechanism, bytes);
            }

            if (digestMechanism != null)
            {
                byte[] bytes = ConvertUtils.ULongToBytes(digestMechanism.Value);
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
                _lowLevelStruct.RequestedAttributesLen = Convert.ToUInt64(requestedAttributes.Length);
            }

            if (requiredAttributes != null)
            {
                _lowLevelStruct.RequiredAttributes = UnmanagedMemory.Allocate(requiredAttributes.Length);
                UnmanagedMemory.Write(_lowLevelStruct.RequiredAttributes, requiredAttributes);
                _lowLevelStruct.RequiredAttributesLen = Convert.ToUInt64(requiredAttributes.Length);
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
