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

namespace Net.Pkcs11Interop.HighLevelAPI.Factories
{
    /// <summary>
    /// Pkcs11Interop uses this factory to create ISlot instances possibly extended with vendor specific methods.
    /// </summary>
    public class SlotFactory : ISlotFactory
    {
        /// <summary>
        /// Platform specific factory for creation of ISlot instances
        /// </summary>
        private ISlotFactory _factory = null;

        /// <summary>
        /// Initializes a new instance of the SlotFactory class
        /// </summary>
        public SlotFactory()
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI40.Factories.SlotFactory();
                else
                    _factory = new HighLevelAPI41.Factories.SlotFactory();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _factory = new HighLevelAPI80.Factories.SlotFactory();
                else
                    _factory = new HighLevelAPI81.Factories.SlotFactory();
            }
        }

        /// <summary>
        /// Creates slot with specified handle
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11">Low level PKCS#11 wrapper</param>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        public ISlot CreateSlot(Pkcs11Factories factories, LowLevelPkcs11 pkcs11, ulong slotId)
        {
            return _factory.CreateSlot(factories, pkcs11, slotId);
        }
    }
}
