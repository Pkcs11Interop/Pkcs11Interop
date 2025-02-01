/*
 *  Copyright 2012-2025 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI41;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Mock.LowLevelAPI41
{
    /// <summary>
    /// Low level PKCS#11 wrapper extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockPkcs11Library : Pkcs11Library
    {
        /// <summary>
        /// Delegates for vendor specific unmanaged functions
        /// </summary>
        private MockDelegates _mockDelegates = null;

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        public MockPkcs11Library(string libraryPath)
            : base(libraryPath)
        {
            try
            {
                _mockDelegates = new MockDelegates(_libraryHandle);
            }
            catch
            {
                base.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Loads PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        public MockPkcs11Library(string libraryPath, bool useGetFunctionList)
            : base(libraryPath, useGetFunctionList)
        {
            try
            {
                _mockDelegates = new MockDelegates(_libraryHandle);
            }
            catch
            {
                base.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Obtains a list of unmanaged struct sizes.
        /// </summary>
        /// <param name="sizeList">
        /// If set to null then the number of sizes is returned in "count" parameter, without actually returning a list of sizes.
        /// If not set to null then "count" parameter must contain the lenght of sizeList array and size list is returned in "sizeList" parameter.
        /// </param>
        /// <param name="count">Location that receives the number of sizes</param>
        /// <returns>CKR_ARGUMENTS_BAD, CKR_BUFFER_TOO_SMALL, CKR_OK</returns>
        public CKR C_GetUnmanagedStructSizeList(NativeULong[] sizeList, ref NativeULong count)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            NativeULong rv = _mockDelegates.C_GetUnmanagedStructSizeList(sizeList, ref count);
            return ConvertUtils.UInt32ToCKR(rv);
        }

        /// <summary>
        /// Ejects token from slot.
        /// </summary>
        /// <param name="slotId">The ID of the token's slot</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SLOT_ID_INVALID, CKR_OK</returns>
        public CKR C_EjectToken(NativeULong slotId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            NativeULong rv = _mockDelegates.C_EjectToken(slotId);
            return ConvertUtils.UInt32ToCKR(rv);
        }

        /// <summary>
        /// Logs a user into a token interactively.
        /// </summary>
        /// <param name="session">The session's handle</param>
        /// <returns>CKR_CRYPTOKI_NOT_INITIALIZED, CKR_SESSION_HANDLE_INVALID, CKR_USER_ALREADY_LOGGED_IN, CKR_USER_ANOTHER_ALREADY_LOGGED_IN, CKR_OK</returns>
        public CKR C_InteractiveLogin(NativeULong session)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            NativeULong rv = _mockDelegates.C_InteractiveLogin(session);
            return ConvertUtils.UInt32ToCKR(rv);
        }
    }
}
