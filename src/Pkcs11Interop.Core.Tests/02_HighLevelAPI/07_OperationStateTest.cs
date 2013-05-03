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

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetOperationState and SetOperationState tests.
    /// </summary>
    [TestFixture()]
    public class OperationStateTest
    {
        /// <summary>
        /// Basic GetOperationState and SetOperationState test.
        /// </summary>
        [Test()]
        public void BasicOperationStateTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Get operation state
                    byte[] state = session.GetOperationState();

                    // Do something interesting with operation state
                    Assert.IsNotNull(state);

                    // Let's set state so the test is complete
                    // Note that CK_INVALID_HANDLE is passed in as encryptionKey and authenticationKey
                    session.SetOperationState(state, new ObjectHandle(), new ObjectHandle());
                }
            }
        }
    }
}

