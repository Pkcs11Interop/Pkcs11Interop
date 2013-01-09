/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace Net.Pkcs11Interop.Common
{
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
