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
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI81
{
    /// <summary>
    /// C_SignInit, C_Sign, C_SignUpdate, C_SignFinal, C_VerifyInit, C_Verify, C_VerifyUpdate and C_VerifyFinal tests.
    /// </summary>
    [TestFixture()]
    public class _21_SignAndVerifyTest
    {
        /// <summary>
        /// C_SignInit, C_Sign, C_VerifyInit and C_Verify test with CKM_RSA_PKCS mechanism.
        /// </summary>
        [Test()]
        public void _01_SignAndVerifySinglePartTest()
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
                
                // Generate asymetric key pair
                ulong pubKeyId = CK.CK_INVALID_HANDLE;
                ulong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                // Initialize signing operation
                rv = pkcs11.C_SignInit(session, ref mechanism, privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");

                // Get length of signature in first call
                ulong signatureLen = 0;
                rv = pkcs11.C_Sign(session, sourceData, Convert.ToUInt64(sourceData.Length), null, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(signatureLen > 0);
                
                // Allocate array for signature
                byte[] signature = new byte[signatureLen];

                // Get signature in second call
                rv = pkcs11.C_Sign(session, sourceData, Convert.ToUInt64(sourceData.Length), signature, ref signatureLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with signature

                // Initialize verification operation
                rv = pkcs11.C_VerifyInit(session, ref mechanism, pubKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Verify signature
                rv = pkcs11.C_Verify(session, sourceData, Convert.ToUInt64(sourceData.Length), signature, Convert.ToUInt64(signature.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with verification result

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

        /// <summary>
        /// C_SignInit, C_SignUpdate, C_SignFinal, C_VerifyInit, C_VerifyUpdate and C_VerifyFinal test.
        /// </summary>
        [Test()]
        public void _02_SignAndVerifyMultiPartTest()
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
                
                // Generate asymetric key pair
                ulong pubKeyId = CK.CK_INVALID_HANDLE;
                ulong privKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKeyPair(pkcs11, session, ref pubKeyId, ref privKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Specify signing mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA1_RSA_PKCS);

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                byte[] signature = null;

                // Multipart signature functions C_SignUpdate and C_SignFinal can be used i.e. for signing of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData))
                {
                    // Initialize signing operation
                    rv = pkcs11.C_SignInit(session, ref mechanism, privKeyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];

                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        rv = pkcs11.C_SignUpdate(session, part, Convert.ToUInt64(bytesRead));
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                    }

                    // Get the length of signature in first call
                    ulong signatureLen = 0;
                    rv = pkcs11.C_SignFinal(session, null, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    Assert.IsTrue(signatureLen > 0);
                    
                    // Allocate array for signature
                    signature = new byte[signatureLen];

                    // Get signature in second call
                    rv = pkcs11.C_SignFinal(session, signature, ref signatureLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }

                // Do something interesting with signature

                // Multipart verification functions C_VerifyUpdate and C_VerifyFinal can be used i.e. for signature verification of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData))
                {
                    // Initialize verification operation
                    rv = pkcs11.C_VerifyInit(session, ref mechanism, pubKeyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    
                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        rv = pkcs11.C_VerifyUpdate(session, part, Convert.ToUInt64(bytesRead));
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                    }

                    // Verify signature
                    rv = pkcs11.C_VerifyFinal(session, signature, Convert.ToUInt64(signature.Length));
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }

                // Do something interesting with verification result

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

