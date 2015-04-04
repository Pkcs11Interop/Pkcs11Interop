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
    /// Structure that provides the parameters to the CKM_SKIPJACK_RELAYX mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_SKIPJACK_RELAYX_PARAMS
    {
        /// <summary>
        /// Length of old wrapped key in bytes
        /// </summary>
        public uint OldWrappedXLen;

        /// <summary>
        /// Pointer to old wrapper key
        /// </summary>
        public IntPtr OldWrappedX;

        /// <summary>
        /// Length of the old password
        /// </summary>
        public uint OldPasswordLen;

        /// <summary>
        /// Pointer to the buffer which contains the old user-supplied password
        /// </summary>
        public IntPtr OldPassword;

        /// <summary>
        /// Old key exchange public key size
        /// </summary>
        public uint OldPublicDataLen;

        /// <summary>
        /// Pointer to old key exchange public key value
        /// </summary>
        public IntPtr OldPublicData;

        /// <summary>
        /// Size of old random Ra in bytes
        /// </summary>
        public uint OldRandomLen;

        /// <summary>
        /// Pointer to old Ra data
        /// </summary>
        public IntPtr OldRandomA;

        /// <summary>
        /// Length of the new password
        /// </summary>
        public uint NewPasswordLen;

        /// <summary>
        /// Pointer to the buffer which contains the new usersupplied password
        /// </summary>
        public IntPtr NewPassword;

        /// <summary>
        /// New key exchange public key size
        /// </summary>
        public uint NewPublicDataLen;

        /// <summary>
        /// Pointer to new key exchange public key value
        /// </summary>
        public IntPtr NewPublicData;

        /// <summary>
        /// Size of new random Ra in bytes
        /// </summary>
        public uint NewRandomLen;

        /// <summary>
        /// Pointer to new Ra data
        /// </summary>
        public IntPtr NewRandomA;
    }
}
