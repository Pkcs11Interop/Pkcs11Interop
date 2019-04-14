/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI41;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI41
{
    /// <summary>
    /// C_GetInfo tests.
    /// </summary>
    [TestFixture()]
    public class _02_GetInfoTest
    {
        /// <summary>
        /// Basic C_GetInfo test.
        /// </summary>
        [Test()]
        public void _01_BasicGetInfoTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11Library.C_Initialize(Settings.InitArgs41);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                CK_INFO info = new CK_INFO();
                rv = pkcs11Library.C_GetInfo(ref info);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Do something interesting with library information
                Assert.IsFalse(String.IsNullOrEmpty(ConvertUtils.BytesToUtf8String(info.ManufacturerId)));
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

