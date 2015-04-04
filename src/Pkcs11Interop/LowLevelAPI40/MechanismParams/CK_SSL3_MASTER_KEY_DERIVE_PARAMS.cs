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
    /// Structure that provides the parameters to the CKM_SSL3_MASTER_KEY_DERIVE and CKM_SSL3_MASTER_KEY_DERIVE_DH mechanisms
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_SSL3_MASTER_KEY_DERIVE_PARAMS
    {
        /// <summary>
        /// Client's and server's random data information
        /// </summary>
        public CK_SSL3_RANDOM_DATA RandomInfo;

        /// <summary>
        /// Pointer to a CK_VERSION structure which receives the SSL protocol version information
        /// </summary>
        public IntPtr Version;
    }
}
