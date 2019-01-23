/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
    /// Flags that define the type of session
    /// </summary>
    public class SessionFlags
    {
        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI40.SessionFlags _sessionFlags40 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI41.SessionFlags _sessionFlags41 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI80.SessionFlags _sessionFlags80 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI81.SessionFlags _sessionFlags81 = null;

        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.Flags : _sessionFlags41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.Flags : _sessionFlags81.Flags;
            }
        }

        /// <summary>
        /// True if the session is read/write; false if the session is read-only
        /// </summary>
        public bool RwSession
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.RwSession : _sessionFlags41.RwSession;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.RwSession : _sessionFlags81.RwSession;
            }
        }

        /// <summary>
        /// This flag is provided for backward compatibility, and should always be set to true
        /// </summary>
        public bool SerialSession
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.SerialSession : _sessionFlags41.SerialSession;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.SerialSession : _sessionFlags81.SerialSession;
            }
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI40.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags40 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI41.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags41 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI80.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags80 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI81.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags81 = sessionFlags;
        }
    }
}
