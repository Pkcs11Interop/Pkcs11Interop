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
    /// Information about a slot
    /// </summary>
    public class SlotInfo
    {
        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI41.SlotInfo _slotInfo4 = null;

        /// <summary>
        /// Platform specific SlotInfo
        /// </summary>
        private HighLevelAPI81.SlotInfo _slotInfo8 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _slotInfo4.SlotId : _slotInfo8.SlotId;
            }
        }

        /// <summary>
        /// Description of the slot
        /// </summary>
        public string SlotDescription
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _slotInfo4.SlotDescription : _slotInfo8.SlotDescription;
            }
        }

        /// <summary>
        /// ID of the slot manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _slotInfo4.ManufacturerId : _slotInfo8.ManufacturerId;
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
                    _slotFlags = (Platform.UnmanagedLongSize == 4) ? new SlotFlags(_slotInfo4.SlotFlags) : new SlotFlags(_slotInfo8.SlotFlags);

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
                return (Platform.UnmanagedLongSize == 4) ? _slotInfo4.HardwareVersion : _slotInfo8.HardwareVersion;
            }
        }

        /// <summary>
        /// Version number of the slot's firmware
        /// </summary>
        public string FirmwareVersion
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _slotInfo4.FirmwareVersion : _slotInfo8.FirmwareVersion;
            }
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI41.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo4 = slotInfo;
        }

        /// <summary>
        /// Converts platform specific SlotInfo to platfrom neutral SlotInfo
        /// </summary>
        /// <param name="slotInfo">Platform specific SlotInfo</param>
        internal SlotInfo(HighLevelAPI81.SlotInfo slotInfo)
        {
            if (slotInfo == null)
                throw new ArgumentNullException("slotInfo");

            _slotInfo8 = slotInfo;
        }
    }
}
