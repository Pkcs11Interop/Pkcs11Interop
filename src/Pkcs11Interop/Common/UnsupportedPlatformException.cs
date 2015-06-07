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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Exception indicating that Pkcs11Interop is being used on an unsupported platform
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class UnsupportedPlatformException : Exception
    {
        /// <summary>
        /// Initializes new instance of UnsupportedPlatformException class
        /// </summary>
        /// <param name="message">Message that describes the error</param>
        public UnsupportedPlatformException(string message)
            : base(message)
        {

        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes new instance of UnsupportedPlatformException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected UnsupportedPlatformException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
#endif
    }
}
