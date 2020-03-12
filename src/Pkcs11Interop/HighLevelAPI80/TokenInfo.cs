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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI80;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80
{
    /// <summary>
    /// Information about a token
    /// </summary>
    public class TokenInfo : ITokenInfo
    {
        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        protected NativeULong _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_slotId);
            }
        }

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        protected string _label = null;

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// ID of the device manufacturer
        /// </summary>
        protected string _manufacturerId = null;

        /// <summary>
        /// ID of the device manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return _manufacturerId;
            }
        }

        /// <summary>
        /// Model of the device
        /// </summary>
        protected string _model = null;

        /// <summary>
        /// Model of the device
        /// </summary>
        public string Model
        {
            get
            {
                return _model;
            }
        }

        /// <summary>
        /// Serial number of the device
        /// </summary>
        protected string _serialNumber = null;

        /// <summary>
        /// Serial number of the device
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
        }

        /// <summary>
        /// Bit flags indicating capabilities and status of the device
        /// </summary>
        protected TokenFlags _tokenFlags = null;

        /// <summary>
        /// Bit flags indicating capabilities and status of the device
        /// </summary>
        public ITokenFlags TokenFlags
        {
            get
            {
                return _tokenFlags;
            }
        }

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        protected NativeULong _maxSessionCount = 0;

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxSessionCount
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_maxSessionCount);
            }
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        protected NativeULong _sessionCount = 0;

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public ulong SessionCount
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_sessionCount);
            }
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        protected NativeULong _maxRwSessionCount = 0;

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxRwSessionCount
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_maxRwSessionCount);
            }
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        protected NativeULong _rwSessionCount = 0;

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public ulong RwSessionCount
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_rwSessionCount);
            }
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        protected NativeULong _maxPinLen = 0;

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public ulong MaxPinLen
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_maxPinLen);
            }
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        protected NativeULong _minPinLen = 0;

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public ulong MinPinLen
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_minPinLen);
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        protected NativeULong _totalPublicMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public ulong TotalPublicMemory
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_totalPublicMemory);
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        protected NativeULong _freePublicMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public ulong FreePublicMemory
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_freePublicMemory);
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        protected NativeULong _totalPrivateMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public ulong TotalPrivateMemory
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_totalPrivateMemory);
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        protected NativeULong _freePrivateMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public ulong FreePrivateMemory
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_freePrivateMemory);
            }
        }

        /// <summary>
        /// Version number of hardware
        /// </summary>
        protected string _hardwareVersion = null;

        /// <summary>
        /// Version number of hardware
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                return _hardwareVersion;
            }
        }

        /// <summary>
        /// Version number of firmware
        /// </summary>
        protected string _firmwareVersion = null;

        /// <summary>
        /// Version number of firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                return _firmwareVersion;
            }
        }

        /// <summary>
        /// Current time (the value of this field only makes sense for tokens equipped with a clock)
        /// </summary>
        protected string _utcTimeString = null;

        /// <summary>
        /// Current time (the value of this field only makes sense for tokens equipped with a clock)
        /// </summary>
        public string UtcTimeString
        {
            get
            {
                return _utcTimeString;
            }
        }

        /// <summary>
        /// UtcTimeString converted to DateTime or null if conversion failed
        /// </summary>
        protected DateTime? _utcTime = null;

        /// <summary>
        /// UtcTimeString converted to DateTime or null if conversion failed
        /// </summary>
        public DateTime? UtcTime
        {
            get
            {
                return _utcTime;
            }
        }

        /// <summary>
        /// Converts low level CK_TOKEN_INFO structure to high level TokenInfo class
        /// </summary>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        /// <param name="ck_token_info">Low level CK_TOKEN_INFO structure</param>
        protected internal TokenInfo(NativeULong slotId, CK_TOKEN_INFO ck_token_info)
        {
            _slotId = slotId;
            _label = ConvertUtils.BytesToUtf8String(ck_token_info.Label, true);
            _manufacturerId = ConvertUtils.BytesToUtf8String(ck_token_info.ManufacturerId, true);
            _model = ConvertUtils.BytesToUtf8String(ck_token_info.Model, true);
            _serialNumber = ConvertUtils.BytesToUtf8String(ck_token_info.SerialNumber, true);
            _tokenFlags = new TokenFlags(ck_token_info.Flags);
            _maxSessionCount = ck_token_info.MaxSessionCount;
            _sessionCount = ck_token_info.SessionCount;
            _maxRwSessionCount = ck_token_info.MaxRwSessionCount;
            _rwSessionCount = ck_token_info.RwSessionCount;
            _maxPinLen = ck_token_info.MaxPinLen;
            _minPinLen = ck_token_info.MinPinLen;
            _totalPublicMemory = ck_token_info.TotalPublicMemory;
            _freePublicMemory = ck_token_info.FreePublicMemory;
            _totalPrivateMemory = ck_token_info.TotalPrivateMemory;
            _freePrivateMemory = ck_token_info.FreePrivateMemory;
            _hardwareVersion = ck_token_info.HardwareVersion.ToString();
            _firmwareVersion = ck_token_info.FirmwareVersion.ToString();
            _utcTimeString = ConvertUtils.BytesToUtf8String(ck_token_info.UtcTime, true);

            try
            {
                _utcTime = ConvertUtils.UtcTimeStringToDateTime(_utcTimeString);
            }
            catch
            {
                _utcTime = null;
            }
        }
    }
}
