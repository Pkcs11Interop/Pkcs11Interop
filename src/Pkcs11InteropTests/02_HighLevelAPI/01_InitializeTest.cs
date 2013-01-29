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
using Net.Pkcs11Interop.HighLevelAPI;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Pkcs11 construct, dispose, initialize and finalize tests.
    /// </summary>
    [TestFixture()]
    public class InitializeTest
    {
        /// <summary>
        /// Basic construct and dispose test.
        /// </summary>
        [Test()]
        public void BasicPkcs11DisposeTest()
        {
            // Unmanaged PKCS#11 library is loaded by the constructor of Pkcs11 class.
            // Every PKCS#11 library needs to be initialized with C_Initialize method
            // which is also called automatically by the constructor of Pkcs11 class.
            Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false);
            
            // Do something  interesting
            
            // Unmanaged PKCS#11 library is unloaded by Dispose() method.
            // C_Finalize should be the last call made by an application and it
            // is also called automatically by Dispose() method.
            pkcs11.Dispose();
        }
        
        /// <summary>
        /// Using statement test.
        /// </summary>
        [Test()]
        public void UsingPkcs11DisposeTest()
        {
            // Pkcs11 class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Do something interesting
            }
        }

        /// <summary>
        /// Example for single-threaded applications.
        /// </summary>
        [Test()]
        public void SingleThreadedInitializeTest()
        {
            // If an application will not be accessing PKCS#11 library from multiple threads
            // simultaneously, it should specify "false" as a value of "useOsLocking" parameter.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Do something interesting
            }
        }
        
        /// <summary>
        /// Example for multi-threaded applications.
        /// </summary>
        [Test()]
        public void MultiThreadedInitializeTest()
        {
            // If an application will be accessing PKCS#11 library from multiple threads
            // simultaneously, it should specify "true" as a value of "useOsLocking" parameter.
            // PKCS#11 library will use the native operation system threading model for locking.
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, true))
            {
                // Do something interesting
            }
        }
    }
}

