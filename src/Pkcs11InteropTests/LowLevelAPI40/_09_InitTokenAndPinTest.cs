/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI40;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
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
            if (Platform.UnmanagedLongSize != 4 && Platform.StructPackingSize != 0)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(Settings.InitArgs40);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                rv = pkcs11.C_GetTokenInfo(slotId, ref tokenInfo);
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
                    rv = pkcs11.C_InitToken(slotId, Settings.SecurityOfficerPinArray, Convert.ToUInt32(Settings.SecurityOfficerPinArray.Length), label);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Open RW session
                    uint session = CK.CK_INVALID_HANDLE;
                    rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Login as SO (security officer)
                    rv = pkcs11.C_Login(session, CKU.CKU_SO, Settings.SecurityOfficerPinArray, Convert.ToUInt32(Settings.SecurityOfficerPinArray.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Initialize user pin
                    rv = pkcs11.C_InitPIN(session, Settings.NormalUserPinArray, Convert.ToUInt32(Settings.NormalUserPinArray.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    rv = pkcs11.C_Logout(session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    rv = pkcs11.C_CloseSession(session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

