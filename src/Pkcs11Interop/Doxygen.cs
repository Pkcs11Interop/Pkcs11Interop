/*! \mainpage Managed .NET wrapper for unmanaged PKCS#11 libraries
 * 
 * \section overview Overview
 * 
 * <a href="https://pkcs11interop.net">Pkcs11interop</a> forms a bridge between the unmanaged ANSI C and managed .NET worlds. It loads unmanaged PKCS#11 library provided by the cryptographic device vendor and makes its functions accessible to .NET application.
 * 
 * Pkcs11interop uses `System.Runtime.InteropServices` to define platform invoke methods for unmanaged PKCS#11 API and specifies how data is marshaled between managed and unmanaged memory.
 * 
 * Following figure presents the typical usage of Pkcs11Interop library in .NET application (left side) and internal architecture of Pkcs11Interop library (right side):
 * 
 * \image html pkcs11interop-architecture.png 
 * 
 * \section lowlevelapi-s-and-highlevelapi-s LowLevelAPI-s and HighLevelAPI-s
 * 
 * Pkcs11Interop API is logically divided into the set of `LowLevelAPI`-s and the set of `HighLevelAPI`-s.
 * 
 * In order to bring the full power of PKCS#11 API to the .NET environment `LowLevelAPI`-s follow ANSI C API defined by PKCS#11 specification as closely as possible and because of that require C-like coding style with a manual memory management.
 * 
 * On the other hand `HighLevelAPI`-s, which are built on top of `LowLevelAPI`-s, use garbage collector for automatic memory management and utilize developer friendly constructs such as collections or streams.
 * 
 * \section fours-and-eights-in-the-api-s Fours and eights in the API-s
 * 
 * The C `ulong` type is extensively used throughout the PKCS#11 ANSI C API and unfortunately it is one of the most difficult types to marshal since there is no equivalent type in .NET that universally matches its size.
 * 
 * The problem is that the C `ulong` type can be 4 bytes long on some platforms (Win32, Win64 and Unix32) and in the same time it can be 8 bytes long on the other platforms (Unix64). In .NET there is `uint` type which is 4 bytes long regardless of platform and there is `ulong` type which is 8 bytes long regardless of platform.
 * 
 * Neither of .NET types can be used as a multiplatform alternative for C `ulong` type and the only option is to use and marshal two different sets of functions and structures:
 * 
 * 1. set with `uint` .NET type for platforms where C `ulong` type is **4** bytes long for LowLevelAPI<b>4</b>0, LowLevelAPI<b>4</b>1, HighLevelAPI<b>4</b>0 and HighLevelAPI<b>4</b>1
 * 2. set with `ulong` .NET type for platforms where C `ulong` type is **8** bytes long for LowLevelAPI<b>8</b>0, LowLevelAPI<b>8</b>1, HighLevelAPI<b>8</b>0 and HighLevelAPI<b>8</b>1
 *    
 * \section zeros-and-ones-in-the-api-s Zeros and ones in the API-s
 * 
 * <a href="https://github.com/Pkcs11Interop/PKCS11-SPECS">PKCS#11 specifications</a> v2.01 - v2.30 all vaguely state:
 * 
 * <blockquote>Cryptoki structures are packed to occupy as little space as is possible. In particular, on the Win32 and Win16 platforms, Cryptoki structures should be packed with 1-byte alignment. In a UNIX environment, it may or may not be necessary (or even possible) to alter the byte-alignment of structures.</blockquote>
 * 
 * One could say that packing with 1-byte alignment should be preferred on all platforms but most of PKCS#11 libraries available for Unix platforms use the default byte alignment instead. Structure packing in .NET is controlled by the `Pack` field of `System.Runtime.InteropServices.StructLayoutAttribute` which cannot be modified in the runtime so the only option is to use and marshal two different sets of structures:
 * 
 * 1. set with `Pack` field set to **1** to indicate 1-byte alignment for LowLevelAPI4<b>1</b>, LowLevelAPI8<b>1</b>, HighLevelAPI4<b>1</b> and HighLevelAPI8<b>1</b>
 * 2. set with `Pack` field set to **0** to indicate the default byte alignment for LowLevelAPI4<b>0</b>, LowLevelAPI8<b>0</b>, HighLevelAPI4<b>0</b> and HighLevelAPI8<b>0</b>
 *
 * \section recommended-api-s Recommended API-s
 * 
 * Up to version 4, Pkcs11Interop used individual classes located in `Net.Pkcs11Interop.HighLevelAPI` namespace that automatically used the correct set of platform dependent API-s.
 * 
 * Since version 5, Pkcs11Interop uses <a href="https://en.wikipedia.org/wiki/Factory_method_pattern">factory method pattern</a> implemented by `Net.Pkcs11Interop.HighLevelAPI.Pkcs11InteropFactories` class that automatically instantiates the correct set of platform dependent API-s.
 */


/*! 
 * \namespace Net.Pkcs11Interop
 * \brief Base namespace of Pkcs11Interop project
 * 
 * 
 * \namespace Net.Pkcs11Interop.Common
 * \brief Components shared by all the LowLevelAPI-s and HighLevelAPI-s
 * 
 * 
 * \namespace Net.Pkcs11Interop.Logging
 * \brief Logger components shared by all the LowLevelAPI-s and HighLevelAPI-s
 * 
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI
 * \brief High level .NET friendly API recommended for multiplatform development
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI.Factories
 * \brief Interfaces for factories implemented by individual HighLevelAPI-s
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
 * \brief Interfaces for mechanism parameters implemented by individual HighLevelAPI-s
 * 
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI40
 * \brief Low level C-like API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32) and structs are packed with the default byte alignment
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI40.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI40
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI40
 * \brief High level .NET friendly API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32) and structs are packed with the default byte alignment
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI40.Factories
 * \brief Factory classes that create objects usable by HighLevelAPI40
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI40.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI40
 * 
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI41
 * \brief Low level C-like API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32) and structs are packed with 1-byte alignment
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI41.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI41
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI41
 * \brief High level .NET friendly API for platforms where C 'long' type is 4 bytes long (Win32, Win64 and Unix32) and structs are packed with 1-byte alignment
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI41.Factories
 * \brief Factory classes that create objects usable by HighLevelAPI41
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI41.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI41
 * 
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI80
 * \brief Low level C-like API for platforms where C 'long' type is 8 bytes long (Unix64) and structs are packed with the default byte alignment
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI80.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI80
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI80
 * \brief High level .NET friendly API for platforms where C 'long' type is 8 bytes long (Unix64) and structs are packed with the default byte alignment
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI80.Factories
 * \brief Factory classes that create objects usable by HighLevelAPI80
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI80
 * 
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI81
 * \brief Low level C-like API for platforms where C 'long' type is 8 bytes long (Unix64) and structs are packed with 1-byte alignment
 * 
 * \namespace Net.Pkcs11Interop.LowLevelAPI81.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by LowLevelAPI81
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI81
 * \brief High level .NET friendly API for platforms where C 'long' type is 8 bytes long (Unix64) and structs are packed with 1-byte alignment
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI81.Factories
 * \brief Factory classes that create objects usable by HighLevelAPI81
 * 
 * \namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
 * \brief Classes that can hold parameters for various mechanisms usable by HighLevelAPI81
 */


/*!
 * \example Settings.cs
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
 * \example HighLevelAPI/_28_VendorExtensionsTest.cs
 * \example HighLevelAPI/Helpers.cs
 */