/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI40;
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
{
    /// <summary>
    /// C_GetMechanismList and C_GetMechanismInfo tests.
    /// </summary>
    [TestFixture()]
    public class _05_MechanismListAndInfoTest
    {
        /// <summary>
        /// Basic C_GetMechanismList and C_GetMechanismInfo test.
        /// </summary>
        [Test()]
        public void _01_BasicMechanismListAndInfoTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                // Get number of supported mechanisms in first call
                NativeULong mechanismCount = 0;
                rv = pkcs11Library.C_GetMechanismList(slotId, null, ref mechanismCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(mechanismCount > 0);
                
                // Allocate array for supported mechanisms
                CKM[] mechanismList = new CKM[mechanismCount];
                
                // Get supported mechanisms in second call
                rv = pkcs11Library.C_GetMechanismList(slotId, mechanismList, ref mechanismCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Analyze first supported mechanism
                CK_MECHANISM_INFO mechanismInfo = new CK_MECHANISM_INFO();
                rv = pkcs11Library.C_GetMechanismInfo(slotId, mechanismList[0], ref mechanismInfo);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with mechanism info
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

