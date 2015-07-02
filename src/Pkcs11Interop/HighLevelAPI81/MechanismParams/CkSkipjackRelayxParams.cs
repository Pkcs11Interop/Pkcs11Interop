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
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SKIPJACK_RELAYX mechanism
    /// </summary>
    public class CkSkipjackRelayxParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_SKIPJACK_RELAYX_PARAMS _lowLevelStruct = new CK_SKIPJACK_RELAYX_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkSkipjackRelayxParams class.
        /// </summary>
        /// <param name='oldWrappedX'>Old wrapper key</param>
        /// <param name='oldPassword'>Old user-supplied password</param>
        /// <param name='oldPublicData'>Old key exchange public key value</param>
        /// <param name='oldRandomA'>Old Ra data</param>
        /// <param name='newPassword'>New user-supplied password</param>
        /// <param name='newPublicData'>New key exchange public key value</param>
        /// <param name='newRandomA'>New Ra data</param>
        public CkSkipjackRelayxParams(byte[] oldWrappedX, byte[] oldPassword, byte[] oldPublicData, byte[] oldRandomA, byte[] newPassword, byte[] newPublicData, byte[] newRandomA)
        {
            _lowLevelStruct.OldWrappedXLen = 0;
            _lowLevelStruct.OldWrappedX = IntPtr.Zero;
            _lowLevelStruct.OldPasswordLen = 0;
            _lowLevelStruct.OldPassword = IntPtr.Zero;
            _lowLevelStruct.OldPublicDataLen = 0;
            _lowLevelStruct.OldPublicData = IntPtr.Zero;
            _lowLevelStruct.OldRandomLen = 0;
            _lowLevelStruct.OldRandomA = IntPtr.Zero;
            _lowLevelStruct.NewPasswordLen = 0;
            _lowLevelStruct.NewPassword = IntPtr.Zero;
            _lowLevelStruct.NewPublicDataLen = 0;
            _lowLevelStruct.NewPublicData = IntPtr.Zero;
            _lowLevelStruct.NewRandomLen = 0;
            _lowLevelStruct.NewRandomA = IntPtr.Zero;

            if (oldWrappedX != null)
            {
                _lowLevelStruct.OldWrappedX = UnmanagedMemory.Allocate(oldWrappedX.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldWrappedX, oldWrappedX);
                _lowLevelStruct.OldWrappedXLen = Convert.ToUInt64(oldWrappedX.Length);
            }

            if (oldPassword != null)
            {
                _lowLevelStruct.OldPassword = UnmanagedMemory.Allocate(oldPassword.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldPassword, oldPassword);
                _lowLevelStruct.OldPasswordLen = Convert.ToUInt64(oldPassword.Length);
            }

            if (oldPublicData != null)
            {
                _lowLevelStruct.OldPublicData = UnmanagedMemory.Allocate(oldPublicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldPublicData, oldPublicData);
                _lowLevelStruct.OldPublicDataLen = Convert.ToUInt64(oldPublicData.Length);
            }

            if (oldRandomA != null)
            {
                _lowLevelStruct.OldRandomA = UnmanagedMemory.Allocate(oldRandomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldRandomA, oldRandomA);
                _lowLevelStruct.OldRandomLen = Convert.ToUInt64(oldRandomA.Length);
            }

            if (newPassword != null)
            {
                _lowLevelStruct.NewPassword = UnmanagedMemory.Allocate(newPassword.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewPassword, newPassword);
                _lowLevelStruct.NewPasswordLen = Convert.ToUInt64(newPassword.Length);
            }

            if (newPublicData != null)
            {
                _lowLevelStruct.NewPublicData = UnmanagedMemory.Allocate(newPublicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewPublicData, newPublicData);
                _lowLevelStruct.NewPublicDataLen = Convert.ToUInt64(newPublicData.Length);
            }

            if (newRandomA != null)
            {
                _lowLevelStruct.NewRandomA = UnmanagedMemory.Allocate(newRandomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewRandomA, newRandomA);
                _lowLevelStruct.NewRandomLen = Convert.ToUInt64(newRandomA.Length);
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
                UnmanagedMemory.Free(ref _lowLevelStruct.OldWrappedX);
                _lowLevelStruct.OldWrappedXLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.OldPassword);
                _lowLevelStruct.OldPasswordLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.OldPublicData);
                _lowLevelStruct.OldPublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.OldRandomA);
                _lowLevelStruct.OldRandomLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.NewPassword);
                _lowLevelStruct.NewPasswordLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.NewPublicData);
                _lowLevelStruct.NewPublicDataLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.NewRandomA);
                _lowLevelStruct.NewRandomLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSkipjackRelayxParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
