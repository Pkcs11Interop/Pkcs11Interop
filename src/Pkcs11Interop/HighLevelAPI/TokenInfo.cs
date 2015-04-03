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

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a token
    /// </summary>
    public class TokenInfo
    {
        /// <summary>
        /// Platform specific TokenInfo
        /// </summary>
        private HighLevelAPI41.TokenInfo _tokenInfo4 = null;

        /// <summary>
        /// Platform specific TokenInfo
        /// </summary>
        private HighLevelAPI81.TokenInfo _tokenInfo8 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.SlotId : _tokenInfo8.SlotId;
            }
        }

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        public string Label
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.Label : _tokenInfo8.Label;
            }
        }

        /// <summary>
        /// ID of the device manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.ManufacturerId : _tokenInfo8.ManufacturerId;
            }
        }

        /// <summary>
        /// Model of the device
        /// </summary>
        public string Model
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.Model : _tokenInfo8.Model;
            }
        }

        /// <summary>
        /// Serial number of the device
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.SerialNumber : _tokenInfo8.SerialNumber;
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
                if (_tokenFlags == null)
                    _tokenFlags = (Platform.UnmanagedLongSize == 4) ? new TokenFlags(_tokenInfo4.TokenFlags) : new TokenFlags(_tokenInfo8.TokenFlags);

                return _tokenFlags;
            }
        }

        /// <summary>
        /// Maximum number of sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxSessionCount
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.MaxSessionCount : _tokenInfo8.MaxSessionCount;
            }
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public ulong SessionCount
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.SessionCount : _tokenInfo8.SessionCount;
            }
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxRwSessionCount
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.MaxRwSessionCount : _tokenInfo8.MaxRwSessionCount;
            }
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public ulong RwSessionCount
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.RwSessionCount : _tokenInfo8.RwSessionCount;
            }
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public ulong MaxPinLen
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.MaxPinLen : _tokenInfo8.MaxPinLen;
            }
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public ulong MinPinLen
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.MinPinLen : _tokenInfo8.MinPinLen;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public ulong TotalPublicMemory
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.TotalPublicMemory : _tokenInfo8.TotalPublicMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public ulong FreePublicMemory
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.FreePublicMemory : _tokenInfo8.FreePublicMemory;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public ulong TotalPrivateMemory
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.TotalPrivateMemory : _tokenInfo8.TotalPrivateMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public ulong FreePrivateMemory
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.FreePrivateMemory : _tokenInfo8.FreePrivateMemory;
            }
        }

        /// <summary>
        /// Version number of hardware
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.HardwareVersion : _tokenInfo8.HardwareVersion;
            }
        }

        /// <summary>
        /// Version number of firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.FirmwareVersion : _tokenInfo8.FirmwareVersion;
            }
        }

        /// <summary>
        /// Current time (the value of this field only makes sense for tokens equipped with a clock)
        /// </summary>
        public string UtcTimeString
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.UtcTimeString : _tokenInfo8.UtcTimeString;
            }
        }

        /// <summary>
        /// UtcTimeString converted to DateTime or null if conversion failed
        /// </summary>
        public DateTime? UtcTime
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _tokenInfo4.UtcTime : _tokenInfo8.UtcTime;
            }
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI41.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo4 = tokenInfo;
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI81.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo8 = tokenInfo;
        }
    }
}
