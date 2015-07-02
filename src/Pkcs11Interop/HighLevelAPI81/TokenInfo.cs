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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Information about a token
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        private ulong _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return _slotId;
            }
        }

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        private string _label = null;

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
        private string _manufacturerId = null;

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
        private string _model = null;

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
        private string _serialNumber = null;

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
        private TokenFlags _tokenFlags = null;

        /// <summary>
        /// Bit flags indicating capabilities and status of the device
        /// </summary>
        public TokenFlags TokenFlags
        {
            get
            {
                return _tokenFlags;
            }
        }

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        private ulong _maxSessionCount = 0;

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxSessionCount
        {
            get
            {
                return _maxSessionCount;
            }
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        private ulong _sessionCount = 0;

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public ulong SessionCount
        {
            get
            {
                return _sessionCount;
            }
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        private ulong _maxRwSessionCount = 0;

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxRwSessionCount
        {
            get
            {
                return _maxRwSessionCount;
            }
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        private ulong _rwSessionCount = 0;

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public ulong RwSessionCount
        {
            get
            {
                return _rwSessionCount;
            }
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        private ulong _maxPinLen = 0;

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public ulong MaxPinLen
        {
            get
            {
                return _maxPinLen;
            }
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        private ulong _minPinLen = 0;

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public ulong MinPinLen
        {
            get
            {
                return _minPinLen;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        private ulong _totalPublicMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public ulong TotalPublicMemory
        {
            get
            {
                return _totalPublicMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        private ulong _freePublicMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public ulong FreePublicMemory
        {
            get
            {
                return _freePublicMemory;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        private ulong _totalPrivateMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public ulong TotalPrivateMemory
        {
            get
            {
                return _totalPrivateMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        private ulong _freePrivateMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public ulong FreePrivateMemory
        {
            get
            {
                return _freePrivateMemory;
            }
        }

        /// <summary>
        /// Version number of hardware
        /// </summary>
        private string _hardwareVersion = null;

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
        private string _firmwareVersion = null;

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
        private string _utcTimeString = null;

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
        private DateTime? _utcTime = null;

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
        internal TokenInfo(ulong slotId, CK_TOKEN_INFO ck_token_info)
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
