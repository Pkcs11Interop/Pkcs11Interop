# Getting Started with Pkcs11Interop

Follow the instructions provided by the vendor of your cryptographic device to install and configure the device along with all the required support software. Consult the device documentation to determine the exact location of the unmanaged PKCS#11 library provided by the vendor.

Create a new C# console application project in Visual Studio and install the [Pkcs11Interop NuGet package](https://www.nuget.org/packages/Pkcs11Interop/) using the [NuGet Package Manager UI](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui#finding-and-installing-a-package) or any other tool of your choice. Replace the contents of the `Program.cs` file in your project with the following code, which displays basic information about your unmanaged PKCS#11 library and all accessible slots.

**WARNING: Don't forget to replace the value of the `pkcs11LibraryPath` variable.**

```csharp
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up a custom DllImportResolver that may be needed on some Linux distributions
            SetupCustomDllImportResolver();

            // Specify the path to unmanaged PKCS#11 library provided by the cryptographic device vendor
            string pkcs11LibraryPath = @"c:\SoftHSM2\lib\softhsm2-x64.dll";

            // Create factories used by Pkcs11Interop library
            Pkcs11InteropFactories factories = new Pkcs11InteropFactories();

            // Load unmanaged PKCS#11 library
            using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, pkcs11LibraryPath, AppType.MultiThreaded))
            {
                // Show general information about loaded library
                ILibraryInfo libraryInfo = pkcs11Library.GetInfo();

                Console.WriteLine("Library");
                Console.WriteLine("  Manufacturer:       " + libraryInfo.ManufacturerId);
                Console.WriteLine("  Description:        " + libraryInfo.LibraryDescription);
                Console.WriteLine("  Version:            " + libraryInfo.LibraryVersion);

                // Get list of all available slots
                foreach (ISlot slot in pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent))
                {
                    // Show basic information about slot
                    ISlotInfo slotInfo = slot.GetSlotInfo();

                    Console.WriteLine();
                    Console.WriteLine("Slot");
                    Console.WriteLine("  Manufacturer:       " + slotInfo.ManufacturerId);
                    Console.WriteLine("  Description:        " + slotInfo.SlotDescription);
                    Console.WriteLine("  Token present:      " + slotInfo.SlotFlags.TokenPresent);

                    if (slotInfo.SlotFlags.TokenPresent)
                    {
                        // Show basic information about token present in the slot
                        ITokenInfo tokenInfo = slot.GetTokenInfo();

                        Console.WriteLine("Token");
                        Console.WriteLine("  Manufacturer:       " + tokenInfo.ManufacturerId);
                        Console.WriteLine("  Model:              " + tokenInfo.Model);
                        Console.WriteLine("  Serial number:      " + tokenInfo.SerialNumber);
                        Console.WriteLine("  Label:              " + tokenInfo.Label);

                        // Show list of mechanisms (algorithms) supported by the token
                        Console.WriteLine("Supported mechanisms: ");
                        foreach (CKM mechanism in slot.GetMechanismList())
                            Console.WriteLine("  " + mechanism);
                    }
                }
            }
        }

        /// <summary>
        /// Sets up a custom DllImportResolver that may be needed when Pkcs11Interop is running on Linux with .NET 5 or later
        /// </summary>
        static void SetupCustomDllImportResolver()
        {
#if NET5_0_OR_GREATER
            if (Platform.IsLinux)
            {
                // Pkcs11Interop uses native functions from "libdl.so", but Ubuntu 22.04 and possibly also other distros have "libdl.so.2".
                // Therefore, we need to set up a DllImportResolver to remap "libdl" to "libdl.so.2".
                NativeLibrary.SetDllImportResolver(typeof(Pkcs11InteropFactories).Assembly, (libraryName, assembly, dllImportSearchPath) =>
                {
                    if (libraryName == "libdl")
                    {
                        // Note: This mapping might need to be modified if your distribution uses a different version of libdl.
                        return NativeLibrary.Load("libdl.so.2", assembly, dllImportSearchPath);
                    }
                    else
                    {
                        return NativeLibrary.Load(libraryName, assembly, dllImportSearchPath);
                    }
                });
            }
#endif
        }
    }
}
```

When you execute your application, you should see output similar to this:

```
Library
  Manufacturer:       SoftHSM
  Description:        Implementation of PKCS11
  Version:            2.5

Slot
  Manufacturer:       SoftHSM project
  Description:        SoftHSM slot ID 0x50261aaf
  Token present:      True
Token
  Manufacturer:       SoftHSM project
  Model:              SoftHSM v2
  Serial number:      7ae072b9d0261aaf
  Label:              My token 1
Supported mechanisms:
  CKM_MD5
  CKM_SHA_1
  CKM_SHA224
  CKM_SHA256
  CKM_SHA384
  CKM_SHA512
  CKM_MD5_HMAC
  CKM_SHA_1_HMAC
  CKM_SHA224_HMAC
  CKM_SHA256_HMAC
  CKM_SHA384_HMAC
  CKM_SHA512_HMAC
  CKM_RSA_PKCS_KEY_PAIR_GEN
  CKM_RSA_PKCS
  CKM_RSA_X_509
  CKM_MD5_RSA_PKCS
  CKM_SHA1_RSA_PKCS
  CKM_RSA_PKCS_OAEP
  CKM_SHA224_RSA_PKCS
  CKM_SHA256_RSA_PKCS
  CKM_SHA384_RSA_PKCS
  CKM_SHA512_RSA_PKCS
  CKM_RSA_PKCS_PSS
  CKM_SHA1_RSA_PKCS_PSS
  CKM_SHA224_RSA_PKCS_PSS
  CKM_SHA256_RSA_PKCS_PSS
  CKM_SHA384_RSA_PKCS_PSS
  CKM_SHA512_RSA_PKCS_PSS
  CKM_GENERIC_SECRET_KEY_GEN
  CKM_DES_KEY_GEN
  CKM_DES2_KEY_GEN
  CKM_DES3_KEY_GEN
  CKM_DES_ECB
  CKM_DES_CBC
  CKM_DES_CBC_PAD
  CKM_DES_ECB_ENCRYPT_DATA
  CKM_DES_CBC_ENCRYPT_DATA
  CKM_DES3_ECB
  CKM_DES3_CBC
  CKM_DES3_CBC_PAD
  CKM_DES3_ECB_ENCRYPT_DATA
  CKM_DES3_CBC_ENCRYPT_DATA
  CKM_DES3_CMAC
  CKM_AES_KEY_GEN
  CKM_AES_ECB
  CKM_AES_CBC
  CKM_AES_CBC_PAD
  CKM_AES_CTR
  CKM_AES_GCM
  CKM_AES_KEY_WRAP
  CKM_AES_KEY_WRAP_PAD
  CKM_AES_ECB_ENCRYPT_DATA
  CKM_AES_CBC_ENCRYPT_DATA
  CKM_AES_CMAC
  CKM_DSA_PARAMETER_GEN
  CKM_DSA_KEY_PAIR_GEN
  CKM_DSA
  CKM_DSA_SHA1
  CKM_DSA_SHA224
  CKM_DSA_SHA256
  CKM_DSA_SHA384
  CKM_DSA_SHA512
  CKM_DH_PKCS_KEY_PAIR_GEN
  CKM_DH_PKCS_PARAMETER_GEN
  CKM_DH_PKCS_DERIVE
  CKM_ECDSA_KEY_PAIR_GEN
  CKM_ECDSA
  CKM_ECDH1_DERIVE

...
```

That's it! You have successfully used the unmanaged PKCS#11 library in your .NET application.

[Next page >](07_CODE_SAMPLES.md)