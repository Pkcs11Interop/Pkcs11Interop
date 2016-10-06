Pkcs11Interop
=============
**Managed .NET wrapper for unmanaged PKCS#11 libraries**

[![Build status](https://ci.appveyor.com/api/projects/status/lb1jxb4t4203g3t9?svg=true)](https://ci.appveyor.com/project/pkcs11interop/pkcs11interop)

## Table of Contents

* [Overview](#overview)
* [Documentation](#documentation)
* [Download](#download)
* [License](#license)
* [Support](#support)
* [Related projects](#related-projects)
* [About](#about)

## Overview

PKCS#11 is cryptography standard maintained by the OASIS PKCS 11 Technical Committee (originally published by RSA Laboratories) that defines ANSI C API to access smart cards and other types of cryptographic hardware.

Pkcs11Interop is managed library written in C# that brings full power of PKCS#11 API to the .NET environment. It loads unmanaged PKCS#11 library provided by the cryptographic device vendor and makes its functions accessible to .NET application.

Following figure presents the typical usage of Pkcs11Interop library in .NET application:

![Pkcs11Interop architecture](doc/images/pkcs11interop-architecture-small.png?raw=true)

Pkcs11Interop library:

* implements .NET wrapper for unmanaged PKCS#11 libraries
* is compliant with [PKCS#11 v2.20](doc/rsa-pkcs11-2.20/) specification and PKCS#11 URI scheme defined in [RFC 7512](doc/pkcs11-uri-scheme)
* is compatible with .NET Framework 2.0 and higher, .NET Core, Mono, Xamarin and Silverlight5
* is supported on Windows, Linux, Mac OS X, Android and iOS
* is supported on both 32-bit and 64-bit platforms
* is open source and completely free for commercial use
* is used in production by several information security and financial organizations
* uses 100% managed and fully documented code
* contains code samples covering all methods of PKCS#11 API

Pkcs11Interop has been confirmed to be working with the following devices:

* Atos CardOS (former Siemens CardOS) smartcard
* Thales nShield Solo (former nCipher nShield) HSM
* SoftHSM (virtual HSM from OpenDNSSEC project)
* Feitian ePass 2003 token
* SafeNet ProtectServer HSM
* SafeNet Luna SA HSM
* Utimaco CryptoServer HSM
* Belgian and Slovak eID cards
* SmartCard-HSM

## Documentation

It is highly recommended that before you start using Pkcs11Interop you should get familiar at least with *"Chapter 2 - Scope"*, *"Chapter 6 - General overview"* and *"Chapter 10 - Objects"* of [PKCS#11 v2.20](doc/rsa-pkcs11-2.20/) specification.

Pkcs11Interop API is fully documented with the inline XML documentation that is displayed by the most of the modern IDEs during the application development. Detailed [Pkcs11Interop API documentation](http://pkcs11interop.net/doc/) is also available online.

Following topics are covered by standalone documents:
* [Pkcs11Interop library architecture](doc/ARCHITECTURE.md)
* [Getting started with Pkcs11Interop](doc/GETTING_STARTED.md)
* [Pkcs11Interop code samples](doc/CODE_SAMPLES.md)
* [Troubleshooting Pkcs11Interop with PKCS11-LOGGER](doc/TROUBLESHOOTING.md)

## Download

Archives with the source code and binaries can be downloaded from [our releases page](https://github.com/Pkcs11Interop/Pkcs11Interop/releases/). [Official NuGet packages](https://www.nuget.org/packages/Pkcs11Interop/) are published in nuget.org repository. All official items are signed with [GnuPG key or code-signing certificate of Jaroslav Imrich](https://www.jimrich.sk/crypto/).

## License

Pkcs11Interop is available under the terms of the [Apache License, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0).  
[Human friendly license summary](https://www.tldrlegal.com/l/apache2) is available at tldrlegal.com but the [full license text](LICENSE.md) always prevails.

## Support

Pick one of the options that best suits your needs:

* Public [mailing list](https://groups.google.com/d/forum/pkcs11interop) available at [pkcs11interop@googlegroups.com](mailto:pkcs11interop@googlegroups.com)
* Public [issue tracker](https://github.com/Pkcs11Interop/Pkcs11Interop/issues) available at GitHub.com
* Questions with [pkcs11 tag](http://stackoverflow.com/questions/tagged/pkcs11) posted at StackOverflow.com
* Commercial support and consulting from the original developer available at [info@pkcs11interop.net](mailto:info@pkcs11interop.net)

## Related projects

* [Pkcs11Admin](http://www.pkcs11admin.net/)  
  GUI tool for administration of PKCS#11 enabled devices based on Pkcs11Interop library.
* [Pkcs11Interop.PDF](http://pkcs11interop.net/extensions/pdf/)  
  Integration layer for Pkcs11Interop and iText (iTextSharp) libraries.
* [PKCS11-LOGGER](https://github.com/Pkcs11Interop/pkcs11-logger)  
  PKCS#11 logging proxy module useful for debugging of PKCS#11 enabled applications.
* [SoftHSM2-for-Windows](https://github.com/disig/SoftHSM2-for-Windows)  
  Pure software implementation of a cryptographic store accessible through a PKCS#11 interface.

## About

Pkcs11Interop has been written by [Jaroslav Imrich](http://www.jimrich.sk).  
Please visit project website - [pkcs11interop.net](http://www.pkcs11interop.net) - for more information.
