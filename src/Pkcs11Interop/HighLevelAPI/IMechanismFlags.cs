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

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Flags specifying mechanism capabilities
    /// </summary>
    public interface IMechanismFlags
    {
        /// <summary>
        /// Bits flags specifying mechanism capabilities
        /// </summary>
        ulong Flags
        {
            get;
        }

        /// <summary>
        /// True if the mechanism is performed by the device; false if the mechanism is performed in software
        /// </summary>
        bool Hw
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_EncryptInit
        /// </summary>
        bool Encrypt
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_DecryptInit
        /// </summary>
        bool Decrypt
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_DigestInit
        /// </summary>
        bool Digest
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignInit
        /// </summary>
        bool Sign
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignRecoverInit
        /// </summary>
        bool SignRecover
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyInit
        /// </summary>
        bool Verify
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyRecoverInit
        /// </summary>
        bool VerifyRecover
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKey
        /// </summary>
        bool Generate
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKeyPair
        /// </summary>
        bool GenerateKeyPair
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_WrapKey
        /// </summary>
        bool Wrap
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_UnwrapKey
        /// </summary>
        bool Unwrap
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with C_DeriveKey
        /// </summary>
        bool Derive
        {
            get;
        }

        /// <summary>
        /// True if there is an extension to the flags; false if no extensions.
        /// </summary>
        bool Extension
        {
            get;
        }

        #region Elliptic Curve

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over Fp
        /// </summary>
        bool EcFp
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over F2m
        /// </summary>
        bool EcF2m
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice ecParameters
        /// </summary>
        bool EcEcParameters
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice namedCurve
        /// </summary>
        bool EcNamedCurve
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point uncompressed
        /// </summary>
        bool EcUncompress
        {
            get;
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point compressed
        /// </summary>
        bool EcCompress
        {
            get;
        }

        #endregion
    }
}
