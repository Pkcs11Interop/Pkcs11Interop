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
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Flags that define the type of session
    /// </summary>
    public class SessionFlags : ISessionFlags
    {
        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        protected NativeULong _flags;

        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        public ulong Flags
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_flags);
            }
        }

        /// <summary>
        /// True if the session is read/write; false if the session is read-only
        /// </summary>
        public bool RwSession
        {
            get
            {
                return ((_flags & CKF.CKF_RW_SESSION) == CKF.CKF_RW_SESSION);
            }
        }

        /// <summary>
        /// This flag is provided for backward compatibility, and should always be set to true
        /// </summary>
        public bool SerialSession
        {
            get
            {
                return ((_flags & CKF.CKF_SERIAL_SESSION) == CKF.CKF_SERIAL_SESSION);
            }
        }

        /// <summary>
        /// Initializes new instance of SessionFlags class
        /// </summary>
        /// <param name="flags">Bit flags that define the type of session</param>
        protected internal SessionFlags(NativeULong flags)
        {
            _flags = flags;
        }
    }
}
