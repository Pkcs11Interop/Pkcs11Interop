/*
 *  Copyright 2012-2024 The Pkcs11Interop Project
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
    /// C_GetOperationState and C_SetOperationState tests.
    /// </summary>
    [TestFixture()]
    public class _07_OperationStateTest
    {
        /// <summary>
        /// Basic C_GetOperationState and C_SetOperationState test.
        /// </summary>
        [Test()]
        public void _01_BasicOperationStateTest()
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
                
                // Open RO (read-only) session
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, CKF.CKF_SERIAL_SESSION, IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Get length of state in first call
                NativeULong stateLen = 0;
                rv = pkcs11Library.C_GetOperationState(session, null, ref stateLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(stateLen > 0);
                
                // Allocate array for state
                byte[] state = new byte[stateLen];

                // Get state in second call
                rv = pkcs11Library.C_GetOperationState(session, state, ref stateLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Let's set state so the test is complete
                rv = pkcs11Library.C_SetOperationState(session, state, ConvertUtils.UInt32FromInt32(state.Length), CK.CK_INVALID_HANDLE, CK.CK_INVALID_HANDLE);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

