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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI.Factories
{
    /// <summary>
    /// Developer uses this factory to create correct IMechanism instances.
    /// </summary>
    public class MechanismFactory : IMechanismFactory
    {
        /// <summary>
        /// Platform specific factory for creation of IMechanism instances
        /// </summary>
        private IMechanismFactory _factory = null;

        /// <summary>
        /// Initializes a new instance of the MechanismFactory class
        /// </summary>
        public MechanismFactory()
        {
            if (Platform.NativeULongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI40.Factories.MechanismFactory();
                else
                    _factory = new HighLevelAPI41.Factories.MechanismFactory();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI80.Factories.MechanismFactory();
                else
                    _factory = new HighLevelAPI81.Factories.MechanismFactory();
            }
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <returns>Mechanism with no parameter</returns>
        public IMechanism Create(ulong type)
        {
            return _factory.Create(type);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <returns>Mechanism with no parameter</returns>
        public IMechanism Create(CKM type)
        {
            return _factory.Create(type);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism Create(ulong type, byte[] parameter)
        {
            return _factory.Create(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism Create(CKM type, byte[] parameter)
        {
            return _factory.Create(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism Create(ulong type, IMechanismParams parameter)
        {
            return _factory.Create(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism Create(CKM type, IMechanismParams parameter)
        {
            return _factory.Create(type, parameter);
        }
    }
}
