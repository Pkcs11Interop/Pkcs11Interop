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
    /// Structure that provides the parameters to the CKM_KEA_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_KEA_DERIVE_PARAMS
    {
        /// <summary>
        /// Option for generating the key (called a TEK). True if the sender (originator) generates the TEK, false if the recipient is regenerating the TEK.
        /// </summary>
        public bool IsSender;

        /// <summary>
        /// Size of random Ra and Rb, in bytes
        /// </summary>
        public uint RandomLen;

        /// <summary>
        /// Pointer to Ra data
        /// </summary>
        public IntPtr RandomA;

        /// <summary>
        /// Pointer to Rb data
        /// </summary>
        public IntPtr RandomB;

        /// <summary>
        /// Other party's KEA public key size
        /// </summary>
        public uint PublicDataLen;

        /// <summary>
        /// Pointer to other party's KEA public key value
        /// </summary>
        public IntPtr PublicData;
    }
}
