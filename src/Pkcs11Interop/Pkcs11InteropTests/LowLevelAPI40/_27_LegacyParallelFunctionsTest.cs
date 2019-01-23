/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
{
    /// <summary>
    /// C_GetFunctionStatus and C_CancelFunction tests.
    /// </summary>
    [TestFixture()]
    public class _27_LegacyParallelFunctionsTest
    {
        /// <summary>
        /// C_GetFunctionStatus test.
        /// </summary>
        [Test()]
        public void _01_GetFunctionStatusTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, CKF.CKF_SERIAL_SESSION, IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                rv = pkcs11.C_GetFunctionStatus(session);
                if (rv != CKR.CKR_FUNCTION_NOT_PARALLEL)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_CancelFunction test.
        /// </summary>
        [Test()]
        public void _02_CancelFunctionTest()
        {
            if (Platform.UnmanagedLongSize != 4 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, CKF.CKF_SERIAL_SESSION, IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                rv = pkcs11.C_CancelFunction(session);
                if (rv != CKR.CKR_FUNCTION_NOT_PARALLEL)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

