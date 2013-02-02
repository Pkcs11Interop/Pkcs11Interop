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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;
using System.Collections.Generic;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Object attributes tests.
    /// </summary>
    [TestFixture()]
    public class ObjectAttributeTest
    {
        /// <summary>
        /// Attribute dispose test.
        /// </summary>
        [Test()]
        public void DisposeAttributeTest()
        {
            // Unmanaged memory for attribute value stored in low level CK_ATTRIBUTE struct
            // is allocated by constructor of ObjectAttribute class.
            ObjectAttribute attr1 = new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA);

            // Do something interesting with attribute

            // This unmanaged memory is freed by Dispose() method.
            attr1.Dispose();


            // ObjectAttribute class can be used in using statement which defines a scope 
            // at the end of which an object will be disposed (and unmanaged memory freed).
            using (ObjectAttribute attr2 = new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA))
            {
                // Do something interesting with attribute
            }


            // Explicit calling of Dispose() method can also be ommitted.
            ObjectAttribute attr3 = new ObjectAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA);

            // Do something interesting with attribute

            // Dispose() method will be called (and unmanaged memory freed) by GC eventually
            // but we cannot be sure when will this occur.
        }

        /// <summary>
        /// Attribute with empty value test.
        /// </summary>
        [Test()]
        public void EmptyAttributeTest()
        {
            // Create attribute without the value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_CLASS);
                Assert.IsTrue(attr.GetValueAsByteArray() == null);
            }
        }

        /// <summary>
        /// Attribute with uint value test.
        /// </summary>
        [Test()]
        public void UintAttributeTest()
        {
            uint value = (uint)CKO.CKO_DATA;

            // Create attribute with uint value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_CLASS, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_CLASS);
                Assert.IsTrue(attr.GetValueAsUint() == value);
            }
        }

        /// <summary>
        /// Attribute with bool value test.
        /// </summary>
        [Test()]
        public void BoolAttributeTest()
        {
            bool value = true;

            // Create attribute with bool value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_TOKEN, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_TOKEN);
                Assert.IsTrue(attr.GetValueAsBool() == value);
            }
        }

        /// <summary>
        /// Attribute with string value test.
        /// </summary>
        [Test()]
        public void StringAttributeTest()
        {
            string value = "Hello world";

            // Create attribute with string value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_LABEL);
                Assert.IsTrue(attr.GetValueAsString() == value);
            }

            value = null;

            // Create attribute with null string value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_LABEL, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_LABEL);
                Assert.IsTrue(attr.GetValueAsString() == value);
            }
        }

        /// <summary>
        /// Attribute with byte array value test.
        /// </summary>
        [Test()]
        public void ByteArrayAttributeTest()
        {
            byte[] value = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };

            // Create attribute with byte array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ID);
                Assert.IsTrue(Convert.ToBase64String(attr.GetValueAsByteArray()) == Convert.ToBase64String(value));
            }

            value = null;

            // Create attribute with null byte array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ID, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ID);
                Assert.IsTrue(attr.GetValueAsByteArray() == value);
            }
        }

        /// <summary>
        /// Attribute with DateTime (CKA_DATE) value test.
        /// </summary>
        [Test()]
        public void DateTimeAttributeTest()
        {
            DateTime value = new DateTime(2012, 1, 30, 0, 0, 0, DateTimeKind.Utc);

            // Create attribute with DateTime value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_START_DATE, value))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_START_DATE);
                Assert.IsTrue(attr.GetValueAsDateTime() == value);
            }
        }

        /// <summary>
        /// Attribute with attribute array value test.
        /// </summary>
        [Test()]
        public void AttributeArrayAttributeTest()
        {
            List<ObjectAttribute> originalValue = new List<ObjectAttribute>();
            originalValue.Add(new ObjectAttribute(CKA.CKA_TOKEN, true));
            originalValue.Add(new ObjectAttribute(CKA.CKA_PRIVATE, true));

            // Create attribute with attribute array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_WRAP_TEMPLATE, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_WRAP_TEMPLATE);

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
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_WRAP_TEMPLATE);

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
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_WRAP_TEMPLATE);

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
        /// Attribute with uint array value test.
        /// </summary>
        [Test()]
        public void UintArrayAttributeTest()
        {
            List<uint> originalValue = new List<uint>();
            originalValue.Add(333333);
            originalValue.Add(666666);
            
            // Create attribute with uint array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);

                List<uint> recoveredValue = attr.GetValueAsUintList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }

            originalValue = null;

            // Create attribute with null uint array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsUintList() == originalValue);
            }

            originalValue = new List<uint>();

            // Create attribute with empty uint array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsUintList() == null);
            }
        }

        /// <summary>
        /// Attribute with mechanism array value test.
        /// </summary>
        [Test()]
        public void MechanismArrayAttributeTest()
        {
            List<CKM> originalValue = new List<CKM>();
            originalValue.Add(CKM.CKM_RSA_PKCS);
            originalValue.Add(CKM.CKM_AES_CBC);

            // Create attribute with mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
                
                List<CKM> recoveredValue = attr.GetValueAsCkmList();
                for (int i = 0; i < recoveredValue.Count; i++)
                    Assert.IsTrue(originalValue[i] == recoveredValue[i]);
            }
            
            originalValue = null;
            
            // Create attribute with null mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsCkmList() == originalValue);
            }
            
            originalValue = new List<CKM>();
            
            // Create attribute with empty mechanism array value
            using (ObjectAttribute attr = new ObjectAttribute(CKA.CKA_ALLOWED_MECHANISMS, originalValue))
            {
                Assert.IsTrue(attr.Type == (uint)CKA.CKA_ALLOWED_MECHANISMS);
                Assert.IsTrue(attr.GetValueAsCkmList() == null);
            }
        }
    }
}
