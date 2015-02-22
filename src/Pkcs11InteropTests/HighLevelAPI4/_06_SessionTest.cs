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
using Net.Pkcs11Interop.HighLevelAPI4;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI4
{
    /// <summary>
    /// OpenSession, CloseSession, CloseAllSessions and GetSessionInfo tests.
    /// </summary>
    [TestFixture()]
    public class _06_SessionTest
    {
        /// <summary>
        /// Basic OpenSession and CloseSession test.
        /// </summary>
        [Test()]
        public void _01_BasicSessionTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _02_UsingSessionTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _03_CloseSessionViaSlotTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _04_CloseAllSessionsTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _05_ReadOnlySessionTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _06_ReadWriteSessionTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _07_SessionInfoTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
