/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using LLA40 = Net.Pkcs11Interop.LowLevelAPI40;
using LLA41 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA80 = Net.Pkcs11Interop.LowLevelAPI80;
using LLA81 = Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.Tests
{
    /// <summary>
    /// Test settings.
    /// </summary>
    public static class Settings
    {
        #region Properties that almost always need to be configured before the tests are executed

#if __ANDROID__
        /// <summary>
        /// Relative name or absolute path of unmanaged PKCS#11 library provided by smartcard or HSM vendor.
        /// </summary>
        public static string Pkcs11LibraryPath = @"libpkcs11-mock.so";
#elif __IOS__
        /// <summary>
        /// Relative name or absolute path of unmanaged PKCS#11 library provided by smartcard or HSM vendor.
        /// </summary>
        public static string Pkcs11LibraryPath = string.Empty;
#else
        /// <summary>
        /// Relative name or absolute path of unmanaged PKCS#11 library provided by smartcard or HSM vendor.
        /// </summary>
        public static string Pkcs11LibraryPath = @"pkcs11-mock-x86.dll";
#endif

        /// <summary>
        /// Flag indicating whether PKCS#11 library should use its internal native threading model for locking.
        /// This should be set to true in all multithreaded applications.
        /// </summary>
        public static bool UseOsLocking = true;

        /// <summary>
        /// Serial number of token (smartcard) that should be used by these tests.
        /// First slot with token present is used when both TokenSerial and TokenLabel properties are null.
        /// </summary>
        public static string TokenSerial = null;

        /// <summary>
        /// Label of the token (smartcard) that should be used by these tests.
        /// First slot with token present is used when both TokenSerial and TokenLabel properties are null.
        /// </summary>
        public static string TokenLabel = null;

        /// <summary>
        /// PIN of the SO user a.k.a. PUK.
        /// </summary>
        public static string SecurityOfficerPin = @"11111111";

        /// <summary>
        /// PIN of the normal user.
        /// </summary>
        public static string NormalUserPin = @"11111111";

        /// <summary>
        /// Application name that is used as a label for all objects created by these tests.
        /// </summary>
        public static string ApplicationName = @"Pkcs11Interop";

        #endregion

        #region Properties that are set automatically in class constructor

        /// <summary>
        /// Arguments passed to the C_Initialize function in LowLevelAPI40 tests.
        /// </summary>
        public static LLA40.CK_C_INITIALIZE_ARGS InitArgs40 = null;

        /// <summary>
        /// Arguments passed to the C_Initialize function in LowLevelAPI41 tests.
        /// </summary>
        public static LLA41.CK_C_INITIALIZE_ARGS InitArgs41 = null;

        /// <summary>
        /// Arguments passed to the C_Initialize function in LowLevelAPI80 tests.
        /// </summary>
        public static LLA80.CK_C_INITIALIZE_ARGS InitArgs80 = null;

        /// <summary>
        /// Arguments passed to the C_Initialize function in LowLevelAPI81 tests.
        /// </summary>
        public static LLA81.CK_C_INITIALIZE_ARGS InitArgs81 = null;

        /// <summary>
        /// PIN of the SO user a.k.a. PUK.
        /// </summary>
        public static byte[] SecurityOfficerPinArray = null;

        /// <summary>
        /// PIN of the normal user.
        /// </summary>
        public static byte[] NormalUserPinArray = null;

        /// <summary>
        /// Application name that is used as a label for all objects created by these tests.
        /// </summary>
        public static byte[] ApplicationNameArray = null;

        /// <summary>
        /// PKCS#11 URI that identifies private key usable in signature creation tests.
        /// </summary>
        public static string PrivateKeyUri = null;

        #endregion

        /// <summary>
        /// Static class constructor
        /// </summary>
        static Settings()
        {
            // Uncomment following three lines to enable logging of PKCS#11 calls with PKCS11-LOGGER library
            // System.Environment.SetEnvironmentVariable("PKCS11_LOGGER_LIBRARY_PATH", Pkcs11LibraryPath);
            // System.Environment.SetEnvironmentVariable("PKCS11_LOGGER_LOG_FILE_PATH", @"c:\pkcs11-logger.txt");
            // Pkcs11LibraryPath = @"c:\pkcs11-logger-x86.dll";

            // Setup arguments passed to the C_Initialize function
            if (UseOsLocking)
            {
                InitArgs40 = new LLA40.CK_C_INITIALIZE_ARGS();
                InitArgs40.Flags = CKF.CKF_OS_LOCKING_OK;

                InitArgs41 = new LLA41.CK_C_INITIALIZE_ARGS();
                InitArgs41.Flags = CKF.CKF_OS_LOCKING_OK;

                InitArgs80 = new LLA80.CK_C_INITIALIZE_ARGS();
                InitArgs80.Flags = CKF.CKF_OS_LOCKING_OK;

                InitArgs81 = new LLA81.CK_C_INITIALIZE_ARGS();
                InitArgs81.Flags = CKF.CKF_OS_LOCKING_OK;
            }

            // Convert strings to byte arrays
            SecurityOfficerPinArray = ConvertUtils.Utf8StringToBytes(SecurityOfficerPin);
            NormalUserPinArray = ConvertUtils.Utf8StringToBytes(NormalUserPin);
            ApplicationNameArray = ConvertUtils.Utf8StringToBytes(ApplicationName);

            // Build PKCS#11 URI that identifies private key usable in signature creation tests
            Pkcs11UriBuilder pkcs11UriBuilder = new Pkcs11UriBuilder();
            pkcs11UriBuilder.ModulePath = Pkcs11LibraryPath;
            pkcs11UriBuilder.Serial = TokenSerial;
            pkcs11UriBuilder.Token = TokenLabel;
            pkcs11UriBuilder.PinValue = NormalUserPin;
            pkcs11UriBuilder.Type = CKO.CKO_PRIVATE_KEY;
            pkcs11UriBuilder.Object = ApplicationName;
            
            PrivateKeyUri = pkcs11UriBuilder.ToString();
        }
    }
}
