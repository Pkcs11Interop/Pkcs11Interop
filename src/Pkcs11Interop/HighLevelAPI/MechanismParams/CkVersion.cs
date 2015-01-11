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
    /// Parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism
    /// </summary>
    public class CkVersion : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkVersion _params4 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkVersion _params8 = null;

        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        public byte Major
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _params4.Major : _params8.Major;
            }
        }

        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        public byte Minor
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _params4.Minor : _params8.Minor;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        public CkVersion(byte major, byte minor)
        {
            if (UnmanagedLong.Size == 4)
                _params4 = new HighLevelAPI4.MechanismParams.CkVersion(major, minor);
            else
                _params8 = new HighLevelAPI8.MechanismParams.CkVersion(major, minor);
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI4.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params4 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI8.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params8 = ckVersion;
        }
        
        #region IMechanismParams

        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (UnmanagedLong.Size == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
        }
        
        #endregion

        /// <summary>
        /// Returns a string that represents the current CkVersion object.
        /// </summary>
        /// <returns>String that represents the current CkVersion object.</returns>
        public override string ToString()
        {
            string version = null;

            if (UnmanagedLong.Size == 4)
                version = ConvertUtils.CkVersionToString((LowLevelAPI4.CK_VERSION)_params4.ToMarshalableStructure());
            else
                version = ConvertUtils.CkVersionToString((LowLevelAPI8.CK_VERSION)_params8.ToMarshalableStructure());
            
            return version;
        }
    }
}
