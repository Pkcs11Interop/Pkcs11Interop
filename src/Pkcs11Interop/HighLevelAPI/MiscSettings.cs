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
using System.Collections.Generic;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Miscellaneous settings that operation of Pkcs11Interop library
    /// </summary>
    public static class MiscSettings
    {
        /// <summary>
        /// Attributes that are known to contain nested attributes
        /// </summary>
        private static Dictionary<ulong, string> _attributesWithNestedAttributes = null;

        /// <summary>
        /// Attributes that are known to contain nested attributes
        /// </summary>
        public static Dictionary<ulong, string> AttributesWithNestedAttributes
        {
            get
            {
                return _attributesWithNestedAttributes;
            }
        }

        /// <summary>
        /// Initializes members of MiscSettings class
        /// </summary>
        static MiscSettings()
        {
            _attributesWithNestedAttributes = new Dictionary<ulong, string>();
            _attributesWithNestedAttributes.Add(ConvertUtils.UInt64FromCKA(CKA.CKA_WRAP_TEMPLATE), "CKA_WRAP_TEMPLATE");
            _attributesWithNestedAttributes.Add(ConvertUtils.UInt64FromCKA(CKA.CKA_UNWRAP_TEMPLATE), "CKA_UNWRAP_TEMPLATE");
            _attributesWithNestedAttributes.Add(ConvertUtils.UInt64FromCKA(CKA.CKA_DERIVE_TEMPLATE), "CKA_DERIVE_TEMPLATE");
        }
    }
}
