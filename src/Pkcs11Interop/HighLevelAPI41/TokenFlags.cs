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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NativeULong = System.UInt32;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.HighLevelAPI41
{
    /// <summary>
    /// Flags indicating capabilities and status of the device
    /// </summary>
    public class TokenFlags : ITokenFlags
    {
        /// <summary>
        /// Bits flags indicating capabilities and status of the device
        /// </summary>
        protected NativeULong _flags;

        /// <summary>
        /// Bits flags indicating capabilities and status of the device
        /// </summary>
        public ulong Flags
        {
            get
            {
                return ConvertUtils.UInt32ToUInt64(_flags);
            }
        }

        /// <summary>
        /// True if the token has its own random number generator
        /// </summary>
        public bool Rng
        {
            get
            {
                return ((_flags & CKF.CKF_RNG) == CKF.CKF_RNG);
            }
        }

        /// <summary>
        /// True if the token is write-protected
        /// </summary>
        public bool WriteProtected
        {
            get
            {
                return ((_flags & CKF.CKF_WRITE_PROTECTED) == CKF.CKF_WRITE_PROTECTED);
            }
        }

        /// <summary>
        /// True if there are some cryptographic functions that a user must be logged in to perform
        /// </summary>
        public bool LoginRequired
        {
            get
            {
                return ((_flags & CKF.CKF_LOGIN_REQUIRED) == CKF.CKF_LOGIN_REQUIRED);
            }
        }

        /// <summary>
        /// True if the normal user's PIN has been initialized
        /// </summary>
        public bool UserPinInitialized
        {
            get
            {
                return ((_flags & CKF.CKF_USER_PIN_INITIALIZED) == CKF.CKF_USER_PIN_INITIALIZED);
            }
        }

        /// <summary>
        /// True if a successful save of a session's cryptographic operations state always contains all keys needed to restore the state of the session
        /// </summary>
        public bool RestoreKeyNotNeeded
        {
            get
            {
                return ((_flags & CKF.CKF_RESTORE_KEY_NOT_NEEDED) == CKF.CKF_RESTORE_KEY_NOT_NEEDED);
            }
        }

        /// <summary>
        /// True if token has its own hardware clock
        /// </summary>
        public bool ClockOnToken
        {
            get
            {
                return ((_flags & CKF.CKF_CLOCK_ON_TOKEN) == CKF.CKF_CLOCK_ON_TOKEN);
            }
        }
        
        /// <summary>
        /// True if token has a "protected authentication path", whereby a user can log into the token without passing a PIN through the Cryptoki library
        /// </summary>
        public bool ProtectedAuthenticationPath
        {
            get
            {
                return ((_flags & CKF.CKF_PROTECTED_AUTHENTICATION_PATH) == CKF.CKF_PROTECTED_AUTHENTICATION_PATH);
            }
        }

        /// <summary>
        /// True if a single session with the token can perform dual cryptographic operations
        /// </summary>
        public bool DualCryptoOperations
        {
            get
            {
                return ((_flags & CKF.CKF_DUAL_CRYPTO_OPERATIONS) == CKF.CKF_DUAL_CRYPTO_OPERATIONS);
            }
        }

        /// <summary>
        /// True if the token has been initialized using C_InitializeToken or an equivalent mechanism
        /// </summary>
        public bool TokenInitialized
        {
            get
            {
                return ((_flags & CKF.CKF_TOKEN_INITIALIZED) == CKF.CKF_TOKEN_INITIALIZED);
            }
        }

        /// <summary>
        /// True if the token supports secondary authentication for private key objects
        /// </summary>
        public bool SecondaryAuthentication
        {
            get
            {
                return ((_flags & CKF.CKF_SECONDARY_AUTHENTICATION) == CKF.CKF_SECONDARY_AUTHENTICATION);
            }
        }

        /// <summary>
        /// True if an incorrect user login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool UserPinCountLow
        {
            get
            {
                return ((_flags & CKF.CKF_USER_PIN_COUNT_LOW) == CKF.CKF_USER_PIN_COUNT_LOW);
            }
        }

        /// <summary>
        /// True if supplying an incorrect user PIN will make it to become locked
        /// </summary>
        public bool UserPinFinalTry
        {
            get
            {
                return ((_flags & CKF.CKF_USER_PIN_FINAL_TRY) == CKF.CKF_USER_PIN_FINAL_TRY);
            }
        }

        /// <summary>
        /// True if the user PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool UserPinLocked
        {
            get
            {
                return ((_flags & CKF.CKF_USER_PIN_LOCKED) == CKF.CKF_USER_PIN_LOCKED);
            }
        }

        /// <summary>
        /// True if the user PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card
        /// </summary>
        public bool UserPinToBeChanged
        {
            get
            {
                return ((_flags & CKF.CKF_USER_PIN_TO_BE_CHANGED) == CKF.CKF_USER_PIN_TO_BE_CHANGED);
            }
        }

        /// <summary>
        /// True if an incorrect SO login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool SoPinCountLow
        {
            get
            {
                return ((_flags & CKF.CKF_SO_PIN_COUNT_LOW) == CKF.CKF_SO_PIN_COUNT_LOW);
            }
        }

        /// <summary>
        /// True if supplying an incorrect SO PIN will make it to become locked.
        /// </summary>
        public bool SoPinFinalTry
        {
            get
            {
                return ((_flags & CKF.CKF_SO_PIN_FINAL_TRY) == CKF.CKF_SO_PIN_FINAL_TRY);
            }
        }

        /// <summary>
        /// True if the SO PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool SoPinLocked
        {
            get
            {
                return ((_flags & CKF.CKF_SO_PIN_LOCKED) == CKF.CKF_SO_PIN_LOCKED);
            }
        }

        /// <summary>
        /// True if the SO PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        public bool SoPinToBeChanged
        {
            get
            {
                return ((_flags & CKF.CKF_SO_PIN_TO_BE_CHANGED) == CKF.CKF_SO_PIN_TO_BE_CHANGED);
            }
        }

        /// <summary>
        /// Initializes new instance of TokenFlags class
        /// </summary>
        /// <param name="flags">Bits flags indicating capabilities and status of the device</param>
        protected internal TokenFlags(NativeULong flags)
        {
            _flags = flags;
        }
    }
}
