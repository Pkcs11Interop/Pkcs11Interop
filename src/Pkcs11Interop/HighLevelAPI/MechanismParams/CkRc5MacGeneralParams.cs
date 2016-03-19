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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RC5_MAC_GENERAL mechanism
    /// </summary>
    public class CkRc5MacGeneralParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkRc5MacGeneralParams _params40 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkRc5MacGeneralParams _params41 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkRc5MacGeneralParams _params80 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkRc5MacGeneralParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkRc5MacGeneralParams class.
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        public CkRc5MacGeneralParams(ulong wordsize, ulong rounds, ulong macLength)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkRc5MacGeneralParams(Convert.ToUInt32(wordsize), Convert.ToUInt32(rounds), Convert.ToUInt32(macLength));
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkRc5MacGeneralParams(Convert.ToUInt32(wordsize), Convert.ToUInt32(rounds), Convert.ToUInt32(macLength));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkRc5MacGeneralParams(wordsize, rounds, macLength);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkRc5MacGeneralParams(wordsize, rounds, macLength);
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
