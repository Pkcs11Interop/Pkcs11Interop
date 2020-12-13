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
    /// Parameters for the CKM_TLS_MAC mechanism
    /// </summary>
    public class CkTlsMacParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkTlsMacParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkTlsMacParams _params40 = null;

        /// <summary>
        /// Platform specific CkTlsMacParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkTlsMacParams _params41 = null;

        /// <summary>
        /// Platform specific CkTlsMacParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkTlsMacParams _params80 = null;

        /// <summary>
        /// Platform specific CkTlsMacParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkTlsMacParams _params81 = null;

        /// <summary>
        /// Initializes a new instance of the CkTlsMacParams class.
        /// </summary>
        /// <param name="prfHashMechanism">Hash mechanism used in the TLS12 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)</param>
        /// <param name="macLength">Length of the MAC tag required or offered</param>
        /// <param name="serverOrClient">Should be set to "1" for "server finished" label or to "2" for "client finished" label</param>
        public CkTlsMacParams(ulong prfHashMechanism, ulong macLength, ulong serverOrClient)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkTlsMacParams(Convert.ToUInt32(prfHashMechanism), Convert.ToUInt32(macLength), Convert.ToUInt32(serverOrClient));
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkTlsMacParams(Convert.ToUInt32(prfHashMechanism), Convert.ToUInt32(macLength), Convert.ToUInt32(serverOrClient));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkTlsMacParams(prfHashMechanism, macLength, serverOrClient);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkTlsMacParams(prfHashMechanism, macLength, serverOrClient);
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
