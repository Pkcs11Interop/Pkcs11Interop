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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Logical reader that potentially contains a token
    /// </summary>
    public interface ISlot
    {
        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        Pkcs11InteropFactories Factories
        {
            get;
        }

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        ulong SlotId
        {
            get;
        }

        /// <summary>
        /// Obtains information about a particular slot in the system
        /// </summary>
        /// <returns>Slot information</returns>
        ISlotInfo GetSlotInfo();

        /// <summary>
        /// Obtains information about a particular token in the system.
        /// </summary>
        /// <returns>Token information</returns>
        ITokenInfo GetTokenInfo();

        /// <summary>
        /// Obtains a list of mechanism types supported by a token
        /// </summary>
        /// <returns>List of mechanism types supported by a token</returns>
        List<CKM> GetMechanismList();

        /// <summary>
        /// Obtains information about a particular mechanism possibly supported by a token
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <returns>Information about mechanism</returns>
        IMechanismInfo GetMechanismInfo(CKM mechanism);

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        void InitToken(string soPin, string label);

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        void InitToken(byte[] soPin, byte[] label);

        /// <summary>
        /// Opens a session between an application and a token in a particular slot
        /// </summary>
        /// <param name="sessionType">Type of session to be opened</param>
        /// <returns>Session</returns>
        ISession OpenSession(SessionType sessionType);

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        /// <param name="session">Session</param>
        void CloseSession(ISession session);

        /// <summary>
        /// Closes all sessions an application has with a token
        /// </summary>
        void CloseAllSessions();
    }
}
