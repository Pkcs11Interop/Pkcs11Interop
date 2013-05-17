/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_PKCS5_PBKD2 mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
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
        public uint PasswordLen;
    }
}
