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
    /// Parameters for the CKM_RSA_PKCS_PSS mechanism
    /// </summary>
    public class CkRsaPkcsPssParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkRsaPkcsPssParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkRsaPkcsPssParams _params40 = null;

        /// <summary>
        /// Platform specific CkRsaPkcsPssParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkRsaPkcsPssParams _params41 = null;

        /// <summary>
        /// Platform specific CkRsaPkcsPssParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkRsaPkcsPssParams _params80 = null;

        /// <summary>
        /// Platform specific CkRsaPkcsPssParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkRsaPkcsPssParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsPssParams class.
        /// </summary>
        /// <param name='hashAlg'>Hash algorithm used in the PSS encoding (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='len'>Length, in bytes, of the salt value used in the PSS encoding</param>
        public CkRsaPkcsPssParams(ulong hashAlg, ulong mgf, ulong len)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkRsaPkcsPssParams(Convert.ToUInt32(hashAlg), Convert.ToUInt32(mgf), Convert.ToUInt32(len));
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkRsaPkcsPssParams(Convert.ToUInt32(hashAlg), Convert.ToUInt32(mgf), Convert.ToUInt32(len));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkRsaPkcsPssParams(hashAlg, mgf, len);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkRsaPkcsPssParams(hashAlg, mgf, len);
            }
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
    }
}
