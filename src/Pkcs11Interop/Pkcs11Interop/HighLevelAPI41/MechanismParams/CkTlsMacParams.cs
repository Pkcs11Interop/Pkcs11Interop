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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_TLS_MAC mechanism
    /// </summary>
    public class CkTlsMacParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_TLS_MAC_PARAMS _lowLevelStruct = new CK_TLS_MAC_PARAMS();

        /// <summary>
        /// Initializes a new instance of the CkTlsMacParams class.
        /// </summary>
        /// <param name="prfHashMechanism">Hash mechanism used in the TLS12 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)</param>
        /// <param name="macLength">Length of the MAC tag required or offered</param>
        /// <param name="serverOrClient">Should be set to "1" for "server finished" label or to "2" for "client finished" label</param>
        public CkTlsMacParams(NativeULong prfHashMechanism, NativeULong macLength, NativeULong serverOrClient)
        {
            _lowLevelStruct.PrfHashMechanism = prfHashMechanism;
            _lowLevelStruct.MacLength = macLength;
            _lowLevelStruct.ServerOrClient = serverOrClient;
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
