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

namespace Net.Pkcs11Interop.Tests
{
    /// <summary>
    /// Test settings.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// The PKCS#11 unmanaged library path
        /// </summary>
        public static string Pkcs11LibraryPath = @"siecap11.dll";

        /// <summary>
        /// Serial number of token that should be used in tests
        /// </summary>
        public static string TokenSerial = @"7BFF2737350B262C";

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

        /// <summary>
        /// PKCS#11 URI that identifies private key usable in signature creation tests
        /// </summary>
        public static string PrivateKeyUri = null;

        /// <summary>
        /// Static constructor
        /// </summary>
        static Settings()
        {
            // Uncomment following three lines to enable logging of PKCS#11 calls with PKCS11-LOGGER library
            // System.Environment.SetEnvironmentVariable("PKCS11_LOGGER_LIBRARY_PATH", @"siecap11.dll");
            // System.Environment.SetEnvironmentVariable("PKCS11_LOGGER_LOG_FILE_PATH", @"c:\pkcs11-logger.txt");
            // Pkcs11LibraryPath = @"c:\pkcs11-logger-x86.dll";

            // Build PKCS#11 URI that identifies private key usable in signature creation tests
            Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
            pkcs11UriBuilder.Serial = TokenSerial;
            pkcs11UriBuilder.Type = CKO.CKO_PRIVATE_KEY;
            pkcs11UriBuilder.Object = "John Doe";
            pkcs11UriBuilder.ModulePath = Pkcs11LibraryPath;
            pkcs11UriBuilder.PinValue = NormalUserPin;
            PrivateKeyUri = pkcs11UriBuilder.ToString();
        }
    }
}
