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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Bit flags
    /// </summary>
    public class CKF
    {
        /// <summary>
        /// True if a token is present in the slot
        /// </summary>
        public const uint CKF_TOKEN_PRESENT = 0x00000001;

        /// <summary>
        /// True if the reader supports removable devices
        /// </summary>
        public const uint CKF_REMOVABLE_DEVICE = 0x00000002;
        
        /// <summary>
        /// True if the slot is a hardware slot, as opposed to a software slot implementing a "soft token"
        /// </summary>
        public const uint CKF_HW_SLOT = 0x00000004;
        
        /// <summary>
        /// True if the token has its own random number generator
        /// </summary>
        public const uint CKF_RNG = 0x00000001;

        /// <summary>
        /// True if the token is write-protected
        /// </summary>
        public const uint CKF_WRITE_PROTECTED = 0x00000002;

        /// <summary>
        /// True if there are some cryptographic functions that a user must be logged in to perform
        /// </summary>
        public const uint CKF_LOGIN_REQUIRED = 0x00000004;

        /// <summary>
        /// True if the normal user's PIN has been initialized
        /// </summary>
        public const uint CKF_USER_PIN_INITIALIZED = 0x00000008;

        /// <summary>
        /// True if a successful save of a session's cryptographic operations state always contains all keys needed to restore the state of the session
        /// </summary>
        public const uint CKF_RESTORE_KEY_NOT_NEEDED = 0x00000020;

        /// <summary>
        /// True if token has its own hardware clock
        /// </summary>
        public const uint CKF_CLOCK_ON_TOKEN = 0x00000040;

        /// <summary>
        /// True if token has a "protected authentication path", whereby a user can log into the token without passing a PIN through the Cryptoki library
        /// </summary>
        public const uint CKF_PROTECTED_AUTHENTICATION_PATH = 0x00000100;

        /// <summary>
        /// True if a single session with the token can perform dual cryptographic operations
        /// </summary>
        public const uint CKF_DUAL_CRYPTO_OPERATIONS = 0x00000200;

        /// <summary>
        /// True if the token has been initialized using C_InitializeToken or an equivalent mechanism outside the scope of this standard. Calling C_InitializeToken when this flag is set will cause the token to be reinitialized.
        /// </summary>
        public const uint CKF_TOKEN_INITIALIZED = 0x00000400;

        /// <summary>
        /// True if the token supports secondary authentication for private key objects.
        /// </summary>
        public const uint CKF_SECONDARY_AUTHENTICATION = 0x00000800;

        /// <summary>
        /// True if an incorrect user login PIN has been entered at least once since the last successful authentication.
        /// </summary>
        public const uint CKF_USER_PIN_COUNT_LOW = 0x00010000;

        /// <summary>
        /// True if supplying an incorrect user PIN will it to become locked.
        /// </summary>
        public const uint CKF_USER_PIN_FINAL_TRY = 0x00020000;

        /// <summary>
        /// True if the user PIN has been locked. User login to the token is not possible.
        /// </summary>
        public const uint CKF_USER_PIN_LOCKED = 0x00040000;

        /// <summary>
        /// True if the user PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        public const uint CKF_USER_PIN_TO_BE_CHANGED = 0x00080000;
        
        /// <summary>
        /// True if an incorrect SO login PIN has been entered at least once since the last successful authentication.
        /// </summary>
        public const uint CKF_SO_PIN_COUNT_LOW = 0x00100000;

        /// <summary>
        /// True if supplying an incorrect SO PIN will it to become locked.
        /// </summary>
        public const uint CKF_SO_PIN_FINAL_TRY = 0x00200000;

        /// <summary>
        /// True if the SO PIN has been locked. User login to the token is not possible.
        /// </summary>
        public const uint CKF_SO_PIN_LOCKED = 0x00400000;

        /// <summary>
        /// True if the SO PIN value is the default value set by token initialization or manufacturing, or the PIN has been expired by the card.
        /// </summary>
        public const uint CKF_SO_PIN_TO_BE_CHANGED = 0x00800000;

        /// <summary>
        /// True if the session is read/write; false if the session is read-only
        /// </summary>
        public const uint CKF_RW_SESSION = 0x00000002;
        
        /// <summary>
        /// This flag is provided for backward compatibility, and should always be set to true
        /// </summary>
        public const uint CKF_SERIAL_SESSION = 0x00000004;

        /// <summary>
        /// Identifies attribute whose value is an array of attributes
        /// </summary>
        public const uint CKF_ARRAY_ATTRIBUTE = 0x40000000;

        /// <summary>
        /// True if the mechanism is performed by the device; false if the mechanism is performed in software
        /// </summary>
        public const uint CKF_HW = 0x00000001;

        /// <summary>
        /// True if the mechanism can be used with C_EncryptInit
        /// </summary>
        public const uint CKF_ENCRYPT = 0x00000100;

        /// <summary>
        /// True if the mechanism can be used with C_DecryptInit
        /// </summary>
        public const uint CKF_DECRYPT = 0x00000200;

        /// <summary>
        /// True if the mechanism can be used with C_DigestInit
        /// </summary>
        public const uint CKF_DIGEST = 0x00000400;

        /// <summary>
        /// True if the mechanism can be used with C_SignInit
        /// </summary>
        public const uint CKF_SIGN = 0x00000800;

        /// <summary>
        /// True if the mechanism can be used with C_SignRecoverInit
        /// </summary>
        public const uint CKF_SIGN_RECOVER = 0x00001000;

        /// <summary>
        /// True if the mechanism can be used with C_VerifyInit
        /// </summary>
        public const uint CKF_VERIFY = 0x00002000;

        /// <summary>
        /// True if the mechanism can be used with C_VerifyRecoverInit
        /// </summary>
        public const uint CKF_VERIFY_RECOVER = 0x00004000;

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKey
        /// </summary>
        public const uint CKF_GENERATE = 0x00008000;

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKeyPair
        /// </summary>
        public const uint CKF_GENERATE_KEY_PAIR = 0x00010000;

        /// <summary>
        /// True if the mechanism can be used with C_WrapKey
        /// </summary>
        public const uint CKF_WRAP = 0x00020000;

        /// <summary>
        /// True if the mechanism can be used with C_UnwrapKey
        /// </summary>
        public const uint CKF_UNWRAP = 0x00040000;

        /// <summary>
        /// True if the mechanism can be used with C_DeriveKey
        /// </summary>
        public const uint CKF_DERIVE = 0x00080000;

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over Fp
        /// </summary>
        public const uint CKF_EC_F_P = 0x00100000;

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over F2m
        /// </summary>
        public const uint CKF_EC_F_2M = 0x00200000;

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice ecParameters
        /// </summary>
        public const uint CKF_EC_ECPARAMETERS = 0x00400000;

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice namedCurve
        /// </summary>
        public const uint CKF_EC_NAMEDCURVE = 0x00800000;

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point uncompressed
        /// </summary>
        public const uint CKF_EC_UNCOMPRESS = 0x01000000;

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point compressed
        /// </summary>
        public const uint CKF_EC_COMPRESS = 0x02000000;

        /// <summary>
        /// True if there is an extension to the flags; false if no extensions
        /// </summary>
        public const uint CKF_EXTENSION = 0x80000000;

        /// <summary>
        /// True if application threads which are executing calls to the library may not use native operating system calls to spawn new threads; false if they may
        /// </summary>
        public const uint CKF_LIBRARY_CANT_CREATE_OS_THREADS = 0x00000001;

        /// <summary>
        /// True if the library can use the native operation system threading model for locking; false otherwise
        /// </summary>
        public const uint CKF_OS_LOCKING_OK = 0x00000002;
        
        /// <summary>
        /// Flag indicating that C_WaitForSlotEvent should not block until an event occurs - it should return immediately instead
        /// </summary>
        public const uint CKF_DONT_BLOCK = 1;

        /// <summary>
        /// True if the OTP computation shall be for the next OTP, rather than the current one
        /// </summary>
        public const uint CKF_NEXT_OTP = 0x00000001;

        /// <summary>
        /// True if the OTP computation must not include a time value
        /// </summary>
        public const uint CKF_EXCLUDE_TIME = 0x00000002;

        /// <summary>
        /// True if the OTP computation must not include a counter value
        /// </summary>
        public const uint CKF_EXCLUDE_COUNTER = 0x00000004;

        /// <summary>
        /// True if the OTP computation must not include a challenge
        /// </summary>
        public const uint CKF_EXCLUDE_CHALLENGE = 0x00000008;

        /// <summary>
        /// True if the OTP computation must not include a PIN value
        /// </summary>
        public const uint CKF_EXCLUDE_PIN = 0x00000010;

        /// <summary>
        /// True if the OTP returned shall be in a form suitable for human consumption
        /// </summary>
        public const uint CKF_USER_FRIENDLY_OTP = 0x00000020;
    }
}
