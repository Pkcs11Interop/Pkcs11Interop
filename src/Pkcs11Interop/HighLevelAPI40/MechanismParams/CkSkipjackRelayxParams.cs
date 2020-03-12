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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SKIPJACK_RELAYX mechanism
    /// </summary>
    public class CkSkipjackRelayxParams : ICkSkipjackRelayxParams
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
                _lowLevelStruct.OldWrappedXLen = ConvertUtils.UInt32FromInt32(oldWrappedX.Length);
            }

            if (oldPassword != null)
            {
                _lowLevelStruct.OldPassword = UnmanagedMemory.Allocate(oldPassword.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldPassword, oldPassword);
                _lowLevelStruct.OldPasswordLen = ConvertUtils.UInt32FromInt32(oldPassword.Length);
            }

            if (oldPublicData != null)
            {
                _lowLevelStruct.OldPublicData = UnmanagedMemory.Allocate(oldPublicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldPublicData, oldPublicData);
                _lowLevelStruct.OldPublicDataLen = ConvertUtils.UInt32FromInt32(oldPublicData.Length);
            }

            if (oldRandomA != null)
            {
                _lowLevelStruct.OldRandomA = UnmanagedMemory.Allocate(oldRandomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.OldRandomA, oldRandomA);
                _lowLevelStruct.OldRandomLen = ConvertUtils.UInt32FromInt32(oldRandomA.Length);
            }

            if (newPassword != null)
            {
                _lowLevelStruct.NewPassword = UnmanagedMemory.Allocate(newPassword.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewPassword, newPassword);
                _lowLevelStruct.NewPasswordLen = ConvertUtils.UInt32FromInt32(newPassword.Length);
            }

            if (newPublicData != null)
            {
                _lowLevelStruct.NewPublicData = UnmanagedMemory.Allocate(newPublicData.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewPublicData, newPublicData);
                _lowLevelStruct.NewPublicDataLen = ConvertUtils.UInt32FromInt32(newPublicData.Length);
            }

            if (newRandomA != null)
            {
                _lowLevelStruct.NewRandomA = UnmanagedMemory.Allocate(newRandomA.Length);
                UnmanagedMemory.Write(_lowLevelStruct.NewRandomA, newRandomA);
                _lowLevelStruct.NewRandomLen = ConvertUtils.UInt32FromInt32(newRandomA.Length);
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
