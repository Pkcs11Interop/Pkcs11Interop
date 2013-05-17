/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_RC2_CBC and CKM_RC2_CBC_PAD mechanisms
    /// </summary>
    public class CkRc2CbcParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_RC2_CBC_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_RC2_CBC_PARAMS();
        
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
        /// Converts object to low level mechanism parameters
        /// </summary>
        /// <returns>Low level mechanism parameters</returns>
        public object ToLowLevelParams()
        {
            return _lowLevelStruct;
        }
        
        #endregion
    }
}
