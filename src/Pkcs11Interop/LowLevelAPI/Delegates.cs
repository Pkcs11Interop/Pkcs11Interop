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

namespace Net.Pkcs11Interop.LowLevelAPI
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
    internal delegate CKR C_GetSlotListDelegate(bool tokenPresent, uint[] slotList, ref uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetSlotInfoDelegate(uint slotId, ref CK_SLOT_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetTokenInfoDelegate(uint slotId, ref CK_TOKEN_INFO info);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismListDelegate(uint slotId, CKM[] mechanismList, ref uint count);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate CKR C_GetMechanismInfoDelegate(uint slotId, CKM type, ref CK_MECHANISM_INFO info);
    
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
    internal delegate CKR C_LoginDelegate(uint session, CKU userType, byte[] pin, uint pinLen);
    
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
}
