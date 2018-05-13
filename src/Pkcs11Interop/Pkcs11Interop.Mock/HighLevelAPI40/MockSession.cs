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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.Mock.HighLevelAPI40
{
    /// <summary>
    /// Logical connection between an application and a token extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockSession : Net.Pkcs11Interop.HighLevelAPI40.Session, IMockSession
    {
        /// <summary>
        /// Initializes new instance of Session class
        /// </summary>
        /// <param name="factories">Factories used by Pkcs11Interop library</param>
        /// <param name="pkcs11">Low level PKCS#11 wrapper</param>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        internal MockSession(Pkcs11Factories factories, LowLevelAPI40.MockPkcs11 pkcs11, ulong sessionId)
            : base(factories, pkcs11, sessionId)
        {

        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// </summary>
        /// <param name="session">Instance of the extended class</param>
        public void InteractiveLogin()
        {
            CKR rv = ((LowLevelAPI40.MockPkcs11)_p11).C_InteractiveLogin(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InteractiveLogin", rv);
        }
    }
}
