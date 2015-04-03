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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_InitializeDelegate(CK_C_INITIALIZE_ARGS initArgs);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FinalizeDelegate(IntPtr reserved);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetInfoDelegate(ref CK_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetFunctionListDelegate(out IntPtr functionList);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSlotListDelegate([MarshalAs(UnmanagedType.U1)] bool tokenPresent, uint[] slotList, ref uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSlotInfoDelegate(uint slotId, ref CK_SLOT_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetTokenInfoDelegate(uint slotId, ref CK_TOKEN_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismListDelegate(uint slotId, uint[] mechanismList, ref uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismInfoDelegate(uint slotId, uint type, ref CK_MECHANISM_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_InitTokenDelegate(uint slotId, byte[] pin, uint pinLen, byte[] label);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_InitPINDelegate(uint session, byte[] pin, uint pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetPINDelegate(uint session, byte[] oldPin, uint oldPinLen, byte[] newPin, uint newPinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_OpenSessionDelegate(uint slotId, uint flags, IntPtr application, IntPtr notify, ref uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CloseSessionDelegate(uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CloseAllSessionsDelegate(uint slotId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSessionInfoDelegate(uint session, ref CK_SESSION_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetOperationStateDelegate(uint session, byte[] operationState, ref uint operationStateLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetOperationStateDelegate(uint session, byte[] operationState, uint operationStateLen, uint encryptionKey, uint authenticationKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_LoginDelegate(uint session, uint userType, byte[] pin, uint pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_LogoutDelegate(uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CreateObjectDelegate(uint session, CK_ATTRIBUTE[] template, uint count, ref uint objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CopyObjectDelegate(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count, ref uint newObjectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DestroyObjectDelegate(uint session, uint objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetObjectSizeDelegate(uint session, uint objectId, ref uint size);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetAttributeValueDelegate(uint session, uint objectId, [In, Out] CK_ATTRIBUTE[] template, uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetAttributeValueDelegate(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsInitDelegate(uint session, CK_ATTRIBUTE[] template, uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsDelegate(uint session, uint[] objectId, uint maxObjectCount, ref uint objectCount);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsFinalDelegate(uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptDelegate(uint session, byte[] data, uint dataLen, byte[] encryptedData, ref uint encryptedDataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptUpdateDelegate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptFinalDelegate(uint session, byte[] lastEncryptedPart, ref uint lastEncryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptDelegate(uint session, byte[] encryptedData, uint encryptedDataLen, byte[] data, ref uint dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptUpdateDelegate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptFinalDelegate(uint session, byte[] lastPart, ref uint lastPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestInitDelegate(uint session, ref CK_MECHANISM mechanism);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestDelegate(uint session, byte[] data, uint dataLen, byte[] digest, ref uint digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestUpdateDelegate(uint session, byte[] part, uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestKeyDelegate(uint session, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestFinalDelegate(uint session, byte[] digest, ref uint digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignDelegate(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignUpdateDelegate(uint session, byte[] part, uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignFinalDelegate(uint session, byte[] signature, ref uint signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignRecoverInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignRecoverDelegate(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyDelegate(uint session, byte[] data, uint dataLen, byte[] signature, uint signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyUpdateDelegate(uint session, byte[] part, uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyFinalDelegate(uint session, byte[] signature, uint signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyRecoverInitDelegate(uint session, ref CK_MECHANISM mechanism, uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyRecoverDelegate(uint session, byte[] signature, uint signatureLen, byte[] data, ref uint dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestEncryptUpdateDelegate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptDigestUpdateDelegate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignEncryptUpdateDelegate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptVerifyUpdateDelegate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateKeyDelegate(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, uint count, ref uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateKeyPairDelegate(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, uint publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, uint privateKeyAttributeCount, ref uint publicKey, ref uint privateKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_WrapKeyDelegate(uint session, ref CK_MECHANISM mechanism, uint wrappingKey, uint key, byte[] wrappedKey, ref uint wrappedKeyLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_UnwrapKeyDelegate(uint session, ref CK_MECHANISM mechanism, uint unwrappingKey, byte[] wrappedKey, uint wrappedKeyLen, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DeriveKeyDelegate(uint session, ref CK_MECHANISM mechanism, uint baseKey, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SeedRandomDelegate(uint session, byte[] seed, uint seedLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateRandomDelegate(uint session, byte[] randomData, uint randomLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetFunctionStatusDelegate(uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CancelFunctionDelegate(uint session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_WaitForSlotEventDelegate(uint flags, ref uint slot, IntPtr reserved);

    /// <summary>
    /// Holds delegates for all PKCS#11 functions
    /// </summary>
    internal class Delegates
    {
        /// <summary>
        /// Delegate for C_Initialize
        /// </summary>
        internal C_InitializeDelegate C_Initialize = null;

        /// <summary>
        /// Delegate for C_Finalize
        /// </summary>
        internal C_FinalizeDelegate C_Finalize = null;

        /// <summary>
        /// Delegate for C_GetInfo
        /// </summary>
        internal C_GetInfoDelegate C_GetInfo = null;

        /// <summary>
        /// Delegate for C_GetFunctionList
        /// </summary>
        internal C_GetFunctionListDelegate C_GetFunctionList = null;

        /// <summary>
        /// Delegate for C_GetSlotList
        /// </summary>
        internal C_GetSlotListDelegate C_GetSlotList = null;

        /// <summary>
        /// Delegate for C_GetSlotInfo
        /// </summary>
        internal C_GetSlotInfoDelegate C_GetSlotInfo = null;

        /// <summary>
        /// Delegate for C_GetTokenInfo
        /// </summary>
        internal C_GetTokenInfoDelegate C_GetTokenInfo = null;

        /// <summary>
        /// Delegate for C_GetMechanismList
        /// </summary>
        internal C_GetMechanismListDelegate C_GetMechanismList = null;

        /// <summary>
        /// Delegate for C_GetMechanismInfo
        /// </summary>
        internal C_GetMechanismInfoDelegate C_GetMechanismInfo = null;

        /// <summary>
        /// Delegate for C_InitToken
        /// </summary>
        internal C_InitTokenDelegate C_InitToken = null;

        /// <summary>
        /// Delegate for C_InitPIN
        /// </summary>
        internal C_InitPINDelegate C_InitPIN = null;

        /// <summary>
        /// Delegate for C_SetPIN
        /// </summary>
        internal C_SetPINDelegate C_SetPIN = null;

        /// <summary>
        /// Delegate for C_OpenSession
        /// </summary>
        internal C_OpenSessionDelegate C_OpenSession = null;

        /// <summary>
        /// Delegate for C_CloseSession
        /// </summary>
        internal C_CloseSessionDelegate C_CloseSession = null;

        /// <summary>
        /// Delegate for C_CloseAllSessions
        /// </summary>
        internal C_CloseAllSessionsDelegate C_CloseAllSessions = null;

        /// <summary>
        /// Delegate for C_GetSessionInfo
        /// </summary>
        internal C_GetSessionInfoDelegate C_GetSessionInfo = null;

        /// <summary>
        /// Delegate for C_GetOperationState
        /// </summary>
        internal C_GetOperationStateDelegate C_GetOperationState = null;

        /// <summary>
        /// Delegate for C_SetOperationState
        /// </summary>
        internal C_SetOperationStateDelegate C_SetOperationState = null;

        /// <summary>
        /// Delegate for C_Login
        /// </summary>
        internal C_LoginDelegate C_Login = null;

        /// <summary>
        /// Delegate for C_Logout
        /// </summary>
        internal C_LogoutDelegate C_Logout = null;

        /// <summary>
        /// Delegate for C_CreateObject
        /// </summary>
        internal C_CreateObjectDelegate C_CreateObject = null;

        /// <summary>
        /// Delegate for C_CopyObject
        /// </summary>
        internal C_CopyObjectDelegate C_CopyObject = null;

        /// <summary>
        /// Delegate for C_DestroyObject
        /// </summary>
        internal C_DestroyObjectDelegate C_DestroyObject = null;

        /// <summary>
        /// Delegate for C_GetObjectSize
        /// </summary>
        internal C_GetObjectSizeDelegate C_GetObjectSize = null;

        /// <summary>
        /// Delegate for C_GetAttributeValue
        /// </summary>
        internal C_GetAttributeValueDelegate C_GetAttributeValue = null;

        /// <summary>
        /// Delegate for C_SetAttributeValue
        /// </summary>
        internal C_SetAttributeValueDelegate C_SetAttributeValue = null;

        /// <summary>
        /// Delegate for C_FindObjectsInit
        /// </summary>
        internal C_FindObjectsInitDelegate C_FindObjectsInit = null;

        /// <summary>
        /// Delegate for C_FindObjects
        /// </summary>
        internal C_FindObjectsDelegate C_FindObjects = null;

        /// <summary>
        /// Delegate for C_FindObjectsFinal
        /// </summary>
        internal C_FindObjectsFinalDelegate C_FindObjectsFinal = null;

        /// <summary>
        /// Delegate for C_EncryptInit
        /// </summary>
        internal C_EncryptInitDelegate C_EncryptInit = null;

        /// <summary>
        /// Delegate for C_Encrypt
        /// </summary>
        internal C_EncryptDelegate C_Encrypt = null;

        /// <summary>
        /// Delegate for C_EncryptUpdate
        /// </summary>
        internal C_EncryptUpdateDelegate C_EncryptUpdate = null;

        /// <summary>
        /// Delegate for C_EncryptFinal
        /// </summary>
        internal C_EncryptFinalDelegate C_EncryptFinal = null;

        /// <summary>
        /// Delegate for C_DecryptInit
        /// </summary>
        internal C_DecryptInitDelegate C_DecryptInit = null;

        /// <summary>
        /// Delegate for C_Decrypt
        /// </summary>
        internal C_DecryptDelegate C_Decrypt = null;

        /// <summary>
        /// Delegate for C_DecryptUpdate
        /// </summary>
        internal C_DecryptUpdateDelegate C_DecryptUpdate = null;

        /// <summary>
        /// Delegate for C_DecryptFinal
        /// </summary>
        internal C_DecryptFinalDelegate C_DecryptFinal = null;

        /// <summary>
        /// Delegate for C_DigestInit
        /// </summary>
        internal C_DigestInitDelegate C_DigestInit = null;

        /// <summary>
        /// Delegate for C_Digest
        /// </summary>
        internal C_DigestDelegate C_Digest = null;

        /// <summary>
        /// Delegate for C_DigestUpdate
        /// </summary>
        internal C_DigestUpdateDelegate C_DigestUpdate = null;

        /// <summary>
        /// Delegate for C_DigestKey
        /// </summary>
        internal C_DigestKeyDelegate C_DigestKey = null;

        /// <summary>
        /// Delegate for C_DigestFinal
        /// </summary>
        internal C_DigestFinalDelegate C_DigestFinal = null;

        /// <summary>
        /// Delegate for C_SignInit
        /// </summary>
        internal C_SignInitDelegate C_SignInit = null;

        /// <summary>
        /// Delegate for C_Sign
        /// </summary>
        internal C_SignDelegate C_Sign = null;

        /// <summary>
        /// Delegate for C_SignUpdate
        /// </summary>
        internal C_SignUpdateDelegate C_SignUpdate = null;

        /// <summary>
        /// Delegate for C_SignFinal
        /// </summary>
        internal C_SignFinalDelegate C_SignFinal = null;

        /// <summary>
        /// Delegate for C_SignRecoverInit
        /// </summary>
        internal C_SignRecoverInitDelegate C_SignRecoverInit = null;

        /// <summary>
        /// Delegate for C_SignRecover
        /// </summary>
        internal C_SignRecoverDelegate C_SignRecover = null;

        /// <summary>
        /// Delegate for C_VerifyInit
        /// </summary>
        internal C_VerifyInitDelegate C_VerifyInit = null;

        /// <summary>
        /// Delegate for C_Verify
        /// </summary>
        internal C_VerifyDelegate C_Verify = null;

        /// <summary>
        /// Delegate for C_VerifyUpdate
        /// </summary>
        internal C_VerifyUpdateDelegate C_VerifyUpdate = null;

        /// <summary>
        /// Delegate for C_VerifyFinal
        /// </summary>
        internal C_VerifyFinalDelegate C_VerifyFinal = null;

        /// <summary>
        /// Delegate for C_VerifyRecoverInit
        /// </summary>
        internal C_VerifyRecoverInitDelegate C_VerifyRecoverInit = null;

        /// <summary>
        /// Delegate for C_VerifyRecover
        /// </summary>
        internal C_VerifyRecoverDelegate C_VerifyRecover = null;

        /// <summary>
        /// Delegate for C_DigestEncryptUpdate
        /// </summary>
        internal C_DigestEncryptUpdateDelegate C_DigestEncryptUpdate = null;

        /// <summary>
        /// Delegate for C_DecryptDigestUpdate
        /// </summary>
        internal C_DecryptDigestUpdateDelegate C_DecryptDigestUpdate = null;

        /// <summary>
        /// Delegate for C_SignEncryptUpdate
        /// </summary>
        internal C_SignEncryptUpdateDelegate C_SignEncryptUpdate = null;

        /// <summary>
        /// Delegate for C_DecryptVerifyUpdate
        /// </summary>
        internal C_DecryptVerifyUpdateDelegate C_DecryptVerifyUpdate = null;

        /// <summary>
        /// Delegate for C_GenerateKey
        /// </summary>
        internal C_GenerateKeyDelegate C_GenerateKey = null;

        /// <summary>
        /// Delegate for C_GenerateKeyPair
        /// </summary>
        internal C_GenerateKeyPairDelegate C_GenerateKeyPair = null;

        /// <summary>
        /// Delegate for C_WrapKey
        /// </summary>
        internal C_WrapKeyDelegate C_WrapKey = null;

        /// <summary>
        /// Delegate for C_UnwrapKey
        /// </summary>
        internal C_UnwrapKeyDelegate C_UnwrapKey = null;

        /// <summary>
        /// Delegate for C_DeriveKey
        /// </summary>
        internal C_DeriveKeyDelegate C_DeriveKey = null;

        /// <summary>
        /// Delegate for C_SeedRandom
        /// </summary>
        internal C_SeedRandomDelegate C_SeedRandom = null;

        /// <summary>
        /// Delegate for C_GenerateRandom
        /// </summary>
        internal C_GenerateRandomDelegate C_GenerateRandom = null;

        /// <summary>
        /// Delegate for C_GetFunctionStatus
        /// </summary>
        internal C_GetFunctionStatusDelegate C_GetFunctionStatus = null;

        /// <summary>
        /// Delegate for C_CancelFunction
        /// </summary>
        internal C_CancelFunctionDelegate C_CancelFunction = null;

        /// <summary>
        /// Delegate for C_WaitForSlotEvent
        /// </summary>
        internal C_WaitForSlotEventDelegate C_WaitForSlotEvent = null;

        /// <summary>
        /// Initializes new instance of Delegates class
        /// </summary>
        /// <param name="libraryHandle">Handle to the PKCS#11 library</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        internal Delegates(IntPtr libraryHandle, bool useGetFunctionList)
        {
            if (useGetFunctionList)
            {
                if (libraryHandle != IntPtr.Zero)
                {
                    InitializeWithGetFunctionList(libraryHandle);
                }
                else
                {
                    InitializeWithGetFunctionList();
                }
            }
            else
            {
                if (libraryHandle != IntPtr.Zero)
                {
                    InitializeWithoutGetFunctionList(libraryHandle);
                }
                else
                {
                    InitializeWithoutGetFunctionList();
                }
            }
        }

        /// <summary>
        /// Get delegates with C_GetFunctionList function from the dynamically loaded shared PKCS#11 library
        /// </summary>
        /// <param name="libraryHandle">Handle to the PKCS#11 library</param>
        private void InitializeWithGetFunctionList(IntPtr libraryHandle)
        {
            IntPtr cGetFunctionListPtr = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetFunctionList");
            C_GetFunctionListDelegate cGetFunctionList = (C_GetFunctionListDelegate)Marshal.GetDelegateForFunctionPointer(cGetFunctionListPtr, typeof(C_GetFunctionListDelegate));

            IntPtr functionList = IntPtr.Zero;

            CKR rv = cGetFunctionList(out functionList);
            if ((rv != CKR.CKR_OK) || (functionList == IntPtr.Zero))
                throw new Pkcs11Exception("C_GetFunctionList", rv);

            CK_FUNCTION_LIST ckFunctionList = (CK_FUNCTION_LIST)UnmanagedMemory.Read(functionList, typeof(CK_FUNCTION_LIST));
            Initialize(ckFunctionList);
        }

        /// <summary>
        /// Get delegates with C_GetFunctionList function from the statically linked PKCS#11 library
        /// </summary>
        private void InitializeWithGetFunctionList()
        {
            IntPtr functionList = IntPtr.Zero;

            CKR rv = NativeMethods.C_GetFunctionList(out functionList);
            if ((rv != CKR.CKR_OK) || (functionList == IntPtr.Zero))
                throw new Pkcs11Exception("C_GetFunctionList", rv);

            CK_FUNCTION_LIST ckFunctionList = (CK_FUNCTION_LIST)UnmanagedMemory.Read(functionList, typeof(CK_FUNCTION_LIST));
            Initialize(ckFunctionList);
        }

        /// <summary>
        /// Get delegates without C_GetFunctionList function from the dynamically loaded shared PKCS#11 library
        /// </summary>
        /// <param name="libraryHandle">Handle to the PKCS#11 library</param>
        private void InitializeWithoutGetFunctionList(IntPtr libraryHandle)
        {
            CK_FUNCTION_LIST ckFunctionList = new CK_FUNCTION_LIST();
            ckFunctionList.C_Initialize = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Initialize");
            ckFunctionList.C_Finalize = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Finalize");
            ckFunctionList.C_GetInfo = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetInfo");
            ckFunctionList.C_GetFunctionList = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetFunctionList");
            ckFunctionList.C_GetSlotList = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetSlotList");
            ckFunctionList.C_GetSlotInfo = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetSlotInfo");
            ckFunctionList.C_GetTokenInfo = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetTokenInfo");
            ckFunctionList.C_GetMechanismList = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetMechanismList");
            ckFunctionList.C_GetMechanismInfo = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetMechanismInfo");
            ckFunctionList.C_InitToken = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_InitToken");
            ckFunctionList.C_InitPIN = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_InitPIN");
            ckFunctionList.C_SetPIN = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SetPIN");
            ckFunctionList.C_OpenSession = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_OpenSession");
            ckFunctionList.C_CloseSession = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_CloseSession");
            ckFunctionList.C_CloseAllSessions = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_CloseAllSessions");
            ckFunctionList.C_GetSessionInfo = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetSessionInfo");
            ckFunctionList.C_GetOperationState = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetOperationState");
            ckFunctionList.C_SetOperationState = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SetOperationState");
            ckFunctionList.C_Login = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Login");
            ckFunctionList.C_Logout = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Logout");
            ckFunctionList.C_CreateObject = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_CreateObject");
            ckFunctionList.C_CopyObject = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_CopyObject");
            ckFunctionList.C_DestroyObject = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DestroyObject");
            ckFunctionList.C_GetObjectSize = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetObjectSize");
            ckFunctionList.C_GetAttributeValue = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetAttributeValue");
            ckFunctionList.C_SetAttributeValue = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SetAttributeValue");
            ckFunctionList.C_FindObjectsInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_FindObjectsInit");
            ckFunctionList.C_FindObjects = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_FindObjects");
            ckFunctionList.C_FindObjectsFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_FindObjectsFinal");
            ckFunctionList.C_EncryptInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_EncryptInit");
            ckFunctionList.C_Encrypt = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Encrypt");
            ckFunctionList.C_EncryptUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_EncryptUpdate");
            ckFunctionList.C_EncryptFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_EncryptFinal");
            ckFunctionList.C_DecryptInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DecryptInit");
            ckFunctionList.C_Decrypt = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Decrypt");
            ckFunctionList.C_DecryptUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DecryptUpdate");
            ckFunctionList.C_DecryptFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DecryptFinal");
            ckFunctionList.C_DigestInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DigestInit");
            ckFunctionList.C_Digest = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Digest");
            ckFunctionList.C_DigestUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DigestUpdate");
            ckFunctionList.C_DigestKey = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DigestKey");
            ckFunctionList.C_DigestFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DigestFinal");
            ckFunctionList.C_SignInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignInit");
            ckFunctionList.C_Sign = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Sign");
            ckFunctionList.C_SignUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignUpdate");
            ckFunctionList.C_SignFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignFinal");
            ckFunctionList.C_SignRecoverInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignRecoverInit");
            ckFunctionList.C_SignRecover = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignRecover");
            ckFunctionList.C_VerifyInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_VerifyInit");
            ckFunctionList.C_Verify = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_Verify");
            ckFunctionList.C_VerifyUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_VerifyUpdate");
            ckFunctionList.C_VerifyFinal = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_VerifyFinal");
            ckFunctionList.C_VerifyRecoverInit = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_VerifyRecoverInit");
            ckFunctionList.C_VerifyRecover = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_VerifyRecover");
            ckFunctionList.C_DigestEncryptUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DigestEncryptUpdate");
            ckFunctionList.C_DecryptDigestUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DecryptDigestUpdate");
            ckFunctionList.C_SignEncryptUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SignEncryptUpdate");
            ckFunctionList.C_DecryptVerifyUpdate = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DecryptVerifyUpdate");
            ckFunctionList.C_GenerateKey = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GenerateKey");
            ckFunctionList.C_GenerateKeyPair = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GenerateKeyPair");
            ckFunctionList.C_WrapKey = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_WrapKey");
            ckFunctionList.C_UnwrapKey = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_UnwrapKey");
            ckFunctionList.C_DeriveKey = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_DeriveKey");
            ckFunctionList.C_SeedRandom = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_SeedRandom");
            ckFunctionList.C_GenerateRandom = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GenerateRandom");
            ckFunctionList.C_GetFunctionStatus = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_GetFunctionStatus");
            ckFunctionList.C_CancelFunction = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_CancelFunction");
            ckFunctionList.C_WaitForSlotEvent = UnmanagedLibrary.GetFunctionPointer(libraryHandle, "C_WaitForSlotEvent");

            Initialize(ckFunctionList);
        }

        /// <summary>
        /// Get delegates without C_GetFunctionList function from the statically linked PKCS#11 library
        /// </summary>
        private void InitializeWithoutGetFunctionList()
        {
            C_Initialize = NativeMethods.C_Initialize;
            C_Finalize = NativeMethods.C_Finalize;
            C_GetInfo = NativeMethods.C_GetInfo;
            C_GetFunctionList = NativeMethods.C_GetFunctionList;
            C_GetSlotList = NativeMethods.C_GetSlotList;
            C_GetSlotInfo = NativeMethods.C_GetSlotInfo;
            C_GetTokenInfo = NativeMethods.C_GetTokenInfo;
            C_GetMechanismList = NativeMethods.C_GetMechanismList;
            C_GetMechanismInfo = NativeMethods.C_GetMechanismInfo;
            C_InitToken = NativeMethods.C_InitToken;
            C_InitPIN = NativeMethods.C_InitPIN;
            C_SetPIN = NativeMethods.C_SetPIN;
            C_OpenSession = NativeMethods.C_OpenSession;
            C_CloseSession = NativeMethods.C_CloseSession;
            C_CloseAllSessions = NativeMethods.C_CloseAllSessions;
            C_GetSessionInfo = NativeMethods.C_GetSessionInfo;
            C_GetOperationState = NativeMethods.C_GetOperationState;
            C_SetOperationState = NativeMethods.C_SetOperationState;
            C_Login = NativeMethods.C_Login;
            C_Logout = NativeMethods.C_Logout;
            C_CreateObject = NativeMethods.C_CreateObject;
            C_CopyObject = NativeMethods.C_CopyObject;
            C_DestroyObject = NativeMethods.C_DestroyObject;
            C_GetObjectSize = NativeMethods.C_GetObjectSize;
            C_GetAttributeValue = NativeMethods.C_GetAttributeValue;
            C_SetAttributeValue = NativeMethods.C_SetAttributeValue;
            C_FindObjectsInit = NativeMethods.C_FindObjectsInit;
            C_FindObjects = NativeMethods.C_FindObjects;
            C_FindObjectsFinal = NativeMethods.C_FindObjectsFinal;
            C_EncryptInit = NativeMethods.C_EncryptInit;
            C_Encrypt = NativeMethods.C_Encrypt;
            C_EncryptUpdate = NativeMethods.C_EncryptUpdate;
            C_EncryptFinal = NativeMethods.C_EncryptFinal;
            C_DecryptInit = NativeMethods.C_DecryptInit;
            C_Decrypt = NativeMethods.C_Decrypt;
            C_DecryptUpdate = NativeMethods.C_DecryptUpdate;
            C_DecryptFinal = NativeMethods.C_DecryptFinal;
            C_DigestInit = NativeMethods.C_DigestInit;
            C_Digest = NativeMethods.C_Digest;
            C_DigestUpdate = NativeMethods.C_DigestUpdate;
            C_DigestKey = NativeMethods.C_DigestKey;
            C_DigestFinal = NativeMethods.C_DigestFinal;
            C_SignInit = NativeMethods.C_SignInit;
            C_Sign = NativeMethods.C_Sign;
            C_SignUpdate = NativeMethods.C_SignUpdate;
            C_SignFinal = NativeMethods.C_SignFinal;
            C_SignRecoverInit = NativeMethods.C_SignRecoverInit;
            C_SignRecover = NativeMethods.C_SignRecover;
            C_VerifyInit = NativeMethods.C_VerifyInit;
            C_Verify = NativeMethods.C_Verify;
            C_VerifyUpdate = NativeMethods.C_VerifyUpdate;
            C_VerifyFinal = NativeMethods.C_VerifyFinal;
            C_VerifyRecoverInit = NativeMethods.C_VerifyRecoverInit;
            C_VerifyRecover = NativeMethods.C_VerifyRecover;
            C_DigestEncryptUpdate = NativeMethods.C_DigestEncryptUpdate;
            C_DecryptDigestUpdate = NativeMethods.C_DecryptDigestUpdate;
            C_SignEncryptUpdate = NativeMethods.C_SignEncryptUpdate;
            C_DecryptVerifyUpdate = NativeMethods.C_DecryptVerifyUpdate;
            C_GenerateKey = NativeMethods.C_GenerateKey;
            C_GenerateKeyPair = NativeMethods.C_GenerateKeyPair;
            C_WrapKey = NativeMethods.C_WrapKey;
            C_UnwrapKey = NativeMethods.C_UnwrapKey;
            C_DeriveKey = NativeMethods.C_DeriveKey;
            C_SeedRandom = NativeMethods.C_SeedRandom;
            C_GenerateRandom = NativeMethods.C_GenerateRandom;
            C_GetFunctionStatus = NativeMethods.C_GetFunctionStatus;
            C_CancelFunction = NativeMethods.C_CancelFunction;
            C_WaitForSlotEvent = NativeMethods.C_WaitForSlotEvent;
        }

        /// <summary>
        /// Get delegates from unmanaged function pointers
        /// </summary>
        /// <param name="ckFunctionList">Structure which contains cryptoki function pointers</param>
        private void Initialize(CK_FUNCTION_LIST ckFunctionList)
        {
            C_Initialize = (C_InitializeDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Initialize, typeof(C_InitializeDelegate));
            C_Finalize = (C_FinalizeDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Finalize, typeof(C_FinalizeDelegate));
            C_GetInfo = (C_GetInfoDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetInfo, typeof(C_GetInfoDelegate));
            C_GetFunctionList = (C_GetFunctionListDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetFunctionList, typeof(C_GetFunctionListDelegate));
            C_GetSlotList = (C_GetSlotListDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetSlotList, typeof(C_GetSlotListDelegate));
            C_GetSlotInfo = (C_GetSlotInfoDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetSlotInfo, typeof(C_GetSlotInfoDelegate));
            C_GetTokenInfo = (C_GetTokenInfoDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetTokenInfo, typeof(C_GetTokenInfoDelegate));
            C_GetMechanismList = (C_GetMechanismListDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetMechanismList, typeof(C_GetMechanismListDelegate));
            C_GetMechanismInfo = (C_GetMechanismInfoDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetMechanismInfo, typeof(C_GetMechanismInfoDelegate));
            C_InitToken = (C_InitTokenDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_InitToken, typeof(C_InitTokenDelegate));
            C_InitPIN = (C_InitPINDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_InitPIN, typeof(C_InitPINDelegate));
            C_SetPIN = (C_SetPINDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SetPIN, typeof(C_SetPINDelegate));
            C_OpenSession = (C_OpenSessionDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_OpenSession, typeof(C_OpenSessionDelegate));
            C_CloseSession = (C_CloseSessionDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_CloseSession, typeof(C_CloseSessionDelegate));
            C_CloseAllSessions = (C_CloseAllSessionsDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_CloseAllSessions, typeof(C_CloseAllSessionsDelegate));
            C_GetSessionInfo = (C_GetSessionInfoDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetSessionInfo, typeof(C_GetSessionInfoDelegate));
            C_GetOperationState = (C_GetOperationStateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetOperationState, typeof(C_GetOperationStateDelegate));
            C_SetOperationState = (C_SetOperationStateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SetOperationState, typeof(C_SetOperationStateDelegate));
            C_Login = (C_LoginDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Login, typeof(C_LoginDelegate));
            C_Logout = (C_LogoutDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Logout, typeof(C_LogoutDelegate));
            C_CreateObject = (C_CreateObjectDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_CreateObject, typeof(C_CreateObjectDelegate));
            C_CopyObject = (C_CopyObjectDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_CopyObject, typeof(C_CopyObjectDelegate));
            C_DestroyObject = (C_DestroyObjectDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DestroyObject, typeof(C_DestroyObjectDelegate));
            C_GetObjectSize = (C_GetObjectSizeDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetObjectSize, typeof(C_GetObjectSizeDelegate));
            C_GetAttributeValue = (C_GetAttributeValueDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetAttributeValue, typeof(C_GetAttributeValueDelegate));
            C_SetAttributeValue = (C_SetAttributeValueDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SetAttributeValue, typeof(C_SetAttributeValueDelegate));
            C_FindObjectsInit = (C_FindObjectsInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_FindObjectsInit, typeof(C_FindObjectsInitDelegate));
            C_FindObjects = (C_FindObjectsDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_FindObjects, typeof(C_FindObjectsDelegate));
            C_FindObjectsFinal = (C_FindObjectsFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_FindObjectsFinal, typeof(C_FindObjectsFinalDelegate));
            C_EncryptInit = (C_EncryptInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_EncryptInit, typeof(C_EncryptInitDelegate));
            C_Encrypt = (C_EncryptDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Encrypt, typeof(C_EncryptDelegate));
            C_EncryptUpdate = (C_EncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_EncryptUpdate, typeof(C_EncryptUpdateDelegate));
            C_EncryptFinal = (C_EncryptFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_EncryptFinal, typeof(C_EncryptFinalDelegate));
            C_DecryptInit = (C_DecryptInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DecryptInit, typeof(C_DecryptInitDelegate));
            C_Decrypt = (C_DecryptDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Decrypt, typeof(C_DecryptDelegate));
            C_DecryptUpdate = (C_DecryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DecryptUpdate, typeof(C_DecryptUpdateDelegate));
            C_DecryptFinal = (C_DecryptFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DecryptFinal, typeof(C_DecryptFinalDelegate));
            C_DigestInit = (C_DigestInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DigestInit, typeof(C_DigestInitDelegate));
            C_Digest = (C_DigestDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Digest, typeof(C_DigestDelegate));
            C_DigestUpdate = (C_DigestUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DigestUpdate, typeof(C_DigestUpdateDelegate));
            C_DigestKey = (C_DigestKeyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DigestKey, typeof(C_DigestKeyDelegate));
            C_DigestFinal = (C_DigestFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DigestFinal, typeof(C_DigestFinalDelegate));
            C_SignInit = (C_SignInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignInit, typeof(C_SignInitDelegate));
            C_Sign = (C_SignDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Sign, typeof(C_SignDelegate));
            C_SignUpdate = (C_SignUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignUpdate, typeof(C_SignUpdateDelegate));
            C_SignFinal = (C_SignFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignFinal, typeof(C_SignFinalDelegate));
            C_SignRecoverInit = (C_SignRecoverInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignRecoverInit, typeof(C_SignRecoverInitDelegate));
            C_SignRecover = (C_SignRecoverDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignRecover, typeof(C_SignRecoverDelegate));
            C_VerifyInit = (C_VerifyInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_VerifyInit, typeof(C_VerifyInitDelegate));
            C_Verify = (C_VerifyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_Verify, typeof(C_VerifyDelegate));
            C_VerifyUpdate = (C_VerifyUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_VerifyUpdate, typeof(C_VerifyUpdateDelegate));
            C_VerifyFinal = (C_VerifyFinalDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_VerifyFinal, typeof(C_VerifyFinalDelegate));
            C_VerifyRecoverInit = (C_VerifyRecoverInitDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_VerifyRecoverInit, typeof(C_VerifyRecoverInitDelegate));
            C_VerifyRecover = (C_VerifyRecoverDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_VerifyRecover, typeof(C_VerifyRecoverDelegate));
            C_DigestEncryptUpdate = (C_DigestEncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DigestEncryptUpdate, typeof(C_DigestEncryptUpdateDelegate));
            C_DecryptDigestUpdate = (C_DecryptDigestUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DecryptDigestUpdate, typeof(C_DecryptDigestUpdateDelegate));
            C_SignEncryptUpdate = (C_SignEncryptUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SignEncryptUpdate, typeof(C_SignEncryptUpdateDelegate));
            C_DecryptVerifyUpdate = (C_DecryptVerifyUpdateDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DecryptVerifyUpdate, typeof(C_DecryptVerifyUpdateDelegate));
            C_GenerateKey = (C_GenerateKeyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GenerateKey, typeof(C_GenerateKeyDelegate));
            C_GenerateKeyPair = (C_GenerateKeyPairDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GenerateKeyPair, typeof(C_GenerateKeyPairDelegate));
            C_WrapKey = (C_WrapKeyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_WrapKey, typeof(C_WrapKeyDelegate));
            C_UnwrapKey = (C_UnwrapKeyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_UnwrapKey, typeof(C_UnwrapKeyDelegate));
            C_DeriveKey = (C_DeriveKeyDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_DeriveKey, typeof(C_DeriveKeyDelegate));
            C_SeedRandom = (C_SeedRandomDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_SeedRandom, typeof(C_SeedRandomDelegate));
            C_GenerateRandom = (C_GenerateRandomDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GenerateRandom, typeof(C_GenerateRandomDelegate));
            C_GetFunctionStatus = (C_GetFunctionStatusDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_GetFunctionStatus, typeof(C_GetFunctionStatusDelegate));
            C_CancelFunction = (C_CancelFunctionDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_CancelFunction, typeof(C_CancelFunctionDelegate));
            C_WaitForSlotEvent = (C_WaitForSlotEventDelegate)Marshal.GetDelegateForFunctionPointer(ckFunctionList.C_WaitForSlotEvent, typeof(C_WaitForSlotEventDelegate));
        }
    }
}
