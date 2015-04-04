/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using System.Runtime.InteropServices;

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
        public uint CertificateHandle;

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
        public uint RequestedAttributesLen;

        /// <summary>
        /// Pointer to DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes
        /// </summary>
        public IntPtr RequiredAttributes;

        /// <summary>
        /// Length in bytes, of the value pointed to by RequiredAttributes
        /// </summary>
        public uint RequiredAttributesLen;
    }
}
