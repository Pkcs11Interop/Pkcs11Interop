/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Provides information about a particular mechanism
    /// </summary>
    public class MechanismInfo
    {
        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI40.MechanismInfo _mechanismInfo40 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI41.MechanismInfo _mechanismInfo41 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI80.MechanismInfo _mechanismInfo80 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI81.MechanismInfo _mechanismInfo81 = null;

        /// <summary>
        /// Mechanism
        /// </summary>
        public CKM Mechanism
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.Mechanism : _mechanismInfo41.Mechanism;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.Mechanism : _mechanismInfo81.Mechanism;
            }
        }

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.MinKeySize : _mechanismInfo41.MinKeySize;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.MinKeySize : _mechanismInfo81.MinKeySize;
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.MaxKeySize : _mechanismInfo41.MaxKeySize;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.MaxKeySize : _mechanismInfo81.MaxKeySize;
            }
        }

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        private MechanismFlags _mechanismFlags = null;

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        public MechanismFlags MechanismFlags
        {
            get
            {
                if (_mechanismFlags == null)
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _mechanismFlags = (Platform.StructPackingSize == 0) ? new MechanismFlags(_mechanismInfo40.MechanismFlags) : new MechanismFlags(_mechanismInfo41.MechanismFlags);
                    else
                        _mechanismFlags = (Platform.StructPackingSize == 0) ? new MechanismFlags(_mechanismInfo80.MechanismFlags) : new MechanismFlags(_mechanismInfo81.MechanismFlags);
                }

                return _mechanismFlags;
            }
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI40.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo40 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI41.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo41 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI80.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo80 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI81.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo81 = mechanismInfo;
        }
    }
}
