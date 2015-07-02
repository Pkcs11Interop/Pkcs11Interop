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
using Net.Pkcs11Interop.LowLevelAPI41;

namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism
    /// </summary>
    public class CkVersion : IMechanismParams
    {
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_VERSION _lowLevelStruct = new CK_VERSION();

        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        public byte Major
        {
            get
            {
                return _lowLevelStruct.Major[0];
            }
        }

        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        public byte Minor
        {
            get
            {
                return _lowLevelStruct.Minor[0];
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        public CkVersion(byte major, byte minor)
        {
            _lowLevelStruct.Major = new byte[] { major };
            _lowLevelStruct.Minor = new byte[] { minor };
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

        /// <summary>
        /// Returns a string that represents the current CkVersion object.
        /// </summary>
        /// <returns>String that represents the current CkVersion object.</returns>
        public override string ToString()
        {
            return _lowLevelStruct.ToString();
        }
    }
}
