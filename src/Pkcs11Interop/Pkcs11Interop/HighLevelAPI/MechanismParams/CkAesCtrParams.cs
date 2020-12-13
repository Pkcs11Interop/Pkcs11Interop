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

using System;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_AES_CTR mechanism
    /// </summary>
    public class CkAesCtrParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkAesCtrParams _params40 = null;

        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkAesCtrParams _params41 = null;

        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkAesCtrParams _params80 = null;

        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkAesCtrParams _params81 = null;

        /// <summary>
        /// Initializes a new instance of the CkAesCtrParams class.
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        public CkAesCtrParams(ulong counterBits, byte[] cb)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkAesCtrParams(Convert.ToUInt32(counterBits), cb);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkAesCtrParams(Convert.ToUInt32(counterBits), cb);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkAesCtrParams(counterBits, cb);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkAesCtrParams(counterBits, cb);
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
