/*
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

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Factory for creation of IPkcs11UriUtils instances
    /// </summary>
    interface IPkcs11UriUtilsFactory
    {
        /// <summary>
        /// Creates IPkcs11UriUtils instance
        /// </summary>
        /// <returns>Utility class connecting PKCS#11 URI and Pkcs11Interop types</returns>
        IPkcs11UriUtils CreatePkcs11UriUtils();
    }
}
