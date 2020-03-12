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
    /// Exception indicating that unmanaged function has returned error
    /// </summary>
    [Serializable]
    public class UnmanagedException : Exception
    {
        /// <summary>
        /// Error code returned by the last unmanaged function.
        /// Errors are explained at https://docs.microsoft.com/en-us/windows/desktop/Debug/system-error-codes
        /// </summary>
        private int? _errorCode = null;

        /// <summary>
        /// Error code returned by the last unmanaged function.
        /// Errors are explained at https://docs.microsoft.com/en-us/windows/desktop/Debug/system-error-codes
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

        /// <summary>
        /// Initializes new instance of UnmanagedException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected UnmanagedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            bool errorCodeSet = info.GetBoolean("ErrorCodeSet");

            if (errorCodeSet)
                _errorCode = info.GetInt32("ErrorCode");
        }

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">SerializationInfo to populate with data</param>
        /// <param name="context">The destination for this serialization</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            bool errorCodeSet = (_errorCode != null);
            info.AddValue("ErrorCodeSet", errorCodeSet);

            if (errorCodeSet)
                info.AddValue("ErrorCode", _errorCode.Value);

            base.GetObjectData(info, context);
        }
    }
}
