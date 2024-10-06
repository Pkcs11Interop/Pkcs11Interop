# Pkcs11Interop Code Samples

The Pkcs11Interop source code includes unit tests that cover all methods of the PKCS#11 API. These unit tests are documented and also serve as official code samples.

**WARNING: Our documentation and code samples do not cover the theory of security/cryptography or the strengths/weaknesses of specific algorithms. You should always understand what you are doing and why. Please do not simply copy our code samples and expect them to fully solve your usage scenario. Cryptography is an advanced topic, and it is essential to consult a solid, preferably recent, reference to make the best use of it.**

*Note: New objects/keys with "Pkcs11Interop" label are created/generated for every test method that needs to work with objects. These objects are deleted at the end of the test method, but they may persist if the test fails. Any leftover objects with the "Pkcs11Interop" label can be safely deleted using the [Pkcs11Admin](https://www.pkcs11admin.net/) application.*

## Reusable Artifacts in Unit Tests

The following source files contain artifacts reused in all unit tests:

* **Source file: [Settings.cs](../src/Pkcs11Interop.Tests/Settings.cs)**  
  A static class with general test settings, including the path to the unmanaged PKCS#11 library, PINs, etc.

* **Source file: [HighLevelAPI/Helpers.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/Helpers.cs)**  
  A static class with utility methods that enhance the readability of unit tests.

## Valuable Code Samples

The following source files contain valuable code samples:

* **Test file: [HighLevelAPI/_01_InitializeTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_01_InitializeTest.cs)**  
  Involved PKCS#11 functions: `C_Initialize`, `C_Finalize`  
  Demonstrates how to initialize and release the PKCS#11 library in single-threaded or multi-threaded applications.

* **Test file: [HighLevelAPI/_02_GetInfoTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_02_GetInfoTest.cs)**  
  Involved PKCS#11 functions: `C_GetInfo`  
  Demonstrates how to obtain basic information about the PKCS#11 library.

* **Test file: [HighLevelAPI/_03_SlotListInfoAndEventTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_03_SlotListInfoAndEventTest.cs)**  
  Involved PKCS#11 functions: `C_GetSlotList`, `C_GetSlotInfo`, `C_WaitForSlotEvent`  
  Demonstrates how to retrieve the list of available slots and basic information about each slot.

* **Test file: [HighLevelAPI/_04_TokenInfoTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_04_TokenInfoTest.cs)**  
  Involved PKCS#11 functions: `C_GetTokenInfo`  
  Demonstrates how to obtain basic information about the token.

* **Test file: [HighLevelAPI/_05_MechanismListAndInfoTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_05_MechanismListAndInfoTest.cs)**  
  Involved PKCS#11 functions: `C_GetMechanismList`, `C_GetMechanismInfo`  
  Demonstrates how to retrieve the list of supported mechanisms and obtain details about one of them.

* **Test file: [HighLevelAPI/_06_SessionTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_06_SessionTest.cs)**  
  Involved PKCS#11 functions: `C_OpenSession`, `C_CloseSession`, `C_CloseAllSessions`, `C_GetSessionInfo`  
  Demonstrates how to open and close read-only or read-write sessions with the token.

* **Test file: [HighLevelAPI/_07_OperationStateTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_07_OperationStateTest.cs)**  
  Involved PKCS#11 functions: `C_GetOperationState`, `C_SetOperationState`  
  Demonstrates how to save and restore the state of cryptographic operations.

* **Test file: [HighLevelAPI/_08_LoginTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_08_LoginTest.cs)**  
  Involved PKCS#11 functions: `C_Login`, `C_Logout`  
  Demonstrates how to log in and log out as a normal user or security officer.

* **Test file: [HighLevelAPI/_09_InitTokenAndPinTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_09_InitTokenAndPinTest.cs)**  
  Involved PKCS#11 functions: `C_InitToken`, `C_InitPIN`  
  Demonstrates how to initialize a token and its PIN.

* **Test file: [HighLevelAPI/_10_SetPinTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_10_SetPinTest.cs)**  
  Involved PKCS#11 functions: `C_SetPIN`  
  Demonstrates how to change a PIN.

* **Test file: [HighLevelAPI/_11_SeedAndGenerateRandomTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_11_SeedAndGenerateRandomTest.cs)**  
  Involved PKCS#11 functions: `C_SeedRandom`, `C_GenerateRandom`  
  Demonstrates how to seed the RNG and generate random bytes.

* **Test file: [HighLevelAPI/_12_DigestTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_12_DigestTest.cs)**  
  Involved PKCS#11 functions: `C_DigestInit`, `C_Digest`, `C_DigestUpdate`, `C_DigestFinal`, `C_DigestKey`  
  Demonstrates how to compute the digest of arbitrary data.

* **Test file: [HighLevelAPI/_13_ObjectAttributeTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_13_ObjectAttributeTest.cs)**  
  Involved PKCS#11 functions: None  
  Demonstrates how to create and dispose of object attributes holding data of various types.

* **Test file: [HighLevelAPI/_14_MechanismTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_14_MechanismTest.cs)**  
  Involved PKCS#11 functions: None  
  Demonstrates how to create and dispose of mechanisms with parameters of various types.

* **Test file: [HighLevelAPI/_15_CreateCopyDestroyObjectTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_15_CreateCopyDestroyObjectTest.cs)**  
  Involved PKCS#11 functions: `C_CreateObject`, `C_DestroyObject`, `C_CopyObject`, `C_GetObjectSize`  
  Demonstrates how to create, destroy, and copy data objects.

* **Test file: [HighLevelAPI/_16_GetAndSetAttributeValueTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_16_GetAndSetAttributeValueTest.cs)**  
  Involved PKCS#11 functions: `C_GetAttributeValue`, `C_SetAttributeValue`  
  Demonstrates how to get and set values of object attributes.

* **Test file: [HighLevelAPI/_17_ObjectFindingTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_17_ObjectFindingTest.cs)**  
  Involved PKCS#11 functions: `C_FindObjectsInit`, `C_FindObjects`, `C_FindObjectsFinal`  
  Demonstrates how to search for objects (keys, certificates, etc.).

* **Test file: [HighLevelAPI/_18_GenerateKeyAndKeyPairTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_18_GenerateKeyAndKeyPairTest.cs)**  
  Involved PKCS#11 functions: `C_GenerateKey`, `C_GenerateKeyPair`  
  Demonstrates how to generate symmetric keys or asymmetric key pairs.

* **Test file: [HighLevelAPI/_19_EncryptAndDecryptTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_19_EncryptAndDecryptTest.cs)**  
  Involved PKCS#11 functions: `C_EncryptInit`, `C_Encrypt`, `C_EncryptUpdate`, `C_EncryptFinal`, `C_DecryptInit`, `C_Decrypt`, `C_DecryptUpdate`, `C_DecryptFinal`  
  Demonstrates how to encrypt and decrypt arbitrary data.

* **Test file: [HighLevelAPI/_20_SignAndVerifyTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_20_SignAndVerifyTest.cs)**  
  Involved PKCS#11 functions: `C_SignInit`, `C_Sign`, `C_SignUpdate`, `C_SignFinal`, `C_VerifyInit`, `C_Verify`, `C_VerifyUpdate`, `C_VerifyFinal`  
  Demonstrates how to create and verify a signature of arbitrary data.

* **Test file: [HighLevelAPI/_21_SignAndVerifyRecoverTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_21_SignAndVerifyRecoverTest.cs)**  
  Involved PKCS#11 functions: `C_SignRecoverInit`, `C_SignRecover`, `C_VerifyRecoverInit`, `C_VerifyRecover`  
  Demonstrates how to create and verify a signature of arbitrary data where the data can be recovered from the signature.

* **Test file: [HighLevelAPI/_22_DigestEncryptAndDecryptDigestTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_22_DigestEncryptAndDecryptDigestTest.cs)**  
  Involved PKCS#11 functions: `C_DigestEncryptUpdate`, `C_DecryptDigestUpdate`  
  Demonstrates how to digest and encrypt arbitrary data simultaneously and then decrypt and digest simultaneously.

* **Test file: [HighLevelAPI/_23_SignEncryptAndDecryptVerifyTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_23_SignEncryptAndDecryptVerifyTest.cs)**  
  Involved PKCS#11 functions: `C_SignEncryptUpdate`, `C_DecryptVerifyUpdate`  
  Demonstrates how to encrypt and create a signature of arbitrary data simultaneously and then decrypt and verify the signature simultaneously.

* **Test file: [HighLevelAPI/_24_WrapAndUnwrapKeyTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_24_WrapAndUnwrapKeyTest.cs)**  
  Involved PKCS#11 functions: `C_WrapKey`, `C_UnwrapKey`  
  Demonstrates how to wrap and unwrap keys.

* **Test file: [HighLevelAPI/_25_DeriveKeyTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_25_DeriveKeyTest.cs)**  
  Involved PKCS#11 functions: `C_DeriveKey`  
  Demonstrates how to perform key derivation.

* **Test file: [HighLevelAPI/_26_LegacyParallelFunctionsTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_26_LegacyParallelFunctionsTest.cs)**  
  Involved PKCS#11 functions: `C_GetFunctionStatus`, `C_CancelFunction`  
  Demonstrates how to call legacy parallel functions.

* **Test file: [HighLevelAPI/_27_Pkcs11UriUtilsTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_27_Pkcs11UriUtilsTest.cs)**  
  Involved PKCS#11 functions: None  
  Demonstrates how to use PKCS#11 URI in a signature creation application.

* **Test file: [HighLevelAPI/_28_VendorExtensionsTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_28_VendorExtensionsTest.cs)**  
  Involved vendor-specific PKCS#11 functions: `C_GetUnmanagedStructSizeList`, `C_EjectToken`, `C_InteractiveLogin`  
  Demonstrates how to use the [Pkcs11Interop.Mock](../src/Pkcs11Interop.Mock/) library with vendor-specific extensions of the [PKCS11-MOCK](https://github.com/Pkcs11Interop/pkcs11-mock) module.

* **Test file: [HighLevelAPI/_29_LoggingTest.cs](../src/Pkcs11Interop.Tests/HighLevelAPI/_29_LoggingTest.cs)**  
  Involved PKCS#11 functions: None  
  Demonstrates how to utilize managed logging in the Pkcs11Interop library.

[Next page >](05_TROUBLESHOOTING.md)