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
    /// Type, value and length of an OTP parameter
    /// </summary>
    public class CkOtpParam : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_OTP_PARAM _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_OTP_PARAM();

        /// <summary>
        /// Parameter type
        /// </summary>
        public uint Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _lowLevelStruct.Type;
            }
        }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public byte[] Value
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_lowLevelStruct.Value == IntPtr.Zero) ? null : LowLevelAPI.UnmanagedMemory.Read(_lowLevelStruct.Value, (int)_lowLevelStruct.ValueLen);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkOtpParam class.
        /// </summary>
        /// <param name='type'>Parameter type</param>
        /// <param name='value'>Value of the parameter</param>
        public CkOtpParam(uint type, byte[] value)
        {
            _lowLevelStruct.Type = 0;
            _lowLevelStruct.Value = IntPtr.Zero;
            _lowLevelStruct.ValueLen = 0;

            _lowLevelStruct.Type = type;

            if (value != null)
            {
                _lowLevelStruct.Value = LowLevelAPI.UnmanagedMemory.Allocate(value.Length);
                LowLevelAPI.UnmanagedMemory.Write(_lowLevelStruct.Value, value);
                _lowLevelStruct.ValueLen = (uint)value.Length;
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Value);
                _lowLevelStruct.ValueLen = 0;

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkOtpParam()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
