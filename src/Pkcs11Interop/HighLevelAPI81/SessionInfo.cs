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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Information about a session
    /// </summary>
    public class SessionInfo
    {   
        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        private ulong _sessionId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                return _sessionId;
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        private ulong _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return _slotId;
            }
        }

        /// <summary>
        /// The state of the session
        /// </summary>
        private CKS _state = 0;

        /// <summary>
        /// The state of the session
        /// </summary>
        public CKS State
        {
            get
            {
                return _state;
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
                return _sessionFlags;
            }
        }

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        private ulong _deviceError = 0;

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        public ulong DeviceError
        {
            get
            {
                return _deviceError;
            }
        }

        /// <summary>
        /// Converts low level CK_SESSION_INFO structure to high level SessionInfo class
        /// </summary>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        /// <param name="ck_session_info">Low level CK_SESSION_INFO structure</param>
        internal SessionInfo(ulong sessionId, CK_SESSION_INFO ck_session_info)
        {
            _sessionId = sessionId;
            _slotId = ck_session_info.SlotId;
            _state = (CKS)ck_session_info.State;
            _sessionFlags = new SessionFlags(ck_session_info.Flags);
            _deviceError = ck_session_info.DeviceError;
        }
    }
}
