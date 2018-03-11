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

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
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
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Get number of slots in first call
                NativeULong slotCount = 0;
                rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(slotCount > 0);
                
                // Allocate array for slot IDs
                NativeULong[] slotList = new NativeULong[slotCount];
                
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
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Get number of slots in first call
                NativeULong slotCount = 0;
                rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(slotCount > 0);
                
                // Allocate array for slot IDs
                NativeULong[] slotList = new NativeULong[slotCount];
                
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
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs80);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());

                // Wait for a slot event
                NativeULong slot = 0;
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

