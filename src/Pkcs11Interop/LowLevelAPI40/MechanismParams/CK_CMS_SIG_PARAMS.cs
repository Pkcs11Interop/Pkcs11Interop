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

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI40.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_CMS_SIG mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_CMS_SIG_PARAMS
    {
        /// <summary>
        /// Object handle for a certificate associated with the signing key
        /// </summary>
        public NativeULong CertificateHandle;

        /// <summary>
        /// Mechanism to use when signing a constructed CMS SignedAttributes value
        /// </summary>
        public IntPtr SigningMechanism;

        /// <summary>
        /// Mechanism to use when digesting the data
        /// </summary>
        public IntPtr DigestMechanism;

        /// <summary>
        /// NULL-terminated string indicating complete MIME Content-type of message to be signed or null if the message is a MIME object
        /// </summary>
        public IntPtr ContentType;

        /// <summary>
        /// Pointer to DER-encoded list of CMS Attributes the caller requests to be included in the signed attributes
        /// </summary>
        public IntPtr RequestedAttributes;

        /// <summary>
        /// Length in bytes of the value pointed to by RequestedAttributes
        /// </summary>
        public NativeULong RequestedAttributesLen;

        /// <summary>
        /// Pointer to DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes
        /// </summary>
        public IntPtr RequiredAttributes;

        /// <summary>
        /// Length in bytes, of the value pointed to by RequiredAttributes
        /// </summary>
        public NativeULong RequiredAttributesLen;
    }
}
