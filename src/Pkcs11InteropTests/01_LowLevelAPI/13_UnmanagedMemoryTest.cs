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
using Net.Pkcs11Interop.LowLevelAPI;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// Unmanaged memory tests.
    /// </summary>
    [TestFixture()]
    public class UnmanagedMemoryTest
    {
        /// <summary>
        /// Allocate, Write, Read and Free test.
        /// </summary>
        [Test()]
        public void AllocateAndFreeTest()
        {
            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            byte[] recoveredValue = null;
            
            IntPtr ptr = IntPtr.Zero;
            ptr = UnmanagedMemory.Allocate(originalValue.Length);
            Assert.IsTrue(ptr != IntPtr.Zero);
            
            UnmanagedMemory.Write(ptr, originalValue);
            recoveredValue = UnmanagedMemory.Read(ptr, originalValue.Length);
            Assert.IsTrue(recoveredValue != null);
            Assert.IsTrue(Convert.ToBase64String(originalValue) == Convert.ToBase64String(recoveredValue));
            
            UnmanagedMemory.Free(ref ptr);
            Assert.IsTrue(ptr == IntPtr.Zero);
        }
    }
}

