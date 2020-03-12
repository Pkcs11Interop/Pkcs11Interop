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
    /// Exception indicating an attempt to load unmanaged PKCS#11 library designated for a different architecture
    /// </summary>
    [Serializable]
    public class LibraryArchitectureException : Exception
    {
        /// <summary>
        /// Initializes new instance of LibraryArchitectureException class
        /// </summary>
        public LibraryArchitectureException()
            : this(Platform.Uses64BitRuntime ? "Unable to load 32-bit unmanaged library into 64-bit runtime" : "Unable to load 64-bit unmanaged library into 32-bit runtime")
        {

        }

        /// <summary>
        /// Initializes a new instance of LibraryArchitectureException class with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LibraryArchitectureException(Exception innerException)
            : this(Platform.Uses64BitRuntime ? "Unable to load 32-bit unmanaged library into 64-bit runtime" : "Unable to load 64-bit unmanaged library into 32-bit runtime", innerException)
        {

        }

        /// <summary>
        /// Initializes new instance of LibraryArchitectureException class
        /// </summary>
        /// <param name="message">Message that describes the error</param>
        public LibraryArchitectureException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of LibraryArchitectureException class with a specified error message and a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public LibraryArchitectureException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Initializes new instance of LibraryArchitectureException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected LibraryArchitectureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
