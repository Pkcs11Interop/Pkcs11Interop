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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI81.MechanismParams;
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RC2_MAC_GENERAL mechanism
    /// </summary>
    public class CkRc2MacGeneralParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RC2_MAC_GENERAL_PARAMS _lowLevelStruct = new CK_RC2_MAC_GENERAL_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRc2MacGeneralParams class.
        /// </summary>
        /// <param name='effectiveBits'>The effective number of bits in the RC2 search space</param>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        public CkRc2MacGeneralParams(NativeULong effectiveBits, NativeULong macLength)
        {
            _lowLevelStruct.EffectiveBits = effectiveBits;
            _lowLevelStruct.MacLength = macLength;
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            return _lowLevelStruct;
        }
        
        #endregion
    }
}
