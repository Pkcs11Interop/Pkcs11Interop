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
    /// Parameters for the CKM_AES_CTR mechanism
    /// </summary>
    public class CkAesCtrParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkAesCtrParams _params4 = null;

        /// <summary>
        /// Platform specific CkAesCtrParams
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkAesCtrParams _params8 = null;

        /// <summary>
        /// Initializes a new instance of the CkAesCtrParams class.
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        public CkAesCtrParams(ulong counterBits, byte[] cb)
        {
            if (Platform.UnmanagedLongSize == 4)
                _params4 = new HighLevelAPI41.MechanismParams.CkAesCtrParams(Convert.ToUInt32(counterBits), cb);
            else
                _params8 = new HighLevelAPI8.MechanismParams.CkAesCtrParams(counterBits, cb);
        }
        
        #region IMechanismParams

        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (Platform.UnmanagedLongSize == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
        }
        
        #endregion
    }
}
