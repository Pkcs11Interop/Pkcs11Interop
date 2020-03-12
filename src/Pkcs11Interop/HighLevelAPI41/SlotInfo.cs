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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI41;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41
{
    /// <summary>
    /// Information about a slot
    /// </summary>
    public class SlotInfo : ISlotInfo
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
                return  ConvertUtils.UInt32ToUInt64(_slotId);
            }
        }

        /// <summary>
        /// Description of the slot
        /// </summary>
        protected string _slotDescription = null;

        /// <summary>
        /// Description of the slot
        /// </summary>
        public string SlotDescription
        {
            get
            {
                return _slotDescription;
            }
        }

        /// <summary>
        /// ID of the slot manufacturer
        /// </summary>
        protected string _manufacturerId = null;

        /// <summary>
        /// ID of the slot manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return _manufacturerId;
            }
        }

        /// <summary>
        /// Flags that provide capabilities of the slot
        /// </summary>
        protected SlotFlags _slotFlags = null;

        /// <summary>
        /// Flags that provide capabilities of the slot
        /// </summary>
        public ISlotFlags SlotFlags
        {
            get
            {
                return _slotFlags;
            }
        }

        /// <summary>
        /// Version number of the slot's hardware
        /// </summary>
        protected string _hardwareVersion = null;

        /// <summary>
        /// Version number of the slot's hardware
        /// </summary>
        public string HardwareVersion
        {
            get
            {
                return _hardwareVersion;
            }
        }

        /// <summary>
        /// Version number of the slot's firmware
        /// </summary>
        protected string _firmwareVersion = null;
        
        /// <summary>
        /// Version number of the slot's firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                return _firmwareVersion;
            }
        }

        /// <summary>
        /// Converts low level CK_SLOT_INFO structure to high level SlotInfo class
        /// </summary>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        /// <param name="ck_slot_info">Low level CK_SLOT_INFO structure</param>
        protected internal SlotInfo(NativeULong slotId, CK_SLOT_INFO ck_slot_info)
        {
            _slotId = slotId;
            _slotDescription = ConvertUtils.BytesToUtf8String(ck_slot_info.SlotDescription, true);
            _manufacturerId = ConvertUtils.BytesToUtf8String(ck_slot_info.ManufacturerId, true);
            _slotFlags = new SlotFlags(ck_slot_info.Flags);
            _hardwareVersion = ck_slot_info.HardwareVersion.ToString();
            _firmwareVersion = ck_slot_info.FirmwareVersion.ToString();
        }
    }
}
