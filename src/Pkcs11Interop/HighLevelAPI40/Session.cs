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
using System.IO;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.LowLevelAPI40;
using NativeLong = System.Int32;
using NativeULong = System.UInt32;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI40
{
    /// <summary>
    /// Class representing a logical connection between an application and a token
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        protected bool _disposed = false;

        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(Session));

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
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _factories;
            }
        }

        /// <summary>
        /// Low level PKCS#11 wrapper
        /// </summary>
        protected LowLevelAPI40.Pkcs11Library _pkcs11Library = null;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        protected NativeULong _sessionId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return ConvertUtils.UInt32ToUInt64(_sessionId);
            }
        }

        /// <summary>
        /// Flag indicating whether session should be closed when object is disposed
        /// </summary>
        protected bool _closeWhenDisposed = true;

        /// <summary>
        /// Flag indicating whether session should be closed when object is disposed
        /// </summary>
        public bool CloseWhenDisposed
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _closeWhenDisposed;
            }
            set
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                _logger.Debug("Session({0})::CloseWhenDisposed", _sessionId);

                _closeWhenDisposed = value;
            }
        }

        /// <summary>
        /// Initializes new instance of Session class
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="pkcs11Library">Low level PKCS#11 wrapper</param>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        protected internal Session(Pkcs11InteropFactories factories, LowLevelAPI40.Pkcs11Library pkcs11Library, ulong sessionId)
        {
            _logger.Debug("Session({0})::ctor", sessionId);

            if (factories == null)
                throw new ArgumentNullException("factories");

            if (pkcs11Library == null)
                throw new ArgumentNullException("pkcs11Library");

            if (sessionId == CK.CK_INVALID_HANDLE)
                throw new ArgumentException("Invalid handle specified", "sessionId");

            _factories = factories;
            _pkcs11Library = pkcs11Library;
            _sessionId = ConvertUtils.UInt32FromUInt64(sessionId);
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        public void CloseSession()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::CloseSession", _sessionId);

            _logger.Info("Closing session {0}", _sessionId);

            CKR rv = _pkcs11Library.C_CloseSession(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CloseSession", rv);

            _sessionId = CK.CK_INVALID_HANDLE;
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(string userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::InitPin1", _sessionId);

            byte[] pinValue = null;
            NativeULong pinValueLen = 0;
            if (userPin != null)
            {
                pinValue = ConvertUtils.Utf8StringToBytes(userPin);
                pinValueLen = ConvertUtils.UInt32FromInt32(pinValue.Length);
            }

            CKR rv = _pkcs11Library.C_InitPIN(_sessionId, pinValue, pinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InitPIN", rv);
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(byte[] userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::InitPin2", _sessionId);

            byte[] pinValue = null;
            NativeULong pinValueLen = 0;
            if (userPin != null)
            {
                pinValue = userPin;
                pinValueLen = ConvertUtils.UInt32FromInt32(userPin.Length);
            }
            
            CKR rv = _pkcs11Library.C_InitPIN(_sessionId, pinValue, pinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_InitPIN", rv);
        }

        /// <summary>
        /// Modifies the PIN of the user that is currently logged in, or the CKU_USER PIN if the session is not logged in.
        /// </summary>
        /// <param name="oldPin">Old PIN value</param>
        /// <param name="newPin">New PIN value</param>
        public void SetPin(string oldPin, string newPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SetPin1", _sessionId);

            byte[] oldPinValue = null;
            NativeULong oldPinValueLen = 0;
            if (oldPin != null)
            {
                oldPinValue = ConvertUtils.Utf8StringToBytes(oldPin);
                oldPinValueLen = ConvertUtils.UInt32FromInt32(oldPinValue.Length);
            }

            byte[] newPinValue = null;
            NativeULong newPinValueLen = 0;
            if (newPin != null)
            {
                newPinValue = ConvertUtils.Utf8StringToBytes(newPin);
                newPinValueLen = ConvertUtils.UInt32FromInt32(newPinValue.Length);
            }

            CKR rv = _pkcs11Library.C_SetPIN(_sessionId, oldPinValue, oldPinValueLen, newPinValue, newPinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetPIN", rv);
        }

        /// <summary>
        /// Modifies the PIN of the user that is currently logged in, or the CKU_USER PIN if the session is not logged in.
        /// </summary>
        /// <param name="oldPin">Old PIN value</param>
        /// <param name="newPin">New PIN value</param>
        public void SetPin(byte[] oldPin, byte[] newPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SetPin2", _sessionId);

            byte[] oldPinValue = null;
            NativeULong oldPinValueLen = 0;
            if (oldPin != null)
            {
                oldPinValue = oldPin;
                oldPinValueLen = ConvertUtils.UInt32FromInt32(oldPin.Length);
            }
            
            byte[] newPinValue = null;
            NativeULong newPinValueLen = 0;
            if (newPin != null)
            {
                newPinValue = newPin;
                newPinValueLen = ConvertUtils.UInt32FromInt32(newPin.Length);
            }
            
            CKR rv = _pkcs11Library.C_SetPIN(_sessionId, oldPinValue, oldPinValueLen, newPinValue, newPinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetPIN", rv);
        }

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <returns>Information about a session</returns>
        public ISessionInfo GetSessionInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetSessionInfo", _sessionId);

            CK_SESSION_INFO sessionInfo = new CK_SESSION_INFO();
            CKR rv = _pkcs11Library.C_GetSessionInfo(_sessionId, ref sessionInfo);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSessionInfo", rv);

            return new SessionInfo(_sessionId, sessionInfo);
        }

        /// <summary>
        /// Obtains a copy of the cryptographic operations state of a session encoded as an array of bytes
        /// </summary>
        /// <returns>Operations state of a session</returns>
        public byte[] GetOperationState()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetOperationState", _sessionId);

            NativeULong operationStateLen = 0;
            CKR rv = _pkcs11Library.C_GetOperationState(_sessionId, null, ref operationStateLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetOperationState", rv);

            byte[] operationState = new byte[operationStateLen];
            rv = _pkcs11Library.C_GetOperationState(_sessionId, operationState, ref operationStateLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetOperationState", rv);

            return operationState;
        }

        /// <summary>
        /// Restores the cryptographic operations state of a session from an array of bytes obtained with GetOperationState
        /// </summary>
        /// <param name="state">Array of bytes obtained with GetOperationState</param>
        /// <param name="encryptionKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing encryption or decryption operation in the restored session</param>
        /// <param name="authenticationKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing signature, MACing, or verification operation in the restored session</param>
        public void SetOperationState(byte[] state, IObjectHandle encryptionKey, IObjectHandle authenticationKey)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SetOperationState", _sessionId);

            if (state == null)
                throw new ArgumentNullException("state");

            if (encryptionKey == null)
                throw new ArgumentNullException("encryptionKey");

            if (authenticationKey == null)
                throw new ArgumentNullException("authenticationKey");

            CKR rv = _pkcs11Library.C_SetOperationState(_sessionId, state, ConvertUtils.UInt32FromInt32(state.Length), ConvertUtils.UInt32FromUInt64(encryptionKey.ObjectId), ConvertUtils.UInt32FromUInt64(authenticationKey.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetOperationState", rv);
        }

        /// <summary>
        /// Logs a user into a token
        /// </summary>
        /// <param name="userType">Type of user</param>
        /// <param name="pin">Pin of user</param>
        public void Login(CKU userType, string pin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Login1", _sessionId);

            if (_logger.IsEnabled(Pkcs11InteropLogLevel.Info))
                _logger.Info("Logging as {0} into session {1}", Pkcs11InteropLogUtils.ToString(userType), _sessionId);

            byte[] pinValue = null;
            NativeULong pinValueLen = 0;
            if (pin != null)
            {
                pinValue = ConvertUtils.Utf8StringToBytes(pin);
                pinValueLen = ConvertUtils.UInt32FromInt32(pinValue.Length);
            }

            CKR rv = _pkcs11Library.C_Login(_sessionId, userType, pinValue, pinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Login", rv);
        }

        /// <summary>
        /// Logs a user into a token
        /// </summary>
        /// <param name="userType">Type of user</param>
        /// <param name="pin">Pin of user</param>
        public void Login(CKU userType, byte[] pin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Login2", _sessionId);

            if (_logger.IsEnabled(Pkcs11InteropLogLevel.Info))
                _logger.Info("Logging as {0} into session {1}", Pkcs11InteropLogUtils.ToString(userType), _sessionId);

            byte[] pinValue = null;
            NativeULong pinValueLen = 0;
            if (pin != null)
            {
                pinValue = pin;
                pinValueLen = ConvertUtils.UInt32FromInt32(pin.Length);
            }

            CKR rv = _pkcs11Library.C_Login(_sessionId, userType, pinValue, pinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Login", rv);
        }

        /// <summary>
        /// Logs a user out from a token
        /// </summary>
        public void Logout()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Logout", _sessionId);

            _logger.Info("Logging out of session {0}", _sessionId);

            CKR rv = _pkcs11Library.C_Logout(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Logout", rv);
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="attributes">Object attributes</param>
        /// <returns>Handle of created object</returns>
        public IObjectHandle CreateObject(List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::CreateObject", _sessionId);

            NativeULong objectId = CK.CK_INVALID_HANDLE;

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = ConvertUtils.UInt32FromInt32(attributes.Count);
                template = new CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(templateLength); i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
            }

            CKR rv = _pkcs11Library.C_CreateObject(_sessionId, template, templateLength, ref objectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CreateObject", rv);

            return new ObjectHandle(objectId);
        }

        /// <summary>
        /// Copies an object, creating a new object for the copy
        /// </summary>
        /// <param name="objectHandle">Handle of object to be copied</param>
        /// <param name="attributes">New values for any attributes of the object that can ordinarily be modified</param>
        /// <returns>Handle of copied object</returns>
        public IObjectHandle CopyObject(IObjectHandle objectHandle, List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::CopyObject", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            NativeULong objectId = CK.CK_INVALID_HANDLE;

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLength = 0;

            if (attributes != null)
            {
                templateLength = ConvertUtils.UInt32FromInt32(attributes.Count);
                template = new CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(templateLength); i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
            }

            CKR rv = _pkcs11Library.C_CopyObject(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), template, templateLength, ref objectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CopyObject", rv);

            return new ObjectHandle(objectId);
        }

        /// <summary>
        /// Destroys an object
        /// </summary>
        /// <param name="objectHandle">Handle of object to be destroyed</param>
        public void DestroyObject(IObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DestroyObject", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            CKR rv = _pkcs11Library.C_DestroyObject(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DestroyObject", rv);
        }

        /// <summary>
        /// Gets the size of an object in bytes.
        /// </summary>
        /// <param name="objectHandle">Handle of object</param>
        /// <returns>Size of an object in bytes</returns>
        public ulong GetObjectSize(IObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetObjectSize", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            NativeULong objectSize = 0;
            CKR rv = _pkcs11Library.C_GetObjectSize(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), ref objectSize);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetObjectSize", rv);

            return ConvertUtils.UInt32ToUInt64(objectSize);
        }

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        public List<IObjectAttribute> GetAttributeValue(IObjectHandle objectHandle, List<CKA> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetAttributeValue1", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            List<ulong> ulongs = new List<ulong>();
            foreach (CKA attribute in attributes)
                ulongs.Add(ConvertUtils.UInt32FromCKA(attribute));

            return GetAttributeValue(objectHandle, ulongs);
        }

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        public List<IObjectAttribute> GetAttributeValue(IObjectHandle objectHandle, List<ulong> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetAttributeValue2", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            // Prepare array of CK_ATTRIBUTEs
            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[attributes.Count];
            for (int i = 0; i < attributes.Count; i++)
                template[i] = CkaUtils.CreateAttribute(ConvertUtils.UInt32FromUInt64(attributes[i]));

            // Determine size of attribute values
            CKR rv = _pkcs11Library.C_GetAttributeValue(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), template, ConvertUtils.UInt32FromInt32(template.Length));
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_ATTRIBUTE_SENSITIVE) && (rv != CKR.CKR_ATTRIBUTE_TYPE_INVALID))
                throw new Pkcs11Exception("C_GetAttributeValue", rv);

            // Allocate memory for each attribute
            for (int i = 0; i < template.Length; i++)
            {
                // PKCS#11 v2.20 page 133:
                // If the specified attribute (i.e., the attribute specified by the type field) for the object
                // cannot be revealed because the object is sensitive or unextractable, then the
                // ulValueLen field in that triple is modified to hold the value -1 (i.e., when it is cast to a
                // CK_LONG, it holds -1).
                if ((NativeLong)template[i].valueLen != -1)
                    template[i].value = Common.UnmanagedMemory.Allocate(ConvertUtils.UInt32ToInt32(template[i].valueLen));
            }

            // Read values of attributes
            rv = _pkcs11Library.C_GetAttributeValue(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), template, ConvertUtils.UInt32FromInt32(template.Length));
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_ATTRIBUTE_SENSITIVE) && (rv != CKR.CKR_ATTRIBUTE_TYPE_INVALID))
                throw new Pkcs11Exception("C_GetAttributeValue", rv);

            // Third call to C_GetAttributeValue is needed if any of the attributes is an array attribute
            bool thirdCallNeeded = false;
            for (int i = 0; i < template.Length; i++)
            {
                if (MiscSettings.AttributesWithNestedAttributes.ContainsKey(ConvertUtils.UInt32ToUInt64(template[i].type)))
                {
                    // PKCS#11 v2.20 page 133:
                    // If the specified attribute (i.e., the attribute specified by the type field) for the object
                    // cannot be revealed because the object is sensitive or unextractable, then the
                    // ulValueLen field in that triple is modified to hold the value -1 (i.e., when it is cast to a
                    // CK_LONG, it holds -1).
                    if ((NativeLong)template[i].valueLen == -1)
                        continue;

                    int ckAttributeSize = UnmanagedMemory.SizeOf(typeof(CK_ATTRIBUTE));
                    int nestedAttrCount = ConvertUtils.UInt32ToInt32(template[i].valueLen) / ckAttributeSize;
                    int nestedAttrCountMod = ConvertUtils.UInt32ToInt32(template[i].valueLen) % ckAttributeSize;

                    if (nestedAttrCountMod != 0)
                        throw new Exception("Unable to read attribute value as attribute array");

                    if (nestedAttrCount == 0)
                    {
                        continue;
                    }
                    else
                    {
                        thirdCallNeeded = true;

                        // Allocate memory for each nested attribute
                        for (int j = 0; j < nestedAttrCount; j++)
                        {
                            IntPtr tempPointer = new IntPtr(template[i].value.ToInt64() + (j * ckAttributeSize));
                            CK_ATTRIBUTE tempAttribute = (CK_ATTRIBUTE)UnmanagedMemory.Read(tempPointer, typeof(CK_ATTRIBUTE));

                            if ((NativeLong)tempAttribute.valueLen != -1)
                                tempAttribute.value = Common.UnmanagedMemory.Allocate(ConvertUtils.UInt32ToInt32(tempAttribute.valueLen));

                            Common.UnmanagedMemory.Write(tempPointer, tempAttribute);
                        }
                    }
                }
            }

            // Read values of all nested attributes
            if (thirdCallNeeded)
            {
                rv = _pkcs11Library.C_GetAttributeValue(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), template, ConvertUtils.UInt32FromInt32(template.Length));
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_ATTRIBUTE_SENSITIVE) && (rv != CKR.CKR_ATTRIBUTE_TYPE_INVALID))
                    throw new Pkcs11Exception("C_GetAttributeValue", rv);
            }

            // Convert CK_ATTRIBUTEs to ObjectAttributes
            List<IObjectAttribute> outAttributes = new List<IObjectAttribute>();
            for (int i = 0; i < template.Length; i++)
                outAttributes.Add(new ObjectAttribute(template[i]));

            return outAttributes;
        }

        /// <summary>
        /// Modifies the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be modified</param>
        /// <param name="attributes">List of attributes that should be modified</param>
        public void SetAttributeValue(IObjectHandle objectHandle, List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SetAttributeValue", _sessionId);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");
            
            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[attributes.Count];
            for (int i = 0; i < attributes.Count; i++)
                template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_SetAttributeValue(_sessionId, ConvertUtils.UInt32FromUInt64(objectHandle.ObjectId), template, ConvertUtils.UInt32FromInt32(template.Length));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetAttributeValue", rv);
        }

        /// <summary>
        /// Initializes a search for token and session objects that match a attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        public void FindObjectsInit(List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::FindObjectsInit", _sessionId);

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = ConvertUtils.UInt32FromInt32(attributes.Count);
                template = new CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(templateLength); i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
            }

            CKR rv = _pkcs11Library.C_FindObjectsInit(_sessionId, template, templateLength);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsInit", rv);
        }

        /// <summary>
        /// Continues a search for token and session objects that match a template, obtaining additional object handles
        /// </summary>
        /// <param name="objectCount">Maximum number of object handles to be returned</param>
        /// <returns>Found object handles</returns>
        public List<IObjectHandle> FindObjects(int objectCount)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::FindObjects", _sessionId);

            List<IObjectHandle> foundObjects = new List<IObjectHandle>();

            NativeULong[] objects = new NativeULong[objectCount];
            NativeULong foundObjectsCount = 0;
            CKR rv = _pkcs11Library.C_FindObjects(_sessionId, objects, ConvertUtils.UInt32FromInt32(objectCount), ref foundObjectsCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjects", rv);

            for (int i = 0; i < ConvertUtils.UInt32ToInt32(foundObjectsCount); i++)
                foundObjects.Add(new ObjectHandle(objects[i]));

            return foundObjects;
        }

        /// <summary>
        /// Terminates a search for token and session objects
        /// </summary>
        public void FindObjectsFinal()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::FindObjectsFinal", _sessionId);

            CKR rv = _pkcs11Library.C_FindObjectsFinal(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsFinal", rv);
        }

        /// <summary>
        /// Searches for all token and session objects that match provided attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        /// <returns>Handles of found objects</returns>
        public List<IObjectHandle> FindAllObjects(List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::FindAllObjects", _sessionId);

            List<IObjectHandle> foundObjects = new List<IObjectHandle>();

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = ConvertUtils.UInt32FromInt32(attributes.Count);
                template = new CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(templateLength); i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
            }

            CKR rv = _pkcs11Library.C_FindObjectsInit(_sessionId, template, templateLength);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsInit", rv);

            NativeULong objectsLength = 256;
            NativeULong[] objects = new NativeULong[objectsLength];
            NativeULong objectCount = objectsLength;
            while (objectCount == objectsLength)
            {
                rv = _pkcs11Library.C_FindObjects(_sessionId, objects, objectsLength, ref objectCount);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_FindObjects", rv);

                for (int i = 0; i < ConvertUtils.UInt32ToInt32(objectCount); i++)
                    foundObjects.Add(new ObjectHandle(objects[i]));
            }

            rv = _pkcs11Library.C_FindObjectsFinal(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsFinal", rv);

            return foundObjects;
        }

        /// <summary>
        /// Encrypts single-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be encrypted</param>
        /// <returns>Encrypted data</returns>
        public byte[] Encrypt(IMechanism mechanism, IObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Encrypt1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_EncryptInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            NativeULong encryptedDataLen = 0;
            rv = _pkcs11Library.C_Encrypt(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), null, ref encryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Encrypt", rv);

            byte[] encryptedData = new byte[encryptedDataLen];
            rv = _pkcs11Library.C_Encrypt(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), encryptedData, ref encryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Encrypt", rv);

            if (encryptedData.Length != ConvertUtils.UInt32ToInt32(encryptedDataLen))
                Array.Resize(ref encryptedData, ConvertUtils.UInt32ToInt32(encryptedDataLen));

            return encryptedData;
        }

        /// <summary>
        /// Encrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be encrypted should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        public void Encrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Encrypt2", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            Encrypt(mechanism, keyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Encrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be encrypted should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        public void Encrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Encrypt3", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_EncryptInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            NativeULong encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);
            
            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);
                rv = _pkcs11Library.C_EncryptUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_EncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, ConvertUtils.UInt32ToInt32(encryptedPartLen));
            }

            byte[] lastEncryptedPart = null;
            NativeULong lastEncryptedPartLen = 0;
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, ConvertUtils.UInt32ToInt32(lastEncryptedPartLen));
        }

        /// <summary>
        /// Decrypts single-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="encryptedData">Data to be decrypted</param>
        /// <returns>Decrypted data</returns>
        public byte[] Decrypt(IMechanism mechanism, IObjectHandle keyHandle, byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Decrypt1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (encryptedData == null)
                throw new ArgumentNullException("encryptedData");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DecryptInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            NativeULong decryptedDataLen = 0;
            rv = _pkcs11Library.C_Decrypt(_sessionId, encryptedData, ConvertUtils.UInt32FromInt32(encryptedData.Length), null, ref decryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Decrypt", rv);

            byte[] decryptedData = new byte[decryptedDataLen];
            rv = _pkcs11Library.C_Decrypt(_sessionId, encryptedData, ConvertUtils.UInt32FromInt32(encryptedData.Length), decryptedData, ref decryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Decrypt", rv);

            if (decryptedData.Length != ConvertUtils.UInt32ToInt32(decryptedDataLen))
                Array.Resize(ref decryptedData, ConvertUtils.UInt32ToInt32(decryptedDataLen));

            return decryptedData;
        }

        /// <summary>
        /// Decrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which encrypted data should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        public void Decrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Decrypt2", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            Decrypt(mechanism, keyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Decrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which encrypted data should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        public void Decrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Decrypt3", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");
            
            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DecryptInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            NativeULong partLen = ConvertUtils.UInt32FromInt32(part.Length);

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = ConvertUtils.UInt32FromInt32(part.Length);
                rv = _pkcs11Library.C_DecryptUpdate(_sessionId, encryptedPart, ConvertUtils.UInt32FromInt32(bytesRead), part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptUpdate", rv);

                outputStream.Write(part, 0, ConvertUtils.UInt32ToInt32(partLen));
            }

            byte[] lastPart = null;
            NativeULong lastPartLen = 0;
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, ConvertUtils.UInt32ToInt32(lastPartLen));
        }

        /// <summary>
        /// Digests the value of a secret key
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="keyHandle">Handle of the secret key to be digested</param>
        /// <returns>Digest</returns>
        public byte[] DigestKey(IMechanism mechanism, IObjectHandle keyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DigestKey", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();
            
            CKR rv = _pkcs11Library.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);
            
            rv = _pkcs11Library.C_DigestKey(_sessionId, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestKey", rv);
            
            NativeULong digestLen = 0;
            rv = _pkcs11Library.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);
            
            byte[] digest = new byte[digestLen];
            rv = _pkcs11Library.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != ConvertUtils.UInt32ToInt32(digestLen))
                Array.Resize(ref digest, ConvertUtils.UInt32ToInt32(digestLen));

            return digest;
        }

        /// <summary>
        /// Digests single-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="data">Data to be digested</param>
        /// <returns>Digest</returns>
        public byte[] Digest(IMechanism mechanism, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Digest1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (data == null)
                throw new ArgumentNullException("data");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            NativeULong digestLen = 0;
            rv = _pkcs11Library.C_Digest(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Digest", rv);

            byte[] digest = new byte[digestLen];
            rv = _pkcs11Library.C_Digest(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Digest", rv);

            if (digest.Length != ConvertUtils.UInt32ToInt32(digestLen))
                Array.Resize(ref digest, ConvertUtils.UInt32ToInt32(digestLen));

            return digest;
        }

        /// <summary>
        /// Digests multi-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Digest</returns>
        public byte[] Digest(IMechanism mechanism, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Digest2", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            return Digest(mechanism, inputStream, 4096);
        }

        /// <summary>
        /// Digests multi-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Digest</returns>
        public byte[] Digest(IMechanism mechanism, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Digest3", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _pkcs11Library.C_DigestUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead));
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DigestUpdate", rv);
            }

            NativeULong digestLen = 0;
            rv = _pkcs11Library.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _pkcs11Library.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != ConvertUtils.UInt32ToInt32(digestLen))
                Array.Resize(ref digest, ConvertUtils.UInt32ToInt32(digestLen));

            return digest;
        }

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <param name="performLogin">Flag indicating whether context specific login should be performed</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <returns>Signature</returns>
        protected byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] data, bool performLogin, byte[] keyPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_SignInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            if (performLogin)
            {
                byte[] pinValue = null;
                NativeULong pinValueLen = 0;
                if (keyPin != null)
                {
                    pinValue = keyPin;
                    pinValueLen = ConvertUtils.UInt32FromInt32(keyPin.Length);
                }

                rv = _pkcs11Library.C_Login(_sessionId, CKU.CKU_CONTEXT_SPECIFIC, pinValue, pinValueLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_Login", rv);
            }

            NativeULong signatureLen = 0;
            rv = _pkcs11Library.C_Sign(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Sign", rv);

            byte[] signature = new byte[signatureLen];
            rv = _pkcs11Library.C_Sign(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Sign", rv);

            if (signature.Length != ConvertUtils.UInt32ToInt32(signatureLen))
                Array.Resize(ref signature, ConvertUtils.UInt32ToInt32(signatureLen));

            return signature;
        }

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign1a", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            return Sign(mechanism, keyHandle, data, false, null);
        }

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign1b", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            return Sign(mechanism, keyHandle, data, true, ConvertUtils.Utf8StringToBytes(keyPin));
        }

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign1c", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            return Sign(mechanism, keyHandle, data, true, keyPin);
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign2a", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            return Sign(mechanism, keyHandle, inputStream, 4096);
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign2b", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            return Sign(mechanism, keyHandle, keyPin, inputStream, 4096);
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign2c", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            return Sign(mechanism, keyHandle, keyPin, inputStream, 4096);
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <param name="performLogin">Flag indicating whether context specific login should be performed</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <returns>Signature</returns>
        protected byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, int bufferLength, bool performLogin, byte[] keyPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign3", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_SignInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            if (performLogin)
            {
                byte[] pinValue = null;
                NativeULong pinValueLen = 0;
                if (keyPin != null)
                {
                    pinValue = keyPin;
                    pinValueLen = ConvertUtils.UInt32FromInt32(keyPin.Length);
                }

                rv = _pkcs11Library.C_Login(_sessionId, CKU.CKU_CONTEXT_SPECIFIC, pinValue, pinValueLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_Login", rv);
            }

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _pkcs11Library.C_SignUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead));
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_SignUpdate", rv);
            }

            NativeULong signatureLen = 0;
            rv = _pkcs11Library.C_SignFinal(_sessionId, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            byte[] signature = new byte[signatureLen];
            rv = _pkcs11Library.C_SignFinal(_sessionId, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            if (signature.Length != ConvertUtils.UInt32ToInt32(signatureLen))
                Array.Resize(ref signature, ConvertUtils.UInt32ToInt32(signatureLen));

            return signature;
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign3a", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return Sign(mechanism, keyHandle, inputStream, bufferLength, false, null);
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign3b", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return Sign(mechanism, keyHandle, inputStream, bufferLength, true, ConvertUtils.Utf8StringToBytes(keyPin));
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Sign3c", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return Sign(mechanism, keyHandle, inputStream, bufferLength, true, keyPin);
        }

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <param name="performLogin">Flag indicating whether context specific login should be performed</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <returns>Signature</returns>
        protected byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] data, bool performLogin, byte[] keyPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignRecover1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_SignRecoverInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecoverInit", rv);

            if (performLogin)
            {
                byte[] pinValue = null;
                NativeULong pinValueLen = 0;
                if (keyPin != null)
                {
                    pinValue = keyPin;
                    pinValueLen = ConvertUtils.UInt32FromInt32(keyPin.Length);
                }

                rv = _pkcs11Library.C_Login(_sessionId, CKU.CKU_CONTEXT_SPECIFIC, pinValue, pinValueLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_Login", rv);
            }

            NativeULong signatureLen = 0;
            rv = _pkcs11Library.C_SignRecover(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecover", rv);

            byte[] signature = new byte[signatureLen];
            rv = _pkcs11Library.C_SignRecover(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecover", rv);

            if (signature.Length != ConvertUtils.UInt32ToInt32(signatureLen))
                Array.Resize(ref signature, ConvertUtils.UInt32ToInt32(signatureLen));

            return signature;
        }

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignRecover1a", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            return SignRecover(mechanism, keyHandle, data, false, null);
        }

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignRecover1b", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            return SignRecover(mechanism, keyHandle, data, true, ConvertUtils.Utf8StringToBytes(keyPin));
        }

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignRecover1c", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            return SignRecover(mechanism, keyHandle, data, true, keyPin);
        }

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="data">Data that was signed</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        public void Verify(IMechanism mechanism, IObjectHandle keyHandle, byte[] data, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Verify1", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            if (signature == null)
                throw new ArgumentNullException("signature");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_VerifyInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            rv = _pkcs11Library.C_Verify(_sessionId, data, ConvertUtils.UInt32FromInt32(data.Length), signature, ConvertUtils.UInt32FromInt32(signature.Length));
            if (rv == CKR.CKR_OK)
                isValid = true;
            else if (rv == CKR.CKR_SIGNATURE_INVALID)
                isValid = false;
            else 
                throw new Pkcs11Exception("C_Verify", rv);
        }

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="inputStream">Input stream from which data that was signed should be read</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        public void Verify(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Verify2", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (signature == null)
                throw new ArgumentNullException("signature");

            Verify(mechanism, keyHandle, inputStream, signature, out isValid, 4096);
        }

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="inputStream">Input stream from which data that was signed should be read</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        public void Verify(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::Verify3", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (signature == null)
                throw new ArgumentNullException("signature");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_VerifyInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _pkcs11Library.C_VerifyUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead));
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_VerifyUpdate", rv);
            }

            rv = _pkcs11Library.C_VerifyFinal(_sessionId, signature, ConvertUtils.UInt32FromInt32(signature.Length));
            if (rv == CKR.CKR_OK)
                isValid = true;
            else if (rv == CKR.CKR_SIGNATURE_INVALID)
                isValid = false;
            else 
                throw new Pkcs11Exception("C_VerifyFinal", rv);
        }

        /// <summary>
        /// Verifies signature of data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        /// <returns>Data recovered from the signature</returns>
        public byte[] VerifyRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::VerifyRecover", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (signature == null)
                throw new ArgumentNullException("signature");
            
            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_VerifyRecoverInit(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyRecoverInit", rv);

            NativeULong dataLen = 0;
            rv = _pkcs11Library.C_VerifyRecover(_sessionId, signature, ConvertUtils.UInt32FromInt32(signature.Length), null, ref dataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyRecover", rv);

            byte[] data = new byte[dataLen];
            rv = _pkcs11Library.C_VerifyRecover(_sessionId, signature, ConvertUtils.UInt32FromInt32(signature.Length), data, ref dataLen);
            if (rv == CKR.CKR_OK)
                isValid = true;
            else if (rv == CKR.CKR_SIGNATURE_INVALID)
                isValid = false;
            else 
                throw new Pkcs11Exception("C_VerifyRecover", rv);

            if (data.Length != ConvertUtils.UInt32ToInt32(dataLen))
                Array.Resize(ref data, ConvertUtils.UInt32ToInt32(dataLen));

            return data;
        }

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="digest">Digest</param>
        /// <param name="encryptedData">Encrypted data</param>
        public void DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DigestEncrypt1", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                digest = DigestEncrypt(digestingMechanism, encryptionMechanism, keyHandle, inputMemoryStream, outputMemorySteam);
                encryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Digest</returns>
        public byte[] DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DigestEncrypt2", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            return DigestEncrypt(digestingMechanism, encryptionMechanism, keyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Digest</returns>
        public byte[] DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DigestEncrypt3", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckDigestingMechanism = (CK_MECHANISM)digestingMechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DigestInit(_sessionId, ref ckDigestingMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            CK_MECHANISM ckEncryptionMechanism = (CK_MECHANISM)encryptionMechanism.ToMarshalableStructure();

            rv = _pkcs11Library.C_EncryptInit(_sessionId, ref ckEncryptionMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            NativeULong encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);
                rv = _pkcs11Library.C_DigestEncryptUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DigestEncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, ConvertUtils.UInt32ToInt32(encryptedPartLen));
            }

            byte[] lastEncryptedPart = null;
            NativeULong lastEncryptedPartLen = 0;
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, ConvertUtils.UInt32ToInt32(lastEncryptedPartLen));

            NativeULong digestLen = 0;
            rv = _pkcs11Library.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _pkcs11Library.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != ConvertUtils.UInt32ToInt32(digestLen))
                Array.Resize(ref digest, ConvertUtils.UInt32ToInt32(digestLen));

            return digest;
        }

        /// <summary>
        /// Digests and decrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="digest">Digest</param>
        /// <param name="decryptedData">Decrypted data</param>
        public void DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] decryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptDigest1", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                digest = DecryptDigest(digestingMechanism, decryptionMechanism, keyHandle, inputMemoryStream, outputMemorySteam);
                decryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Digests and decrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <returns>Digest</returns>
        public byte[] DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptDigest2", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            return DecryptDigest(digestingMechanism, decryptionMechanism, keyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Digests and decrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Digest</returns>
        public byte[] DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptDigest3", _sessionId);

            if (digestingMechanism == null)
                throw new ArgumentNullException("digestingMechanism");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckDigestingMechanism = (CK_MECHANISM)digestingMechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_DigestInit(_sessionId, ref ckDigestingMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            CK_MECHANISM ckDecryptionMechanism = (CK_MECHANISM)decryptionMechanism.ToMarshalableStructure();

            rv = _pkcs11Library.C_DecryptInit(_sessionId, ref ckDecryptionMechanism, ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            NativeULong partLen = ConvertUtils.UInt32FromInt32(part.Length);

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = ConvertUtils.UInt32FromInt32(part.Length);
                rv = _pkcs11Library.C_DecryptDigestUpdate(_sessionId, encryptedPart, ConvertUtils.UInt32FromInt32(bytesRead), part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptDigestUpdate", rv);

                outputStream.Write(part, 0, ConvertUtils.UInt32ToInt32(partLen));
            }

            byte[] lastPart = null;
            NativeULong lastPartLen = 0;
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, ConvertUtils.UInt32ToInt32(lastPartLen));

            NativeULong digestLen = 0;
            rv = _pkcs11Library.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _pkcs11Library.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != ConvertUtils.UInt32ToInt32(digestLen))
                Array.Resize(ref digest, ConvertUtils.UInt32ToInt32(digestLen));

            return digest;
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="signature">Signature</param>
        /// <param name="encryptedData">Encrypted data</param>
        public void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt1a", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");
            
            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                signature = SignEncrypt(signingMechanism, signingKeyHandle, encryptionMechanism, encryptionKeyHandle, inputMemoryStream, outputMemorySteam);
                encryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="signature">Signature</param>
        /// <param name="encryptedData">Encrypted data</param>
        public void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt1b", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                signature = SignEncrypt(signingMechanism, signingKeyHandle, signingKeyPin, encryptionMechanism, encryptionKeyHandle, inputMemoryStream, outputMemorySteam);
                encryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="signature">Signature</param>
        /// <param name="encryptedData">Encrypted data</param>
        public void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt1c", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                signature = SignEncrypt(signingMechanism, signingKeyHandle, signingKeyPin, encryptionMechanism, encryptionKeyHandle, inputMemoryStream, outputMemorySteam);
                encryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt2a", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");
            
            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            return SignEncrypt(signingMechanism, signingKeyHandle, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt2b", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            return SignEncrypt(signingMechanism, signingKeyHandle, signingKeyPin, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt2c", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            return SignEncrypt(signingMechanism, signingKeyHandle, signingKeyPin, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, 4096);
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <param name="performLogin">Flag indicating whether context specific login should be performed</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <returns>Signature</returns>
        protected byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength, bool performLogin, byte[] signingKeyPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt3", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckSigningMechanism = (CK_MECHANISM)signingMechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_SignInit(_sessionId, ref ckSigningMechanism, ConvertUtils.UInt32FromUInt64(signingKeyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            if (performLogin)
            {
                byte[] pinValue = null;
                NativeULong pinValueLen = 0;
                if (signingKeyPin != null)
                {
                    pinValue = signingKeyPin;
                    pinValueLen = ConvertUtils.UInt32FromInt32(signingKeyPin.Length);
                }

                rv = _pkcs11Library.C_Login(_sessionId, CKU.CKU_CONTEXT_SPECIFIC, pinValue, pinValueLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_Login", rv);
            }

            CK_MECHANISM ckEncryptionMechanism = (CK_MECHANISM)encryptionMechanism.ToMarshalableStructure();

            rv = _pkcs11Library.C_EncryptInit(_sessionId, ref ckEncryptionMechanism, ConvertUtils.UInt32FromUInt64(encryptionKeyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            NativeULong encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = ConvertUtils.UInt32FromInt32(encryptedPart.Length);
                rv = _pkcs11Library.C_SignEncryptUpdate(_sessionId, part, ConvertUtils.UInt32FromInt32(bytesRead), encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_SignEncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, ConvertUtils.UInt32ToInt32(encryptedPartLen));
            }

            byte[] lastEncryptedPart = null;
            NativeULong lastEncryptedPartLen = 0;
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _pkcs11Library.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, ConvertUtils.UInt32ToInt32(lastEncryptedPartLen));

            NativeULong signatureLen = 0;
            rv = _pkcs11Library.C_SignFinal(_sessionId, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            byte[] signature = new byte[signatureLen];
            rv = _pkcs11Library.C_SignFinal(_sessionId, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            if (signature.Length != ConvertUtils.UInt32ToInt32(signatureLen))
                Array.Resize(ref signature, ConvertUtils.UInt32ToInt32(signatureLen));

            return signature;
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt3a", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");
            
            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");
            
            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");
            
            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return SignEncrypt(signingMechanism, signingKeyHandle, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, bufferLength, false, null);
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt3b", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return SignEncrypt(signingMechanism, signingKeyHandle, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, bufferLength, true, ConvertUtils.Utf8StringToBytes(signingKeyPin));
        }

        /// <summary>
        /// Signs and encrypts data
        /// </summary>
        /// <param name="signingMechanism">Signing mechanism</param>
        /// <param name="signingKeyHandle">Handle of the signing key</param>
        /// <param name="signingKeyPin">Context specific signature pin</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SignEncrypt3c", _sessionId);

            if (signingMechanism == null)
                throw new ArgumentNullException("signingMechanism");

            if (signingKeyHandle == null)
                throw new ArgumentNullException("signingKeyHandle");

            if (encryptionMechanism == null)
                throw new ArgumentNullException("encryptionMechanism");

            if (encryptionKeyHandle == null)
                throw new ArgumentNullException("encryptionKeyHandle");

            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            return SignEncrypt(signingMechanism, signingKeyHandle, encryptionMechanism, encryptionKeyHandle, inputStream, outputStream, bufferLength, true, signingKeyPin);
        }

        /// <summary>
        /// Decrypts data and verifies a signature of data
        /// </summary>
        /// <param name="verificationMechanism">Verification mechanism</param>
        /// <param name="verificationKeyHandle">Handle of the verification key</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="decryptionKeyHandle">Handle of the decryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="signature">Signature</param>
        /// <param name="decryptedData">Decrypted data</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        public void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, byte[] data, byte[] signature, out byte[] decryptedData, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptVerify1", _sessionId);

            if (verificationMechanism == null)
                throw new ArgumentNullException("verificationMechanism");
            
            if (verificationKeyHandle == null)
                throw new ArgumentNullException("verificationKeyHandle");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (decryptionKeyHandle == null)
                throw new ArgumentNullException("decryptionKeyHandle");

            if (data == null)
                throw new ArgumentNullException("data");
            
            if (signature == null)
                throw new ArgumentNullException("signature");

            using (MemoryStream inputMemoryStream = new MemoryStream(data), outputMemorySteam = new MemoryStream())
            {
                DecryptVerify(verificationMechanism, verificationKeyHandle, decryptionMechanism, decryptionKeyHandle, inputMemoryStream, outputMemorySteam, signature, out isValid);
                decryptedData = outputMemorySteam.ToArray();
            }
        }

        /// <summary>
        /// Decrypts data and verifies a signature of data
        /// </summary>
        /// <param name="verificationMechanism">Verification mechanism</param>
        /// <param name="verificationKeyHandle">Handle of the verification key</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="decryptionKeyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        public void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptVerify2", _sessionId);

            if (verificationMechanism == null)
                throw new ArgumentNullException("verificationMechanism");
            
            if (verificationKeyHandle == null)
                throw new ArgumentNullException("verificationKeyHandle");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (decryptionKeyHandle == null)
                throw new ArgumentNullException("decryptionKeyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (signature == null)
                throw new ArgumentNullException("signature");

            DecryptVerify(verificationMechanism, verificationKeyHandle, decryptionMechanism, decryptionKeyHandle, inputStream, outputStream, signature, out isValid, 4096);
        }

        /// <summary>
        /// Decrypts data and verifies a signature of data
        /// </summary>
        /// <param name="verificationMechanism">Verification mechanism</param>
        /// <param name="verificationKeyHandle">Handle of the verification key</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="decryptionKeyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        public void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DecryptVerify3", _sessionId);

            if (verificationMechanism == null)
                throw new ArgumentNullException("verificationMechanism");
            
            if (verificationKeyHandle == null)
                throw new ArgumentNullException("verificationKeyHandle");
            
            if (decryptionMechanism == null)
                throw new ArgumentNullException("decryptionMechanism");
            
            if (decryptionKeyHandle == null)
                throw new ArgumentNullException("decryptionKeyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");
            
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");
            
            if (signature == null)
                throw new ArgumentNullException("signature");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            CK_MECHANISM ckVerificationMechanism = (CK_MECHANISM)verificationMechanism.ToMarshalableStructure();

            CKR rv = _pkcs11Library.C_VerifyInit(_sessionId, ref ckVerificationMechanism, ConvertUtils.UInt32FromUInt64(verificationKeyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            CK_MECHANISM ckDecryptionMechanism = (CK_MECHANISM)decryptionMechanism.ToMarshalableStructure();

            rv = _pkcs11Library.C_DecryptInit(_sessionId, ref ckDecryptionMechanism, ConvertUtils.UInt32FromUInt64(decryptionKeyHandle.ObjectId));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            NativeULong partLen = ConvertUtils.UInt32FromInt32(part.Length);

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = ConvertUtils.UInt32FromInt32(part.Length);
                rv = _pkcs11Library.C_DecryptVerifyUpdate(_sessionId, encryptedPart, ConvertUtils.UInt32FromInt32(bytesRead), part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptVerifyUpdate", rv);

                outputStream.Write(part, 0, ConvertUtils.UInt32ToInt32(partLen));
            }

            byte[] lastPart = null;
            NativeULong lastPartLen = 0;
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _pkcs11Library.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, ConvertUtils.UInt32ToInt32(lastPartLen));

            rv = _pkcs11Library.C_VerifyFinal(_sessionId, signature, ConvertUtils.UInt32FromInt32(signature.Length));
            if (rv == CKR.CKR_OK)
                isValid = true;
            else if (rv == CKR.CKR_SIGNATURE_INVALID)
                isValid = false;
            else 
                throw new Pkcs11Exception("C_VerifyFinal", rv);
        }

        /// <summary>
        /// Generates a secret key or set of domain parameters, creating a new object
        /// </summary>
        /// <param name="mechanism">Generation mechanism</param>
        /// <param name="attributes">Attributes of the new key or set of domain parameters</param>
        /// <returns>Handle of the new key or set of domain parameters</returns>
        public IObjectHandle GenerateKey(IMechanism mechanism, List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GenerateKey", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = ConvertUtils.UInt32FromInt32(attributes.Count);
                template = new CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(templateLength); i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
            }

            NativeULong keyId = CK.CK_INVALID_HANDLE;
            CKR rv = _pkcs11Library.C_GenerateKey(_sessionId, ref ckMechanism, template, templateLength, ref keyId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GenerateKey", rv);

            return new ObjectHandle(keyId);
        }

        /// <summary>
        /// Generates a public/private key pair, creating new key objects
        /// </summary>
        /// <param name="mechanism">Key generation mechanism</param>
        /// <param name="publicKeyAttributes">Attributes of the public key</param>
        /// <param name="privateKeyAttributes">Attributes of the private key</param>
        /// <param name="publicKeyHandle">Handle of the new public key</param>
        /// <param name="privateKeyHandle">Handle of the new private key</param>
        public void GenerateKeyPair(IMechanism mechanism, List<IObjectAttribute> publicKeyAttributes, List<IObjectAttribute> privateKeyAttributes, out IObjectHandle publicKeyHandle, out IObjectHandle privateKeyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GenerateKeyPair", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CK_ATTRIBUTE[] publicKeyTemplate = null;
            NativeULong publicKeyTemplateLength = 0;
            
            if (publicKeyAttributes != null)
            {
                publicKeyTemplateLength = ConvertUtils.UInt32FromInt32(publicKeyAttributes.Count);
                publicKeyTemplate = new CK_ATTRIBUTE[publicKeyTemplateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(publicKeyTemplateLength); i++)
                    publicKeyTemplate[i] = (CK_ATTRIBUTE)publicKeyAttributes[i].ToMarshalableStructure();
            }

            CK_ATTRIBUTE[] privateKeyTemplate = null;
            NativeULong privateKeyTemplateLength = 0;
            
            if (privateKeyAttributes != null)
            {
                privateKeyTemplateLength = ConvertUtils.UInt32FromInt32(privateKeyAttributes.Count);
                privateKeyTemplate = new CK_ATTRIBUTE[privateKeyTemplateLength];
                for (int i = 0; i < ConvertUtils.UInt32ToInt32(privateKeyTemplateLength); i++)
                    privateKeyTemplate[i] = (CK_ATTRIBUTE)privateKeyAttributes[i].ToMarshalableStructure();
            }

            NativeULong publicKeyId = CK.CK_INVALID_HANDLE;
            NativeULong privateKeyId = CK.CK_INVALID_HANDLE;
            CKR rv = _pkcs11Library.C_GenerateKeyPair(_sessionId, ref ckMechanism, publicKeyTemplate, publicKeyTemplateLength, privateKeyTemplate, privateKeyTemplateLength, ref publicKeyId, ref privateKeyId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GenerateKeyPair", rv);

            publicKeyHandle = new ObjectHandle(publicKeyId);
            privateKeyHandle = new ObjectHandle(privateKeyId);
        }

        /// <summary>
        /// Wraps (i.e., encrypts) a private or secret key
        /// </summary>
        /// <param name="mechanism">Wrapping mechanism</param>
        /// <param name="wrappingKeyHandle">Handle of wrapping key</param>
        /// <param name="keyHandle">Handle of key to be wrapped</param>
        /// <returns>Wrapped key</returns>
        public byte[] WrapKey(IMechanism mechanism, IObjectHandle wrappingKeyHandle, IObjectHandle keyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::WrapKey", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (wrappingKeyHandle == null)
                throw new ArgumentNullException("wrappingKeyHandle");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            NativeULong wrappedKeyLen = 0;
            CKR rv = _pkcs11Library.C_WrapKey(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(wrappingKeyHandle.ObjectId), ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId), null, ref wrappedKeyLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_WrapKey", rv);

            byte[] wrappedKey = new byte[wrappedKeyLen];
            rv = _pkcs11Library.C_WrapKey(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(wrappingKeyHandle.ObjectId), ConvertUtils.UInt32FromUInt64(keyHandle.ObjectId), wrappedKey, ref wrappedKeyLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_WrapKey", rv);

            if (wrappedKey.Length != ConvertUtils.UInt32ToInt32(wrappedKeyLen))
                Array.Resize(ref wrappedKey, ConvertUtils.UInt32ToInt32(wrappedKeyLen));

            return wrappedKey;
        }

        /// <summary>
        /// Unwraps (i.e. decrypts) a wrapped key, creating a new private key or secret key object
        /// </summary>
        /// <param name="mechanism">Unwrapping mechanism</param>
        /// <param name="unwrappingKeyHandle">Handle of unwrapping key</param>
        /// <param name="wrappedKey">Wrapped key</param>
        /// <param name="attributes">Attributes for unwrapped key</param>
        /// <returns>Handle of unwrapped key</returns>
        public IObjectHandle UnwrapKey(IMechanism mechanism, IObjectHandle unwrappingKeyHandle, byte[] wrappedKey, List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::UnwrapKey", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (unwrappingKeyHandle == null)
                throw new ArgumentNullException("unwrappingKeyHandle");
            
            if (wrappedKey == null)
                throw new ArgumentNullException("wrappedKey");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLen = 0;
            if (attributes != null)
            {
                template = new CK_ATTRIBUTE[attributes.Count];
                for (int i = 0; i < attributes.Count; i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
                templateLen = ConvertUtils.UInt32FromInt32(attributes.Count);
            }

            NativeULong unwrappedKey = CK.CK_INVALID_HANDLE;
            CKR rv = _pkcs11Library.C_UnwrapKey(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(unwrappingKeyHandle.ObjectId), wrappedKey, ConvertUtils.UInt32FromInt32(wrappedKey.Length), template, templateLen, ref unwrappedKey);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_UnwrapKey", rv);

            return new ObjectHandle(unwrappedKey);
        }

        /// <summary>
        /// Derives a key from a base key, creating a new key object
        /// </summary>
        /// <param name="mechanism">Derivation mechanism</param>
        /// <param name="baseKeyHandle">Handle of base key</param>
        /// <param name="attributes">Attributes for the new key</param>
        /// <returns>Handle of derived key</returns>
        public IObjectHandle DeriveKey(IMechanism mechanism, IObjectHandle baseKeyHandle, List<IObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::DeriveKey", _sessionId);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (baseKeyHandle == null)
                throw new ArgumentNullException("baseKeyHandle");

            CK_MECHANISM ckMechanism = (CK_MECHANISM)mechanism.ToMarshalableStructure();

            CK_ATTRIBUTE[] template = null;
            NativeULong templateLen = 0;
            if (attributes != null)
            {
                template = new CK_ATTRIBUTE[attributes.Count];
                for (int i = 0; i < attributes.Count; i++)
                    template[i] = (CK_ATTRIBUTE)attributes[i].ToMarshalableStructure();
                templateLen = ConvertUtils.UInt32FromInt32(attributes.Count);
            }

            NativeULong derivedKey = CK.CK_INVALID_HANDLE;
            CKR rv = _pkcs11Library.C_DeriveKey(_sessionId, ref ckMechanism, ConvertUtils.UInt32FromUInt64(baseKeyHandle.ObjectId), template, templateLen, ref derivedKey);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DeriveKey", rv);

            return new ObjectHandle(derivedKey);
        }

        /// <summary>
        /// Mixes additional seed material into the token's random number generator
        /// </summary>
        /// <param name="seed">Seed material</param>
        public void SeedRandom(byte[] seed)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::SeedRandom", _sessionId);

            if (seed == null)
                throw new ArgumentNullException("seed");

            CKR rv = _pkcs11Library.C_SeedRandom(_sessionId, seed, ConvertUtils.UInt32FromInt32(seed.Length));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SeedRandom", rv);
        }

        /// <summary>
        /// Generates random or pseudo-random data
        /// </summary>
        /// <param name="length">Length in bytes of the random or pseudo-random data to be generated</param>
        /// <returns>Generated random or pseudo-random data</returns>
        public byte[] GenerateRandom(int length)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GenerateRandom", _sessionId);

            if (length < 1)
                throw new ArgumentException("Value has to be positive number", "length");

            byte[] randomData = new byte[length];
            CKR rv = _pkcs11Library.C_GenerateRandom(_sessionId, randomData, ConvertUtils.UInt32FromInt32(length));
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GenerateRandom", rv);

            return randomData;
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void GetFunctionStatus()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::GetFunctionStatus", _sessionId);

            CKR rv = _pkcs11Library.C_GetFunctionStatus(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetFunctionStatus", rv);
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void CancelFunction()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Session({0})::CancelFunction", _sessionId);

            CKR rv = _pkcs11Library.C_CancelFunction(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CancelFunction", rv);
        }

        #region IDisposable

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            _logger.Debug("Session({0})::Dispose1", _sessionId);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            _logger.Debug("Session({0})::Dispose2", _sessionId);

            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                    if (_sessionId != CK.CK_INVALID_HANDLE && _closeWhenDisposed == true)
                        CloseSession();
                }

                // Dispose unmanaged objects
                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~Session()
        {
            Dispose(false);
        }

        #endregion
    }
}
