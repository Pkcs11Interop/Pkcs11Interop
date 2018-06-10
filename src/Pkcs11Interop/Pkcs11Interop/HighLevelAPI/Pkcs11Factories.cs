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

using System;
using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Factories to be used by Developer and Pkcs11Interop library
    /// </summary>
    public class Pkcs11Factories
    {
        /// <summary>
        /// Factory for creation of IPkcs11 instances
        /// </summary>
        protected IPkcs11Factory _pkcs11Factory = null;

        /// <summary>
        /// Developer uses this factory to create correct IPkcs11 instances possibly extended with vendor specific methods.
        /// </summary>
        public IPkcs11Factory Pkcs11Factory
        {
            get
            {
                return _pkcs11Factory;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Pkcs11Factory");

                _pkcs11Factory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SlotFactory");

                _slotFactory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SessionFactory");

                _sessionFactory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ObjectAttributeFactory");

                _objectAttributeFactory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ObjectHandleFactory");

                _objectHandleFactory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MechanismFactory");

                _mechanismFactory = value;
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MechanismParamsFactory");

                _mechanismParamsFactory = value;
            }
        }

        /// <summary>
        /// Initializes new instance of Pkcs11Factories class
        /// </summary>
        public Pkcs11Factories()
        {
            _pkcs11Factory = new Pkcs11Factory();
            _slotFactory = new SlotFactory();
            _sessionFactory = new SessionFactory();
            _objectAttributeFactory = new ObjectAttributeFactory();
            _objectHandleFactory = new ObjectHandleFactory();
            _mechanismFactory = new MechanismFactory();
            _mechanismParamsFactory = new MechanismParamsFactory();
        }
    }
}
