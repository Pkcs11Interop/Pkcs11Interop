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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// General constants
    /// </summary>
    public class CK
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
        public static bool IsCkInformationUnavailable(ulong value)
        {
            if (Platform.UnmanagedLongSize == 4)
                return (value == CK_UNAVAILABLE_INFORMATION_4);
            else
                return (value == CK_UNAVAILABLE_INFORMATION_8);
        }

        /// <summary>
        /// Specifies no practical limit
        /// </summary>
        public const uint CK_EFFECTIVELY_INFINITE = 0;

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
