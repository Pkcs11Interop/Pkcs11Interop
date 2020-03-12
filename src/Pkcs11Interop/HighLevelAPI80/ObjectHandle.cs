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
using Net.Pkcs11Interop.HighLevelAPI;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80
{
    /// <summary>
    /// Token-specific identifier for an object
    /// </summary>
    public class ObjectHandle : IObjectHandle
    {
        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        protected NativeULong _objectId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        public ulong ObjectId
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_objectId);
            }
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class with ObjectId set to CK_INVALID_HANDLE
        /// </summary>
        public ObjectHandle()
        {
            _objectId = CK.CK_INVALID_HANDLE;
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class
        /// </summary>
        /// <param name="objectId">PKCS#11 handle of object</param>
        public ObjectHandle(ulong objectId)
        {
            _objectId = ConvertUtils.UInt64FromUInt64(objectId);
        }
    }
}
