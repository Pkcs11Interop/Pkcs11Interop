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
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;
using System.ComponentModel;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /*!
     * \example 01_LowLevelAPI/01_InitializeTest.cs
     * \example 01_LowLevelAPI/02_GetInfoTest.cs
     * \example 01_LowLevelAPI/03_SlotListInfoAndEventTest.cs
     * \example 01_LowLevelAPI/04_TokenInfoTest.cs
     * \example 01_LowLevelAPI/05_MechanismListAndInfoTest.cs
     * \example 01_LowLevelAPI/06_SessionTest.cs
     * \example 01_LowLevelAPI/07_OperationStateTest.cs
     * \example 01_LowLevelAPI/08_LoginTest.cs
     * \example 01_LowLevelAPI/09_InitTokenAndPinTest.cs
     * \example 01_LowLevelAPI/10_SetPinTest.cs
     * \example 01_LowLevelAPI/11_SeedAndGenerateRandomTest.cs
     * \example 01_LowLevelAPI/12_DigestTest.cs
     * \example 01_LowLevelAPI/13_UnmanagedMemoryTest.cs
     * \example 01_LowLevelAPI/14_ObjectAttributeTest.cs
     * \example 01_LowLevelAPI/15_MechanismTest.cs
     * \example 01_LowLevelAPI/16_CreateCopyDestroyObjectTest.cs
     * \example 01_LowLevelAPI/17_GetAndSetAttributeValueTest.cs
     * \example 01_LowLevelAPI/18_ObjectFindingTest.cs
     * \example 01_LowLevelAPI/19_GenerateKeyAndKeyPairTest.cs
     * \example 01_LowLevelAPI/20_EncryptAndDecryptTest.cs
     * \example 01_LowLevelAPI/21_SignAndVerifyTest.cs
     * \example 01_LowLevelAPI/22_SignAndVerifyRecoverTest.cs
     * \example 01_LowLevelAPI/23_DigestEncryptAndDecryptDigestTest.cs
     * \example 01_LowLevelAPI/24_SignEncryptAndDecryptVerifyTest.cs
     * \example 01_LowLevelAPI/25_WrapAndUnwrapKeyTest.cs
     * \example 01_LowLevelAPI/26_DeriveKeyTest.cs
     * \example 01_LowLevelAPI/27_LegacyParallelFunctionsTest.cs
     * \example 01_LowLevelAPI/28_Helpers.cs
     */

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
        /// Handle to the PKCS#11 library
        /// </summary>
        private IntPtr _libraryHandle = IntPtr.Zero;

        /// <summary>
        /// Cryptoki function list
        /// </summary>
        private CK_FUNCTION_LIST _functionList;

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        public Pkcs11(string libraryPath)
        {
            if (libraryPath == null)
                throw new ArgumentNullException("libraryPath");

            _libraryHandle = UnmanagedLibrary.Load(libraryPath);
            C_GetFunctionList(out _functionList);
        }

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        public Pkcs11(string libraryPath, bool useGetFunctionList)
        {
            if (libraryPath == null)
                throw new ArgumentNullException("libraryPath");

            _libraryHandle = UnmanagedLibrary.Load(libraryPath);

            if (useGetFunctionList)
                C_GetFunctionList(out _functionList);
            else
                GetFunctionList(out _functionList);
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

            C_InitializeDelegate cInitialize = (C_InitializeDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Initialize, typeof(C_InitializeDelegate));
            return cInitialize(initArgs);
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

            C_FinalizeDelegate cFinalize = (C_FinalizeDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Finalize, typeof(C_FinalizeDelegate));
            return cFinalize(reserved);
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

            C_GetInfoDelegate cGetInfo = (C_GetInfoDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetInfo, typeof(C_GetInfoDelegate));
            return cGetInfo(ref info);
        }

        /// <summary>
        /// Obtains a pointer to the Cryptoki library's list of function pointers
        /// </summary>
        /// <param name="functionList">Structure that receives function pointers for all the Cryptoki API routines in the library</param>
        private void C_GetFunctionList(out CK_FUNCTION_LIST functionList)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            // Get pointer to C_GetFunctionList function
            IntPtr C_GetFunctionListPointer = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetFunctionList");

            // Get delegate for C_GetFunctionList
            C_GetFunctionListDelegate cGetFunctionList = (C_GetFunctionListDelegate)Marshal.GetDelegateForFunctionPointer(C_GetFunctionListPointer, typeof(C_GetFunctionListDelegate));

            // Call C_GetFunctionList function via delegate
            IntPtr functionListPointer = IntPtr.Zero;
            CKR rv = cGetFunctionList(out functionListPointer);
            if ((rv != CKR.CKR_OK) || (functionListPointer == IntPtr.Zero))
                throw new Pkcs11InteropException("Unable to call C_GetFunctionList");

            PlatformID platformId = System.Environment.OSVersion.Platform;
            if ((platformId == PlatformID.Unix) || (platformId == PlatformID.MacOSX))
            {
                // Workaround for MONO on Linux and OS X where marshaling of CK_FUNCTION_LIST is not working correctly
                CK_FUNCTION_LIST_UNIX functionListUnix = (CK_FUNCTION_LIST_UNIX)UnmanagedMemory.Read(functionListPointer, typeof(CK_FUNCTION_LIST_UNIX));

                // Set output parameter
                functionList = functionListUnix.ConvertToCkFunctionList();
            }
            else
            {
                // Set output parameter
                functionList = (CK_FUNCTION_LIST)UnmanagedMemory.Read(functionListPointer, typeof(CK_FUNCTION_LIST));
            }
        }

        /// <summary>
        /// Obtains function pointers for all the Cryptoki API routines in the library
        /// </summary>
        /// <param name="functionList">Structure that receives function pointers for all the Cryptoki API routines in the library</param>
        private void GetFunctionList(out CK_FUNCTION_LIST functionList)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            // Get function pointers for all the Cryptoki API routines in the library
            CK_FUNCTION_LIST ckFunctionList = new CK_FUNCTION_LIST();
            ckFunctionList.C_Initialize = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Initialize");
            ckFunctionList.C_Finalize = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Finalize");
            ckFunctionList.C_GetInfo = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetInfo");
            ckFunctionList.C_GetFunctionList = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetFunctionList");
            ckFunctionList.C_GetSlotList = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetSlotList");
            ckFunctionList.C_GetSlotInfo = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetSlotInfo");
            ckFunctionList.C_GetTokenInfo = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetTokenInfo");
            ckFunctionList.C_GetMechanismList = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetMechanismList");
            ckFunctionList.C_GetMechanismInfo = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetMechanismInfo");
            ckFunctionList.C_InitToken = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_InitToken");
            ckFunctionList.C_InitPIN = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_InitPIN");
            ckFunctionList.C_SetPIN = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SetPIN");
            ckFunctionList.C_OpenSession = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_OpenSession");
            ckFunctionList.C_CloseSession = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_CloseSession");
            ckFunctionList.C_CloseAllSessions = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_CloseAllSessions");
            ckFunctionList.C_GetSessionInfo = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetSessionInfo");
            ckFunctionList.C_GetOperationState = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetOperationState");
            ckFunctionList.C_SetOperationState = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SetOperationState");
            ckFunctionList.C_Login = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Login");
            ckFunctionList.C_Logout = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Logout");
            ckFunctionList.C_CreateObject = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_CreateObject");
            ckFunctionList.C_CopyObject = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_CopyObject");
            ckFunctionList.C_DestroyObject = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DestroyObject");
            ckFunctionList.C_GetObjectSize = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetObjectSize");
            ckFunctionList.C_GetAttributeValue = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetAttributeValue");
            ckFunctionList.C_SetAttributeValue = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SetAttributeValue");
            ckFunctionList.C_FindObjectsInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_FindObjectsInit");
            ckFunctionList.C_FindObjects = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_FindObjects");
            ckFunctionList.C_FindObjectsFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_FindObjectsFinal");
            ckFunctionList.C_EncryptInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_EncryptInit");
            ckFunctionList.C_Encrypt = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Encrypt");
            ckFunctionList.C_EncryptUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_EncryptUpdate");
            ckFunctionList.C_EncryptFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_EncryptFinal");
            ckFunctionList.C_DecryptInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DecryptInit");
            ckFunctionList.C_Decrypt = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Decrypt");
            ckFunctionList.C_DecryptUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DecryptUpdate");
            ckFunctionList.C_DecryptFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DecryptFinal");
            ckFunctionList.C_DigestInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DigestInit");
            ckFunctionList.C_Digest = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Digest");
            ckFunctionList.C_DigestUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DigestUpdate");
            ckFunctionList.C_DigestKey = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DigestKey");
            ckFunctionList.C_DigestFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DigestFinal");
            ckFunctionList.C_SignInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignInit");
            ckFunctionList.C_Sign = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Sign");
            ckFunctionList.C_SignUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignUpdate");
            ckFunctionList.C_SignFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignFinal");
            ckFunctionList.C_SignRecoverInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignRecoverInit");
            ckFunctionList.C_SignRecover = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignRecover");
            ckFunctionList.C_VerifyInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_VerifyInit");
            ckFunctionList.C_Verify = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_Verify");
            ckFunctionList.C_VerifyUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_VerifyUpdate");
            ckFunctionList.C_VerifyFinal = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_VerifyFinal");
            ckFunctionList.C_VerifyRecoverInit = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_VerifyRecoverInit");
            ckFunctionList.C_VerifyRecover = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_VerifyRecover");
            ckFunctionList.C_DigestEncryptUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DigestEncryptUpdate");
            ckFunctionList.C_DecryptDigestUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DecryptDigestUpdate");
            ckFunctionList.C_SignEncryptUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SignEncryptUpdate");
            ckFunctionList.C_DecryptVerifyUpdate = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DecryptVerifyUpdate");
            ckFunctionList.C_GenerateKey = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GenerateKey");
            ckFunctionList.C_GenerateKeyPair = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GenerateKeyPair");
            ckFunctionList.C_WrapKey = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_WrapKey");
            ckFunctionList.C_UnwrapKey = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_UnwrapKey");
            ckFunctionList.C_DeriveKey = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_DeriveKey");
            ckFunctionList.C_SeedRandom = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_SeedRandom");
            ckFunctionList.C_GenerateRandom = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GenerateRandom");
            ckFunctionList.C_GetFunctionStatus = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_GetFunctionStatus");
            ckFunctionList.C_CancelFunction = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_CancelFunction");
            ckFunctionList.C_WaitForSlotEvent = UnmanagedLibrary.GetFunctionPointer(_libraryHandle, "C_WaitForSlotEvent");

            // Set output parameter
            functionList = ckFunctionList;
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

            C_GetSlotListDelegate cGetSlotList = (C_GetSlotListDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetSlotList, typeof(C_GetSlotListDelegate));
            return cGetSlotList(tokenPresent, slotList, ref count);
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

            C_GetSlotInfoDelegate cGetSlotInfo = (C_GetSlotInfoDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetSlotInfo, typeof(C_GetSlotInfoDelegate));
            return cGetSlotInfo(slotId, ref info);
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

            C_GetTokenInfoDelegate cGetTokenInfo = (C_GetTokenInfoDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetTokenInfo, typeof(C_GetTokenInfoDelegate));
            return cGetTokenInfo(slotId, ref info);
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

            C_GetMechanismListDelegate cGetMechanismList = (C_GetMechanismListDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetMechanismList, typeof(C_GetMechanismListDelegate));
            return cGetMechanismList(slotId, mechanismList, ref count);
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

            C_GetMechanismInfoDelegate cGetMechanismInfo = (C_GetMechanismInfoDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetMechanismInfo, typeof(C_GetMechanismInfoDelegate));
            return cGetMechanismInfo(slotId, type, ref info);
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

            C_InitTokenDelegate cInitToken = (C_InitTokenDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_InitToken, typeof(C_InitTokenDelegate));
            return cInitToken(slotId, pin, pinLen, label);
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

            C_InitPINDelegate cInitPIN = (C_InitPINDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_InitPIN, typeof(C_InitPINDelegate));
            return cInitPIN(session, pin, pinLen);
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

            C_SetPINDelegate cSetPIN = (C_SetPINDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SetPIN, typeof(C_SetPINDelegate));
            return cSetPIN(session, oldPin, oldPinLen, newPin, newPinLen);
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

            C_OpenSessionDelegate cOpenSession = (C_OpenSessionDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_OpenSession, typeof(C_OpenSessionDelegate));
            return cOpenSession(slotId, flags, application, notify, ref session);
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

            C_CloseSessionDelegate cCloseSession = (C_CloseSessionDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_CloseSession, typeof(C_CloseSessionDelegate));
            return cCloseSession(session);
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

            C_CloseAllSessionsDelegate cCloseAllSessions = (C_CloseAllSessionsDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_CloseAllSessions, typeof(C_CloseAllSessionsDelegate));
            return cCloseAllSessions(slotId);
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

            C_GetSessionInfoDelegate cGetSessionInfo = (C_GetSessionInfoDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetSessionInfo, typeof(C_GetSessionInfoDelegate));
            return cGetSessionInfo(session, ref info);
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

            C_GetOperationStateDelegate cGetOperationState = (C_GetOperationStateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetOperationState, typeof(C_GetOperationStateDelegate));
            return cGetOperationState(session, operationState, ref operationStateLen);
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

            C_SetOperationStateDelegate cSetOperationState = (C_SetOperationStateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SetOperationState, typeof(C_SetOperationStateDelegate));
            return cSetOperationState(session, operationState, operationStateLen, encryptionKey, authenticationKey);
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

            C_LoginDelegate cLogin = (C_LoginDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Login, typeof(C_LoginDelegate));
            return cLogin(session, userType, pin, pinLen);
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

            C_LogoutDelegate cLogout = (C_LogoutDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Logout, typeof(C_LogoutDelegate));
            return cLogout(session);
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

            C_CreateObjectDelegate cCreateObject = (C_CreateObjectDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_CreateObject, typeof(C_CreateObjectDelegate));
            return cCreateObject(session, template, count, ref objectId);
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

            C_CopyObjectDelegate cCopyObject = (C_CopyObjectDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_CopyObject, typeof(C_CopyObjectDelegate));
            return cCopyObject(session, objectId, template, count, ref newObjectId);
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

            C_DestroyObjectDelegate cDestroyObject = (C_DestroyObjectDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DestroyObject, typeof(C_DestroyObjectDelegate));
            return cDestroyObject(session, objectId);
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

            C_GetObjectSizeDelegate cGetObjectSize = (C_GetObjectSizeDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetObjectSize, typeof(C_GetObjectSizeDelegate));
            return cGetObjectSize(session, objectId, ref size);
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

            C_GetAttributeValueDelegate cGetAttributeValue = (C_GetAttributeValueDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetAttributeValue, typeof(C_GetAttributeValueDelegate));
            return cGetAttributeValue(session, objectId, template, count);
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

            C_SetAttributeValueDelegate cSetAttributeValue = (C_SetAttributeValueDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SetAttributeValue, typeof(C_SetAttributeValueDelegate));
            return cSetAttributeValue(session, objectId, template, count);
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

            C_FindObjectsInitDelegate cFindObjectsInit = (C_FindObjectsInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_FindObjectsInit, typeof(C_FindObjectsInitDelegate));
            return cFindObjectsInit(session, template, count);
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

            C_FindObjectsDelegate cFindObjects = (C_FindObjectsDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_FindObjects, typeof(C_FindObjectsDelegate));
            return cFindObjects(session, objectId, maxObjectCount, ref objectCount);
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

            C_FindObjectsFinalDelegate cFindObjectsFinal = (C_FindObjectsFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_FindObjectsFinal, typeof(C_FindObjectsFinalDelegate));
            return cFindObjectsFinal(session);
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

            C_EncryptInitDelegate cEncryptInit = (C_EncryptInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_EncryptInit, typeof(C_EncryptInitDelegate));
            return cEncryptInit(session, ref mechanism, key);
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

            C_EncryptDelegate cEncrypt = (C_EncryptDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Encrypt, typeof(C_EncryptDelegate));
            return cEncrypt(session, data, dataLen, encryptedData, ref encryptedDataLen);
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

            C_EncryptUpdateDelegate cEncryptUpdate = (C_EncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_EncryptUpdate, typeof(C_EncryptUpdateDelegate));
            return cEncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
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

            C_EncryptFinalDelegate cEncryptFinal = (C_EncryptFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_EncryptFinal, typeof(C_EncryptFinalDelegate));
            return cEncryptFinal(session, lastEncryptedPart, ref lastEncryptedPartLen);
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

            C_DecryptInitDelegate cDecryptInit = (C_DecryptInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DecryptInit, typeof(C_DecryptInitDelegate));
            return cDecryptInit(session, ref mechanism, key);
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

            C_DecryptDelegate cDecrypt = (C_DecryptDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Decrypt, typeof(C_DecryptDelegate));
            return cDecrypt(session, encryptedData, encryptedDataLen, data, ref dataLen);
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

            C_DecryptUpdateDelegate cDecryptUpdate = (C_DecryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DecryptUpdate, typeof(C_DecryptUpdateDelegate));
            return cDecryptUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
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

            C_DecryptFinalDelegate cDecryptFinal = (C_DecryptFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DecryptFinal, typeof(C_DecryptFinalDelegate));
            return cDecryptFinal(session, lastPart, ref lastPartLen);
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

            C_DigestInitDelegate cDigestInit = (C_DigestInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DigestInit, typeof(C_DigestInitDelegate));
            return cDigestInit(session, ref mechanism);
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

            C_DigestDelegate cDigest = (C_DigestDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Digest, typeof(C_DigestDelegate));
            return cDigest(session, data, dataLen, digest, ref digestLen);
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

            C_DigestUpdateDelegate cDigestUpdate = (C_DigestUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DigestUpdate, typeof(C_DigestUpdateDelegate));
            return cDigestUpdate(session, part, partLen);
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

            C_DigestKeyDelegate cDigestKey = (C_DigestKeyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DigestKey, typeof(C_DigestKeyDelegate));
            return cDigestKey(session, key);
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

            C_DigestFinalDelegate cDigestFinal = (C_DigestFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DigestFinal, typeof(C_DigestFinalDelegate));
            return cDigestFinal(session, digest, ref digestLen);
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

            C_SignInitDelegate cSignInit = (C_SignInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignInit, typeof(C_SignInitDelegate));
            return cSignInit(session, ref mechanism, key);
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

            C_SignDelegate cSign = (C_SignDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Sign, typeof(C_SignDelegate));
            return cSign(session, data, dataLen, signature, ref signatureLen);
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

            C_SignUpdateDelegate cSignUpdate = (C_SignUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignUpdate, typeof(C_SignUpdateDelegate));
            return cSignUpdate(session, part, partLen);
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

            C_SignFinalDelegate cSignFinal = (C_SignFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignFinal, typeof(C_SignFinalDelegate));
            return cSignFinal(session, signature, ref signatureLen);
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

            C_SignRecoverInitDelegate cSignRecoverInit = (C_SignRecoverInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignRecoverInit, typeof(C_SignRecoverInitDelegate));
            return cSignRecoverInit(session, ref mechanism, key);
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

            C_SignRecoverDelegate cSignRecover = (C_SignRecoverDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignRecover, typeof(C_SignRecoverDelegate));
            return cSignRecover(session, data, dataLen, signature, ref signatureLen);
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

            C_VerifyInitDelegate cVerifyInit = (C_VerifyInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_VerifyInit, typeof(C_VerifyInitDelegate));
            return cVerifyInit(session, ref mechanism, key);
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

            C_VerifyDelegate cVerify = (C_VerifyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_Verify, typeof(C_VerifyDelegate));
            return cVerify(session, data, dataLen, signature, signatureLen);
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

            C_VerifyUpdateDelegate cVerifyUpdate = (C_VerifyUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_VerifyUpdate, typeof(C_VerifyUpdateDelegate));
            return cVerifyUpdate(session, part, partLen);
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

            C_VerifyFinalDelegate cVerifyFinal = (C_VerifyFinalDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_VerifyFinal, typeof(C_VerifyFinalDelegate));
            return cVerifyFinal(session, signature, signatureLen);
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

            C_VerifyRecoverInitDelegate cVerifyRecoverInit = (C_VerifyRecoverInitDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_VerifyRecoverInit, typeof(C_VerifyRecoverInitDelegate));
            return cVerifyRecoverInit(session, ref mechanism, key);
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

            C_VerifyRecoverDelegate cVerifyRecover = (C_VerifyRecoverDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_VerifyRecover, typeof(C_VerifyRecoverDelegate));
            return cVerifyRecover(session, signature, signatureLen, data, ref dataLen);
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

            C_DigestEncryptUpdateDelegate cDigestEncryptUpdate = (C_DigestEncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DigestEncryptUpdate, typeof(C_DigestEncryptUpdateDelegate));
            return cDigestEncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
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

            C_DecryptDigestUpdateDelegate cDecryptDigestUpdate = (C_DecryptDigestUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DecryptDigestUpdate, typeof(C_DecryptDigestUpdateDelegate));
            return cDecryptDigestUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
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

            C_SignEncryptUpdateDelegate cSignEncryptUpdate = (C_SignEncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SignEncryptUpdate, typeof(C_SignEncryptUpdateDelegate));
            return cSignEncryptUpdate(session, part, partLen, encryptedPart, ref encryptedPartLen);
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

            C_DecryptVerifyUpdateDelegate cDecryptVerifyUpdate = (C_DecryptVerifyUpdateDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DecryptVerifyUpdate, typeof(C_DecryptVerifyUpdateDelegate));
            return cDecryptVerifyUpdate(session, encryptedPart, encryptedPartLen, part, ref partLen);
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

            C_GenerateKeyDelegate cGenerateKey = (C_GenerateKeyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GenerateKey, typeof(C_GenerateKeyDelegate));
            return cGenerateKey(session, ref mechanism, template, count, ref key);
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

            C_GenerateKeyPairDelegate cGenerateKeyPair = (C_GenerateKeyPairDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GenerateKeyPair, typeof(C_GenerateKeyPairDelegate));
            return cGenerateKeyPair(session, ref mechanism, publicKeyTemplate, publicKeyAttributeCount, privateKeyTemplate, privateKeyAttributeCount, ref publicKey, ref privateKey);
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

            C_WrapKeyDelegate cWrapKey = (C_WrapKeyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_WrapKey, typeof(C_WrapKeyDelegate));
            return cWrapKey(session, ref mechanism, wrappingKey, key, wrappedKey, ref wrappedKeyLen);
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

            C_UnwrapKeyDelegate cUnwrapKey = (C_UnwrapKeyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_UnwrapKey, typeof(C_UnwrapKeyDelegate));
            return cUnwrapKey(session, ref mechanism, unwrappingKey, wrappedKey, wrappedKeyLen, template, attributeCount, ref key);
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

            C_DeriveKeyDelegate cDeriveKey = (C_DeriveKeyDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_DeriveKey, typeof(C_DeriveKeyDelegate));
            return cDeriveKey(session, ref mechanism, baseKey, template, attributeCount, ref key);
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

            C_SeedRandomDelegate cSeedRandom = (C_SeedRandomDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_SeedRandom, typeof(C_SeedRandomDelegate));
            return cSeedRandom(session, seed, seedLen);
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

            C_GenerateRandomDelegate cGenerateRandom = (C_GenerateRandomDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GenerateRandom, typeof(C_GenerateRandomDelegate));
            return cGenerateRandom(session, randomData, randomLen);
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

            C_GetFunctionStatusDelegate cGetFunctionStatus = (C_GetFunctionStatusDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_GetFunctionStatus, typeof(C_GetFunctionStatusDelegate));
            return cGetFunctionStatus(session);
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

            C_CancelFunctionDelegate cCancelFunction = (C_CancelFunctionDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_CancelFunction, typeof(C_CancelFunctionDelegate));
            return cCancelFunction(session);
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

            C_WaitForSlotEventDelegate cWaitForSlotEvent = (C_WaitForSlotEventDelegate)Marshal.GetDelegateForFunctionPointer(_functionList.C_WaitForSlotEvent, typeof(C_WaitForSlotEventDelegate));
            return cWaitForSlotEvent(flags, ref slot, reserved);
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
