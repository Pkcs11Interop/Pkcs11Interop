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

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Structure which contains a Cryptoki version and a function pointer to each function in the Cryptoki API. Please note that CK_FUNCTION_LIST_UNIX uses different marshaling than CK_FUNCTION_LIST.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CK_FUNCTION_LIST_UNIX
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

        /// <summary>
        /// Converts CK_FUNCTION_LIST_UNIX to CK_FUNCTION_LIST
        /// </summary>
        /// <returns>CK_FUNCTION_LIST converted from CK_FUNCTION_LIST_UNIX</returns>
        public CK_FUNCTION_LIST ConvertToCkFunctionList()
        {
            CK_FUNCTION_LIST ckFunctionList = new CK_FUNCTION_LIST();
            ckFunctionList.version = this.version;
            ckFunctionList.C_Initialize = this.C_Initialize;
            ckFunctionList.C_Finalize = this.C_Finalize;
            ckFunctionList.C_GetInfo = this.C_GetInfo;
            ckFunctionList.C_GetFunctionList = this.C_GetFunctionList;
            ckFunctionList.C_GetSlotList = this.C_GetSlotList;
            ckFunctionList.C_GetSlotInfo = this.C_GetSlotInfo;
            ckFunctionList.C_GetTokenInfo = this.C_GetTokenInfo;
            ckFunctionList.C_GetMechanismList = this.C_GetMechanismList;
            ckFunctionList.C_GetMechanismInfo = this.C_GetMechanismInfo;
            ckFunctionList.C_InitToken = this.C_InitToken;
            ckFunctionList.C_InitPIN = this.C_InitPIN;
            ckFunctionList.C_SetPIN = this.C_SetPIN;
            ckFunctionList.C_OpenSession = this.C_OpenSession;
            ckFunctionList.C_CloseSession = this.C_CloseSession;
            ckFunctionList.C_CloseAllSessions = this.C_CloseAllSessions;
            ckFunctionList.C_GetSessionInfo = this.C_GetSessionInfo;
            ckFunctionList.C_GetOperationState = this.C_GetOperationState;
            ckFunctionList.C_SetOperationState = this.C_SetOperationState;
            ckFunctionList.C_Login = this.C_Login;
            ckFunctionList.C_Logout = this.C_Logout;
            ckFunctionList.C_CreateObject = this.C_CreateObject;
            ckFunctionList.C_CopyObject = this.C_CopyObject;
            ckFunctionList.C_DestroyObject = this.C_DestroyObject;
            ckFunctionList.C_GetObjectSize = this.C_GetObjectSize;
            ckFunctionList.C_GetAttributeValue = this.C_GetAttributeValue;
            ckFunctionList.C_SetAttributeValue = this.C_SetAttributeValue;
            ckFunctionList.C_FindObjectsInit = this.C_FindObjectsInit;
            ckFunctionList.C_FindObjects = this.C_FindObjects;
            ckFunctionList.C_FindObjectsFinal = this.C_FindObjectsFinal;
            ckFunctionList.C_EncryptInit = this.C_EncryptInit;
            ckFunctionList.C_Encrypt = this.C_Encrypt;
            ckFunctionList.C_EncryptUpdate = this.C_EncryptUpdate;
            ckFunctionList.C_EncryptFinal = this.C_EncryptFinal;
            ckFunctionList.C_DecryptInit = this.C_DecryptInit;
            ckFunctionList.C_Decrypt = this.C_Decrypt;
            ckFunctionList.C_DecryptUpdate = this.C_DecryptUpdate;
            ckFunctionList.C_DecryptFinal = this.C_DecryptFinal;
            ckFunctionList.C_DigestInit = this.C_DigestInit;
            ckFunctionList.C_Digest = this.C_Digest;
            ckFunctionList.C_DigestUpdate = this.C_DigestUpdate;
            ckFunctionList.C_DigestKey = this.C_DigestKey;
            ckFunctionList.C_DigestFinal = this.C_DigestFinal;
            ckFunctionList.C_SignInit = this.C_SignInit;
            ckFunctionList.C_Sign = this.C_Sign;
            ckFunctionList.C_SignUpdate = this.C_SignUpdate;
            ckFunctionList.C_SignFinal = this.C_SignFinal;
            ckFunctionList.C_SignRecoverInit = this.C_SignRecoverInit;
            ckFunctionList.C_SignRecover = this.C_SignRecover;
            ckFunctionList.C_VerifyInit = this.C_VerifyInit;
            ckFunctionList.C_Verify = this.C_Verify;
            ckFunctionList.C_VerifyUpdate = this.C_VerifyUpdate;
            ckFunctionList.C_VerifyFinal = this.C_VerifyFinal;
            ckFunctionList.C_VerifyRecoverInit = this.C_VerifyRecoverInit;
            ckFunctionList.C_VerifyRecover = this.C_VerifyRecover;
            ckFunctionList.C_DigestEncryptUpdate = this.C_DigestEncryptUpdate;
            ckFunctionList.C_DecryptDigestUpdate = this.C_DecryptDigestUpdate;
            ckFunctionList.C_SignEncryptUpdate = this.C_SignEncryptUpdate;
            ckFunctionList.C_DecryptVerifyUpdate = this.C_DecryptVerifyUpdate;
            ckFunctionList.C_GenerateKey = this.C_GenerateKey;
            ckFunctionList.C_GenerateKeyPair = this.C_GenerateKeyPair;
            ckFunctionList.C_WrapKey = this.C_WrapKey;
            ckFunctionList.C_UnwrapKey = this.C_UnwrapKey;
            ckFunctionList.C_DeriveKey = this.C_DeriveKey;
            ckFunctionList.C_SeedRandom = this.C_SeedRandom;
            ckFunctionList.C_GenerateRandom = this.C_GenerateRandom;
            ckFunctionList.C_GetFunctionStatus = this.C_GetFunctionStatus;
            ckFunctionList.C_CancelFunction = this.C_CancelFunction;
            ckFunctionList.C_WaitForSlotEvent = this.C_WaitForSlotEvent;
            return ckFunctionList;
        }
    }
}
