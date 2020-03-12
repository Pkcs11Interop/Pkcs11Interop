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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// High level PKCS#11 wrapper
    /// </summary>
    public interface IPkcs11Library : IDisposable
    {
        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        Pkcs11InteropFactories Factories
        {
            get;
        }

        /// <summary>
        /// Gets general information about loaded PKCS#11 library
        /// </summary>
        /// <returns>General information about loaded PKCS#11 library</returns>
        ILibraryInfo GetInfo();

        /// <summary>
        /// Obtains a list of slots in the system
        /// </summary>
        /// <param name="slotsType">Type of slots to be obtained</param>
        /// <returns>List of available slots</returns>
        List<ISlot> GetSlotList(SlotsType slotsType);

        /// <summary>
        /// Waits for a slot event, such as token insertion or token removal, to occur
        /// </summary>
        /// <param name="waitType">Type of waiting for a slot event</param>
        /// <param name="eventOccured">Flag indicating whether event occured</param>
        /// <param name="slotId">PKCS#11 handle of slot that the event occurred in</param>
        void WaitForSlotEvent(WaitType waitType, out bool eventOccured, out ulong slotId);
    }
}
