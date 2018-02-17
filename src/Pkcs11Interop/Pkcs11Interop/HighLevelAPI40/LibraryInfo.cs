﻿/*
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
using Net.Pkcs11Interop.LowLevelAPI40;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// General information about PKCS#11 library (CK_INFO)
    /// </summary>
    public class LibraryInfo
    {
        /// <summary>
        /// Cryptoki interface version number
        /// </summary>
        private string _cryptokiVersion = null;

        /// <summary>
        /// Cryptoki interface version number
        /// </summary>
        public string CryptokiVersion
        {
            get
            {
                return _cryptokiVersion;
            }
        }

        /// <summary>
        /// ID of the Cryptoki library manufacturer
        /// </summary>
        private string _manufacturerId = null;

        /// <summary>
        /// ID of the Cryptoki library manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return _manufacturerId;
            }
        }

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        private NativeULong _flags = 0;

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        public NativeULong Flags
        {
            get
            {
                return _flags;
            }
        }

        /// <summary>
        /// Description of the library
        /// </summary>
        private string _libraryDescription = null;

        /// <summary>
        /// Description of the library
        /// </summary>
        public string LibraryDescription
        {
            get
            {
                return _libraryDescription;
            }
        }

        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        private string _libraryVersion = null;
        
        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        public string LibraryVersion
        {
            get
            {
                return _libraryVersion;
            }
        }

        /// <summary>
        /// Converts low level CK_INFO structure to high level LibraryInfo class
        /// </summary>
        /// <param name="ck_info">Low level CK_INFO structure</param>
        internal LibraryInfo(CK_INFO ck_info)
        {
            _cryptokiVersion = ck_info.CryptokiVersion.ToString();
            _manufacturerId = ConvertUtils.BytesToUtf8String(ck_info.ManufacturerId, true);
            _flags = ck_info.Flags;
            _libraryDescription = ConvertUtils.BytesToUtf8String(ck_info.LibraryDescription, true);
            _libraryVersion = ck_info.LibraryVersion.ToString();
        }
    }
}
