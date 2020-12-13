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

namespace Net.Pkcs11Interop.LowLevelAPI41
{
    internal static class NativeMethods
    {
#if XAMARINMAC2_0
        private const string _dllName = "NonExistingLibrary";
#else
        private const string _dllName = "__Internal";
#endif

        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Initialize(CK_C_INITIALIZE_ARGS initArgs);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Finalize(IntPtr reserved);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetInfo(ref CK_INFO info);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetFunctionList(out IntPtr functionList);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetSlotList([MarshalAs(UnmanagedType.U1)] bool tokenPresent, uint[] slotList, ref uint count);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetSlotInfo(uint slotId, ref CK_SLOT_INFO info);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetTokenInfo(uint slotId, ref CK_TOKEN_INFO info);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetMechanismList(uint slotId, uint[] mechanismList, ref uint count);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetMechanismInfo(uint slotId, uint type, ref CK_MECHANISM_INFO info);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_InitToken(uint slotId, byte[] pin, uint pinLen, byte[] label);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_InitPIN(uint session, byte[] pin, uint pinLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SetPIN(uint session, byte[] oldPin, uint oldPinLen, byte[] newPin, uint newPinLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_OpenSession(uint slotId, uint flags, IntPtr application, IntPtr notify, ref uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_CloseSession(uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_CloseAllSessions(uint slotId);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetSessionInfo(uint session, ref CK_SESSION_INFO info);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetOperationState(uint session, byte[] operationState, ref uint operationStateLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SetOperationState(uint session, byte[] operationState, uint operationStateLen, uint encryptionKey, uint authenticationKey);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Login(uint session, uint userType, byte[] pin, uint pinLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Logout(uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_CreateObject(uint session, CK_ATTRIBUTE[] template, uint count, ref uint objectId);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_CopyObject(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count, ref uint newObjectId);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DestroyObject(uint session, uint objectId);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetObjectSize(uint session, uint objectId, ref uint size);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetAttributeValue(uint session, uint objectId, [In, Out] CK_ATTRIBUTE[] template, uint count);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SetAttributeValue(uint session, uint objectId, CK_ATTRIBUTE[] template, uint count);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_FindObjectsInit(uint session, CK_ATTRIBUTE[] template, uint count);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_FindObjects(uint session, uint[] objectId, uint maxObjectCount, ref uint objectCount);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_FindObjectsFinal(uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_EncryptInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Encrypt(uint session, byte[] data, uint dataLen, byte[] encryptedData, ref uint encryptedDataLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_EncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_EncryptFinal(uint session, byte[] lastEncryptedPart, ref uint lastEncryptedPartLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DecryptInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Decrypt(uint session, byte[] encryptedData, uint encryptedDataLen, byte[] data, ref uint dataLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DecryptUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DecryptFinal(uint session, byte[] lastPart, ref uint lastPartLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DigestInit(uint session, ref CK_MECHANISM mechanism);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Digest(uint session, byte[] data, uint dataLen, byte[] digest, ref uint digestLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DigestUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DigestKey(uint session, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DigestFinal(uint session, byte[] digest, ref uint digestLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Sign(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignFinal(uint session, byte[] signature, ref uint signatureLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignRecover(uint session, byte[] data, uint dataLen, byte[] signature, ref uint signatureLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_VerifyInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_Verify(uint session, byte[] data, uint dataLen, byte[] signature, uint signatureLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_VerifyUpdate(uint session, byte[] part, uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_VerifyFinal(uint session, byte[] signature, uint signatureLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_VerifyRecoverInit(uint session, ref CK_MECHANISM mechanism, uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_VerifyRecover(uint session, byte[] signature, uint signatureLen, byte[] data, ref uint dataLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DigestEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DecryptDigestUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SignEncryptUpdate(uint session, byte[] part, uint partLen, byte[] encryptedPart, ref uint encryptedPartLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DecryptVerifyUpdate(uint session, byte[] encryptedPart, uint encryptedPartLen, byte[] part, ref uint partLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GenerateKey(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] template, uint count, ref uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GenerateKeyPair(uint session, ref CK_MECHANISM mechanism, CK_ATTRIBUTE[] publicKeyTemplate, uint publicKeyAttributeCount, CK_ATTRIBUTE[] privateKeyTemplate, uint privateKeyAttributeCount, ref uint publicKey, ref uint privateKey);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_WrapKey(uint session, ref CK_MECHANISM mechanism, uint wrappingKey, uint key, byte[] wrappedKey, ref uint wrappedKeyLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_UnwrapKey(uint session, ref CK_MECHANISM mechanism, uint unwrappingKey, byte[] wrappedKey, uint wrappedKeyLen, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_DeriveKey(uint session, ref CK_MECHANISM mechanism, uint baseKey, CK_ATTRIBUTE[] template, uint attributeCount, ref uint key);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_SeedRandom(uint session, byte[] seed, uint seedLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GenerateRandom(uint session, byte[] randomData, uint randomLen);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_GetFunctionStatus(uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_CancelFunction(uint session);
    
        [DllImport(_dllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint C_WaitForSlotEvent(uint flags, ref uint slot, IntPtr reserved);
    }
}
