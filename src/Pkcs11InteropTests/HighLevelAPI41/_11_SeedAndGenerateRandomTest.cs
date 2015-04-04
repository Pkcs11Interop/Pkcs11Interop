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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI41;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
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
        public void _01_SeedRandomTest() // TODO - Test on device that supports this method
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
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
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
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

