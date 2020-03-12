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

using System;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// General constants
    /// </summary>
    public static class CK
    {
        /// <summary>
        /// The following value is always invalid if used as a session handle or object handle
        /// </summary>
        public const uint CK_INVALID_HANDLE = 0;

        /// <summary>
        /// Token and/or library is unable or unwilling to provide information
        /// </summary>
        public const uint CK_UNAVAILABLE_INFORMATION_4 = ~0U;

        /// <summary>
        /// Token and/or library is unable or unwilling to provide information
        /// </summary>
        public const ulong CK_UNAVAILABLE_INFORMATION_8 = ~0UL;

        /// <summary>
        /// Checks whether provided number has value of CK_UNAVAILABLE_INFORMATION constant
        /// </summary>
        /// <param name="value">Number to be checked</param>
        /// <returns>True if number has value of CK_UNAVAILABLE_INFORMATION constant false otherwise</returns>
        public static bool IsCkInformationUnavailable(ulong value)
        {
            if (Platform.NativeULongSize == 4)
                return (value == CK_UNAVAILABLE_INFORMATION_4);
            else
                return (value == CK_UNAVAILABLE_INFORMATION_8);
        }

        /// <summary>
        /// Specifies no practical limit
        /// </summary>
        public const uint CK_EFFECTIVELY_INFINITE = 0;

        /// <summary>
        /// No certificate category specified
        /// </summary>
        public const uint CK_CERTIFICATE_CATEGORY_UNSPECIFIED = 0;

        /// <summary>
        /// Certificate belongs to owner of the token
        /// </summary>
        public const uint CK_CERTIFICATE_CATEGORY_TOKEN_USER = 1;

        /// <summary>
        /// Certificate belongs to a certificate authority
        /// </summary>
        public const uint CK_CERTIFICATE_CATEGORY_AUTHORITY = 2;

        /// <summary>
        /// Certificate belongs to an end entity (i.e. not a CA)
        /// </summary>
        public const uint CK_CERTIFICATE_CATEGORY_OTHER_ENTITY = 3;

        /// <summary>
        /// No JAVA MIDP security domain specified
        /// </summary>
        public const uint CK_SECURITY_DOMAIN_UNSPECIFIED = 0;

        /// <summary>
        /// Manufacturer protection JAVA MIDP security domain
        /// </summary>
        public const uint CK_SECURITY_DOMAIN_MANUFACTURER = 1;

        /// <summary>
        /// Operator protection JAVA MIDP security domain
        /// </summary>
        public const uint CK_SECURITY_DOMAIN_OPERATOR = 2;

        /// <summary>
        /// Third party protection JAVA MIDP security domain
        /// </summary>
        public const uint CK_SECURITY_DOMAIN_THIRD_PARTY = 3;

        /// <summary>
        /// Decimal (default) (UTF8-encoded) format of OTP value
        /// </summary>
        public const uint CK_OTP_FORMAT_DECIMAL = 0;

        /// <summary>
        /// Hexadecimal (UTF8-encoded) format of OTP value
        /// </summary>
        public const uint CK_OTP_FORMAT_HEXADECIMAL = 1;

        /// <summary>
        /// Alphanumeric (UTF8-encoded) format of OTP value
        /// </summary>
        public const uint CK_OTP_FORMAT_ALPHANUMERIC = 2;

        /// <summary>
        /// Binary format of OTP value
        /// </summary>
        public const uint CK_OTP_FORMAT_BINARY = 3;

        /// <summary>
        /// OTP parameter, if supplied, will be ignored
        /// </summary>
        public const uint CK_OTP_PARAM_IGNORED = 0;

        /// <summary>
        /// OTP parameter may be supplied but need not be
        /// </summary>
        public const uint CK_OTP_PARAM_OPTIONAL = 1;

        /// <summary>
        /// OTP parameter must be supplied
        /// </summary>
        public const uint CK_OTP_PARAM_MANDATORY = 2;

        /// <summary>
        /// An actual OTP value
        /// </summary>
        public const uint CK_OTP_VALUE = 0;

        /// <summary>
        /// A UTF8 string containing a PIN for use when computing or verifying PIN-based OTP values
        /// </summary>
        public const uint CK_OTP_PIN = 1;

        /// <summary>
        /// Challenge to use when computing or verifying challenge-based OTP values
        /// </summary>
        public const uint CK_OTP_CHALLENGE = 2;

        /// <summary>
        /// UTC time value in the form YYYYMMDDhhmmss to use when computing or verifying time-based OTP values
        /// </summary>
        public const uint CK_OTP_TIME = 3;

        /// <summary>
        /// Counter value to use when computing or verifying counter-based OTP values
        /// </summary>
        public const uint CK_OTP_COUNTER = 4;

        /// <summary>
        /// Bit flags indicating the characteristics of the sought OTP as defined below
        /// </summary>
        public const uint CK_OTP_FLAGS = 5;

        /// <summary>
        /// Desired output length (overrides any default value)
        /// </summary>
        public const uint CK_OTP_OUTPUT_LENGTH = 6;

        /// <summary>
        /// Returned OTP format
        /// </summary>
        public const uint CK_OTP_OUTPUT_FORMAT = 7;
    }
}
