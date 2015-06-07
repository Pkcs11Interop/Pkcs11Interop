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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Exception indicating that unmanaged function has returned error
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
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

#if !SILVERLIGHT
        /// <summary>
        /// Initializes new instance of UnmanagedException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected UnmanagedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                bool errorCodeSet = info.GetBoolean("ErrorCodeSet");
                if (errorCodeSet)
                    _errorCode = info.GetInt32("ErrorCode");
            }
        }

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">SerializationInfo to populate with data</param>
        /// <param name="context">The destination for this serialization</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                bool errorCodeSet = (_errorCode != null);
                info.AddValue("ErrorCodeSet", errorCodeSet);
                if (errorCodeSet)
                    info.AddValue("ErrorCode", _errorCode.Value);
            }

            base.GetObjectData(info, context);
        }
#endif
    }
}
