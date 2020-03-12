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
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    #region Delegate definitions

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_InitializeDelegate(CK_C_INITIALIZE_ARGS initArgs);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_FinalizeDelegate(IntPtr reserved);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetInfoDelegate(ref CK_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetFunctionListDelegate(out IntPtr functionList);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetSlotListDelegate([MarshalAs(UnmanagedType.U1)] bool tokenPresent, NativeULong[] slotList, ref NativeULong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetSlotInfoDelegate(NativeULong slotId, ref CK_SLOT_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetTokenInfoDelegate(NativeULong slotId, ref CK_TOKEN_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetMechanismListDelegate(NativeULong slotId, NativeULong[] mechanismList, ref NativeULong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetMechanismInfoDelegate(NativeULong slotId, NativeULong type, ref CK_MECHANISM_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_InitTokenDelegate(NativeULong slotId, byte[] pin, NativeULong pinLen, byte[] label);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_InitPINDelegate(NativeULong session, byte[] pin, NativeULong pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SetPINDelegate(NativeULong session, byte[] oldPin, NativeULong oldPinLen, byte[] newPin, NativeULong newPinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_OpenSessionDelegate(NativeULong slotId, NativeULong flags, IntPtr application, IntPtr notify, ref NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_CloseSessionDelegate(NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_CloseAllSessionsDelegate(NativeULong slotId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetSessionInfoDelegate(NativeULong session, ref CK_SESSION_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetOperationStateDelegate(NativeULong session, byte[] operationState, ref NativeULong operationStateLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SetOperationStateDelegate(NativeULong session, byte[] operationState, NativeULong operationStateLen, NativeULong encryptionKey, NativeULong authenticationKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_LoginDelegate(NativeULong session, NativeULong userType, byte[] pin, NativeULong pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_LogoutDelegate(NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_CreateObjectDelegate(NativeULong session, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_CopyObjectDelegate(NativeULong session, NativeULong objectId, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong newObjectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DestroyObjectDelegate(NativeULong session, NativeULong objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetObjectSizeDelegate(NativeULong session, NativeULong objectId, ref NativeULong size);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetAttributeValueDelegate(NativeULong session, NativeULong objectId, [In, Out] CK_ATTRIBUTE[] template, NativeULong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SetAttributeValueDelegate(NativeULong session, NativeULong objectId, CK_ATTRIBUTE[] template, NativeULong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_FindObjectsInitDelegate(NativeULong session, CK_ATTRIBUTE[] template, NativeULong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_FindObjectsDelegate(NativeULong session, NativeULong[] objectId, NativeULong maxObjectCount, ref NativeULong objectCount);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_FindObjectsFinalDelegate(NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_EncryptInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_EncryptDelegate(NativeULong session, byte[] data, NativeULong dataLen, byte[] encryptedData, ref NativeULong encryptedDataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_EncryptUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_EncryptFinalDelegate(NativeULong session, byte[] lastEncryptedPart, ref NativeULong lastEncryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptDelegate(NativeULong session, byte[] encryptedData, NativeULong encryptedDataLen, byte[] data, ref NativeULong dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptUpdateDelegate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptFinalDelegate(NativeULong session, byte[] lastPart, ref NativeULong lastPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestInitDelegate(NativeULong session, ref CK_MECHANISM mechanism);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestDelegate(NativeULong session, byte[] data, NativeULong dataLen, byte[] digest, ref NativeULong digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestKeyDelegate(NativeULong session, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestFinalDelegate(NativeULong session, byte[] digest, ref NativeULong digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignDelegate(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, ref NativeULong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignFinalDelegate(NativeULong session, byte[] signature, ref NativeULong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignRecoverInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignRecoverDelegate(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, ref NativeULong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyDelegate(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, NativeULong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyFinalDelegate(NativeULong session, byte[] signature, NativeULong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyRecoverInitDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_VerifyRecoverDelegate(NativeULong session, byte[] signature, NativeULong signatureLen, byte[] data, ref NativeULong dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DigestEncryptUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptDigestUpdateDelegate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SignEncryptUpdateDelegate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DecryptVerifyUpdateDelegate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GenerateKeyDelegate(NativeULong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GenerateKeyPairDelegate(NativeULong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, NativeULong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, NativeULong privateKeyAttributeCount, ref NativeULong publicKey, ref NativeULong privateKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_WrapKeyDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong wrappingKey, NativeULong key, byte[] wrappedKey, ref NativeULong wrappedKeyLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_UnwrapKeyDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong unwrappingKey, byte[] wrappedKey, NativeULong wrappedKeyLen, CK_ATTRIBUTE[] template, NativeULong attributeCount, ref NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_DeriveKeyDelegate(NativeULong session, ref CK_MECHANISM mechanism, NativeULong baseKey, CK_ATTRIBUTE[] template, NativeULong attributeCount, ref NativeULong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_SeedRandomDelegate(NativeULong session, byte[] seed, NativeULong seedLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GenerateRandomDelegate(NativeULong session, byte[] randomData, NativeULong randomLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_GetFunctionStatusDelegate(NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_CancelFunctionDelegate(NativeULong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate NativeULong C_WaitForSlotEventDelegate(NativeULong flags, ref NativeULong slot, IntPtr reserved);

    #endregion

    /// <summary>
    /// Holds delegates for all PKCS#11 functions
    /// </summary>
    internal class Delegates
    {
        /// <summary>
        /// Definition of unmanaged methods (used on iOS)
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Initialize(CK_C_INITIALIZE_ARGS initArgs);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Finalize(IntPtr reserved);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetInfo(ref CK_INFO info);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetFunctionList(out IntPtr functionList);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetSlotList([MarshalAs(UnmanagedType.U1)] bool tokenPresent, NativeULong[] slotList, ref NativeULong count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetSlotInfo(NativeULong slotId, ref CK_SLOT_INFO info);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetTokenInfo(NativeULong slotId, ref CK_TOKEN_INFO info);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetMechanismList(NativeULong slotId, NativeULong[] mechanismList, ref NativeULong count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetMechanismInfo(NativeULong slotId, NativeULong type, ref CK_MECHANISM_INFO info);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_InitToken(NativeULong slotId, byte[] pin, NativeULong pinLen, byte[] label);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_InitPIN(NativeULong session, byte[] pin, NativeULong pinLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SetPIN(NativeULong session, byte[] oldPin, NativeULong oldPinLen, byte[] newPin, NativeULong newPinLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_OpenSession(NativeULong slotId, NativeULong flags, IntPtr application, IntPtr notify, ref NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_CloseSession(NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_CloseAllSessions(NativeULong slotId);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetSessionInfo(NativeULong session, ref CK_SESSION_INFO info);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetOperationState(NativeULong session, byte[] operationState, ref NativeULong operationStateLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SetOperationState(NativeULong session, byte[] operationState, NativeULong operationStateLen, NativeULong encryptionKey, NativeULong authenticationKey);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Login(NativeULong session, NativeULong userType, byte[] pin, NativeULong pinLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Logout(NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_CreateObject(NativeULong session, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong objectId);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_CopyObject(NativeULong session, NativeULong objectId, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong newObjectId);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DestroyObject(NativeULong session, NativeULong objectId);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetObjectSize(NativeULong session, NativeULong objectId, ref NativeULong size);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetAttributeValue(NativeULong session, NativeULong objectId, [In, Out] CK_ATTRIBUTE[] template, NativeULong count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SetAttributeValue(NativeULong session, NativeULong objectId, CK_ATTRIBUTE[] template, NativeULong count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_FindObjectsInit(NativeULong session, CK_ATTRIBUTE[] template, NativeULong count);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_FindObjects(NativeULong session, NativeULong[] objectId, NativeULong maxObjectCount, ref NativeULong objectCount);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_FindObjectsFinal(NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_EncryptInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Encrypt(NativeULong session, byte[] data, NativeULong dataLen, byte[] encryptedData, ref NativeULong encryptedDataLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_EncryptUpdate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_EncryptFinal(NativeULong session, byte[] lastEncryptedPart, ref NativeULong lastEncryptedPartLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DecryptInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Decrypt(NativeULong session, byte[] encryptedData, NativeULong encryptedDataLen, byte[] data, ref NativeULong dataLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DecryptUpdate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DecryptFinal(NativeULong session, byte[] lastPart, ref NativeULong lastPartLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DigestInit(NativeULong session, ref CK_MECHANISM mechanism);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Digest(NativeULong session, byte[] data, NativeULong dataLen, byte[] digest, ref NativeULong digestLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DigestUpdate(NativeULong session, byte[] part, NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DigestKey(NativeULong session, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DigestFinal(NativeULong session, byte[] digest, ref NativeULong digestLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Sign(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, ref NativeULong signatureLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignUpdate(NativeULong session, byte[] part, NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignFinal(NativeULong session, byte[] signature, ref NativeULong signatureLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignRecoverInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignRecover(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, ref NativeULong signatureLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_VerifyInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_Verify(NativeULong session, byte[] data, NativeULong dataLen, byte[] signature, NativeULong signatureLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_VerifyUpdate(NativeULong session, byte[] part, NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_VerifyFinal(NativeULong session, byte[] signature, NativeULong signatureLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_VerifyRecoverInit(NativeULong session, ref CK_MECHANISM mechanism, NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_VerifyRecover(NativeULong session, byte[] signature, NativeULong signatureLen, byte[] data, ref NativeULong dataLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DigestEncryptUpdate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DecryptDigestUpdate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SignEncryptUpdate(NativeULong session, byte[] part, NativeULong partLen, byte[] encryptedPart, ref NativeULong encryptedPartLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DecryptVerifyUpdate(NativeULong session, byte[] encryptedPart, NativeULong encryptedPartLen, byte[] part, ref NativeULong partLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GenerateKey(NativeULong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, NativeULong count, ref NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GenerateKeyPair(NativeULong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, NativeULong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, NativeULong privateKeyAttributeCount, ref NativeULong publicKey, ref NativeULong privateKey);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_WrapKey(NativeULong session, ref CK_MECHANISM mechanism, NativeULong wrappingKey, NativeULong key, byte[] wrappedKey, ref NativeULong wrappedKeyLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_UnwrapKey(NativeULong session, ref CK_MECHANISM mechanism, NativeULong unwrappingKey, byte[] wrappedKey, NativeULong wrappedKeyLen, CK_ATTRIBUTE[] template, NativeULong attributeCount, ref NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_DeriveKey(NativeULong session, ref CK_MECHANISM mechanism, NativeULong baseKey, CK_ATTRIBUTE[] template, NativeULong attributeCount, ref NativeULong key);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_SeedRandom(NativeULong session, byte[] seed, NativeULong seedLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GenerateRandom(NativeULong session, byte[] randomData, NativeULong randomLen);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_GetFunctionStatus(NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_CancelFunction(NativeULong session);

            [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern NativeULong C_WaitForSlotEvent(NativeULong flags, ref NativeULong slot, IntPtr reserved);
        }

        #region Delegate instances

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

        #endregion

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
            C_GetFunctionListDelegate cGetFunctionList = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetFunctionListDelegate>(cGetFunctionListPtr);

            IntPtr functionList = IntPtr.Zero;

            CKR rv = ConvertUtils.UInt64ToCKR(cGetFunctionList(out functionList));
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

            CKR rv = ConvertUtils.UInt64ToCKR(NativeMethods.C_GetFunctionList(out functionList));
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
            C_Initialize = UnmanagedLibrary.GetDelegateForFunctionPointer<C_InitializeDelegate>(ckFunctionList.C_Initialize);
            C_Finalize = UnmanagedLibrary.GetDelegateForFunctionPointer<C_FinalizeDelegate>(ckFunctionList.C_Finalize);
            C_GetInfo = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetInfoDelegate>(ckFunctionList.C_GetInfo);
            C_GetFunctionList = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetFunctionListDelegate>(ckFunctionList.C_GetFunctionList);
            C_GetSlotList = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetSlotListDelegate>(ckFunctionList.C_GetSlotList);
            C_GetSlotInfo = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetSlotInfoDelegate>(ckFunctionList.C_GetSlotInfo);
            C_GetTokenInfo = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetTokenInfoDelegate>(ckFunctionList.C_GetTokenInfo);
            C_GetMechanismList = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetMechanismListDelegate>(ckFunctionList.C_GetMechanismList);
            C_GetMechanismInfo = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetMechanismInfoDelegate>(ckFunctionList.C_GetMechanismInfo);
            C_InitToken = UnmanagedLibrary.GetDelegateForFunctionPointer<C_InitTokenDelegate>(ckFunctionList.C_InitToken);
            C_InitPIN = UnmanagedLibrary.GetDelegateForFunctionPointer<C_InitPINDelegate>(ckFunctionList.C_InitPIN);
            C_SetPIN = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SetPINDelegate>(ckFunctionList.C_SetPIN);
            C_OpenSession = UnmanagedLibrary.GetDelegateForFunctionPointer<C_OpenSessionDelegate>(ckFunctionList.C_OpenSession);
            C_CloseSession = UnmanagedLibrary.GetDelegateForFunctionPointer<C_CloseSessionDelegate>(ckFunctionList.C_CloseSession);
            C_CloseAllSessions = UnmanagedLibrary.GetDelegateForFunctionPointer<C_CloseAllSessionsDelegate>(ckFunctionList.C_CloseAllSessions);
            C_GetSessionInfo = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetSessionInfoDelegate>(ckFunctionList.C_GetSessionInfo);
            C_GetOperationState = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetOperationStateDelegate>(ckFunctionList.C_GetOperationState);
            C_SetOperationState = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SetOperationStateDelegate>(ckFunctionList.C_SetOperationState);
            C_Login = UnmanagedLibrary.GetDelegateForFunctionPointer<C_LoginDelegate>(ckFunctionList.C_Login);
            C_Logout = UnmanagedLibrary.GetDelegateForFunctionPointer<C_LogoutDelegate>(ckFunctionList.C_Logout);
            C_CreateObject = UnmanagedLibrary.GetDelegateForFunctionPointer<C_CreateObjectDelegate>(ckFunctionList.C_CreateObject);
            C_CopyObject = UnmanagedLibrary.GetDelegateForFunctionPointer<C_CopyObjectDelegate>(ckFunctionList.C_CopyObject);
            C_DestroyObject = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DestroyObjectDelegate>(ckFunctionList.C_DestroyObject);
            C_GetObjectSize = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetObjectSizeDelegate>(ckFunctionList.C_GetObjectSize);
            C_GetAttributeValue = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetAttributeValueDelegate>(ckFunctionList.C_GetAttributeValue);
            C_SetAttributeValue = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SetAttributeValueDelegate>(ckFunctionList.C_SetAttributeValue);
            C_FindObjectsInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_FindObjectsInitDelegate>(ckFunctionList.C_FindObjectsInit);
            C_FindObjects = UnmanagedLibrary.GetDelegateForFunctionPointer<C_FindObjectsDelegate>(ckFunctionList.C_FindObjects);
            C_FindObjectsFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_FindObjectsFinalDelegate>(ckFunctionList.C_FindObjectsFinal);
            C_EncryptInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_EncryptInitDelegate>(ckFunctionList.C_EncryptInit);
            C_Encrypt = UnmanagedLibrary.GetDelegateForFunctionPointer<C_EncryptDelegate>(ckFunctionList.C_Encrypt);
            C_EncryptUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_EncryptUpdateDelegate>(ckFunctionList.C_EncryptUpdate);
            C_EncryptFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_EncryptFinalDelegate>(ckFunctionList.C_EncryptFinal);
            C_DecryptInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptInitDelegate>(ckFunctionList.C_DecryptInit);
            C_Decrypt = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptDelegate>(ckFunctionList.C_Decrypt);
            C_DecryptUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptUpdateDelegate>(ckFunctionList.C_DecryptUpdate);
            C_DecryptFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptFinalDelegate>(ckFunctionList.C_DecryptFinal);
            C_DigestInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestInitDelegate>(ckFunctionList.C_DigestInit);
            C_Digest = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestDelegate>(ckFunctionList.C_Digest);
            C_DigestUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestUpdateDelegate>(ckFunctionList.C_DigestUpdate);
            C_DigestKey = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestKeyDelegate>(ckFunctionList.C_DigestKey);
            C_DigestFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestFinalDelegate>(ckFunctionList.C_DigestFinal);
            C_SignInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignInitDelegate>(ckFunctionList.C_SignInit);
            C_Sign = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignDelegate>(ckFunctionList.C_Sign);
            C_SignUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignUpdateDelegate>(ckFunctionList.C_SignUpdate);
            C_SignFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignFinalDelegate>(ckFunctionList.C_SignFinal);
            C_SignRecoverInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignRecoverInitDelegate>(ckFunctionList.C_SignRecoverInit);
            C_SignRecover = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignRecoverDelegate>(ckFunctionList.C_SignRecover);
            C_VerifyInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyInitDelegate>(ckFunctionList.C_VerifyInit);
            C_Verify = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyDelegate>(ckFunctionList.C_Verify);
            C_VerifyUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyUpdateDelegate>(ckFunctionList.C_VerifyUpdate);
            C_VerifyFinal = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyFinalDelegate>(ckFunctionList.C_VerifyFinal);
            C_VerifyRecoverInit = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyRecoverInitDelegate>(ckFunctionList.C_VerifyRecoverInit);
            C_VerifyRecover = UnmanagedLibrary.GetDelegateForFunctionPointer<C_VerifyRecoverDelegate>(ckFunctionList.C_VerifyRecover);
            C_DigestEncryptUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DigestEncryptUpdateDelegate>(ckFunctionList.C_DigestEncryptUpdate);
            C_DecryptDigestUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptDigestUpdateDelegate>(ckFunctionList.C_DecryptDigestUpdate);
            C_SignEncryptUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SignEncryptUpdateDelegate>(ckFunctionList.C_SignEncryptUpdate);
            C_DecryptVerifyUpdate = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DecryptVerifyUpdateDelegate>(ckFunctionList.C_DecryptVerifyUpdate);
            C_GenerateKey = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GenerateKeyDelegate>(ckFunctionList.C_GenerateKey);
            C_GenerateKeyPair = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GenerateKeyPairDelegate>(ckFunctionList.C_GenerateKeyPair);
            C_WrapKey = UnmanagedLibrary.GetDelegateForFunctionPointer<C_WrapKeyDelegate>(ckFunctionList.C_WrapKey);
            C_UnwrapKey = UnmanagedLibrary.GetDelegateForFunctionPointer<C_UnwrapKeyDelegate>(ckFunctionList.C_UnwrapKey);
            C_DeriveKey = UnmanagedLibrary.GetDelegateForFunctionPointer<C_DeriveKeyDelegate>(ckFunctionList.C_DeriveKey);
            C_SeedRandom = UnmanagedLibrary.GetDelegateForFunctionPointer<C_SeedRandomDelegate>(ckFunctionList.C_SeedRandom);
            C_GenerateRandom = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GenerateRandomDelegate>(ckFunctionList.C_GenerateRandom);
            C_GetFunctionStatus = UnmanagedLibrary.GetDelegateForFunctionPointer<C_GetFunctionStatusDelegate>(ckFunctionList.C_GetFunctionStatus);
            C_CancelFunction = UnmanagedLibrary.GetDelegateForFunctionPointer<C_CancelFunctionDelegate>(ckFunctionList.C_CancelFunction);
            C_WaitForSlotEvent = UnmanagedLibrary.GetDelegateForFunctionPointer<C_WaitForSlotEventDelegate>(ckFunctionList.C_WaitForSlotEvent);
        }
    }
}
