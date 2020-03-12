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
    /// Exception with the name of PKCS#11 attribute whose value could not be read or converted
    /// </summary>
    [Serializable]
    public class AttributeValueException : Exception
    {
        /// <summary>
        /// Attribute whose value could not be read or converted
        /// </summary>
        private CKA _attribute = CKA.CKA_VENDOR_DEFINED;

        /// <summary>
        /// Attribute whose value could not be read or converted
        /// </summary>
        public CKA Attribute
        {
            get
            {
                return _attribute;
            }
        }

        /// <summary>
        /// Initializes new instance of AttributeValueException class
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        public AttributeValueException(CKA attribute)
            : base(string.Format("Value of attribute {0} could not be read", attribute.ToString()))
        {
            _attribute = attribute;
        }

        /// <summary>
        /// Initializes a new instance of AttributeValueException class with a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public AttributeValueException(CKA attribute, Exception innerException)
            : base(string.Format("Value of attribute {0} could not be converted", attribute.ToString()), innerException)
        {
            _attribute = attribute;
        }

        /// <summary>
        /// Initializes new instance of AttributeValueException class
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        public AttributeValueException(uint attribute)
            : this((CKA)attribute)
        {

        }

        /// <summary>
        /// Initializes a new instance of AttributeValueException class with a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public AttributeValueException(uint attribute, Exception innerException)
            : this((CKA)attribute, innerException)
        {

        }

        /// <summary>
        /// Initializes new instance of AttributeValueException class
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        public AttributeValueException(ulong attribute)
            : this((CKA)Convert.ToUInt32(attribute))
        {

        }

        /// <summary>
        /// Initializes a new instance of AttributeValueException class with a reference to the inner exception that is the cause of this exception
        /// </summary>
        /// <param name="attribute">Attribute whose value could not be read or converted</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        public AttributeValueException(ulong attribute, Exception innerException)
            : this((CKA)Convert.ToUInt32(attribute), innerException)
        {

        }

        /// <summary>
        /// Initializes new instance of AttributeValueException class with serialized data
        /// </summary>
        /// <param name="info">SerializationInfo that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">StreamingContext that contains contextual information about the source or destination</param>
        protected AttributeValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            _attribute = (CKA)info.GetUInt32("Attribute");
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

            info.AddValue("Attribute", _attribute);

            base.GetObjectData(info, context);
        }
    }
}
