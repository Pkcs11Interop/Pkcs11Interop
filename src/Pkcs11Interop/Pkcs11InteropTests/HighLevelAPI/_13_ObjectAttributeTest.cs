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
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;
using HLA40 = Net.Pkcs11Interop.HighLevelAPI40;
using HLA41 = Net.Pkcs11Interop.HighLevelAPI41;
using HLA80 = Net.Pkcs11Interop.HighLevelAPI80;
using HLA81 = Net.Pkcs11Interop.HighLevelAPI81;
using LLA40 = Net.Pkcs11Interop.LowLevelAPI40;
using LLA41 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA80 = Net.Pkcs11Interop.LowLevelAPI80;
using LLA81 = Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
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
            // Unmanaged memory for attribute value stored in low level CK_ATTRIBUTE struct
            // is allocated by constructor of ObjectAttribute class.
            IObjectAttribute attr1 = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);

            // Do something interesting with attribute

            // This unmanaged memory is freed by Dispose() method.
            attr1.Dispose();


            // ObjectAttribute class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (IObjectAttribute attr2 = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA))
            {
                // Do something interesting with attribute
            }


            #pragma warning disable 0219

            // Explicit calling of Dispose() method can also be ommitted.
            IObjectAttribute attr3 = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, CKO.CKO_DATA);

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
            // Create attribute without the value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_CLASS));
                Assert.IsTrue(attr.GetValueAsByteArray() == null);
            }
        }

        /// <summary>
        /// Attribute with ulong value test.
        /// </summary>
        [Test()]
        public void _03_ULongAttributeTest()
        {
            ulong value = NativeULongUtils.ConvertUInt64FromCKO(CKO.CKO_DATA);

            // Create attribute with ulong value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_CLASS, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_CLASS));
                Assert.IsTrue(attr.GetValueAsUlong() == value);
            }
        }

        /// <summary>
        /// Attribute with bool value test.
        /// </summary>
        [Test()]
        public void _04_BoolAttributeTest()
        {
            bool value = true;

            // Create attribute with bool value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_TOKEN));
                Assert.IsTrue(attr.GetValueAsBool() == value);
            }
        }

        /// <summary>
        /// Attribute with string value test.
        /// </summary>
        [Test()]
        public void _05_StringAttributeTest()
        {
            string value = "Hello world";

            // Create attribute with string value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_LABEL));
                Assert.IsTrue(attr.GetValueAsString() == value);
            }

            value = null;

            // Create attribute with null string value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_LABEL));
                Assert.IsTrue(attr.GetValueAsString() == value);
            }
        }

        /// <summary>
        /// Attribute with byte array value test.
        /// </summary>
        [Test()]
        public void _06_ByteArrayAttributeTest()
        {
            byte[] value = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };

            // Create attribute with byte array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ID));
                Assert.IsTrue(ConvertUtils.BytesToBase64String(attr.GetValueAsByteArray()) == ConvertUtils.BytesToBase64String(value));
            }

            value = null;

            // Create attribute with null byte array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ID));
                Assert.IsTrue(attr.GetValueAsByteArray() == value);
            }
        }

        /// <summary>
        /// Attribute with DateTime (CKA_DATE) value test.
        /// </summary>
        [Test()]
        public void _07_DateTimeAttributeTest()
        {
            DateTime value = new DateTime(2012, 1, 30, 0, 0, 0, DateTimeKind.Utc);

            // Create attribute with DateTime value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_START_DATE, value))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_START_DATE));
                Assert.IsTrue(attr.GetValueAsDateTime() == value);
            }
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void _08_AttributeArrayAttributeTest()
        {
            IObjectAttribute nestedAttribute1 = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_TOKEN, true);
            IObjectAttribute nestedAttribute2 = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_PRIVATE, true);

            List<IObjectAttribute> originalValue = new List<IObjectAttribute>();
            originalValue.Add(nestedAttribute1);
            originalValue.Add(nestedAttribute2);

            // Create attribute with attribute array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_WRAP_TEMPLATE));

                List<IObjectAttribute> recoveredValue = attr.GetValueAsObjectAttributeList();
                Assert.IsTrue(recoveredValue.Count == 2);
                Assert.IsTrue(recoveredValue[0].Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_TOKEN));
                Assert.IsTrue(recoveredValue[0].GetValueAsBool() == true);
                Assert.IsTrue(recoveredValue[1].Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_PRIVATE));
                Assert.IsTrue(recoveredValue[1].GetValueAsBool() == true);
            }

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA40.ObjectAttribute objectAttribute40a = (HLA40.ObjectAttribute)nestedAttribute1;
                    LLA40.CK_ATTRIBUTE ckAttribute40a = (LLA40.CK_ATTRIBUTE)typeof(HLA40.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute40a);
                    ckAttribute40a.value = IntPtr.Zero;
                    ckAttribute40a.valueLen = 0;
                    typeof(HLA40.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute40a, ckAttribute40a);

                    // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA40.ObjectAttribute objectAttribute40b = (HLA40.ObjectAttribute)nestedAttribute2;
                    LLA40.CK_ATTRIBUTE ckAttribute40b = (LLA40.CK_ATTRIBUTE)typeof(HLA40.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute40b);
                    ckAttribute40b.value = IntPtr.Zero;
                    ckAttribute40b.valueLen = 0;
                    typeof(HLA40.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute40b, ckAttribute40b);
                }
                else
                {
                    // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA41.ObjectAttribute objectAttribute41a = (HLA41.ObjectAttribute)nestedAttribute1;
                    LLA41.CK_ATTRIBUTE ckAttribute41a = (LLA41.CK_ATTRIBUTE)typeof(HLA41.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute41a);
                    ckAttribute41a.value = IntPtr.Zero;
                    ckAttribute41a.valueLen = 0;
                    typeof(HLA41.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute41a, ckAttribute41a);

                    // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA41.ObjectAttribute objectAttribute41b = (HLA41.ObjectAttribute)nestedAttribute2;
                    LLA41.CK_ATTRIBUTE ckAttribute41b = (LLA41.CK_ATTRIBUTE)typeof(HLA41.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute41b);
                    ckAttribute41b.value = IntPtr.Zero;
                    ckAttribute41b.valueLen = 0;
                    typeof(HLA41.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute41b, ckAttribute41b);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA80.ObjectAttribute objectAttribute80a = (HLA80.ObjectAttribute)nestedAttribute1;
                    LLA80.CK_ATTRIBUTE ckAttribute80a = (LLA80.CK_ATTRIBUTE)typeof(HLA80.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute80a);
                    ckAttribute80a.value = IntPtr.Zero;
                    ckAttribute80a.valueLen = 0;
                    typeof(HLA80.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute80a, ckAttribute80a);

                    // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA80.ObjectAttribute objectAttribute80b = (HLA80.ObjectAttribute)nestedAttribute2;
                    LLA80.CK_ATTRIBUTE ckAttribute80b = (LLA80.CK_ATTRIBUTE)typeof(HLA80.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute80b);
                    ckAttribute80b.value = IntPtr.Zero;
                    ckAttribute80b.valueLen = 0;
                    typeof(HLA80.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute80b, ckAttribute80b);
                }
                else
                {
                    // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA81.ObjectAttribute objectAttribute81a = (HLA81.ObjectAttribute)nestedAttribute1;
                    LLA81.CK_ATTRIBUTE ckAttribute81a = (LLA81.CK_ATTRIBUTE)typeof(HLA81.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute81a);
                    ckAttribute81a.value = IntPtr.Zero;
                    ckAttribute81a.valueLen = 0;
                    typeof(HLA81.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute81a, ckAttribute81a);

                    // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                    // therefore private low level attribute structure needs to be modified to prevent double free.
                    // This special handling is needed only in this synthetic test and should be avoided in real world application.
                    HLA81.ObjectAttribute objectAttribute81b = (HLA81.ObjectAttribute)nestedAttribute2;
                    LLA81.CK_ATTRIBUTE ckAttribute81b = (LLA81.CK_ATTRIBUTE)typeof(HLA81.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute81b);
                    ckAttribute81b.value = IntPtr.Zero;
                    ckAttribute81b.valueLen = 0;
                    typeof(HLA81.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute81b, ckAttribute81b);
                }
            }

            originalValue = null;

            // Create attribute with null attribute array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_WRAP_TEMPLATE));
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == originalValue);
            }

            originalValue = new List<IObjectAttribute>();

            // Create attribute with empty attribute array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_WRAP_TEMPLATE));
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == null);
            }
        }

        /// <summary>
        /// Attribute with ulong array value test.
        /// </summary>
        [Test()]
        public void _09_ULongArrayAttributeTest()
        {
            List<ulong> originalValue = new List<ulong>();
            originalValue.Add(333333);
            originalValue.Add(666666);
            
            // Create attribute with ulong array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));

                List<ulong> recoveredValue = attr.GetValueAsULongList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }

            originalValue = null;

            // Create attribute with null ulong array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsULongList() == originalValue);
            }

            originalValue = new List<ulong>();

            // Create attribute with empty ulong array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsULongList() == null);
            }
        }

        /// <summary>
        /// Attribute with mechanism array value test.
        /// </summary>
        [Test()]
        public void _10_MechanismArrayAttributeTest()
        {
            List<CKM> originalValue = new List<CKM>();
            originalValue.Add(CKM.CKM_RSA_PKCS);
            originalValue.Add(CKM.CKM_AES_CBC);

            // Create attribute with mechanism array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                
                List<CKM> recoveredValue = attr.GetValueAsCkmList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }
            
            originalValue = null;
            
            // Create attribute with null mechanism array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsCkmList() == originalValue);
            }
            
            originalValue = new List<CKM>();
            
            // Create attribute with empty mechanism array value
            using (IObjectAttribute attr = Settings.Factories.ObjectAttributeFactory.CreateObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == NativeULongUtils.ConvertUInt64FromCKA(CKA.CKA_ALLOWED_MECHANISMS));
                Assert.IsTrue(attr.GetValueAsCkmList() == null);
            }
        }
    }
}
