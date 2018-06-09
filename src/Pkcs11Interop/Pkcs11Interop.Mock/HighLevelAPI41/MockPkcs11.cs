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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI41
{
    /// <summary>
    /// High level PKCS#11 wrapper extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockPkcs11 : Net.Pkcs11Interop.HighLevelAPI41.Pkcs11, IMockPkcs11
    {
        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        public MockPkcs11(Pkcs11Factories factories, string libraryPath, AppType appType)
            : base(factories)
        {
            try
            {
                _p11 = new LowLevelAPI41.MockPkcs11(libraryPath);
                Initialize(appType);
            }
            catch
            {
                _p11.Dispose();
                _p11 = null;
                throw;
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <param name="initType">Source of PKCS#11 function pointers</param>
        public MockPkcs11(Pkcs11Factories factories, string libraryPath, AppType appType, InitType initType)
            : base(factories)
        {
            try
            {
                _p11 = new LowLevelAPI41.MockPkcs11(libraryPath, (initType == InitType.WithFunctionList));
                Initialize(appType);
            }
            catch
            {
                _p11.Dispose();
                _p11 = null;
                throw;
            }
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// </summary>
        /// <returns>List of unmanaged struct sizes</returns>
        public List<ulong> GetUnmanagedStructSizeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            NativeULong sizeCount = 0;
            CKR rv = ((LowLevelAPI41.MockPkcs11)_p11).C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            NativeULong[] sizeList = new NativeULong[sizeCount];
            rv = ((LowLevelAPI41.MockPkcs11)_p11).C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != ConvertUtils.UInt32ToInt32(sizeCount))
                Array.Resize(ref sizeList, ConvertUtils.UInt32ToInt32(sizeCount));

            ulong[] ulongList = Array.ConvertAll(sizeList, p => ConvertUtils.UInt32ToUInt64(p));

            return new List<ulong>(ulongList);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
