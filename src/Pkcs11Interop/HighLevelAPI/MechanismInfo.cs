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

using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
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
        private uint _minKeySize = 0;

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public uint MinKeySize
        {
            get
            {
                return _minKeySize;
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        private uint _maxKeySize = 0;

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public uint MaxKeySize
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
        internal MechanismInfo(CKM mechanism, LowLevelAPI.CK_MECHANISM_INFO ck_mechanism_info)
        {
            _mechanism = mechanism;
            _minKeySize = ck_mechanism_info.MinKeySize;
            _maxKeySize = ck_mechanism_info.MaxKeySize;
            _mechanismFlags = new MechanismFlags(ck_mechanism_info.Flags);
        }
    }
}
