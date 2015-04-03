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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using HLA4 = Net.Pkcs11Interop.HighLevelAPI41;
using HLA8 = Net.Pkcs11Interop.HighLevelAPI8;
using LLA4 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA8 = Net.Pkcs11Interop.LowLevelAPI81;

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
            // Create attribute without the value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_CLASS);
                Assert.IsTrue(attr.GetValueAsByteArray() == null);
            }
        }

        /// <summary>
        /// Attribute with ulong value test.
        /// </summary>
        [Test()]
        public void _03_UintAttributeTest()
        {
            ulong value = (ulong)CKO.CKO_DATA;

            // Create attribute with ulong value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_CLASS);
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
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_TOKEN, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_TOKEN);
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
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_LABEL);
                Assert.IsTrue(attr.GetValueAsString() == value);
            }

            value = null;

            // Create attribute with null string value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_LABEL);
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
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ID);
                Assert.IsTrue(Convert.ToBase64String(attr.GetValueAsByteArray()) == Convert.ToBase64String(value));
            }

            value = null;

            // Create attribute with null byte array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ID);
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
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_START_DATE, value))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_START_DATE);
                Assert.IsTrue(attr.GetValueAsDateTime() == value);
            }
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void _08_AttributeArrayAttributeTest()
        {
            ObjectAttribute nestedAttribute1 = new ObjectAttribute(CKA.CKA_TOKEN, true);
            ObjectAttribute nestedAttribute2 = new ObjectAttribute(CKA.CKA_PRIVATE, true);

            List<ObjectAttribute> originalValue = new List<ObjectAttribute>();
            originalValue.Add(nestedAttribute1);
            originalValue.Add(nestedAttribute2);

            // Create attribute with attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);

                List<ObjectAttribute> recoveredValue = attr.GetValueAsObjectAttributeList();
                Assert.IsTrue(recoveredValue.Count == 2);
                Assert.IsTrue(recoveredValue[0].Type == (ulong)CKA.CKA_TOKEN);
                Assert.IsTrue(recoveredValue[0].GetValueAsBool() == true);
                Assert.IsTrue(recoveredValue[1].Type == (ulong)CKA.CKA_PRIVATE);
                Assert.IsTrue(recoveredValue[1].GetValueAsBool() == true);
            }

            if (Platform.UnmanagedLongSize == 4)
            {
                // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                // therefore private low level attribute structure needs to be modified to prevent double free.
                // This special handling is needed only in this synthetic test and should be avoided in real world application.
                HLA4.ObjectAttribute objectAttribute41 = (HLA4.ObjectAttribute)typeof(ObjectAttribute).GetField("_objectAttribute4", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute1);
                LLA4.CK_ATTRIBUTE ckAttribute1 = (LLA4.CK_ATTRIBUTE)typeof(HLA4.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute41);
                ckAttribute1.value = IntPtr.Zero;
                ckAttribute1.valueLen = 0;
                typeof(HLA4.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute41, ckAttribute1);

                // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                // therefore private low level attribute structure needs to be modified to prevent double free.
                // This special handling is needed only in this synthetic test and should be avoided in real world application.
                HLA4.ObjectAttribute objectAttribute42 = (HLA4.ObjectAttribute)typeof(ObjectAttribute).GetField("_objectAttribute4", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute2);
                LLA4.CK_ATTRIBUTE ckAttribute2 = (LLA4.CK_ATTRIBUTE)typeof(HLA4.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute42);
                ckAttribute2.value = IntPtr.Zero;
                ckAttribute2.valueLen = 0;
                typeof(HLA4.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute42, ckAttribute2);
            }
            else
            {
                // There is the same pointer to unmanaged memory in both nestedAttribute1 and recoveredValue[0] instances
                // therefore private low level attribute structure needs to be modified to prevent double free.
                // This special handling is needed only in this synthetic test and should be avoided in real world application.
                HLA8.ObjectAttribute objectAttribute81 = (HLA8.ObjectAttribute)typeof(ObjectAttribute).GetField("_objectAttribute8", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute1);
                LLA8.CK_ATTRIBUTE ckAttribute1 = (LLA8.CK_ATTRIBUTE)typeof(HLA8.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute81);
                ckAttribute1.value = IntPtr.Zero;
                ckAttribute1.valueLen = 0;
                typeof(HLA8.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute81, ckAttribute1);

                // There is the same pointer to unmanaged memory in both nestedAttribute2 and recoveredValue[1] instances
                // therefore private low level attribute structure needs to be modified to prevent double free.
                // This special handling is needed only in this synthetic test and should be avoided in real world application.
                HLA8.ObjectAttribute objectAttribute82 = (HLA8.ObjectAttribute)typeof(ObjectAttribute).GetField("_objectAttribute8", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(nestedAttribute2);
                LLA8.CK_ATTRIBUTE ckAttribute2 = (LLA8.CK_ATTRIBUTE)typeof(HLA8.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(objectAttribute82);
                ckAttribute2.value = IntPtr.Zero;
                ckAttribute2.valueLen = 0;
                typeof(HLA8.ObjectAttribute).GetField("_ckAttribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objectAttribute82, ckAttribute2);
            }

            originalValue = null;

            // Create attribute with null attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == originalValue);
            }

            originalValue = new List<ObjectAttribute>();

            // Create attribute with empty attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);
                Assert.IsTrue(attr.GetValueAsObjectAttributeList() == null);
            }
        }

        /// <summary>
        /// Attribute with ulong array value test.
        /// </summary>
        [Test()]
        public void _09_UintArrayAttributeTest()
        {
            List<ulong> originalValue = new List<ulong>();
            originalValue.Add(333333);
            originalValue.Add(666666);
            
            // Create attribute with ulong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);

                List<ulong> recoveredValue = attr.GetValueAsUlongList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }

            originalValue = null;

            // Create attribute with null ulong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsUlongList() == originalValue);
            }

            originalValue = new List<ulong>();

            // Create attribute with empty ulong array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsUlongList() == null);
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
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);
                
                List<CKM> recoveredValue = attr.GetValueAsCkmList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }
            
            originalValue = null;
            
            // Create attribute with null mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsCkmList() == originalValue);
            }
            
            originalValue = new List<CKM>();
            
            // Create attribute with empty mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsCkmList() == null);
            }
        }
    }
}
