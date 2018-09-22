/*
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

using System;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Logging
{
    /// <summary>
    /// Logger that logs everything to console
    /// </summary>
    public class ConsolePkcs11InteropLogger : IPkcs11InteropLogger
    {
        /// <summary>
        /// Static object for global console access locking
        /// </summary>
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Representation of null string
        /// </summary>
        private static readonly string _null = "<null>";

        /// <summary>
        /// Logger name
        /// </summary>
        private readonly string _loggerName = null;

        /// <summary>
        /// Initializes new instance of ConsolePkcs11InteropLogger class
        /// </summary>
        /// <param name="type">Type for which logger should be created</param>
        internal ConsolePkcs11InteropLogger(Type type)
        {
            _loggerName = type.FullName;
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
            DateTime dateTime = DateTime.UtcNow;
            string formattedMessage = (message == null) ? _null : string.Format(message, args);
            string formattedException = (exception == null) ? _null : exception.ToString();

            lock (_lockObject)
            {
                Console.WriteLine(string.Format("{0:O} - {1} - {2} - {3} - {4}", dateTime, level.ToString(), _loggerName, formattedMessage, formattedException));
            }
        }

        /// <summary>
        /// Checks whether messages with specified level will be logged
        /// </summary>
        /// <param name="level">Message log level</param>
        /// <returns>True if log level is enabled false otherwise</returns>
        public bool IsEnabled(Pkcs11InteropLogLevel level)
        {
            return true;
        }
    }
}
