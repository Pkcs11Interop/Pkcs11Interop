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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
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
        private CK_OTP_SIGNATURE_INFO _lowLevelStruct = new CK_OTP_SIGNATURE_INFO();

        /// <summary>
        /// Flag indicating whether high level list of OTP parameters left this instance
        /// </summary>
        private bool _paramsLeftInstance = false;

        /// <summary>
        /// List of OTP parameters
        /// </summary>
        private List<CkOtpParam> _params = new List<CkOtpParam>();

        /// <summary>
        /// List of OTP parameters
        /// </summary>
        public IList<CkOtpParam> Params
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                // Since now it is the caller's responsibility to dispose parameters
                _paramsLeftInstance = true;
                return _params.AsReadOnly();
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkOtpSignatureInfo class.
        /// </summary>
        /// <param name='signature'>Signature value returned by all OTP mechanisms in successful calls to Sign method</param>
        public CkOtpSignatureInfo(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // PKCS#11 v2.20a1 page 14:
            // Since C_Sign and C_SignFinal follows the convention described in Section 11.2 of [1]
            // on producing output, a call to C_Sign (or C_SignFinal) with pSignature set to
            // NULL_PTR will return (in the pulSignatureLen parameter) the required number of bytes
            // to hold the CK_OTP_SIGNATURE_INFO structure as well as all the data in all its
            // CK_OTP_PARAM components. If an application allocates a memory block based on this
            // information, it shall therefore not subsequently de-allocate components of such a received
            // value but rather de-allocate the complete CK_OTP_PARAMS structure itself. A
            // Cryptoki library that is called with a non-NULL pSignature pointer will assume that it
            // points to a contiguous memory block of the size indicated by the pulSignatureLen
            // parameter.

            // Create CK_OTP_SIGNATURE_INFO from  C_Sign or C_SignFinal output
            // TODO : This may require different low level delegate with IntPtr as output
            //        but currently I am not aware of any implementation I could test with.
            IntPtr tmpSignature = IntPtr.Zero;
            try
            {
                tmpSignature = UnmanagedMemory.Allocate(signature.Length);
                UnmanagedMemory.Write(tmpSignature, signature);
                UnmanagedMemory.Read(tmpSignature, _lowLevelStruct);
            }
            finally
            {
                UnmanagedMemory.Free(ref tmpSignature);
            }

            // Read all CK_OTP_PARAMs from CK_OTP_SIGNATURE_INFO
            int ckOtpParamSize = UnmanagedMemory.SizeOf(typeof(CK_OTP_PARAM));
            for (int i = 0; i < Convert.ToInt32(_lowLevelStruct.Count); i++)
            {
                // Read CK_OTP_PARAM from CK_OTP_SIGNATURE_INFO
                IntPtr tempPointer = new IntPtr(_lowLevelStruct.Params.ToInt64() + (i * ckOtpParamSize));
                CK_OTP_PARAM ckOtpParam = new CK_OTP_PARAM();
                UnmanagedMemory.Read(tempPointer, ckOtpParam);

                // Read members of CK_OTP_PARAM structure
                ulong ckOtpParamType = ckOtpParam.Type;
                byte[] ckOtpParamValue = UnmanagedMemory.Read(ckOtpParam.Value, Convert.ToInt32(ckOtpParam.ValueLen));

                // Construct new high level CkOtpParam object (creates copy of CK_OTP_PARAM structure which is good)
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
                    if (_paramsLeftInstance == false)
                    {
                        for (int i = 0; i < _params.Count; i++)
                        {
                            if (_params[i] != null)
                            {
                                _params[i].Dispose();
                                _params[i] = null;
                            }
                        }
                    }
                }
                
                // Dispose unmanaged objects

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
