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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Information about a slot
    /// </summary>
    public class SlotInfo
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
        /// Description of the slot
        /// </summary>
        private string _slotDescription = null;

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
        private string _manufacturerId = null;

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
        private SlotFlags _slotFlags = null;

        /// <summary>
        /// Flags that provide capabilities of the slot
        /// </summary>
        public SlotFlags SlotFlags
        {
            get
            {
                return _slotFlags;
            }
        }

        /// <summary>
        /// Version number of the slot's hardware
        /// </summary>
        private string _hardwareVersion = null;

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
        private string _firmwareVersion = null;
        
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
        internal SlotInfo(ulong slotId, CK_SLOT_INFO ck_slot_info)
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
