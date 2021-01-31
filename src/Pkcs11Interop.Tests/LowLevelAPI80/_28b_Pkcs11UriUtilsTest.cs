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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// Pkcs11UriUtils tests
    /// </summary>
    [TestFixture()]
    public partial class _28_Pkcs11UriUtilsTest
    {
        /// <summary>
        /// LibraryInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _02_LibraryInfoMatches()
        {
            Helpers.CheckPlatform();

            // Empty URI
            Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
            CK_INFO libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // Empty attribute
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=;library-description=bar;library-version=1");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("                                ");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // Unknown path attribute in URI
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=foo;library-description=bar;library-version=1;foo=bar");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // All attributes matching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=foo;library-description=bar;library-version=1");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // LibraryManufacturer nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=foo;library-description=bar;library-version=1");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // LibraryDescription nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=foo;library-description=bar;library-version=1");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x01 }, Minor = new byte[] { 0x00 } };
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

            // LibraryVersion nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:library-manufacturer=foo;library-description=bar;library-version=1");
            libraryInfo = new CK_INFO();
            libraryInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            libraryInfo.LibraryDescription = ConvertUtils.Utf8StringToBytes("bar");
            libraryInfo.LibraryVersion = new CK_VERSION() { Major = new byte[] { 0x00 }, Minor = new byte[] { 0x01 } };
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));
        }

        /// <summary>
        /// SlotInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _03_SlotInfoMatches()
        {
            Helpers.CheckPlatform();

            // Empty URI
            Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
            CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            NativeULong slotId = 1;
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // Empty attribute
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=;slot-description=bar;slot-id=1");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("                                ");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            slotId = 1;
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // Unknown path attribute in URI
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=foo;slot-description=bar;slot-id=1;foo=bar");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            slotId = 1;
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // All attributes matching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=foo;slot-description=bar;slot-id=1");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            slotId = 1;
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // Manufacturer nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=foo;slot-description=bar;slot-id=1");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            slotId = 1;
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // Description nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=foo;slot-description=bar;slot-id=1");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("foo");
            slotId = 1;
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));

            // Slot id nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:slot-manufacturer=foo;slot-description=bar;slot-id=1");
            slotInfo = new CK_SLOT_INFO();
            slotInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            slotInfo.SlotDescription = ConvertUtils.Utf8StringToBytes("bar");
            slotId = 2;
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo, slotId));
        }

        /// <summary>
        /// TokenInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _04_TokenInfoMatches()
        {
            Helpers.CheckPlatform();

            // Empty URI
            Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
            CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // Empty attribute
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("                                ");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // Unknown path attribute in URI
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar;foo=bar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // All attributes matching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // Label nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // ManufacturerId nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // SerialNumber nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("012");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foobar");
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

            // Model nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:token=foo;manufacturer=bar;serial=123;model=foobar");
            tokenInfo = new CK_TOKEN_INFO();
            tokenInfo.Label = ConvertUtils.Utf8StringToBytes("foo");
            tokenInfo.ManufacturerId = ConvertUtils.Utf8StringToBytes("bar");
            tokenInfo.SerialNumber = ConvertUtils.Utf8StringToBytes("123");
            tokenInfo.Model = ConvertUtils.Utf8StringToBytes("foo bar");
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));
        }

        /// <summary>
        /// ObjectAttribute matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _05_ObjectAttributesMatches()
        {
            Helpers.CheckPlatform();

            // Empty URI
            Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
            List<CK_ATTRIBUTE> objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Empty attribute
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=;id=%01%02%03");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, string.Empty));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Unknown path attribute in URI
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03;foo=bar");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // All attributes matching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Type nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Object nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foo bar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Id nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<CK_ATTRIBUTE>();
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x04, 0x05, 0x06 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            try
            {
                // Type present in URI but missing in list
                pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
                objectAttributes = new List<CK_ATTRIBUTE>();
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_LABEL, "foobar"));
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
                Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes);
                Assert.Fail("Exception expected but not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Pkcs11UriException);
            }

            try
            {
                // Object present in URI but missing in list
                pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
                objectAttributes = new List<CK_ATTRIBUTE>();
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
                Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes);
                Assert.Fail("Exception expected but not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Pkcs11UriException);
            }

            try
            {
                // Id present in URI but missing in list
                pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
                objectAttributes = new List<CK_ATTRIBUTE>();
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
                objectAttributes.Add(CkaUtils.CreateAttribute(CKA.CKA_ID, new byte[] { 0x04, 0x05, 0x06 }));
                Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes);
                Assert.Fail("Exception expected but not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Pkcs11UriException);
            }
        }

        /// <summary>
        /// PKCS#11 URI matching slot list extraction
        /// </summary>
        [Test()]
        public void _06_GetMatchingSlotList()
        {
            Helpers.CheckPlatform();

            using (Pkcs11Library pkcs11Library = new Pkcs11Library(Settings.Pkcs11LibraryPath))
            {
                CKR rv = pkcs11Library.C_Initialize(Settings.InitArgs80);
                Assert.IsTrue(rv == CKR.CKR_OK);

                // Get all slots
                NativeULong allSlotsCount = 0;
                rv = pkcs11Library.C_GetSlotList(true, null, ref allSlotsCount);
                Assert.IsTrue(rv == CKR.CKR_OK);
                Assert.IsTrue(allSlotsCount > 0);
                NativeULong[] allSlots = new NativeULong[allSlotsCount];
                rv = pkcs11Library.C_GetSlotList(true, allSlots, ref allSlotsCount);
                Assert.IsTrue(rv == CKR.CKR_OK);

                // Empty URI
                Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
                NativeULong[] matchedSlots = null;
                rv = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11Library, true, out matchedSlots);
                Assert.IsTrue(rv == CKR.CKR_OK);
                Assert.IsTrue(matchedSlots.Length == allSlots.Length);

                // Unknown path attribute in URI
                pkcs11uri = new Pkcs11Uri(@"pkcs11:vendor=foobar");
                rv = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11Library, true, out matchedSlots);
                Assert.IsTrue(rv == CKR.CKR_OK);
                Assert.IsTrue(matchedSlots.Length == 0);

                // All attributes matching one slot
                CK_INFO libraryInfo = new CK_INFO();
                rv = pkcs11Library.C_GetInfo(ref libraryInfo);
                Assert.IsTrue(rv == CKR.CKR_OK);
                CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
                rv = pkcs11Library.C_GetSlotInfo(allSlots[0], ref slotInfo);
                Assert.IsTrue(rv == CKR.CKR_OK);
                CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
                rv = pkcs11Library.C_GetTokenInfo(allSlots[0], ref tokenInfo);
                Assert.IsTrue(rv == CKR.CKR_OK);

                Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = ConvertUtils.BytesToUtf8String(libraryInfo.ManufacturerId, true);
                pkcs11UriBuilder.LibraryDescription = ConvertUtils.BytesToUtf8String(libraryInfo.LibraryDescription, true);
                pkcs11UriBuilder.LibraryVersion = libraryInfo.LibraryVersion.ToString();
                pkcs11UriBuilder.SlotManufacturer = ConvertUtils.BytesToUtf8String(slotInfo.ManufacturerId, true);
                pkcs11UriBuilder.SlotDescription = ConvertUtils.BytesToUtf8String(slotInfo.SlotDescription, true);
                pkcs11UriBuilder.SlotId = allSlots[0];
                pkcs11UriBuilder.Token = ConvertUtils.BytesToUtf8String(tokenInfo.Label, true);
                pkcs11UriBuilder.Manufacturer = ConvertUtils.BytesToUtf8String(tokenInfo.ManufacturerId, true);
                pkcs11UriBuilder.Serial = ConvertUtils.BytesToUtf8String(tokenInfo.SerialNumber, true);
                pkcs11UriBuilder.Model = ConvertUtils.BytesToUtf8String(tokenInfo.Model, true);
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();

                rv = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11Library, true, out matchedSlots);
                Assert.IsTrue(rv == CKR.CKR_OK);
                Assert.IsTrue(matchedSlots.Length == 1);

                // One attribute nonmatching
                pkcs11UriBuilder.Serial = "foobar";
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                rv = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11Library, true, out matchedSlots);
                Assert.IsTrue(rv == CKR.CKR_OK);
                Assert.IsTrue(matchedSlots.Length == 0);

                rv = pkcs11Library.C_Finalize(IntPtr.Zero);
                Assert.IsTrue(rv == CKR.CKR_OK);
            }
        }

        /// <summary>
        /// PKCS#11 URI matching ObjectAttribute extraction
        /// </summary>
        [Test()]
        public void _07_GetObjectAttributes()
        {
            Helpers.CheckPlatform();

            string uri = @"pkcs11:object=foo;type=private;id=%01%02%03";

            Pkcs11Uri pkcs11uri = new Pkcs11Uri(uri);

            CK_ATTRIBUTE[] attributes = null;
            Pkcs11UriUtils.GetObjectAttributes(pkcs11uri, out attributes);

            Assert.IsTrue(attributes != null);
            Assert.IsTrue(attributes.Length == 3);

            Assert.IsTrue(attributes[0].type == ConvertUtils.UInt64FromCKA(CKA.CKA_CLASS));
            NativeULong ckaClass = 0;
            CkaUtils.ConvertValue(ref attributes[0], out ckaClass);
            Assert.IsTrue(ckaClass == ConvertUtils.UInt64FromCKO(CKO.CKO_PRIVATE_KEY));

            Assert.IsTrue(attributes[1].type == ConvertUtils.UInt64FromCKA(CKA.CKA_LABEL));
            string ckaLabel = null;
            CkaUtils.ConvertValue(ref attributes[1], out ckaLabel);
            Assert.IsTrue(ckaLabel == "foo");

            Assert.IsTrue(attributes[2].type == ConvertUtils.UInt64FromCKA(CKA.CKA_ID));
            byte[] ckaId = null;
            CkaUtils.ConvertValue(ref attributes[2], out ckaId);
            Assert.IsTrue(Common.Helpers.ByteArraysMatch(ckaId, new byte[] { 0x01, 0x02, 0x03 }));
        }
    }
}
