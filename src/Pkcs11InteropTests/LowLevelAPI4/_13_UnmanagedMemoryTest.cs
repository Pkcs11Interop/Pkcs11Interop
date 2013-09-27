/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI4
{
    /// <summary>
    /// Unmanaged memory tests.
    /// </summary>
    [TestFixture()]
    public class _13_UnmanagedMemoryTest
    {
        /// <summary>
        /// Allocate, Write, Read and Free test.
        /// </summary>
        [Test()]
        public void _01_AllocateAndFreeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 4, "Test cannot be executed on this platform");

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            byte[] recoveredValue = null;
            
            IntPtr ptr = IntPtr.Zero;
            ptr = Common.UnmanagedMemory.Allocate(originalValue.Length);
            Assert.IsTrue(ptr != IntPtr.Zero);

            Common.UnmanagedMemory.Write(ptr, originalValue);
            recoveredValue = Common.UnmanagedMemory.Read(ptr, originalValue.Length);
            Assert.IsTrue(recoveredValue != null);
            Assert.IsTrue(Convert.ToBase64String(originalValue) == Convert.ToBase64String(recoveredValue));

            Common.UnmanagedMemory.Free(ref ptr);
            Assert.IsTrue(ptr == IntPtr.Zero);
        }
    }
}

