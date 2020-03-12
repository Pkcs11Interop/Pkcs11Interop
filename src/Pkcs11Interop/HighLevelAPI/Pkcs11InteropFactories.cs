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

using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Factories to be used by Developer and Pkcs11Interop library
    /// </summary>
    public class Pkcs11InteropFactories
    {
        /// <summary>
        /// Factory for creation of IPkcs11Library instances
        /// </summary>
        protected IPkcs11LibraryFactory _pkcs11LibraryFactory = null;

        /// <summary>
        /// Developer uses this factory to create correct IPkcs11Library instances possibly extended with vendor specific methods.
        /// </summary>
        public IPkcs11LibraryFactory Pkcs11LibraryFactory
        {
            get
            {
                return _pkcs11LibraryFactory;
            }
        }

        /// <summary>
        /// Factory for creation of ISlot instances
        /// </summary>
        protected ISlotFactory _slotFactory = null;

        /// <summary>
        /// Pkcs11Interop uses this factory to create ISlot instances possibly extended with vendor specific methods.
        /// </summary>
        public ISlotFactory SlotFactory
        {
            get
            {
                return _slotFactory;
            }
        }

        /// <summary>
        /// Factory for creation of ISession instances
        /// </summary>
        protected ISessionFactory _sessionFactory = null;

        /// <summary>
        /// Pkcs11Interop uses this factory to create ISession instances possibly extended with vendor specific methods.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        /// <summary>
        /// Factory for creation of IObjectAttribute instances
        /// </summary>
        protected IObjectAttributeFactory _objectAttributeFactory = null;

        /// <summary>
        /// Developer uses this factory to create correct IObjectAttribute instances.
        /// </summary>
        public IObjectAttributeFactory ObjectAttributeFactory
        {
            get
            {
                return _objectAttributeFactory;
            }
        }

        /// <summary>
        /// Factory for creation of IObjectHandle instances
        /// </summary>
        protected IObjectHandleFactory _objectHandleFactory = null;

        /// <summary>
        /// Developer rarely uses this factory to create correct IObjectHandle instances.
        /// </summary>
        public IObjectHandleFactory ObjectHandleFactory
        {
            get
            {
                return _objectHandleFactory;
            }
        }

        /// <summary>
        /// Factory for creation of IMechanism instances
        /// </summary>
        protected IMechanismFactory _mechanismFactory = null;

        /// <summary>
        /// Developer uses this factory to create correct IMechanism instances.
        /// </summary>
        public IMechanismFactory MechanismFactory
        {
            get
            {
                return _mechanismFactory;
            }
        }

        /// <summary>
        /// Factory for creation of IMechanismParams instances
        /// </summary>
        protected IMechanismParamsFactory _mechanismParamsFactory = null;

        /// <summary>
        /// Developer uses this factory to create correct IMechanismParams instances.
        /// </summary>
        public IMechanismParamsFactory MechanismParamsFactory
        {
            get
            {
                return _mechanismParamsFactory;
            }
        }

        /// <summary>
        /// Initializes new instance of Pkcs11Factories class with default factories
        /// </summary>
        public Pkcs11InteropFactories()
        {
            _pkcs11LibraryFactory = new Pkcs11LibraryFactory();
            _slotFactory = new SlotFactory();
            _sessionFactory = new SessionFactory();
            _objectAttributeFactory = new ObjectAttributeFactory();
            _objectHandleFactory = new ObjectHandleFactory();
            _mechanismFactory = new MechanismFactory();
            _mechanismParamsFactory = new MechanismParamsFactory();
        }

        /// <summary>
        /// Initializes new instance of Pkcs11Factories class with custom or default factories
        /// </summary>
        /// <param name="pkcs11LibraryFactory">Custom factory for creation of IPkcs11Library instances or null for the default factory</param>
        /// <param name="slotFactory">Custom factory for creation of ISlot instances or null for the default factory</param>
        /// <param name="sessionFactory">Custom factory for creation of ISession instances or null for the default factory</param>
        /// <param name="objectAttributeFactory">Custom factory for creation of IObjectAttribute instances or null for the default factory</param>
        /// <param name="objectHandleFactory">Custom factory for creation of IObjectHandle instances or null for the default factory</param>
        /// <param name="mechanismFactory">Custom factory for creation of IMechanism instances or null for the default factory</param>
        /// <param name="mechanismParamsFactory">Custom factory for creation of IMechanismParams instances or null for the default factory</param>
        public Pkcs11InteropFactories(IPkcs11LibraryFactory pkcs11LibraryFactory, ISlotFactory slotFactory, ISessionFactory sessionFactory, IObjectAttributeFactory objectAttributeFactory, IObjectHandleFactory objectHandleFactory, IMechanismFactory mechanismFactory, IMechanismParamsFactory mechanismParamsFactory)
        {
            _pkcs11LibraryFactory = (pkcs11LibraryFactory != null) ? pkcs11LibraryFactory : new Pkcs11LibraryFactory();
            _slotFactory = (slotFactory != null) ? slotFactory : new SlotFactory();
            _sessionFactory = (sessionFactory != null) ? sessionFactory : new SessionFactory();
            _objectAttributeFactory = (objectAttributeFactory != null) ? objectAttributeFactory : new ObjectAttributeFactory();
            _objectHandleFactory = (objectHandleFactory != null) ? objectHandleFactory : new ObjectHandleFactory();
            _mechanismFactory = (mechanismFactory != null) ? mechanismFactory : new MechanismFactory();
            _mechanismParamsFactory = (mechanismParamsFactory != null) ? mechanismParamsFactory : new MechanismParamsFactory();
        }
    }
}
