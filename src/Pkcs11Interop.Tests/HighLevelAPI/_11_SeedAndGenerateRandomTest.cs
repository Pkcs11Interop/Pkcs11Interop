/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// SeedRandom and GenerateRandom tests.
    /// </summary>
    [TestFixture()]
    public class _11_SeedAndGenerateRandomTest
    {
        /// <summary>
        /// SeedRandom test.
        /// </summary>
        [Test()]
        public void _01_SeedRandomTest()
        {
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11Library);
                
                // Open RO (read-only) session
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Mix additional seed material into the token's random number generator
                    byte[] seed = ConvertUtils.Utf8StringToBytes("Additional seed material");
                    session.SeedRandom(seed);

                    // Do something interesting with random number generator
                }
            }
        }
        
        /// <summary>
        /// C_GenerateRandom test.
        /// </summary>
        [Test()]
        public void _02_GenerateRandomTest()
        {
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11Library);
                
                // Open RO (read-only) session
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Get random or pseudo-random data
                    byte[] randomData = session.GenerateRandom(256);

                    // Do something interesting with random data
                    Assert.IsTrue(randomData.Length == 256);
                }
            }
        }
    }
}
