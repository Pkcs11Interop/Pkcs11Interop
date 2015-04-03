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

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Provides information about a particular mechanism
    /// </summary>
    public class MechanismInfo
    {
        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI4.MechanismInfo _mechanismInfo4 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI8.MechanismInfo _mechanismInfo8 = null;

        /// <summary>
        /// Mechanism
        /// </summary>
        public CKM Mechanism
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _mechanismInfo4.Mechanism : _mechanismInfo8.Mechanism;
            }
        }

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _mechanismInfo4.MinKeySize : _mechanismInfo8.MinKeySize;
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _mechanismInfo4.MaxKeySize : _mechanismInfo8.MaxKeySize;
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
                if (_mechanismFlags == null)
                    _mechanismFlags = (Platform.UnmanagedLongSize == 4) ? new MechanismFlags(_mechanismInfo4.MechanismFlags) : new MechanismFlags(_mechanismInfo8.MechanismFlags);

                return _mechanismFlags;
            }
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI4.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo4 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI8.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo8 = mechanismInfo;
        }
    }
}
