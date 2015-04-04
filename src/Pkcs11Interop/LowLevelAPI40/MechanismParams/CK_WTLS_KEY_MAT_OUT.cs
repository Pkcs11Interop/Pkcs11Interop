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
    /// Structure that contains the resulting key handles and initialization vectors after performing a C_DeriveKey function with the CKM_WTLS_SEVER_KEY_AND_MAC_DERIVE or with the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
#if SILVERLIGHT
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public class CK_WTLS_KEY_MAT_OUT
#else
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_WTLS_KEY_MAT_OUT
#endif
    {
        /// <summary>
        /// Key handle for the resulting MAC secret key
        /// </summary>
        public uint MacSecret;

        /// <summary>
        /// Key handle for the resulting secret key
        /// </summary>
        public uint Key;

        /// <summary>
        /// Pointer to a location which receives the initialization vector (IV) created (if any)
        /// </summary>
        public IntPtr IV;
    }
}
