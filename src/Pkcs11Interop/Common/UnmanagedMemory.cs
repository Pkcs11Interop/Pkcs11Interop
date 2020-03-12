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

using Net.Pkcs11Interop.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that helps to manage unmanaged memory
    /// </summary>
    public static class UnmanagedMemory
    {
        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private static Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(UnmanagedMemory));

        /// <summary>
        /// Lock object for list of all memory allocations
        /// </summary>
        private static object _allocationsLock = new object();

        /// <summary>
        /// List of all memory allocations performed by this class
        /// </summary>
        private static Dictionary<IntPtr, int> _allocations = new Dictionary<IntPtr, int>();

        /// <summary>
        /// Flag indicating whether all memory allocations should be logged
        /// </summary>
        private static bool _debugModeEnabled = false;

        /// <summary>
        /// Flag indicating whether all memory allocations should be logged
        /// </summary>
        public static bool DebugModeEnabled
        {
            get
            {
                return _debugModeEnabled;
            }
            set
            {
                _debugModeEnabled = value;
            }
        }

        /// <summary>
        /// Allocates unmanaged zero-filled memory
        /// </summary>
        /// <param name="size">Number of bytes required</param>
        /// <returns>Pointer to newly allocated unmanaged zero-filled memory</returns>
        public static IntPtr Allocate(int size)
        {
            if (size < 0)
                throw new ArgumentException("Value has to be positive number", "size");

            IntPtr memory = IntPtr.Zero;

            // Allocate memory and fill it with zeros
            // Note: new byte array is automaticaly filled with zeros
            memory = Marshal.AllocHGlobal(size);
            Write(memory, new byte[size]);

            if (_debugModeEnabled)
            {
                lock (_allocationsLock)
                {
                    if (!_allocations.ContainsKey(memory))
                    {
                        _allocations.Add(memory, size);
                        
                        _logger.Debug("Allocated {0} bytes at {1}. Allocations: {2}", size, memory, _allocations.Count);
                    }
                    else
                    {
                        throw new Exception(string.Format("Already allocated {0} bytes at {1}", _allocations[memory], memory));
                    }
                }
            }

            return memory;
        }

        /// <summary>
        /// Frees previously allocated unmanaged memory
        /// </summary>
        /// <param name="memory">Pointer to the previously allocated unmanaged memory</param>
        public static void Free(ref IntPtr memory)
        {
            if (memory == IntPtr.Zero)
                return;

            if (_debugModeEnabled)
            {
                lock (_allocations)
                {
                    if (_allocations.ContainsKey(memory))
                    {
                        int size = _allocations[memory];
                        _allocations.Remove(memory);

                        _logger.Debug("Freeing {0} bytes at {1}. Allocations: {2}", size, memory, _allocations.Count);
                    }
                    else
                    {
                        throw new Exception(string.Format("Unable to free previously unallocated memory at {0}", memory));
                    }
                }
            }

            Marshal.FreeHGlobal(memory);
            memory = IntPtr.Zero;
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
