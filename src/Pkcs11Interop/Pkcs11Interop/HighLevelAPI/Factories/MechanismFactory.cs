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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI.Factories
{
    /// <summary>
    /// Factory for creation of IMechanism instances
    /// </summary>
    public class MechanismFactory : IMechanismFactory
    {
        #region Static instance of the factory

        /// <summary>
        /// Ready to use static instance of the factory
        /// </summary>
        private static MechanismFactory _instance = null;

        /// <summary>
        /// Ready to use static instance of the factory
        /// </summary>
        public static MechanismFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Static constructor that initializes static instance of the factory
        /// </summary>
        static MechanismFactory()
        {
            _instance = new MechanismFactory();
        }

        #endregion

        /// <summary>
        /// Platform specific factory for creation of IMechanism instances
        /// </summary>
        private IMechanismFactory _factory = null;

        /// <summary>
        /// Initializes a new instance of the MechanismFactory class
        /// </summary>
        public MechanismFactory()
        {
            if (Platform.UnmanagedLongSize == 4)
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
        public IMechanism CreateMechanism(ulong type)
        {
            return _factory.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <returns>Mechanism with no parameter</returns>
        public IMechanism CreateMechanism(CKM type)
        {
            return _factory.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism CreateMechanism(ulong type, byte[] parameter)
        {
            return _factory.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism CreateMechanism(CKM type, byte[] parameter)
        {
            return _factory.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism CreateMechanism(ulong type, IMechanismParams parameter)
        {
            return _factory.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism and its parameter</returns>
        public IMechanism CreateMechanism(CKM type, IMechanismParams parameter)
        {
            return _factory.CreateMechanism(type, parameter);
        }
    }
}
