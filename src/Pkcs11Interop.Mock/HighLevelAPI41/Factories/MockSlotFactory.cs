﻿/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI.Factories;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI41.Factories
{
    /// <summary>
    /// Factory for creation of ISlot instances
    /// </summary>
    public class MockSlotFactory : ISlotFactory
    {
        /// <summary>
        /// Creates slot with specified handle
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11Library">Low level PKCS#11 wrapper</param>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        public ISlot Create(Pkcs11InteropFactories factories, LowLevelPkcs11Library pkcs11Library, ulong slotId)
        {
            LowLevelAPI41.MockPkcs11Library p11 = pkcs11Library as LowLevelAPI41.MockPkcs11Library;
            if (p11 == null)
                throw new ArgumentException("Incorrect type of low level PKCS#11 wrapper");

            return new MockSlot(factories, p11, slotId);
        }
    }
}
