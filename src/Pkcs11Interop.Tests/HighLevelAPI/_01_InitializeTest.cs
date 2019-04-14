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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Pkcs11 constructor, dispose, initialize and finalize tests.
    /// </summary>
    [TestFixture()]
    public class _01_InitializeTest
    {
        /// <summary>
        /// Basic constructor and dispose test.
        /// </summary>
        [Test()]
        public void _01_BasicPkcs11DisposeTest()
        {
            // Unmanaged PKCS#11 library is usually loaded by the constructor of class implementing IPkcs11 interface.
            // Every PKCS#11 library needs to be initialized with C_Initialize method which is also called automatically by the constructor mentioned above.
            IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType);

            // Do something  interesting

            // Unmanaged PKCS#11 library is usually unloaded by Dispose() method of class implementing IPkcs11 interface.
            // C_Finalize should be the last call made by an application and it is also called automatically by Dispose() method mentioned above.
            pkcs11.Dispose();
        }
        
        /// <summary>
        /// Using statement test.
        /// </summary>
        [Test()]
        public void _02_UsingPkcs11DisposeTest()
        {
            // Instance of class implementing IPkcs11 interface can be used in using statement 
            // which defines a scope at the end of which an object will be disposed.
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
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
            // If an application will not be accessing PKCS#11 library from multiple threads simultaneously, 
            // it should specify "AppType.SingleThreaded" as a value of "appType" parameter.
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, AppType.SingleThreaded))
            {
                // Do something interesting
            }
        }
        
        /// <summary>
        /// Example for multi-threaded applications.
        /// </summary>
        [Test()]
        public void _04_MultiThreadedInitializeTest()
        {
            // If an application will be accessing PKCS#11 library from multiple threads simultaneously,
            // it should specify "AppType.MultiThreaded" as a value of "appType" parameter.
            // PKCS#11 library will use the native operation system threading model for locking.
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, AppType.MultiThreaded))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for libraries that support C_GetFunctionList()
        /// </summary>
        [Test()]
        public void _05_Pkcs11WithGetFunctionListTest()
        {
            // Before an application can perform any cryptographic operations with PKCS#11 library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native function - GetProcAddress() on Windows and dlsym() on Unix.
            // InitType enum can be used to specify which approach should be used.
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType, InitType.WithFunctionList))
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
            // Before an application can perform any cryptographic operations with PKCS#11 library 
            // it has to obtain function pointers for all the Cryptoki API routines present in the library.
            // This can be done either via C_GetFunctionList() function or via platform specific native function - GetProcAddress() on Windows and dlsym() on Unix.
            // InitType enum can be used to specify which approach should be used.
            using (IPkcs11Library pkcs11 = Settings.Factories.Pkcs11Factory.CreatePkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType, InitType.WithoutFunctionList))
            {
                // Do something interesting
            }
        }
    }
}
