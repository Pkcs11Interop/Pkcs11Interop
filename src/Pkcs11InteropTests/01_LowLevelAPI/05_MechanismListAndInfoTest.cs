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
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// C_GetMechanismList and C_GetMechanismInfo tests.
    /// </summary>
    [TestFixture()]
    public class MechanismListAndInfoTest
    {
        /// <summary>
        /// Basic C_GetMechanismList and C_GetMechanismInfo test.
        /// </summary>
        [Test()]
        public void BasicMechanismListAndInfoTest()
        {
            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(null);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                // Get number of supported mechanisms in first call
                uint mechanismCount = 0;
                rv = pkcs11.C_GetMechanismList(slotId, null, ref mechanismCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                Assert.IsTrue(mechanismCount > 0);
                
                // Allocate array for supported mechanisms
                CKM[] mechanismList = new CKM[mechanismCount];
                
                // Get supported mechanisms in second call
                rv = pkcs11.C_GetMechanismList(slotId, mechanismList, ref mechanismCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Analyze first supported mechanism
                CK_MECHANISM_INFO mechanismInfo = new CK_MECHANISM_INFO();
                rv = pkcs11.C_GetMechanismInfo(slotId, mechanismList[0], ref mechanismInfo);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with mechanism info
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

