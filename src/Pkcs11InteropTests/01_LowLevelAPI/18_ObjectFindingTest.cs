/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// C_FindObjectsInit, C_FindObjects and C_FindObjectsFinal tests.
    /// </summary>
    [TestFixture()]
    public class ObjectFindingTest
    {
        /// <summary>
        /// Basic C_FindObjectsInit, C_FindObjects and C_FindObjectsFinal test.
        /// </summary>
        [Test()]
        public void BasicObjectFindingTest()
        {
            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(null);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, (uint)Settings.NormalUserPinArray.Length);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Let's create two objects so we can find something
                uint objectId1 = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId1);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                uint objectId2 = CK.CK_INVALID_HANDLE;
                rv = Helpers.CreateDataObject(pkcs11, session, ref objectId2);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Prepare attribute template that defines search criteria
                CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[2];
                template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA);
                template[1] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);

                // Initialize searching
                rv = pkcs11.C_FindObjectsInit(session, template, (uint)template.Length);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get search results
                uint foundObjectCount = 0;
                uint[] foundObjectIds = new uint[2];
                foundObjectIds[0] = CK.CK_INVALID_HANDLE;
                foundObjectIds[1] = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_FindObjects(session, foundObjectIds, (uint)foundObjectIds.Length, ref foundObjectCount);
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

