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
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI41
{
    /// <summary>
    /// Logical reader that potentially contains a token extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockSlot : Net.Pkcs11Interop.HighLevelAPI41.Slot, IMockSlot
    {
        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private static Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(MockSlot));

        /// <summary>
        /// Initializes new instance of Slot class
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11">Low level PKCS#11 wrapper</param>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        internal MockSlot(Pkcs11Factories factories, LowLevelAPI41.MockPkcs11 pkcs11, ulong slotId)
            : base(factories, pkcs11, slotId)
        {
            _logger.Debug("MockSlot({0})::ctor", _slotId);
        }

        /// <summary>
        /// Ejects token from slot.
        /// </summary>
        public void EjectToken()
        {
            _logger.Debug("MockSlot({0})::EjectToken", _slotId);

            CKR rv = ((LowLevelAPI41.MockPkcs11)_p11).C_EjectToken(_slotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EjectToken", rv);
        }
    }
}
