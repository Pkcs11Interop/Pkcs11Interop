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
        /// Platform specific CkOtpParam
        /// </summary>
        internal HighLevelAPI41.MechanismParams.CkOtpParam _params4 = null;

        /// <summary>
        /// Platform specific CkOtpParam
        /// </summary>
        internal HighLevelAPI81.MechanismParams.CkOtpParam _params8 = null;

        /// <summary>
        /// Parameter type
        /// </summary>
        public ulong Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (Platform.UnmanagedLongSize == 4) ? _params4.Type : _params8.Type;
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

                return (Platform.UnmanagedLongSize == 4) ? _params4.Value : _params8.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkOtpParam class.
        /// </summary>
        /// <param name='type'>Parameter type</param>
        /// <param name='value'>Value of the parameter</param>
        public CkOtpParam(ulong type, byte[] value)
        {
            if (Platform.UnmanagedLongSize == 4)
                _params4 = new HighLevelAPI41.MechanismParams.CkOtpParam(Convert.ToUInt32(type), value);
            else
                _params8 = new HighLevelAPI81.MechanismParams.CkOtpParam(type, value);
        }

        /// <summary>
        /// Initializes a new instance of the CkOtpParam class.
        /// </summary>
        /// <param name='ckOtpParam'>Platform specific CkOtpParam</param>
        internal CkOtpParam(HighLevelAPI41.MechanismParams.CkOtpParam ckOtpParam)
        {
            if (ckOtpParam == null)
                throw new ArgumentNullException("ckOtpParam");
            
            _params4 = ckOtpParam;
        }

        /// <summary>
        /// Initializes a new instance of the CkOtpParam class.
        /// </summary>
        /// <param name='ckOtpParam'>Platform specific CkOtpParam</param>
        internal CkOtpParam(HighLevelAPI81.MechanismParams.CkOtpParam ckOtpParam)
        {
            if (ckOtpParam == null)
                throw new ArgumentNullException("ckOtpParam");
            
            _params8 = ckOtpParam;
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

            if (Platform.UnmanagedLongSize == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
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
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects

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
