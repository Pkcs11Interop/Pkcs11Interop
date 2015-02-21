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
