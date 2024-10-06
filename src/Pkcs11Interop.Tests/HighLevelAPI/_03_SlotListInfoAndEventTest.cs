/*
 *  Copyright 2012-2024 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
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
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Get list of available slots
                List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);

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
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Get list of available slots
                List<ISlot> slots = pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent);
                
                // Do something interesting with slots
                Assert.IsNotNull(slots);
                Assert.IsTrue(slots.Count > 0);

                // Analyze first slot
                ISlotInfo slotInfo = slots[0].GetSlotInfo();

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
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Wait for a slot event
                bool eventOccured = false;
                ulong slotId = 0;
                pkcs11Library.WaitForSlotEvent(WaitType.NonBlocking, out eventOccured, out slotId);
                Assert.IsFalse(eventOccured);
            }
        }
    }
}
