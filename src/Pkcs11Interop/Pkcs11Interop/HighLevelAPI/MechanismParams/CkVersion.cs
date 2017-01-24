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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism
    /// </summary>
    public class CkVersion : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkVersion _params40 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkVersion _params41 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkVersion _params80 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkVersion _params81 = null;

        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        public byte Major
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _params40.Major : _params41.Major;
                else
                    return (Platform.StructPackingSize == 0) ? _params80.Major : _params81.Major;
            }
        }

        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        public byte Minor
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _params40.Minor : _params41.Minor;
                else
                    return (Platform.StructPackingSize == 0) ? _params80.Minor : _params81.Minor;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        public CkVersion(byte major, byte minor)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkVersion(major, minor);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkVersion(major, minor);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkVersion(major, minor);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkVersion(major, minor);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI40.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params40 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI41.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params41 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI80.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params80 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI81.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params81 = ckVersion;
        }
        
        #region IMechanismParams

        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _params40.ToMarshalableStructure() : _params41.ToMarshalableStructure();
            else
                return (Platform.StructPackingSize == 0) ? _params80.ToMarshalableStructure() : _params81.ToMarshalableStructure();
        }
        
        #endregion

        /// <summary>
        /// Returns a string that represents the current CkVersion object.
        /// </summary>
        /// <returns>String that represents the current CkVersion object.</returns>
        public override string ToString()
        {
            string version = null;

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    version = ((LowLevelAPI40.CK_VERSION)_params40.ToMarshalableStructure()).ToString();
                else
                    version = ((LowLevelAPI41.CK_VERSION)_params41.ToMarshalableStructure()).ToString();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    version = ((LowLevelAPI80.CK_VERSION)_params80.ToMarshalableStructure()).ToString();
                else
                    version = ((LowLevelAPI81.CK_VERSION)_params81.ToMarshalableStructure()).ToString();
            }
            
            return version;
        }
    }
}
