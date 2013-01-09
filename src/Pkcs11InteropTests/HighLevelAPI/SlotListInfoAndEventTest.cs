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

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.HighLevelAPI;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetSlotList, GetSlotInfo and WaitForSlotEvent tests.
    /// </summary>
    [TestFixture()]
    public class SlotListInfoAndEventTest
    {
        /// <summary>
        /// GetSlotList test.
        /// </summary>
        [Test()]
        public void SlotListTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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
        public void BasicSlotListAndInfoTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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
        public void WaitForSlotEventTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Wait for a slot event
                uint slotId = pkcs11.WaitForSlotEvent(true);
                Assert.IsTrue(slotId == CK.CK_INVALID_HANDLE);
            }
        }
    }
}

