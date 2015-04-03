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
    /// Structure, which provides the parameters to the CKM_TLS_PRF mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_TLS_PRF_PARAMS
    {
        /// <summary>
        /// Pointer to the input seed
        /// </summary>
        public IntPtr Seed;

        /// <summary>
        /// Length in bytes of the input seed
        /// </summary>
        public uint SeedLen;

        /// <summary>
        /// Pointer to the identifying label
        /// </summary>
        public IntPtr Label;

        /// <summary>
        /// Length in bytes of the identifying label
        /// </summary>
        public uint LabelLen;

        /// <summary>
        /// Pointer receiving the output of the operation
        /// </summary>
        public IntPtr Output;

        /// <summary>
        /// Pointer to the length in bytes that the output to be created shall have, has to hold the desired length as input and will receive the calculated length as output
        /// </summary>
        public IntPtr OutputLen;
    }
}
