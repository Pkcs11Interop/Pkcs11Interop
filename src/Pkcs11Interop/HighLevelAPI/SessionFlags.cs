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
