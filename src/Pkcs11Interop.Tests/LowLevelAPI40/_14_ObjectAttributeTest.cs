/*
 *  Copyright 2012-2021 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI40;
using NUnit.Framework;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI40
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
            Helpers.CheckPlatform();

            // Create attribute without the value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_CLASS);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_CLASS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with NativeULong value test.
        /// </summary>
        [Test()]
        public void _02_UintAttributeTest()
        {
            Helpers.CheckPlatform();

            NativeULong originalValue = ConvertUtils.UInt32FromCKO(CKO.CKO_DATA);
            // Create attribute with NativeULong value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_CLASS, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_CLASS));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(UnmanagedMemory.SizeOf(typeof(NativeULong))));
            
            NativeULong recoveredValue = 0;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_CLASS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with bool value test.
        /// </summary>
        [Test()]
        public void _03_BoolAttributeTest()
        {
            Helpers.CheckPlatform();

            bool originalValue = true;
            // Create attribute with bool value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_TOKEN));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 1);
            
            bool recoveredValue = false;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_TOKEN));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with string value test.
        /// </summary>
        [Test()]
        public void _04_StringAttributeTest()
        {
            Helpers.CheckPlatform();

            string originalValue = "Hello world";
            // Create attribute with string value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_LABEL, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_LABEL));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(originalValue.Length));
            
            string recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_LABEL));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null string value
            attr = CkaUtils.CreateAttribute(CKA.CKA_LABEL, (string)null);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_LABEL));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with byte array value test.
        /// </summary>
        [Test()]
        public void _05_ByteArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            // Create attribute with byte array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ID, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ID));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(originalValue.Length));
            
            byte[] recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(ConvertUtils.BytesToBase64String(originalValue) == ConvertUtils.BytesToBase64String(recoveredValue));

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ID));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null byte array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ID, (byte[])null);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ID));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with DateTime (CKA_DATE) value test.
        /// </summary>
        [Test()]
        public void _06_DateTimeAttributeTest()
        {
            Helpers.CheckPlatform();

            DateTime originalValue = new DateTime(2012, 1, 30, 0, 0, 0, DateTimeKind.Utc);
            // Create attribute with DateTime value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_START_DATE, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_START_DATE));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 8);
            
            DateTime? recoveredValue = null;
            // Read the value of attribute
            CkaUtils.ConvertValue(ref attr, out recoveredValue);
            Assert.IsTrue(originalValue == recoveredValue);

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_START_DATE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void _07_AttributeArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            CK_ATTRIBUTE[] originalValue = new CK_ATTRIBUTE[2];
            originalValue[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            originalValue[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, true);
            // Create attribute with attribute array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_WRAP_TEMPLATE));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE)) * originalValue.Length));

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
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_WRAP_TEMPLATE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null attribute array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, (CK_ATTRIBUTE[])null);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_WRAP_TEMPLATE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with empty attribute array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_WRAP_TEMPLATE, new CK_ATTRIBUTE[0]);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_WRAP_TEMPLATE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with NativeULong array value test.
        /// </summary>
        [Test()]
        public void _08_UintArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            NativeULong[] originalValue = new NativeULong[2];
            originalValue[0] = 333333;
            originalValue[1] = 666666;
            // Create attribute with NativeULong array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(UnmanagedMemory.SizeOf(typeof(NativeULong)) * originalValue.Length));
            
            NativeULong[] recoveredValue = null;
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
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with null NativeULong array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, (NativeULong[])null);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with empty NativeULong array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, new NativeULong[0]);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Attribute with mechanism array value test.
        /// </summary>
        [Test()]
        public void _09_MechanismArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            CKM[] originalValue = new CKM[2];
            originalValue[0] = CKM.CKM_RSA_PKCS;
            originalValue[1] = CKM.CKM_AES_CBC;
            // Create attribute with mechanism array value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(UnmanagedMemory.SizeOf(typeof(NativeULong)) * originalValue.Length));
            
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
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Create attribute with null mechanism array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, (CKM[])null);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
            
            // Create attribute with empty mechanism array value
            attr = CkaUtils.CreateAttribute(CKA.CKA_ALLOWED_MECHANISMS, new CKM[0]);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }

        /// <summary>
        /// Custom attribute with manually set value test.
        /// </summary>
        [Test()]
        public void _10_CustomAttributeTest()
        {
            Helpers.CheckPlatform();

            byte[] originalValue = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };

            // Create attribute without the value
            CK_ATTRIBUTE attr = CkaUtils.CreateAttribute(CKA.CKA_VALUE);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_VALUE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);

            // Allocate unmanaged memory for attribute value..
            attr.value = UnmanagedMemory.Allocate(originalValue.Length);
            // ..then set the value of attribute..
            UnmanagedMemory.Write(attr.value, originalValue);
            // ..and finally set the length of attribute value.
            attr.valueLen = ConvertUtils.UInt32FromInt32(originalValue.Length);
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_VALUE));
            Assert.IsTrue(attr.value != IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == ConvertUtils.UInt32FromInt32(originalValue.Length));

            // Read the value of attribute
            byte[] recoveredValue = UnmanagedMemory.Read(attr.value, ConvertUtils.UInt32ToInt32(attr.valueLen));
            Assert.IsTrue(ConvertUtils.BytesToBase64String(originalValue) == ConvertUtils.BytesToBase64String(recoveredValue));

            // Free attribute value
            UnmanagedMemory.Free(ref attr.value);
            attr.valueLen = 0;
            Assert.IsTrue(attr.type == ConvertUtils.UInt32FromCKA(CKA.CKA_VALUE));
            Assert.IsTrue(attr.value == IntPtr.Zero);
            Assert.IsTrue(attr.valueLen == 0);
        }
    }
}

