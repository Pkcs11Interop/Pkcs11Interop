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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
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
        public CkRc2MacGeneralParams(uint effectiveBits, uint macLength)
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
