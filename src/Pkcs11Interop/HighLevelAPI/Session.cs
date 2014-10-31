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
using System.IO;
using Net.Pkcs11Interop.Common;

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
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI4.Session _session4 = null;

        /// <summary>
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI8.Session _session8 = null;

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return (UnmanagedLong.Size == 4) ? _session4.SessionId : _session8.SessionId;
            }
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI4.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session4 = session;
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI8.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session8 = session;
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        public void CloseSession()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.CloseSession();
            else
                _session8.CloseSession();
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(string userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.InitPin(userPin);
            else
                _session8.InitPin(userPin);
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(byte[] userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.InitPin(userPin);
            else
                _session8.InitPin(userPin);
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

            if (UnmanagedLong.Size == 4)
                _session4.SetPin(oldPin, newPin);
            else
                _session8.SetPin(oldPin, newPin);
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

            if (UnmanagedLong.Size == 4)
                _session4.SetPin(oldPin, newPin);
            else
                _session8.SetPin(oldPin, newPin);
        }

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <returns>Information about a session</returns>
        public SessionInfo GetSessionInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.SessionInfo hlaSessionInfo = _session4.GetSessionInfo();
                return new SessionInfo(hlaSessionInfo);
            }
            else
            {
                HighLevelAPI8.SessionInfo hlaSessionInfo = _session8.GetSessionInfo();
                return new SessionInfo(hlaSessionInfo);
            }
        }

        /// <summary>
        /// Obtains a copy of the cryptographic operations state of a session encoded as an array of bytes
        /// </summary>
        /// <returns>Operations state of a session</returns>
        public byte[] GetOperationState()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _session4.GetOperationState();
            else
                return _session8.GetOperationState();
        }

        /// <summary>
        /// Restores the cryptographic operations state of a session from an array of bytes obtained with GetOperationState
        /// </summary>
        /// <param name="state">Array of bytes obtained with GetOperationState</param>
        /// <param name="encryptionKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing encryption or decryption operation in the restored session</param>
        /// <param name="authenticationKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing signature, MACing, or verification operation in the restored session</param>
        public void SetOperationState(byte[] state, ObjectHandle encryptionKey, ObjectHandle authenticationKey)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (state == null)
                throw new ArgumentNullException("state");

            if (encryptionKey == null)
                throw new ArgumentNullException("encryptionKey");

            if (authenticationKey == null)
                throw new ArgumentNullException("authenticationKey");

            if (UnmanagedLong.Size == 4)
                _session4.SetOperationState(state, encryptionKey.ObjectHandle4, authenticationKey.ObjectHandle4);
            else
                _session8.SetOperationState(state, encryptionKey.ObjectHandle8, authenticationKey.ObjectHandle8);
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

            if (UnmanagedLong.Size == 4)
                _session4.Login(userType, pin);
            else
                _session8.Login(userType, pin);
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

            if (UnmanagedLong.Size == 4)
                _session4.Login(userType, pin);
            else
                _session8.Login(userType, pin);
        }

        /// <summary>
        /// Logs a user out from a token
        /// </summary>
        public void Logout()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.Logout();
            else
                _session8.Logout();
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                HighLevelAPI4.ObjectHandle hlaObjectHandle = _session4.CreateObject(hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                HighLevelAPI8.ObjectHandle hlaObjectHandle = _session8.CreateObject(hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
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

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                HighLevelAPI4.ObjectHandle hlaObjectHandle = _session4.CopyObject(objectHandle.ObjectHandle4, hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                HighLevelAPI8.ObjectHandle hlaObjectHandle = _session8.CopyObject(objectHandle.ObjectHandle8, hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
        }

        /// <summary>
        /// Destroys an object
        /// </summary>
        /// <param name="objectHandle">Handle of object to be destroyed</param>
        public void DestroyObject(ObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (UnmanagedLong.Size == 4)
                _session4.DestroyObject(objectHandle.ObjectHandle4);
            else
                _session8.DestroyObject(objectHandle.ObjectHandle8);
        }

        /// <summary>
        /// Gets the size of an object in bytes.
        /// </summary>
        /// <param name="objectHandle">Handle of object</param>
        /// <returns>Size of an object in bytes</returns>
        public ulong GetObjectSize(ObjectHandle objectHandle)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (UnmanagedLong.Size == 4)
                return _session4.GetObjectSize(objectHandle.ObjectHandle4);
            else
                return _session8.GetObjectSize(objectHandle.ObjectHandle8);
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

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            List<ulong> ulongAttributes = new List<ulong>();
            foreach (CKA attribute in attributes)
                ulongAttributes.Add(Convert.ToUInt64((uint)attribute));

            return GetAttributeValue(objectHandle, ulongAttributes);
        }

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        public List<ObjectAttribute> GetAttributeValue(ObjectHandle objectHandle, List<ulong> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            if (UnmanagedLong.Size == 4)
            {
                List<uint> uintList = new List<uint>();
                for (int i = 0; i < attributes.Count; i++)
                    uintList.Add(Convert.ToUInt32(attributes[i]));

                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = _session4.GetAttributeValue(objectHandle.ObjectHandle4, uintList);
                return ObjectAttribute.ConvertFromHighLevelAPI4List(hlaAttributes);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = _session8.GetAttributeValue(objectHandle.ObjectHandle8, attributes);
                return ObjectAttribute.ConvertFromHighLevelAPI8List(hlaAttributes);
            }
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

            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            if (attributes == null)
                throw new ArgumentNullException("attributes");
            
            if (attributes.Count < 1)
                throw new ArgumentException("No attributes specified", "attributes");

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                _session4.SetAttributeValue(objectHandle.ObjectHandle4, hlaAttributes);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                _session8.SetAttributeValue(objectHandle.ObjectHandle8, hlaAttributes);
            }
        }

        /// <summary>
        /// Initializes a search for token and session objects that match a attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        public void FindObjectsInit(List<ObjectAttribute> attributes)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                _session4.FindObjectsInit(hlaAttributes);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                _session8.FindObjectsInit(hlaAttributes);
            }
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectHandle> hlaObjectHandles = _session4.FindObjects(objectCount);
                return ObjectHandle.ConvertFromHighLevelAPI4List(hlaObjectHandles);
            }
            else
            {
                List<HighLevelAPI8.ObjectHandle> hlaObjectHandles = _session8.FindObjects(objectCount);
                return ObjectHandle.ConvertFromHighLevelAPI8List(hlaObjectHandles);
            }
        }

        /// <summary>
        /// Terminates a search for token and session objects
        /// </summary>
        public void FindObjectsFinal()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.FindObjectsFinal();
            else
                _session8.FindObjectsFinal();
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                List<HighLevelAPI4.ObjectHandle> hlaObjectHandles = _session4.FindAllObjects(hlaObjectAttributes);
                return ObjectHandle.ConvertFromHighLevelAPI4List(hlaObjectHandles);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                List<HighLevelAPI8.ObjectHandle> hlaObjectHandles = _session8.FindAllObjects(hlaObjectAttributes);
                return ObjectHandle.ConvertFromHighLevelAPI8List(hlaObjectHandles);
            }
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

            if (UnmanagedLong.Size == 4)
                return _session4.Encrypt(mechanism.Mechanism4, keyHandle.ObjectHandle4, data);
            else
                return _session8.Encrypt(mechanism.Mechanism8, keyHandle.ObjectHandle8, data);
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

            if (UnmanagedLong.Size == 4)
                _session4.Encrypt(mechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, outputStream, bufferLength);
            else
                _session8.Encrypt(mechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, outputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.Decrypt(mechanism.Mechanism4, keyHandle.ObjectHandle4, encryptedData);
            else
                return _session8.Decrypt(mechanism.Mechanism8, keyHandle.ObjectHandle8, encryptedData);
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

            if (UnmanagedLong.Size == 4)
                _session4.Decrypt(mechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, outputStream, bufferLength);
            else
                _session8.Decrypt(mechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, outputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.DigestKey(mechanism.Mechanism4, keyHandle.ObjectHandle4);
            else
                return _session8.DigestKey(mechanism.Mechanism8, keyHandle.ObjectHandle8);
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

            if (UnmanagedLong.Size == 4)
                return _session4.Digest(mechanism.Mechanism4, data);
            else
                return _session8.Digest(mechanism.Mechanism8, data);
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

            if (UnmanagedLong.Size == 4)
                return _session4.Digest(mechanism.Mechanism4, inputStream, bufferLength);
            else
                return _session8.Digest(mechanism.Mechanism8, inputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.Sign(mechanism.Mechanism4, keyHandle.ObjectHandle4, data);
            else
                return _session8.Sign(mechanism.Mechanism8, keyHandle.ObjectHandle8, data);
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

            if (UnmanagedLong.Size == 4)
                return _session4.Sign(mechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, bufferLength);
            else
                return _session8.Sign(mechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.SignRecover(mechanism.Mechanism4, keyHandle.ObjectHandle4, data);
            else
                return _session8.SignRecover(mechanism.Mechanism8, keyHandle.ObjectHandle8, data);
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

            if (UnmanagedLong.Size == 4)
                _session4.Verify(mechanism.Mechanism4, keyHandle.ObjectHandle4, data, signature, out isValid);
            else
                _session8.Verify(mechanism.Mechanism8, keyHandle.ObjectHandle8, data, signature, out isValid);
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

            if (UnmanagedLong.Size == 4)
                _session4.Verify(mechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, signature, out isValid, bufferLength);
            else
                _session8.Verify(mechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, signature, out isValid, bufferLength);
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
            
            if (UnmanagedLong.Size == 4)
                return _session4.VerifyRecover(mechanism.Mechanism4, keyHandle.ObjectHandle4, signature, out isValid);
            else
                return _session8.VerifyRecover(mechanism.Mechanism8, keyHandle.ObjectHandle8, signature, out isValid);
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

            if (UnmanagedLong.Size == 4)
                return _session4.DigestEncrypt(digestingMechanism.Mechanism4, encryptionMechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, outputStream, bufferLength);
            else
                return _session8.DigestEncrypt(digestingMechanism.Mechanism8, encryptionMechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, outputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.DecryptDigest(digestingMechanism.Mechanism4, decryptionMechanism.Mechanism4, keyHandle.ObjectHandle4, inputStream, outputStream, bufferLength);
            else
                return _session8.DecryptDigest(digestingMechanism.Mechanism8, decryptionMechanism.Mechanism8, keyHandle.ObjectHandle8, inputStream, outputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                return _session4.SignEncrypt(signingMechanism.Mechanism4, signingKeyHandle.ObjectHandle4, encryptionMechanism.Mechanism4, encryptionKeyHandle.ObjectHandle4, inputStream, outputStream, bufferLength);
            else
                return _session8.SignEncrypt(signingMechanism.Mechanism8, signingKeyHandle.ObjectHandle8, encryptionMechanism.Mechanism8, encryptionKeyHandle.ObjectHandle8, inputStream, outputStream, bufferLength);
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

            if (UnmanagedLong.Size == 4)
                _session4.DecryptVerify(verificationMechanism.Mechanism4, verificationKeyHandle.ObjectHandle4, decryptionMechanism.Mechanism4, decryptionKeyHandle.ObjectHandle4, inputStream, outputStream, signature, out isValid, bufferLength);
            else
                _session8.DecryptVerify(verificationMechanism.Mechanism8, verificationKeyHandle.ObjectHandle8, decryptionMechanism.Mechanism8, decryptionKeyHandle.ObjectHandle8, inputStream, outputStream, signature, out isValid, bufferLength);
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                HighLevelAPI4.ObjectHandle hlaObjectHandle = _session4.GenerateKey(mechanism.Mechanism4, hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                HighLevelAPI8.ObjectHandle hlaObjectHandle = _session8.GenerateKey(mechanism.Mechanism8, hlaAttributes);
                return new ObjectHandle(hlaObjectHandle);
            }
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(publicKeyAttributes);
                List<HighLevelAPI4.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(privateKeyAttributes);

                HighLevelAPI4.ObjectHandle hlaPublicKeyHandle = null;
                HighLevelAPI4.ObjectHandle hlaPrivateKeyHandle = null;

                _session4.GenerateKeyPair(mechanism.Mechanism4, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(publicKeyAttributes);
                List<HighLevelAPI8.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(privateKeyAttributes);

                HighLevelAPI8.ObjectHandle hlaPublicKeyHandle = null;
                HighLevelAPI8.ObjectHandle hlaPrivateKeyHandle = null;

                _session8.GenerateKeyPair(mechanism.Mechanism8, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
            }
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

            if (UnmanagedLong.Size == 4)
                return _session4.WrapKey(mechanism.Mechanism4, wrappingKeyHandle.ObjectHandle4, keyHandle.ObjectHandle4);
            else
                return _session8.WrapKey(mechanism.Mechanism8, wrappingKeyHandle.ObjectHandle8, keyHandle.ObjectHandle8);
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                HighLevelAPI4.ObjectHandle unwrappedKeyHandle = _session4.UnwrapKey(mechanism.Mechanism4, unwrappingKeyHandle.ObjectHandle4, wrappedKey, hlaAttributes);
                return new ObjectHandle(unwrappedKeyHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                HighLevelAPI8.ObjectHandle unwrappedKeyHandle = _session8.UnwrapKey(mechanism.Mechanism8, unwrappingKeyHandle.ObjectHandle8, wrappedKey, hlaAttributes);
                return new ObjectHandle(unwrappedKeyHandle);
            }
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI4List(attributes);
                HighLevelAPI4.ObjectHandle unwrappedKeyHandle = _session4.DeriveKey(mechanism.Mechanism4, baseKeyHandle.ObjectHandle4, hlaAttributes);
                return new ObjectHandle(unwrappedKeyHandle);
            }
            else
            {
                List<HighLevelAPI8.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI8List(attributes);
                HighLevelAPI8.ObjectHandle unwrappedKeyHandle = _session8.DeriveKey(mechanism.Mechanism8, baseKeyHandle.ObjectHandle8, hlaAttributes);
                return new ObjectHandle(unwrappedKeyHandle);
            }
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

            if (UnmanagedLong.Size == 4)
                _session4.SeedRandom(seed);
            else
                _session8.SeedRandom(seed);
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

            if (UnmanagedLong.Size == 4)
                return _session4.GenerateRandom(length);
            else
                return _session8.GenerateRandom(length);
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void GetFunctionStatus()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.GetFunctionStatus();
            else
                _session8.GetFunctionStatus();
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void CancelFunction()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                _session4.CancelFunction();
            else
                _session8.CancelFunction();
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
                    if (_session4 != null)
                    {
                        _session4.Dispose();
                        _session4 = null;
                    }

                    if (_session8 != null)
                    {
                        _session8.Dispose();
                        _session8 = null;
                    }
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
