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
using System.Collections.Generic;
using Net.Pkcs11Interop.LowLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for OTP mechanisms in a generic fashion
    /// </summary>
    public class CkOtpParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_OTP_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_OTP_PARAMS();

        /// <summary>
        /// List of OTP parameters
        /// </summary>
        private List<CkOtpParam> _parameters = null;

        /// <summary>
        /// Initializes a new instance of the CkOtpParams class.
        /// </summary>
        /// <param name='parameters'>List of OTP parameters</param>
        public CkOtpParams(List<CkOtpParam> parameters)
        {
            _lowLevelStruct.Params = IntPtr.Zero;
            _lowLevelStruct.Count = 0;

            if (parameters.Count > 0)
            {
                // Keep reference to parameters so GC will not free them while this object exists
                _parameters = parameters;

                // Allocate memory for parameters
                int ckOtpParamSize = UnmanagedMemory.SizeOf(typeof(CK_OTP_PARAM));
                _lowLevelStruct.Params = UnmanagedMemory.Allocate(ckOtpParamSize * _parameters.Count);
                _lowLevelStruct.Count = (uint)_parameters.Count;

                // Copy paramaters to allocated memory
                for (int i = 0; i < _parameters.Count; i++)
                {
                    IntPtr tempPointer = new IntPtr(_lowLevelStruct.Params.ToInt32() + (i * ckOtpParamSize));
                    UnmanagedMemory.Write(tempPointer, _parameters[i].ToLowLevelParams());
                }
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
                LowLevelAPI.UnmanagedMemory.Free(ref _lowLevelStruct.Params);
                _lowLevelStruct.Count = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkOtpParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
