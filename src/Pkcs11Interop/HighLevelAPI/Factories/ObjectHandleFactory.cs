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
    /// Developer rarely uses this factory to create correct IObjectHandle instances.
    /// </summary>
    public class ObjectHandleFactory : IObjectHandleFactory
    {
        /// <summary>
        /// Platform specific factory for creation of IObjectHandle instances
        /// </summary>
        private IObjectHandleFactory _factory = null;

        /// <summary>
        /// Initializes a new instance of the ObjectHandleFactory class
        /// </summary>
        public ObjectHandleFactory()
        {
            if (Platform.NativeULongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI40.Factories.ObjectHandleFactory();
                else
                    _factory = new HighLevelAPI41.Factories.ObjectHandleFactory();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI80.Factories.ObjectHandleFactory();
                else
                    _factory = new HighLevelAPI81.Factories.ObjectHandleFactory();
            }
        }

        /// <summary>
        /// Creates object identifier with CK_INVALID_HANDLE value
        /// </summary>
        /// <returns>Token-specific identifier for an object</returns>
        public IObjectHandle Create()
        {
            return _factory.Create();
        }

        /// <summary>
        /// Creates object identifier with provided value
        /// </summary>
        /// <param name="objectId">PKCS#11 handle of object</param>
        /// <returns>Token-specific identifier for an object</returns>
        public IObjectHandle Create(ulong objectId)
        {
            return _factory.Create(objectId);
        }
    }
}
