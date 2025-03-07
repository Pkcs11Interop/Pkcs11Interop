﻿/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.Factories
{
    /// <summary>
    /// Factory for creation of IObjectHandle instances
    /// </summary>
    public class ObjectHandleFactory : IObjectHandleFactory
    {
        /// <summary>
        /// Creates object identifier with CK_INVALID_HANDLE value
        /// </summary>
        /// <returns>Token-specific identifier for an object</returns>
        public IObjectHandle Create()
        {
            return new ObjectHandle();
        }

        /// <summary>
        /// Creates object identifier with provided value
        /// </summary>
        /// <param name="objectId">PKCS#11 handle of object</param>
        /// <returns>Token-specific identifier for an object</returns>
        public IObjectHandle Create(ulong objectId)
        {
            return new ObjectHandle(objectId);
        }
    }
}
