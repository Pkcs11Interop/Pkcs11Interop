/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_SKIPJACK_RELAYX mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
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
