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

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI80
{
    /// <summary>
    /// Logical connection between an application and a token extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockSession : Net.Pkcs11Interop.HighLevelAPI80.Session, IMockSession
    {
        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(MockSession));

        /// <summary>
        /// Initializes new instance of Session class
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11Library">Low level PKCS#11 wrapper</param>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        internal MockSession(Pkcs11InteropFactories factories, LowLevelAPI80.MockPkcs11Library pkcs11Library, ulong sessionId)
            : base(factories, pkcs11Library, sessionId)
        {
            _logger.Debug("MockSession({0})::ctor", _sessionId);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public void InteractiveLogin()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("MockSession({0})::InteractiveLogin", _sessionId);

            CKR rv = ((LowLevelAPI80.MockPkcs11Library)_pkcs11Library).C_InteractiveLogin(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            _logger.Debug("MockSession({0})::Dispose", _sessionId);

            base.Dispose(disposing);
        }
    }
}
