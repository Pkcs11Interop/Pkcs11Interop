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
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
{
    /// <summary>
    /// C_DeriveKey tests.
    /// </summary>
    [TestFixture()]
    public class _26_DeriveKeyTest
    {
        /// <summary>
        /// C_DeriveKey test.
        /// </summary>
        [Test()]
        public void _01_BasicDeriveKeyTest()
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

                // Open RW session
                NativeULong session = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11Library.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, ConvertUtils.UInt32FromInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                NativeULong baseKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11Library, session, ref baseKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Generate random data needed for key derivation
                byte[] data = new byte[24];
                rv = pkcs11Library.C_GenerateRandom(session, data, ConvertUtils.UInt32FromInt32(data.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify mechanism parameters
                // Note that we are allocating unmanaged memory that will have to be freed later
                CK_KEY_DERIVATION_STRING_DATA mechanismParams = new CK_KEY_DERIVATION_STRING_DATA();
                mechanismParams.Data = UnmanagedMemory.Allocate(data.Length);
                UnmanagedMemory.Write(mechanismParams.Data, data);
                mechanismParams.Len = ConvertUtils.UInt32FromInt32(data.Length);

                // Specify derivation mechanism with parameters
                // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, mechanismParams);

                // Derive key
                NativeULong derivedKey = CK.CK_INVALID_HANDLE;
                rv = pkcs11Library.C_DeriveKey(session, ref mechanism, baseKeyId, null, 0, ref derivedKey);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with derived key
                Assert.IsTrue(derivedKey != CK.CK_INVALID_HANDLE);

                // In LowLevelAPI we have to free all unmanaged memory we previously allocated
                UnmanagedMemory.Free(ref mechanismParams.Data);
                mechanismParams.Len = 0;

                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter
                UnmanagedMemory.Free(ref mechanism.Parameter);
                mechanism.ParameterLen = 0;

                rv = pkcs11Library.C_DestroyObject(session, baseKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_DestroyObject(session, derivedKey);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11Library.C_Logout(session);
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

