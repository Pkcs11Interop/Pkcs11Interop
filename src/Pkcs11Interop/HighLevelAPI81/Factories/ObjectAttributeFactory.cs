﻿/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.Factories
{
    /// <summary>
    /// Factory for creation of IObjectAttribute instances
    /// </summary>
    public class ObjectAttributeFactory : IObjectAttributeFactory
    {
        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type)
        {
            return new ObjectAttribute(type);
        }

        /// <summary>
        /// Creates attribute of given type with no value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type)
        {
            return new ObjectAttribute(type);
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, ulong value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with ulong value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, ulong value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKC value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, CKC value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKK value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, CKK value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with CKO value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, CKO value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, bool value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with bool value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, bool value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, string value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with string value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, string value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, byte[] value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with byte array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, byte[] value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, DateTime value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with DateTime (CK_DATE) value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, DateTime value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, List<IObjectAttribute> value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with attribute array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, List<IObjectAttribute> value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, List<ulong> value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with ulong array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, List<ulong> value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(ulong type, List<CKM> value)
        {
            return new ObjectAttribute(type, value);
        }

        /// <summary>
        /// Creates attribute of given type with mechanism array value
        /// </summary>
        /// <param name="type">Attribute type</param>
        /// <param name="value">Attribute value</param>
        /// <returns>Attribute of cryptoki object</returns>
        public IObjectAttribute Create(CKA type, List<CKM> value)
        {
            return new ObjectAttribute(type, value);
        }
    }
}
