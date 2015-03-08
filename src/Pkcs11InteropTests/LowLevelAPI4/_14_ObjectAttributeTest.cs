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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI4;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI4
{
    /// <summary>
    /// Object attributes tests.
    /// </summary>
    [TestFixture()]
    public class _14_ObjectAttributeTest
    {
        /// <summary>
        /// Attribute with empty value test.
        /// </summary>
        [Test()]
        public void _01_EmptyAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            // Create attribute without the value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_CLASS);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_CLASS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with uint value test.
        /// </summary>
        [Test()]
        public void _02_UintAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            uint originalValue = (uint)CKO.CKO_DATA;
            // Create attribute with uint value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_CLASS, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_CLASS);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == UnmanagedMemory.SizeOf(typeof(uint)));
            
            uint recoveredValue = 0;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_CLASS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with bool value test.
        /// </summary>
        [Test()]
        public void _03_BoolAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            bool originalValue = true;
            // Create attribute with bool value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_TOKEN);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 1);
            
            bool recoveredValue = false;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_TOKEN);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with string value test.
        /// </summary>
        [Test()]
        public void _04_StringAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            string originalValue = "Hello world";
            // Create attribute with string value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_LABEL, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_LABEL);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == originalValue.Length);
            
            string recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_LABEL);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null string value
            attr = CkaUtils.CreateAttribute(CKA.CKA_LABEL, (string)null);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_LABEL);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with byte array value test.
        /// </summary>
        [Test()]
        public void _05_ByteArrayAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            // Create attribute with byte array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ID, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ID);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == originalValue.Length);
            
            byte[] recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(Convert.ToBase64String(originalValue) == Convert.ToBase64String(recoveredValue));

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ID);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null byte array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ID, (byte[])null);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ID);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with DateTime (CKA_DATE) value test.
        /// </summary>
        [Test()]
        public void _06_DateTimeAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            DateTime originalValue = new DateTime(2012, 1, 30, 0, 0, 0, DateTimeKind.Utc);
            // Create attribute with DateTime value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_START_DATE, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_START_DATE);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 8);
            
            DateTime? recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_START_DATE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void _07_AttributeArrayAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            CK_ATTRIBUTE[] originalValue = new CK_ATTRIBUTE[2];
            originalValue[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            originalValue[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, true);
            // Create attribute with attribute array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_WRAP_TEMPLATE);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == (UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE)) * originalValue.Length));

            CK_ATTRIBUTE[] recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue.Length == recoveredValue.Length);
            for (int i = 0; i < recoveredValue.Length; i++)
            {
                Assert.IsTrue(originalValue[i].type == recoveredValue[i].type);
                Assert.IsTrue(originalValue[i].valueLen == recoveredValue[i].valueLen);
                
                bool originalBool = false;
                // Read the value of nested attribute
                CkaUtils.ConvertValue(ref originalValue[i], out originalBool);

                bool recoveredBool = true;
                // Read the value of nested attribute
                CkaUtils.ConvertValue(ref recoveredValue[i], out recoveredBool);
                Assert.IsTrue(originalBool == recoveredBool);

                // In this example there is the same pointer to unmanaged memory 
                // in both originalValue and recoveredValue array therefore it
                // needs to be freed only once.
                Assert.IsTrue(originalValue[i].value == recoveredValue[i].value);
                // Free value of nested attributes
                UnmanagedMemory.Free(ref originalValue[i].value);
                originalValue[i].valueLen = 0;
                recoveredValue[i].value = IntPtr.Zero;
                recoveredValue[i].valueLen = 0;
            }

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_WRAP_TEMPLATE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null attribute array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, (CK_ATTRIBUTE[])null);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_WRAP_TEMPLATE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with empty attribute array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, new CK_ATTRIBUTE[0]);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_WRAP_TEMPLATE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with uint array value test.
        /// </summary>
        [Test()]
        public void _08_UintArrayAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            uint[] originalValue = new uint[2];
            originalValue[0] = 333333;
            originalValue[1] = 666666;
            // Create attribute with uint array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == (UnmanagedMemory.SizeOf(typeof(uint)) * originalValue.Length));
            
            uint[] recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue.Length == recoveredValue.Length);
            for (int i = 0; i < recoveredValue.Length; i++)
            {
                Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }
            
            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with null uint array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, (uint[])null);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with empty uint array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, new uint[0]);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with mechanism array value test.
        /// </summary>
        [Test()]
        public void _09_MechanismArrayAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            CKM[] originalValue = new CKM[2];
            originalValue[0] = CKM.CKM_RSA_PKCS;
            originalValue[1] = CKM.CKM_AES_CBC;
            // Create attribute with mechanism array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == (UnmanagedMemory.SizeOf(typeof(uint)) * originalValue.Length));
            
            CKM[] recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue.Length == recoveredValue.Length);
            for (int i = 0; i < recoveredValue.Length; i++)
            {
                Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null mechanism array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, (CKM[])null);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with empty mechanism array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, new CKM[0]);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Custom attribute with manually set value test.
        /// </summary>
        [Test()]
        public void _10_CustomAttributeTest()
        {
			if (UnmanagedLong.Size != 4)
				Assert.Inconclusive("Test cannot be executed on this platform");

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };

            // Create attribute without the value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_VALUE);
            Assert.IsTrue(attr.type == (uint)CKA.CKA_VALUE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Allocate unmanaged memory for attribute value..
            attr.value = UnmanagedMemory.Allocate(originalValue.Length);
            // ..then set the value of attribute..
            UnmanagedMemory.Write(attr.value, originalValue);
            // ..and finally set the length of attribute value.
            attr.valueLen = (uint)originalValue.Length;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_VALUE);
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == originalValue.Length);

            // Read the value of attribute
            byte[] recoveredValue = UnmanagedMemory.Read(attr.value, Convert.ToInt32(attr.valueLen));
            Assert.IsTrue(Convert.ToBase64String(originalValue) == Convert.ToBase64String(recoveredValue));

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == (uint)CKA.CKA_VALUE);
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }
    }
}

