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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Flags indicating capabilities and status of the device
    /// </summary>
    public class TokenFlags
    {
        /// <summary>
        /// Platform specific TokenFlags
        /// </summary>
        private HighLevelAPI4.TokenFlags _tokenFlags4 = null;

        /// <summary>
        /// Platform specific TokenFlags
        /// </summary>
        private HighLevelAPI8.TokenFlags _tokenFlags8 = null;

        /// <summary>
        /// Bits flags indicating capabilities and status of the device
        /// </summary>
        public ulong Flags
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.Flags : _tokenFlags8.Flags;
            }
        }

        /// <summary>
        /// True if the token has its own random number generator
        /// </summary>
        public bool Rng
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.Rng : _tokenFlags8.Rng;
            }
        }

        /// <summary>
        /// True if the token is write-protected
        /// </summary>
        public bool WriteProtected
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.WriteProtected : _tokenFlags8.WriteProtected;
            }
        }

        /// <summary>
        /// True if there are some cryptographic functions that a user must be logged in to perform
        /// </summary>
        public bool LoginRequired
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.LoginRequired : _tokenFlags8.LoginRequired;
            }
        }

        /// <summary>
        /// True if the normal user's PIN has been initialized
        /// </summary>
        public bool UserPinInitialized
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.UserPinInitialized : _tokenFlags8.UserPinInitialized;
            }
        }

        /// <summary>
        /// True if a successful save of a session's cryptographic operations state always contains all keys needed to restore the state of the session
        /// </summary>
        public bool RestoreKeyNotNeeded
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.RestoreKeyNotNeeded : _tokenFlags8.RestoreKeyNotNeeded;
            }
        }

        /// <summary>
        /// True if token has its own hardware clock
        /// </summary>
        public bool ClockOnToken
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.ClockOnToken : _tokenFlags8.ClockOnToken;
            }
        }
        
        /// <summary>
        /// True if token has a “protected authentication path”, whereby a user can log into the token without passing a PIN through the Cryptoki library
        /// </summary>
        public bool ProtectedAuthenticationPath
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.ProtectedAuthenticationPath : _tokenFlags8.ProtectedAuthenticationPath;
            }
        }

        /// <summary>
        /// True if a single session with the token can perform dual cryptographic operations
        /// </summary>
        public bool DualCryptoOperations
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.DualCryptoOperations : _tokenFlags8.DualCryptoOperations;
            }
        }

        /// <summary>
        /// True if the token has been initialized using C_InitializeToken or an equivalent mechanism
        /// </summary>
        public bool TokenInitialized
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.TokenInitialized : _tokenFlags8.TokenInitialized;
            }
        }

        /// <summary>
        /// True if the token supports secondary authentication for private key objects
        /// </summary>
        public bool SecondaryAuthentication
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.SecondaryAuthentication : _tokenFlags8.SecondaryAuthentication;
            }
        }

        /// <summary>
        /// True if an incorrect user login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool UserPinCountLow
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.UserPinCountLow : _tokenFlags8.UserPinCountLow;
            }
        }

        /// <summary>
        /// True if supplying an incorrect user PIN will make it to become locked
        /// </summary>
        public bool UserPinFinalTry
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.UserPinFinalTry : _tokenFlags8.UserPinFinalTry;
            }
        }

        /// <summary>
        /// True if the user PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool UserPinLocked
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.UserPinLocked : _tokenFlags8.UserPinLocked;
            }
        }

        /// <summary>
        /// True if the user PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card
        /// </summary>
        public bool UserPinToBeChanged
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.UserPinToBeChanged : _tokenFlags8.UserPinToBeChanged;
            }
        }

        /// <summary>
        /// True if an incorrect SO login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool SoPinCountLow
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.SoPinCountLow : _tokenFlags8.SoPinCountLow;
            }
        }

        /// <summary>
        /// True if supplying an incorrect SO PIN will make it to become locked.
        /// </summary>
        public bool SoPinFinalTry
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.SoPinFinalTry : _tokenFlags8.SoPinFinalTry;
            }
        }

        /// <summary>
        /// True if the SO PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool SoPinLocked
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.SoPinLocked : _tokenFlags8.SoPinLocked;
            }
        }

        /// <summary>
        /// True if the SO PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        public bool SoPinToBeChanged
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _tokenFlags4.SoPinToBeChanged : _tokenFlags8.SoPinToBeChanged;
            }
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI4.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags4 = tokenFlags;
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI8.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags8 = tokenFlags;
        }
    }
}
