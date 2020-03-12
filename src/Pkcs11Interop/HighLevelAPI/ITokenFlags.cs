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
    /// Flags indicating capabilities and status of the device
    /// </summary>
    public interface ITokenFlags
    {
        /// <summary>
        /// Bits flags indicating capabilities and status of the device
        /// </summary>
        ulong Flags
        {
            get;
        }

        /// <summary>
        /// True if the token has its own random number generator
        /// </summary>
        bool Rng
        {
            get;
        }

        /// <summary>
        /// True if the token is write-protected
        /// </summary>
        bool WriteProtected
        {
            get;
        }

        /// <summary>
        /// True if there are some cryptographic functions that a user must be logged in to perform
        /// </summary>
        bool LoginRequired
        {
            get;
        }

        /// <summary>
        /// True if the normal user's PIN has been initialized
        /// </summary>
        bool UserPinInitialized
        {
            get;
        }

        /// <summary>
        /// True if a successful save of a session's cryptographic operations state always contains all keys needed to restore the state of the session
        /// </summary>
        bool RestoreKeyNotNeeded
        {
            get;
        }

        /// <summary>
        /// True if token has its own hardware clock
        /// </summary>
        bool ClockOnToken
        {
            get;
        }

        /// <summary>
        /// True if token has a "protected authentication path", whereby a user can log into the token without passing a PIN through the Cryptoki library
        /// </summary>
        bool ProtectedAuthenticationPath
        {
            get;
        }

        /// <summary>
        /// True if a single session with the token can perform dual cryptographic operations
        /// </summary>
        bool DualCryptoOperations
        {
            get;
        }

        /// <summary>
        /// True if the token has been initialized using C_InitializeToken or an equivalent mechanism
        /// </summary>
        bool TokenInitialized
        {
            get;
        }

        /// <summary>
        /// True if the token supports secondary authentication for private key objects
        /// </summary>
        bool SecondaryAuthentication
        {
            get;
        }

        /// <summary>
        /// True if an incorrect user login PIN has been entered at least once since the last successful authentication
        /// </summary>
        bool UserPinCountLow
        {
            get;
        }

        /// <summary>
        /// True if supplying an incorrect user PIN will make it to become locked
        /// </summary>
        bool UserPinFinalTry
        {
            get;
        }

        /// <summary>
        /// True if the user PIN has been locked. User login to the token is not possible.
        /// </summary>
        bool UserPinLocked
        {
            get;
        }

        /// <summary>
        /// True if the user PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card
        /// </summary>
        bool UserPinToBeChanged
        {
            get;
        }

        /// <summary>
        /// True if an incorrect SO login PIN has been entered at least once since the last successful authentication
        /// </summary>
        bool SoPinCountLow
        {
            get;
        }

        /// <summary>
        /// True if supplying an incorrect SO PIN will make it to become locked.
        /// </summary>
        bool SoPinFinalTry
        {
            get;
        }

        /// <summary>
        /// True if the SO PIN has been locked. User login to the token is not possible.
        /// </summary>
        bool SoPinLocked
        {
            get;
        }

        /// <summary>
        /// True if the SO PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        bool SoPinToBeChanged
        {
            get;
        }
    }
}
