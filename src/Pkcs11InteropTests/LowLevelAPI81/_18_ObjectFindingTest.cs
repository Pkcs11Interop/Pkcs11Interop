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

