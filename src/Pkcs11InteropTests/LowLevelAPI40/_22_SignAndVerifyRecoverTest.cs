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
    /// C_SignRecoverInit, C_SignRecover, C_VerifyRecoverInit and C_VerifyRecover tests.
    /// </summary>
    [TestFixture()]
    public class _22_SignAndVerifyRecoverTest
    {
        /// <summary>
        /// Basic C_SignRecoverInit, C_SignRecover, C_VerifyRecoverInit and C_VerifyRecover test.
        /// </summary>
        [Test()]
        public void _01_BasicSignAndVerifyRecoverTest()
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
                
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, Convert.ToUInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate asymetric key pair
                uint pubKeyId = CK.CK_INVALID_HANDLE;
                uint privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS);
                
                // Initialize signing operation
                rv = pkcs11.C_SignRecoverInit(session, ref mechanism, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                
                // Get length of signature in first call
                uint signatureLen = 0;
                rv = pkcs11.C_SignRecover(session, sourceData, Convert.ToUInt32(sourceData.Length), null, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(signatureLen > 0);
                
                // Allocate array for signature
                byte[] signature = new byte[signatureLen];
                
                // Get signature in second call
                rv = pkcs11.C_SignRecover(session, sourceData, Convert.ToUInt32(sourceData.Length), signature, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with signature
                
                // Initialize verification operation
                rv = pkcs11.C_VerifyRecoverInit(session, ref mechanism, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get length of recovered data in first call
                uint recoveredDataLen = 0;
                rv = pkcs11.C_VerifyRecover(session, signature, Convert.ToUInt32(signature.Length), null, ref recoveredDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(recoveredDataLen > 0);
                
                // Allocate array for recovered data
                byte[] recoveredData = new byte[recoveredDataLen];

                // Verify signature and get recovered data in second call
                rv = pkcs11.C_VerifyRecover(session, signature, Convert.ToUInt32(signature.Length), recoveredData, ref recoveredDataLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with verification result and recovered data
                Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(recoveredData));

                rv = pkcs11.C_DestroyObject(session, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_DestroyObject(session, pubKeyId);
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
