/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_EXTRACT_KEY_FROM_KEY mechanism
    /// </summary>
    public class CkExtractParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkExtractParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkExtractParams _params40 = null;

        /// <summary>
        /// Platform specific CkExtractParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkExtractParams _params41 = null;

        /// <summary>
        /// Platform specific CkExtractParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkExtractParams _params80 = null;

        /// <summary>
        /// Platform specific CkExtractParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkExtractParams _params81 = null;

        /// <summary>
        /// Initializes a new instance of the CkExtractParams class.
        /// </summary>
        /// <param name='bit'>Specifies which bit of the base key should be used as the first bit of the derived key</param>
        public CkExtractParams(ulong bit)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkExtractParams(Convert.ToUInt32(bit));
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkExtractParams(Convert.ToUInt32(bit));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkExtractParams(bit);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkExtractParams(bit);
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
