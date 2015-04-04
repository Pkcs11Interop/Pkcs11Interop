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

namespace Net.Pkcs11Interop.LowLevelAPI80.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_KEY_WRAP_SET_OAEP mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_KEY_WRAP_SET_OAEP_PARAMS
    {
        /// <summary>
        /// Block contents byte
        /// </summary>
        public byte BC;

        /// <summary>
        /// Concatenation of hash of plaintext data (if present) and extra data (if present)
        /// </summary>
        public IntPtr X;
        
        /// <summary>
        /// Length in bytes of concatenation of hash of plaintext data (if present) and extra data (if present) or 0 if neither is present
        /// </summary>
        public ulong XLen;
    }
}
