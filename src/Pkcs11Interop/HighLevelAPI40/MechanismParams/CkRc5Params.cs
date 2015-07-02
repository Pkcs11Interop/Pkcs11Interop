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
    /// Parameters for the CKM_RC5_ECB and CKM_RC5_MAC mechanisms
    /// </summary>
    public class CkRc5Params : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RC5_PARAMS _lowLevelStruct = new CK_RC5_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRc5Params class.
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        public CkRc5Params(uint wordsize, uint rounds)
        {
            _lowLevelStruct.Wordsize = wordsize;
            _lowLevelStruct.Rounds = rounds;
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
