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

using System;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// General purpose exception for Pkcs11Interop project
    /// </summary>
    public class Pkcs11InteropException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the Pkcs11InteropException class
        /// </summary>
        public Pkcs11InteropException()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Pkcs11InteropException class with a specified error message
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public Pkcs11InteropException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the Pkcs11InteropException class with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public Pkcs11InteropException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
