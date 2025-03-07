﻿/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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

using Net.Pkcs11Interop.HighLevelAPI;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI
{
    /// <summary>
    /// Logical connection between an application and a token extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public interface IMockSession : ISession
    {
        /// <summary>
        /// Logs a user into a token interactively.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        void InteractiveLogin();
    }
}
