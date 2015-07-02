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
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_CAMELLIA_CTR mechanism
    /// </summary>
    public class CkCamelliaCtrParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_CAMELLIA_CTR_PARAMS _lowLevelStruct = new CK_CAMELLIA_CTR_PARAMS();
        
        /// <summary>
        /// Initializes a new instance of the CkCamelliaCtrParams class.
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        public CkCamelliaCtrParams(uint counterBits, byte[] cb)
        {
            _lowLevelStruct.CounterBits = 0;
            _lowLevelStruct.Cb = new byte[16];

            _lowLevelStruct.CounterBits = counterBits;

            if (cb == null)
                throw new ArgumentNullException("cb");
            
            if (cb.Length != 16)
                throw new ArgumentOutOfRangeException("cb", "Array has to be 16 bytes long");

            Array.Copy(cb, _lowLevelStruct.Cb, cb.Length);
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
