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
    /// Structure that contains the resulting key handles and initialization vectors after performing a C_DeriveKey function with the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
    /// </summary>
#if SILVERLIGHT
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CK_SSL3_KEY_MAT_OUT
#else
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SSL3_KEY_MAT_OUT
#endif
    {
        /// <summary>
        /// Key handle for the resulting Client MAC Secret key
        /// </summary>
        public ulong ClientMacSecret;
        
        /// <summary>
        /// Key handle for the resulting Server MAC Secret key
        /// </summary>
        public ulong ServerMacSecret;

        /// <summary>
        /// Key handle for the resulting Client Secret key
        /// </summary>
        public ulong ClientKey;

        /// <summary>
        /// Key handle for the resulting Server Secret key
        /// </summary>
        public ulong ServerKey;

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
