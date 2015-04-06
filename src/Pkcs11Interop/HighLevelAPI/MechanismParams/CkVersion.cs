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
        private HighLevelAPI40.MechanismParams.CkVersion _params40 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkVersion _params41 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkVersion _params80 = null;

        /// <summary>
        /// Platform specific CkVersion
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkVersion _params81 = null;

        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        public byte Major
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _params40.Major : _params41.Major;
                else
                    return (Platform.StructPackingSize == 0) ? _params80.Major : _params81.Major;
            }
        }

        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        public byte Minor
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _params40.Minor : _params41.Minor;
                else
                    return (Platform.StructPackingSize == 0) ? _params80.Minor : _params81.Minor;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        public CkVersion(byte major, byte minor)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkVersion(major, minor);
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkVersion(major, minor);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkVersion(major, minor);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkVersion(major, minor);
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI40.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params40 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI41.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params41 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI80.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params80 = ckVersion;
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='ckVersion'>Platform specific CkVersion</param>
        internal CkVersion(HighLevelAPI81.MechanismParams.CkVersion ckVersion)
        {
            if (ckVersion == null)
                throw new ArgumentNullException("ckVersion");

            _params81 = ckVersion;
        }
        
        #region IMechanismParams

        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _params40.ToMarshalableStructure() : _params41.ToMarshalableStructure();
            else
                return (Platform.StructPackingSize == 0) ? _params80.ToMarshalableStructure() : _params81.ToMarshalableStructure();
        }
        
        #endregion

        /// <summary>
        /// Returns a string that represents the current CkVersion object.
        /// </summary>
        /// <returns>String that represents the current CkVersion object.</returns>
        public override string ToString()
        {
            string version = null;

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    version = ((LowLevelAPI40.CK_VERSION)_params40.ToMarshalableStructure()).ToString();
                else
                    version = ((LowLevelAPI41.CK_VERSION)_params41.ToMarshalableStructure()).ToString();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    version = ((LowLevelAPI80.CK_VERSION)_params80.ToMarshalableStructure()).ToString();
                else
                    version = ((LowLevelAPI81.CK_VERSION)_params81.ToMarshalableStructure()).ToString();
            }
            
            return version;
        }
    }
}
