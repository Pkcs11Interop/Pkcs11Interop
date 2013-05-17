/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
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
