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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// General information about PKCS#11 library (CK_INFO)
    /// </summary>
    public interface ILibraryInfo
    {
        /// <summary>
        /// Cryptoki interface version number
        /// </summary>
        string CryptokiVersion
        {
            get;
        }
        
        /// <summary>
        /// ID of the Cryptoki library manufacturer
        /// </summary>
        string ManufacturerId
        {
            get;
        }

        /// <summary>
        /// Bit flags reserved for future versions
        /// </summary>
        ulong Flags
        {
            get;
        }

        /// <summary>
        /// Description of the library
        /// </summary>
        string LibraryDescription
        {
            get;
        }

        /// <summary>
        /// Cryptoki library version number
        /// </summary>
        string LibraryVersion
        {
            get;
        }
    }
}
