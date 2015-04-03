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
    /// InitToken and InitPin tests.
    /// </summary>
    [TestFixture()]
    public class _09_InitTokenAndPinTest
    {
        /// <summary>
        /// Basic InitToken and InitPin test.
        /// </summary>
        [Test()]
        public void _01_BasicInitTokenAndPinTest()
        {
            if (Platform.UnmanagedLongSize != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                TokenInfo tokenInfo = slot.GetTokenInfo();

                // Check if token needs to be initialized
                if (!tokenInfo.TokenFlags.TokenInitialized)
                {
                    // Initialize token and SO (security officer) pin
                    slot.InitToken(Settings.SecurityOfficerPin, Settings.ApplicationName);

                    // Open RW session
                    using (Session session = slot.OpenSession(false))
                    {
                        // Login as SO (security officer)
                        session.Login(CKU.CKU_SO, Settings.SecurityOfficerPin);

                        // Initialize user pin
                        session.InitPin(Settings.NormalUserPin);

                        session.Logout();
                    }
                }
            }
        }
    }
}

