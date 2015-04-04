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
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NUnit.Framework;

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

                // Open RW session
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, Convert.ToUInt32(Settings.NormalUserPinArray.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                uint baseKeyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref baseKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Generate random data needed for key derivation
                byte[] data = new byte[24];
                rv = pkcs11.C_GenerateRandom(session, data, Convert.ToUInt32(data.Length));
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify mechanism parameters
                // Note that we are allocating unmanaged memory that will have to be freed later
                CK_KEY_DERIVATION_STRING_DATA mechanismParams = new CK_KEY_DERIVATION_STRING_DATA();
                mechanismParams.Data = UnmanagedMemory.Allocate(data.Length);
                UnmanagedMemory.Write(mechanismParams.Data, data);
                mechanismParams.Len = Convert.ToUInt32(data.Length);

                // Specify derivation mechanism with parameters
                // Note that CkmUtils.CreateMechanism() automaticaly copies mechanismParams into newly allocated unmanaged memory
                CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_XOR_BASE_AND_DATA, mechanismParams);

                // Derive key
                uint derivedKey = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_DeriveKey(session, ref mechanism, baseKeyId, null, 0, ref derivedKey);
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

                rv = pkcs11.C_DestroyObject(session, baseKeyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                rv = pkcs11.C_DestroyObject(session, derivedKey);
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

