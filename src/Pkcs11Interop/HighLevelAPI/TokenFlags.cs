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
        private HighLevelAPI40.TokenFlags _tokenFlags40 = null;

        /// <summary>
        /// Platform specific TokenFlags
        /// </summary>
        private HighLevelAPI41.TokenFlags _tokenFlags41 = null;

        /// <summary>
        /// Platform specific TokenFlags
        /// </summary>
        private HighLevelAPI80.TokenFlags _tokenFlags80 = null;

        /// <summary>
        /// Platform specific TokenFlags
        /// </summary>
        private HighLevelAPI81.TokenFlags _tokenFlags81 = null;

        /// <summary>
        /// Bits flags indicating capabilities and status of the device
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.Flags : _tokenFlags41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.Flags : _tokenFlags81.Flags;
            }
        }

        /// <summary>
        /// True if the token has its own random number generator
        /// </summary>
        public bool Rng
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.Rng : _tokenFlags41.Rng;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.Rng : _tokenFlags81.Rng;
            }
        }

        /// <summary>
        /// True if the token is write-protected
        /// </summary>
        public bool WriteProtected
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.WriteProtected : _tokenFlags41.WriteProtected;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.WriteProtected : _tokenFlags81.WriteProtected;
            }
        }

        /// <summary>
        /// True if there are some cryptographic functions that a user must be logged in to perform
        /// </summary>
        public bool LoginRequired
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.LoginRequired : _tokenFlags41.LoginRequired;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.LoginRequired : _tokenFlags81.LoginRequired;
            }
        }

        /// <summary>
        /// True if the normal user's PIN has been initialized
        /// </summary>
        public bool UserPinInitialized
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.UserPinInitialized : _tokenFlags41.UserPinInitialized;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.UserPinInitialized : _tokenFlags81.UserPinInitialized;
            }
        }

        /// <summary>
        /// True if a successful save of a session's cryptographic operations state always contains all keys needed to restore the state of the session
        /// </summary>
        public bool RestoreKeyNotNeeded
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.RestoreKeyNotNeeded : _tokenFlags41.RestoreKeyNotNeeded;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.RestoreKeyNotNeeded : _tokenFlags81.RestoreKeyNotNeeded;
            }
        }

        /// <summary>
        /// True if token has its own hardware clock
        /// </summary>
        public bool ClockOnToken
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.ClockOnToken : _tokenFlags41.ClockOnToken;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.ClockOnToken : _tokenFlags81.ClockOnToken;
            }
        }
        
        /// <summary>
        /// True if token has a “protected authentication path”, whereby a user can log into the token without passing a PIN through the Cryptoki library
        /// </summary>
        public bool ProtectedAuthenticationPath
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.ProtectedAuthenticationPath : _tokenFlags41.ProtectedAuthenticationPath;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.ProtectedAuthenticationPath : _tokenFlags81.ProtectedAuthenticationPath;
            }
        }

        /// <summary>
        /// True if a single session with the token can perform dual cryptographic operations
        /// </summary>
        public bool DualCryptoOperations
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.DualCryptoOperations : _tokenFlags41.DualCryptoOperations;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.DualCryptoOperations : _tokenFlags81.DualCryptoOperations;
            }
        }

        /// <summary>
        /// True if the token has been initialized using C_InitializeToken or an equivalent mechanism
        /// </summary>
        public bool TokenInitialized
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.TokenInitialized : _tokenFlags41.TokenInitialized;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.TokenInitialized : _tokenFlags81.TokenInitialized;
            }
        }

        /// <summary>
        /// True if the token supports secondary authentication for private key objects
        /// </summary>
        public bool SecondaryAuthentication
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.SecondaryAuthentication : _tokenFlags41.SecondaryAuthentication;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.SecondaryAuthentication : _tokenFlags81.SecondaryAuthentication;
            }
        }

        /// <summary>
        /// True if an incorrect user login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool UserPinCountLow
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.UserPinCountLow : _tokenFlags41.UserPinCountLow;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.UserPinCountLow : _tokenFlags81.UserPinCountLow;
            }
        }

        /// <summary>
        /// True if supplying an incorrect user PIN will make it to become locked
        /// </summary>
        public bool UserPinFinalTry
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.UserPinFinalTry : _tokenFlags41.UserPinFinalTry;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.UserPinFinalTry : _tokenFlags81.UserPinFinalTry;
            }
        }

        /// <summary>
        /// True if the user PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool UserPinLocked
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.UserPinLocked : _tokenFlags41.UserPinLocked;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.UserPinLocked : _tokenFlags81.UserPinLocked;
            }
        }

        /// <summary>
        /// True if the user PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card
        /// </summary>
        public bool UserPinToBeChanged
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.UserPinToBeChanged : _tokenFlags41.UserPinToBeChanged;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.UserPinToBeChanged : _tokenFlags81.UserPinToBeChanged;
            }
        }

        /// <summary>
        /// True if an incorrect SO login PIN has been entered at least once since the last successful authentication
        /// </summary>
        public bool SoPinCountLow
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.SoPinCountLow : _tokenFlags41.SoPinCountLow;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.SoPinCountLow : _tokenFlags81.SoPinCountLow;
            }
        }

        /// <summary>
        /// True if supplying an incorrect SO PIN will make it to become locked.
        /// </summary>
        public bool SoPinFinalTry
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.SoPinFinalTry : _tokenFlags41.SoPinFinalTry;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.SoPinFinalTry : _tokenFlags81.SoPinFinalTry;
            }
        }

        /// <summary>
        /// True if the SO PIN has been locked. User login to the token is not possible.
        /// </summary>
        public bool SoPinLocked
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.SoPinLocked : _tokenFlags41.SoPinLocked;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.SoPinLocked : _tokenFlags81.SoPinLocked;
            }
        }

        /// <summary>
        /// True if the SO PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        public bool SoPinToBeChanged
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _tokenFlags40.SoPinToBeChanged : _tokenFlags41.SoPinToBeChanged;
                else
                    return (Platform.StructPackingSize == 0) ? _tokenFlags80.SoPinToBeChanged : _tokenFlags81.SoPinToBeChanged;
            }
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI40.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags40 = tokenFlags;
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI41.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags41 = tokenFlags;
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI80.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags80 = tokenFlags;
        }

        /// <summary>
        /// Converts platform specific TokenFlags to platfrom neutral TokenFlags
        /// </summary>
        /// <param name="tokenFlags">Platform specific TokenFlags</param>
        internal TokenFlags(HighLevelAPI81.TokenFlags tokenFlags)
        {
            if (tokenFlags == null)
                throw new ArgumentNullException("tokenFlags");

            _tokenFlags81 = tokenFlags;
        }
    }
}
