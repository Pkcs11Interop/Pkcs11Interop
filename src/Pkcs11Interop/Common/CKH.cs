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
