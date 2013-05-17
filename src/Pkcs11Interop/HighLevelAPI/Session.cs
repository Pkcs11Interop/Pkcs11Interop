/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Collections.Generic;
using System.Text;
using Net.Pkcs11Interop.Common;
using System.IO;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Class representing a logical connection between an application and a token
    /// </summary>
    public class Session : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level PKCS#11 wrapper
        /// </summary>
        private LowLevelAPI.Pkcs11 _p11 = null;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        private uint _sessionId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public uint SessionId
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _sessionId;
            }
        }

        /// <summary>
        /// Initializes new instance of Session class
        /// </summary>
        /// <param name="pkcs11">Low level PKCS#11 wrapper</param>
        /// <param name="sessionId">PKCS#11 handle of session</param>
        internal Session(LowLevelAPI.Pkcs11 pkcs11, uint sessionId)
        {
            if (pkcs11 == null)
                throw new ArgumentNullException("pkcs11");

            if (sessionId == CK.CK_INVALID_HANDLE)
                throw new ArgumentException("Invalid handle specified", "sessionId");

            _p11 = pkcs11;
            _sessionId = sessionId;
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        public void CloseSession()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            CKR rv = _p11.C_CloseSession(_sessionId);
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

            byte[] pinValue = null;
            uint pinValueLen = 0;
            if (userPin != null)
            {
                pinValue = ConvertUtils.Utf8StringToBytes(userPin);
                pinValueLen = (uint)pinValue.Length;
            }

            CKR rv = _p11.C_InitPIN(_sessionId, pinValue, pinValueLen);
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

            byte[] pinValue = null;
            uint pinValueLen = 0;
            if (userPin != null)
            {
                pinValue = userPin;
                pinValueLen = (uint)userPin.Length;
            }
            
            CKR rv = _p11.C_InitPIN(_sessionId, pinValue, pinValueLen);
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

            byte[] oldPinValue = null;
            uint oldPinValueLen = 0;
            if (oldPin != null)
            {
                oldPinValue = ConvertUtils.Utf8StringToBytes(oldPin);
                oldPinValueLen = (uint)oldPinValue.Length;
            }

            byte[] newPinValue = null;
            uint newPinValueLen = 0;
            if (newPin != null)
            {
                newPinValue = ConvertUtils.Utf8StringToBytes(newPin);
                newPinValueLen = (uint)newPinValue.Length;
            }

            CKR rv = _p11.C_SetPIN(_sessionId, oldPinValue, oldPinValueLen, newPinValue, newPinValueLen);
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

            byte[] oldPinValue = null;
            uint oldPinValueLen = 0;
            if (oldPin != null)
            {
                oldPinValue = oldPin;
                oldPinValueLen = (uint)oldPin.Length;
            }
            
            byte[] newPinValue = null;
            uint newPinValueLen = 0;
            if (newPin != null)
            {
                newPinValue = newPin;
                newPinValueLen = (uint)newPin.Length;
            }
            
            CKR rv = _p11.C_SetPIN(_sessionId, oldPinValue, oldPinValueLen, newPinValue, newPinValueLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetPIN", rv);
        }

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <returns>Information about a session</returns>
        public SessionInfo GetSessionInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            LowLevelAPI.CK_SESSION_INFO sessionInfo = new LowLevelAPI.CK_SESSION_INFO();
            CKR rv = _p11.C_GetSessionInfo(_sessionId, ref sessionInfo);
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

            uint operationStateLen = 0;
            CKR rv = _p11.C_GetOperationState(_sessionId, null, ref operationStateLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetOperationState", rv);

            byte[] operationState = new byte[operationStateLen];
            rv = _p11.C_GetOperationState(_sessionId, operationState, ref operationStateLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetOperationState", rv);

            return operationState;
        }

        /// <summary>
        /// Restores the cryptographic operations state of a session from an array of bytes obtained with GetOperationState
        /// </summary>
        /// <param name="state">Array of bytes obtained with GetOperationState</param>
        /// <param name="encryptionKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing encryption or decryption operation in the restored session</param>
        /// <param name="authenticationKey">CK_INVALID_HANDLE of handle to the key which will be used for an ongoing signature, MACing, or verification operation in the restored session</param>
        public void SetOperationState(byte[] state, ObjectHandle encryptionKey, ObjectHandle authenticationKey)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            CKR rv = _p11.C_SetOperationState(_sessionId, state, (uint)state.Length, encryptionKey.ObjectId, authenticationKey.ObjectId);
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

            byte[] pinValue = null;
            uint pinValueLen = 0;
            if (pin != null)
            {
                pinValue = ConvertUtils.Utf8StringToBytes(pin);
                pinValueLen = (uint)pinValue.Length;
            }

            CKR rv = _p11.C_Login(_sessionId, userType, pinValue, pinValueLen);
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

            byte[] pinValue = null;
            uint pinValueLen = 0;
            if (pin != null)
            {
                pinValue = pin;
                pinValueLen = (uint)pin.Length;
            }
            
            CKR rv = _p11.C_Login(_sessionId, userType, pinValue, pinValueLen);
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

            CKR rv = _p11.C_Logout(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Logout", rv);
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="attributes">Object attributes</param>
        /// <returns>Handle of created object</returns>
        public ObjectHandle CreateObject(List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint objectId = CK.CK_INVALID_HANDLE;

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = (uint)attributes.Count;
                template = new LowLevelAPI.CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < templateLength; i++)
                    template[i] = attributes[i].CkAttribute;
            }

            CKR rv = _p11.C_CreateObject(_sessionId, template, templateLength, ref objectId);
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
        public ObjectHandle CopyObject(ObjectHandle objectHandle, List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint objectId = CK.CK_INVALID_HANDLE;

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLength = 0;

            if (attributes != null)
            {
                templateLength = (uint)attributes.Count;
                template = new LowLevelAPI.CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < templateLength; i++)
                    template[i] = attributes[i].CkAttribute;
            }

            CKR rv = _p11.C_CopyObject(_sessionId, objectHandle.ObjectId, template, templateLength, ref objectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CopyObject", rv);

            return new ObjectHandle(objectId);
        }

        /// <summary>
        /// Destroys an object
        /// </summary>
        /// <param name="objectHandle">Handle of object to be destroyed</param>
        public void DestroyObject(ObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            CKR rv = _p11.C_DestroyObject(_sessionId, objectHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DestroyObject", rv);
        }

        /// <summary>
        /// Gets the size of an object in bytes.
        /// </summary>
        /// <param name="objectHandle">Handle of object</param>
        /// <returns>Size of an object in bytes</returns>
        public uint GetObjectSize(ObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint objectSize = 0;
            CKR rv = _p11.C_GetObjectSize(_sessionId, objectHandle.ObjectId, ref objectSize);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetObjectSize", rv);

            return objectSize;
        }

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        public List<ObjectAttribute> GetAttributeValue(ObjectHandle objectHandle, List<CKA> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            // Prepare array of CK_ATTRIBUTEs
            LowLevelAPI.CK_ATTRIBUTE[] template = new LowLevelAPI.CK_ATTRIBUTE[attributes.Count];
            for (int i = 0; i < attributes.Count; i++)
                template[i] = LowLevelAPI.CkaUtils.CreateAttribute(attributes[i]);

            // Determine size of attribute values
            CKR rv = _p11.C_GetAttributeValue(_sessionId, objectHandle.ObjectId, template, (uint)template.Length);
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_ATTRIBUTE_SENSITIVE) && (rv != CKR.CKR_ATTRIBUTE_TYPE_INVALID))
                throw new Pkcs11Exception("C_GetAttributeValue", rv);

            // Allocate memory for each attribute
            for (int i = 0; i < template.Length; i++)
            {
                if ((int)template[i].valueLen != -1)
                    template[i].value = LowLevelAPI.UnmanagedMemory.Allocate((int)template[i].valueLen);
            }

            // TODO - Add support for CKA.CKA_WRAP_TEMPLATE and CKA.CKA_UNWRAP_TEMPLATE (CKF_ARRAY_ATTRIBUTE)

            // Read values of attributes
            _p11.C_GetAttributeValue(_sessionId, objectHandle.ObjectId, template, (uint)template.Length);
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_ATTRIBUTE_SENSITIVE) && (rv != CKR.CKR_ATTRIBUTE_TYPE_INVALID))
                throw new Pkcs11Exception("C_GetAttributeValue", rv);

            // Convert CK_ATTRIBUTEs to ObjectAttributes
            List<ObjectAttribute> outAttributes = new List<ObjectAttribute>();
            for (int i = 0; i < template.Length; i++)
                outAttributes.Add(new ObjectAttribute(template[i]));

            return outAttributes;
        }

        /// <summary>
        /// Modifies the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be modified</param>
        /// <param name="attributes">List of attributes that should be modified</param>
        public void SetAttributeValue(ObjectHandle objectHandle, List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (attributes == null)
                throw new ArgumentNullException("attributes");
            
            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            LowLevelAPI.CK_ATTRIBUTE[] template = new LowLevelAPI.CK_ATTRIBUTE[attributes.Count];
            for (int i = 0; i < attributes.Count; i++)
                template[i] = attributes[i].CkAttribute;

            CKR rv = _p11.C_SetAttributeValue(_sessionId, objectHandle.ObjectId, template, (uint)template.Length);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SetAttributeValue", rv);
        }

        /// <summary>
        /// Initializes a search for token and session objects that match a attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        public void FindObjectsInit(List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = (uint)attributes.Count;
                template = new LowLevelAPI.CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < templateLength; i++)
                    template[i] = attributes[i].CkAttribute;
            }

            CKR rv = _p11.C_FindObjectsInit(_sessionId, template, templateLength);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsInit", rv);
        }

        /// <summary>
        /// Continues a search for token and session objects that match a template, obtaining additional object handles
        /// </summary>
        /// <param name="objectCount">Maximum number of object handles to be returned</param>
        /// <returns>Found object handles</returns>
        public List<ObjectHandle> FindObjects(int objectCount)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            List<ObjectHandle> foundObjects = new List<ObjectHandle>();

            uint[] objects = new uint[objectCount];
            uint foundObjectsCount = 0;
            CKR rv = _p11.C_FindObjects(_sessionId, objects, (uint)objectCount, ref foundObjectsCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjects", rv);

            for (int i = 0; i < foundObjectsCount; i++)
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

            CKR rv = _p11.C_FindObjectsFinal(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsFinal", rv);
        }

        /// <summary>
        /// Searches for all token and session objects that match provided attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        /// <returns>Handles of found objects</returns>
        public List<ObjectHandle> FindAllObjects(List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            List<ObjectHandle> foundObjects = new List<ObjectHandle>();

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = (uint)attributes.Count;
                template = new LowLevelAPI.CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < templateLength; i++)
                    template[i] = attributes[i].CkAttribute;
            }

            CKR rv = _p11.C_FindObjectsInit(_sessionId, template, templateLength);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_FindObjectsInit", rv);

            uint objectsLength = 256;
            uint[] objects = new uint[objectsLength];
            uint objectCount = objectsLength;
            while (objectCount == objectsLength)
            {
                rv = _p11.C_FindObjects(_sessionId, objects, objectsLength, ref objectCount);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_FindObjects", rv);

                for (int i = 0; i < objectCount; i++)
                    foundObjects.Add(new ObjectHandle(objects[i]));
            }

            rv = _p11.C_FindObjectsFinal(_sessionId);
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
        public byte[] Encrypt(Mechanism mechanism, ObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            if (data == null)
                throw new ArgumentNullException("data");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_EncryptInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            uint encryptedDataLen = 0;
            rv = _p11.C_Encrypt(_sessionId, data, (uint)data.Length, null, ref encryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Encrypt", rv);

            byte[] encryptedData = new byte[encryptedDataLen];
            rv = _p11.C_Encrypt(_sessionId, data, (uint)data.Length, encryptedData, ref encryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Encrypt", rv);

            if (encryptedData.Length != encryptedDataLen)
                Array.Resize(ref encryptedData, (int)encryptedDataLen);

            return encryptedData;
        }

        /// <summary>
        /// Encrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be encrypted should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        public void Encrypt(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public void Encrypt(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_EncryptInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            uint encryptedPartLen = (uint)encryptedPart.Length;
            
            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = (uint)encryptedPart.Length;
                rv = _p11.C_EncryptUpdate(_sessionId, part, (uint)bytesRead, encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_EncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, (int)encryptedPartLen);
            }

            byte[] lastEncryptedPart = null;
            uint lastEncryptedPartLen = 0;
            rv = _p11.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _p11.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, (int)lastEncryptedPartLen);
        }

        /// <summary>
        /// Decrypts single-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="encryptedData">Data to be decrypted</param>
        /// <returns>Decrypted data</returns>
        public byte[] Decrypt(Mechanism mechanism, ObjectHandle keyHandle, byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (encryptedData == null)
                throw new ArgumentNullException("encryptedData");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_DecryptInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            uint decryptedDataLen = 0;
            rv = _p11.C_Decrypt(_sessionId, encryptedData, (uint)encryptedData.Length, null, ref decryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Decrypt", rv);

            byte[] decryptedData = new byte[decryptedDataLen];
            rv = _p11.C_Decrypt(_sessionId, encryptedData, (uint)encryptedData.Length, decryptedData, ref decryptedDataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Decrypt", rv);

            if (decryptedData.Length != decryptedDataLen)
                Array.Resize(ref decryptedData, (int)decryptedDataLen);

            return decryptedData;
        }

        /// <summary>
        /// Decrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which encrypted data should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        public void Decrypt(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public void Decrypt(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_DecryptInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            uint partLen = (uint)part.Length;

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = (uint)part.Length;
                rv = _p11.C_DecryptUpdate(_sessionId, encryptedPart, (uint)bytesRead, part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptUpdate", rv);

                outputStream.Write(part, 0, (int)partLen);
            }

            byte[] lastPart = null;
            uint lastPartLen = 0;
            rv = _p11.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _p11.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, (int)lastPartLen);
        }

        /// <summary>
        /// Digests the value of a secret key
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="keyHandle">Handle of the secret key to be digested</param>
        /// <returns>Digest</returns>
        public byte[] DigestKey(Mechanism mechanism, ObjectHandle keyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;
            
            CKR rv = _p11.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);
            
            rv = _p11.C_DigestKey(_sessionId, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestKey", rv);
            
            uint digestLen = 0;
            rv = _p11.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);
            
            byte[] digest = new byte[digestLen];
            rv = _p11.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != digestLen)
                Array.Resize(ref digest, (int)digestLen);

            return digest;
        }

        /// <summary>
        /// Digests single-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="data">Data to be digested</param>
        /// <returns>Digest</returns>
        public byte[] Digest(Mechanism mechanism, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (data == null)
                throw new ArgumentNullException("data");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            uint digestLen = 0;
            rv = _p11.C_Digest(_sessionId, data, (uint)data.Length, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Digest", rv);

            byte[] digest = new byte[digestLen];
            rv = _p11.C_Digest(_sessionId, data, (uint)data.Length, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Digest", rv);

            if (digest.Length != digestLen)
                Array.Resize(ref digest, (int)digestLen);

            return digest;
        }

        /// <summary>
        /// Digests multi-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Digest</returns>
        public byte[] Digest(Mechanism mechanism, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public byte[] Digest(Mechanism mechanism, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_DigestInit(_sessionId, ref ckMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _p11.C_DigestUpdate(_sessionId, part, (uint)bytesRead);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DigestUpdate", rv);
            }

            uint digestLen = 0;
            rv = _p11.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _p11.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != digestLen)
                Array.Resize(ref digest, (int)digestLen);

            return digest;
        }

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] Sign(Mechanism mechanism, ObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_SignInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            uint signatureLen = 0;
            rv = _p11.C_Sign(_sessionId, data, (uint)data.Length, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Sign", rv);

            byte[] signature = new byte[signatureLen];
            rv = _p11.C_Sign(_sessionId, data, (uint)data.Length, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_Sign", rv);

            if (signature.Length != signatureLen)
                Array.Resize(ref signature, (int)signatureLen);

            return signature;
        }

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        public byte[] Sign(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] Sign(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (inputStream == null)
                throw new ArgumentNullException("inputStream");

            if (bufferLength < 1)
                throw new ArgumentException("Value has to be positive number", "bufferLength");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_SignInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _p11.C_SignUpdate(_sessionId, part, (uint)bytesRead);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_SignUpdate", rv);
            }

            uint signatureLen = 0;
            rv = _p11.C_SignFinal(_sessionId, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            byte[] signature = new byte[signatureLen];
            rv = _p11.C_SignFinal(_sessionId, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            if (signature.Length != signatureLen)
                Array.Resize(ref signature, (int)signatureLen);

            return signature;
        }

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        public byte[] SignRecover(Mechanism mechanism, ObjectHandle keyHandle, byte[] data)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_SignRecoverInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecoverInit", rv);

            uint signatureLen = 0;
            rv = _p11.C_SignRecover(_sessionId, data, (uint)data.Length, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecover", rv);

            byte[] signature = new byte[signatureLen];
            rv = _p11.C_SignRecover(_sessionId, data, (uint)data.Length, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignRecover", rv);

            if (signature.Length != signatureLen)
                Array.Resize(ref signature, (int)signatureLen);

            return signature;
        }

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="data">Data that was signed</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        public void Verify(Mechanism mechanism, ObjectHandle keyHandle, byte[] data, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (data == null)
                throw new ArgumentNullException("data");

            if (signature == null)
                throw new ArgumentNullException("signature");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_VerifyInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            rv = _p11.C_Verify(_sessionId, data, (uint)data.Length, signature, (uint)signature.Length);
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
        public void Verify(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public void Verify(Mechanism mechanism, ObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_VerifyInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            byte[] part = new byte[bufferLength];
            int bytesRead = 0;

            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                rv = _p11.C_VerifyUpdate(_sessionId, part, (uint)bytesRead);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_VerifyUpdate", rv);
            }

            rv = _p11.C_VerifyFinal(_sessionId, signature, (uint)signature.Length);
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
        public byte[] VerifyRecover(Mechanism mechanism, ObjectHandle keyHandle, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");
            
            if (signature == null)
                throw new ArgumentNullException("signature");
            
            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            CKR rv = _p11.C_VerifyRecoverInit(_sessionId, ref ckMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyRecoverInit", rv);

            uint dataLen = 0;
            rv = _p11.C_VerifyRecover(_sessionId, signature, (uint)signature.Length, null, ref dataLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyRecover", rv);

            byte[] data = new byte[dataLen];
            rv = _p11.C_VerifyRecover(_sessionId, signature, (uint)signature.Length, data, ref dataLen);
            if (rv == CKR.CKR_OK)
                isValid = true;
            else if (rv == CKR.CKR_SIGNATURE_INVALID)
                isValid = false;
            else 
                throw new Pkcs11Exception("C_VerifyRecover", rv);

            if (data.Length != dataLen)
                Array.Resize(ref data, (int)dataLen);

            return data;
        }

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="encryptedData">Encrypted data</param>
        /// <param name="digest">Digest</param>
        public void DigestEncrypt(Mechanism digestingMechanism, Mechanism encryptionMechanism, ObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public byte[] DigestEncrypt(Mechanism digestingMechanism, Mechanism encryptionMechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public byte[] DigestEncrypt(Mechanism digestingMechanism, Mechanism encryptionMechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckDigestingMechanism = digestingMechanism.CkMechanism;

            CKR rv = _p11.C_DigestInit(_sessionId, ref ckDigestingMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            LowLevelAPI.CK_MECHANISM ckEncryptionMechanism = encryptionMechanism.CkMechanism;

            rv = _p11.C_EncryptInit(_sessionId, ref ckEncryptionMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            uint encryptedPartLen = (uint)encryptedPart.Length;

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = (uint)encryptedPart.Length;
                rv = _p11.C_DigestEncryptUpdate(_sessionId, part, (uint)bytesRead, encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DigestEncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, (int)encryptedPartLen);
            }

            byte[] lastEncryptedPart = null;
            uint lastEncryptedPartLen = 0;
            rv = _p11.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _p11.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, (int)lastEncryptedPartLen);

            uint digestLen = 0;
            rv = _p11.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _p11.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != digestLen)
                Array.Resize(ref digest, (int)digestLen);

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
        public void DecryptDigest(Mechanism digestingMechanism, Mechanism decryptionMechanism, ObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] decryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public byte[] DecryptDigest(Mechanism digestingMechanism, Mechanism decryptionMechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public byte[] DecryptDigest(Mechanism digestingMechanism, Mechanism decryptionMechanism, ObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckDigestingMechanism = digestingMechanism.CkMechanism;

            CKR rv = _p11.C_DigestInit(_sessionId, ref ckDigestingMechanism);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestInit", rv);

            LowLevelAPI.CK_MECHANISM ckDecryptionMechanism = decryptionMechanism.CkMechanism;

            rv = _p11.C_DecryptInit(_sessionId, ref ckDecryptionMechanism, keyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            uint partLen = (uint)part.Length;

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = (uint)part.Length;
                rv = _p11.C_DecryptDigestUpdate(_sessionId, encryptedPart, (uint)bytesRead, part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptDigestUpdate", rv);

                outputStream.Write(part, 0, (int)partLen);
            }

            byte[] lastPart = null;
            uint lastPartLen = 0;
            rv = _p11.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _p11.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, (int)lastPartLen);

            uint digestLen = 0;
            rv = _p11.C_DigestFinal(_sessionId, null, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            byte[] digest = new byte[digestLen];
            rv = _p11.C_DigestFinal(_sessionId, digest, ref digestLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DigestFinal", rv);

            if (digest.Length != digestLen)
                Array.Resize(ref digest, (int)digestLen);

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
        public void SignEncrypt(Mechanism signingMechanism, ObjectHandle signingKeyHandle, Mechanism encryptionMechanism, ObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(Mechanism signingMechanism, ObjectHandle signingKeyHandle, Mechanism encryptionMechanism, ObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="encryptionKeyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        public byte[] SignEncrypt(Mechanism signingMechanism, ObjectHandle signingKeyHandle, Mechanism encryptionMechanism, ObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckSigningMechanism = signingMechanism.CkMechanism;

            CKR rv = _p11.C_SignInit(_sessionId, ref ckSigningMechanism, signingKeyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignInit", rv);

            LowLevelAPI.CK_MECHANISM ckEncryptionMechanism = encryptionMechanism.CkMechanism;

            rv = _p11.C_EncryptInit(_sessionId, ref ckEncryptionMechanism, encryptionKeyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptInit", rv);

            byte[] part = new byte[bufferLength];
            byte[] encryptedPart = new byte[bufferLength];
            uint encryptedPartLen = (uint)encryptedPart.Length;

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
            {
                encryptedPartLen = (uint)encryptedPart.Length;
                rv = _p11.C_SignEncryptUpdate(_sessionId, part, (uint)bytesRead, encryptedPart, ref encryptedPartLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_SignEncryptUpdate", rv);

                outputStream.Write(encryptedPart, 0, (int)encryptedPartLen);
            }

            byte[] lastEncryptedPart = null;
            uint lastEncryptedPartLen = 0;
            rv = _p11.C_EncryptFinal(_sessionId, null, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            lastEncryptedPart = new byte[lastEncryptedPartLen];
            rv = _p11.C_EncryptFinal(_sessionId, lastEncryptedPart, ref lastEncryptedPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_EncryptFinal", rv);

            if (lastEncryptedPartLen > 0)
                outputStream.Write(lastEncryptedPart, 0, (int)lastEncryptedPartLen);

            uint signatureLen = 0;
            rv = _p11.C_SignFinal(_sessionId, null, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            byte[] signature = new byte[signatureLen];
            rv = _p11.C_SignFinal(_sessionId, signature, ref signatureLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_SignFinal", rv);

            if (signature.Length != signatureLen)
                Array.Resize(ref signature, (int)signatureLen);

            return signature;
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
        public void DecryptVerify(Mechanism verificationMechanism, ObjectHandle verificationKeyHandle, Mechanism decryptionMechanism, ObjectHandle decryptionKeyHandle, byte[] data, byte[] signature, out byte[] decryptedData, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public void DecryptVerify(Mechanism verificationMechanism, ObjectHandle verificationKeyHandle, Mechanism decryptionMechanism, ObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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
        public void DecryptVerify(Mechanism verificationMechanism, ObjectHandle verificationKeyHandle, Mechanism decryptionMechanism, ObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid, int bufferLength)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

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

            LowLevelAPI.CK_MECHANISM ckVerificationMechanism = verificationMechanism.CkMechanism;

            CKR rv = _p11.C_VerifyInit(_sessionId, ref ckVerificationMechanism, verificationKeyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_VerifyInit", rv);

            LowLevelAPI.CK_MECHANISM ckDecryptionMechanism = decryptionMechanism.CkMechanism;

            rv = _p11.C_DecryptInit(_sessionId, ref ckDecryptionMechanism, decryptionKeyHandle.ObjectId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptInit", rv);

            byte[] encryptedPart = new byte[bufferLength];
            byte[] part = new byte[bufferLength];
            uint partLen = (uint)part.Length;

            int bytesRead = 0;
            while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
            {
                partLen = (uint)part.Length;
                rv = _p11.C_DecryptVerifyUpdate(_sessionId, encryptedPart, (uint)bytesRead, part, ref partLen);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_DecryptVerifyUpdate", rv);

                outputStream.Write(part, 0, (int)partLen);
            }

            byte[] lastPart = null;
            uint lastPartLen = 0;
            rv = _p11.C_DecryptFinal(_sessionId, null, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            lastPart = new byte[lastPartLen];
            rv = _p11.C_DecryptFinal(_sessionId, lastPart, ref lastPartLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_DecryptFinal", rv);

            if (lastPartLen > 0)
                outputStream.Write(lastPart, 0, (int)lastPartLen);

            rv = _p11.C_VerifyFinal(_sessionId, signature, (uint)signature.Length);
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
        public ObjectHandle GenerateKey(Mechanism mechanism, List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLength = 0;
            
            if (attributes != null)
            {
                templateLength = (uint)attributes.Count;
                template = new LowLevelAPI.CK_ATTRIBUTE[templateLength];
                for (int i = 0; i < templateLength; i++)
                    template[i] = attributes[i].CkAttribute;
            }

            uint keyId = CK.CK_INVALID_HANDLE;
            CKR rv = _p11.C_GenerateKey(_sessionId, ref ckMechanism, template, templateLength, ref keyId);
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
        public void GenerateKeyPair(Mechanism mechanism, List<ObjectAttribute> publicKeyAttributes, List<ObjectAttribute> privateKeyAttributes, out ObjectHandle publicKeyHandle, out ObjectHandle privateKeyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            LowLevelAPI.CK_ATTRIBUTE[] publicKeyTemplate = null;
            uint publicKeyTemplateLength = 0;
            
            if (publicKeyAttributes != null)
            {
                publicKeyTemplateLength = (uint)publicKeyAttributes.Count;
                publicKeyTemplate = new LowLevelAPI.CK_ATTRIBUTE[publicKeyTemplateLength];
                for (int i = 0; i < publicKeyTemplateLength; i++)
                    publicKeyTemplate[i] = publicKeyAttributes[i].CkAttribute;
            }

            LowLevelAPI.CK_ATTRIBUTE[] privateKeyTemplate = null;
            uint privateKeyTemplateLength = 0;
            
            if (privateKeyAttributes != null)
            {
                privateKeyTemplateLength = (uint)privateKeyAttributes.Count;
                privateKeyTemplate = new LowLevelAPI.CK_ATTRIBUTE[privateKeyTemplateLength];
                for (int i = 0; i < privateKeyTemplateLength; i++)
                    privateKeyTemplate[i] = privateKeyAttributes[i].CkAttribute;
            }

            uint publicKeyId = CK.CK_INVALID_HANDLE;
            uint privateKeyId = CK.CK_INVALID_HANDLE;
            CKR rv = _p11.C_GenerateKeyPair(_sessionId, ref ckMechanism, publicKeyTemplate, publicKeyTemplateLength, privateKeyTemplate, privateKeyTemplateLength, ref publicKeyId, ref privateKeyId);
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
        public byte[] WrapKey(Mechanism mechanism, ObjectHandle wrappingKeyHandle, ObjectHandle keyHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (wrappingKeyHandle == null)
                throw new ArgumentNullException("wrappingKeyHandle");
            
            if (keyHandle == null)
                throw new ArgumentNullException("keyHandle");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            uint wrappedKeyLen = 0;
            CKR rv = _p11.C_WrapKey(_sessionId, ref ckMechanism, wrappingKeyHandle.ObjectId, keyHandle.ObjectId, null, ref wrappedKeyLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_WrapKey", rv);

            byte[] wrappedKey = new byte[wrappedKeyLen];
            rv = _p11.C_WrapKey(_sessionId, ref ckMechanism, wrappingKeyHandle.ObjectId, keyHandle.ObjectId, wrappedKey, ref wrappedKeyLen);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_WrapKey", rv);

            if (wrappedKey.Length != wrappedKeyLen)
                Array.Resize(ref wrappedKey, (int)wrappedKeyLen);

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
        public ObjectHandle UnwrapKey(Mechanism mechanism, ObjectHandle unwrappingKeyHandle, byte[] wrappedKey, List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (unwrappingKeyHandle == null)
                throw new ArgumentNullException("unwrappingKeyHandle");
            
            if (wrappedKey == null)
                throw new ArgumentNullException("wrappedKey");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLen = 0;
            if (attributes != null)
            {
                template = new LowLevelAPI.CK_ATTRIBUTE[attributes.Count];
                for (int i = 0; i < attributes.Count; i++)
                    template[i] = attributes[i].CkAttribute;
                templateLen = (uint)attributes.Count;
            }

            uint unwrappedKey = CK.CK_INVALID_HANDLE;
            CKR rv = _p11.C_UnwrapKey(_sessionId, ref ckMechanism, unwrappingKeyHandle.ObjectId, wrappedKey, (uint)wrappedKey.Length, template, templateLen, ref unwrappedKey);
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
        public ObjectHandle DeriveKey(Mechanism mechanism, ObjectHandle baseKeyHandle, List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (mechanism == null)
                throw new ArgumentNullException("mechanism");
            
            if (baseKeyHandle == null)
                throw new ArgumentNullException("baseKeyHandle");

            LowLevelAPI.CK_MECHANISM ckMechanism = mechanism.CkMechanism;

            LowLevelAPI.CK_ATTRIBUTE[] template = null;
            uint templateLen = 0;
            if (attributes != null)
            {
                template = new LowLevelAPI.CK_ATTRIBUTE[attributes.Count];
                for (int i = 0; i < attributes.Count; i++)
                    template[i] = attributes[i].CkAttribute;
                templateLen = (uint)attributes.Count;
            }

            uint derivedKey = CK.CK_INVALID_HANDLE;
            CKR rv = _p11.C_DeriveKey(_sessionId, ref ckMechanism, baseKeyHandle.ObjectId, template, templateLen, ref derivedKey);
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

            if (seed == null)
                throw new ArgumentNullException("seed");

            CKR rv = _p11.C_SeedRandom(_sessionId, seed, (uint)seed.Length);
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

            if (length < 1)
                throw new ArgumentException("Value has to be positive number", "length");

            byte[] randomData = new byte[length];
            CKR rv = _p11.C_GenerateRandom(_sessionId, randomData, (uint)length);
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

            CKR rv = _p11.C_GetFunctionStatus(_sessionId);
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

            CKR rv = _p11.C_CancelFunction(_sessionId);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_CancelFunction", rv);
        }

        #region IDisposable

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                    if (_sessionId != CK.CK_INVALID_HANDLE)
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
