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
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using System;
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

