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
    /// Structure that provides the parameters to the CKM_RSA_PKCS_OAEP mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_RSA_PKCS_OAEP_PARAMS
    {
        /// <summary>
        /// Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)
        /// </summary>
        public NativeULong HashAlg;

        /// <summary>
        /// Mask generation function to use on the encoded block (CKG)
        /// </summary>
        public NativeULong Mgf;
        
        /// <summary>
        /// Source of the encoding parameter (CKZ)
        /// </summary>
        public NativeULong Source;
        
        /// <summary>
        /// Data used as the input for the encoding parameter source
        /// </summary>
        public IntPtr SourceData;
        
        /// <summary>
        /// Length of the encoding parameter source input
        /// </summary>
        public NativeULong SourceDataLen;
    }
}
