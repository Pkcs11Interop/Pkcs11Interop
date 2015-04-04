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
    /// Flags that define the type of session
    /// </summary>
    public class SessionFlags
    {
        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI40.SessionFlags _sessionFlags40 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI41.SessionFlags _sessionFlags41 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI80.SessionFlags _sessionFlags80 = null;

        /// <summary>
        /// Platform specific SessionFlags
        /// </summary>
        private HighLevelAPI81.SessionFlags _sessionFlags81 = null;

        /// <summary>
        /// Bit flags that define the type of session
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.Flags : _sessionFlags41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.Flags : _sessionFlags81.Flags;
            }
        }

        /// <summary>
        /// True if the session is read/write; false if the session is read-only
        /// </summary>
        public bool RwSession
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.RwSession : _sessionFlags41.RwSession;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.RwSession : _sessionFlags81.RwSession;
            }
        }

        /// <summary>
        /// This flag is provided for backward compatibility, and should always be set to true
        /// </summary>
        public bool SerialSession
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _sessionFlags40.SerialSession : _sessionFlags41.SerialSession;
                else
                    return (Platform.StructPackingSize == 0) ? _sessionFlags80.SerialSession : _sessionFlags81.SerialSession;
            }
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI40.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags40 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI41.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags41 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI80.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags80 = sessionFlags;
        }

        /// <summary>
        /// Converts platform specific SessionFlags to platfrom neutral SessionFlags
        /// </summary>
        /// <param name="sessionFlags">Platform specific SessionFlags</param>
        internal SessionFlags(HighLevelAPI81.SessionFlags sessionFlags)
        {
            if (sessionFlags == null)
                throw new ArgumentNullException("sessionFlags");

            _sessionFlags81 = sessionFlags;
        }
    }
}
