/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
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
            Helpers.CheckPlatform();

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            byte[] recoveredValue = null;
            
            IntPtr ptr = IntPtr.Zero;
            ptr = UnmanagedMemory.Allocate(originalValue.Length);
            Assert.IsTrue(ptr != IntPtr.Zero);

            UnmanagedMemory.Write(ptr, originalValue);
            recoveredValue = UnmanagedMemory.Read(ptr, originalValue.Length);
            Assert.IsTrue(recoveredValue != null);
            Assert.IsTrue(ConvertUtils.BytesToBase64String(originalValue) == ConvertUtils.BytesToBase64String(recoveredValue));

            UnmanagedMemory.Free(ref ptr);
            Assert.IsTrue(ptr == IntPtr.Zero);
        }
    }
}

