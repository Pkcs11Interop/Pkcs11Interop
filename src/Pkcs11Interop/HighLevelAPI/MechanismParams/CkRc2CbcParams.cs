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
    /// Parameters for the CKM_RC2_CBC and CKM_RC2_CBC_PAD mechanisms
    /// </summary>
    public class CkRc2CbcParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkRc2CbcParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkRc2CbcParams _params40 = null;

        /// <summary>
        /// Platform specific CkRc2CbcParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkRc2CbcParams _params41 = null;

        /// <summary>
        /// Platform specific CkRc2CbcParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkRc2CbcParams _params80 = null;

        /// <summary>
        /// Platform specific CkRc2CbcParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkRc2CbcParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkRc2CbcParams class.
        /// </summary>
        /// <param name='effectiveBits'>The effective number of bits in the RC2 search space</param>
        /// <param name='iv'>The initialization vector (IV) for cipher block chaining mode</param>
        public CkRc2CbcParams(ulong effectiveBits, byte[] iv)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkRc2CbcParams(Convert.ToUInt32(effectiveBits), iv);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkRc2CbcParams(Convert.ToUInt32(effectiveBits), iv);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkRc2CbcParams(effectiveBits, iv);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkRc2CbcParams(effectiveBits, iv);
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
