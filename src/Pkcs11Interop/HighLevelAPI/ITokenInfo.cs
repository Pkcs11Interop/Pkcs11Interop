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

using System;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a token
    /// </summary>
    public interface ITokenInfo
    {
        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        ulong SlotId
        {
            get;
        }

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        string Label
        {
            get;
        }

        /// <summary>
        /// ID of the device manufacturer
        /// </summary>
        string ManufacturerId
        {
            get;
        }

        /// <summary>
        /// Model of the device
        /// </summary>
        string Model
        {
            get;
        }

        /// <summary>
        /// Serial number of the device
        /// </summary>
        string SerialNumber
        {
            get;
        }

        /// <summary>
        /// Bit flags indicating capabilities and status of the device
        /// </summary>
        ITokenFlags TokenFlags
        {
            get;
        }

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        ulong MaxSessionCount
        {
            get;
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        ulong SessionCount
        {
            get;
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        ulong MaxRwSessionCount
        {
            get;
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        ulong RwSessionCount
        {
            get;
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        ulong MaxPinLen
        {
            get;
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        ulong MinPinLen
        {
            get;
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        ulong TotalPublicMemory
        {
            get;
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        ulong FreePublicMemory
        {
            get;
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        ulong TotalPrivateMemory
        {
            get;
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        ulong FreePrivateMemory
        {
            get;
        }

        /// <summary>
        /// Version number of hardware
        /// </summary>
        string HardwareVersion
        {
            get;
        }

        /// <summary>
        /// Version number of firmware
        /// </summary>
        string FirmwareVersion
        {
            get;
        }

        /// <summary>
        /// Current time (the value of this field only makes sense for tokens equipped with a clock)
        /// </summary>
        string UtcTimeString
        {
            get;
        }

        /// <summary>
        /// UtcTimeString converted to DateTime or null if conversion failed
        /// </summary>
        DateTime? UtcTime
        {
            get;
        }
    }
}
