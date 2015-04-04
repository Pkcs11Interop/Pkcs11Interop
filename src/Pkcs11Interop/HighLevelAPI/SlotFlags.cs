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
    /// Flags that provide capabilities of the slot
    /// </summary>
    public class SlotFlags
    {
        /// <summary>
        /// Platform specific SlotFlags
        /// </summary>
        private HighLevelAPI40.SlotFlags _slotFlags40 = null;

        /// <summary>
        /// Platform specific SlotFlags
        /// </summary>
        private HighLevelAPI41.SlotFlags _slotFlags41 = null;

        /// <summary>
        /// Platform specific SlotFlags
        /// </summary>
        private HighLevelAPI80.SlotFlags _slotFlags80 = null;

        /// <summary>
        /// Platform specific SlotFlags
        /// </summary>
        private HighLevelAPI81.SlotFlags _slotFlags81 = null;

        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotFlags40.Flags : _slotFlags41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _slotFlags80.Flags : _slotFlags81.Flags;
            }
        }

        /// <summary>
        /// True if a token is present in the slot (e.g. a device is in the reader)
        /// </summary>
        public bool TokenPresent
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotFlags40.TokenPresent : _slotFlags41.TokenPresent;
                else
                    return (Platform.StructPackingSize == 0) ? _slotFlags80.TokenPresent : _slotFlags81.TokenPresent;
            }
        }

        /// <summary>
        /// True if the reader supports removable devices
        /// </summary>
        public bool RemovableDevice
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotFlags40.RemovableDevice : _slotFlags41.RemovableDevice;
                else
                    return (Platform.StructPackingSize == 0) ? _slotFlags80.RemovableDevice : _slotFlags81.RemovableDevice;
            }
        }

        /// <summary>
        /// True if the slot is a hardware slot, as opposed to a software slot implementing a "soft token"
        /// </summary>
        public bool HardwareSlot
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slotFlags40.HardwareSlot : _slotFlags41.HardwareSlot;
                else
                    return (Platform.StructPackingSize == 0) ? _slotFlags80.HardwareSlot : _slotFlags81.HardwareSlot;
            }
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI40.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags40 = slotFlags;
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI41.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags41 = slotFlags;
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI80.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags80 = slotFlags;
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI81.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags81 = slotFlags;
        }
    }
}
