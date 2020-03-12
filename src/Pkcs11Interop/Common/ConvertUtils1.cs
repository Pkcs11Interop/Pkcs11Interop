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
using System.Text;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps with data type conversions.
    /// </summary>
    public static partial class ConvertUtils
    {
        #region Bool <-> Bytes

        /// <summary>
        /// Converts bool to byte array
        /// </summary>
        /// <param name='value'>Bool that should be converted</param>
        /// <returns>Byte array with bool value</returns>
        public static byte[] BoolToBytes(bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // Cryptoki uses boolean flag with size of 1 byte
            int unmanagedSize = 1;
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of bool ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));
            
            return bytes;
        }

        /// <summary>
        /// Converts byte array to bool
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>Bool with value from byte array</returns>
        public static bool BytesToBool(byte[] value)
        {
            // Cryptoki uses boolean flag with size of 1 byte
            if ((value == null) || (value.Length != 1))
                throw new Exception("Unable to convert bytes to bool");
            
            return BitConverter.ToBoolean(value, 0);
        }

        #endregion

        #region Utf8String <-> Bytes

        /// <summary>
        /// Converts UTF-8 string to byte array (not null terminated)
        /// </summary>
        /// <param name='value'>String that should be converted</param>
        /// <returns>Byte array with string value</returns>
        public static byte[] Utf8StringToBytes(string value)
        {
            return (value == null) ? null : UTF8Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// Converts UTF-8 string to byte array padded or trimmed to specified length
        /// </summary>
        /// <param name='value'>String that should be converted</param>
        /// <param name='outputLength'>Expected length of byte array</param>
        /// <param name='paddingByte'>Padding byte that will be used for padding to expected length</param>
        /// <returns>Byte array with string value padded or trimmed to specified length</returns>
        public static byte[] Utf8StringToBytes(string value, int outputLength, byte paddingByte)
        {
            if (outputLength < 1)
                throw new ArgumentException("Value has to be positive number", "outputLength");

            byte[] output = new byte[outputLength];
            for (int i = 0; i < outputLength; i++)
                output[i] = paddingByte;

            if (value != null)
            {
                byte[] bytes = ConvertUtils.Utf8StringToBytes(value);
                if (bytes.Length > outputLength)
                    Array.Copy(bytes, 0, output, 0, outputLength);
                else
                    Array.Copy(bytes, 0, output, 0, bytes.Length);
            }

            return output;
        }

        /// <summary>
        /// Converts byte array (not null terminated) to UTF-8 string
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>String with value from byte array</returns>
        public static string BytesToUtf8String(byte[] value)
        {
            return (value == null) ? null : Encoding.UTF8.GetString(value, 0, value.Length);
        }

        /// <summary>
        /// Converts byte array to UTF-8 string (not null terminated)
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <param name='trimEnd'>Flag indicating whether white space characters should be removed from the end of resulting string</param>
        /// <returns>String with value from byte array</returns>
        public static string BytesToUtf8String(byte[] value, bool trimEnd)
        {
            string result = BytesToUtf8String(value);

            if ((value != null) && (trimEnd))
                result = result.TrimEnd(null);

            return result;
        }

        /// <summary>
        /// Converts specified range of byte array to UTF-8 string (not null terminated)
        /// </summary>
        /// <param name='value'>Byte array that should be processed</param>
        /// <param name='index'>Starting index of bytes to decode</param>
        /// <param name='count'>Number of bytes to decode</param>
        /// <returns>String with value from byte array</returns>
        public static string BytesToUtf8String(byte[] value, int index, int count)
        {
            return (value == null) ? null : UTF8Encoding.UTF8.GetString(value, index, count);
        }

        #endregion

        #region UtcTimeString <-> DateTime

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

        #endregion

        #region String <-> HexString

        /// <summary>
        /// Converts byte array to hex encoded string
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>String with hex encoded value from byte array</returns>
        public static string BytesToHexString(byte[] value)
        {
            if (value == null)
                return null;

            return BitConverter.ToString(value).Replace("-", "");
        }

        /// <summary>
        /// Converts hex encoded string to byte array
        /// </summary>
        /// <param name="value">String that should be converted</param>
        /// <returns>Byte array decoded from string</returns>
        public static byte[] HexStringToBytes(string value)
        {
            if (value == null)
                return null;

            if ((value.Length % 2) != 0)
                throw new ArgumentException("Hex encoded string must contain an even number of characters", "value");

            byte[] bytes = new byte[value.Length / 2];

            for (int i = 0; i < value.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);

            return bytes;
        }

        #endregion

        #region String <-> Base64String

        /// <summary>
        /// Converts byte array to Base64 encoded string
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>String with Base64 encoded value from byte array</returns>
        public static string BytesToBase64String(byte[] value)
        {
            return Convert.ToBase64String(value);
        }

        /// <summary>
        /// Converts Base64 encoded string to byte array
        /// </summary>
        /// <param name="value">String that should be converted</param>
        /// <returns>Byte array decoded from string</returns>
        public static byte[] Base64StringToBytes(string value)
        {
            return Convert.FromBase64String(value);
        }

        #endregion

    }
}
