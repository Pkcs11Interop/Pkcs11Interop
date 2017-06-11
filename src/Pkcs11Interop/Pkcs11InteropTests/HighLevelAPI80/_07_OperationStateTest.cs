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
using Net.Pkcs11Interop.HighLevelAPI80;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI80
{
    /// <summary>
    /// GetOperationState and SetOperationState tests.
    /// </summary>
    [TestFixture()]
    public class _07_OperationStateTest
    {
        /// <summary>
        /// Basic GetOperationState and SetOperationState test.
        /// </summary>
        [Test()]
        public void _01_BasicOperationStateTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(SessionType.ReadOnly))
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

