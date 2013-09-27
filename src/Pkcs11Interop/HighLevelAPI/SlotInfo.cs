/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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
    /// Information about a slot
    /// </summary>
    public class SlotInfo
    {
        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI4.SlotInfo _slotInfo4 = null;

        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI8.SlotInfo _slotInfo8 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotInfo4.SlotId : _slotInfo8.SlotId;
            }
        }

        /// <summary>
        /// Description of the slot
        /// </summary>
        public string SlotDescription
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotInfo4.SlotDescription : _slotInfo8.SlotDescription;
            }
        }

        /// <summary>
        /// ID of the slot manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotInfo4.ManufacturerId : _slotInfo8.ManufacturerId;
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
                    _slotFlags = (UnmanagedLong.Size == 4) ? new SlotFlags(_slotInfo4.SlotFlags) : new SlotFlags(_slotInfo8.SlotFlags);

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
                return (UnmanagedLong.Size == 4) ? _slotInfo4.HardwareVersion : _slotInfo8.HardwareVersion;
            }
        }

        /// <summary>
        /// Version number of the slot's firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotInfo4.FirmwareVersion : _slotInfo8.FirmwareVersion;
            }
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI4.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo4 = slotInfo;
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI8.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo8 = slotInfo;
        }
    }
}
