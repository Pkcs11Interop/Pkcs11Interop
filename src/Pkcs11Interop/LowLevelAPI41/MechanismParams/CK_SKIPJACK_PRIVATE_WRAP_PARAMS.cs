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
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
{
    /// <summary>
    /// Structure that provides the parameters to the CKM_SKIPJACK_PRIVATE_WRAP mechanism
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_SKIPJACK_PRIVATE_WRAP_PARAMS
    {
        /// <summary>
        /// Length of the password
        /// </summary>
        public uint PasswordLen;
        
        /// <summary>
        /// Pointer to the buffer which contains the user-supplied password
        /// </summary>
        public IntPtr Password;

        /// <summary>
        /// Other party's key exchange public key size
        /// </summary>
        public uint PublicDataLen;

        /// <summary>
        /// Pointer to other party's key exchange public key value
        /// </summary>
        public IntPtr PublicData;
        
        /// <summary>
        /// Length of prime and base values
        /// </summary>
        public uint PAndGLen;

        /// <summary>
        /// Length of subprime value
        /// </summary>
        public uint QLen;

        /// <summary>
        /// Size of random Ra, in bytes
        /// </summary>
        public uint RandomLen;

        /// <summary>
        /// Pointer to Ra data
        /// </summary>
        public IntPtr RandomA;

        /// <summary>
        /// Pointer to Prime, p, value
        /// </summary>
        public IntPtr PrimeP;

        /// <summary>
        /// Pointer to Base, g, value
        /// </summary>
        public IntPtr BaseG;

        /// <summary>
        /// Pointer to Subprime, q, value
        /// </summary>
        public IntPtr SubprimeQ;
    }
}
