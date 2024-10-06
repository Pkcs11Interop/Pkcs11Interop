# Basic PKCS#11 related terms

**PKCS#11** is a cryptography standard that specifies an ANSI C API for devices that hold cryptographic keys and perform cryptographic functions. It isolates an application from the low-level details of the cryptographic device so that the application does not need to change to use a different type of device. The first version of the PKCS#11 specification was published by [RSA Laboratories](https://www.rsa.com/) back in 1995. RSA published subsequent versions of the specification up until v2.20. In 2013, RSA [contributed](https://lists.oasis-open.org/archives/pkcs11/201303/msg00001.html) a draft of the PKCS#11 v2.30 specification to the [OASIS PKCS 11 Technical Committee](https://www.oasis-open.org/committees/pkcs11/), which then became the official maintainer of the specification. The Pkcs11Interop project maintains the [PKCS11-SPECS](https://github.com/Pkcs11Interop/PKCS11-SPECS) repository with copies of all PKCS#11 specification versions.

The following image depicts a few basic PKCS#11 related terms you must be aware of while using the `Pkcs11Interop` library:

![Basic PKCS#11 related terms](images/basic-pkcs11-terms.jpg)

Here's a brief explanation of numbered items:
1. **Application**  
   Represents any type of application (console app, winforms app, web app, etc.) that needs to perform certain cryptographic operations with keys stored on cryptographic devices (HSM, smartcard, etc.). In order to do that, the application dynamically loads the PKCS#11 library provided by the device vendor.
2. **PKCS#11 library**  
   The PKCS#11 library (`.dll` file on Windows, `.dylib` file on macOS, `.so` file on Linux etc.) is usually provided by the cryptographic device vendor and acts as a driver that makes slots, tokens, and keys stored on tokens accessible to the application.
3. **Slot without the token**  
   Slot represents device interface (e.g. smartcard reader) in which a cryptographic token (e.g. smartcard) may not be present.
4. **Slot with the token present**  
   Slot represents device interface (e.g. smartcard reader) in which a cryptographic token (e.g. smartcard) may be present.
5. **Token**  
   Token represents the device that stores cryptographic keys and can use them to perform cryptographic operations (signing/verification, encryption/decryption, etc. ) without revealing their value to the outside world. Tokens may be implemented entirely in software with no special hardware needed (e.g. [BouncyHSM](https://github.com/harrison314/BouncyHsm)).

Of course, since PKCS#11 provides only a logical view of slots and tokens, there may be other physical interpretations. It is possible that multiple slots may share the same physical reader. The point is that a system has some number of slots, and applications can connect to tokens in any or all of those slots.

Additionally, there is one more important PKCS#11 term that is not depicted in the image: **Mechanism**. Mechanisms represent cryptographic algorithms. Every token may support a different set of mechanisms (e.g. one token supports RSA encryption and another does not) or may support mechanisms with different limitations (e.g. one token supports RSA keys up to 2048 bits and another up to 4096 bits).

[Next page >](02_ARCHITECTURE.md)