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
using NativeULong = System.UInt64;

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    internal static class NativeMethods
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
}
