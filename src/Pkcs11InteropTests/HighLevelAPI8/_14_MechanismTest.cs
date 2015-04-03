/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
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
using System.Reflection;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using Net.Pkcs11Interop.HighLevelAPI8.MechanismParams;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
{
    /// <summary>
    /// Mechanism tests.
    /// </summary>
    [TestFixture()]
    public class _14_MechanismTest
    {
        /// <summary>
        /// Mechanism dispose test.
        /// </summary>
        [Test()]
        public void _01_DisposeMechanismTest()
        {
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] parameter = new byte[8];
            System.Random rng = new Random();
            rng.NextBytes(parameter);
            
            // Unmanaged memory for mechanism parameter stored in low level CK_MECHANISM struct
            // is allocated by constructor of Mechanism class.
            Mechanism mechanism1 = new Mechanism(CKM.CKM_DES_CBC, parameter);
            
            // Do something interesting with mechanism
            
            // This unmanaged memory is freed by Dispose() method.
            mechanism1.Dispose();
            
            
            // Mechanism class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (Mechanism mechanism2 = new Mechanism(CKM.CKM_DES_CBC, parameter))
            {
                // Do something interesting with mechanism
            }


            #pragma warning disable 0219

            // Explicit calling of Dispose() method can also be ommitted
            // and this is the prefered way how to use Mechanism class.
            Mechanism mechanism3 = new Mechanism(CKM.CKM_DES_CBC, parameter);
            
            // Do something interesting with mechanism
            
            // Dispose() method will be called (and unmanaged memory freed) by GC eventually
            // but we cannot be sure when will this occur.

            #pragma warning restore 0219
        }

        /// <summary>
        /// Mechanism with empty parameter test.
        /// </summary>
        [Test()]
        public void _02_EmptyParameterTest()
        {
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            // Create mechanism without the parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Type == (ulong)CKM.CKM_RSA_PKCS);

            // We access private Mechanism member just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (ulong)CKM.CKM_RSA_PKCS);
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void _03_ByteArrayParameterTest()
        {
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);
            
            // Create mechanism with the byte array parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == (ulong)CKM.CKM_AES_CBC);

            // We access private Mechanism member here just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (ulong)CKM.CKM_AES_CBC);
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == (ulong)parameter.Length);

            parameter = null;
            
            // Create mechanism with null byte array parameter
            mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == (ulong)CKM.CKM_AES_CBC);

            // We access private Mechanism member here just for the testing purposes
            ckMechanism = (Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (ulong)CKM.CKM_AES_CBC);
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with object as parameter test.
        /// </summary>
        [Test()]
        public void _04_ObjectParameterTest()
        {
            if (Platform.UnmanagedLongSize != 8)
                Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            CkKeyDerivationStringData parameter = new CkKeyDerivationStringData(data);

            // Create mechanism with the object as parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Type == (ulong)CKM.CKM_XOR_BASE_AND_DATA);

            // We access private Mechanism member here just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI81.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (ulong)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(Convert.ToInt32(ckMechanism.ParameterLen) == Net.Pkcs11Interop.Common.UnmanagedMemory.SizeOf(typeof(Net.Pkcs11Interop.LowLevelAPI81.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
        }
    }
}
