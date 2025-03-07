/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Resulting key handles and initialization vectors after performing a DeriveKey method with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    public interface ICkSsl3KeyMatOut : IDisposable
    {
        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        IObjectHandle ClientMacSecret
        {
            get;
        }

        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        IObjectHandle ServerMacSecret
        {
            get;
        }

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        IObjectHandle ClientKey
        {
            get;
        }

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        IObjectHandle ServerKey
        {
            get;
        }

        /// <summary>
        /// Initialization vector (IV) created for the client
        /// </summary>
        byte[] IVClient
        {
            get;
        }

        /// <summary>
        /// Initialization vector (IV) created for the server
        /// </summary>
        byte[] IVServer
        {
            get;
        }
    }
}

