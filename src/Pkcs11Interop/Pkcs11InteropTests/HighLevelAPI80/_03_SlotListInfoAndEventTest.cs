/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI80;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI80
{
    /// <summary>
    /// GetSlotList, GetSlotInfo and WaitForSlotEvent tests.
    /// </summary>
    [TestFixture()]
    public class _03_SlotListInfoAndEventTest
    {
        /// <summary>
        /// GetSlotList test.
        /// </summary>
        [Test()]
        public void _01_SlotListTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Get list of available slots
                List<Slot> slots = pkcs11.GetSlotList(false);

                // Do something interesting with slots
                Assert.IsNotNull(slots);
                Assert.IsTrue(slots.Count > 0);
            }
        }
        
        /// <summary>
        /// GetSlotInfo test.
        /// </summary>
        [Test()]
        public void _02_BasicSlotListAndInfoTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Get list of available slots
                List<Slot> slots = pkcs11.GetSlotList(false);
                
                // Do something interesting with slots
                Assert.IsNotNull(slots);
                Assert.IsTrue(slots.Count > 0);

                // Analyze first slot
                SlotInfo slotInfo = slots[0].GetSlotInfo();

                // Do something interesting with slot info
                Assert.IsNotNull(slotInfo.ManufacturerId);
            }
        }
        
        /// <summary>
        /// WaitForSlotEvent test.
        /// </summary>
        [Test()]
        public void _03_WaitForSlotEventTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Wait for a slot event
                bool eventOccured = false;
                ulong slotId = 0;
                pkcs11.WaitForSlotEvent(true, out eventOccured, out slotId);
                Assert.IsFalse(eventOccured);
            }
        }
    }
}

