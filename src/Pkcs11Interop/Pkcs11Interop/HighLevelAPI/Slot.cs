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

using System;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Logical reader that potentially contains a token
    /// </summary>
    public class Slot
    {
        /// <summary>
        /// Platform specific Slot
        /// </summary>
        private HighLevelAPI40.Slot _slot40 = null;

        /// <summary>
        /// Platform specific Slot. Use with caution!
        /// </summary>
        public HighLevelAPI40.Slot HLA40Slot
        {
            get
            {
                return _slot40;
            }
        }

        /// <summary>
        /// Platform specific Slot
        /// </summary>
        private HighLevelAPI41.Slot _slot41 = null;

        /// <summary>
        /// Platform specific Slot. Use with caution!
        /// </summary>
        public HighLevelAPI41.Slot HLA41Slot
        {
            get
            {
                return _slot41;
            }
        }

        /// <summary>
        /// Platform specific Slot
        /// </summary>
        private HighLevelAPI80.Slot _slot80 = null;

        /// <summary>
        /// Platform specific Slot. Use with caution!
        /// </summary>
        public HighLevelAPI80.Slot HLA80Slot
        {
            get
            {
                return _slot80;
            }
        }

        /// <summary>
        /// Platform specific Slot
        /// </summary>
        private HighLevelAPI81.Slot _slot81 = null;

        /// <summary>
        /// Platform specific Slot. Use with caution!
        /// </summary>
        public HighLevelAPI81.Slot HLA81Slot
        {
            get
            {
                return _slot81;
            }
        }

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _slot40.SlotId : _slot41.SlotId;
                else
                    return (Platform.StructPackingSize == 0) ? _slot80.SlotId : _slot81.SlotId;
            }
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI40.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot40 = slot;
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI41.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot41 = slot;
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI80.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot80 = slot;
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI81.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot81 = slot;
        }

        /// <summary>
        /// Obtains information about a particular slot in the system
        /// </summary>
        /// <returns>Slot information</returns>
        public SlotInfo GetSlotInfo()
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? new SlotInfo(_slot40.GetSlotInfo()) : new SlotInfo(_slot41.GetSlotInfo());
            else
                return (Platform.StructPackingSize == 0) ? new SlotInfo(_slot80.GetSlotInfo()) : new SlotInfo(_slot81.GetSlotInfo());
        }

        /// <summary>
        /// Obtains information about a particular token in the system.
        /// </summary>
        /// <returns>Token information</returns>
        public TokenInfo GetTokenInfo()
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? new TokenInfo(_slot40.GetTokenInfo()) : new TokenInfo(_slot41.GetTokenInfo());
            else
                return (Platform.StructPackingSize == 0) ? new TokenInfo(_slot80.GetTokenInfo()) : new TokenInfo(_slot81.GetTokenInfo());
        }

        /// <summary>
        /// Obtains a list of mechanism types supported by a token
        /// </summary>
        /// <returns>List of mechanism types supported by a token</returns>
        public List<CKM> GetMechanismList()
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _slot40.GetMechanismList() : _slot41.GetMechanismList();
            else
                return (Platform.StructPackingSize == 0) ? _slot80.GetMechanismList() : _slot81.GetMechanismList();
        }

        /// <summary>
        /// Obtains information about a particular mechanism possibly supported by a token
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <returns>Information about mechanism</returns>
        public MechanismInfo GetMechanismInfo(CKM mechanism)
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? new MechanismInfo(_slot40.GetMechanismInfo(mechanism)) : new MechanismInfo(_slot41.GetMechanismInfo(mechanism));
            else
                return (Platform.StructPackingSize == 0) ? new MechanismInfo(_slot80.GetMechanismInfo(mechanism)) : new MechanismInfo(_slot81.GetMechanismInfo(mechanism));
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(string soPin, string label)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _slot40.InitToken(soPin, label);
                else
                    _slot41.InitToken(soPin, label);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _slot80.InitToken(soPin, label);
                else
                    _slot81.InitToken(soPin, label);
            }
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(byte[] soPin, byte[] label)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _slot40.InitToken(soPin, label);
                else
                    _slot41.InitToken(soPin, label);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _slot80.InitToken(soPin, label);
                else
                    _slot81.InitToken(soPin, label);
            }
        }

        /// <summary>
        /// Opens a session between an application and a token in a particular slot
        /// </summary>
        /// <param name="readOnly">Flag indicating whether session should be read only</param>
        /// <returns>Session</returns>
        public Session OpenSession(bool readOnly)
        {
            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? new Session(_slot40.OpenSession(readOnly)) : new Session(_slot41.OpenSession(readOnly));
            else
                return (Platform.StructPackingSize == 0) ? new Session(_slot80.OpenSession(readOnly)) : new Session(_slot81.OpenSession(readOnly));
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        /// <param name="session">Session</param>
        public void CloseSession(Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            session.CloseSession();
        }

        /// <summary>
        /// Closes all sessions an application has with a token
        /// </summary>
        public void CloseAllSessions()
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _slot40.CloseAllSessions();
                else
                    _slot41.CloseAllSessions();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _slot80.CloseAllSessions();
                else
                    _slot81.CloseAllSessions();
            }
        }
    }
}
