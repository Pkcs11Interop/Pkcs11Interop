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
    /// Structure that provides the parameters to the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_PKCS5_PBKD2_PARAMS
    {
        /// <summary>
        /// Source of the salt value (CKZ)
        /// </summary>
        public uint SaltSource;

        /// <summary>
        /// Data used as the input for the salt source
        /// </summary>
        public IntPtr SaltSourceData;

        /// <summary>
        /// Length of the salt source input
        /// </summary>
        public uint SaltSourceDataLen;

        /// <summary>
        /// Number of iterations to perform when generating each block of random data
        /// </summary>
        public uint Iterations;

        /// <summary>
        /// Pseudo-random function to used to generate the key (CKP)
        /// </summary>
        public uint Prf;

        /// <summary>
        /// Data used as the input for PRF in addition to the salt value
        /// </summary>
        public IntPtr PrfData;

        /// <summary>
        /// Length of the input data for the PRF
        /// </summary>
        public uint PrfDataLen;

        /// <summary>
        /// Points to the password to be used in the PBE key generation
        /// </summary>
        public IntPtr Password;

        /// <summary>
        /// Length in bytes of the password information
        /// </summary>
        public IntPtr PasswordLen;
    }
}
