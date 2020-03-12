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

namespace Net.Pkcs11Interop.Logging
{
    /// <summary>
    /// Message log levels
    /// </summary>
    public enum Pkcs11InteropLogLevel
    {
        /// <summary>
        /// Trace log level
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Debug log level
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Info log level
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warning log level
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Error log level
        /// </summary>
        Error = 4,

        /// <summary>
        /// Fatal log level
        /// </summary>
        Fatal = 5,

        /// <summary>
        /// None log level
        /// </summary>
        None = 6
    }
}
