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
    /// Factory for creation of simple trace/console/file loggers
    /// </summary>
    public class SimplePkcs11InteropLoggerFactory : IPkcs11InteropLoggerFactory
    {
        /// <summary>
        /// Flag indicating whether output via System.Diagnostics.Trace class is enabled
        /// </summary>
        private bool _diagnosticsTraceOutputEnabled = false;

        /// <summary>
        /// Enables output via System.Diagnostics.Trace class
        /// </summary>
        public void EnableDiagnosticsTraceOutput()
        {
            _diagnosticsTraceOutputEnabled = true;
        }

        /// <summary>
        /// Disables output via System.Diagnostics.Trace class
        /// </summary>
        public void DisableDiagnosticsTraceOutput()
        {
            _diagnosticsTraceOutputEnabled = false;
        }

        /// <summary>
        /// Flag indicating whether console output is enabled
        /// </summary>
        private bool _consoleOutputEnabled = false;

        /// <summary>
        /// Enables console output
        /// </summary>
        public void EnableConsoleOutput()
        {
            _consoleOutputEnabled = true;
        }

        /// <summary>
        /// Disables console output
        /// </summary>
        public void DisableConsoleOutput()
        {
            _consoleOutputEnabled = false;
        }

        /// <summary>
        /// Path to the log file - null value indicates disabled file output
        /// </summary>
        string _filePath = null;

        /// <summary>
        /// Enables output to file
        /// </summary>
        /// <param name="filePath">Path to the log file</param>
        public void EnableFileOutput(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            _filePath = filePath;
        }

        /// <summary>
        /// Disables output to file
        /// </summary>
        public void DisableFileOutput()
        {
            _filePath = null;
        }

        /// <summary>
        /// Minimal level of messages that should be logged
        /// </summary>
        private Pkcs11InteropLogLevel _minLogLevel = Pkcs11InteropLogLevel.Info;

        /// <summary>
        /// Minimal level of messages that should be logged
        /// </summary>
        public Pkcs11InteropLogLevel MinLogLevel
        {
            get
            {
                return _minLogLevel;
            }
            set
            {
                _minLogLevel = value;
            }
        }

        /// <summary>
        /// Creates logger for type
        /// </summary>
        /// <param name="type">Type for which logger should be created</param>
        /// <returns>Logger for specified type</returns>
        public IPkcs11InteropLogger CreateLogger(Type type)
        {
            return new SimplePkcs11InteropLogger(type, _minLogLevel, _diagnosticsTraceOutputEnabled, _consoleOutputEnabled, _filePath);
        }
    }
}
