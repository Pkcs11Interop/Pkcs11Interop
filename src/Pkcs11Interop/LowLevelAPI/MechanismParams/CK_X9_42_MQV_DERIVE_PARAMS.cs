/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_X9_42_MQV_DERIVE key derivation mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_X9_42_MQV_DERIVE_PARAMS
    {
        /// <summary>
        /// Key derivation function used on the shared secret value (CKD)
        /// </summary>
        public uint Kdf;

        /// <summary>
        /// The length in bytes of the other info
        /// </summary>
        public uint OtherInfoLen;

        /// <summary>
        /// Some data shared between the two parties
        /// </summary>
        public IntPtr OtherInfo;

        /// <summary>
        /// The length in bytes of the other party's first X9.42 Diffie-Hellman public key
        /// </summary>
        public uint PublicDataLen;

        /// <summary>
        /// Pointer to other party's first X9.42 Diffie-Hellman public key value
        /// </summary>
        public IntPtr PublicData;

        /// <summary>
        /// The length in bytes of the second X9.42 Diffie-Hellman private key
        /// </summary>
        public uint PrivateDataLen;

        /// <summary>
        /// Key handle for second X9.42 Diffie-Hellman private key value
        /// </summary>
        public uint PrivateData;

        /// <summary>
        /// The length in bytes of the other party's second X9.42 Diffie-Hellman public key
        /// </summary>
        public uint PublicDataLen2;

        /// <summary>
        /// Pointer to other party's second X9.42 Diffie-Hellman public key value
        /// </summary>
        public IntPtr PublicData2;

        /// <summary>
        /// Handle to the first party's ephemeral public key
        /// </summary>
        public uint PublicKey;
    }
}
