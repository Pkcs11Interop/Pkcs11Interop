/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11Library);

                // Get supported mechanisms
                List<CKM> mechanisms = slot.GetMechanismList();

                Assert.IsTrue(mechanisms.Count > 0);

                // Analyze first supported mechanism
                IMechanismInfo mechanismInfo = slot.GetMechanismInfo(mechanisms[0]);

                // Do something interesting with mechanism info
                Assert.IsNotNull(mechanismInfo.MechanismFlags);
            }
        }
    }
}
