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
    /// Provides information about a token
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_TOKEN_INFO
    {
        /// <summary>
        /// Application-defined label, assigned during token initialization. Must be padded with the blank character (‘ ‘). Should not be null-terminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Label;

        /// <summary>
        /// ID of the device manufacturer. Must be padded with the blank character (‘ ‘). Should not be nullterminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] ManufacturerId;

        /// <summary>
        /// Model of the device. Must be padded with the blank character (‘ ‘). Should not be null-terminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Model;

        /// <summary>
        /// Character-string serial number of the device. Must be padded with the blank character (‘ ‘). Should not be null-terminated.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] SerialNumber;

        /// <summary>
        /// Bit flags indicating capabilities and status of the device
        /// </summary>
        public uint Flags;

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public uint MaxSessionCount;

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public uint SessionCount;

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public uint MaxRwSessionCount;

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public uint RwSessionCount;

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public uint MaxPinLen;

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public uint MinPinLen;

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public uint TotalPublicMemory;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public uint FreePublicMemory;

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public uint TotalPrivateMemory;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public uint FreePrivateMemory;

        /// <summary>
        /// Version number of hardware
        /// </summary>
        public CK_VERSION HardwareVersion;

        /// <summary>
        /// Version number of firmware
        /// </summary>
        public CK_VERSION FirmwareVersion;

        /// <summary>
        /// Current time as a character-string of length 16, represented in the format YYYYMMDDhhmmssxx (4 characters for the year; 2 characters each for the month, the day, the hour, the minute, and the second; and 2 additional reserved ‘0' characters). The value of this field only makes sense for tokens equipped with a clock, as indicated in the token information flags.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] UtcTime;
    }
}
