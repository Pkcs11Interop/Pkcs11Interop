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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Mask generation functions
    /// </summary>
    public enum CKG : uint
    {
        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-1 digest algorithm
        /// </summary>
        CKG_MGF1_SHA1 = 0x00000001,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-256 digest algorithm
        /// </summary>
        CKG_MGF1_SHA256 = 0x00000002,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-384 digest algorithm
        /// </summary>
        CKG_MGF1_SHA384 = 0x00000003,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-512 digest algorithm
        /// </summary>
        CKG_MGF1_SHA512 = 0x00000004,

        /// <summary>
        /// PKCS #1 Mask Generation Function with SHA-224 digest algorithm
        /// </summary>
        CKG_MGF1_SHA224 = 0x00000005
    }
}
