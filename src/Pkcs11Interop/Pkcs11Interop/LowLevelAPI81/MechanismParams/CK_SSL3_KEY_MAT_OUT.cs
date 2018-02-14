﻿/*
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
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that contains the resulting key handles and initialization vectors after performing a C_DeriveKey function with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SSL3_KEY_MAT_OUT
    {
        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        public ulong ClientMacSecret;
        
        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        public ulong ServerMacSecret;

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        public ulong ClientKey;

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        public ulong ServerKey;

        /// <summary>
        /// Pointer to a location which receives the initialization vector (IV) created for the client (if any)
        /// </summary>
        public IntPtr IVClient;

        /// <summary>
        /// Pointer to a location which receives the initialization vector (IV) created for the server (if any)
        /// </summary>
        public IntPtr IVServer;
    }
}
