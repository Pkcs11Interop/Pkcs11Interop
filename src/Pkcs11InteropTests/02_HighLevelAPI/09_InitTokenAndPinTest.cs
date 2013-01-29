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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// InitToken and InitPin tests.
    /// </summary>
    [TestFixture()]
    public class InitTokenAndPinTest
    {
        /// <summary>
        /// Basic InitToken and InitPin test.
        /// </summary>
        [Test()]
        public void BasicInitTokenAndPinTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
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

