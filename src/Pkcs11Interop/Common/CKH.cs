/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Hardware feature types
    /// </summary>
    public enum CKH : uint
    {
        /// <summary>
        /// Monotonic counter objects represent hardware counters that exist on the device.
        /// </summary>
        CKH_MONOTONIC_COUNTER = 0x00000001,
        
        /// <summary>
        /// Clock objects represent real-time clocks that exist on the device.
        /// </summary>
        CKH_CLOCK = 0x00000002,

        /// <summary>
        /// User interface objects represent the presentation capabilities of the device.
        /// </summary>
        CKH_USER_INTERFACE = 0x00000003,

        /// <summary>
        /// Permanently reserved for token vendors.
        /// </summary>
        CKH_VENDOR_DEFINED = 0x80000000
    }
}
