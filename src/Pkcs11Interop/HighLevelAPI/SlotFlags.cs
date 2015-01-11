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
        private HighLevelAPI4.SlotFlags _slotFlags4 = null;

        /// <summary>
        /// Platform specific SlotFlags
        /// </summary>
        private HighLevelAPI8.SlotFlags _slotFlags8 = null;

        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        public ulong Flags
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotFlags4.Flags : _slotFlags8.Flags;
            }
        }

        /// <summary>
        /// True if a token is present in the slot (e.g. a device is in the reader)
        /// </summary>
        public bool TokenPresent
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotFlags4.TokenPresent : _slotFlags8.TokenPresent;
            }
        }

        /// <summary>
        /// True if the reader supports removable devices
        /// </summary>
        public bool RemovableDevice
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotFlags4.RemovableDevice : _slotFlags8.RemovableDevice;
            }
        }

        /// <summary>
        /// True if the slot is a hardware slot, as opposed to a software slot implementing a "soft token"
        /// </summary>
        public bool HardwareSlot
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slotFlags4.HardwareSlot : _slotFlags8.HardwareSlot;
            }
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI4.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags4 = slotFlags;
        }

        /// <summary>
        /// Converts platform specific SlotFlags to platfrom neutral SlotFlags
        /// </summary>
        /// <param name="slotFlags">Platform specific SlotFlags</param>
        internal SlotFlags(HighLevelAPI8.SlotFlags slotFlags)
        {
            if (slotFlags == null)
                throw new ArgumentNullException("slotFlags");

            _slotFlags8 = slotFlags;
        }
    }
}
