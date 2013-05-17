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

using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Information about a slot
    /// </summary>
    public class SlotInfo
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
        internal SlotInfo(uint slotId, LowLevelAPI.CK_SLOT_INFO ck_slot_info)
        {
            _slotId = slotId;
            _slotDescription = ConvertUtils.BytesToUtf8String(ck_slot_info.SlotDescription, true);
            _manufacturerId = ConvertUtils.BytesToUtf8String(ck_slot_info.ManufacturerId, true);
            _slotFlags = new SlotFlags(ck_slot_info.Flags);
            _hardwareVersion = ConvertUtils.CkVersionToString(ck_slot_info.HardwareVersion);
            _firmwareVersion = ConvertUtils.CkVersionToString(ck_slot_info.FirmwareVersion);
        }
    }
}
