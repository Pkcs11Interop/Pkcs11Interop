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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI40;

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
        private uint _flags = 0;

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        public uint Flags
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
