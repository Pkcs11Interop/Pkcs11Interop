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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Source of PKCS#11 function pointers
    /// </summary>
    public enum InitType
    {
        /// <summary>
        /// Recommended option: PKCS#11 function pointers will be acquired with single call of C_GetFunctionList function
        /// </summary>
        WithFunctionList,

        /// <summary>
        /// PKCS#11 function pointers will be acquired with multiple calls of GetProcAddress or dlsym function
        /// </summary>
        WithoutFunctionList
    }
}
