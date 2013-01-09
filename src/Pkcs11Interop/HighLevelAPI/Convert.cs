/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Utility class that helps with data type conversions.
    /// </summary>
    internal class Convert
    {
        /// <summary>
        /// Converts byte array to UTF8 string.
        /// </summary>
        /// <param name='bytes'>Byte array that should be converted.</param>
        /// <param name='trimEnd'>Flag indicating whether white space characters should be removed from the end of resulting string.</param>
        /// <returns>UTF8 string</returns>
        public static string ByteArrayToUtf8String(byte[] bytes, bool trimEnd)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            string result = UTF8Encoding.UTF8.GetString(bytes);

            if (trimEnd)
                result = result.TrimEnd(null);

            return result;
        }

        /// <summary>
        /// Converts CK_VERSION to string
        /// </summary>
        /// <param name='ck_version'>CK_VERSION structure that should be converted.</param>
        /// <returns>String with version information.</returns>
        public static string CkVersionToString(LowLevelAPI.CK_VERSION ck_version)
        {
            return string.Format("{0}.{1}", ck_version.Major[0], ck_version.Minor[0]);
        }

        /// <summary>
        /// Converts string with UTC time to DateTime
        /// </summary>
        /// <param name='utcTime'>UTC time that should be converted (formatted as string of length 16 represented in the format YYYYMMDDhhmmssxx).</param>
        /// <returns>DateTime if successful, null otherwise.</returns>
        public static DateTime? UtcTimeStringToDateTime(string utcTime)
        {
            DateTime? dateTime = null;

            if (!string.IsNullOrEmpty(utcTime))
            {
                if (utcTime.Length == 16)
                {
                    int year = Int32.Parse(utcTime.Substring(0, 4));
                    int month = Int32.Parse(utcTime.Substring(4, 2));
                    int day = Int32.Parse(utcTime.Substring(6, 2));
                    int hour = Int32.Parse(utcTime.Substring(8, 2));
                    int minute = Int32.Parse(utcTime.Substring(10, 2));
                    int second = Int32.Parse(utcTime.Substring(12, 2));
                    int millisecond = Int32.Parse(utcTime.Substring(14, 2));

                    dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
                }
            }

            return dateTime;
        }
    }
}
