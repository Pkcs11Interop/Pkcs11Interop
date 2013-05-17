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
        private LowLevelAPI.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS();
        
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
                _lowLevelStruct.OldWrappedX = LowLevelAPI.UnmanagedMemory.Allocate(oldWrappedX.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.OldWrappedX, oldWrappedX);
                _lowLevelStruct.OldWrappedXLen = (uint)oldWrappedX.Length;
            }

            if (oldPassword != null)
            {
                _lowLevelStruct.OldPassword = LowLevelAPI.UnmanagedMemory.Allocate(oldPassword.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.OldPassword, oldPassword);
                _lowLevelStruct.OldPasswordLen = (uint)oldPassword.Length;
            }

            if (oldPublicData != null)
            {
                _lowLevelStruct.OldPublicData = LowLevelAPI.UnmanagedMemory.Allocate(oldPublicData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.OldPublicData, oldPublicData);
                _lowLevelStruct.OldPublicDataLen = (uint)oldPublicData.Length;
            }

            if (oldRandomA != null)
            {
                _lowLevelStruct.OldRandomA = LowLevelAPI.UnmanagedMemory.Allocate(oldRandomA.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.OldRandomA, oldRandomA);
                _lowLevelStruct.OldRandomLen = (uint)oldRandomA.Length;
            }

            if (newPassword != null)
            {
                _lowLevelStruct.NewPassword = LowLevelAPI.UnmanagedMemory.Allocate(newPassword.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.NewPassword, newPassword);
                _lowLevelStruct.NewPasswordLen = (uint)newPassword.Length;
            }

            if (newPublicData != null)
            {
                _lowLevelStruct.NewPublicData = LowLevelAPI.UnmanagedMemory.Allocate(newPublicData.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.NewPublicData, newPublicData);
                _lowLevelStruct.NewPublicDataLen = (uint)newPublicData.Length;
            }

            if (newRandomA != null)
            {
                _lowLevelStruct.NewRandomA = LowLevelAPI.UnmanagedMemory.Allocate(newRandomA.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.NewRandomA, newRandomA);
                _lowLevelStruct.NewRandomLen = (uint)newRandomA.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.OldWrappedX);
                _lowLevelStruct.OldWrappedXLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.OldPassword);
                _lowLevelStruct.OldPasswordLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.OldPublicData);
                _lowLevelStruct.OldPublicDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.OldRandomA);
                _lowLevelStruct.OldRandomLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.NewPassword);
                _lowLevelStruct.NewPasswordLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.NewPublicData);
                _lowLevelStruct.NewPublicDataLen = 0;
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.NewRandomA);
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
