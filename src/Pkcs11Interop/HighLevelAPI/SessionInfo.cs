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

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a session
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// Platform specific SessionInfo
        /// </summary>
        private HighLevelAPI40.SessionInfo _sessionInfo40 = null;

        /// <summary>
        /// Platform specific SessionInfo
        /// </summary>
        private HighLevelAPI41.SessionInfo _sessionInfo41 = null;

        /// <summary>
        /// Platform specific SessionInfo
        /// </summary>
        private HighLevelAPI80.SessionInfo _sessionInfo80 = null;

        /// <summary>
        /// Platform specific SessionInfo
        /// </summary>
        private HighLevelAPI81.SessionInfo _sessionInfo81 = null;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionInfo40.SessionId : _sessionInfo41.SessionId;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionInfo80.SessionId : _sessionInfo81.SessionId;
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        public ulong SlotId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionInfo40.SlotId : _sessionInfo41.SlotId;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionInfo80.SlotId : _sessionInfo81.SlotId;
            }
        }

        /// <summary>
        /// The state of the session
        /// </summary>
        public CKS State
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionInfo40.State : _sessionInfo41.State;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionInfo80.State : _sessionInfo81.State;
            }
        }

        /// <summary>
        /// Flags that define the type of session
        /// </summary>
        private SessionFlags _sessionFlags = null;

        /// <summary>
        /// Flags that define the type of session
        /// </summary>
        public SessionFlags SessionFlags
        {
            get
            {
                if (_sessionFlags == null)
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _sessionFlags = (Platform.StructPackingSize == 0) ? new SessionFlags(_sessionInfo40.SessionFlags) : new SessionFlags(_sessionInfo41.SessionFlags);
                    else
                        _sessionFlags = (Platform.StructPackingSize == 0) ? new SessionFlags(_sessionInfo80.SessionFlags) : new SessionFlags(_sessionInfo81.SessionFlags);
                }

                return _sessionFlags;
            }
        }

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        public ulong DeviceError
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionInfo40.DeviceError : _sessionInfo41.DeviceError;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionInfo80.DeviceError : _sessionInfo81.DeviceError;
            }
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI40.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo40 = sessionInfo;
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI41.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo41 = sessionInfo;
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI80.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo80 = sessionInfo;
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI81.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo81 = sessionInfo;
        }
    }
}
