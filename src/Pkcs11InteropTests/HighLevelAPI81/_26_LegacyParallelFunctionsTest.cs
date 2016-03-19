/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI81;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI81
{
    /// <summary>
    /// GetFunctionStatus and CancelFunction tests.
    /// </summary>
    [TestFixture()]
    public class _26_LegacyParallelFunctionsTest
    {
        /// <summary>
        /// GetFunctionStatus test.
        /// </summary>
        [Test()]
        public void _01_GetFunctionStatusTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                    try
                    {
                        session.GetFunctionStatus();
                    }
                    catch (Pkcs11Exception ex)
                    {
                        if (ex.RV != CKR.CKR_FUNCTION_NOT_PARALLEL)
                            throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// CancelFunction test.
        /// </summary>
        [Test()]
        public void _02_CancelFunctionTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
            
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                    try
                    {
                        session.CancelFunction();
                    } catch (Pkcs11Exception ex)
                    {
                        if (ex.RV != CKR.CKR_FUNCTION_NOT_PARALLEL)
                            throw;
                    }
                }
            }
        }
    }
}
