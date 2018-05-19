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
using Net.Pkcs11Interop.LowLevelAPI41;
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
{
    /// <summary>
    /// C_GetAttributeValue and C_SetAttributeValue tests.
    /// </summary>
    [TestFixture()]
    public class _17_GetAndSetAttributeValueTest
    {
        /// <summary>
        /// C_GetAttributeValue test.
        /// </summary>
        [Test()]
        public void _01_GetAttributeValueTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeULongUtils.ConvertUInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Create object
                NativeULong objectId = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare list of empty attributes we want to read
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[2];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_LABEL);
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_VALUE);

                // Get size of each individual attribute value in first call
                rv = pkcs11.C_GetAttributeValue(session, objectId, template, NativeULongUtils.ConvertUInt32FromInt32(template.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // In LowLevelAPI we have to allocate unmanaged memory for attribute value
                for (int i = 0; i < template.Length; i++)
                    template[i].value = UnmanagedMemory.Allocate(NativeULongUtils.ConvertUInt32ToInt32(template[i].valueLen));

                // Get attribute value in second call
                rv = pkcs11.C_GetAttributeValue(session, objectId, template, NativeULongUtils.ConvertUInt32FromInt32(template.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with attribute value
                byte[] ckaLabel = UnmanagedMemory.Read(template[0].value, NativeULongUtils.ConvertUInt32ToInt32(template[0].valueLen));
                Assert.IsTrue(ConvertUtils.BytesToBase64String(ckaLabel) == ConvertUtils.BytesToBase64String(Settings.ApplicationNameArray));

                // In LowLevelAPI we have to free unmanaged memory taken by attributes
                for (int i = 0; i < template.Length; i++)
                {
                    UnmanagedMemory.Free(ref template[i].value);
                    template[i].valueLen = 0;
                }

                rv = pkcs11.C_DestroyObject(session, objectId);
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

        /// <summary>
        /// C_SetAttributeValue test.
        /// </summary>
        [Test()]
        public void _02_SetAttributeValueTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11);
                
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, NativeULongUtils.ConvertUInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Create object
                NativeULong objectId = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare list of attributes we want to set
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[2];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, "Hello world");
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_VALUE, "New data object content");

                // Set attributes
                rv = pkcs11.C_SetAttributeValue(session, objectId, template, NativeULongUtils.ConvertUInt32FromInt32(template.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // In LowLevelAPI we have to free unmanaged memory taken by attributes
                for (int i = 0; i < template.Length; i++)
                {
                    UnmanagedMemory.Free(ref template[i].value);
                    template[i].valueLen = 0;
                }
                
                rv = pkcs11.C_DestroyObject(session, objectId);
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

