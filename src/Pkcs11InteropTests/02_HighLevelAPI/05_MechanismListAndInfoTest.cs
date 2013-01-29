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
    /// GetMechanismList and GetMechanismInfo tests.
    /// </summary>
    [TestFixture()]
    public class MechanismListAndInfoTest
    {
        /// <summary>
        /// Basic GetMechanismList and GetMechanismInfo test.
        /// </summary>
        [Test()]
        public void BasicMechanismListAndInfoTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                // Get supported mechanisms
                List<CKM> mechanisms = slot.GetMechanismList();

                Assert.IsTrue(mechanisms.Count > 0);

                // Analyze first supported mechanism
                MechanismInfo mechanismInfo = slot.GetMechanismInfo(mechanisms[0]);

                // Do something interesting with mechanism info
                Assert.IsNotNull(mechanismInfo.MechanismFlags);
            }
        }
    }
}

