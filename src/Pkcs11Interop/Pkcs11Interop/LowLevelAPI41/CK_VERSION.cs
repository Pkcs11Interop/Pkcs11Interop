﻿/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

using System.Runtime.InteropServices;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.LowLevelAPI41
{
    /// <summary>
    /// Describes the version
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct CK_VERSION
    {
        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Major;
        
        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public byte[] Minor;

        /// <summary>
        /// Returns a string that represents the current CK_VERSION structure.
        /// </summary>
        /// <returns>String that represents the current CK_VERSION structure.</returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}", Major[0], Minor[0]);
        }
    }
}
