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
    /// Logger that does not log anything
    /// </summary>
    public class NullPkcs11InteropLogger : IPkcs11InteropLogger
    {
        /// <summary>
        /// Initializes new instance of NullPkcs11InteropLogger class
        /// </summary>
        internal NullPkcs11InteropLogger()
        {

        }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="level">Message log level</param>
        /// <param name="exception">Optional exception to be logged</param>
        /// <param name="message">Message to be logged</param>
        /// <param name="args">Message format arguments</param>
        public void Log(Pkcs11InteropLogLevel level, Exception exception, string message, params object[] args)
        {

        }

        /// <summary>
        /// Checks whether messages with specified level will be logged
        /// </summary>
        /// <param name="level">Message log level</param>
        /// <returns>True if log level is enabled false otherwise</returns>
        public bool IsEnabled(Pkcs11InteropLogLevel level)
        {
            return false;
        }
    }
}
