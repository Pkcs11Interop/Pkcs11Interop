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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI8;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI8
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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

            List<ObjectAttribute> originalValue = new List<ObjectAttribute>();
            originalValue.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));
            originalValue.Add(new ObjectAttribute(CKA.CKA_PRIVATE, true));

            // Create attribute with attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);

                try
                {
                    attr.GetValueAsObjectAttributeList();
                }
                catch (NotImplementedException)
                {
                    // Reading attribute value as List<ObjectAttribute> is currently not implemented in HighLevelAPI
                }
            }

            originalValue = null;

            // Create attribute with null attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);

                try
                {
                    attr.GetValueAsObjectAttributeList();
                }
                catch (NotImplementedException)
                {
                    // Reading attribute value as List<ObjectAttribute> is currently not implemented in HighLevelAPI
                }
            }

            originalValue = new List<ObjectAttribute>();

            // Create attribute with empty attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (ulong)CKA.CKA_WRAP_TEMPLATE);

                try
                {
                    attr.GetValueAsObjectAttributeList();
                }
                catch (NotImplementedException)
                {
                    // Reading attribute value as List<ObjectAttribute> is currently not implemented in HighLevelAPI
                }
            }
        }

        /// <summary>
        /// Attribute with ulong array value test.
        /// </summary>
        [Test()]
        public void _09_UintArrayAttributeTest()
        {
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
            Assert.IsTrue(UnmanagedLong.Size == 8, "Test cannot be executed on this platform");

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
