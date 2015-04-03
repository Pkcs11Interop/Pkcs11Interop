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
    /// Structure that provides the parameters to the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_RC5_CBC_PARAMS
    {
        /// <summary>
        /// Wordsize of RC5 cipher in bytes
        /// </summary>
        public uint Wordsize;

        /// <summary>
        /// Number of rounds of RC5 encipherment
        /// </summary>
        public uint Rounds;

        /// <summary>
        /// Pointer to initialization vector (IV) for CBC encryption
        /// </summary>
        public IntPtr Iv;

        /// <summary>
        /// Length of initialization vector (must be same as blocksize)
        /// </summary>
        public uint IvLen;
    }
}
