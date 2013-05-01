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
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
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
            // Unmanaged PKCS#11 library is loaded by the constructor of Pkcs11 class
            // and unloaded by Dispose() method.
            Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath);
            
            // Do something  interesting
            
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
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
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
            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                // PKCS#11 library needs to be initialized with C_Initialize method.
                // If an application will not be accessing PKCS#11 library from multiple threads
                // simultaneously, it can generally call C_Initialize with initArgs parameter set to null.
                rv = pkcs11.C_Initialize(null);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Do something interesting
                
                // C_Finalize is called to indicate that an application is finished
                // with the PKCS#11 library. It should be the last call made by an application.
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }

        /// <summary>
        /// Example for multi-threaded applications.
        /// </summary>
        [Test()]
        public void MultiThreadedInitializeTest()
        {
            CKR rv = CKR.CKR_OK;

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                // If an application will be accessing PKCS#11 library from multiple threads
                // simultaneously, it has to provide initArgs parameter to C_Initialize method.
                // The easiest way is to set CKF_OS_LOCKING_OK flag, which will indicate that 
                // PKCS#11 library can use the native operation system threading model for locking.
                CK_C_INITIALIZE_ARGS initArgs = new CK_C_INITIALIZE_ARGS();
                initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
                
                rv = pkcs11.C_Initialize(initArgs);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Do something interesting
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

