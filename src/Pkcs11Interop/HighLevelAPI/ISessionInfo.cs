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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a session
    /// </summary>
    public interface ISessionInfo
    {
        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        ulong SessionId
        {
            get;
        }

        /// <summary>
        /// PKCS#11 handle of slot that interfaces with the token
        /// </summary>
        ulong SlotId
        {
            get;
        }

        /// <summary>
        /// The state of the session
        /// </summary>
        CKS State
        {
            get;
        }

        /// <summary>
        /// Flags that define the type of session
        /// </summary>
        ISessionFlags SessionFlags
        {
            get;
        }

        /// <summary>
        /// An error code defined by the cryptographic device used for errors not covered by Cryptoki
        /// </summary>
        ulong DeviceError
        {
            get;
        }
    }
}
