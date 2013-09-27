/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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

namespace Net.Pkcs11Interop.Tests
{
    /// <summary>
    /// Test settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The PKCS#11 unmanaged library path
        /// </summary>
        public static string Pkcs11LibraryPath = @"siecap11.dll";

        /// <summary>
        /// The SO pin (PUK).
        /// </summary>
        public static string SecurityOfficerPin = @"11111111";

        /// <summary>
        /// The SO pin (PUK).
        /// </summary>
        public static byte[] SecurityOfficerPinArray = ConvertUtils.Utf8StringToBytes(SecurityOfficerPin);

        /// <summary>
        /// The normal user pin.
        /// </summary>
        public static string NormalUserPin = @"11111111";

        /// <summary>
        /// The normal user pin.
        /// </summary>
        public static byte[] NormalUserPinArray = ConvertUtils.Utf8StringToBytes(NormalUserPin);

        /// <summary>
        /// The name of the application.
        /// </summary>
        public static string ApplicationName = @"Pkcs11Interop";

        /// <summary>
        /// The name of the application.
        /// </summary>
        public static byte[] ApplicationNameArray = ConvertUtils.Utf8StringToBytes(ApplicationName);
    }
}

