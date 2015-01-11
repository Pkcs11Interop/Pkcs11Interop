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
    /// Return values
    /// </summary>
    public enum CKR : uint
    {
        /// <summary>
        /// The function executed successfully
        /// </summary>
        CKR_OK = 0x00000000,

        /// <summary>
        /// Cryptoki function aborts and returns CKR_FUNCTION_CANCELED, when CKR_CANCEL is returned by CKN_SURRENDER callback
        /// </summary>
        CKR_CANCEL = 0x00000001,

        /// <summary>
        /// The computer that the Cryptoki library is running on has insufficient memory to perform the requested function
        /// </summary>
        CKR_HOST_MEMORY = 0x00000002,
        
        /// <summary>
        /// The specified slot ID is not valid
        /// </summary>
        CKR_SLOT_ID_INVALID = 0x00000003,
        
        /// <summary>
        /// Some horrible, unrecoverable error has occurred
        /// </summary>
        CKR_GENERAL_ERROR = 0x00000005,

        /// <summary>
        /// The requested function could not be performed
        /// </summary>
        CKR_FUNCTION_FAILED = 0x00000006,

        /// <summary>
        /// Generic error code which indicates that the arguments supplied to the Cryptoki function were in some way not appropriate
        /// </summary>
        CKR_ARGUMENTS_BAD = 0x00000007,

        /// <summary>
        /// Returned when C_GetSlotEvent is called in non-blocking mode and there are no new slot events to return
        /// </summary>
        CKR_NO_EVENT = 0x00000008,

        /// <summary>
        /// Returned by C_Initialize when application did not allow library to use the native operation system threading model for locking and the library cannot function properly without being able to spawn new threads
        /// </summary>
        CKR_NEED_TO_CREATE_THREADS = 0x00000009,

        /// <summary>
        /// Returned by C_Initialize when the type of locking requested by the application for thread-safety is not available in this library
        /// </summary>
        CKR_CANT_LOCK = 0x0000000A,

        /// <summary>
        /// An attempt was made to set a value for an attribute which may not be set by the application, or which may not be modified by the application
        /// </summary>
        CKR_ATTRIBUTE_READ_ONLY = 0x00000010,

        /// <summary>
        /// An attempt was made to obtain the value of an attribute of an object which cannot be satisfied because the object is either sensitive or unextractable
        /// </summary>
        CKR_ATTRIBUTE_SENSITIVE = 0x00000011,

        /// <summary>
        /// An invalid attribute type was specified in a template
        /// </summary>
        CKR_ATTRIBUTE_TYPE_INVALID = 0x00000012,

        /// <summary>
        /// An invalid value was specified for a particular attribute in a template
        /// </summary>
        CKR_ATTRIBUTE_VALUE_INVALID = 0x00000013,

        /// <summary>
        /// The plaintext input data to a cryptographic operation is invalid
        /// </summary>
        CKR_DATA_INVALID = 0x00000020,

        /// <summary>
        /// The plaintext input data to a cryptographic operation has a bad length
        /// </summary>
        CKR_DATA_LEN_RANGE = 0x00000021,

        /// <summary>
        /// Some problem has occurred with the token and/or slot
        /// </summary>
        CKR_DEVICE_ERROR = 0x00000030,

        /// <summary>
        /// The token does not have sufficient memory to perform the requested function
        /// </summary>
        CKR_DEVICE_MEMORY = 0x00000031,

        /// <summary>
        /// The token was removed from its slot during the execution of the function
        /// </summary>
        CKR_DEVICE_REMOVED = 0x00000032,

        /// <summary>
        /// The encrypted input to a decryption operation has been determined to be invalid ciphertext
        /// </summary>
        CKR_ENCRYPTED_DATA_INVALID = 0x00000040,

        /// <summary>
        /// The ciphertext input to a decryption operation has been determined to be invalid ciphertext solely on the basis of its length
        /// </summary>
        CKR_ENCRYPTED_DATA_LEN_RANGE = 0x00000041,

        /// <summary>
        /// The function was canceled in mid-execution
        /// </summary>
        CKR_FUNCTION_CANCELED = 0x00000050,

        /// <summary>
        /// There is currently no function executing in parallel in the specified session
        /// </summary>
        CKR_FUNCTION_NOT_PARALLEL = 0x00000051,

        /// <summary>
        /// The requested function is not supported by this Cryptoki library
        /// </summary>
        CKR_FUNCTION_NOT_SUPPORTED = 0x00000054,

        /// <summary>
        /// The specified key handle is not valid
        /// </summary>
        CKR_KEY_HANDLE_INVALID = 0x00000060,

        /// <summary>
        /// Size of supplied key is outside the range of supported key sizes
        /// </summary>
        CKR_KEY_SIZE_RANGE = 0x00000062,

        /// <summary>
        /// The specified key is not the correct type of key to use with the specified mechanism
        /// </summary>
        CKR_KEY_TYPE_INCONSISTENT = 0x00000063,

        /// <summary>
        /// An extraneous key was supplied to C_SetOperationState
        /// </summary>
        CKR_KEY_NOT_NEEDED = 0x00000064,

        /// <summary>
        /// One of the keys supplied to C_SetOperationState is not the same key that was being used in the original saved session
        /// </summary>
        CKR_KEY_CHANGED = 0x00000065,

        /// <summary>
        /// Session state cannot be restored because C_SetOperationState needs to be supplied with one or more keys that were being used in the original saved session
        /// </summary>
        CKR_KEY_NEEDED = 0x00000066,

        /// <summary>
        /// Value of the specified key cannot be digested
        /// </summary>
        CKR_KEY_INDIGESTIBLE = 0x00000067,

        /// <summary>
        /// An attempt has been made to use a key for a cryptographic purpose that the key's attributes are not set to allow it to do
        /// </summary>
        CKR_KEY_FUNCTION_NOT_PERMITTED = 0x00000068,

        /// <summary>
        /// Library is unable to wrap the key in the requested way
        /// </summary>
        CKR_KEY_NOT_WRAPPABLE = 0x00000069,

        /// <summary>
        /// The specified private or secret key can't be wrapped
        /// </summary>
        CKR_KEY_UNEXTRACTABLE = 0x0000006A,

        /// <summary>
        /// An invalid mechanism was specified to the cryptographic operation
        /// </summary>
        CKR_MECHANISM_INVALID = 0x00000070,

        /// <summary>
        /// Invalid parameters were supplied to the mechanism specified to the cryptographic operation
        /// </summary>
        CKR_MECHANISM_PARAM_INVALID = 0x00000071,

        /// <summary>
        /// The specified object handle is not valid
        /// </summary>
        CKR_OBJECT_HANDLE_INVALID = 0x00000082,

        /// <summary>
        /// There is already an active operation which prevents Cryptoki from activating the specified operation
        /// </summary>
        CKR_OPERATION_ACTIVE = 0x00000090,

        /// <summary>
        /// There is no active operation of an appropriate type in the specified session
        /// </summary>
        CKR_OPERATION_NOT_INITIALIZED = 0x00000091,

        /// <summary>
        /// The specified PIN is incorrect
        /// </summary>
        CKR_PIN_INCORRECT = 0x000000A0,

        /// <summary>
        /// The specified PIN has invalid characters in it
        /// </summary>
        CKR_PIN_INVALID = 0x000000A1,

        /// <summary>
        /// The specified PIN is too long or too short
        /// </summary>
        CKR_PIN_LEN_RANGE = 0x000000A2,

        /// <summary>
        /// The specified PIN has expired
        /// </summary>
        CKR_PIN_EXPIRED = 0x000000A3,

        /// <summary>
        /// The specified PIN is locked and cannot be used
        /// </summary>
        CKR_PIN_LOCKED = 0x000000A4,

        /// <summary>
        /// The session was closed during the execution of the function
        /// </summary>
        CKR_SESSION_CLOSED = 0x000000B0,

        /// <summary>
        /// Attempt to open a session failed because the token has too many sessions already open
        /// </summary>
        CKR_SESSION_COUNT = 0x000000B1,

        /// <summary>
        /// The specified session handle was invalid at the time that the function was invoked
        /// </summary>
        CKR_SESSION_HANDLE_INVALID = 0x000000B3,

        /// <summary>
        /// The specified token does not support parallel sessions
        /// </summary>
        CKR_SESSION_PARALLEL_NOT_SUPPORTED = 0x000000B4,

        /// <summary>
        /// The specified session was unable to accomplish the desired action because it is a read-only session
        /// </summary>
        CKR_SESSION_READ_ONLY = 0x000000B5,

        /// <summary>
        /// Returned by C_InitToken when session with the token is open that prevents the token initialization
        /// </summary>
        CKR_SESSION_EXISTS = 0x000000B6,

        /// <summary>
        /// A read-only session already exists, and so the SO cannot be logged in
        /// </summary>
        CKR_SESSION_READ_ONLY_EXISTS = 0x000000B7,

        /// <summary>
        /// A read/write SO session already exists, and so a read-only session cannot be opened
        /// </summary>
        CKR_SESSION_READ_WRITE_SO_EXISTS = 0x000000B8,

        /// <summary>
        /// The provided signature/MAC is invalid
        /// </summary>
        CKR_SIGNATURE_INVALID = 0x000000C0,

        /// <summary>
        /// The provided signature/MAC can be seen to be invalid solely on the basis of its length
        /// </summary>
        CKR_SIGNATURE_LEN_RANGE = 0x000000C1,

        /// <summary>
        /// The template specified for creating an object is incomplete, and lacks some necessary attributes
        /// </summary>
        CKR_TEMPLATE_INCOMPLETE = 0x000000D0,

        /// <summary>
        /// The template specified for creating an object has conflicting attributes
        /// </summary>
        CKR_TEMPLATE_INCONSISTENT = 0x000000D1,

        /// <summary>
        /// The token was not present in its slot at the time that the function was invoked
        /// </summary>
        CKR_TOKEN_NOT_PRESENT = 0x000000E0,

        /// <summary>
        /// The Cryptoki library and/or slot does not recognize the token in the slot
        /// </summary>
        CKR_TOKEN_NOT_RECOGNIZED = 0x000000E1,

        /// <summary>
        /// The requested action could not be performed because the token is write-protected
        /// </summary>
        CKR_TOKEN_WRITE_PROTECTED = 0x000000E2,

        /// <summary>
        /// Key handle specified to be used to unwrap another key is not valid
        /// </summary>
        CKR_UNWRAPPING_KEY_HANDLE_INVALID = 0x000000F0,

        /// <summary>
        /// Unwrapping opration cannot be carried out because the supplied key's size is outside the range of supported key sizes
        /// </summary>
        CKR_UNWRAPPING_KEY_SIZE_RANGE = 0x000000F1,

        /// <summary>
        /// Type of the key specified to unwrap another key is not consistent with the mechanism specified for unwrapping
        /// </summary>
        CKR_UNWRAPPING_KEY_TYPE_INCONSISTENT = 0x000000F2,

        /// <summary>
        /// User cannot be logged into the session because it is already logged into the session
        /// </summary>
        CKR_USER_ALREADY_LOGGED_IN = 0x00000100,

        /// <summary>
        /// The desired action cannot be performed because the appropriate user is not logged in
        /// </summary>
        CKR_USER_NOT_LOGGED_IN = 0x00000101,

        /// <summary>
        /// Normal user's PIN has not yet been initialized
        /// </summary>
        CKR_USER_PIN_NOT_INITIALIZED = 0x00000102,

        /// <summary>
        /// Invalid user type specified
        /// </summary>
        CKR_USER_TYPE_INVALID = 0x00000103,

        /// <summary>
        /// User cannot be logged into the session because another user is already logged into the session
        /// </summary>
        CKR_USER_ANOTHER_ALREADY_LOGGED_IN = 0x00000104,

        /// <summary>
        /// An attempt was made to have more distinct users simultaneously logged into the token than the token and/or library permits
        /// </summary>
        CKR_USER_TOO_MANY_TYPES = 0x00000105,

        /// <summary>
        /// Provided wrapped key is not valid
        /// </summary>
        CKR_WRAPPED_KEY_INVALID = 0x00000110,

        /// <summary>
        /// Provided wrapped key can be seen to be invalid solely on the basis of its length
        /// </summary>
        CKR_WRAPPED_KEY_LEN_RANGE = 0x00000112,

        /// <summary>
        /// Key handle specified to be used to wrap another key is not valid
        /// </summary>
        CKR_WRAPPING_KEY_HANDLE_INVALID = 0x00000113,

        /// <summary>
        /// Wrapping operation cannot be carried out because the supplied wrapping key's size is outside the range of supported key sizes
        /// </summary>
        CKR_WRAPPING_KEY_SIZE_RANGE = 0x00000114,

        /// <summary>
        /// Type of the key specified to wrap another key is not consistent with the mechanism specified for wrapping
        /// </summary>
        CKR_WRAPPING_KEY_TYPE_INCONSISTENT = 0x00000115,

        /// <summary>
        /// Token's random number generator does not accept seeding from an application
        /// </summary>
        CKR_RANDOM_SEED_NOT_SUPPORTED = 0x00000120,

        /// <summary>
        /// Token doesn't have a random number generator
        /// </summary>
        CKR_RANDOM_NO_RNG = 0x00000121,

        /// <summary>
        /// Invalid or unsupported domain parameters were supplied to the function
        /// </summary>
        CKR_DOMAIN_PARAMS_INVALID = 0x00000130,

        /// <summary>
        /// The output of the function is too large to fit in the supplied buffer
        /// </summary>
        CKR_BUFFER_TOO_SMALL = 0x00000150,

        /// <summary>
        /// Supplied saved cryptographic operations state is invalid, and so it cannot be restored to the specified session
        /// </summary>
        CKR_SAVED_STATE_INVALID = 0x00000160,

        /// <summary>
        /// The information requested could not be obtained because the token considers it sensitive, and is not able or willing to reveal it
        /// </summary>
        CKR_INFORMATION_SENSITIVE = 0x00000170,

        /// <summary>
        /// The cryptographic operations state of the specified session cannot be saved
        /// </summary>
        CKR_STATE_UNSAVEABLE = 0x00000180,

        /// <summary>
        /// Function cannot be executed because the Cryptoki library has not yet been initialized
        /// </summary>
        CKR_CRYPTOKI_NOT_INITIALIZED = 0x00000190,

        /// <summary>
        /// Cryptoki library has already been initialized
        /// </summary>
        CKR_CRYPTOKI_ALREADY_INITIALIZED = 0x00000191,

        /// <summary>
        /// Returned by mutex-handling functions who are passed a bad mutex object as an argument
        /// </summary>
        CKR_MUTEX_BAD = 0x000001A0,

        /// <summary>
        /// Mutex supplied to the mutex-unlocking function was not locked
        /// </summary>
        CKR_MUTEX_NOT_LOCKED = 0x000001A1,

        /// <summary>
        /// The supplied OTP was not accepted and the library requests a new OTP computed using a new PIN
        /// </summary>
        CKR_NEW_PIN_MODE = 0x000001B0,
        
        /// <summary>
        /// The supplied OTP was correct but indicated a larger than normal drift in the token's internal state. Application should provide the next one-time password to the library for verification.
        /// </summary>
        CKR_NEXT_OTP = 0x000001B1,

        /// <summary>
        /// The signature request is rejected by the user
        /// </summary>
        CKR_FUNCTION_REJECTED = 0x00000200,

        /// <summary>
        /// Permanently reserved for token vendors
        /// </summary>
        CKR_VENDOR_DEFINED = 0x80000000
    }
}
