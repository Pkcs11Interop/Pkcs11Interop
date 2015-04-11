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
    /// Pkcs11 construct, dispose, initialize and finalize tests.
    /// </summary>
    [TestFixture()]
    public partial class _01_InitializeTest
    {

#if TESTING_WITH_PKCS11_MOCK

        /// <summary>
        /// TODO
        /// </summary>
        [Test()]
        public void _07_StructSizeListTest()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            CKR rv = CKR.CKR_OK;

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                // Get sizes of unmanaged structs
                ulong unmanagedSizesCount = 0;
                rv = pkcs11.C_GetUnmanagedStructSizeList(null, ref unmanagedSizesCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(unmanagedSizesCount > 0);
                ulong[] unmanagedSizes = new ulong[unmanagedSizesCount];
                rv = pkcs11.C_GetUnmanagedStructSizeList(unmanagedSizes, ref unmanagedSizesCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Get sizes of managed structs
                ulong managedSizesCount = 0;
                rv = pkcs11.C_GetManagedStructSizeList(null, ref managedSizesCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                Assert.IsTrue(managedSizesCount > 0);
                ulong[] managedSizes = new ulong[managedSizesCount];
                rv = pkcs11.C_GetManagedStructSizeList(managedSizes, ref managedSizesCount);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Compare sizes of unmanaged and managed structs
                Assert.IsTrue(unmanagedSizes.Length == managedSizes.Length);
                for (int i = 0; i < unmanagedSizes.Length; i++)
                    Assert.IsTrue(unmanagedSizes[i] == managedSizes[i]);
            }
        }

#endif

    }
}
