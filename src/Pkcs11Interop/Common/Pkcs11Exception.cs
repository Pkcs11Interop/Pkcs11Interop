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
    /// Exception with the name of PKCS#11 method that failed and its return value
    /// </summary>
    [Serializable]
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

        /// <summary>
        /// Initializes new instance of Pkcs11Exception class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected Pkcs11Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            _method = info.GetString("Method");
            _rv = (CKR)info.GetUInt32("RV");
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

            info.AddValue("Method", _method);
            info.AddValue("RV", _rv);

            base.GetObjectData(info, context);
        }
    }
}
