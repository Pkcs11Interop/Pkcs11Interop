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

using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI40.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_RSA_PKCS_PSS mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_RSA_PKCS_PSS_PARAMS
    {
        /// <summary>
        /// Hash algorithm used in the PSS encoding (CKM)
        /// </summary>
        public uint HashAlg;
        
        /// <summary>
        /// Mask generation function to use on the encoded block (CKG)
        /// </summary>
        public uint Mgf;

        /// <summary>
        /// Length, in bytes, of the salt value used in the PSS encoding
        /// </summary>
        public uint Len;
    }
}
