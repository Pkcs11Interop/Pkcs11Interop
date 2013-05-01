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
    /// OpenSession, CloseSession, CloseAllSessions and GetSessionInfo tests.
    /// </summary>
    [TestFixture()]
    public class SessionTest
    {
        /// <summary>
        /// Basic OpenSession and CloseSession test.
        /// </summary>
        [Test()]
        public void BasicSessionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);

                // Open RO (read-only) session
                Session session = slot.OpenSession(true);

                // Do something interesting in RO session

                // Close session
                session.CloseSession();
            }
        }

        /// <summary>
        /// Using statement test.
        /// </summary>
        [Test()]
        public void UsingSessionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Session class can be used in using statement which defines a scope 
                // at the end of which the session will be closed automatically.
                using (Session session = slot.OpenSession(true))
                {
                    // Do something interesting in RO session
                }
            }
        }

        /// <summary>
        /// CloseSession via slot test.
        /// </summary>
        [Test()]
        public void CloseSessionViaSlotTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                Session session = slot.OpenSession(true);
                
                // Do something interesting in RO session
                
                // Alternatively session can be closed with CloseSession method of Slot class.
                slot.CloseSession(session);
            }
        }

        /// <summary>
        /// CloseAllSessions test.
        /// </summary>
        [Test()]
        public void CloseAllSessionsTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                Session session = slot.OpenSession(true);
                
                // Do something interesting in RO session
                Assert.IsNotNull(session);
                
                // All sessions can be closed with CloseAllSessions method of Slot class.
                slot.CloseAllSessions();
            }
        }

        /// <summary>
        /// Read-only session test.
        /// </summary>
        [Test()]
        public void ReadOnlySessionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Do something interesting in RO session
                }
            }
        }
        
        /// <summary>
        /// Read-write session test.
        /// </summary>
        [Test()]
        public void ReadWriteSessionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW (read-write) session
                using (Session session = slot.OpenSession(false))
                {
                    // Do something interesting in RW session
                }
            }
        }
        
        /// <summary>
        /// GetSessionInfo test.
        /// </summary>
        [Test()]
        public void SessionInfoTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Get session details
                    SessionInfo sessionInfo = session.GetSessionInfo();

                    // Do something interesting with session info
                    Assert.IsTrue(sessionInfo.SlotId == slot.SlotId);
                    Assert.IsNotNull(sessionInfo.SessionFlags);
                }
            }
        }
    }
}
