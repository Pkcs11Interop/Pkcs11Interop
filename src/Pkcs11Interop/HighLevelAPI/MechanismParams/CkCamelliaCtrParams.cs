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
    /// Parameters for the CKM_CAMELLIA_CTR mechanism
    /// </summary>
    public class CkCamelliaCtrParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkCamelliaCtrParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkCamelliaCtrParams _params40 = null;

        /// <summary>
        /// Platform specific CkCamelliaCtrParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkCamelliaCtrParams _params41 = null;

        /// <summary>
        /// Platform specific CkCamelliaCtrParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkCamelliaCtrParams _params80 = null;

        /// <summary>
        /// Platform specific CkCamelliaCtrParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkCamelliaCtrParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkCamelliaCtrParams class.
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        public CkCamelliaCtrParams(ulong counterBits, byte[] cb)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkCamelliaCtrParams(Convert.ToUInt32(counterBits), cb);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkCamelliaCtrParams(Convert.ToUInt32(counterBits), cb);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkCamelliaCtrParams(counterBits, cb);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkCamelliaCtrParams(counterBits, cb);
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
