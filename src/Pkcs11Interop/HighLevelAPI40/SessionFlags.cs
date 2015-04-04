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

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Flags that define the type of session
    /// </summary>
    public class SessionFlags
    {
        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        private uint _flags;

        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        public uint Flags
        {
            get
            {
                return _flags;
            }
        }

        /// <summary>
        /// True if the session is read/write; false if the session is read-only
        /// </summary>
        public bool RwSession
        {
            get
            {
                return ((_flags & CKF.CKF_RW_SESSION) == CKF.CKF_RW_SESSION);
            }
        }

        /// <summary>
        /// This flag is provided for backward compatibility, and should always be set to true
        /// </summary>
        public bool SerialSession
        {
            get
            {
                return ((_flags & CKF.CKF_SERIAL_SESSION) == CKF.CKF_SERIAL_SESSION);
            }
        }

        /// <summary>
        /// Initializes new instance of SessionFlags class
        /// </summary>
        /// <param name="flags">Bit flags that define the type of session</param>
        internal SessionFlags(uint flags)
        {
            _flags = flags;
        }
    }
}
