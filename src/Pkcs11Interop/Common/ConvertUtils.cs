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

using System;
using System.Text;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps with data type conversions.
    /// </summary>
    public static class ConvertUtils
    {
        /// <summary>
        /// Converts uint to byte array
        /// </summary>
        /// <param name='value'>Uint that should be converted</param>
        /// <returns>Byte array with uint value</returns>
        public static byte[] UIntToBytes(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = Common.UnmanagedMemory.SizeOf(typeof(uint));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of uint ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to uint
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>Uint with value from byte array</returns>
        public static uint BytesToUInt(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(uint))))
                throw new Exception("Unable to convert bytes to uint");

            return BitConverter.ToUInt32(value, 0);
        }

        /// <summary>
        /// Converts ulong to byte array
        /// </summary>
        /// <param name='value'>Uint that should be converted</param>
        /// <returns>Byte array with ulong value</returns>
        public static byte[] ULongToBytes(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            int unmanagedSize = UnmanagedMemory.SizeOf(typeof(ulong));
            if (unmanagedSize != bytes.Length)
                throw new Exception(string.Format("Unmanaged size of ulong ({0}) does not match the length of produced byte array ({1})", unmanagedSize, bytes.Length));

            return bytes;
        }

        /// <summary>
        /// Converts byte array to ulong
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>Uint with value from byte array</returns>
        public static ulong BytesToULong(byte[] value)
        {
            if ((value == null) || (value.Length != UnmanagedMemory.SizeOf(typeof(ulong))))
                throw new Exception("Unable to convert bytes to ulong");

            return BitConverter.ToUInt64(value, 0);
        }

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
                output [i] = paddingByte;

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

        /// <summary>
        /// Converts byte array to hex encoded string
        /// </summary>
        /// <param name='value'>Byte array that should be converted</param>
        /// <returns>String with hex encoded value from byte array</returns>
        public static string BytesToHexString(byte[] value)
        {
            return BitConverter.ToString(value).Replace("-", "");
        }

        /// <summary>
        /// Converts hex encoded string to byte array
        /// </summary>
        /// <param name="value">String that should be converted</param>
        /// <returns>Byte array decoded from string</returns>
        public static byte[] HexStringToBytes(string value)
        {
            byte[] bytes = new byte[value.Length / 2];

            for (int i = 0; i < value.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);

            return bytes;
        }

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
    }
}
