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
    /// Structure which provides all of the necessary information required by the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_PBE_PARAMS
    {
        /// <summary>
        /// Pointer to the location that receives the 8-byte initialization vector (IV), if an IV is required
        /// </summary>
        public IntPtr InitVector;

        /// <summary>
        /// Points to the password to be used in the PBE key generation
        /// </summary>
        public IntPtr Password;

        /// <summary>
        /// Length in bytes of the password information
        /// </summary>
        public ulong PasswordLen;

        /// <summary>
        /// Points to the salt to be used in the PBE key generation
        /// </summary>
        public IntPtr Salt;

        /// <summary>
        /// Length in bytes of the salt information
        /// </summary>
        public ulong SaltLen;

        /// <summary>
        /// Number of iterations required for the generation
        /// </summary>
        public ulong Iteration;
    }
}
