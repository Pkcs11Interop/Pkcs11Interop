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
using Net.Pkcs11Interop.LowLevelAPI81;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Provides information about a particular mechanism
    /// </summary>
    public class MechanismInfo : IMechanismInfo
    {
        /// <summary>
        /// Mechanism
        /// </summary>
        protected CKM _mechanism = 0;

        /// <summary>
        /// Mechanism
        /// </summary>
        public CKM Mechanism
        {
            get
            {
                return _mechanism;
            }
        }

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        protected NativeULong _minKeySize = 0;

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_minKeySize);
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        protected NativeULong _maxKeySize = 0;

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize
        {
            get
            {
                return ConvertUtils.UInt64ToUInt64(_maxKeySize);
            }
        }

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        protected MechanismFlags _mechanismFlags = null;

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        public IMechanismFlags MechanismFlags
        {
            get
            {
                return _mechanismFlags;
            }
        }

        /// <summary>
        /// Converts low level CK_MECHANISM_INFO structure to high level MechanismInfo class
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <param name="ck_mechanism_info">Low level CK_MECHANISM_INFO structure</param>
        protected internal MechanismInfo(CKM mechanism, CK_MECHANISM_INFO ck_mechanism_info)
        {
            _mechanism = mechanism;
            _minKeySize = ck_mechanism_info.MinKeySize;
            _maxKeySize = ck_mechanism_info.MaxKeySize;
            _mechanismFlags = new MechanismFlags(ck_mechanism_info.Flags);
        }
    }
}
