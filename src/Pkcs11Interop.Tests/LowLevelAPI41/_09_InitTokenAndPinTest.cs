/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
    /// C_InitToken and C_InitPIN tests.
    /// </summary>
    [TestFixture()]
    public class _09_InitTokenAndPinTest
    {
        /// <summary>
        /// Basic C_InitToken and C_InitPIN test.
        /// </summary>
        [Test()]
        public void _01_BasicInitTokenAndPinTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                NativeULong slotId = Helpers.GetUsableSlot(pkcs11Library);
                
                CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                rv = pkcs11Library.C_GetTokenInfo(slotId, ref tokenInfo);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Check if token needs to be initialized
                if ((tokenInfo.Flags & CKF.CKF_TOKEN_INITIALIZED) != CKF.CKF_TOKEN_INITIALIZED)
                {
                    // Token label is 32 bytes long string padded with blank characters
                    byte[] label = new byte[32];
                    for (int i = 0; i < label.Length; i++)
                        label[i] = 0x20;
                    Array.Copy(Settings.ApplicationNameArray, 0, label, 0, Settings.ApplicationNameArray.Length);
                    
                    // Initialize token and SO (security officer) pin
                    rv = pkcs11Library.C_InitToken(slotId, Settings.SecurityOfficerPinArray, ConvertUtils.UInt32FromInt32(Settings.SecurityOfficerPinArray.Length), label);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Open RW session
                    NativeULong session = CK.CK_INVALID_HANDLE;
                    rv = pkcs11Library.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Login as SO (security officer)
                    rv = pkcs11Library.C_Login(session, CKU.CKU_SO, Settings.SecurityOfficerPinArray, ConvertUtils.UInt32FromInt32(Settings.SecurityOfficerPinArray.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Initialize user pin
                    rv = pkcs11Library.C_InitPIN(session, Settings.NormalUserPinArray, ConvertUtils.UInt32FromInt32(Settings.NormalUserPinArray.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    rv = pkcs11Library.C_Logout(session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    rv = pkcs11Library.C_CloseSession(session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

