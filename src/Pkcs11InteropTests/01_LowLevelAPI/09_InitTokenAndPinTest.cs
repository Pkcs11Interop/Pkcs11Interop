/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// C_InitToken and C_InitPIN tests.
    /// </summary>
    [TestFixture()]
    public class InitTokenAndPinTest
    {
        /// <summary>
        /// Basic C_InitToken and C_InitPIN test.
        /// </summary>
        [Test()]
        public void BasicInitTokenAndPinTest()
        {
            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(null);
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
                    rv = pkcs11.C_InitToken(slotId, Settings.SecurityOfficerPinArray, (uint)Settings.SecurityOfficerPinArray.Length, label);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Open RW session
                    uint session = CK.CK_INVALID_HANDLE;
                    rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Login as SO (security officer)
                    rv = pkcs11.C_Login(session, CKU.CKU_SO, Settings.SecurityOfficerPinArray, (uint)Settings.SecurityOfficerPinArray.Length);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Initialize user pin
                    rv = pkcs11.C_InitPIN(session, Settings.NormalUserPinArray, (uint)Settings.NormalUserPinArray.Length);
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

