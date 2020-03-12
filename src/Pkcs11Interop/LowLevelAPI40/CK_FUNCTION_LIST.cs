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

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI40
{
    /// <summary>
    /// Structure which contains a Cryptoki version and a function pointer to each function in the Cryptoki API
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal struct CK_FUNCTION_LIST
    {
        /// <summary>
        /// Cryptoki version
        /// </summary>
        internal CK_VERSION version;

        /// <summary>
        /// Pointer to C_Initialize
        /// </summary>
        internal IntPtr C_Initialize;

        /// <summary>
        /// Pointer to C_Finalize
        /// </summary>
        internal IntPtr C_Finalize;

        /// <summary>
        /// Pointer to C_GetInfo
        /// </summary>
        internal IntPtr C_GetInfo;

        /// <summary>
        /// Pointer to C_GetFunctionList
        /// </summary>
        internal IntPtr C_GetFunctionList;

        /// <summary>
        /// Pointer to C_GetSlotList
        /// </summary>
        internal IntPtr C_GetSlotList;

        /// <summary>
        /// Pointer to C_GetSlotInfo
        /// </summary>
        internal IntPtr C_GetSlotInfo;

        /// <summary>
        /// Pointer to C_GetTokenInfo
        /// </summary>
        internal IntPtr C_GetTokenInfo;

        /// <summary>
        /// Pointer to C_GetMechanismList
        /// </summary>
        internal IntPtr C_GetMechanismList;

        /// <summary>
        /// Pointer to C_GetMechanismInfo
        /// </summary>
        internal IntPtr C_GetMechanismInfo;

        /// <summary>
        /// Pointer to C_InitToken
        /// </summary>
        internal IntPtr C_InitToken;

        /// <summary>
        /// Pointer to C_InitPIN
        /// </summary>
        internal IntPtr C_InitPIN;

        /// <summary>
        /// Pointer to C_SetPIN
        /// </summary>
        internal IntPtr C_SetPIN;

        /// <summary>
        /// Pointer to C_OpenSession
        /// </summary>
        internal IntPtr C_OpenSession;

        /// <summary>
        /// Pointer to C_CloseSession
        /// </summary>
        internal IntPtr C_CloseSession;

        /// <summary>
        /// Pointer to C_CloseAllSessions
        /// </summary>
        internal IntPtr C_CloseAllSessions;

        /// <summary>
        /// Pointer to C_GetSessionInfo
        /// </summary>
        internal IntPtr C_GetSessionInfo;

        /// <summary>
        /// Pointer to C_GetOperationState
        /// </summary>
        internal IntPtr C_GetOperationState;

        /// <summary>
        /// Pointer to C_SetOperationState
        /// </summary>
        internal IntPtr C_SetOperationState;

        /// <summary>
        /// Pointer to C_Login
        /// </summary>
        internal IntPtr C_Login;

        /// <summary>
        /// Pointer to C_Logout
        /// </summary>
        internal IntPtr C_Logout;

        /// <summary>
        /// Pointer to C_CreateObject
        /// </summary>
        internal IntPtr C_CreateObject;

        /// <summary>
        /// Pointer to C_CopyObject
        /// </summary>
        internal IntPtr C_CopyObject;

        /// <summary>
        /// Pointer to C_DestroyObject
        /// </summary>
        internal IntPtr C_DestroyObject;

        /// <summary>
        /// Pointer to C_GetObjectSize
        /// </summary>
        internal IntPtr C_GetObjectSize;

        /// <summary>
        /// Pointer to C_GetAttributeValue
        /// </summary>
        internal IntPtr C_GetAttributeValue;

        /// <summary>
        /// Pointer to C_SetAttributeValue
        /// </summary>
        internal IntPtr C_SetAttributeValue;

        /// <summary>
        /// Pointer to C_FindObjectsInit
        /// </summary>
        internal IntPtr C_FindObjectsInit;

        /// <summary>
        /// Pointer to C_FindObjects
        /// </summary>
        internal IntPtr C_FindObjects;

        /// <summary>
        /// Pointer to C_FindObjectsFinal
        /// </summary>
        internal IntPtr C_FindObjectsFinal;

        /// <summary>
        /// Pointer to C_EncryptInit
        /// </summary>
        internal IntPtr C_EncryptInit;

        /// <summary>
        /// Pointer to C_Encrypt
        /// </summary>
        internal IntPtr C_Encrypt;

        /// <summary>
        /// Pointer to C_EncryptUpdate
        /// </summary>
        internal IntPtr C_EncryptUpdate;

        /// <summary>
        /// Pointer to C_EncryptFinal
        /// </summary>
        internal IntPtr C_EncryptFinal;

        /// <summary>
        /// Pointer to C_DecryptInit
        /// </summary>
        internal IntPtr C_DecryptInit;

        /// <summary>
        /// Pointer to C_Decrypt
        /// </summary>
        internal IntPtr C_Decrypt;

        /// <summary>
        /// Pointer to C_DecryptUpdate
        /// </summary>
        internal IntPtr C_DecryptUpdate;

        /// <summary>
        /// Pointer to C_DecryptFinal
        /// </summary>
        internal IntPtr C_DecryptFinal;

        /// <summary>
        /// Pointer to C_DigestInit
        /// </summary>
        internal IntPtr C_DigestInit;

        /// <summary>
        /// Pointer to C_Digest
        /// </summary>
        internal IntPtr C_Digest;

        /// <summary>
        /// Pointer to C_DigestUpdate
        /// </summary>
        internal IntPtr C_DigestUpdate;

        /// <summary>
        /// Pointer to C_DigestKey
        /// </summary>
        internal IntPtr C_DigestKey;

        /// <summary>
        /// Pointer to C_DigestFinal
        /// </summary>
        internal IntPtr C_DigestFinal;

        /// <summary>
        /// Pointer to C_SignInit
        /// </summary>
        internal IntPtr C_SignInit;

        /// <summary>
        /// Pointer to C_Sign
        /// </summary>
        internal IntPtr C_Sign;

        /// <summary>
        /// Pointer to C_SignUpdate
        /// </summary>
        internal IntPtr C_SignUpdate;

        /// <summary>
        /// Pointer to C_SignFinal
        /// </summary>
        internal IntPtr C_SignFinal;

        /// <summary>
        /// Pointer to C_SignRecoverInit
        /// </summary>
        internal IntPtr C_SignRecoverInit;

        /// <summary>
        /// Pointer to C_SignRecover
        /// </summary>
        internal IntPtr C_SignRecover;

        /// <summary>
        /// Pointer to C_VerifyInit
        /// </summary>
        internal IntPtr C_VerifyInit;

        /// <summary>
        /// Pointer to C_Verify
        /// </summary>
        internal IntPtr C_Verify;

        /// <summary>
        /// Pointer to C_VerifyUpdate
        /// </summary>
        internal IntPtr C_VerifyUpdate;

        /// <summary>
        /// Pointer to C_VerifyFinal
        /// </summary>
        internal IntPtr C_VerifyFinal;

        /// <summary>
        /// Pointer to C_VerifyRecoverInit
        /// </summary>
        internal IntPtr C_VerifyRecoverInit;

        /// <summary>
        /// Pointer to C_VerifyRecover
        /// </summary>
        internal IntPtr C_VerifyRecover;

        /// <summary>
        /// Pointer to C_DigestEncryptUpdate
        /// </summary>
        internal IntPtr C_DigestEncryptUpdate;

        /// <summary>
        /// Pointer to C_DecryptDigestUpdate
        /// </summary>
        internal IntPtr C_DecryptDigestUpdate;

        /// <summary>
        /// Pointer to C_SignEncryptUpdate
        /// </summary>
        internal IntPtr C_SignEncryptUpdate;

        /// <summary>
        /// Pointer to C_DecryptVerifyUpdate
        /// </summary>
        internal IntPtr C_DecryptVerifyUpdate;

        /// <summary>
        /// Pointer to C_GenerateKey
        /// </summary>
        internal IntPtr C_GenerateKey;

        /// <summary>
        /// Pointer to C_GenerateKeyPair
        /// </summary>
        internal IntPtr C_GenerateKeyPair;

        /// <summary>
        /// Pointer to C_WrapKey
        /// </summary>
        internal IntPtr C_WrapKey;

        /// <summary>
        /// Pointer to C_UnwrapKey
        /// </summary>
        internal IntPtr C_UnwrapKey;

        /// <summary>
        /// Pointer to C_DeriveKey
        /// </summary>
        internal IntPtr C_DeriveKey;

        /// <summary>
        /// Pointer to C_SeedRandom
        /// </summary>
        internal IntPtr C_SeedRandom;

        /// <summary>
        /// Pointer to C_GenerateRandom
        /// </summary>
        internal IntPtr C_GenerateRandom;

        /// <summary>
        /// Pointer to C_GetFunctionStatus
        /// </summary>
        internal IntPtr C_GetFunctionStatus;

        /// <summary>
        /// Pointer to C_CancelFunction
        /// </summary>
        internal IntPtr C_CancelFunction;

        /// <summary>
        /// Pointer to C_WaitForSlotEvent
        /// </summary>
        internal IntPtr C_WaitForSlotEvent;
    }
}
