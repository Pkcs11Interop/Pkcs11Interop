/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System.Runtime.InteropServices;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI81
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
        public NativeULong Flags;

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public NativeULong MaxSessionCount;

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public NativeULong SessionCount;

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public NativeULong MaxRwSessionCount;

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public NativeULong RwSessionCount;

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public NativeULong MaxPinLen;

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public NativeULong MinPinLen;

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public NativeULong TotalPublicMemory;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public NativeULong FreePublicMemory;

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public NativeULong TotalPrivateMemory;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public NativeULong FreePrivateMemory;

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
