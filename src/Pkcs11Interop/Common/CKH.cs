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

using System;

// Note: Code in this file is maintained manually.

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
