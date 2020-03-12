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
    /// Factory for creation of loggers
    /// </summary>
    public static class Pkcs11InteropLoggerFactory
    {
        /// <summary>
        /// Logger factory implementation
        /// </summary>
        private static IPkcs11InteropLoggerFactory _loggerFactory = new NullPkcs11InteropLoggerFactory();

        /// <summary>
        /// Sets logger factory implementation that will be used by Pkcs11Interop library
        /// </summary>
        /// <param name="loggerFactory"></param>
        public static void SetLoggerFactory(IPkcs11InteropLoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                _loggerFactory = new NullPkcs11InteropLoggerFactory();
            }
            else
            {
                _loggerFactory = loggerFactory;
            }
        }

        /// <summary>
        /// Creates logger for type
        /// </summary>
        /// <param name="type">Type for which logger should be created</param>
        /// <returns>Logger for specified type</returns>
        public static Pkcs11InteropLogger GetLogger(Type type)
        {
            IPkcs11InteropLogger logger = _loggerFactory.CreateLogger(type);
            return new Pkcs11InteropLogger(logger);
        }
    }
}
