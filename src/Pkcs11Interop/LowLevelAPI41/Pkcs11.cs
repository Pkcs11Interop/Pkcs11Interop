/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
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
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI41
{
    /// <summary>
    /// Low level PKCS#11 wrapper
    /// </summary>
    public class Pkcs11 : IDisposable
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
        /// Handle to the PKCS#11 library
        /// </summary>
        private IntPtr _libraryHandle = IntPtr.Zero;

        /// <summary>
        /// Handle to the PKCS#11 library. Use with caution!
        /// </summary>
        public IntPtr LibraryHandle
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _libraryHandle;
            }
        }

        /// <summary>
        /// Delegates for PKCS#11 functions
        /// </summary>
        private Delegates _delegates = null;

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        public Pkcs11(string libraryPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(libraryPath))
                    _libraryHandle = UnmanagedLibrary.Load(libraryPath);

                _delegates = new Delegates(_libraryHandle, true);
            }
            catch
            {
                Release();
                throw;
            }
        }

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        public Pkcs11(string libraryPath, bool useGetFunctionList)
        {
            try
            {
                if (!string.IsNullOrEmpty(libraryPath))
                    _libraryHandle = UnmanagedLibrary.Load(libraryPath);

                _delegates = new Delegates(_libraryHandle, useGetFunctionList);
            }
            catch
            {
                Release();
                throw;
            }
        }

        /// <summary>
        /// Unloads PKCS#11 library. Called automaticaly when object is being disposed.
        /// </summary>
        private void Release()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (_libraryHandle != IntPtr.Zero)
            {
                UnmanagedLibrary.Unload(_libraryHandle);
                _libraryHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Initializes the Cryptoki library
        /// </summary>
        /// <param name="initArgs">CK_C_INITIALIZE_ARGS structure containing information on how the library should deal with multi-threaded access or null if an application will not be accessing Cryptoki through multiple threads simultaneously</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CANT_LOCK, CKR_CRYPTOKI_ALREADY_INITIALIZED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_NEED_TO_CREATE_THREADS, CKR_OK</returns>
        public CKR C_Initialize(CK_C_INITIALIZE_ARGS initArgs)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Initialize(initArgs);
        }

        /// <summary>
        /// Called to indicate that an application is finished with the Cryptoki library. It should be the last Cryptoki call made by an application.
        /// </summary>
        /// <param name="reserved">Reserved for future versions. For this version, it should be set to null.</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK</returns>
        public CKR C_Finalize(IntPtr reserved)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Finalize(reserved);
        }

        /// <summary>
        /// Returns general information about Cryptoki
        /// </summary>
        /// <param name="info">Structure that receives the information</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK</returns>
        public CKR C_GetInfo(ref CK_INFO info)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetInfo(ref info);
        }

        /// <summary>
        /// Returns a pointer to the Cryptoki library's list of function pointers
        /// </summary>
        /// <param name="functionList">Pointer to a value which will receive a pointer to the library's CK_FUNCTION_LIST structure</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK</returns>
        public CKR C_GetFunctionList(out IntPtr functionList)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetFunctionList(out functionList);
        }

        /// <summary>
        /// Obtains a list of slots in the system
        /// </summary>
        /// <param name="tokenPresent">Indicates whether the list obtained includes only those slots with a token present (true) or all slots (false)</param>
        /// <param name="slotList">
        /// If set to null then the number of slots is returned in "count" parameter, without actually returning a list of slots.
        /// If not set to null then "count" parameter must contain the lenght of slotList array and slot list is returned in "slotList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of slots</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK</returns>
        public CKR C_GetSlotList(bool tokenPresent, uint[] slotList, ref uint count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetSlotList(tokenPresent, slotList, ref count);
        }

        /// <summary>
        /// Obtains information about a particular slot in the system
        /// </summary>
        /// <param name="slotId">The ID of the slot</param>
        /// <param name="info">Structure that receives the slot information</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SLOT_ID_INVALID</returns>
        public CKR C_GetSlotInfo(uint slotId, ref CK_SLOT_INFO info)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetSlotInfo(slotId, ref info);
        }

        /// <summary>
        /// Obtains information about a particular token in the system
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <param name="info">Structure that receives the token information</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT, CKR_TOKEN_NOT_RECOGNIZED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_GetTokenInfo(uint slotId, ref CK_TOKEN_INFO info)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetTokenInfo(slotId, ref info);
        }

        /// <summary>
        /// Obtains a list of mechanism types supported by a token
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <param name="mechanismList">
        /// If set to null then the number of mechanisms is returned in "count" parameter, without actually returning a list of mechanisms.
        /// If not set to null then "count" parameter must contain the lenght of mechanismList array and mechanism list is returned in "mechanismList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of mechanisms</param>
        /// <returns>CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT, CKR_TOKEN_NOT_RECOGNIZED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_GetMechanismList(uint slotId, CKM[] mechanismList, ref uint count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint[] uintList = null;
            if (mechanismList != null)
                uintList = new uint[mechanismList.Length];

            CKR rv = _delegates.C_GetMechanismList(slotId, uintList, ref count);

            if (mechanismList != null)
            {
                for (int i = 0; i < mechanismList.Length; i++)
                    mechanismList[i] = (CKM)uintList[i];
            }

            return rv;
        }

        /// <summary>
        /// Obtains information about a particular mechanism possibly supported by a token
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <param name="type">The type of mechanism</param>
        /// <param name="info">Structure that receives the mechanism information</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_MECHANISM_INVALID, CKR_OK, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT, CKR_TOKEN_NOT_RECOGNIZED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_GetMechanismInfo(uint slotId, CKM type, ref CK_MECHANISM_INFO info)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetMechanismInfo(slotId, (uint)type, ref info);
        }

        /// <summary>
        /// Initializes a token
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <param name="pin">SO's initial PIN or null to use protected authentication path (pinpad)</param>
        /// <param name="pinLen">The length of the PIN in bytes</param>
        /// <param name="label">32-byte long label of the token which must be padded with blank characters</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_PIN_INCORRECT, CKR_PIN_LOCKED, CKR_SESSION_EXISTS, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT, CKR_TOKEN_NOT_RECOGNIZED, CKR_TOKEN_WRITE_PROTECTED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_InitToken(uint slotId, byte[] pin, uint pinLen, byte[] label)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_InitToken(slotId, pin, pinLen, label);
        }

        /// <summary>
        /// Initializes the normal user's PIN
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="pin">Normal user's PIN or null to use protected authentication path (pinpad)</param>
        /// <param name="pinLen">The length of the PIN in bytes</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_PIN_INVALID, CKR_PIN_LEN_RANGE, CKR_SESSION_CLOSED, CKR_SESSION_READ_ONLY, CKR_SESSION_HANDLE_INVALID, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN, CKR_ARGUMENTS_BAD</returns>
        public CKR C_InitPIN(uint session, byte[] pin, uint pinLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_InitPIN(session, pin, pinLen);
        }

        /// <summary>
        /// Modifies the PIN of the user that is currently logged in, or the CKU_USER PIN if the session is not logged in
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="oldPin">Old PIN or null to use protected authentication path (pinpad)</param>
        /// <param name="oldPinLen">The length of the old PIN in bytes</param>
        /// <param name="newPin">New PIN or null to use protected authentication path (pinpad)</param>
        /// <param name="newPinLen">The length of the new PIN in bytes</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_PIN_INCORRECT, CKR_PIN_INVALID, CKR_PIN_LEN_RANGE, CKR_PIN_LOCKED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TOKEN_WRITE_PROTECTED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_SetPIN(uint session, byte[] oldPin, uint oldPinLen, byte[] newPin, uint newPinLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SetPIN(session, oldPin, oldPinLen, newPin, newPinLen);
        }

        /// <summary>
        /// Opens a session between an application and a token in a particular slot
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <param name="flags">Flags indicating the type of session</param>
        /// <param name="application">An application defined pointer to be passed to the notification callback</param>
        /// <param name="notify">The address of the notification callback function</param>
        /// <param name="session">Location that receives the handle for the new session</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SESSION_COUNT, CKR_SESSION_PARALLEL_NOT_SUPPORTED, CKR_SESSION_READ_WRITE_SO_EXISTS, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT, CKR_TOKEN_NOT_RECOGNIZED, CKR_TOKEN_WRITE_PROTECTED, CKR_ARGUMENTS_BAD</returns>
        public CKR C_OpenSession(uint slotId, uint flags, IntPtr application, IntPtr notify, ref uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_OpenSession(slotId, flags, application, notify, ref session);
        }

        /// <summary>
        /// Closes a session between an application and a token
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_CloseSession(uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_CloseSession(session);
        }

        /// <summary>
        /// Closes all sessions an application has with a token
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SLOT_ID_INVALID, CKR_TOKEN_NOT_PRESENT</returns>
        public CKR C_CloseAllSessions(uint slotId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_CloseAllSessions(slotId);
        }

        /// <summary>
        /// Obtains information about a session
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="info">Structure that receives the session information</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_ARGUMENTS_BAD</returns>
        public CKR C_GetSessionInfo(uint session, ref CK_SESSION_INFO info)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetSessionInfo(session, ref info);
        }

        /// <summary>
        /// Obtains a copy of the cryptographic operations state of a session encoded as byte array
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="operationState">
        /// If set to null then the length of state is returned in "operationStateLen" parameter, without actually returning a state.
        /// If not set to null then "operationStateLen" parameter must contain the lenght of operationState array and state is returned in "operationState" parameter.
        /// </param>
        /// <param name="operationStateLen">Location that receives the length in bytes of the state</param>
        /// <returns>CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_STATE_UNSAVEABLE, CKR_ARGUMENTS_BAD</returns>
        public CKR C_GetOperationState(uint session, byte[] operationState, ref uint operationStateLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetOperationState(session, operationState, ref operationStateLen);
        }

        /// <summary>
        /// Restores the cryptographic operations state of a session from bytes obtained with C_GetOperationState
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="operationState">Saved session state</param>
        /// <param name="operationStateLen">Length of saved session state</param>
        /// <param name="encryptionKey">Handle to the key which will be used for an ongoing encryption or decryption operation in the restored session or CK_INVALID_HANDLE if not needed</param>
        /// <param name="authenticationKey">Handle to the key which will be used for an ongoing operation in the restored session or CK_INVALID_HANDLE if not needed</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_CHANGED, CKR_KEY_NEEDED, CKR_KEY_NOT_NEEDED, CKR_OK, CKR_SAVED_STATE_INVALID, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_ARGUMENTS_BAD</returns>
        public CKR C_SetOperationState(uint session, byte[] operationState, uint operationStateLen, uint encryptionKey, uint authenticationKey)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SetOperationState(session, operationState, operationStateLen, encryptionKey, authenticationKey);
        }

        /// <summary>
        /// Logs a user into a token
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="userType">The user type</param>
        /// <param name="pin">User's PIN or null to use protected authentication path (pinpad)</param>
        /// <param name="pinLen">Length of user's PIN</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_PIN_INCORRECT, CKR_PIN_LOCKED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY_EXISTS, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_USER_PIN_NOT_INITIALIZED, CKR_USER_TOO_MANY_TYPES, CKR_USER_TYPE_INVALID</returns>
        public CKR C_Login(uint session, CKU userType, byte[] pin, uint pinLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Login(session, (uint)userType, pin, pinLen);
        }

        /// <summary>
        /// Logs a user out from a token
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_Logout(uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Logout(session);
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="template">Object's template</param>
        /// <param name="count">The number of attributes in the template</param>
        /// <param name="objectId">Location that receives the new object's handle</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_DOMAIN_PARAMS_INVALID, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCOMPLETE, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_CreateObject(uint session, CK_ATTRIBUTE[] template, uint count, ref uint objectId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_CreateObject(session, template, count, ref objectId);
        }

        /// <summary>
        /// Copies an object, creating a new object for the copy
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">The object's handle</param>
        /// <param name="template">Template for the new object</param>
        /// <param name="count">The number of attributes in the template</param>
        /// <param name="newObjectId">Location that receives the handle for the copy of the object</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OBJECT_HANDLE_INVALID, CKR_OK, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_CopyObject(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count, ref uint newObjectId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_CopyObject(session, objectId, template, count, ref newObjectId);
        }

        /// <summary>
        /// Destroys an object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">The object's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OBJECT_HANDLE_INVALID, CKR_OK, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TOKEN_WRITE_PROTECTED</returns>
        public CKR C_DestroyObject(uint session, uint objectId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DestroyObject(session, objectId);
        }

        /// <summary>
        /// Gets the size of an object in bytes
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">The object's handle</param>
        /// <param name="size">Location that receives the size in bytes of the object</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_INFORMATION_SENSITIVE, CKR_OBJECT_HANDLE_INVALID, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_GetObjectSize(uint session, uint objectId, ref uint size)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetObjectSize(session, objectId, ref size);
        }

        /// <summary>
        /// Obtains the value of one or more attributes of an object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">The object's handle</param>
        /// <param name="template">Template that specifies which attribute values are to be obtained, and receives the attribute values</param>
        /// <param name="count">The number of attributes in the template</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_SENSITIVE, CKR_ATTRIBUTE_TYPE_INVALID, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OBJECT_HANDLE_INVALID, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_GetAttributeValue(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetAttributeValue(session, objectId, template, count);
        }

        /// <summary>
        /// Modifies the value of one or more attributes of an object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">The object's handle</param>
        /// <param name="template">Template that specifies which attribute values are to be modified and their new values</param>
        /// <param name="count">The number of attributes in the template</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OBJECT_HANDLE_INVALID, CKR_OK, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SetAttributeValue(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SetAttributeValue(session, objectId, template, count);
        }

        /// <summary>
        /// Initializes a search for token and session objects that match a template
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="template">Search template that specifies the attribute values to match</param>
        /// <param name="count">The number of attributes in the search template</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_FindObjectsInit(uint session, CK_ATTRIBUTE[] template, uint count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_FindObjectsInit(session, template, count);
        }

        /// <summary>
        /// Continues a search for token and session objects that match a template, obtaining additional object handles
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="objectId">Location that receives the list (array) of additional object handles</param>
        /// <param name="maxObjectCount">The maximum number of object handles to be returned</param>
        /// <param name="objectCount">Location that receives the actual number of object handles returned</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_FindObjects(uint session, uint[] objectId, uint maxObjectCount, ref uint objectCount)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_FindObjects(session, objectId, maxObjectCount, ref objectCount);
        }

        /// <summary>
        /// Terminates a search for token and session objects
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_FindObjectsFinal(uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_FindObjectsFinal(session);
        }

        /// <summary>
        /// Initializes an encryption operation
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">The encryption mechanism</param>
        /// <param name="key">The handle of the encryption key</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_EncryptInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_EncryptInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Encrypts single-part data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="data">Data to be encrypted</param>
        /// <param name="dataLen">Length of data in bytes</param>
        /// <param name="encryptedData">
        /// If set to null then the length of encrypted data is returned in "encryptedDataLen" parameter, without actually returning encrypted data.
        /// If not set to null then "encryptedDataLen" parameter must contain the lenght of encryptedData array and encrypted data is returned in "encryptedData" parameter.
        /// </param>
        /// <param name="encryptedDataLen">Location that holds the length in bytes of the encrypted data</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_INVALID, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_Encrypt(uint session, byte[] data, uint dataLen, byte[] encryptedData, ref uint encryptedDataLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Encrypt(session, data, dataLen, encryptedData, ref encryptedDataLen);
        }

        /// <summary>
        /// Continues a multi-part encryption operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">The data part to be encrypted</param>
        /// <param name="partLen">Length of data part in bytes</param>
        /// <param name="encryptedPart">
        /// If set to null then the length of encrypted data part is returned in "encryptedPartLen" parameter, without actually returning encrypted data part.
        /// If not set to null then "encryptedPartLen" parameter must contain the lenght of encryptedPart array and encrypted data part is returned in "encryptedPart" parameter.
        /// </param>
        /// <param name="encryptedPartLen">Location that holds the length in bytes of the encrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_EncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_EncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
        }

        /// <summary>
        /// Finishes a multi-part encryption operation
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="lastEncryptedPart">
        /// If set to null then the length of last encrypted data part is returned in "lastEncryptedPartLen" parameter, without actually returning last encrypted data part.
        /// If not set to null then "lastEncryptedPartLen" parameter must contain the lenght of lastEncryptedPart array and last encrypted data part is returned in "lastEncryptedPart" parameter.
        /// </param>
        /// <param name="lastEncryptedPartLen">Location that holds the length of the last encrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_EncryptFinal(uint session, byte[] lastEncryptedPart, ref uint lastEncryptedPartLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_EncryptFinal(session, lastEncryptedPart, ref lastEncryptedPartLen);
        }

        /// <summary>
        /// Initializes a decryption operation
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">The decryption mechanism</param>
        /// <param name="key">The handle of the decryption key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_DecryptInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DecryptInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Decrypts encrypted data in a single part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="encryptedData">Encrypted data</param>
        /// <param name="encryptedDataLen">The length of the encrypted data</param>
        /// <param name="data">
        /// If set to null then the length of decrypted data is returned in "dataLen" parameter, without actually returning decrypted data.
        /// If not set to null then "dataLen" parameter must contain the lenght of data array and decrypted data is returned in "data" parameter.
        /// </param>
        /// <param name="dataLen">Location that holds the length of the decrypted data</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_ENCRYPTED_DATA_INVALID, CKR_ENCRYPTED_DATA_LEN_RANGE, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_Decrypt(uint session, byte[] encryptedData, uint encryptedDataLen, byte[] data, ref uint dataLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Decrypt(session, encryptedData, encryptedDataLen, data, ref dataLen);
        }

        /// <summary>
        /// Continues a multi-part decryption operation, processing another encrypted data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="encryptedPart">Encrypted data part</param>
        /// <param name="encryptedPartLen">Length of the encrypted data part</param>
        /// <param name="part">
        /// If set to null then the length of decrypted data part is returned in "partLen" parameter, without actually returning decrypted data part.
        /// If not set to null then "partLen" parameter must contain the lenght of part array and decrypted data part is returned in "part" parameter.
        /// </param>
        /// <param name="partLen">Location that holds the length of the decrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_ENCRYPTED_DATA_INVALID, CKR_ENCRYPTED_DATA_LEN_RANGE, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_DecryptUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DecryptUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
        }

        /// <summary>
        /// Finishes a multi-part decryption operation
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="lastPart">
        /// If set to null then the length of last decrypted data part is returned in "lastPartLen" parameter, without actually returning last decrypted data part.
        /// If not set to null then "lastPartLen" parameter must contain the lenght of lastPart array and last decrypted data part is returned in "lastPart" parameter.
        /// </param>
        /// <param name="lastPartLen">Location that holds the length of the last decrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_ENCRYPTED_DATA_INVALID, CKR_ENCRYPTED_DATA_LEN_RANGE, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_DecryptFinal(uint session, byte[] lastPart, ref uint lastPartLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DecryptFinal(session, lastPart, ref lastPartLen);
        }

        /// <summary>
        /// Initializes a message-digesting operation
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">The digesting mechanism</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_DigestInit(uint session, ref CK_MECHANISM mechanism)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DigestInit(session, ref mechanism);
        }

        /// <summary>
        /// Digests data in a single part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="data">Data to be digested</param>
        /// <param name="dataLen">The length of the data to be digested</param>
        /// <param name="digest">
        /// If set to null then the length of digest is returned in "digestLen" parameter, without actually returning digest.
        /// If not set to null then "digestLen" parameter must contain the lenght of digest array and digest is returned in "digest" parameter.
        /// </param>
        /// <param name="digestLen">Location that holds the length of the message digest</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_Digest(uint session, byte[] data, uint dataLen, byte[] digest, ref uint digestLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Digest(session, data, dataLen, digest, ref digestLen);
        }

        /// <summary>
        /// Continues a multi-part message-digesting operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">Data part</param>
        /// <param name="partLen">The length of the data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DigestUpdate(uint session, byte[] part, uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DigestUpdate(session, part, partLen);
        }

        /// <summary>
        /// Continues a multi-part message-digesting operation by digesting the value of a secret key
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="key">The handle of the secret key to be digested</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_HANDLE_INVALID, CKR_KEY_INDIGESTIBLE, CKR_KEY_SIZE_RANGE, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DigestKey(uint session, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DigestKey(session, key);
        }

        /// <summary>
        /// Finishes a multi-part message-digesting operation, returning the message digest
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="digest">
        /// If set to null then the length of digest is returned in "digestLen" parameter, without actually returning digest.
        /// If not set to null then "digestLen" parameter must contain the lenght of digest array and digest is returned in "digest" parameter.
        /// </param>
        /// <param name="digestLen">Location that holds the length of the message digest</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DigestFinal(uint session, byte[] digest, ref uint digestLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DigestFinal(session, digest, ref digestLen);
        }

        /// <summary>
        /// Initializes a signature operation, where the signature is an appendix to the data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="key">Handle of the signature key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED,CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SignInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Signs data in a single part, where the signature is an appendix to the data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="data">Data to be signed</param>
        /// <param name="dataLen">The length of the data</param>
        /// <param name="signature">
        /// If set to null then the length of signature is returned in "signatureLen" parameter, without actually returning signature.
        /// If not set to null then "signatureLen" parameter must contain the lenght of signature array and signature is returned in "signature" parameter.
        /// </param>
        /// <param name="signatureLen">Location that holds the length of the signature</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_INVALID, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN, CKR_FUNCTION_REJECTED</returns>
        public CKR C_Sign(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Sign(session, data, dataLen, signature, ref signatureLen);
        }

        /// <summary>
        /// Continues a multi-part signature operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">Data part</param>
        /// <param name="partLen">The length of the data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SignUpdate(uint session, byte[] part, uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignUpdate(session, part, partLen);
        }

        /// <summary>
        /// Finishes a multi-part signature operation, returning the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="signature">
        /// If set to null then the length of signature is returned in "signatureLen" parameter, without actually returning signature.
        /// If not set to null then "signatureLen" parameter must contain the lenght of signature array and signature is returned in "signature" parameter.
        /// </param>
        /// <param name="signatureLen">Location that holds the length of the signature</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN, CKR_FUNCTION_REJECTED</returns>
        public CKR C_SignFinal(uint session, byte[] signature, ref uint signatureLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignFinal(session, signature, ref signatureLen);
        }

        /// <summary>
        /// Initializes a signature operation, where the data can be recovered from the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Signature mechanism</param>
        /// <param name="key">Handle of the signature key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SignRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignRecoverInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Signs data in a single operation, where the data can be recovered from the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="data">Data to be signed</param>
        /// <param name="dataLen">The length of data to be signed</param>
        /// <param name="signature">
        /// If set to null then the length of signature is returned in "signatureLen" parameter, without actually returning signature.
        /// If not set to null then "signatureLen" parameter must contain the lenght of signature array and signature is returned in "signature" parameter.
        /// </param>
        /// <param name="signatureLen">Location that holds the length of the signature</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_INVALID, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SignRecover(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignRecover(session, data, dataLen, signature, ref signatureLen);
        }

        /// <summary>
        /// Initializes a verification operation, where the signature is an appendix to the data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">The verification mechanism</param>
        /// <param name="key">The handle of the verification key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_VerifyInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_VerifyInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Verifies a signature in a single-part operation, where the signature is an appendix to the data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="data">Data that were signed</param>
        /// <param name="dataLen">The length of the data</param>
        /// <param name="signature">Signature of data</param>
        /// <param name="signatureLen">The length of signature</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_INVALID, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SIGNATURE_INVALID, CKR_SIGNATURE_LEN_RANGE</returns>
        public CKR C_Verify(uint session, byte[] data, uint dataLen, byte[] signature, uint signatureLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_Verify(session, data, dataLen, signature, signatureLen);
        }

        /// <summary>
        /// Continues a multi-part verification operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">Data part</param>
        /// <param name="partLen">The length of the data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_VerifyUpdate(uint session, byte[] part, uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_VerifyUpdate(session, part, partLen);
        }

        /// <summary>
        /// Finishes a multi-part verification operation, checking the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="signature">Signature</param>
        /// <param name="signatureLen">The length of signature</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SIGNATURE_INVALID, CKR_SIGNATURE_LEN_RANGE</returns>
        public CKR C_VerifyFinal(uint session, byte[] signature, uint signatureLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_VerifyFinal(session, signature, signatureLen);
        }

        /// <summary>
        /// Initializes a signature verification operation, where the data is recovered from the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Verification mechanism</param>
        /// <param name="key">The handle of the verification key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_FUNCTION_NOT_PERMITTED, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_VerifyRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_VerifyRecoverInit(session, ref mechanism, key);
        }

        /// <summary>
        /// Verifies a signature in a single-part operation, where the data is recovered from the signature
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="signature">Signature</param>
        /// <param name="signatureLen">The length of signature</param>
        /// <param name="data">
        /// If set to null then the length of recovered data is returned in "dataLen" parameter, without actually returning recovered data.
        /// If not set to null then "dataLen" parameter must contain the lenght of data array and recovered data is returned in "data" parameter.
        /// </param>
        /// <param name="dataLen">Location that holds the length of the decrypted data</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_INVALID, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SIGNATURE_LEN_RANGE, CKR_SIGNATURE_INVALID</returns>
        public CKR C_VerifyRecover(uint session, byte[] signature, uint signatureLen, byte[] data, ref uint dataLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_VerifyRecover(session, signature, signatureLen, data, ref dataLen);
        }

        /// <summary>
        /// Continues multi-part digest and encryption operations, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">The data part to be digested and encrypted</param>
        /// <param name="partLen">Length of data part in bytes</param>
        /// <param name="encryptedPart">
        /// If set to null then the length of encrypted data part is returned in "encryptedPartLen" parameter, without actually returning encrypted data part.
        /// If not set to null then "encryptedPartLen" parameter must contain the lenght of encryptedPart array and encrypted data part is returned in "encryptedPart" parameter.
        /// </param>
        /// <param name="encryptedPartLen">Location that holds the length in bytes of the encrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DigestEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DigestEncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
        }

        /// <summary>
        /// Continues a multi-part combined decryption and digest operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="encryptedPart">Encrypted data part</param>
        /// <param name="encryptedPartLen">Length of the encrypted data part</param>
        /// <param name="part">
        /// If set to null then the length of decrypted data part is returned in "partLen" parameter, without actually returning decrypted data part.
        /// If not set to null then "partLen" parameter must contain the lenght of part array and decrypted data part is returned in "part" parameter.
        /// </param>
        /// <param name="partLen">Location that holds the length of the decrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_ENCRYPTED_DATA_INVALID, CKR_ENCRYPTED_DATA_LEN_RANGE, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DecryptDigestUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DecryptDigestUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
        }

        /// <summary>
        /// Continues a multi-part combined signature and encryption operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="part">The data part to be signed and encrypted</param>
        /// <param name="partLen">Length of data part in bytes</param>
        /// <param name="encryptedPart">
        /// If set to null then the length of encrypted data part is returned in "encryptedPartLen" parameter, without actually returning encrypted data part.
        /// If not set to null then "encryptedPartLen" parameter must contain the lenght of encryptedPart array and encrypted data part is returned in "encryptedPart" parameter.
        /// </param>
        /// <param name="encryptedPartLen">Location that holds the length in bytes of the encrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SignEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SignEncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
        }

        /// <summary>
        /// Continues a multi-part combined decryption and verification operation, processing another data part
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="encryptedPart">Encrypted data part</param>
        /// <param name="encryptedPartLen">Length of the encrypted data part</param>
        /// <param name="part">
        /// If set to null then the length of decrypted data part is returned in "partLen" parameter, without actually returning decrypted data part.
        /// If not set to null then "partLen" parameter must contain the lenght of part array and decrypted data part is returned in "part" parameter.
        /// </param>
        /// <param name="partLen">Location that holds the length of the decrypted data part</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DATA_LEN_RANGE, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_ENCRYPTED_DATA_INVALID, CKR_ENCRYPTED_DATA_LEN_RANGE, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_NOT_INITIALIZED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID</returns>
        public CKR C_DecryptVerifyUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DecryptVerifyUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
        }

        /// <summary>
        /// Generates a secret key or set of domain parameters, creating a new object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Key generation mechanism</param>
        /// <param name="template">The template for the new key or set of domain parameters</param>
        /// <param name="count">The number of attributes in the template</param>
        /// <param name="key">Location that receives the handle of the new key or set of domain parameters</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCOMPLETE, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_GenerateKey(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, uint count, ref uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GenerateKey(session, ref mechanism, template, count, ref key);
        }

        /// <summary>
        /// Generates a public/private key pair, creating new key objects
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Key generation mechanism</param>
        /// <param name="publicKeyTemplate">The template for the public key</param>
        /// <param name="publicKeyAttributeCount">The number of attributes in the public-key template</param>
        /// <param name="privateKeyTemplate">The template for the private key</param>
        /// <param name="privateKeyAttributeCount">The number of attributes in the private-key template</param>
        /// <param name="publicKey">Location that receives the handle of the new public key</param>
        /// <param name="privateKey">Location that receives the handle of the new private key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_DOMAIN_PARAMS_INVALID, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCOMPLETE, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_GenerateKeyPair(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, uint publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, uint privateKeyAttributeCount, ref uint publicKey, ref uint privateKey)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GenerateKeyPair(session, ref mechanism, publicKeyTemplate, publicKeyAttributeCount, privateKeyTemplate, privateKeyAttributeCount, ref publicKey, ref privateKey);
        }

        /// <summary>
        /// Wraps (i.e., encrypts) a private or secret key
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Wrapping mechanism</param>
        /// <param name="wrappingKey">The handle of the wrapping key</param>
        /// <param name="key">The handle of the key to be wrapped</param>
        /// <param name="wrappedKey">
        /// If set to null then the length of wrapped key is returned in "wrappedKeyLen" parameter, without actually returning wrapped key.
        /// If not set to null then "wrappedKeyLen" parameter must contain the lenght of wrappedKey array and wrapped key is returned in "wrappedKey" parameter.
        /// </param>
        /// <param name="wrappedKeyLen">Location that receives the length of the wrapped key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_HANDLE_INVALID, CKR_KEY_NOT_WRAPPABLE, CKR_KEY_SIZE_RANGE, CKR_KEY_UNEXTRACTABLE, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN, CKR_WRAPPING_KEY_HANDLE_INVALID, CKR_WRAPPING_KEY_SIZE_RANGE, CKR_WRAPPING_KEY_TYPE_INCONSISTENT</returns>
        public CKR C_WrapKey(uint session, ref CK_MECHANISM mechanism, uint wrappingKey, uint key, byte[] wrappedKey, ref uint wrappedKeyLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_WrapKey(session, ref mechanism, wrappingKey, key, wrappedKey, ref wrappedKeyLen);
        }

        /// <summary>
        /// Unwraps (i.e. decrypts) a wrapped key, creating a new private key or secret key object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Unwrapping mechanism</param>
        /// <param name="unwrappingKey">The handle of the unwrapping key</param>
        /// <param name="wrappedKey">Wrapped key</param>
        /// <param name="wrappedKeyLen">The length of the wrapped key</param>
        /// <param name="template">The template for the new key</param>
        /// <param name="attributeCount">The number of attributes in the template</param>
        /// <param name="key">Location that receives the handle of the unwrapped key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_BUFFER_TOO_SMALL, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_DOMAIN_PARAMS_INVALID, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCOMPLETE, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_UNWRAPPING_KEY_HANDLE_INVALID, CKR_UNWRAPPING_KEY_SIZE_RANGE, CKR_UNWRAPPING_KEY_TYPE_INCONSISTENT, CKR_USER_NOT_LOGGED_IN, CKR_WRAPPED_KEY_INVALID, CKR_WRAPPED_KEY_LEN_RANGE</returns>
        public CKR C_UnwrapKey(uint session, ref CK_MECHANISM mechanism, uint unwrappingKey, byte[] wrappedKey, uint wrappedKeyLen, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_UnwrapKey(session, ref mechanism, unwrappingKey, wrappedKey, wrappedKeyLen, template, attributeCount, ref key);
        }

        /// <summary>
        /// Derives a key from a base key, creating a new key object
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="mechanism">Key derivation mechanism</param>
        /// <param name="baseKey">The handle of the base key</param>
        /// <param name="template">The template for the new key</param>
        /// <param name="attributeCount">The number of attributes in the template</param>
        /// <param name="key">Location that receives the handle of the derived key</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_ATTRIBUTE_READ_ONLY, CKR_ATTRIBUTE_TYPE_INVALID, CKR_ATTRIBUTE_VALUE_INVALID, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_DOMAIN_PARAMS_INVALID, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_KEY_HANDLE_INVALID, CKR_KEY_SIZE_RANGE, CKR_KEY_TYPE_INCONSISTENT, CKR_MECHANISM_INVALID, CKR_MECHANISM_PARAM_INVALID, CKR_OK, CKR_OPERATION_ACTIVE, CKR_PIN_EXPIRED, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_SESSION_READ_ONLY, CKR_TEMPLATE_INCOMPLETE, CKR_TEMPLATE_INCONSISTENT, CKR_TOKEN_WRITE_PROTECTED, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_DeriveKey(uint session, ref CK_MECHANISM mechanism, uint baseKey, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_DeriveKey(session, ref mechanism, baseKey, template, attributeCount, ref key);
        }

        /// <summary>
        /// Mixes additional seed material into the token's random number generator
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="seed">The seed material</param>
        /// <param name="seedLen">The length of the seed material</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_ACTIVE, CKR_RANDOM_SEED_NOT_SUPPORTED, CKR_RANDOM_NO_RNG, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_SeedRandom(uint session, byte[] seed, uint seedLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_SeedRandom(session, seed, seedLen);
        }

        /// <summary>
        /// Generates random or pseudo-random data
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <param name="randomData">Location that receives the random data</param>
        /// <param name="randomLen">The length in bytes of the random or pseudo-random data to be generated</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_DEVICE_ERROR, CKR_DEVICE_MEMORY, CKR_DEVICE_REMOVED, CKR_FUNCTION_CANCELED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_OK, CKR_OPERATION_ACTIVE, CKR_RANDOM_NO_RNG, CKR_SESSION_CLOSED, CKR_SESSION_HANDLE_INVALID, CKR_USER_NOT_LOGGED_IN</returns>
        public CKR C_GenerateRandom(uint session, byte[] randomData, uint randomLen)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GenerateRandom(session, randomData, randomLen);
        }

        /// <summary>
        /// Legacy function which should simply return the value CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_FUNCTION_NOT_PARALLEL</returns>
        public CKR C_GetFunctionStatus(uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_GetFunctionStatus(session);
        }

        /// <summary>
        /// Legacy function which should simply return the value CKR_FUNCTION_NOT_PARALLEL
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_FUNCTION_NOT_PARALLEL</returns>
        public CKR C_CancelFunction(uint session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_CancelFunction(session);
        }

        /// <summary>
        /// Waits for a slot event, such as token insertion or token removal, to occur
        /// </summary>
        /// <param name="flags">Determines whether or not the C_WaitForSlotEvent call blocks (i.e., waits for a slot event to occur)</param>
        /// <param name="slot">Location which will receive the ID of the slot that the event occurred in</param>
        /// <param name="reserved">Reserved for future versions (should be null)</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_CRYPTOKI_NOT_INITIALIZED, CKR_FUNCTION_FAILED, CKR_GENERAL_ERROR, CKR_HOST_MEMORY, CKR_NO_EVENT, CKR_OK</returns>
        public CKR C_WaitForSlotEvent(uint flags, ref uint slot, IntPtr reserved)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _delegates.C_WaitForSlotEvent(flags, ref slot, reserved);
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
                }

                // Dispose unmanaged objects
                Release();

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~Pkcs11()
        {
            Dispose(false);
        }

        #endregion
    }
}
