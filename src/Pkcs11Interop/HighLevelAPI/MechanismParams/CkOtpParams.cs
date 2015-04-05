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
        /// Platform specific CkOtpParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkOtpParams _params40 = null;

        /// <summary>
        /// Platform specific CkOtpParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkOtpParams _params41 = null;

        /// <summary>
        /// Platform specific CkOtpParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkOtpParams _params80 = null;

        /// <summary>
        /// Platform specific CkOtpParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkOtpParams _params81 = null;

        /// <summary>
        /// Initializes a new instance of the CkOtpParams class.
        /// </summary>
        /// <param name='parameters'>List of OTP parameters</param>
        public CkOtpParams(List<CkOtpParam> parameters)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.MechanismParams.CkOtpParam> hlaParameters = new List<HighLevelAPI40.MechanismParams.CkOtpParam>();

                    if ((parameters != null) && (parameters.Count > 0))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                            hlaParameters.Add(parameters[i]._params40);
                    }

                    _params40 = new HighLevelAPI40.MechanismParams.CkOtpParams(hlaParameters);
                }
                else
                {
                    List<HighLevelAPI41.MechanismParams.CkOtpParam> hlaParameters = new List<HighLevelAPI41.MechanismParams.CkOtpParam>();

                    if ((parameters != null) && (parameters.Count > 0))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                            hlaParameters.Add(parameters[i]._params41);
                    }

                    _params41 = new HighLevelAPI41.MechanismParams.CkOtpParams(hlaParameters);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.MechanismParams.CkOtpParam> hlaParameters = new List<HighLevelAPI80.MechanismParams.CkOtpParam>();

                    if ((parameters != null) && (parameters.Count > 0))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                            hlaParameters.Add(parameters[i]._params80);
                    }

                    _params80 = new HighLevelAPI80.MechanismParams.CkOtpParams(hlaParameters);
                }
                else
                {
                    List<HighLevelAPI81.MechanismParams.CkOtpParam> hlaParameters = new List<HighLevelAPI81.MechanismParams.CkOtpParam>();

                    if ((parameters != null) && (parameters.Count > 0))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                            hlaParameters.Add(parameters[i]._params81);
                    }

                    _params81 = new HighLevelAPI81.MechanismParams.CkOtpParams(hlaParameters);
                }
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

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _params40.ToMarshalableStructure() : _params41.ToMarshalableStructure();
            else
                return (Platform.StructPackingSize == 0) ? _params80.ToMarshalableStructure() : _params81.ToMarshalableStructure();
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
                    if (_params40 != null)
                    {
                        _params40.Dispose();
                        _params40 = null;
                    }

                    if (_params41 != null)
                    {
                        _params41.Dispose();
                        _params41 = null;
                    }

                    if (_params80 != null)
                    {
                        _params80.Dispose();
                        _params80 = null;
                    }

                    if (_params81 != null)
                    {
                        _params81.Dispose();
                        _params81 = null;
                    }
                }
                
                // Dispose unmanaged objects
                
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
