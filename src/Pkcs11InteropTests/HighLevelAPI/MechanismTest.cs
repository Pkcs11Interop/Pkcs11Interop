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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using System.Reflection;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Mechanism tests.
    /// </summary>
    [TestFixture()]
    public class MechanismTest
    {
        /// <summary>
        /// Mechanism with empty parameter test.
        /// </summary>
        [Test()]
        public void EmptyParameterTest()
        {
            // Create mechanism without the parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_RSA_PKCS);
            Assert.IsTrue(mechanism.Type == (uint)CKM.CKM_RSA_PKCS);

            // We access private Mechanism member just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (uint)CKM.CKM_RSA_PKCS);
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with byte array parameter test.
        /// </summary>
        [Test()]
        public void ByteArrayParameterTest()
        {
            byte[] parameter = new byte[16];
            System.Random rng = new Random();
            rng.NextBytes(parameter);
            
            // Create mechanism with the byte array parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == (uint)CKM.CKM_AES_CBC);

            // We access private Mechanism member here just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (uint)CKM.CKM_AES_CBC);
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == parameter.Length);

            parameter = null;
            
            // Create mechanism with null byte array parameter
            mechanism = new Mechanism(CKM.CKM_AES_CBC, parameter);
            Assert.IsTrue(mechanism.Type == (uint)CKM.CKM_AES_CBC);

            // We access private Mechanism member here just for the testing purposes
            ckMechanism = (Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (uint)CKM.CKM_AES_CBC);
            Assert.IsTrue(ckMechanism.Parameter == IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == 0);
        }

        /// <summary>
        /// Mechanism with object as parameter test.
        /// </summary>
        [Test()]
        public void ObjectParameterTest()
        {
            byte[] data = new byte[24];
            System.Random rng = new Random();
            rng.NextBytes(data);

            // Specify mechanism parameters
            CkKeyDerivationStringData parameter = new CkKeyDerivationStringData();
            parameter.Data = data;

            // Create mechanism with the object as parameter
            Mechanism mechanism = new Mechanism(CKM.CKM_XOR_BASE_AND_DATA, parameter);
            Assert.IsTrue(mechanism.Type == (uint)CKM.CKM_XOR_BASE_AND_DATA);

            // We access private Mechanism member here just for the testing purposes
            Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM ckMechanism = (Net.Pkcs11Interop.LowLevelAPI.CK_MECHANISM)typeof(Mechanism).GetField("_ckMechanism", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mechanism);
            Assert.IsTrue(ckMechanism.Mechanism == (uint)CKM.CKM_XOR_BASE_AND_DATA);
            Assert.IsTrue(ckMechanism.Parameter != IntPtr.Zero);
            Assert.IsTrue(ckMechanism.ParameterLen == Net.Pkcs11Interop.LowLevelAPI.UnmanagedMemory.SizeOf(typeof(Net.Pkcs11Interop.LowLevelAPI.MechanismParams.CK_KEY_DERIVATION_STRING_DATA)));
        }
    }
}
