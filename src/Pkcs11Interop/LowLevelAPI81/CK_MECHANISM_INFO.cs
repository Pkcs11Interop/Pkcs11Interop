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

using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    /// <summary>
    /// Provides information about a particular mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_MECHANISM_INFO
    {
        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize;

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize;

        /// <summary>
        /// Bit flags specifying mechanism capabilities
        /// </summary>
        public ulong Flags;
    }
}
