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

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Specifies a particular mechanism and any parameters it requires
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_MECHANISM
    {
        /// <summary>
        /// The type of mechanism
        /// </summary>
        public uint Mechanism;

        /// <summary>
        /// Pointer to the parameter if required by the mechanism
        /// </summary>
        public IntPtr Parameter;

        /// <summary>
        /// Length of the parameter in bytes
        /// </summary>
        public uint ParameterLen;
    }
}
