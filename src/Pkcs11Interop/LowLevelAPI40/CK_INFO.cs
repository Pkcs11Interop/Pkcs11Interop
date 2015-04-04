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

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Provides general information about Cryptoki
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
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
