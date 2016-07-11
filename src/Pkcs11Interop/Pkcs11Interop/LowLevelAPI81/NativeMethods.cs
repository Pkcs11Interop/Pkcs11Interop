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
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI81
{
    internal static class NativeMethods
    {
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Initialize(CK_C_INITIALIZE_ARGS initArgs);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Finalize(IntPtr reserved);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetInfo(ref CK_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetFunctionList(out IntPtr functionList);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetSlotList([MarshalAs(UnmanagedType.U1)] bool tokenPresent, ulong[] slotList, ref ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetSlotInfo(ulong slotId, ref CK_SLOT_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetTokenInfo(ulong slotId, ref CK_TOKEN_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetMechanismList(ulong slotId, ulong[] mechanismList, ref ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetMechanismInfo(ulong slotId, ulong type, ref CK_MECHANISM_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_InitToken(ulong slotId, byte[] pin, ulong pinLen, byte[] label);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_InitPIN(ulong session, byte[] pin, ulong pinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SetPIN(ulong session, byte[] oldPin, ulong oldPinLen, byte[] newPin, ulong newPinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_OpenSession(ulong slotId, ulong flags, IntPtr application, IntPtr notify, ref ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_CloseSession(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_CloseAllSessions(ulong slotId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetSessionInfo(ulong session, ref CK_SESSION_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetOperationState(ulong session, byte[] operationState, ref ulong operationStateLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SetOperationState(ulong session, byte[] operationState, ulong operationStateLen, ulong encryptionKey, ulong authenticationKey);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Login(ulong session, ulong userType, byte[] pin, ulong pinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Logout(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_CreateObject(ulong session, CK_ATTRIBUTE[] template, ulong count, ref ulong objectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_CopyObject(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count, ref ulong newObjectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DestroyObject(ulong session, ulong objectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetObjectSize(ulong session, ulong objectId, ref ulong size);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetAttributeValue(ulong session, ulong objectId, [In, Out] CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SetAttributeValue(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_FindObjectsInit(ulong session, CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_FindObjects(ulong session, ulong[] objectId, ulong maxObjectCount, ref ulong objectCount);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_FindObjectsFinal(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_EncryptInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Encrypt(ulong session, byte[] data, ulong dataLen, byte[] encryptedData, ref ulong encryptedDataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_EncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_EncryptFinal(ulong session, byte[] lastEncryptedPart, ref ulong lastEncryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DecryptInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Decrypt(ulong session, byte[] encryptedData, ulong encryptedDataLen, byte[] data, ref ulong dataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DecryptUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DecryptFinal(ulong session, byte[] lastPart, ref ulong lastPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DigestInit(ulong session, ref CK_MECHANISM mechanism);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Digest(ulong session, byte[] data, ulong dataLen, byte[] digest, ref ulong digestLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DigestUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DigestKey(ulong session, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DigestFinal(ulong session, byte[] digest, ref ulong digestLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Sign(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignFinal(ulong session, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignRecoverInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignRecover(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_VerifyInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_Verify(ulong session, byte[] data, ulong dataLen, byte[] signature, ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_VerifyUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_VerifyFinal(ulong session, byte[] signature, ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_VerifyRecoverInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_VerifyRecover(ulong session, byte[] signature, ulong signatureLen, byte[] data, ref ulong dataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DigestEncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DecryptDigestUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SignEncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DecryptVerifyUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GenerateKey(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, ulong count, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GenerateKeyPair(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, ulong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, ulong privateKeyAttributeCount, ref ulong publicKey, ref ulong privateKey);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_WrapKey(ulong session, ref CK_MECHANISM mechanism, ulong wrappingKey, ulong key, byte[] wrappedKey, ref ulong wrappedKeyLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_UnwrapKey(ulong session, ref CK_MECHANISM mechanism, ulong unwrappingKey, byte[] wrappedKey, ulong wrappedKeyLen, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_DeriveKey(ulong session, ref CK_MECHANISM mechanism, ulong baseKey, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_SeedRandom(ulong session, byte[] seed, ulong seedLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GenerateRandom(ulong session, byte[] randomData, ulong randomLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_GetFunctionStatus(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_CancelFunction(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong C_WaitForSlotEvent(ulong flags, ref ulong slot, IntPtr reserved);
    }
}
