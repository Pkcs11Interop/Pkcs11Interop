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
    /// Exception with the name of PKCS#11 method that failed and its return value
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Pkcs11Exception : Exception
    {
        /// <summary>
        /// Name of method that caused exception
        /// </summary>
        private string _method = null;

        /// <summary>
        /// Name of method that caused exception
        /// </summary>
        public string Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Return value of method that caused exception
        /// </summary>
        private CKR _rv = CKR.CKR_OK;

        /// <summary>
        /// Return value of method that caused exception
        /// </summary>
        public CKR RV
        {
            get
            {
                return _rv;
            }
        }

        /// <summary>
        /// Initializes new instance of Pkcs11Exception class
        /// </summary>
        /// <param name="method">Name of method that caused exception</param>
        /// <param name="rv">Return value of method that caused exception</param>
        public Pkcs11Exception(string method, CKR rv)
            : base(string.Format("Method {0} returned {1}", method, rv.ToString()))
        {
            _method = method;
            _rv = rv;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes new instance of Pkcs11Exception class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected Pkcs11Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                _method = info.GetString("Method");
                _rv = (CKR)info.GetUInt32("RV");
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
                info.AddValue("Method", _method);
                info.AddValue("RV", _rv);
            }

            base.GetObjectData(info, context);
        }
#endif
    }
}
