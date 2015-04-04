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
    /// C_DigestInit, C_Digest, C_DigestUpdate, C_DigestFinal and C_DigestKey tests.
    /// </summary>
    [TestFixture()]
    public class _12_DigestTest
    {
        /// <summary>
        /// C_DigestInit and C_Digest test.
        /// </summary>
        [Test()]
        public void _01_DigestSinglePartTest()
        {
            if (Platform.UnmanagedLongSize != 8 && Platform.StructPackingSize != 1)
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

                // Specify digesting mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA_1);

                // Initialize digesting operation
                rv = pkcs11.C_DigestInit(session, ref mechanism);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");

                // Get length of digest value in first call
                ulong digestLen = 0;
                rv = pkcs11.C_Digest(session, sourceData, Convert.ToUInt64(sourceData.Length), null, ref digestLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(digestLen > 0);
                
                // Allocate array for digest value
                byte[] digest = new byte[digestLen];

                // Get digest value in second call
                rv = pkcs11.C_Digest(session, sourceData, Convert.ToUInt64(sourceData.Length), digest, ref digestLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with digest value
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_DigestInit, C_DigestUpdate and C_DigestFinal test.
        /// </summary>
        [Test()]
        public void _02_DigestMultiPartTest()
        {
            if (Platform.UnmanagedLongSize != 8 && Platform.StructPackingSize != 1)
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
                
                // Specify digesting mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA_1);

                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Hello world");
                byte[] digest = null;

                // Multipart digesting functions C_DigestUpdate and C_DigestFinal can be used i.e. for digesting of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData))
                {
                    // Initialize digesting operation
                    rv = pkcs11.C_DigestInit(session, ref mechanism);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];

                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Digest each individual source data part
                        rv = pkcs11.C_DigestUpdate(session, part, Convert.ToUInt64(bytesRead));
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                    }

                    // Get length of digest value in first call
                    ulong digestLen = 0;
                    rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());;

                    Assert.IsTrue(digestLen > 0);
                    
                    // Allocate array for digest value
                    digest = new byte[digestLen];

                    // Get digest value in second call
                    rv = pkcs11.C_DigestFinal(session, digest, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }

                // Do something interesting with digest value
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// C_DigestInit, C_DigestKey and C_DigestFinal test.
        /// </summary>
        [Test()]
        public void _03_DigestKeyTest() // TODO - Test on device that supports this method
        {
            if (Platform.UnmanagedLongSize != 8 && Platform.StructPackingSize != 1)
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
                
                // Generate symetric key
                ulong keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify digesting mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA_1);
                
                // Initialize digesting operation
                rv = pkcs11.C_DigestInit(session, ref mechanism);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Digest key
                rv = pkcs11.C_DigestKey(session, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get length of digest value in first call
                ulong digestLen = 0;
                rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());;
                
                Assert.IsTrue(digestLen > 0);
                
                // Allocate array for digest value
                byte[] digest = new byte[digestLen];
                
                // Get digest value in second call
                rv = pkcs11.C_DigestFinal(session, digest, ref digestLen);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Do something interesting with digest value

                rv = pkcs11.C_DestroyObject(session, keyId);
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

