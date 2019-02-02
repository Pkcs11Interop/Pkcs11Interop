/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);

                // Open RO (read-only) session
                ISession session = slot.OpenSession(SessionType.ReadOnly);

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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Session class can be used in using statement which defines a scope 
                // at the end of which the session will be closed automatically.
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                ISession session = slot.OpenSession(SessionType.ReadOnly);
                
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                ISession session = slot.OpenSession(SessionType.ReadOnly);
                
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RW (read-write) session
                using (ISession session = slot.OpenSession(SessionType.ReadWrite))
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
            using (IPkcs11 pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Get session details
                    ISessionInfo sessionInfo = session.GetSessionInfo();

                    // Do something interesting with session info
                    Assert.IsTrue(sessionInfo.SlotId == slot.SlotId);
                    Assert.IsNotNull(sessionInfo.SessionFlags);
                }
            }
        }
    }
}
