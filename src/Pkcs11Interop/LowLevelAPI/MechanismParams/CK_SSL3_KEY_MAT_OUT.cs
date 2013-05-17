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
    /// Structure that contains the resulting key handles and initialization vectors after performing a C_DeriveKey function with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SSL3_KEY_MAT_OUT
    {
        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        public uint ClientMacSecret;
        
        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        public uint ServerMacSecret;

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        public uint ClientKey;

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        public uint ServerKey;

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
