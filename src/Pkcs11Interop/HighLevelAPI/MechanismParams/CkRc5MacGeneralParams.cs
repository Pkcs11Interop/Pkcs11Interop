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
    /// Parameters for the CKM_RC5_MAC_GENERAL mechanism
    /// </summary>
    public class CkRc5MacGeneralParams : IMechanismParams
    {
        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI40.MechanismParams.CkRc5MacGeneralParams _params40 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI41.MechanismParams.CkRc5MacGeneralParams _params41 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI80.MechanismParams.CkRc5MacGeneralParams _params80 = null;

        /// <summary>
        /// Platform specific CkRc5MacGeneralParams
        /// </summary>
        private HighLevelAPI81.MechanismParams.CkRc5MacGeneralParams _params81 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkRc5MacGeneralParams class.
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        public CkRc5MacGeneralParams(ulong wordsize, ulong rounds, ulong macLength)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _params40 = new HighLevelAPI40.MechanismParams.CkRc5MacGeneralParams(Convert.ToUInt32(wordsize), Convert.ToUInt32(rounds), Convert.ToUInt32(macLength));
                else
                    _params41 = new HighLevelAPI41.MechanismParams.CkRc5MacGeneralParams(Convert.ToUInt32(wordsize), Convert.ToUInt32(rounds), Convert.ToUInt32(macLength));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _params80 = new HighLevelAPI80.MechanismParams.CkRc5MacGeneralParams(wordsize, rounds, macLength);
                else
                    _params81 = new HighLevelAPI81.MechanismParams.CkRc5MacGeneralParams(wordsize, rounds, macLength);
            }
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
    }
}
