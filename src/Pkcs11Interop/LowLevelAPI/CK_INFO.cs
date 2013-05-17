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

using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Provides general information about Cryptoki
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_INFO
    {
        /// <summary>
        /// Cryptoki interface version number, for compatibility with future revisions of this interface.
        /// </summary>
        public CK_VERSION CryptokiVersion;

        /// <summary>
        /// ID of the Cryptoki library manufacturer. Must be padded with the blank character (‘ ‘). Should not be null-terminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] ManufacturerId;

        /// <summary>
        /// Bit flags reserved for future versions. Must be zero for this version
        /// </summary>
        public uint Flags;

        /// <summary>
        /// Character-string description of the library. Must be padded with the blank character (‘ ‘). Should not be null-terminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] LibraryDescription;

        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        public CK_VERSION LibraryVersion;
    }
}
