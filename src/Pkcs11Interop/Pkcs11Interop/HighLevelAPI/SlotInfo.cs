/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a slot
    /// </summary>
    public class SlotInfo
    {
        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI40.SlotInfo _slotInfo40 = null;

        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI41.SlotInfo _slotInfo41 = null;

        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI80.SlotInfo _slotInfo80 = null;

        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI81.SlotInfo _slotInfo81 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotInfo40.SlotId : _slotInfo41.SlotId;
                else
                    return (Platform.StructPackingSize == 0) ? _slotInfo80.SlotId : _slotInfo81.SlotId;
            }
        }

        /// <summary>
        /// Description of the slot
        /// </summary>
        public string SlotDescription
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotInfo40.SlotDescription : _slotInfo41.SlotDescription;
                else
                    return (Platform.StructPackingSize == 0) ? _slotInfo80.SlotDescription : _slotInfo81.SlotDescription;
            }
        }

        /// <summary>
        /// ID of the slot manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotInfo40.ManufacturerId : _slotInfo41.ManufacturerId;
                else
                    return (Platform.StructPackingSize == 0) ? _slotInfo80.ManufacturerId : _slotInfo81.ManufacturerId;
            }
        }

        /// <summary>
        /// Flags that provide capabilities of the slot
        /// </summary>
        private SlotFlags _slotFlags = null;

        /// <summary>
        /// Flags that provide capabilities of the slot
        /// </summary>
        public SlotFlags SlotFlags
        {
            get
            {
                if (_slotFlags == null)
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _slotFlags = (Platform.StructPackingSize == 0) ? new SlotFlags(_slotInfo40.SlotFlags) : new SlotFlags(_slotInfo41.SlotFlags);
                    else
                        _slotFlags = (Platform.StructPackingSize == 0) ? new SlotFlags(_slotInfo80.SlotFlags) : new SlotFlags(_slotInfo81.SlotFlags);
                }

                return _slotFlags;
            }
        }

        /// <summary>
        /// Version number of the slot's hardware
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotInfo40.HardwareVersion : _slotInfo41.HardwareVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _slotInfo80.HardwareVersion : _slotInfo81.HardwareVersion;
            }
        }

        /// <summary>
        /// Version number of the slot's firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotInfo40.FirmwareVersion : _slotInfo41.FirmwareVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _slotInfo80.FirmwareVersion : _slotInfo81.FirmwareVersion;
            }
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI40.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo40 = slotInfo;
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI41.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo41 = slotInfo;
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI80.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo80 = slotInfo;
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI81.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo81 = slotInfo;
        }
    }
}
