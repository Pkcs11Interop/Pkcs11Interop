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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Exception indicating that unmanaged function has returned error
    /// </summary>
    public class UnmanagedException : Exception
    {
        /// <summary>
        /// Error code returned by the last unmanaged function
        /// </summary>
        private int? _errorCode = null;

        /// <summary>
        /// Error code returned by the last unmanaged function
        /// </summary>
        public int? ErrorCode
        {
            get
            {
                return _errorCode;
            }
        }

        /// <summary>
        /// Initializes new instance of UnmanagedException class
        /// </summary>
        /// <param name="message">Message that describes the error</param>
        public UnmanagedException(string message)
            : base(message)
        {
            _errorCode = null;
        }

        /// <summary>
        /// Initializes new instance of UnmanagedException class
        /// </summary>
        /// <param name="message">Message that describes the error</param>
        /// <param name="errorCode">Error code returned by the last unmanaged function</param>
        public UnmanagedException(string message, int errorCode)
            : base(message)
        {
            _errorCode = errorCode;
        }
    }
}
