/*! \mainpage Managed .NET wrapper for unmanaged PKCS#11 libraries
 * 
 * \tableofcontents
 * 
 * \section sec_overview Overview
 * 
 * <a href="https://www.oasis-open.org/committees/pkcs11/">PKCS#11</a> is cryptography standard originally published by RSA Laboratories that defines ANSI C API to access smart cards and other types of cryptographic hardware. Standard is currently being maintained and developed by the OASIS PKCS 11 Technical Committee.
 * 
 * <a href="http://www.pkcs11interop.net">Pkcs11interop</a> is managed library written in C# that brings full power of PKCS#11 API to the .NET environment. It uses System.Runtime.InteropServices to define platform invoke methods for accessing unmanaged PKCS#11 API and specifies how data is marshaled between managed and unmanaged memory. Pkcs11Interop library supports both 32-bit and 64-bit platforms and can be used with <a href="http://www.microsoft.com/net">.NET Framework</a> 2.0 or higher on Microsoft Windows or with <a href="http://www.mono-project.com/">Mono</a> on Linux, Mac OS X, BSD and others.
 * 
 * \section sec_library_desing Library design
 *
 * Pkcs11Interop API is logically divided into the set of <b>LowLevelAPIs</b> and the set of <b>HighLevelAPIs.</b>
 * 
  * \subsection sec_library_desing_lowlevelaapi LowLevelAPI
 * 
 * - Provides exact PKCS#11 interface as defined by RSA Laboratories
 * - Gives the developer a full power of PKCS#11 ANSI C API
 * - Requires C style coding with manual management of unmanaged memory
 * 
 * \subsection sec_library_desing_highlevelaapi HighLevelAPI
 * 
 * - Built on top of LowLevelAPI
 * - Utilizes developer friendly constructs and supports Streams
 * - Garbage collector takes care of unmanaged memory handling
 * 
 * The following figure shows the architecture of Pkcs11Interop:
 * 
 * \image html pkcs11interop-architecture.png 
 *
 * You may be wondering why there is more than one LowLevelAPI and what do numbers 4 and 8 in the namespaces mean:
 * 
 * The C 'long' type is extensively used throughout the PKCS#11 ANSI C API as CK_ULONG type and unfortunately it is the most difficult type to marshal since there is no type in .NET that matches its size. The problem is that the C 'long' type can be 4 bytes long on some platforms (Win32, Win64 and Unix32) and in the same time it can be 8 bytes long on the other platforms (Unix64). In .NET there is 'int' type which is 4 bytes long regardless of platform and there is 'long' type which is 8 bytes long regardless of platform. Neither of them can be used as a multiplatform alternative for C 'long' type and the only solution is to use and marshal two different sets of structures, one with 'int' .NET type for platforms where C 'long' type is 4 bytes long (LowLevelAPI41 and HighLevelAPI41) and the other with 'long' .NET type for platforms where C 'long' type is 8 bytes long (LowLevelAPI81 and HighLevel8).
 * 
 * <b>When developing your application you should always prefer Net.Pkcs11Interop.HighLevelAPI that automatically uses correct set of platform dependent APIs.</b>
 * 
 * \section sec_supported_features Supported features
 * 
 * Pkcs11Interop implements full PKCS#11 v2.20 specification with the following mostly temporary exceptions and limitations:
 * 
 * - Reading the value of CKA_WRAP_TEMPLATE and CKA_UNWRAP_TEMPLATE attributes is supported only in LowLevelAPIs.
 * - Locking related types CK_CREATEMUTEX, CK_DESTROYMUTEX, CK_LOCKMUTEX and CK_UNLOCKMUTEX are not supported, but native OS threading model (CKF_OS_LOCKING_OK) can be used without any problems.
 * - CK_NOTIFY notification callbacks are not supported.
 * 
 * \section sec_documentation Documentation
 * 
 * Before you start using Pkcs11Interop you should at least read "Chapter 2 - Scope", "Chapter 6 - General overview" and "Chapter 10 - Objects" of <a href="/rsa-pkcs11-2.20/pkcs-11v2-20.pdf">PKCS#11&nbsp;standard</a> which has later been extended by <a href="/rsa-pkcs11-2.20/pkcs-11v2-20a1.pdf">Amendment&nbsp;1</a>, <a href="/rsa-pkcs11-2.20/pkcs-11v2-20a2.pdf">Amendment&nbsp;2</a> and <a href="/rsa-pkcs11-2.20/pkcs-11v2-20a3.pdf">Amendment&nbsp;3</a>.
 * 
 * \section sec_code_samples Code samples
 * 
 * Pkcs11Interop source code contains unit tests covering all methods of PKCS#11 API. Unit tests are well documented and they also serve as <a href="examples.html">official code samples</a>.
 * 
 * <b>WARNING: Our documentation and code samples do not cover the theory of security/cryptography or the strengths/weaknesses of specific algorithms. You should always understand what you are doing and why. Please do not simply copy our code samples and expect it to fully solve your usage scenario. Cryptography is an advanced topic and one should consult a solid and preferably recent reference in order to make the best of it.</b>
 * 
 * \section sec_more_info More info
 * 
 * Please visit project website - <a class="el" href="http://www.pkcs11interop.net">www.pkcs11interop.net</a> - for more information regarding updates, licensing, support etc.
 */


/*! 
 * \namespace Net.Pkcs11Interop
 * \brief Base namespace of Pkcs11Interop project
 * 
 * \namespace Net.Pkcs11Interop.Common
 * \brief Components shared by all the LowLevelAPIs and HighLevelAPIs
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI41
 * \brief Low level C-like API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32)
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI41
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI41
 * \brief High level .NET friendly API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32)
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI41
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI81
 * \brief Low level C-like API for platforms where C 'long' type is 8 bytes long (Unix64)
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI81
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI81
 * \brief High level .NET friendly API for platforms where C 'long' type is 8 bytes long (Unix64)
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI81
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI
 * \brief High level .NET friendly API usable for multiplatform development
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI
 */


/*!
 * \example HighLevelAPI/_01_InitializeTest.cs
 * \example HighLevelAPI/_02_GetInfoTest.cs
 * \example HighLevelAPI/_03_SlotListInfoAndEventTest.cs
 * \example HighLevelAPI/_04_TokenInfoTest.cs
 * \example HighLevelAPI/_05_MechanismListAndInfoTest.cs
 * \example HighLevelAPI/_06_SessionTest.cs
 * \example HighLevelAPI/_07_OperationStateTest.cs
 * \example HighLevelAPI/_08_LoginTest.cs
 * \example HighLevelAPI/_09_InitTokenAndPinTest.cs
 * \example HighLevelAPI/_10_SetPinTest.cs
 * \example HighLevelAPI/_11_SeedAndGenerateRandomTest.cs
 * \example HighLevelAPI/_12_DigestTest.cs
 * \example HighLevelAPI/_13_ObjectAttributeTest.cs
 * \example HighLevelAPI/_14_MechanismTest.cs
 * \example HighLevelAPI/_15_CreateCopyDestroyObjectTest.cs
 * \example HighLevelAPI/_16_GetAndSetAttributeValueTest.cs
 * \example HighLevelAPI/_17_ObjectFindingTest.cs
 * \example HighLevelAPI/_18_GenerateKeyAndKeyPairTest.cs
 * \example HighLevelAPI/_19_EncryptAndDecryptTest.cs
 * \example HighLevelAPI/_20_SignAndVerifyTest.cs
 * \example HighLevelAPI/_21_SignAndVerifyRecoverTest.cs
 * \example HighLevelAPI/_22_DigestEncryptAndDecryptDigestTest.cs
 * \example HighLevelAPI/_23_SignEncryptAndDecryptVerifyTest.cs
 * \example HighLevelAPI/_24_WrapAndUnwrapKeyTest.cs
 * \example HighLevelAPI/_25_DeriveKeyTest.cs
 * \example HighLevelAPI/_26_LegacyParallelFunctionsTest.cs
 * \example HighLevelAPI/_27_Pkcs11UriUtilsTest.cs
 * \example HighLevelAPI/Helpers.cs
 */