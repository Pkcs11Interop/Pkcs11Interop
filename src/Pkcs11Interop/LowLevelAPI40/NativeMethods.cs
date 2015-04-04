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

namespace Net.Pkcs11Interop.LowLevelAPI40
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
        internal static extern CKR C_GetSlotList(bool tokenPresent, uint[] slotList, ref uint count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetSlotInfo(uint slotId, ref CK_SLOT_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetTokenInfo(uint slotId, ref CK_TOKEN_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetMechanismList(uint slotId, uint[] mechanismList, ref uint count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetMechanismInfo(uint slotId, uint type, ref CK_MECHANISM_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_InitToken(uint slotId, byte[] pin, uint pinLen, byte[] label);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_InitPIN(uint session, byte[] pin, uint pinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetPIN(uint session, byte[] oldPin, uint oldPinLen, byte[] newPin, uint newPinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_OpenSession(uint slotId, uint flags, IntPtr application, IntPtr notify, ref uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CloseSession(uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CloseAllSessions(uint slotId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetSessionInfo(uint session, ref CK_SESSION_INFO info);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetOperationState(uint session, byte[] operationState, ref uint operationStateLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetOperationState(uint session, byte[] operationState, uint operationStateLen, uint encryptionKey, uint authenticationKey);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Login(uint session, uint userType, byte[] pin, uint pinLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Logout(uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CreateObject(uint session, CK_ATTRIBUTE[] template, uint count, ref uint objectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CopyObject(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count, ref uint newObjectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DestroyObject(uint session, uint objectId);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetObjectSize(uint session, uint objectId, ref uint size);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetAttributeValue(uint session, uint objectId, [In, Out] CK_ATTRIBUTE[] template, uint count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SetAttributeValue(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjectsInit(uint session, CK_ATTRIBUTE[] template, uint count);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjects(uint session, uint[] objectId, uint maxObjectCount, ref uint objectCount);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_FindObjectsFinal(uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Encrypt(uint session, byte[] data, uint dataLen, byte[] encryptedData, ref uint encryptedDataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_EncryptFinal(uint session, byte[] lastEncryptedPart, ref uint lastEncryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Decrypt(uint session, byte[] encryptedData, uint encryptedDataLen, byte[] data, ref uint dataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptFinal(uint session, byte[] lastPart, ref uint lastPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestInit(uint session, ref CK_MECHANISM mechanism);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Digest(uint session, byte[] data, uint dataLen, byte[] digest, ref uint digestLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestKey(uint session, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestFinal(uint session, byte[] digest, ref uint digestLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Sign(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignFinal(uint session, byte[] signature, ref uint signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignRecover(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_Verify(uint session, byte[] data, uint dataLen, byte[] signature, uint signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyFinal(uint session, byte[] signature, uint signatureLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_VerifyRecover(uint session, byte[] signature, uint signatureLen, byte[] data, ref uint dataLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DigestEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptDigestUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SignEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DecryptVerifyUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateKey(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, uint count, ref uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateKeyPair(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, uint publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, uint privateKeyAttributeCount, ref uint publicKey, ref uint privateKey);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_WrapKey(uint session, ref CK_MECHANISM mechanism, uint wrappingKey, uint key, byte[] wrappedKey, ref uint wrappedKeyLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_UnwrapKey(uint session, ref CK_MECHANISM mechanism, uint unwrappingKey, byte[] wrappedKey, uint wrappedKeyLen, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_DeriveKey(uint session, ref CK_MECHANISM mechanism, uint baseKey, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_SeedRandom(uint session, byte[] seed, uint seedLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GenerateRandom(uint session, byte[] randomData, uint randomLen);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_GetFunctionStatus(uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_CancelFunction(uint session);
    
        [DllImport("__Internal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern CKR C_WaitForSlotEvent(uint flags, ref uint slot, IntPtr reserved);
    }
}
