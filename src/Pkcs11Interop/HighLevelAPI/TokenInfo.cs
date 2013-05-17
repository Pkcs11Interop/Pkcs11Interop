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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a token
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        private uint _slotId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public uint SlotId
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
        private uint _maxSessionCount = 0;

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public uint MaxSessionCount
        {
            get
            {
                return _maxSessionCount;
            }
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        private uint _sessionCount = 0;

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public uint SessionCount
        {
            get
            {
                return _sessionCount;
            }
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        private uint _maxRwSessionCount = 0;

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public uint MaxRwSessionCount
        {
            get
            {
                return _maxRwSessionCount;
            }
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        private uint _rwSessionCount = 0;

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public uint RwSessionCount
        {
            get
            {
                return _rwSessionCount;
            }
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        private uint _maxPinLen = 0;

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public uint MaxPinLen
        {
            get
            {
                return _maxPinLen;
            }
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        private uint _minPinLen = 0;

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public uint MinPinLen
        {
            get
            {
                return _minPinLen;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        private uint _totalPublicMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public uint TotalPublicMemory
        {
            get
            {
                return _totalPublicMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        private uint _freePublicMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public uint FreePublicMemory
        {
            get
            {
                return _freePublicMemory;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        private uint _totalPrivateMemory = 0;

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public uint TotalPrivateMemory
        {
            get
            {
                return _totalPrivateMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        private uint _freePrivateMemory = 0;

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public uint FreePrivateMemory
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
        internal TokenInfo(uint slotId, LowLevelAPI.CK_TOKEN_INFO ck_token_info)
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
            _hardwareVersion = ConvertUtils.CkVersionToString(ck_token_info.HardwareVersion);
            _firmwareVersion = ConvertUtils.CkVersionToString(ck_token_info.FirmwareVersion);
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
