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

namespace Net.Pkcs11Interop.LowLevelAPI80
{
    internal static class NativeMethods
    {
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Initialize(CK_C_INITIALIZE_ARGS initArgs);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Finalize(IntPtr reserved);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetInfo(ref CK_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetFunctionList(out IntPtr functionList);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetSlotList(bool tokenPresent, ulong[] slotList, ref ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetSlotInfo(ulong slotId, ref CK_SLOT_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetTokenInfo(ulong slotId, ref CK_TOKEN_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetMechanismList(ulong slotId, ulong[] mechanismList, ref ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetMechanismInfo(ulong slotId, ulong type, ref CK_MECHANISM_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_InitToken(ulong slotId, byte[] pin, ulong pinLen, byte[] label);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_InitPIN(ulong session, byte[] pin, ulong pinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetPIN(ulong session, byte[] oldPin, ulong oldPinLen, byte[] newPin, ulong newPinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_OpenSession(ulong slotId, ulong flags, IntPtr application, IntPtr notify, ref ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CloseSession(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CloseAllSessions(ulong slotId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetSessionInfo(ulong session, ref CK_SESSION_INFO info);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetOperationState(ulong session, byte[] operationState, ref ulong operationStateLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetOperationState(ulong session, byte[] operationState, ulong operationStateLen, ulong encryptionKey, ulong authenticationKey);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Login(ulong session, ulong userType, byte[] pin, ulong pinLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Logout(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CreateObject(ulong session, CK_ATTRIBUTE[] template, ulong count, ref ulong objectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CopyObject(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count, ref ulong newObjectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DestroyObject(ulong session, ulong objectId);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetObjectSize(ulong session, ulong objectId, ref ulong size);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetAttributeValue(ulong session, ulong objectId, [In, Out] CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetAttributeValue(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjectsInit(ulong session, CK_ATTRIBUTE[] template, ulong count);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjects(ulong session, ulong[] objectId, ulong maxObjectCount, ref ulong objectCount);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjectsFinal(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Encrypt(ulong session, byte[] data, ulong dataLen, byte[] encryptedData, ref ulong encryptedDataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptFinal(ulong session, byte[] lastEncryptedPart, ref ulong lastEncryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Decrypt(ulong session, byte[] encryptedData, ulong encryptedDataLen, byte[] data, ref ulong dataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptFinal(ulong session, byte[] lastPart, ref ulong lastPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestInit(ulong session, ref CK_MECHANISM mechanism);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Digest(ulong session, byte[] data, ulong dataLen, byte[] digest, ref ulong digestLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestKey(ulong session, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestFinal(ulong session, byte[] digest, ref ulong digestLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Sign(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignFinal(ulong session, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignRecoverInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignRecover(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Verify(ulong session, byte[] data, ulong dataLen, byte[] signature, ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyUpdate(ulong session, byte[] part, ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyFinal(ulong session, byte[] signature, ulong signatureLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyRecoverInit(ulong session, ref CK_MECHANISM mechanism, ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyRecover(ulong session, byte[] signature, ulong signatureLen, byte[] data, ref ulong dataLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestEncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptDigestUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignEncryptUpdate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptVerifyUpdate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateKey(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, ulong count, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateKeyPair(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, ulong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, ulong privateKeyAttributeCount, ref ulong publicKey, ref ulong privateKey);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_WrapKey(ulong session, ref CK_MECHANISM mechanism, ulong wrappingKey, ulong key, byte[] wrappedKey, ref ulong wrappedKeyLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_UnwrapKey(ulong session, ref CK_MECHANISM mechanism, ulong unwrappingKey, byte[] wrappedKey, ulong wrappedKeyLen, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DeriveKey(ulong session, ref CK_MECHANISM mechanism, ulong baseKey, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SeedRandom(ulong session, byte[] seed, ulong seedLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateRandom(ulong session, byte[] randomData, ulong randomLen);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetFunctionStatus(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CancelFunction(ulong session);

        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_WaitForSlotEvent(ulong flags, ref ulong slot, IntPtr reserved);
    }
}
