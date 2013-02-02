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
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// SeedRandom and GenerateRandom tests.
    /// </summary>
    [TestFixture()]
    public class SeedAndGenerateRandomTest
    {
        /// <summary>
        /// SeedRandom test.
        /// </summary>
        [Test()]
        public void SeedRandomTest() // TODO - Test on device that supports this method
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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
        public void GenerateRandomTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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

