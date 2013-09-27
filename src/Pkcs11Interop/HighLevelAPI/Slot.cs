/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
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
        private HighLevelAPI4.Slot _slot4 = null;

        /// <summary>
        /// Platform specific Slot
        /// </summary>
        private HighLevelAPI8.Slot _slot8 = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _slot4.SlotId : _slot8.SlotId;
            }
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI4.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot4 = slot;
        }

        /// <summary>
        /// Converts platform specific Slot to platfrom neutral Slot
        /// </summary>
        /// <param name="slot">Platform specific Slot</param>
        internal Slot(HighLevelAPI8.Slot slot)
        {
            if (slot == null)
                throw new ArgumentNullException("slot");

            _slot8 = slot;
        }

        /// <summary>
        /// Obtains information about a particular slot in the system
        /// </summary>
        /// <returns>Slot information</returns>
        public SlotInfo GetSlotInfo()
        {
            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.SlotInfo hlaSlotInfo = _slot4.GetSlotInfo();
                return new SlotInfo(hlaSlotInfo);
            }
            else
            {
                HighLevelAPI8.SlotInfo hlaSlotInfo = _slot8.GetSlotInfo();
                return new SlotInfo(hlaSlotInfo);
            }
        }

        /// <summary>
        /// Obtains information about a particular token in the system.
        /// </summary>
        /// <returns>Token information</returns>
        public TokenInfo GetTokenInfo()
        {
            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.TokenInfo hlaTokenInfo = _slot4.GetTokenInfo();
                return new TokenInfo(hlaTokenInfo);
            }
            else
            {
                HighLevelAPI8.TokenInfo hlaTokenInfo = _slot8.GetTokenInfo();
                return new TokenInfo(hlaTokenInfo);
            }
        }

        /// <summary>
        /// Obtains a list of mechanism types supported by a token
        /// </summary>
        /// <returns>List of mechanism types supported by a token</returns>
        public List<CKM> GetMechanismList()
        {
            if (UnmanagedLong.Size == 4)
                return _slot4.GetMechanismList();
            else
                return _slot8.GetMechanismList();
        }

        /// <summary>
        /// Obtains information about a particular mechanism possibly supported by a token
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <returns>Information about mechanism</returns>
        public MechanismInfo GetMechanismInfo(CKM mechanism)
        {
            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.MechanismInfo hlaMechanismInfo = _slot4.GetMechanismInfo(mechanism);
                return new MechanismInfo(hlaMechanismInfo);
            }
            else
            {
                HighLevelAPI8.MechanismInfo hlaMechanismInfo = _slot8.GetMechanismInfo(mechanism);
                return new MechanismInfo(hlaMechanismInfo);
            }
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(string soPin, string label)
        {
            if (UnmanagedLong.Size == 4)
                _slot4.InitToken(soPin, label);
            else
                _slot8.InitToken(soPin, label);
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(byte[] soPin, byte[] label)
        {
            if (UnmanagedLong.Size == 4)
                _slot4.InitToken(soPin, label);
            else
                _slot8.InitToken(soPin, label);
        }

        /// <summary>
        /// Opens a session between an application and a token in a particular slot
        /// </summary>
        /// <param name="readOnly">Flag indicating whether session should be read only</param>
        /// <returns>Session</returns>
        public Session OpenSession(bool readOnly)
        {
            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.Session hlaSession = _slot4.OpenSession(readOnly);
                return new Session(hlaSession);
            }
            else
            {
                HighLevelAPI8.Session hlaSession = _slot8.OpenSession(readOnly);
                return new Session(hlaSession);
            }
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
            if (UnmanagedLong.Size == 4)
                _slot4.CloseAllSessions();
            else
                _slot8.CloseAllSessions();
        }
    }
}
