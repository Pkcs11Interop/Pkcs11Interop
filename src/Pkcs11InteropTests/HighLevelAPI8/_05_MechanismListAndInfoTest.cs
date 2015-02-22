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
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
{
    /// <summary>
    /// GetMechanismList and GetMechanismInfo tests.
    /// </summary>
    [TestFixture()]
    public class _05_MechanismListAndInfoTest
    {
        /// <summary>
        /// Basic GetMechanismList and GetMechanismInfo test.
        /// </summary>
        [Test()]
        public void _01_BasicMechanismListAndInfoTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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

