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
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters returned by all OTP mechanisms in successful calls to Sign method
    /// </summary>
    public class CkOtpSignatureInfo : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_OTP_SIGNATURE_INFO _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_OTP_SIGNATURE_INFO();

        /// <summary>
        /// List of OTP parameters
        /// </summary>
        private List<CkOtpParam> _params = null;

        /// <summary>
        /// List of OTP parameters
        /// </summary>
        public IList<CkOtpParam> Params
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (_params == null) ? null : _params.AsReadOnly();
            }
        }

        /// <summary>
        /// Unmanaged memory with CK_OTP_SIGNATURE_INFO structure
        /// </summary>
        private IntPtr _signature = IntPtr.Zero;
        
        /// <summary>
        /// Initializes a new instance of the CkOtpSignatureInfo class.
        /// </summary>
        /// <param name='signature'>Signature value returned by all OTP mechanisms in successful calls to Sign method</param>
        public CkOtpSignatureInfo(byte[] signature)
        {
            if (signature == null)
                return;

            int ckOtpParamSize = UnmanagedMemory.SizeOf(typeof(CK_OTP_PARAM));

            if (signature.Length % ckOtpParamSize != 0)
                throw new ArgumentException("Invalid array length", "signature");

            // Copy signature to unmanaged memory
            _signature = UnmanagedMemory.Allocate(signature.Length);
            UnmanagedMemory.Write(_signature, signature);

            // Read CK_OTP_SIGNATURE_INFO from unmanaged memory
            UnmanagedMemory.Read(_signature, _lowLevelStruct);

            for (int i = 0; i < _lowLevelStruct.Count; i++)
            {
                // Read CK_OTP_PARAM from CK_OTP_SIGNATURE_INFO
                IntPtr tempPointer = new IntPtr(_lowLevelStruct.Params.ToInt32() + (i * ckOtpParamSize));
                CK_OTP_PARAM ckOtpParam = new CK_OTP_PARAM();
                UnmanagedMemory.Read(tempPointer, ckOtpParam);

                // Read members of CK_OTP_PARAM structure
                uint ckOtpParamType = ckOtpParam.Type;
                byte[] ckOtpParamValue = UnmanagedMemory.Read(ckOtpParam.Value, (int)ckOtpParam.ValueLen);

                // Construct high level CkOtpParam class
                _params.Add(new CkOtpParam(ckOtpParamType, ckOtpParamValue));
            }
        }

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
                UnmanagedMemory.Free(ref _signature);

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkOtpSignatureInfo()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
