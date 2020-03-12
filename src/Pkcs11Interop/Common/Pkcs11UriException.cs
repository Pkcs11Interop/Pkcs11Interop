/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using System.Runtime.Serialization;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Exception that indicates error in PKCS#11 URI parsing or building process
    /// </summary>
    [Serializable]
    public class Pkcs11UriException : Exception
    {
        /// <summary>
        /// Initializes a new instance of Pkcs11UriException class with a specified error message
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public Pkcs11UriException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of Pkcs11UriException class with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public Pkcs11UriException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes new instance of Pkcs11UriException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected Pkcs11UriException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
