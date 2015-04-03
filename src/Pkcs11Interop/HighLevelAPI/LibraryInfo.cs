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
        private HighLevelAPI4.LibraryInfo _libraryInfo4 = null;

        /// <summary>
        /// Platform specific LibraryInfo
        /// </summary>
        private HighLevelAPI8.LibraryInfo _libraryInfo8 = null;

        /// <summary>
        /// Cryptoki interface version number
        /// </summary>
        public string CryptokiVersion
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _libraryInfo4.CryptokiVersion : _libraryInfo8.CryptokiVersion;
            }
        }

        /// <summary>
        /// ID of the Cryptoki library manufacturer
        /// </summary>
        public string ManufacturerId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _libraryInfo4.ManufacturerId : _libraryInfo8.ManufacturerId;
            }
        }

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        public ulong Flags
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _libraryInfo4.Flags : _libraryInfo8.Flags;
            }
        }

        /// <summary>
        /// Description of the library
        /// </summary>
        public string LibraryDescription
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _libraryInfo4.LibraryDescription : _libraryInfo8.LibraryDescription;
            }
        }

        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        public string LibraryVersion
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _libraryInfo4.LibraryVersion : _libraryInfo8.LibraryVersion;
            }
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI4.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo4 = libraryInfo;
        }

        /// <summary>
        /// Converts platform specific LibraryInfo to platfrom neutral LibraryInfo
        /// </summary>
        /// <param name="libraryInfo">Platform specific LibraryInfo</param>
        internal LibraryInfo(HighLevelAPI8.LibraryInfo libraryInfo)
        {
            if (libraryInfo == null)
                throw new ArgumentNullException("libraryInfo");

            _libraryInfo8 = libraryInfo;
        }
    }
}
