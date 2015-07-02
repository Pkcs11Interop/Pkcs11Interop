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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RSA_PKCS_PSS mechanism
    /// </summary>
    public class CkRsaPkcsPssParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RSA_PKCS_PSS_PARAMS _lowLevelStruct = new CK_RSA_PKCS_PSS_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRsaPkcsPssParams class.
        /// </summary>
        /// <param name='hashAlg'>Hash algorithm used in the PSS encoding (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='len'>Length, in bytes, of the salt value used in the PSS encoding</param>
        public CkRsaPkcsPssParams(uint hashAlg, uint mgf, uint len)
        {
            _lowLevelStruct.HashAlg = hashAlg;
            _lowLevelStruct.Mgf = mgf;
            _lowLevelStruct.Len = len;
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
