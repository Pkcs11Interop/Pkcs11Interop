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

using Net.Pkcs11Interop.Common;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI.Factories
{
    /// <summary>
    /// Developer uses this factory to create correct IPkcs11Library instances possibly extended with vendor specific methods.
    /// </summary>
    public class Pkcs11LibraryFactory : IPkcs11LibraryFactory
    {
        /// <summary>
        /// Platform specific factory for creation of IPkcs11Library instances
        /// </summary>
        private IPkcs11LibraryFactory _factory = null;

        /// <summary>
        /// Initializes a new instance of the Pkcs11LibraryFactory class
        /// </summary>
        public Pkcs11LibraryFactory()
        {
            if (Platform.NativeULongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI40.Factories.Pkcs11LibraryFactory();
                else
                    _factory = new HighLevelAPI41.Factories.Pkcs11LibraryFactory();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI80.Factories.Pkcs11LibraryFactory();
                else
                    _factory = new HighLevelAPI81.Factories.Pkcs11LibraryFactory();
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <returns>High level PKCS#11 wrapper</returns>
        public IPkcs11Library LoadPkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType)
        {
            return _factory.LoadPkcs11Library(factories, libraryPath, appType);
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <param name="initType">Source of PKCS#11 function pointers</param>
        /// <returns>High level PKCS#11 wrapper</returns>
        public IPkcs11Library LoadPkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType, InitType initType)
        {
            return _factory.LoadPkcs11Library(factories, libraryPath, appType, initType);
        }
    }
}
