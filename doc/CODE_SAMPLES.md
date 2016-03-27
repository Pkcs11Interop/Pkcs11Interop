# Pkcs11Interop code samples

Pkcs11Interop source code contains unit tests covering all methods of PKCS#11 API. Unit tests are well documented and they also serve as official code samples.

**WARNING: Our documentation and code samples do not cover the theory of security/cryptography or the strengths/weaknesses of specific algorithms. You should always understand what you are doing and why. Please do not simply copy our code samples and expect it to fully solve your usage scenario. Cryptography is an advanced topic and one should consult a solid and preferably recent reference in order to make the best of it.**

Following source files contains artifacts reused in all unit tests:

* Source file: [Settings.cs](../src/Pkcs11InteropTests/Settings.cs)  
  Static class with general test settings such as path to unmanaged PKCS#11 library, PINs etc.

* Source file: [HighLevelAPI/Helpers.cs](../src/Pkcs11InteropTests/HighLevelAPI/Helpers.cs)  
  Static class with utility methods that help to increase the readability of unit tests.

Following source files contain valuable code samples:

* Test file: [HighLevelAPI/_01_InitializeTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_01_InitializeTest.cs)  
  Involved PKCS#11 functions: C_Initialize, C_Finalize  
  Demonstrates how to initialize and release PKCS#11 library in single-threaded or multi-threaded applications.

* Test file: [HighLevelAPI/_02_GetInfoTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_02_GetInfoTest.cs)  
  Involved PKCS#11 functions: C_GetInfo  
  Demonstrates how to get basic information about PKCS#11 library.

* Test file: [HighLevelAPI/_03_SlotListInfoAndEventTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_03_SlotListInfoAndEventTest.cs)  
  Involved PKCS#11 functions: C_GetSlotList, C_GetSlotInfo, C_WaitForSlotEvent  
  Demonstrates how to get the list of available slots and basic information about the slot.

* Test file: [HighLevelAPI/_04_TokenInfoTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_04_TokenInfoTest.cs)  
  Involved PKCS#11 functions: C_GetTokenInfo  
  Demonstrates how to get basic information about the token.

* Test file: [HighLevelAPI/_05_MechanismListAndInfoTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_05_MechanismListAndInfoTest.cs)  
  Involved PKCS#11 functions: C_GetMechanismList, C_GetMechanismInfo  
  Demonstrates how to get the list of supported mechanisms and more details about one of them.

* Test file: [HighLevelAPI/_06_SessionTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_06_SessionTest.cs)  
  Involved PKCS#11 functions: C_OpenSession, C_CloseSession, C_CloseAllSessions, C_GetSessionInfo  
  Demonstrates how to open and close read-only or read-write session with the token.

* Test file: [HighLevelAPI/_07_OperationStateTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_07_OperationStateTest.cs)  
  Involved PKCS#11 functions: C_GetOperationState, C_SetOperationState  
  Demonstrates how to save and restore the state of cryptographic operation.

* Test file: [HighLevelAPI/_08_LoginTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_08_LoginTest.cs)  
  Involved PKCS#11 functions: C_Login, C_Logout  
  Demonstrates how to login and logout as normal user or security officer.

* Test file: [HighLevelAPI/x_09_InitTokenAndPinTestx.cs](../src/Pkcs11InteropTests/HighLevelAPI/_09_InitTokenAndPinTest.cs)  
  Involved PKCS#11 functions: C_InitToken, C_InitPIN  
  Demonstrates how to initialize token and PIN.

* Test file: [HighLevelAPI/_10_SetPinTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_10_SetPinTest.cs)  
  Involved PKCS#11 functions: C_SetPIN  
  Demonstrates how to change PIN.

* Test file: [HighLevelAPI/_11_SeedAndGenerateRandomTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_11_SeedAndGenerateRandomTest.cs)  
  Involved PKCS#11 functions: C_SeedRandom, C_GenerateRandom  
  Demonstrates how to seed RNG and generate random bytes.

* Test file: [HighLevelAPI/_12_DigestTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_12_DigestTest.cs)  
  Involved PKCS#11 functions: C_DigestInit, C_Digest, C_DigestUpdate, C_DigestFinal, C_DigestKey  
  Demonstrates how to compute digest of arbitrary data.

* Test file: [HighLevelAPI/_13_ObjectAttributeTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_13_ObjectAttributeTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to create and dispose object attributes holding data of various types.

* Test file: [HighLevelAPI/_14_MechanismTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_14_MechanismTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to create and dispose mechanisms with parameters of various types.

* Test file: [HighLevelAPI/_15_CreateCopyDestroyObjectTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_15_CreateCopyDestroyObjectTest.cs)  
  Involved PKCS#11 functions: C_CreateObject, C_DestroyObject, C_CopyObject, C_GetObjectSize  
  Demonstrates how to create, destroy and copy data objects.

* Test file: [HighLevelAPI/_16_GetAndSetAttributeValueTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_16_GetAndSetAttributeValueTest.cs)  
  Involved PKCS#11 functions: C_GetAttributeValue, C_SetAttributeValue  
  Demonstrates how to get and set values of object attributes.

* Test file: [HighLevelAPI/_17_ObjectFindingTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_17_ObjectFindingTest.cs)  
  Involved PKCS#11 functions: C_FindObjectsInit, C_FindObjects, C_FindObjectsFinal  
  Demonstrates how to search for objects (keys, certificates, ...).

* Test file: [HighLevelAPI/_18_GenerateKeyAndKeyPairTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_18_GenerateKeyAndKeyPairTest.cs)  
  Involved PKCS#11 functions: C_GenerateKey, C_GenerateKeyPair  
  Demonstrates how to generate symmetric keys or asymmetric key pairs.

* Test file: [HighLevelAPI/_19_EncryptAndDecryptTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_19_EncryptAndDecryptTest.cs)  
  Involved PKCS#11 functions: C_EncryptInit, C_Encrypt, C_EncryptUpdate, C_EncryptFinal, C_DecryptInit, C_Decrypt, C_DecryptUpdate, C_DecryptFinal  
  Demonstrates how to encrypt and decrypt arbitrary data.

* Test file: [HighLevelAPI/_20_SignAndVerifyTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_20_SignAndVerifyTest.cs)  
  Involved PKCS#11 functions: C_SignInit, C_Sign, C_SignUpdate, C_SignFinal, C_VerifyInit, C_Verify, C_VerifyUpdate, C_VerifyFinal  
  Demonstrates how to create and verify signature of arbitrary data.

* Test file: [HighLevelAPI/_21_SignAndVerifyRecoverTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_21_SignAndVerifyRecoverTest.cs)  
  Involved PKCS#11 functions: C_SignRecoverInit, C_SignRecover, C_VerifyRecoverInit, C_VerifyRecover  
  Demonstrates how to create and verify signature of arbitrary data where the data can be recovered from the signature.

* Test file: [HighLevelAPI/_22_DigestEncryptAndDecryptDigestTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_22_DigestEncryptAndDecryptDigestTest.cs)  
  Involved PKCS#11 functions: C_DigestEncryptUpdate, C_DecryptDigestUpdate  
  Demonstrates how to digest and encrypt arbitrary data simultaneously and then decrypt and digest simultaneously.

* Test file: [HighLevelAPI/_23_SignEncryptAndDecryptVerifyTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_23_SignEncryptAndDecryptVerifyTest.cs)  
  Involved PKCS#11 functions: C_SignEncryptUpdate, C_DecryptVerifyUpdate  
  Demonstrates how to encrypt and create signature of arbitrary data simultaneously and then decrypt and verify signature simultaneously.

* Test file: [HighLevelAPI/_24_WrapAndUnwrapKeyTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_24_WrapAndUnwrapKeyTest.cs)  
  Involved PKCS#11 functions: C_WrapKey, C_UnwrapKey  
  Demonstrates how to wrap and unwrap key.

* Test file: [HighLevelAPI/_25_DeriveKeyTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_25_DeriveKeyTest.cs)  
  Involved PKCS#11 functions: C_DeriveKey  
  Demonstrates how to perform key derivation.

* Test file: [HighLevelAPI/_26_LegacyParallelFunctionsTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_26_LegacyParallelFunctionsTest.cs)  
  Involved PKCS#11 functions: C_GetFunctionStatus, C_CancelFunction  
  Demonstrates how to call legacy parallel functions.

* Test file: [HighLevelAPI/_27_Pkcs11UriUtilsTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_27_Pkcs11UriUtilsTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to use PKCS#11 URI in a signature creation application.

* Test file: [HighLevelAPI/_28_Pkcs11ClassExtensionTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_28_Pkcs11ClassExtensionTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to extend Pkcs11 classes with the ability to call C_GetUnmanagedStructSizeList vendor specific function of [PKCS11-MOCK](https://github.com/Pkcs11Interop/pkcs11-mock) module.

* Test file: [HighLevelAPI/_29_SlotClassExtensionTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_29_SlotClassExtensionTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to extend Slot classes with the ability to call C_EjectToken vendor specific function of [PKCS11-MOCK](https://github.com/Pkcs11Interop/pkcs11-mock) module.

* Test file: [HighLevelAPI/_30_SessionClassExtensionTest.cs](../src/Pkcs11InteropTests/HighLevelAPI/_30_SessionClassExtensionTest.cs)  
  Involved PKCS#11 functions: None  
  Demonstrates how to extend Session classes with the ability to call C_InteractiveLogin vendor specific function of [PKCS11-MOCK](https://github.com/Pkcs11Interop/pkcs11-mock) module.
