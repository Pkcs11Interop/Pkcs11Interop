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

using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Mock.HighLevelAPI.Factories;

namespace Net.Pkcs11Interop.Mock.HighLevelAPI
{
    /// <summary>
    /// PKCS11-MOCK specific factories used by Pkcs11Interop library
    /// </summary>
    public class MockPkcs11Factories : Pkcs11Factories
    {
        /// <summary>
        /// Initializes new instance of MockPkcs11Factories class
        /// </summary>
        public MockPkcs11Factories()
        {
            _pkcs11Factory = new MockPkcs11Factory();
            _slotFactory = new MockSlotFactory();
            _sessionFactory = new MockSessionFactory();
        }
    }
}
