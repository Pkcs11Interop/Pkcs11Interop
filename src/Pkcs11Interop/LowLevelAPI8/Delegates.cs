/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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

namespace Net.Pkcs11Interop.LowLevelAPI8
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
    internal delegate CKR C_GetSlotListDelegate(bool tokenPresent, ulong[] slotList, ref ulong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSlotInfoDelegate(ulong slotId, ref CK_SLOT_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetTokenInfoDelegate(ulong slotId, ref CK_TOKEN_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismListDelegate(ulong slotId, ulong[] mechanismList, ref ulong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismInfoDelegate(ulong slotId, ulong type, ref CK_MECHANISM_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_InitTokenDelegate(ulong slotId, byte[] pin, ulong pinLen, byte[] label);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_InitPINDelegate(ulong session, byte[] pin, ulong pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetPINDelegate(ulong session, byte[] oldPin, ulong oldPinLen, byte[] newPin, ulong newPinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_OpenSessionDelegate(ulong slotId, ulong flags, IntPtr application, IntPtr notify, ref ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CloseSessionDelegate(ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CloseAllSessionsDelegate(ulong slotId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSessionInfoDelegate(ulong session, ref CK_SESSION_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetOperationStateDelegate(ulong session, byte[] operationState, ref ulong operationStateLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetOperationStateDelegate(ulong session, byte[] operationState, ulong operationStateLen, ulong encryptionKey, ulong authenticationKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_LoginDelegate(ulong session, ulong userType, byte[] pin, ulong pinLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_LogoutDelegate(ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CreateObjectDelegate(ulong session, CK_ATTRIBUTE[] template, ulong count, ref ulong objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CopyObjectDelegate(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count, ref ulong newObjectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DestroyObjectDelegate(ulong session, ulong objectId);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetObjectSizeDelegate(ulong session, ulong objectId, ref ulong size);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetAttributeValueDelegate(ulong session, ulong objectId, [In, Out] CK_ATTRIBUTE[] template, ulong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SetAttributeValueDelegate(ulong session, ulong objectId, CK_ATTRIBUTE[] template, ulong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsInitDelegate(ulong session, CK_ATTRIBUTE[] template, ulong count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsDelegate(ulong session, ulong[] objectId, ulong maxObjectCount, ref ulong objectCount);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_FindObjectsFinalDelegate(ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptDelegate(ulong session, byte[] data, ulong dataLen, byte[] encryptedData, ref ulong encryptedDataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptUpdateDelegate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_EncryptFinalDelegate(ulong session, byte[] lastEncryptedPart, ref ulong lastEncryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptDelegate(ulong session, byte[] encryptedData, ulong encryptedDataLen, byte[] data, ref ulong dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptUpdateDelegate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptFinalDelegate(ulong session, byte[] lastPart, ref ulong lastPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestInitDelegate(ulong session, ref CK_MECHANISM mechanism);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestDelegate(ulong session, byte[] data, ulong dataLen, byte[] digest, ref ulong digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestUpdateDelegate(ulong session, byte[] part, ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestKeyDelegate(ulong session, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestFinalDelegate(ulong session, byte[] digest, ref ulong digestLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignDelegate(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignUpdateDelegate(ulong session, byte[] part, ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignFinalDelegate(ulong session, byte[] signature, ref ulong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignRecoverInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignRecoverDelegate(ulong session, byte[] data, ulong dataLen, byte[] signature, ref ulong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyDelegate(ulong session, byte[] data, ulong dataLen, byte[] signature, ulong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyUpdateDelegate(ulong session, byte[] part, ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyFinalDelegate(ulong session, byte[] signature, ulong signatureLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyRecoverInitDelegate(ulong session, ref CK_MECHANISM mechanism, ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_VerifyRecoverDelegate(ulong session, byte[] signature, ulong signatureLen, byte[] data, ref ulong dataLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DigestEncryptUpdateDelegate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptDigestUpdateDelegate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SignEncryptUpdateDelegate(ulong session, byte[] part, ulong partLen, byte[] encryptedPart, ref ulong encryptedPartLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DecryptVerifyUpdateDelegate(ulong session, byte[] encryptedPart, ulong encryptedPartLen, byte[] part, ref ulong partLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateKeyDelegate(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, ulong count, ref ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateKeyPairDelegate(ulong session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, ulong publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, ulong privateKeyAttributeCount, ref ulong publicKey, ref ulong privateKey);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_WrapKeyDelegate(ulong session, ref CK_MECHANISM mechanism, ulong wrappingKey, ulong key, byte[] wrappedKey, ref ulong wrappedKeyLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_UnwrapKeyDelegate(ulong session, ref CK_MECHANISM mechanism, ulong unwrappingKey, byte[] wrappedKey, ulong wrappedKeyLen, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_DeriveKeyDelegate(ulong session, ref CK_MECHANISM mechanism, ulong baseKey, CK_ATTRIBUTE[] template, ulong attributeCount, ref ulong key);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_SeedRandomDelegate(ulong session, byte[] seed, ulong seedLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GenerateRandomDelegate(ulong session, byte[] randomData, ulong randomLen);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetFunctionStatusDelegate(ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_CancelFunctionDelegate(ulong session);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_WaitForSlotEventDelegate(ulong flags, ref ulong slot, IntPtr reserved);
}
