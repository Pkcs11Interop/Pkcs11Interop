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
using Net.Pkcs11Interop.LowLevelAPI80;

namespace Net.Pkcs11Interop.HighLevelAPI80
{
    /// <summary>
    /// Provides information about a particular mechanism
    /// </summary>
    public class MechanismInfo
    {
        /// <summary>
        /// Mechanism
        /// </summary>
        private CKM _mechanism = 0;

        /// <summary>
        /// Mechanism
        /// </summary>
        public CKM Mechanism
        {
            get
            {
                return _mechanism;
            }
        }

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        private ulong _minKeySize = 0;

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize
        {
            get
            {
                return _minKeySize;
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        private ulong _maxKeySize = 0;

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize
        {
            get
            {
                return _maxKeySize;
            }
        }

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        private MechanismFlags _mechanismFlags = null;

        /// <summary>
        /// Flags specifying mechanism capabilities
        /// </summary>
        public MechanismFlags MechanismFlags
        {
            get
            {
                return _mechanismFlags;
            }
        }

        /// <summary>
        /// Converts low level CK_MECHANISM_INFO structure to high level MechanismInfo class
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <param name="ck_mechanism_info">Low level CK_MECHANISM_INFO structure</param>
        internal MechanismInfo(CKM mechanism, CK_MECHANISM_INFO ck_mechanism_info)
        {
            _mechanism = mechanism;
            _minKeySize = ck_mechanism_info.MinKeySize;
            _maxKeySize = ck_mechanism_info.MaxKeySize;
            _mechanismFlags = new MechanismFlags(ck_mechanism_info.Flags);
        }
    }
}
