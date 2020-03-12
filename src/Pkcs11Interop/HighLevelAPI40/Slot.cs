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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.LowLevelAPI40;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Logical reader that potentially contains a token
    /// </summary>
    public class Slot : ISlot
    {
        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(Slot));

        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        protected Pkcs11InteropFactories _factories = null;

        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        public Pkcs11InteropFactories Factories
        {
            get
            {
                return _factories;
            }
        }

        /// <summary>
        /// Low level PKCS#11 wrapper
        /// </summary>
        protected LowLevelAPI40.Pkcs11Library _pkcs11Library = null;

        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        protected NativeULong _slotId = 0;
        
        /// <summary>
        /// PKCS#11 handle of slot
        /// </summary>
        public ulong SlotId
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_slotId);
            }
        }

        /// <summary>
        /// Initializes new instance of Slot class
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11Library">Low level PKCS#11 wrapper</param>
        /// <param name="slotId">PKCS#11 handle of slot</param>
        protected internal Slot(Pkcs11InteropFactories factories, LowLevelAPI40.Pkcs11Library pkcs11Library, ulong slotId)
        {
            _logger.Debug("Slot({0})::ctor", slotId);

            if (factories == null)
                throw new ArgumentNullException("factories");

            if (pkcs11Library == null)
                throw new ArgumentNullException("pkcs11Library");

            _factories = factories;
            _pkcs11Library = pkcs11Library;
            _slotId = ConvertUtils.UInt32FromUInt64(slotId);
         }

        /// <summary>
        /// Obtains information about a particular slot in the system
        /// </summary>
        /// <returns>Slot information</returns>
        public ISlotInfo GetSlotInfo()
        {
            _logger.Debug("Slot({0})::GetSlotInfo", _slotId);

            CK_SLOT_INFO slotInfo = new CK_SLOT_INFO();
            CKR rv = _pkcs11Library.C_GetSlotInfo(_slotId, ref slotInfo);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotInfo", rv);

            return new SlotInfo(_slotId, slotInfo);
        }

        /// <summary>
        /// Obtains information about a particular token in the system.
        /// </summary>
        /// <returns>Token information</returns>
        public ITokenInfo GetTokenInfo()
        {
            _logger.Debug("Slot({0})::GetTokenInfo", _slotId);

            CK_TOKEN_INFO tokenInfo = new CK_TOKEN_INFO();
            CKR rv = _pkcs11Library.C_GetTokenInfo(_slotId, ref tokenInfo);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetTokenInfo", rv);

            return new TokenInfo(_slotId, tokenInfo);
        }

        /// <summary>
        /// Obtains a list of mechanism types supported by a token
        /// </summary>
        /// <returns>List of mechanism types supported by a token</returns>
        public List<CKM> GetMechanismList()
        {
            _logger.Debug("Slot({0})::GetMechanismList", _slotId);

            NativeULong mechanismCount = 0;
            CKR rv = _pkcs11Library.C_GetMechanismList(_slotId, null, ref mechanismCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetMechanismList", rv);

            if (mechanismCount < 1)
                return new List<CKM>();

            CKM[] mechanismList = new CKM[mechanismCount];
            rv = _pkcs11Library.C_GetMechanismList(_slotId, mechanismList, ref mechanismCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetMechanismList", rv);

            if (mechanismList.Length != ConvertUtils.UInt32ToInt32(mechanismCount))
                Array.Resize(ref mechanismList, ConvertUtils.UInt32ToInt32(mechanismCount));

            return new List<CKM>(mechanismList);
        }

        /// <summary>
        /// Obtains information about a particular mechanism possibly supported by a token
        /// </summary>
        /// <param name="mechanism">Mechanism</param>
        /// <returns>Information about mechanism</returns>
        public IMechanismInfo GetMechanismInfo(CKM mechanism)
        {
            _logger.Debug("Slot({0})::GetMechanismInfo", _slotId);

            CK_MECHANISM_INFO mechanismInfo = new CK_MECHANISM_INFO();
            CKR rv = _pkcs11Library.C_GetMechanismInfo(_slotId, mechanism, ref mechanismInfo);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetMechanismInfo", rv);
            
            return new MechanismInfo(mechanism, mechanismInfo);
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(string soPin, string label)
        {
            _logger.Debug("Slot({0})::InitToken1", _slotId);

            byte[] soPinValue = null;
            NativeULong soPinValueLen = 0;
            if (soPin != null)
            {
                soPinValue = ConvertUtils.Utf8StringToBytes(soPin);
                soPinValueLen = ConvertUtils.UInt32FromInt32(soPinValue.Length);
            }

            byte[] tokenLabel = ConvertUtils.Utf8StringToBytes(label, 32, 0x20);

            CKR rv = _pkcs11Library.C_InitToken(_slotId, soPinValue, soPinValueLen, tokenLabel);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InitToken", rv);
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="soPin">SO's initial PIN</param>
        /// <param name="label">Label of the token</param>
        public void InitToken(byte[] soPin, byte[] label)
        {
            _logger.Debug("Slot({0})::InitToken2", _slotId);

            byte[] soPinValue = null;
            NativeULong soPinValueLen = 0;
            if (soPin != null)
            {
                soPinValue = soPin;
                soPinValueLen = ConvertUtils.UInt32FromInt32(soPin.Length);
            }

            // PKCS#11 v2.20 page 113:
            // pLabel points to the 32-byte label of the token (which must be padded with
            // blank characters, and which must not be null-terminated).
            byte[] tokenLabel = new byte[32];
            for (int i = 0; i < tokenLabel.Length; i++)
                tokenLabel[i] = 0x20;
            
            if (label != null)
            {
                if (label.Length > 32)
                    throw new Exception("Label too long");
                Array.Copy(label, 0, tokenLabel, 0, label.Length);
            }
            
            CKR rv = _pkcs11Library.C_InitToken(_slotId, soPinValue, soPinValueLen, tokenLabel);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InitToken", rv);
        }

        /// <summary>
        /// Opens a session between an application and a token in a particular slot
        /// </summary>
        /// <param name="sessionType">Type of session to be opened</param>
        /// <returns>Session</returns>
        public ISession OpenSession(SessionType sessionType)
        {
            _logger.Debug("Slot({0})::OpenSession", _slotId);

            NativeULong flags = CKF.CKF_SERIAL_SESSION;
            if (sessionType == SessionType.ReadWrite)
                flags = flags | CKF.CKF_RW_SESSION;

            NativeULong sessionId = CK.CK_INVALID_HANDLE;
            CKR rv = _pkcs11Library.C_OpenSession(_slotId, flags, IntPtr.Zero, IntPtr.Zero, ref sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_OpenSession", rv);

            if (_logger.IsEnabled(Pkcs11InteropLogLevel.Info))
                _logger.Info("Opened {0} session {1} with token in slot {2}", Pkcs11InteropLogUtils.ToString(sessionType), sessionId, _slotId);

            return _factories.SessionFactory.Create(_factories, _pkcs11Library, sessionId);
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        /// <param name="session">Session</param>
        public void CloseSession(ISession session)
        {
            _logger.Debug("Slot({0})::CloseSession", _slotId);

            if (session == null)
                throw new ArgumentNullException("session");

            session.CloseSession();
        }

        /// <summary>
        /// Closes all sessions an application has with a token
        /// </summary>
        public void CloseAllSessions()
        {
            _logger.Debug("Slot({0})::CloseAllSessions", _slotId);

            _logger.Info("Closing all sessions with token in slot {0}", _slotId);

            CKR rv = _pkcs11Library.C_CloseAllSessions(_slotId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CloseAllSessions", rv);
        }
    }
}
