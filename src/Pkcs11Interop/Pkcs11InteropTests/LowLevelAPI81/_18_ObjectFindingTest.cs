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
using Net.Pkcs11Interop.LowLevelAPI81;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI81
{
    /// <summary>
    /// C_FindObjectsInit, C_FindObjects and C_FindObjectsFinal tests.
    /// </summary>
    [TestFixture()]
    public class _18_ObjectFindingTest
    {
        /// <summary>
        /// Basic C_FindObjectsInit, C_FindObjects and C_FindObjectsFinal test.
        /// </summary>
        [Test()]
        public void _01_BasicObjectFindingTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs81);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                ulong slotId = Helpers.GetUsableSlot(pkcs11);
                
                ulong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, Convert.ToUInt64(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Let's create two objects so we can find something
                ulong objectId1 = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId1);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                ulong objectId2 = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId2);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare attribute template that defines search criteria
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[2];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);

                // Initialize searching
                rv = pkcs11.C_FindObjectsInit(session, template, Convert.ToUInt64(template.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get search results
                ulong foundObjectCount = 0;
                ulong[] foundObjectIds = new ulong[2];
                foundObjectIds[0] = CK.CK_INVALID_HANDLE;
                foundObjectIds[1] = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_FindObjects(session, foundObjectIds, Convert.ToUInt64(foundObjectIds.Length), ref foundObjectCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Terminate searching
                rv = pkcs11.C_FindObjectsFinal(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with found objects
                Assert.IsTrue((foundObjectIds[0] != CK.CK_INVALID_HANDLE) && (foundObjectIds[1] != CK.CK_INVALID_HANDLE));

                // In LowLevelAPI we have to free unmanaged memory taken by attributes
                for (int i = 0; i < template.Length; i++)
                {
                    UnmanagedMemory.Free(ref template[i].value);
                    template[i].valueLen = 0;
                }

                rv = pkcs11.C_DestroyObject(session, objectId2);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11.C_DestroyObject(session, objectId1);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11.C_Logout(session);
                if (rv != CKR.CKR_OK)
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

