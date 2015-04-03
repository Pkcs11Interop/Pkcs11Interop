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

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Structure which provides information about the random data of a client and a server in an SSL context
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SSL3_RANDOM_DATA
    {
        /// <summary>
        /// Pointer to the client's random data
        /// </summary>
        public IntPtr ClientRandom;

        /// <summary>
        /// Length in bytes of the client's random data
        /// </summary>
        public uint ClientRandomLen;

        /// <summary>
        /// Pointer to the server's random data
        /// </summary>
        public IntPtr ServerRandom;

        /// <summary>
        /// Length in bytes of the server's random data
        /// </summary>
        public uint ServerRandomLen;
    }
}
