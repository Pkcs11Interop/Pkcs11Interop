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
using Net.Pkcs11Interop.LowLevelAPI41.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RC2_CBC and CKM_RC2_CBC_PAD mechanisms
    /// </summary>
    public class CkRc2CbcParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_RC2_CBC_PARAMS _lowLevelStruct = new CK_RC2_CBC_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkRc2CbcParams class.
        /// </summary>
        /// <param name='effectiveBits'>The effective number of bits in the RC2 search space</param>
        /// <param name='iv'>The initialization vector (IV) for cipher block chaining mode</param>
        public CkRc2CbcParams(uint effectiveBits, byte[] iv)
        {
            _lowLevelStruct.EffectiveBits = 0;
            _lowLevelStruct.Iv = new byte[8];

            _lowLevelStruct.EffectiveBits = effectiveBits;
            
            if (iv == null)
                throw new ArgumentNullException("iv");
            
            if (iv.Length != 8)
                throw new ArgumentOutOfRangeException("iv", "Array has to be 8 bytes long");
            
            Array.Copy(iv, _lowLevelStruct.Iv, iv.Length);
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
