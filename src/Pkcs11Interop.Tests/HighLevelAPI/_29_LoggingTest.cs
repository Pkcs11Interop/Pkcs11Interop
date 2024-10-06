/*
 *  Copyright 2012-2024 The Pkcs11Interop Project
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

using System.IO;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Logging;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Pkcs11InteropLoggerFactory and logging tests.
    /// </summary>
    [TestFixture()]
    public class _29_LoggingTest
    {
        /// <summary>
        /// Basic logging test.
        /// </summary>
        [Test()]
        public void _01_BasicLoggingTest()
        {
            // Specify path to the log file
            string logFilePath = Path.Combine(Path.GetTempPath(), @"Pkcs11Interop.log");

            DeleteFile(logFilePath);

            // Setup logger factory implementation
            var loggerFactory = new SimplePkcs11InteropLoggerFactory();
            loggerFactory.MinLogLevel = Pkcs11InteropLogLevel.Trace;
            loggerFactory.DisableConsoleOutput();
            loggerFactory.DisableDiagnosticsTraceOutput();
            loggerFactory.EnableFileOutput(logFilePath);

            // Set logger factory implementation that will be used by Pkcs11Interop library
            Pkcs11InteropLoggerFactory.SetLoggerFactory(loggerFactory);

            // Use Pkcs11Interop library as usual
            using (IPkcs11Library pkcs11Library = Settings.Factories.Pkcs11LibraryFactory.LoadPkcs11Library(Settings.Factories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                ILibraryInfo libraryInfo = pkcs11Library.GetInfo();

                // Verify that log file was created and contains some logs
                Assert.IsTrue(File.Exists(logFilePath));
                Assert.IsTrue(File.ReadAllText(logFilePath).Contains("Loading PKCS#11 library"));
            }

            DeleteFile(logFilePath);
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
