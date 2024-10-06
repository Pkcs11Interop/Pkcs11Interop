/*
 *  Copyright 2012-2024 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// Pkcs11Library construct, dispose, initialize and finalize tests.
    /// </summary>
    [TestFixture()]
    public class _01_InitializeTest
    {
        /// <summary>
        /// Basic construct and dispose test.
        /// </summary>
        [Test()]
        public void _01_BasicPkcs11DisposeTest()
        {
            Helpers.CheckPlatform();

            // Unmanaged PKCS#11 library is loaded by the constructor of Pkcs11Library class
            // and unloaded by Dispose() method.
            Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath);
            
            // Do something  interesting
            
            pkcs11Library.Dispose();
        }
        
        /// <summary>
        /// Using statement test.
        /// </summary>
        [Test()]
        public void _02_UsingPkcs11DisposeTest()
        {
            Helpers.CheckPlatform();

            // Pkcs11Library class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed.
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for single-threaded applications.
        /// </summary>
        [Test()]
        public void _03_SingleThreadedInitializeTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                // PKCS#11 library needs to be initialized with C_Initialize method.
                // If an application will not be accessing PKCS#11 library from multiple threads
                // simultaneously, it can generally call C_Initialize with initArgs parameter set to null.
                rv = pkcs11Library.C_Initialize(null);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Do something interesting
                
                // C_Finalize is called to indicate that an application is finished
                // with the PKCS#11 library. It should be the last call made by an application.
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// Example for multi-threaded applications.
        /// </summary>
        [Test()]
        public void _04_MultiThreadedInitializeTest()
        {
            Helpers.CheckPlatform();

            CKR rv = CKR.CKR_OK;

            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                // If an application will be accessing PKCS#11 library from multiple threads
                // simultaneously, it has to provide initArgs parameter to C_Initialize method.
                // The easiest way is to set CKF_OS_LOCKING_OK flag, which will indicate that 
                // PKCS#11 library can use the native operation system threading model for locking.
                CK_C_INITIALIZE_ARGS initArgs = new CK_C_INITIALIZE_ARGS();
                initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
                
                rv = pkcs11Library.C_Initialize(initArgs);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Do something interesting
                
                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// Example for libraries that support C_GetFunctionList()
        /// </summary>
        [Test()]
        public void _05_Pkcs11WithGetFunctionListTest()
        {
            Helpers.CheckPlatform();

            // Before an application can perform any cryptographic operations with Cryptoki library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native 
            // function - GetProcAddress() on Windows and dlsym() on Unix.
            // The most simple constructor of Pkcs11Library class uses C_GetFunctionList() approach 
            // but Pkcs11Interop also provides an alternative constructor 
            // that can specify which approach should be used.
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath, true))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for libraries that do not support C_GetFunctionList()
        /// </summary>
        [Test()]
        public void _06_Pkcs11WithoutGetFunctionListTest()
        {
            Helpers.CheckPlatform();

            // Before an application can perform any cryptographic operations with Cryptoki library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native 
            // function - GetProcAddress() on Windows and dlsym() on Unix.
            // The most simple constructor of Pkcs11Library class uses C_GetFunctionList() approach 
            // but Pkcs11Interop also provides an alternative constructor 
            // that can specify which approach should be used.
            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath, false))
            {
                // Do something interesting
            }
        }
    }
}

