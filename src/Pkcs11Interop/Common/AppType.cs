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
    /// Type of application that will be using PKCS#11 library
    /// </summary>
    public enum AppType
    {
        /// <summary>
        /// Recommended option: PKCS#11 library will be used from multi-threaded application and needs to perform locking with native OS threading model (CKF_OS_LOCKING_OK)
        /// </summary>
        MultiThreaded,

        /// <summary>
        /// PKCS#11 library will be used from single-threaded application and does not need to perform any kind of locking
        /// </summary>
        SingleThreaded
    }
}
