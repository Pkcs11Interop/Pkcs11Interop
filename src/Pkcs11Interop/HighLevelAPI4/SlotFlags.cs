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

namespace Net.Pkcs11Interop.HighLevelAPI41
{
    /// <summary>
    /// Flags that provide capabilities of the slot
    /// </summary>
    public class SlotFlags
    {
        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        private uint _flags;

        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        public uint Flags
        {
            get
            {
                return _flags;
            }
        }

        /// <summary>
        /// True if a token is present in the slot (e.g. a device is in the reader)
        /// </summary>
        public bool TokenPresent
        {
            get
            {
                return ((_flags & CKF.CKF_TOKEN_PRESENT) == CKF.CKF_TOKEN_PRESENT);
            }
        }

        /// <summary>
        /// True if the reader supports removable devices
        /// </summary>
        public bool RemovableDevice
        {
            get
            {
                return ((_flags & CKF.CKF_REMOVABLE_DEVICE) == CKF.CKF_REMOVABLE_DEVICE);
            }
        }

        /// <summary>
        /// True if the slot is a hardware slot, as opposed to a software slot implementing a "soft token"
        /// </summary>
        public bool HardwareSlot
        {
            get
            {
                return ((_flags & CKF.CKF_HW_SLOT) == CKF.CKF_HW_SLOT);
            }
        }

        /// <summary>
        /// Initializes new instance of SlotFlags class
        /// </summary>
        /// <param name="flags">Bits flags that provide capabilities of the slot</param>
        internal SlotFlags(uint flags)
        {
            _flags = flags;
        }
    }
}
