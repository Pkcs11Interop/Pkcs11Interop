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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI4;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI4
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
            if (UnmanagedLong.Size != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
            if (UnmanagedLong.Size != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
            if (UnmanagedLong.Size != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Wait for a slot event
                bool eventOccured = false;
                uint slotId = 0;
                pkcs11.WaitForSlotEvent(true, out eventOccured, out slotId);
                Assert.IsFalse(eventOccured);
            }
        }
    }
}

