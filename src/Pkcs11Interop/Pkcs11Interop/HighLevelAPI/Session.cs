/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
        /// Flag indicating whether instance has been disposed
        /// </summary>
        public bool Disposed
        {
            get
            {
                return _disposed;
            }
        }

        /// <summary>
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI40.Session _session40 = null;

        /// <summary>
        /// Platform specific Session. Use with caution!
        /// </summary>
        public HighLevelAPI40.Session HLA40Session
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _session40;
            }
        }

        /// <summary>
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI41.Session _session41 = null;

        /// <summary>
        /// Platform specific Session. Use with caution!
        /// </summary>
        public HighLevelAPI41.Session HLA41Session
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _session41;
            }
        }

        /// <summary>
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI80.Session _session80 = null;

        /// <summary>
        /// Platform specific Session. Use with caution!
        /// </summary>
        public HighLevelAPI80.Session HLA80Session
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _session80;
            }
        }

        /// <summary>
        /// Platform specific Session
        /// </summary>
        private HighLevelAPI81.Session _session81 = null;

        /// <summary>
        /// Platform specific Session. Use with caution!
        /// </summary>
        public HighLevelAPI81.Session HLA81Session
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _session81;
            }
        }

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        public ulong SessionId
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _session40.SessionId : _session41.SessionId;
                else
                    return (Platform.StructPackingSize == 0) ? _session80.SessionId : _session81.SessionId;
            }
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI40.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session40 = session;
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI41.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session41 = session;
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI80.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session80 = session;
        }

        /// <summary>
        /// Converts platform specific Session to platfrom neutral Session
        /// </summary>
        /// <param name="session">Platform specific Session</param>
        internal Session(HighLevelAPI81.Session session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            _session81 = session;
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        public void CloseSession()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.CloseSession();
                else
                    _session41.CloseSession();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.CloseSession();
                else
                    _session81.CloseSession();
            }
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(string userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.InitPin(userPin);
                else
                    _session41.InitPin(userPin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.InitPin(userPin);
                else
                    _session81.InitPin(userPin);
            }
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        public void InitPin(byte[] userPin)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.InitPin(userPin);
                else
                    _session41.InitPin(userPin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.InitPin(userPin);
                else
                    _session81.InitPin(userPin);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.SetPin(oldPin, newPin);
                else
                    _session41.SetPin(oldPin, newPin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.SetPin(oldPin, newPin);
                else
                    _session81.SetPin(oldPin, newPin);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.SetPin(oldPin, newPin);
                else
                    _session41.SetPin(oldPin, newPin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.SetPin(oldPin, newPin);
                else
                    _session81.SetPin(oldPin, newPin);
            }
        }

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <returns>Information about a session</returns>
        public SessionInfo GetSessionInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? new SessionInfo(_session40.GetSessionInfo()) : new SessionInfo(_session41.GetSessionInfo());
            else
                return (Platform.StructPackingSize == 0) ? new SessionInfo(_session80.GetSessionInfo()) : new SessionInfo(_session81.GetSessionInfo());
        }

        /// <summary>
        /// Obtains a copy of the cryptographic operations state of a session encoded as an array of bytes
        /// </summary>
        /// <returns>Operations state of a session</returns>
        public byte[] GetOperationState()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
                return (Platform.StructPackingSize == 0) ? _session40.GetOperationState() : _session41.GetOperationState();
            else
                return (Platform.StructPackingSize == 0) ? _session80.GetOperationState() : _session81.GetOperationState();
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.SetOperationState(state, encryptionKey.ObjectHandle40, authenticationKey.ObjectHandle40);
                else
                    _session41.SetOperationState(state, encryptionKey.ObjectHandle41, authenticationKey.ObjectHandle41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.SetOperationState(state, encryptionKey.ObjectHandle80, authenticationKey.ObjectHandle80);
                else
                    _session81.SetOperationState(state, encryptionKey.ObjectHandle81, authenticationKey.ObjectHandle81);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Login(userType, pin);
                else
                    _session41.Login(userType, pin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Login(userType, pin);
                else
                    _session81.Login(userType, pin);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Login(userType, pin);
                else
                    _session41.Login(userType, pin);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Login(userType, pin);
                else
                    _session81.Login(userType, pin);
            }
        }

        /// <summary>
        /// Logs a user out from a token
        /// </summary>
        public void Logout()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Logout();
                else
                    _session41.Logout();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Logout();
                else
                    _session81.Logout();
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    HighLevelAPI40.ObjectHandle hlaObjectHandle = _session40.CreateObject(hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    HighLevelAPI41.ObjectHandle hlaObjectHandle = _session41.CreateObject(hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    HighLevelAPI80.ObjectHandle hlaObjectHandle = _session80.CreateObject(hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    HighLevelAPI81.ObjectHandle hlaObjectHandle = _session81.CreateObject(hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    HighLevelAPI40.ObjectHandle hlaObjectHandle = _session40.CopyObject(objectHandle.ObjectHandle40, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    HighLevelAPI41.ObjectHandle hlaObjectHandle = _session41.CopyObject(objectHandle.ObjectHandle41, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    HighLevelAPI80.ObjectHandle hlaObjectHandle = _session80.CopyObject(objectHandle.ObjectHandle80, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    HighLevelAPI81.ObjectHandle hlaObjectHandle = _session81.CopyObject(objectHandle.ObjectHandle81, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.DestroyObject(objectHandle.ObjectHandle40);
                else
                    _session41.DestroyObject(objectHandle.ObjectHandle41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.DestroyObject(objectHandle.ObjectHandle80);
                else
                    _session81.DestroyObject(objectHandle.ObjectHandle81);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.GetObjectSize(objectHandle.ObjectHandle40);
                else
                    return _session41.GetObjectSize(objectHandle.ObjectHandle41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.GetObjectSize(objectHandle.ObjectHandle80);
                else
                    return _session81.GetObjectSize(objectHandle.ObjectHandle81);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                List<uint> uintList = new List<uint>();
                for (int i = 0; i < attributes.Count; i++)
                    uintList.Add(Convert.ToUInt32(attributes[i]));

                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = _session40.GetAttributeValue(objectHandle.ObjectHandle40, uintList);
                    return ObjectAttribute.ConvertFromHighLevelAPI40List(hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = _session41.GetAttributeValue(objectHandle.ObjectHandle41, uintList);
                    return ObjectAttribute.ConvertFromHighLevelAPI41List(hlaAttributes);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = _session80.GetAttributeValue(objectHandle.ObjectHandle80, attributes);
                    return ObjectAttribute.ConvertFromHighLevelAPI80List(hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = _session81.GetAttributeValue(objectHandle.ObjectHandle81, attributes);
                    return ObjectAttribute.ConvertFromHighLevelAPI81List(hlaAttributes);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    _session40.SetAttributeValue(objectHandle.ObjectHandle40, hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    _session41.SetAttributeValue(objectHandle.ObjectHandle41, hlaAttributes);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    _session80.SetAttributeValue(objectHandle.ObjectHandle80, hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    _session81.SetAttributeValue(objectHandle.ObjectHandle81, hlaAttributes);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    _session40.FindObjectsInit(hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    _session41.FindObjectsInit(hlaAttributes);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    _session80.FindObjectsInit(hlaAttributes);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    _session81.FindObjectsInit(hlaAttributes);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectHandle> hlaObjectHandles = _session40.FindObjects(objectCount);
                    return ObjectHandle.ConvertFromHighLevelAPI40List(hlaObjectHandles);
                }
                else
                {
                    List<HighLevelAPI41.ObjectHandle> hlaObjectHandles = _session41.FindObjects(objectCount);
                    return ObjectHandle.ConvertFromHighLevelAPI41List(hlaObjectHandles);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectHandle> hlaObjectHandles = _session80.FindObjects(objectCount);
                    return ObjectHandle.ConvertFromHighLevelAPI80List(hlaObjectHandles);
                }
                else
                {
                    List<HighLevelAPI81.ObjectHandle> hlaObjectHandles = _session81.FindObjects(objectCount);
                    return ObjectHandle.ConvertFromHighLevelAPI81List(hlaObjectHandles);
                }
            }
        }

        /// <summary>
        /// Terminates a search for token and session objects
        /// </summary>
        public void FindObjectsFinal()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.FindObjectsFinal();
                else
                    _session41.FindObjectsFinal();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.FindObjectsFinal();
                else
                    _session81.FindObjectsFinal();
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    List<HighLevelAPI40.ObjectHandle> hlaObjectHandles = _session40.FindAllObjects(hlaObjectAttributes);
                    return ObjectHandle.ConvertFromHighLevelAPI40List(hlaObjectHandles);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    List<HighLevelAPI41.ObjectHandle> hlaObjectHandles = _session41.FindAllObjects(hlaObjectAttributes);
                    return ObjectHandle.ConvertFromHighLevelAPI41List(hlaObjectHandles);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    List<HighLevelAPI80.ObjectHandle> hlaObjectHandles = _session80.FindAllObjects(hlaObjectAttributes);
                    return ObjectHandle.ConvertFromHighLevelAPI80List(hlaObjectHandles);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaObjectAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    List<HighLevelAPI81.ObjectHandle> hlaObjectHandles = _session81.FindAllObjects(hlaObjectAttributes);
                    return ObjectHandle.ConvertFromHighLevelAPI81List(hlaObjectHandles);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Encrypt(mechanism.Mechanism40, keyHandle.ObjectHandle40, data);
                else
                    return _session41.Encrypt(mechanism.Mechanism41, keyHandle.ObjectHandle41, data);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Encrypt(mechanism.Mechanism80, keyHandle.ObjectHandle80, data);
                else
                    return _session81.Encrypt(mechanism.Mechanism81, keyHandle.ObjectHandle81, data);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Encrypt(mechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, outputStream, bufferLength);
                else
                    _session41.Encrypt(mechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, outputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Encrypt(mechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, outputStream, bufferLength);
                else
                    _session81.Encrypt(mechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, outputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Decrypt(mechanism.Mechanism40, keyHandle.ObjectHandle40, encryptedData);
                else
                    return _session41.Decrypt(mechanism.Mechanism41, keyHandle.ObjectHandle41, encryptedData);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Decrypt(mechanism.Mechanism80, keyHandle.ObjectHandle80, encryptedData);
                else
                    return _session81.Decrypt(mechanism.Mechanism81, keyHandle.ObjectHandle81, encryptedData);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Decrypt(mechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, outputStream, bufferLength);
                else
                    _session41.Decrypt(mechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, outputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Decrypt(mechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, outputStream, bufferLength);
                else
                    _session81.Decrypt(mechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, outputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.DigestKey(mechanism.Mechanism40, keyHandle.ObjectHandle40);
                else
                    return _session41.DigestKey(mechanism.Mechanism41, keyHandle.ObjectHandle41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.DigestKey(mechanism.Mechanism80, keyHandle.ObjectHandle80);
                else
                    return _session81.DigestKey(mechanism.Mechanism81, keyHandle.ObjectHandle81);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Digest(mechanism.Mechanism40, data);
                else
                    return _session41.Digest(mechanism.Mechanism41, data);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Digest(mechanism.Mechanism80, data);
                else
                    return _session81.Digest(mechanism.Mechanism81, data);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Digest(mechanism.Mechanism40, inputStream, bufferLength);
                else
                    return _session41.Digest(mechanism.Mechanism41, inputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Digest(mechanism.Mechanism80, inputStream, bufferLength);
                else
                    return _session81.Digest(mechanism.Mechanism81, inputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Sign(mechanism.Mechanism40, keyHandle.ObjectHandle40, data);
                else
                    return _session41.Sign(mechanism.Mechanism41, keyHandle.ObjectHandle41, data);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Sign(mechanism.Mechanism80, keyHandle.ObjectHandle80, data);
                else
                    return _session81.Sign(mechanism.Mechanism81, keyHandle.ObjectHandle81, data);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.Sign(mechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, bufferLength);
                else
                    return _session41.Sign(mechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.Sign(mechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, bufferLength);
                else
                    return _session81.Sign(mechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.SignRecover(mechanism.Mechanism40, keyHandle.ObjectHandle40, data);
                else
                    return _session41.SignRecover(mechanism.Mechanism41, keyHandle.ObjectHandle41, data);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.SignRecover(mechanism.Mechanism80, keyHandle.ObjectHandle80, data);
                else
                    return _session81.SignRecover(mechanism.Mechanism81, keyHandle.ObjectHandle81, data);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Verify(mechanism.Mechanism40, keyHandle.ObjectHandle40, data, signature, out isValid);
                else
                    _session41.Verify(mechanism.Mechanism41, keyHandle.ObjectHandle41, data, signature, out isValid);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Verify(mechanism.Mechanism80, keyHandle.ObjectHandle80, data, signature, out isValid);
                else
                    _session81.Verify(mechanism.Mechanism81, keyHandle.ObjectHandle81, data, signature, out isValid);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.Verify(mechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, signature, out isValid, bufferLength);
                else
                    _session41.Verify(mechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, signature, out isValid, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.Verify(mechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, signature, out isValid, bufferLength);
                else
                    _session81.Verify(mechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, signature, out isValid, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.VerifyRecover(mechanism.Mechanism40, keyHandle.ObjectHandle40, signature, out isValid);
                else
                    return _session41.VerifyRecover(mechanism.Mechanism41, keyHandle.ObjectHandle41, signature, out isValid);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.VerifyRecover(mechanism.Mechanism80, keyHandle.ObjectHandle80, signature, out isValid);
                else
                    return _session81.VerifyRecover(mechanism.Mechanism81, keyHandle.ObjectHandle81, signature, out isValid);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.DigestEncrypt(digestingMechanism.Mechanism40, encryptionMechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, outputStream, bufferLength);
                else
                    return _session41.DigestEncrypt(digestingMechanism.Mechanism41, encryptionMechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, outputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.DigestEncrypt(digestingMechanism.Mechanism80, encryptionMechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, outputStream, bufferLength);
                else
                    return _session81.DigestEncrypt(digestingMechanism.Mechanism81, encryptionMechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, outputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.DecryptDigest(digestingMechanism.Mechanism40, decryptionMechanism.Mechanism40, keyHandle.ObjectHandle40, inputStream, outputStream, bufferLength);
                else
                    return _session41.DecryptDigest(digestingMechanism.Mechanism41, decryptionMechanism.Mechanism41, keyHandle.ObjectHandle41, inputStream, outputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.DecryptDigest(digestingMechanism.Mechanism80, decryptionMechanism.Mechanism80, keyHandle.ObjectHandle80, inputStream, outputStream, bufferLength);
                else
                    return _session81.DecryptDigest(digestingMechanism.Mechanism81, decryptionMechanism.Mechanism81, keyHandle.ObjectHandle81, inputStream, outputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.SignEncrypt(signingMechanism.Mechanism40, signingKeyHandle.ObjectHandle40, encryptionMechanism.Mechanism40, encryptionKeyHandle.ObjectHandle40, inputStream, outputStream, bufferLength);
                else
                    return _session41.SignEncrypt(signingMechanism.Mechanism41, signingKeyHandle.ObjectHandle41, encryptionMechanism.Mechanism41, encryptionKeyHandle.ObjectHandle41, inputStream, outputStream, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.SignEncrypt(signingMechanism.Mechanism80, signingKeyHandle.ObjectHandle80, encryptionMechanism.Mechanism80, encryptionKeyHandle.ObjectHandle80, inputStream, outputStream, bufferLength);
                else
                    return _session81.SignEncrypt(signingMechanism.Mechanism81, signingKeyHandle.ObjectHandle81, encryptionMechanism.Mechanism81, encryptionKeyHandle.ObjectHandle81, inputStream, outputStream, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.DecryptVerify(verificationMechanism.Mechanism40, verificationKeyHandle.ObjectHandle40, decryptionMechanism.Mechanism40, decryptionKeyHandle.ObjectHandle40, inputStream, outputStream, signature, out isValid, bufferLength);
                else
                    _session41.DecryptVerify(verificationMechanism.Mechanism41, verificationKeyHandle.ObjectHandle41, decryptionMechanism.Mechanism41, decryptionKeyHandle.ObjectHandle41, inputStream, outputStream, signature, out isValid, bufferLength);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.DecryptVerify(verificationMechanism.Mechanism80, verificationKeyHandle.ObjectHandle80, decryptionMechanism.Mechanism80, decryptionKeyHandle.ObjectHandle80, inputStream, outputStream, signature, out isValid, bufferLength);
                else
                    _session81.DecryptVerify(verificationMechanism.Mechanism81, verificationKeyHandle.ObjectHandle81, decryptionMechanism.Mechanism81, decryptionKeyHandle.ObjectHandle81, inputStream, outputStream, signature, out isValid, bufferLength);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    HighLevelAPI40.ObjectHandle hlaObjectHandle = _session40.GenerateKey(mechanism.Mechanism40, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    HighLevelAPI41.ObjectHandle hlaObjectHandle = _session41.GenerateKey(mechanism.Mechanism41, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    HighLevelAPI80.ObjectHandle hlaObjectHandle = _session80.GenerateKey(mechanism.Mechanism80, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    HighLevelAPI81.ObjectHandle hlaObjectHandle = _session81.GenerateKey(mechanism.Mechanism81, hlaAttributes);
                    return new ObjectHandle(hlaObjectHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(publicKeyAttributes);
                    List<HighLevelAPI40.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(privateKeyAttributes);

                    HighLevelAPI40.ObjectHandle hlaPublicKeyHandle = null;
                    HighLevelAPI40.ObjectHandle hlaPrivateKeyHandle = null;

                    _session40.GenerateKeyPair(mechanism.Mechanism40, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                    publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                    privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(publicKeyAttributes);
                    List<HighLevelAPI41.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(privateKeyAttributes);

                    HighLevelAPI41.ObjectHandle hlaPublicKeyHandle = null;
                    HighLevelAPI41.ObjectHandle hlaPrivateKeyHandle = null;

                    _session41.GenerateKeyPair(mechanism.Mechanism41, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                    publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                    privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(publicKeyAttributes);
                    List<HighLevelAPI80.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(privateKeyAttributes);

                    HighLevelAPI80.ObjectHandle hlaPublicKeyHandle = null;
                    HighLevelAPI80.ObjectHandle hlaPrivateKeyHandle = null;

                    _session80.GenerateKeyPair(mechanism.Mechanism80, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                    publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                    privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaPublicKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(publicKeyAttributes);
                    List<HighLevelAPI81.ObjectAttribute> hlaPrivateKeyAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(privateKeyAttributes);

                    HighLevelAPI81.ObjectHandle hlaPublicKeyHandle = null;
                    HighLevelAPI81.ObjectHandle hlaPrivateKeyHandle = null;

                    _session81.GenerateKeyPair(mechanism.Mechanism81, hlaPublicKeyAttributes, hlaPrivateKeyAttributes, out hlaPublicKeyHandle, out hlaPrivateKeyHandle);

                    publicKeyHandle = new ObjectHandle(hlaPublicKeyHandle);
                    privateKeyHandle = new ObjectHandle(hlaPrivateKeyHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.WrapKey(mechanism.Mechanism40, wrappingKeyHandle.ObjectHandle40, keyHandle.ObjectHandle40);
                else
                    return _session41.WrapKey(mechanism.Mechanism41, wrappingKeyHandle.ObjectHandle41, keyHandle.ObjectHandle41);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.WrapKey(mechanism.Mechanism80, wrappingKeyHandle.ObjectHandle80, keyHandle.ObjectHandle80);
                else
                    return _session81.WrapKey(mechanism.Mechanism81, wrappingKeyHandle.ObjectHandle81, keyHandle.ObjectHandle81);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    HighLevelAPI40.ObjectHandle unwrappedKeyHandle = _session40.UnwrapKey(mechanism.Mechanism40, unwrappingKeyHandle.ObjectHandle40, wrappedKey, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    HighLevelAPI41.ObjectHandle unwrappedKeyHandle = _session41.UnwrapKey(mechanism.Mechanism41, unwrappingKeyHandle.ObjectHandle41, wrappedKey, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    HighLevelAPI80.ObjectHandle unwrappedKeyHandle = _session80.UnwrapKey(mechanism.Mechanism80, unwrappingKeyHandle.ObjectHandle80, wrappedKey, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    HighLevelAPI81.ObjectHandle unwrappedKeyHandle = _session81.UnwrapKey(mechanism.Mechanism81, unwrappingKeyHandle.ObjectHandle81, wrappedKey, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI40List(attributes);
                    HighLevelAPI40.ObjectHandle unwrappedKeyHandle = _session40.DeriveKey(mechanism.Mechanism40, baseKeyHandle.ObjectHandle40, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
                else
                {
                    List<HighLevelAPI41.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI41List(attributes);
                    HighLevelAPI41.ObjectHandle unwrappedKeyHandle = _session41.DeriveKey(mechanism.Mechanism41, baseKeyHandle.ObjectHandle41, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI80List(attributes);
                    HighLevelAPI80.ObjectHandle unwrappedKeyHandle = _session80.DeriveKey(mechanism.Mechanism80, baseKeyHandle.ObjectHandle80, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
                else
                {
                    List<HighLevelAPI81.ObjectAttribute> hlaAttributes = ObjectAttribute.ConvertToHighLevelAPI81List(attributes);
                    HighLevelAPI81.ObjectHandle unwrappedKeyHandle = _session81.DeriveKey(mechanism.Mechanism81, baseKeyHandle.ObjectHandle81, hlaAttributes);
                    return new ObjectHandle(unwrappedKeyHandle);
                }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.SeedRandom(seed);
                else
                    _session41.SeedRandom(seed);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.SeedRandom(seed);
                else
                    _session81.SeedRandom(seed);
            }
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

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return _session40.GenerateRandom(length);
                else
                    return _session41.GenerateRandom(length);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return _session80.GenerateRandom(length);
                else
                    return _session81.GenerateRandom(length);
            }
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void GetFunctionStatus()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.GetFunctionStatus();
                else
                    _session41.GetFunctionStatus();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.GetFunctionStatus();
                else
                    _session81.GetFunctionStatus();
            }
        }

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        public void CancelFunction()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _session40.CancelFunction();
                else
                    _session41.CancelFunction();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _session80.CancelFunction();
                else
                    _session81.CancelFunction();
            }
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
                    if (_session40 != null)
                    {
                        _session40.Dispose();
                        _session40 = null;
                    }

                    if (_session41 != null)
                    {
                        _session41.Dispose();
                        _session41 = null;
                    }

                    if (_session80 != null)
                    {
                        _session80.Dispose();
                        _session80 = null;
                    }

                    if (_session81 != null)
                    {
                        _session81.Dispose();
                        _session81 = null;
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
