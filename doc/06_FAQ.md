# Frequently Asked Questions

## When should I use Pkcs11Interop?

Pkcs11Interop may not be the ideal choice if you are developing an application intended **only for Windows** and your cryptographic device has proper integration with the Windows OS via [CAPI/CNG](https://en.wikipedia.org/wiki/Microsoft_CryptoAPI) (CSP, KSP, minidriver, etc.).

Pkcs11Interop is a suitable choice if you are developing an application intended for **platforms beyond just Windows** and your application will perform cryptographic operations with keys stored in a hardware device such as a smartcard or HSM.

Moreover, if your application will perform just signing or encryption with RSA or EC keys associated with an X.509 certificate, you might find the [Pkcs11Interop.X509Store](https://github.com/Pkcs11Interop/Pkcs11Interop.X509Store) library to be a simpler and more developer-friendly alternative to the full Pkcs11Interop.

## How can I integrate Pkcs11Interop with other built-in .NET classes?

Use the simpler [Pkcs11Interop.X509Store](https://github.com/Pkcs11Interop/Pkcs11Interop.X509Store) library instead of the full Pkcs11Interop. It provides `Pkcs11RsaProvider`, which is a PKCS#11-based implementation of the `System.Security.Cryptography.RSA` algorithm, and `Pkcs11ECDsaProvider`, which is a PKCS#11-based implementation of the `System.Security.Cryptography.ECDsa` algorithm. These providers can be easily integrated with other .NET classes such as `System.Security.Cryptography.Pkcs.SignedCms` or `System.Security.Cryptography.Xml.SignedXml`.

## What algorithms does Pkcs11Interop support?

Pkcs11Interop is a wrapper library and, as such, it supports all the algorithms defined in the [PKCS#11 v2.40](https://github.com/Pkcs11Interop/PKCS11-SPECS/tree/master/v2.40) specification. However, whether a specific algorithm is supported and can be used with your cryptographic device depends solely on your particular device and the PKCS#11 library provided by its vendor. You can use the [Pkcs11Admin](https://www.pkcs11admin.net/) application to display the list of mechanisms/algorithms supported by your PKCS#11 library/device, or you can call the `ISlot::GetMechanismList` method followed by the `ISlot::GetMechanismInfo` method to get this information directly in your application.

## How can I use Pkcs11Interop in a multi-threaded application?

To safely use Pkcs11Interop in a multi-threaded application, follow these four rules:

1. **Use a Single Library Instance**: Under normal circumstances, load the PKCS#11 library only once per process and reuse the `IPkcs11Library` instance across all threads.
2. **Set AppType to MultiThreaded**: When loading the PKCS#11 library, set the `appType` parameter to `AppType.MultiThreaded`. This instructs the PKCS#11 library to handle any required locking internally, so you don't need to manage it yourself.
3. **Maintain a Main Session**: After loading the PKCS#11 library, open a session (referred to as the main session), log in to this session, and keep it open. All PKCS#11 sessions share the same login state, so as long as the main session is open and logged in, all sessions (both existing and newly created) will be logged in.
4. **Create Separate Sessions**: Always create a new session for each cryptographic operation, regardless of the thread. Ensure to close the session once it is no longer needed.

## Can I use a certificate from Pkcs11Interop for SSL connections?

No, you cannot. SSL connections in .NET are handled by platform-specific native libraries (SChannel on Windows, OpenSSL on Linux, etc.), and these libraries cannot use private keys accessible from managed .NET code. In other words, you cannot use Pkcs11Interop for SSL/TLS connections with the built-in .NET networking stack. You would need to write your own SSL/TLS implementation and integrate it with both Pkcs11Interop and the .NET platform. This is not a trivial task and requires a significant amount of work.

## Do you accept pull requests for vendor-specific extensions?

No, we do not accept vendor-specific extensions in the base Pkcs11Interop library to maintain its vendor-neutral stance and avoid potential legal issues. However, we encourage third parties to create separate libraries/repositories for vendor-specific extensions, using Pkcs11Interop as a dependency. For example, [RutokenPkcs11Interop](https://github.com/AktivCo/RutokenPkcs11Interop) is one such implementation. For guidance, the Pkcs11Interop solution includes a `Pkcs11Interop.Mock` project that demonstrates how to implement vendor-specific extensions.