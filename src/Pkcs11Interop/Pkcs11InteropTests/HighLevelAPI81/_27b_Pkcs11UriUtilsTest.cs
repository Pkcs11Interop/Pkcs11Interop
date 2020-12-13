/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI81;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI81
{
    /// <summary>
    /// Pkcs11UriUtils tests
    /// </summary>
    [TestFixture()]
    public partial class _27_Pkcs11UriUtilsTest
    {
        /// <summary>
        /// LibraryInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _02_LibraryInfoMatches()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                LibraryInfo libraryInfo = pkcs11.GetInfo();

                // Empty URI
                Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

                // Unknown path attribute in URI
                pkcs11uri = new Pkcs11Uri(@"pkcs11:vendor=foobar");
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

                // All attributes matching
                Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = libraryInfo.ManufacturerId;
                pkcs11UriBuilder.LibraryDescription = libraryInfo.LibraryDescription;
                pkcs11UriBuilder.LibraryVersion = libraryInfo.LibraryVersion;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

                // LibraryManufacturer nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = "foobar";
                pkcs11UriBuilder.LibraryDescription = libraryInfo.LibraryDescription;
                pkcs11UriBuilder.LibraryVersion = libraryInfo.LibraryVersion;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

                // LibraryDescription nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = libraryInfo.ManufacturerId;
                pkcs11UriBuilder.LibraryDescription = "foobar";
                pkcs11UriBuilder.LibraryVersion = libraryInfo.LibraryVersion;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));

                // LibraryVersion nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = libraryInfo.ManufacturerId;
                pkcs11UriBuilder.LibraryDescription = libraryInfo.LibraryDescription;
                pkcs11UriBuilder.LibraryVersion = "0";
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, libraryInfo));
            }
        }

        /// <summary>
        /// SlotInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _03_SlotInfoMatches()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                List<Slot> slots = pkcs11.GetSlotList(SlotsType.WithTokenPresent);
                Assert.IsTrue(slots != null && slots.Count > 0);
                SlotInfo slotInfo = slots[0].GetSlotInfo();

                // Empty URI
                Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));

                // Unknown path attribute in URI
                pkcs11uri = new Pkcs11Uri(@"pkcs11:vendor=foobar");
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));

                // All attributes matching
                Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.SlotManufacturer = slotInfo.ManufacturerId;
                pkcs11UriBuilder.SlotDescription = slotInfo.SlotDescription;
                pkcs11UriBuilder.SlotId = slotInfo.SlotId;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));

                // Manufacturer nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.SlotManufacturer = "foobar";
                pkcs11UriBuilder.SlotDescription = slotInfo.SlotDescription;
                pkcs11UriBuilder.SlotId = slotInfo.SlotId;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));

                // Description nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.SlotManufacturer = slotInfo.ManufacturerId;
                pkcs11UriBuilder.SlotDescription = "foobar";
                pkcs11UriBuilder.SlotId = slotInfo.SlotId;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));

                // Slot id nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.SlotManufacturer = slotInfo.ManufacturerId;
                pkcs11UriBuilder.SlotDescription = slotInfo.SlotDescription;
                pkcs11UriBuilder.SlotId = slotInfo.SlotId + 1;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, slotInfo));
            }
        }

        /// <summary>
        /// TokenInfo matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _04_TokenInfoMatches()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                List<Slot> slots = pkcs11.GetSlotList(SlotsType.WithTokenPresent);
                Assert.IsTrue(slots != null && slots.Count > 0);
                TokenInfo tokenInfo = slots[0].GetTokenInfo();

                // Empty URI
                Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // Unknown path attribute in URI
                pkcs11uri = new Pkcs11Uri(@"pkcs11:vendor=foobar");
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // All attributes matching
                Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.Token = tokenInfo.Label;
                pkcs11UriBuilder.Manufacturer = tokenInfo.ManufacturerId;
                pkcs11UriBuilder.Serial = tokenInfo.SerialNumber;
                pkcs11UriBuilder.Model = tokenInfo.Model;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // Token nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.Token = "foobar";
                pkcs11UriBuilder.Manufacturer = tokenInfo.ManufacturerId;
                pkcs11UriBuilder.Serial = tokenInfo.SerialNumber;
                pkcs11UriBuilder.Model = tokenInfo.Model;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // Manufacturer nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.Token = tokenInfo.Label;
                pkcs11UriBuilder.Manufacturer = "foobar";
                pkcs11UriBuilder.Serial = tokenInfo.SerialNumber;
                pkcs11UriBuilder.Model = tokenInfo.Model;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // Serial nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.Token = tokenInfo.Label;
                pkcs11UriBuilder.Manufacturer = tokenInfo.ManufacturerId;
                pkcs11UriBuilder.Serial = "foobar";
                pkcs11UriBuilder.Model = tokenInfo.Model;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));

                // Model nonmatching
                pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.Token = tokenInfo.Label;
                pkcs11UriBuilder.Manufacturer = tokenInfo.ManufacturerId;
                pkcs11UriBuilder.Serial = tokenInfo.SerialNumber;
                pkcs11UriBuilder.Model = "foobar";
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, tokenInfo));
            }
        }

        /// <summary>
        /// ObjectAttribute matching against PKCS#11 URI
        /// </summary>
        [Test()]
        public void _05_ObjectAttributesMatches()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            // Empty URI
            Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
            List<ObjectAttribute> objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Empty attribute
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=;id=%01%02%03");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, string.Empty));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Unknown path attribute in URI
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03;foo=bar");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // All attributes matching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsTrue(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Type nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Object nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foo bar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            // Id nonmatching
            pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
            objectAttributes = new List<ObjectAttribute>();
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
            objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x04, 0x05, 0x06 }));
            Assert.IsFalse(Pkcs11UriUtils.Matches(pkcs11uri, objectAttributes));

            try
            {
                // Type present in URI but missing in list
                pkcs11uri = new Pkcs11Uri(@"pkcs11:type=private;object=foobar;id=%01%02%03");
                objectAttributes = new List<ObjectAttribute>();
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_LABEL, "foobar"));
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
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
                objectAttributes = new List<ObjectAttribute>();
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x01, 0x02, 0x03 }));
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
                objectAttributes = new List<ObjectAttribute>();
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_CLASS, CKO.CKO_PUBLIC_KEY));
                objectAttributes.Add(new ObjectAttribute(CKA.CKA_ID, new byte[] { 0x04, 0x05, 0x06 }));
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
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                // Get all slots
                List<Slot> allSlots = pkcs11.GetSlotList(SlotsType.WithTokenPresent);
                Assert.IsTrue(allSlots != null && allSlots.Count > 0);

                // Empty URI
                Pkcs11Uri pkcs11uri = new Pkcs11Uri(@"pkcs11:");
                List<Slot> matchedSlots = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11, true);
                Assert.IsTrue(matchedSlots.Count == allSlots.Count);

                // Unknown path attribute in URI
                pkcs11uri = new Pkcs11Uri(@"pkcs11:vendor=foobar");
                matchedSlots = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11, true);
                Assert.IsTrue(matchedSlots.Count == 0);

                // All attributes matching one slot
                LibraryInfo libraryInfo = pkcs11.GetInfo();
                SlotInfo slotInfo = allSlots[0].GetSlotInfo();
                TokenInfo tokenInfo = allSlots[0].GetTokenInfo();

                Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
                pkcs11UriBuilder.LibraryManufacturer = libraryInfo.ManufacturerId;
                pkcs11UriBuilder.LibraryDescription = libraryInfo.LibraryDescription;
                pkcs11UriBuilder.LibraryVersion = libraryInfo.LibraryVersion;
                pkcs11UriBuilder.SlotManufacturer = slotInfo.ManufacturerId;
                pkcs11UriBuilder.SlotDescription = slotInfo.SlotDescription;
                pkcs11UriBuilder.SlotId = slotInfo.SlotId;
                pkcs11UriBuilder.Token = tokenInfo.Label;
                pkcs11UriBuilder.Manufacturer = tokenInfo.ManufacturerId;
                pkcs11UriBuilder.Serial = tokenInfo.SerialNumber;
                pkcs11UriBuilder.Model = tokenInfo.Model;
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();

                matchedSlots = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11, true);
                Assert.IsTrue(matchedSlots.Count == 1);

                // One attribute nonmatching
                pkcs11UriBuilder.Serial = "foobar";
                pkcs11uri = pkcs11UriBuilder.ToPkcs11Uri();
                matchedSlots = Pkcs11UriUtils.GetMatchingSlotList(pkcs11uri, pkcs11, true);
                Assert.IsTrue(matchedSlots.Count == 0);
            }
        }

        /// <summary>
        /// PKCS#11 URI matching ObjectAttribute extraction
        /// </summary>
        [Test()]
        public void _07_GetObjectAttributes()
        {
            if (Platform.UnmanagedLongSize != 8 || Platform.StructPackingSize != 1)
                Assert.Inconclusive("Test cannot be executed on this platform");

            string uri = @"pkcs11:object=foo;type=private;id=%01%02%03";

            Pkcs11Uri pkcs11uri = new Pkcs11Uri(uri);

            List<ObjectAttribute> attributes = null;
            Pkcs11UriUtils.GetObjectAttributes(pkcs11uri, out attributes);

            Assert.IsTrue(attributes != null);
            Assert.IsTrue(attributes.Count == 3);

            Assert.IsTrue(attributes[0].Type == (ulong)CKA.CKA_CLASS);
            Assert.IsTrue(attributes[0].GetValueAsUlong() == (ulong)CKO.CKO_PRIVATE_KEY);

            Assert.IsTrue(attributes[1].Type == (ulong)CKA.CKA_LABEL);
            Assert.IsTrue(attributes[1].GetValueAsString() == "foo");

            Assert.IsTrue(attributes[2].Type == (ulong)CKA.CKA_ID);
            Assert.IsTrue(Common.Helpers.ByteArraysMatch(attributes[2].GetValueAsByteArray(), new byte[] { 0x01, 0x02, 0x03 }));
        }
    }
}
