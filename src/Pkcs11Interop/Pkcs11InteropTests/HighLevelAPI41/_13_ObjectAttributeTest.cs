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
using System.Reflection;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI41;
using NUnit.Framework;
using CK_ATTRIBUTE = Net.Pkcs11Interop.LowLevelAPI41.CK_ATTRIBUTE;
using NativeLongUtils = Net.Pkcs11Interop.LowLevelAPI41.NativeLongUtils;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI41
{
    /// <summary>
    /// Object attributes tests.
    /// </summary>
    [TestFixture()]
    public class _13_ObjectAttributeTest
    {
        /// <summary>
        /// Attribute dispose test.
        /// </summary>
        [Test()]
        public void _01_DisposeAttributeTest()
        {
            Helpers.CheckPlatform();

            // Unmanaged memory for attribute value stored in low level CK_ATTRIBUTE struct
            // is allocated by constructor of ObjectAttribute class.
            ObjectAttribute attr1 = new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);

            // Do something interesting with attribute

            // This unmanaged memory is freed by Dispose() method.
            attr1.Dispose();


            // ObjectAttribute class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (ObjectAttribute attr2 = new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA))
            {
                // Do something interesting with attribute
            }


            #pragma warning disable 0219

            // Explicit calling of Dispose() method can also be ommitted.
            ObjectAttribute attr3 = new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);

            // Do something interesting with attribute

            // Dispose() method will be called (and unmanaged memory freed) by GC eventually
            // but we cannot be sure when will this occur.

            #pragma warning restore 0219
        }

        /// <summary>
        /// Attribute with empty value test.
        /// </summary>
        [Test()]
        public void _02_EmptyAttributeTest()
        {
            Helpers.CheckPlatform();

            // Create attribute without the value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_CLASS));
                Assert.IsTrue(attr.GetValueAsByteArray() == null);
            }
        }

        /// <summary>
        /// Attribute with NativeULong value test.
        /// </summary>
        [Test()]
        public void _03_UintAttributeTest()
        {
            Helpers.CheckPlatform();

            NativeULong value = NativeLongUtils.ConvertFromCKO(CKO.CKO_DATA);

            // Create attribute with NativeULong value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_CLASS));
                Assert.IsTrue(attr.GetValueAsNativeUlong() == value);
            }
        }

        /// <summary>
        /// Attribute with bool value test.
        /// </summary>
        [Test()]
        public void _04_BoolAttributeTest()
        {
            Helpers.CheckPlatform();

            bool value = true;

            // Create attribute with bool value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_TOKEN, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_TOKEN));
                Assert.IsTrue(attr.GetValueAsBool() == value);
            }
        }

        /// <summary>
        /// Attribute with string value test.
        /// </summary>
        [Test()]
        public void _05_StringAttributeTest()
        {
            Helpers.CheckPlatform();

            string value = "Hello world";

            // Create attribute with string value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_LABEL));
                Assert.IsTrue(attr.GetValueAsString() == value);
            }

            value = null;

            // Create attribute with null string value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_LABEL));
                Assert.IsTrue(attr.GetValueAsString() == value);
            }
        }

        /// <summary>
        /// Attribute with byte array value test.
        /// </summary>
        [Test()]
        public void _06_ByteArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            byte[] value = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };

            // Create attribute with byte array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ID));
                Assert.IsTrue(ConvertUtils.BytesToBase64String(attr.GetValueAsByteArray()) == ConvertUtils.BytesToBase64String(value));
            }

            value = null;

            // Create attribute with null byte array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ID));
                Assert.IsTrue(attr.GetValueAsByteArray() == value);
            }
        }

        /// <summary>
        /// Attribute with DateTime (CKA_DATE) value test.
        /// </summary>
        [Test()]
        public void _07_DateTimeAttributeTest()
        {
            Helpers.CheckPlatform();

            DateTime value = new DateTime(2012, 1, 30, 0, 0, 0, DateTimeKind.Utc);

            // Create attribute with DateTime value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_START_DATE, value))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_START_DATE));
                Assert.IsTrue(attr.GetValueAsDateTime() == value);
            }
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void _08_AttributeArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            ObjectAttribute nestedAttribute1 = new ObjectAttribute(CKA.CKA_TOKEN, true);
            ObjectAttribute nestedAttribute2 = new ObjectAttribute(CKA.CKA_PRIVATE, true);

            List<ObjectAttribute> originalValue = new List<ObjectAttribute>();
            originalValue.Add(nestedAttribute1);
            originalValue.Add(nestedAttribute2);

            // Create attribute with attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_WRAP_TEMPLATE));

                List<ObjectAttribute> recoveredValue = attr.GetValueAsObjectAttributeList();
                Assert.IsTrue(recoveredValue.Count == 2);
                Assert.IsTrue(recoveredValue[0].Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_TOKEN));
                Assert.IsTrue(recoveredValue[0].GetValueAsBool() == true);
                Assert.IsTrue(recoveredValue[1].Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_PRIVATE));
                Assert.IsTrue(recoveredValue[1].GetValueAsBool() == true);
            }

            // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
            // therefore private low level attribute structure needs to be modified to prevent double free.
            // This special handling is needed only in this synthetic test and should be avoided in real world application.
            CK_ATTRIBUTE ckAttribute1 = (CK_ATTRIBUTE)typeof(ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute1);
            ckAttribute1.value = IntPtr.Zero;
            ckAttribute1.valueLen = 0;
            typeof(ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(nestedAttribute1, ckAttribute1);

            // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
            // therefore private low level attribute structure needs to be modified to prevent double free.
            // This special handling is needed only in this synthetic test and should be avoided in real world application.
            CK_ATTRIBUTE ckAttribute2 = (CK_ATTRIBUTE)typeof(ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute2);
            ckAttribute2.value = IntPtr.Zero;
            ckAttribute2.valueLen = 0;
            typeof(ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(nestedAttribute2, ckAttribute2);

            originalValue = null;

            // Create attribute with null attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_WRAP_TEMPLATE));
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == originalValue);
            }

            originalValue = new List<ObjectAttribute>();

            // Create attribute with empty attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_WRAP_TEMPLATE));
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == null);
            }
        }

        /// <summary>
        /// Attribute with NativeULong array value test.
        /// </summary>
        [Test()]
        public void _09_UintArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            List<NativeULong> originalValue = new List<NativeULong>();
            originalValue.Add(333333);
            originalValue.Add(666666);
            
            // Create attribute with NativeULong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));

                List<NativeULong> recoveredValue = attr.GetValueAsNativeULongList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }

            originalValue = null;

            // Create attribute with null NativeULong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsNativeULongList() == originalValue);
            }

            originalValue = new List<NativeULong>();

            // Create attribute with empty NativeULong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsNativeULongList() == null);
            }
        }

        /// <summary>
        /// Attribute with mechanism array value test.
        /// </summary>
        [Test()]
        public void _10_MechanismArrayAttributeTest()
        {
            Helpers.CheckPlatform();

            List<CKM> originalValue = new List<CKM>();
            originalValue.Add(CKM.CKM_RSA_PKCS);
            originalValue.Add(CKM.CKM_AES_CBC);

            // Create attribute with mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                
                List<CKM> recoveredValue = attr.GetValueAsCkmList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }
            
            originalValue = null;
            
            // Create attribute with null mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsCkmList() == originalValue);
            }
            
            originalValue = new List<CKM>();
            
            // Create attribute with empty mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeLongUtils.ConvertFromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsCkmList() == null);
            }
        }
    }
}
