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

namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE and the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanisms
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_WTLS_KEY_MAT_PARAMS
    {
        /// <summary>
        /// The digest mechanism to be used (CKM)
        /// </summary>
        public ulong DigestMechanism;

        /// <summary>
        /// The length (in bits) of the MACing key agreed upon during the protocol handshake phase
        /// </summary>
        public ulong MacSizeInBits;

        /// <summary>
        /// The length (in bits) of the secret key agreed upon during the handshake phase
        /// </summary>
        public ulong KeySizeInBits;

        /// <summary>
        /// The length (in bits) of the IV agreed upon during the handshake phase or if no IV is required, the length should be set to 0
        /// </summary>
        public ulong IVSizeInBits;

        /// <summary>
        /// The current sequence number used for records sent by the client and server respectively
        /// </summary>
        public ulong SequenceNumber;

        /// <summary>
        /// Flag which indicates whether the keys have to be derived for an export version of the protocol
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool IsExport;

        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        public CK_WTLS_RANDOM_DATA RandomInfo;

        /// <summary>
        /// Points to a CK_WTLS_KEY_MAT_OUT structure which receives the handles for the keys generated and the IV
        /// </summary>
        public IntPtr ReturnedKeyMaterial;
    }
}
