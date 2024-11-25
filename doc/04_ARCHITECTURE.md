# Pkcs11Interop Library Architecture

## Overview

Pkcs11Interop forms a bridge between the unmanaged ANSI C and managed .NET worlds. It loads the unmanaged PKCS#11 library provided by the cryptographic device vendor and makes its functions accessible to .NET applications.

Pkcs11Interop uses `System.Runtime.InteropServices` to define platform invoke methods for the unmanaged PKCS#11 API and specifies how data is marshaled between managed and unmanaged memory.

The following figure presents the typical usage of the Pkcs11Interop library in a .NET application (left side) and the internal architecture of the Pkcs11Interop library (right side):

![Pkcs11Interop architecture](images/pkcs11interop-architecture.png)

*Note: Click on the picture for a larger image.*

## LowLevelAPI-s and HighLevelAPI-s

The Pkcs11Interop API is logically divided into a set of `LowLevelAPI`-s and a set of `HighLevelAPI`-s.

To bring the full power of the PKCS#11 API to the .NET environment, the `LowLevelAPI`-s closely follow the ANSI C API defined by the PKCS#11 specification. This requires a C-like coding style with manual memory management.

On the other hand, the `HighLevelAPI`-s, which are built on top of the `LowLevelAPI`-s, use the garbage collector for automatic memory management and utilize developer-friendly constructs such as collections and streams.

## Fours and Eights in the API-s

The C `ulong` type is extensively used throughout the PKCS#11 ANSI C API, and it is one of the most difficult types to marshal since there is no equivalent type in .NET that universally matches its size.

The problem is that the C `ulong` type can be 4 bytes long on some platforms (Win32, Win64, and Unix32) and 8 bytes long on other platforms (Unix64). In .NET, the `uint` type is 4 bytes long regardless of platform, and the `ulong` type is 8 bytes long regardless of platform.

Neither of these .NET types can be used as a multiplatform alternative for the C `ulong` type. The only option is to use and marshal two different sets of functions and structures:

1. A set with the `uint` .NET type for platforms where the C `ulong` type is **4** bytes long  
   for LowLevelAPI**4**0, LowLevelAPI**4**1, HighLevelAPI**4**0, and HighLevelAPI**4**1.
2. A set with the `ulong` .NET type for platforms where the C `ulong` type is **8** bytes long  
   for LowLevelAPI**8**0, LowLevelAPI**8**1, HighLevelAPI**8**0, and HighLevelAPI**8**1.

## Zeros and Ones in the API-s

PKCS#11 specifications v2.01 - v2.30 all vaguely state:

> Cryptoki structures are packed to occupy as little space as is possible. In particular, on the Win32 and Win16 platforms, Cryptoki structures should be packed with 1-byte alignment. In a UNIX environment, it may or may not be necessary (or even possible) to alter the byte-alignment of structures.

While one might assume that packing with 1-byte alignment should be preferred on all platforms, most PKCS#11 libraries available for Unix platforms use the default byte alignment instead. Structure packing in .NET is controlled by the `Pack` field of `System.Runtime.InteropServices.StructLayoutAttribute`, which cannot be modified at runtime. Therefore, the only option is to use and marshal two different sets of structures:

1. A set with the `Pack` field set to **1** to indicate 1-byte alignment  
   for LowLevelAPI4**1**, LowLevelAPI8**1**, HighLevelAPI4**1**, and HighLevelAPI8**1**.
2. A set with the `Pack` field set to **0** to indicate the default byte alignment  
   for LowLevelAPI4**0**, LowLevelAPI8**0**, HighLevelAPI4**0**, and HighLevelAPI8**0**.

## Recommended API-s

Up to version 4, Pkcs11Interop used individual classes located in the `Net.Pkcs11Interop.HighLevelAPI` namespace that automatically used the correct set of platform-dependent API-s.

Since version 5, Pkcs11Interop uses the [factory pattern](https://en.wikipedia.org/wiki/Abstract_factory_pattern) implemented by the `Net.Pkcs11Interop.HighLevelAPI.Pkcs11InteropFactories` class that automatically instantiates the correct set of platform-dependent API-s.

[Next page >](05_INTERFACES.md)