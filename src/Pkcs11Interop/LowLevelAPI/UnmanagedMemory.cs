/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Utility class that helps to manage unmanaged memory
    /// </summary>
    public class UnmanagedMemory
    {
        /// <summary>
        /// Allocates unmanaged zero-filled memory
        /// </summary>
        /// <param name="size">Number of bytes required</param>
        /// <returns>Pointer to newly allocated unmanaged zero-filled memory</returns>
        public static IntPtr Allocate(int size)
        {
            if (size < 0)
                throw new ArgumentException("Value has to be positive number", "size");

            // Allocate memory and fill it with zeros
            // Note: new byte array is automaticaly filled with zeros
            IntPtr memory = Marshal.AllocHGlobal(size);
            Write(memory, new byte[size]);

            return memory;
        }

        /// <summary>
        /// Frees previously allocated unmanaged memory
        /// </summary>
        /// <param name="memory">Pointer to the previously allocated unmanaged memory</param>
        public static void Free(ref IntPtr memory)
        {
            if (memory != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(memory);
                memory = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Returns the unmanaged size of the structure in bytes
        /// </summary>
        /// <param name="structureType">Type of structure whose size should be determined</param>
        /// <returns>Unmanaged size of the structure in bytes</returns>
        public static int SizeOf(Type structureType)
        {
            if (structureType == null)
                throw new ArgumentNullException("structureType");

            return Marshal.SizeOf(structureType);
        }

        /// <summary>
        /// Copies content of byte array to unmanaged memory
        /// </summary>
        /// <param name="memory">Previously allocated unmanaged memory to copy to</param>
        /// <param name="content">Byte array to copy from</param>
        public static void Write(IntPtr memory, byte[] content)
        {
            if (memory == IntPtr.Zero)
                throw new ArgumentNullException("memory");

            if (content == null)
                throw new ArgumentNullException("content");

            Marshal.Copy(content, 0, memory, content.Length);
        }

        /// <summary>
        /// Copies content of structure to unmanaged memory
        /// </summary>
        /// <param name="memory">Previously allocated unmanaged memory to copy to</param>
        /// <param name="structure">Structure to copy from</param>
        public static void Write(IntPtr memory, object structure)
        {
            if (memory == IntPtr.Zero)
                throw new ArgumentNullException("memory");

            if (structure == null)
                throw new ArgumentNullException("structure");

            Marshal.StructureToPtr(structure, memory, false);
        }

        /// <summary>
        /// Creates copy of unmanaged memory contet
        /// </summary>
        /// <param name="memory">Memory that should be copied</param>
        /// <param name="size">Number of bytes that should be copied</param>
        /// <returns>Copy of unmanaged memory contet</returns>
        public static byte[] Read(IntPtr memory, int size)
        {
            if (memory == IntPtr.Zero)
                throw new ArgumentNullException("memory");

            if (size < 0)
                throw new ArgumentException("Value has to be positive number", "size");

            byte[] output = new byte[size];
            Marshal.Copy(memory, output, 0, size);
            return output;
        }

        /// <summary>
        /// Copies content of unmanaged memory to the newly allocated managed structure
        /// </summary>
        /// <param name="memory">Memory that should be copied</param>
        /// <param name="structureType">Type of structure that should be created</param>
        /// <returns>Structure of requested type</returns>
        public static object Read(IntPtr memory, Type structureType)
        {
            if (memory == IntPtr.Zero)
                throw new ArgumentNullException("memory");

            if (structureType == null)
                throw new ArgumentNullException("structureType");

            return Marshal.PtrToStructure(memory, structureType);
        }

        /// <summary>
        /// Copies content of unmanaged memory to the existing managed structure
        /// </summary>
        /// <param name="memory">Memory that should be copied</param>
        /// <param name="structure">Object to which data should be copied</param>
        public static void Read(IntPtr memory, object structure)
        {
            if (memory == IntPtr.Zero)
                throw new ArgumentNullException("memory");
            
            if (structure == null)
                throw new ArgumentNullException("structure");
            
            Marshal.PtrToStructure(memory, structure);
        }
    }
}
