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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Logical connection between an application and a token
    /// </summary>
    public interface ISession : IDisposable
    {
        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        Pkcs11InteropFactories Factories
        {
            get;
        }

        /// <summary>
        /// PKCS#11 handle of session
        /// </summary>
        ulong SessionId
        {
            get;
        }

        /// <summary>
        /// Flag indicating whether session should be closed when object is disposed
        /// </summary>
        bool CloseWhenDisposed
        {
            get;
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        void CloseSession();

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        void InitPin(string userPin);

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="userPin">Pin value</param>
        void InitPin(byte[] userPin);

        /// <summary>
        /// Modifies the PIN of the user that is currently logged in, or the CKU_USER PIN if the session is not logged in.
        /// </summary>
        /// <param name="oldPin">Old PIN value</param>
        /// <param name="newPin">New PIN value</param>
        void SetPin(string oldPin, string newPin);

        /// <summary>
        /// Modifies the PIN of the user that is currently logged in, or the CKU_USER PIN if the session is not logged in.
        /// </summary>
        /// <param name="oldPin">Old PIN value</param>
        /// <param name="newPin">New PIN value</param>
        void SetPin(byte[] oldPin, byte[] newPin);

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <returns>Information about a session</returns>
        ISessionInfo GetSessionInfo();

        /// <summary>
        /// Obtains a copy of the cryptographic operations state of a session encoded as an array of bytes
        /// </summary>
        /// <returns>Operations state of a session</returns>
        byte[] GetOperationState();

        /// <summary>
        /// Restores the cryptographic operations state of a session from an array of bytes obtained with GetOperationState
        /// </summary>
        /// <param name="state">Array of bytes obtained with GetOperationState</param>
        /// <param name="encryptionKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing encryption or decryption operation in the restored session</param>
        /// <param name="authenticationKey">CK_INVALID_HANDLE or handle to the key which will be used for an ongoing signature, MACing, or verification operation in the restored session</param>
        void SetOperationState(byte[] state, IObjectHandle encryptionKey, IObjectHandle authenticationKey);

        /// <summary>
        /// Logs a user into a token
        /// </summary>
        /// <param name="userType">Type of user</param>
        /// <param name="pin">Pin of user</param>
        void Login(CKU userType, string pin);

        /// <summary>
        /// Logs a user into a token
        /// </summary>
        /// <param name="userType">Type of user</param>
        /// <param name="pin">Pin of user</param>
        void Login(CKU userType, byte[] pin);

        /// <summary>
        /// Logs a user out from a token
        /// </summary>
        void Logout();

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="attributes">Object attributes</param>
        /// <returns>Handle of created object</returns>
        IObjectHandle CreateObject(List<IObjectAttribute> attributes);

        /// <summary>
        /// Copies an object, creating a new object for the copy
        /// </summary>
        /// <param name="objectHandle">Handle of object to be copied</param>
        /// <param name="attributes">New values for any attributes of the object that can ordinarily be modified</param>
        /// <returns>Handle of copied object</returns>
        IObjectHandle CopyObject(IObjectHandle objectHandle, List<IObjectAttribute> attributes);

        /// <summary>
        /// Destroys an object
        /// </summary>
        /// <param name="objectHandle">Handle of object to be destroyed</param>
        void DestroyObject(IObjectHandle objectHandle);

        /// <summary>
        /// Gets the size of an object in bytes.
        /// </summary>
        /// <param name="objectHandle">Handle of object</param>
        /// <returns>Size of an object in bytes</returns>
        ulong GetObjectSize(IObjectHandle objectHandle);

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        List<IObjectAttribute> GetAttributeValue(IObjectHandle objectHandle, List<CKA> attributes);

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be read</param>
        /// <param name="attributes">List of attributes that should be read</param>
        /// <returns>Object attributes</returns>
        List<IObjectAttribute> GetAttributeValue(IObjectHandle objectHandle, List<ulong> attributes);

        /// <summary>
        /// Modifies the value of one or more attributes of an object
        /// </summary>
        /// <param name="objectHandle">Handle of object whose attributes should be modified</param>
        /// <param name="attributes">List of attributes that should be modified</param>
        void SetAttributeValue(IObjectHandle objectHandle, List<IObjectAttribute> attributes);

        /// <summary>
        /// Initializes a search for token and session objects that match a attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        void FindObjectsInit(List<IObjectAttribute> attributes);

        /// <summary>
        /// Continues a search for token and session objects that match a template, obtaining additional object handles
        /// </summary>
        /// <param name="objectCount">Maximum number of object handles to be returned</param>
        /// <returns>Found object handles</returns>
        List<IObjectHandle> FindObjects(int objectCount);

        /// <summary>
        /// Terminates a search for token and session objects
        /// </summary>
        void FindObjectsFinal();

        /// <summary>
        /// Searches for all token and session objects that match provided attributes
        /// </summary>
        /// <param name="attributes">Attributes that should be matched</param>
        /// <returns>Handles of found objects</returns>
        List<IObjectHandle> FindAllObjects(List<IObjectAttribute> attributes);

        /// <summary>
        /// Encrypts single-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be encrypted</param>
        /// <returns>Encrypted data</returns>
        byte[] Encrypt(IMechanism mechanism, IObjectHandle keyHandle, byte[] data);

        /// <summary>
        /// Encrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be encrypted should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        void Encrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream);

        /// <summary>
        /// Encrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be encrypted should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        void Encrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength);

        /// <summary>
        /// Decrypts single-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="encryptedData">Data to be decrypted</param>
        /// <returns>Decrypted data</returns>
        byte[] Decrypt(IMechanism mechanism, IObjectHandle keyHandle, byte[] encryptedData);

        /// <summary>
        /// Decrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which encrypted data should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        void Decrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream);

        /// <summary>
        /// Decrypts multi-part data
        /// </summary>
        /// <param name="mechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which encrypted data should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        void Decrypt(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength);

        /// <summary>
        /// Digests the value of a secret key
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="keyHandle">Handle of the secret key to be digested</param>
        /// <returns>Digest</returns>
        byte[] DigestKey(IMechanism mechanism, IObjectHandle keyHandle);

        /// <summary>
        /// Digests single-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="data">Data to be digested</param>
        /// <returns>Digest</returns>
        byte[] Digest(IMechanism mechanism, byte[] data);

        /// <summary>
        /// Digests multi-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Digest</returns>
        byte[] Digest(IMechanism mechanism, Stream inputStream);

        /// <summary>
        /// Digests multi-part data
        /// </summary>
        /// <param name="mechanism">Digesting mechanism</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Digest</returns>
        byte[] Digest(IMechanism mechanism, Stream inputStream, int bufferLength);

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] data);

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, byte[] data);

        /// <summary>
        /// Signs single-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, byte[] data);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, Stream inputStream);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, Stream inputStream);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, int bufferLength);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, Stream inputStream, int bufferLength);

        /// <summary>
        /// Signs multi-part data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="inputStream">Input stream from which data should be read</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        /// <returns>Signature</returns>
        byte[] Sign(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, Stream inputStream, int bufferLength);

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] data);

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, string keyPin, byte[] data);

        /// <summary>
        /// Signs single-part data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="keyHandle">Signature key</param>
        /// <param name="keyPin">Context specific signature pin</param>
        /// <param name="data">Data to be signed</param>
        /// <returns>Signature</returns>
        byte[] SignRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] keyPin, byte[] data);

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="data">Data that was signed</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        void Verify(IMechanism mechanism, IObjectHandle keyHandle, byte[] data, byte[] signature, out bool isValid);

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="inputStream">Input stream from which data that was signed should be read</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        void Verify(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid);

        /// <summary>
        /// Verifies a signature of data, where the signature is an appendix to the data
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="inputStream">Input stream from which data that was signed should be read</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        /// <param name="bufferLength">Size of read buffer in bytes</param>
        void Verify(IMechanism mechanism, IObjectHandle keyHandle, Stream inputStream, byte[] signature, out bool isValid, int bufferLength);

        /// <summary>
        /// Verifies signature of data, where the data can be recovered from the signature
        /// </summary>
        /// <param name="mechanism">Verification mechanism;</param>
        /// <param name="keyHandle">Verification key</param>
        /// <param name="signature">Signature</param>
        /// <param name="isValid">Flag indicating whether signature is valid</param>
        /// <returns>Data recovered from the signature</returns>
        byte[] VerifyRecover(IMechanism mechanism, IObjectHandle keyHandle, byte[] signature, out bool isValid);

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="digest">Digest</param>
        /// <param name="encryptedData">Encrypted data</param>
        void DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] encryptedData);

        /// <summary>
        /// Digests and encrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="encryptionMechanism">Encryption mechanism</param>
        /// <param name="keyHandle">Handle of the encryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where encrypted data should be written</param>
        /// <returns>Digest</returns>
        byte[] DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream);

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
        byte[] DigestEncrypt(IMechanism digestingMechanism, IMechanism encryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength);

        /// <summary>
        /// Digests and decrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="data">Data to be processed</param>
        /// <param name="digest">Digest</param>
        /// <param name="decryptedData">Decrypted data</param>
        void DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, byte[] data, out byte[] digest, out byte[] decryptedData);

        /// <summary>
        /// Digests and decrypts data
        /// </summary>
        /// <param name="digestingMechanism">Digesting mechanism</param>
        /// <param name="decryptionMechanism">Decryption mechanism</param>
        /// <param name="keyHandle">Handle of the decryption key</param>
        /// <param name="inputStream">Input stream from which data to be processed should be read</param>
        /// <param name="outputStream">Output stream where decrypted data should be written</param>
        /// <returns>Digest</returns>
        byte[] DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream);

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
        byte[] DecryptDigest(IMechanism digestingMechanism, IMechanism decryptionMechanism, IObjectHandle keyHandle, Stream inputStream, Stream outputStream, int bufferLength);

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
        void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData);

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
        void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData);

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
        void SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, byte[] data, out byte[] signature, out byte[] encryptedData);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, string signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength);

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
        byte[] SignEncrypt(IMechanism signingMechanism, IObjectHandle signingKeyHandle, byte[] signingKeyPin, IMechanism encryptionMechanism, IObjectHandle encryptionKeyHandle, Stream inputStream, Stream outputStream, int bufferLength);

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
        void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, byte[] data, byte[] signature, out byte[] decryptedData, out bool isValid);

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
        void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid);

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
        void DecryptVerify(IMechanism verificationMechanism, IObjectHandle verificationKeyHandle, IMechanism decryptionMechanism, IObjectHandle decryptionKeyHandle, Stream inputStream, Stream outputStream, byte[] signature, out bool isValid, int bufferLength);

        /// <summary>
        /// Generates a secret key or set of domain parameters, creating a new object
        /// </summary>
        /// <param name="mechanism">Generation mechanism</param>
        /// <param name="attributes">Attributes of the new key or set of domain parameters</param>
        /// <returns>Handle of the new key or set of domain parameters</returns>
        IObjectHandle GenerateKey(IMechanism mechanism, List<IObjectAttribute> attributes);

        /// <summary>
        /// Generates a public/private key pair, creating new key objects
        /// </summary>
        /// <param name="mechanism">Key generation mechanism</param>
        /// <param name="publicKeyAttributes">Attributes of the public key</param>
        /// <param name="privateKeyAttributes">Attributes of the private key</param>
        /// <param name="publicKeyHandle">Handle of the new public key</param>
        /// <param name="privateKeyHandle">Handle of the new private key</param>
        void GenerateKeyPair(IMechanism mechanism, List<IObjectAttribute> publicKeyAttributes, List<IObjectAttribute> privateKeyAttributes, out IObjectHandle publicKeyHandle, out IObjectHandle privateKeyHandle);

        /// <summary>
        /// Wraps (i.e., encrypts) a private or secret key
        /// </summary>
        /// <param name="mechanism">Wrapping mechanism</param>
        /// <param name="wrappingKeyHandle">Handle of wrapping key</param>
        /// <param name="keyHandle">Handle of key to be wrapped</param>
        /// <returns>Wrapped key</returns>
        byte[] WrapKey(IMechanism mechanism, IObjectHandle wrappingKeyHandle, IObjectHandle keyHandle);

        /// <summary>
        /// Unwraps (i.e. decrypts) a wrapped key, creating a new private key or secret key object
        /// </summary>
        /// <param name="mechanism">Unwrapping mechanism</param>
        /// <param name="unwrappingKeyHandle">Handle of unwrapping key</param>
        /// <param name="wrappedKey">Wrapped key</param>
        /// <param name="attributes">Attributes for unwrapped key</param>
        /// <returns>Handle of unwrapped key</returns>
        IObjectHandle UnwrapKey(IMechanism mechanism, IObjectHandle unwrappingKeyHandle, byte[] wrappedKey, List<IObjectAttribute> attributes);

        /// <summary>
        /// Derives a key from a base key, creating a new key object
        /// </summary>
        /// <param name="mechanism">Derivation mechanism</param>
        /// <param name="baseKeyHandle">Handle of base key</param>
        /// <param name="attributes">Attributes for the new key</param>
        /// <returns>Handle of derived key</returns>
        IObjectHandle DeriveKey(IMechanism mechanism, IObjectHandle baseKeyHandle, List<IObjectAttribute> attributes);

        /// <summary>
        /// Mixes additional seed material into the token's random number generator
        /// </summary>
        /// <param name="seed">Seed material</param>
        void SeedRandom(byte[] seed);

        /// <summary>
        /// Generates random or pseudo-random data
        /// </summary>
        /// <param name="length">Length in bytes of the random or pseudo-random data to be generated</param>
        /// <returns>Generated random or pseudo-random data</returns>
        byte[] GenerateRandom(int length);

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        void GetFunctionStatus();

        /// <summary>
        /// Legacy function which should throw CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        void CancelFunction();
    }
}
