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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Flags that provide capabilities of the slot
    /// </summary>
    public class SlotFlags : ISlotFlags
    {
        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        protected NativeULong _flags;

        /// <summary>
        /// Bits flags that provide capabilities of the slot
        /// </summary>
        public ulong Flags
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_flags);
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
        protected internal SlotFlags(NativeULong flags)
        {
            _flags = flags;
        }
    }
}
