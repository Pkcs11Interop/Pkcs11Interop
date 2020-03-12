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
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Corrected structure that provides the parameters to the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_PKCS5_PBKD2_PARAMS2
    {
        /// <summary>
        /// Source of the salt value (CKZ)
        /// </summary>
        public NativeULong SaltSource;

        /// <summary>
        /// Data used as the input for the salt source
        /// </summary>
        public IntPtr SaltSourceData;

        /// <summary>
        /// Length of the salt source input
        /// </summary>
        public NativeULong SaltSourceDataLen;

        /// <summary>
        /// Number of iterations to perform when generating each block of random data
        /// </summary>
        public NativeULong Iterations;

        /// <summary>
        /// Pseudo-random function to used to generate the key (CKP)
        /// </summary>
        public NativeULong Prf;

        /// <summary>
        /// Data used as the input for PRF in addition to the salt value
        /// </summary>
        public IntPtr PrfData;

        /// <summary>
        /// Length of the input data for the PRF
        /// </summary>
        public NativeULong PrfDataLen;

        /// <summary>
        /// Points to the password to be used in the PBE key generation
        /// </summary>
        public IntPtr Password;

        /// <summary>
        /// Length in bytes of the password information
        /// </summary>
        public NativeULong PasswordLen;
    }
}
