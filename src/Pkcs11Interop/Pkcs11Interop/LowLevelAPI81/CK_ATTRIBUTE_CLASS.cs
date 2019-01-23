/*
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
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    /// <summary>
    /// Defines the type, value, and length of an attribute.
    /// This class can be used with Silverlight 5 version of Marshal.PtrToStructure(IntPtr, object) which does not support value types (structs).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CK_ATTRIBUTE_CLASS
    {
        /// <summary>
        /// The attribute type
        /// </summary>
        public ulong type;

        /// <summary>
        /// Pointer to the value of the attribute
        /// </summary>
        public IntPtr value;

        /// <summary>
        /// Length in bytes of the value
        /// </summary>
        public ulong valueLen;

        /// <summary>
        /// Copies instance members to CK_ATTRIBUTE struct
        /// </summary>
        /// <param name="ckAttribute">Destination CK_ATTRIBUTE struct</param>
        public void ToCkAttributeStruct(ref CK_ATTRIBUTE ckAttribute)
        {
            ckAttribute.type = type;
            ckAttribute.value = value;
            ckAttribute.valueLen = valueLen;
        }
    }
}
