/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI.MechanismParams
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
        /// Pointer to the length in bytes (uint) that the output to be created shall have, has to hold the desired length as input and will receive the calculated length as output
        /// </summary>
        public IntPtr OutputLen;
    }
}
