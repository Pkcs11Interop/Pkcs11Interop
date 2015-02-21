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
    /// Exception that indicates error in PKCS#11 URI parsing or building process
    /// </summary>
    public class Pkcs11UriException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Net.Pkcs11Interop.URI.Pkcs11UriException class
        /// </summary>
        public Pkcs11UriException()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Net.Pkcs11Interop.URI.Pkcs11UriException class with a specified error message
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public Pkcs11UriException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Net.Pkcs11Interop.URI.Pkcs11UriException class with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public Pkcs11UriException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
