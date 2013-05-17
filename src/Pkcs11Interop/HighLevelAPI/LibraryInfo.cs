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

using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
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
        internal LibraryInfo(LowLevelAPI.CK_INFO ck_info)
        {
            _cryptokiVersion = ConvertUtils.CkVersionToString(ck_info.CryptokiVersion);
            _manufacturerId = ConvertUtils.BytesToUtf8String(ck_info.ManufacturerId, true);
            _flags = ck_info.Flags;
            _libraryDescription = ConvertUtils.BytesToUtf8String(ck_info.LibraryDescription, true);
            _libraryVersion = ConvertUtils.CkVersionToString(ck_info.LibraryVersion);
        }
    }
}
