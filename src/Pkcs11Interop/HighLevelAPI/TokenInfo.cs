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
        private HighLevelAPI40.TokenInfo _tokenInfo40 = null;

        /// <summary>
        /// Platform specific TokenInfo
        /// </summary>
        private HighLevelAPI41.TokenInfo _tokenInfo41 = null;

        /// <summary>
        /// Platform specific TokenInfo
        /// </summary>
        private HighLevelAPI80.TokenInfo _tokenInfo80 = null;

        /// <summary>
        /// Platform specific TokenInfo
        /// </summary>
        private HighLevelAPI81.TokenInfo _tokenInfo81 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.SlotId : _tokenInfo41.SlotId;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.SlotId : _tokenInfo81.SlotId;
            }
        }

        /// <summary>
        /// Application-defined label, assigned during token initialization
        /// </summary>
        public string Label
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.Label : _tokenInfo41.Label;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.Label : _tokenInfo81.Label;
            }
        }

        /// <summary>
        /// ID of the device manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.ManufacturerId : _tokenInfo41.ManufacturerId;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.ManufacturerId : _tokenInfo81.ManufacturerId;
            }
        }

        /// <summary>
        /// Model of the device
        /// </summary>
        public string Model
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.Model : _tokenInfo41.Model;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.Model : _tokenInfo81.Model;
            }
        }

        /// <summary>
        /// Serial number of the device
        /// </summary>
        public string SerialNumber
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.SerialNumber : _tokenInfo41.SerialNumber;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.SerialNumber : _tokenInfo81.SerialNumber;
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
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _tokenFlags = (Platform.StructPackingSize == 0) ? new TokenFlags(_tokenInfo40.TokenFlags) : new TokenFlags(_tokenInfo41.TokenFlags);
                    else
                        _tokenFlags = (Platform.StructPackingSize == 0) ? new TokenFlags(_tokenInfo80.TokenFlags) : new TokenFlags(_tokenInfo81.TokenFlags);
                }

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
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.MaxSessionCount : _tokenInfo41.MaxSessionCount;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.MaxSessionCount : _tokenInfo81.MaxSessionCount;
            }
        }

        /// <summary>
        /// Number of sessions that this application currently has open with the token
        /// </summary>
        public ulong SessionCount
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.SessionCount : _tokenInfo41.SessionCount;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.SessionCount : _tokenInfo81.SessionCount;
            }
        }

        /// <summary>
        /// Maximum number of read/write sessions that can be opened with the token at one time by a single application
        /// </summary>
        public ulong MaxRwSessionCount
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.MaxRwSessionCount : _tokenInfo41.MaxRwSessionCount;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.MaxRwSessionCount : _tokenInfo81.MaxRwSessionCount;
            }
        }

        /// <summary>
        /// Number of read/write sessions that this application currently has open with the token
        /// </summary>
        public ulong RwSessionCount
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.RwSessionCount : _tokenInfo41.RwSessionCount;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.RwSessionCount : _tokenInfo81.RwSessionCount;
            }
        }

        /// <summary>
        /// Maximum length in bytes of the PIN
        /// </summary>
        public ulong MaxPinLen
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.MaxPinLen : _tokenInfo41.MaxPinLen;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.MaxPinLen : _tokenInfo81.MaxPinLen;
            }
        }

        /// <summary>
        /// Minimum length in bytes of the PIN
        /// </summary>
        public ulong MinPinLen
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.MinPinLen : _tokenInfo41.MinPinLen;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.MinPinLen : _tokenInfo81.MinPinLen;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which public objects may be stored
        /// </summary>
        public ulong TotalPublicMemory
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.TotalPublicMemory : _tokenInfo41.TotalPublicMemory;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.TotalPublicMemory : _tokenInfo81.TotalPublicMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for public objects
        /// </summary>
        public ulong FreePublicMemory
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.FreePublicMemory : _tokenInfo41.FreePublicMemory;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.FreePublicMemory : _tokenInfo81.FreePublicMemory;
            }
        }

        /// <summary>
        /// The total amount of memory on the token in bytes in which private objects may be stored
        /// </summary>
        public ulong TotalPrivateMemory
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.TotalPrivateMemory : _tokenInfo41.TotalPrivateMemory;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.TotalPrivateMemory : _tokenInfo81.TotalPrivateMemory;
            }
        }

        /// <summary>
        /// The amount of free (unused) memory on the token in bytes for private objects
        /// </summary>
        public ulong FreePrivateMemory
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.FreePrivateMemory : _tokenInfo41.FreePrivateMemory;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.FreePrivateMemory : _tokenInfo81.FreePrivateMemory;
            }
        }

        /// <summary>
        /// Version number of hardware
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.HardwareVersion : _tokenInfo41.HardwareVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.HardwareVersion : _tokenInfo81.HardwareVersion;
            }
        }

        /// <summary>
        /// Version number of firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.FirmwareVersion : _tokenInfo41.FirmwareVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.FirmwareVersion : _tokenInfo81.FirmwareVersion;
            }
        }

        /// <summary>
        /// Current time (the value of this field only makes sense for tokens equipped with a clock)
        /// </summary>
        public string UtcTimeString
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.UtcTimeString : _tokenInfo41.UtcTimeString;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.UtcTimeString : _tokenInfo81.UtcTimeString;
            }
        }

        /// <summary>
        /// UtcTimeString converted to DateTime or null if conversion failed
        /// </summary>
        public DateTime? UtcTime
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenInfo40.UtcTime : _tokenInfo41.UtcTime;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenInfo80.UtcTime : _tokenInfo81.UtcTime;
            }
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI40.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo40 = tokenInfo;
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI41.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo41 = tokenInfo;
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI80.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo80 = tokenInfo;
        }

        /// <summary>
        /// Converts platform specific TokenInfo to platfrom neutral TokenInfo
        /// </summary>
        /// <param name="tokenInfo">Platform specific TokenInfo</param>
        internal TokenInfo(HighLevelAPI81.TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
                throw new ArgumentNullException("tokenInfo");

            _tokenInfo81 = tokenInfo;
        }
    }
}
