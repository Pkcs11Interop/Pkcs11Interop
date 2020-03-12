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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Attribute of cryptoki object (CK_ATTRIBUTE alternative)
    /// </summary>
    public interface IObjectAttribute : IDisposable
    {
        /// <summary>
        /// Attribute type
        /// </summary>
        ulong Type
        {
            get;
        }

        /// <summary>
        /// Flag indicating whether attribute value cannot be read either because object is sensitive or unextractable or because specified attribute for the object is invalid.
        /// </summary>
        bool CannotBeRead
        {
            get;
        }

        /// <summary>
        /// Returns managed object corresponding to CK_ATTRIBUTE structure that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        object ToMarshalableStructure();

        /// <summary>
        /// Reads value of attribute and returns it as ulong
        /// </summary>
        /// <returns>Value of attribute</returns>
        ulong GetValueAsUlong();

        /// <summary>
        /// Reads value of attribute and returns it as bool
        /// </summary>
        /// <returns>Value of attribute</returns>
        bool GetValueAsBool();

        /// <summary>
        /// Reads value of attribute and returns it as string
        /// </summary>
        /// <returns>Value of attribute</returns>
        string GetValueAsString();

        /// <summary>
        /// Reads value of attribute and returns it as byte array
        /// </summary>
        /// <returns>Value of attribute</returns>
        byte[] GetValueAsByteArray();

        /// <summary>
        /// Reads value of attribute and returns it as DateTime
        /// </summary>
        /// <returns>Value of attribute</returns>
        DateTime? GetValueAsDateTime();

        /// <summary>
        /// Reads value of attribute and returns it as attribute array
        /// </summary>
        /// <returns>Value of attribute</returns>
        List<IObjectAttribute> GetValueAsObjectAttributeList();

        /// <summary>
        /// Reads value of attribute and returns it as list of ulongs
        /// </summary>
        /// <returns>Value of attribute</returns>
        List<ulong> GetValueAsULongList();

        /// <summary>
        /// Reads value of attribute and returns it as list of mechanisms
        /// </summary>
        /// <returns>Value of attribute</returns>
        List<CKM> GetValueAsCkmList();
    }
}
