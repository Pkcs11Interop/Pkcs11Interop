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
        private HighLevelAPI4.SessionInfo _sessionInfo4 = null;

        /// <summary>
        /// Platform specific SessionInfo
        /// </summary>
        private HighLevelAPI8.SessionInfo _sessionInfo8 = null;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _sessionInfo4.SessionId : _sessionInfo8.SessionId;
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _sessionInfo4.SlotId : _sessionInfo8.SlotId;
            }
        }

        /// <summary>
        /// The state of the session
        /// </summary>
        public CKS State
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _sessionInfo4.State : _sessionInfo8.State;
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
                    _sessionFlags = (Platform.UnmanagedLongSize == 4) ? new SessionFlags(_sessionInfo4.SessionFlags) : new SessionFlags(_sessionInfo8.SessionFlags);

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
                return (Platform.UnmanagedLongSize == 4) ? _sessionInfo4.DeviceError : _sessionInfo8.DeviceError;
            }
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI4.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo4 = sessionInfo;
        }

        /// <summary>
        /// Converts platform specific SessionInfo to platfrom neutral SessionInfo
        /// </summary>
        /// <param name="sessionInfo">Platform specific SessionInfo</param>
        internal SessionInfo(HighLevelAPI8.SessionInfo sessionInfo)
        {
            if (sessionInfo == null)
                throw new ArgumentNullException("sessionInfo");

            _sessionInfo8 = sessionInfo;
        }
    }
}
