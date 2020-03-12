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

using System;
using System.Runtime.InteropServices;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_SKIPJACK_PRIVATE_WRAP mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SKIPJACK_PRIVATE_WRAP_PARAMS
    {
        /// <summary>
        /// Length of the password
        /// </summary>
        public NativeULong PasswordLen;
        
        /// <summary>
        /// Pointer to the buffer which contains the user-supplied password
        /// </summary>
        public IntPtr Password;

        /// <summary>
        /// Other party's key exchange public key size
        /// </summary>
        public NativeULong PublicDataLen;

        /// <summary>
        /// Pointer to other party's key exchange public key value
        /// </summary>
        public IntPtr PublicData;
        
        /// <summary>
        /// Length of prime and base values
        /// </summary>
        public NativeULong PAndGLen;

        /// <summary>
        /// Length of subprime value
        /// </summary>
        public NativeULong QLen;

        /// <summary>
        /// Size of random Ra, in bytes
        /// </summary>
        public NativeULong RandomLen;

        /// <summary>
        /// Pointer to Ra data
        /// </summary>
        public IntPtr RandomA;

        /// <summary>
        /// Pointer to Prime, p, value
        /// </summary>
        public IntPtr PrimeP;

        /// <summary>
        /// Pointer to Base, g, value
        /// </summary>
        public IntPtr BaseG;

        /// <summary>
        /// Pointer to Subprime, q, value
        /// </summary>
        public IntPtr SubprimeQ;
    }
}
