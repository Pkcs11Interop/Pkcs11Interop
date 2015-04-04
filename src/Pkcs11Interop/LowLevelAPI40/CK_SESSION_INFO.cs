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

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Information about a session
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public struct CK_SESSION_INFO
    {
        /// <summary>
        /// ID of the slot that interfaces with the token
        /// </summary>
        public uint SlotId;

        /// <summary>
        /// The state of the session
        /// </summary>
        public uint State;

        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        public uint Flags;

        /// <summary>
        /// An error code defined by the cryptographic device. Used for errors not covered by Cryptoki.
        /// </summary>
        public uint DeviceError;
    }
}
