/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_EXTRACT_KEY_FROM_KEY mechanism
    /// </summary>
    public class CkExtractParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_EXTRACT_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_EXTRACT_PARAMS();

        /// <summary>
        /// Specifies which bit of the base key should be used as the first bit of the derived key
        /// </summary>
        public uint Bit
        {
            get
            {
                return _lowLevelStruct.Bit;
            }
            set
            {
                _lowLevelStruct.Bit = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkExtractParams class.
        /// </summary>
        public CkExtractParams()
        {
            _lowLevelStruct.Bit = 0;
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
