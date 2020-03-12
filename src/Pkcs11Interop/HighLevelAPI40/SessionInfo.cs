/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI40;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Information about a session
    /// </summary>
    public class SessionInfo : ISessionInfo
    {
        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        protected NativeULong _sessionId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_sessionId);
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        protected NativeULong _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_slotId);
            }
        }

        /// <summary>
        /// The state of the session
        /// </summary>
        protected CKS _state = 0;

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
        protected SessionFlags _sessionFlags = null;

        /// <summary>
        /// Flags that define the type of session
        /// </summary>
        public ISessionFlags SessionFlags
        {
            get
            {
                return _sessionFlags;
            }
        }

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        protected NativeULong _deviceError = 0;

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        public ulong DeviceError
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_deviceError);
            }
        }

        /// <summary>
        /// Converts low level CK_SESSION_INFO structure to high level SessionInfo class
        /// </summary>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        /// <param name="ck_session_info">Low level CK_SESSION_INFO structure</param>
        protected internal SessionInfo(NativeULong sessionId, CK_SESSION_INFO ck_session_info)
        {
            _sessionId = sessionId;
            _slotId = ck_session_info.SlotId;
            _state = (CKS)ck_session_info.State;
            _sessionFlags = new SessionFlags(ck_session_info.Flags);
            _deviceError = ck_session_info.DeviceError;
        }
    }
}
