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
        private HighLevelAPI40.MechanismInfo _mechanismInfo40 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI41.MechanismInfo _mechanismInfo41 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI80.MechanismInfo _mechanismInfo80 = null;

        /// <summary>
        /// Platform specific MechanismInfo
        /// </summary>
        private HighLevelAPI81.MechanismInfo _mechanismInfo81 = null;

        /// <summary>
        /// Mechanism
        /// </summary>
        public CKM Mechanism
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.Mechanism : _mechanismInfo41.Mechanism;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.Mechanism : _mechanismInfo81.Mechanism;
            }
        }

        /// <summary>
        /// The minimum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MinKeySize
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.MinKeySize : _mechanismInfo41.MinKeySize;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.MinKeySize : _mechanismInfo81.MinKeySize;
            }
        }

        /// <summary>
        /// The maximum size of the key for the mechanism (whether this is measured in bits or in bytes is mechanism-dependent)
        /// </summary>
        public ulong MaxKeySize
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo40.MaxKeySize : _mechanismInfo41.MaxKeySize;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismInfo80.MaxKeySize : _mechanismInfo81.MaxKeySize;
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
                {
                    if (Platform.UnmanagedLongSize == 4)
                        _mechanismFlags = (Platform.StructPackingSize == 0) ? new MechanismFlags(_mechanismInfo40.MechanismFlags) : new MechanismFlags(_mechanismInfo41.MechanismFlags);
                    else
                        _mechanismFlags = (Platform.StructPackingSize == 0) ? new MechanismFlags(_mechanismInfo80.MechanismFlags) : new MechanismFlags(_mechanismInfo81.MechanismFlags);
                }

                return _mechanismFlags;
            }
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI40.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo40 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI41.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo41 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI80.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo80 = mechanismInfo;
        }

        /// <summary>
        /// Converts platform specific MechanismInfo to platfrom neutral MechanismInfo
        /// </summary>
        /// <param name="mechanismInfo">Platform specific MechanismInfo</param>
        internal MechanismInfo(HighLevelAPI81.MechanismInfo mechanismInfo)
        {
            if (mechanismInfo == null)
                throw new ArgumentNullException("mechanismInfo");

            _mechanismInfo81 = mechanismInfo;
        }
    }
}
