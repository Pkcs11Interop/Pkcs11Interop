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

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI41;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
{
    /// <summary>
    /// C_GetSlotList, C_GetSlotInfo and C_WaitForSlotEvent tests.
    /// </summary>
    [TestFixture()]
    public class _03_SlotListInfoAndEventTest
    {
        /// <summary>
        /// C_GetSlotList test.
        /// </summary>
        [Test()]
        public void _01_SlotListTest()
        {
            if (Platform.UnmanagedLongSize != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs4);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Get number of slots in first call
                uint slotCount = 0;
                rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(slotCount > 0);
                
                // Allocate array for slot IDs
                uint[] slotList = new uint[slotCount];
                
                // Get slot IDs in second call
                rv = pkcs11.C_GetSlotList(true, slotList, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with slots

                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_GetSlotInfo test.
        /// </summary>
        [Test()]
        public void _02_BasicSlotListAndInfoTest()
        {
            if (Platform.UnmanagedLongSize != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs4);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Get number of slots in first call
                uint slotCount = 0;
                rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(slotCount > 0);
                
                // Allocate array for slot IDs
                uint[] slotList = new uint[slotCount];
                
                // Get slot IDs in second call
                rv = pkcs11.C_GetSlotList(true, slotList, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Analyze first slot
                CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
                rv = pkcs11.C_GetSlotInfo(slotList[0], ref slotInfo);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with slot info
                Assert.IsFalse(String.IsNullOrEmpty(ConvertUtils.BytesToUtf8String(slotInfo.ManufacturerId)));
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_WaitForSlotEvent test.
        /// </summary>
        [Test()]
        public void _03_WaitForSlotEventTest()
        {
            if (Platform.UnmanagedLongSize != 4)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs4);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());

                // Wait for a slot event
                uint slot = 0;
                rv = pkcs11.C_WaitForSlotEvent(CKF.CKF_DONT_BLOCK, ref slot, IntPtr.Zero);
                if (rv != CKR.CKR_NO_EVENT)
                    Assert.Fail(rv.ToString());

                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

