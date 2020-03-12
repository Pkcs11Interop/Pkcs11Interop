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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
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
        public NativeULong OldWrappedXLen;

        /// <summary>
        /// Pointer to old wrapper key
        /// </summary>
        public IntPtr OldWrappedX;

        /// <summary>
        /// Length of the old password
        /// </summary>
        public NativeULong OldPasswordLen;

        /// <summary>
        /// Pointer to the buffer which contains the old user-supplied password
        /// </summary>
        public IntPtr OldPassword;

        /// <summary>
        /// Old key exchange public key size
        /// </summary>
        public NativeULong OldPublicDataLen;

        /// <summary>
        /// Pointer to old key exchange public key value
        /// </summary>
        public IntPtr OldPublicData;

        /// <summary>
        /// Size of old random Ra in bytes
        /// </summary>
        public NativeULong OldRandomLen;

        /// <summary>
        /// Pointer to old Ra data
        /// </summary>
        public IntPtr OldRandomA;

        /// <summary>
        /// Length of the new password
        /// </summary>
        public NativeULong NewPasswordLen;

        /// <summary>
        /// Pointer to the buffer which contains the new usersupplied password
        /// </summary>
        public IntPtr NewPassword;

        /// <summary>
        /// New key exchange public key size
        /// </summary>
        public NativeULong NewPublicDataLen;

        /// <summary>
        /// Pointer to new key exchange public key value
        /// </summary>
        public IntPtr NewPublicData;

        /// <summary>
        /// Size of new random Ra in bytes
        /// </summary>
        public NativeULong NewRandomLen;

        /// <summary>
        /// Pointer to new Ra data
        /// </summary>
        public IntPtr NewRandomA;
    }
}
