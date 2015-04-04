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
