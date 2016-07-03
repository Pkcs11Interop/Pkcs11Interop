/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// General information about PKCS#11 library (CK_INFO)
    /// </summary>
    public class LibraryInfo
    {
        /// <summary>
        /// Platform specific LibraryInfo
        /// </summary>
        private HighLevelAPI40.LibraryInfo _libraryInfo40 = null;

        /// <summary>
        /// Platform specific LibraryInfo
        /// </summary>
        private HighLevelAPI41.LibraryInfo _libraryInfo41 = null;

        /// <summary>
        /// Platform specific LibraryInfo
        /// </summary>
        private HighLevelAPI80.LibraryInfo _libraryInfo80 = null;

        /// <summary>
        /// Platform specific LibraryInfo
        /// </summary>
        private HighLevelAPI81.LibraryInfo _libraryInfo81 = null;

        /// <summary>
        /// Cryptoki interface version number
        /// </summary>
        public string CryptokiVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _libraryInfo40.CryptokiVersion : _libraryInfo41.CryptokiVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _libraryInfo80.CryptokiVersion : _libraryInfo81.CryptokiVersion;
            }
        }

        /// <summary>
        /// ID of the Cryptoki library manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _libraryInfo40.ManufacturerId : _libraryInfo41.ManufacturerId;
                else
                    return (Platform.StructPackingSize == 0) ? _libraryInfo80.ManufacturerId : _libraryInfo81.ManufacturerId;
            }
        }

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _libraryInfo40.Flags : _libraryInfo41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _libraryInfo80.Flags : _libraryInfo81.Flags;
            }
        }

        /// <summary>
        /// Description of the library
        /// </summary>
        public string LibraryDescription
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _libraryInfo40.LibraryDescription : _libraryInfo41.LibraryDescription;
                else
                    return (Platform.StructPackingSize == 0) ? _libraryInfo80.LibraryDescription : _libraryInfo81.LibraryDescription;
            }
        }

        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        public string LibraryVersion
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _libraryInfo40.LibraryVersion : _libraryInfo41.LibraryVersion;
                else
                    return (Platform.StructPackingSize == 0) ? _libraryInfo80.LibraryVersion : _libraryInfo81.LibraryVersion;
            }
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI40.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo40 = libraryInfo;
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI41.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo41 = libraryInfo;
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI80.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo80 = libraryInfo;
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI81.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo81 = libraryInfo;
        }
    }
}
