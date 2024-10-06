/*
 *  Copyright 2012-2024 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.LowLevelAPI80;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI80
{
    /// <summary>
    /// Miscellaneous tests that do not fit any other category
    /// </summary>
    [TestFixture()]
    internal class _50_MiscellaneousTests
    {
        /// <summary>
        /// Test for CK_VERSION::ToString method.
        /// </summary>
        [Test()]
        public void _01_CkVersionToStringTest()
        {
            Helpers.CheckPlatform();

            CK_VERSION version = new CK_VERSION() { Major = new byte[] { 0x00 }, Minor = new byte[] { 0x00 } };

            for (int i = 0; i <= 255; i++)
            {
                version.Major[0] = Convert.ToByte(i);

                for (int j = 0; j <= 255; j++)
                {
                    version.Minor[0] = Convert.ToByte(j);

                    if (j == 0)
                    {
                        Assert.IsTrue(version.ToString() == i + "." + j);
                    }
                    else if (j > 0 && j < 10)
                    {
                        Assert.IsTrue(version.ToString() == i + ".0" + j);
                    }
                    else if (j >= 10 && j <= 99)
                    {
                        Assert.IsTrue(version.ToString() == i + "." + j);
                    }
                    else
                    {
                        try
                        {
                            version.ToString();
                            Assert.Fail("Exception expected but not thrown");
                        }
                        catch (Exception ex)
                        {
                            Assert.IsTrue(ex.Message == "Minor part of CK_VERSION exceeds the allowed maximum");
                        }
                    }
                }
            }
        }
    }
}
