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
