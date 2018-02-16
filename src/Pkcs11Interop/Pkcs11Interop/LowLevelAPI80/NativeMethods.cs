/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using NativeLong = System.UInt64;

namespace Net.Pkcs11Interop.LowLevelAPI80
{
    internal static class NativeMethods
    {
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Initialize(CK_C_INITIALIZE_ARGS initArgs);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Finalize(IntPtr reserved);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetInfo(ref CK_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetFunctionList(out IntPtr functionList);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetSlotList([MarshalAs(UnmanagedType.U1)] bool tokenPresent, NativeLong[] slotList, ref NativeLong count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetSlotInfo(NativeLong slotId, ref CK_SLOT_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetTokenInfo(NativeLong slotId, ref CK_TOKEN_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetMechanismList(NativeLong slotId, NativeLong[] mechanismList, ref NativeLong count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetMechanismInfo(NativeLong slotId, NativeLong type, ref CK_MECHANISM_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_InitToken(NativeLong slotId, byte[] pin, NativeLong pinLen, byte[] label);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_InitPIN(NativeLong session, byte[] pin, NativeLong pinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SetPIN(NativeLong session, byte[] oldPin, NativeLong oldPinLen, byte[] newPin, NativeLong newPinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_OpenSession(NativeLong slotId, NativeLong flags, IntPtr application, IntPtr notify, ref NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_CloseSession(NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_CloseAllSessions(NativeLong slotId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetSessionInfo(NativeLong session, ref CK_SESSION_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetOperationState(NativeLong session, byte[] operationState, ref NativeLong operationStateLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SetOperationState(NativeLong session, byte[] operationState, NativeLong operationStateLen, NativeLong encryptionKey, NativeLong authenticationKey);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Login(NativeLong session, NativeLong userType, byte[] pin, NativeLong pinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Logout(NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_CreateObject(NativeLong session, CK_ATTRIBUTE[] template, NativeLong count, ref NativeLong objectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_CopyObject(NativeLong session, NativeLong objectId, CK_ATTRIBUTE[] template, NativeLong count, ref NativeLong newObjectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DestroyObject(NativeLong session, NativeLong objectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetObjectSize(NativeLong session, NativeLong objectId, ref NativeLong size);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetAttributeValue(NativeLong session, NativeLong objectId, [In, Out] CK_ATTRIBUTE[] template, NativeLong count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SetAttributeValue(NativeLong session, NativeLong objectId, CK_ATTRIBUTE[] template, NativeLong count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_FindObjectsInit(NativeLong session, CK_ATTRIBUTE[] template, NativeLong count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_FindObjects(NativeLong session, NativeLong[] objectId, NativeLong maxObjectCount, ref NativeLong objectCount);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_FindObjectsFinal(NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_EncryptInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Encrypt(NativeLong session, byte[] data, NativeLong dataLen, byte[] encryptedData, ref NativeLong encryptedDataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_EncryptUpdate(NativeLong session, byte[] part, NativeLong partLen, byte[] encryptedPart, ref NativeLong encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_EncryptFinal(NativeLong session, byte[] lastEncryptedPart, ref NativeLong lastEncryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DecryptInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Decrypt(NativeLong session, byte[] encryptedData, NativeLong encryptedDataLen, byte[] data, ref NativeLong dataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DecryptUpdate(NativeLong session, byte[] encryptedPart, NativeLong encryptedPartLen, byte[] part, ref NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DecryptFinal(NativeLong session, byte[] lastPart, ref NativeLong lastPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DigestInit(NativeLong session, ref CK_MECHANISM mechanism);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Digest(NativeLong session, byte[] data, NativeLong dataLen, byte[] digest, ref NativeLong digestLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DigestUpdate(NativeLong session, byte[] part, NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DigestKey(NativeLong session, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DigestFinal(NativeLong session, byte[] digest, ref NativeLong digestLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Sign(NativeLong session, byte[] data, NativeLong dataLen, byte[] signature, ref NativeLong signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignUpdate(NativeLong session, byte[] part, NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignFinal(NativeLong session, byte[] signature, ref NativeLong signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignRecoverInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignRecover(NativeLong session, byte[] data, NativeLong dataLen, byte[] signature, ref NativeLong signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_VerifyInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_Verify(NativeLong session, byte[] data, NativeLong dataLen, byte[] signature, NativeLong signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_VerifyUpdate(NativeLong session, byte[] part, NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_VerifyFinal(NativeLong session, byte[] signature, NativeLong signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_VerifyRecoverInit(NativeLong session, ref CK_MECHANISM mechanism, NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_VerifyRecover(NativeLong session, byte[] signature, NativeLong signatureLen, byte[] data, ref NativeLong dataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DigestEncryptUpdate(NativeLong session, byte[] part, NativeLong partLen, byte[] encryptedPart, ref NativeLong encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DecryptDigestUpdate(NativeLong session, byte[] encryptedPart, NativeLong encryptedPartLen, byte[] part, ref NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SignEncryptUpdate(NativeLong session, byte[] part, NativeLong partLen, byte[] encryptedPart, ref NativeLong encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DecryptVerifyUpdate(NativeLong session, byte[] encryptedPart, NativeLong encryptedPartLen, byte[] part, ref NativeLong partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GenerateKey(NativeLong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, NativeLong count, ref NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GenerateKeyPair(NativeLong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, NativeLong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, NativeLong privateKeyAttributeCount, ref NativeLong publicKey, ref NativeLong privateKey);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_WrapKey(NativeLong session, ref CK_MECHANISM mechanism, NativeLong wrappingKey, NativeLong key, byte[] wrappedKey, ref NativeLong wrappedKeyLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_UnwrapKey(NativeLong session, ref CK_MECHANISM mechanism, NativeLong unwrappingKey, byte[] wrappedKey, NativeLong wrappedKeyLen, CK_ATTRIBUTE[] template, NativeLong attributeCount, ref NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_DeriveKey(NativeLong session, ref CK_MECHANISM mechanism, NativeLong baseKey, CK_ATTRIBUTE[] template, NativeLong attributeCount, ref NativeLong key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_SeedRandom(NativeLong session, byte[] seed, NativeLong seedLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GenerateRandom(NativeLong session, byte[] randomData, NativeLong randomLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_GetFunctionStatus(NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_CancelFunction(NativeLong session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern NativeLong C_WaitForSlotEvent(NativeLong flags, ref NativeLong slot, IntPtr reserved);
    }
}
