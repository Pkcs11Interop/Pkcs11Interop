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
    /// Parameters for the CKM_AES_CTR mechanism
    /// </summary>
    public class CkAesCtrParams : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private LowLevelAPI.MechanismParams.CK_AES_CTR_PARAMS _lowLevelStruct = new LowLevelAPI.MechanismParams.CK_AES_CTR_PARAMS();

        /// <summary>
        /// The number of bits in the counter block (cb) that shall be incremented
        /// </summary>
        public uint CounterBits
        {
            get
            {
                return _lowLevelStruct.CounterBits;
            }
            set
            {
                _lowLevelStruct.CounterBits = value;
            }
        }

        /// <summary>
        /// Specifies the counter block (16 bytes)
        /// </summary>
        public byte[] Cb
        {
            get
            {
                return _lowLevelStruct.Cb;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Cb");
                
                if (value.Length != 16)
                    throw new ArgumentOutOfRangeException("Cb", "Array has to be 16 bytes long");
                
                _lowLevelStruct.Cb = value;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkAesCtrParams class.
        /// </summary>
        public CkAesCtrParams()
        {
            _lowLevelStruct.CounterBits = 0;
            _lowLevelStruct.Cb = new byte[16];
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
