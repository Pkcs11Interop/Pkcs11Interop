/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI41;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.HighLevelAPI41
{
    /// <summary>
    /// Information about a session
    /// </summary>
    public class SessionInfo
    {   
        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        private NativeULong _sessionId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public NativeULong SessionId
        {
            get
            {
                return _sessionId;
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        private NativeULong _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        public NativeULong SlotId
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
        private NativeULong _deviceError = 0;

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        public NativeULong DeviceError
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
        internal SessionInfo(NativeULong sessionId, CK_SESSION_INFO ck_session_info)
        {
            _sessionId = sessionId;
            _slotId = ck_session_info.SlotId;
            _state = (CKS)ck_session_info.State;
            _sessionFlags = new SessionFlags(ck_session_info.Flags);
            _deviceError = ck_session_info.DeviceError;
        }
    }
}
