/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using Net.Pkcs11Interop.Common;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI81
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
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

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

