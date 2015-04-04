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
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Defines the type, value, and length of an attribute.
    /// This class can be used with Silverlight 5 version of Marshal.PtrToStructure(IntPtr, object) which does not support value types (structs).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    public class CK_ATTRIBUTE_CLASS
    {
        /// <summary>
        /// The attribute type
        /// </summary>
        public uint type;

        /// <summary>
        /// Pointer to the value of the attribute
        /// </summary>
        public IntPtr value;

        /// <summary>
        /// Length in bytes of the value
        /// </summary>
        public uint valueLen;

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
