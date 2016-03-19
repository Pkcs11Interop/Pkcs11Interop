/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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

namespace Net.Pkcs11Interop.Tests.Common
{
    /// <summary>
    /// Helper methods for PKCS#11 URI tests.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Checks whether two byte arrays contain the same bytes
        /// </summary>
        /// <param name="array1">First byte array</param>
        /// <param name="array2">Second byte array</param>
        /// <returns>True when byte arrays contain the same bytes false otherwise</returns>
        public static bool ByteArraysMatch(byte[] array1, byte[] array2)
        {
            if (array1 == null)
            {
                if (array2 != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (array2 == null)
                {
                    return false;
                }
                else
                {
                    if (array1.Length != array2.Length)
                        return false;

                    for (int i = 0; i < array1.Length; i++)
                    {
                        if (array1[i] != array2[i])
                            return false;
                    }

                    return true;
                }
            }
        }
    }
}
