# Getting started with Pkcs11Interop

Follow the instructions provided by the vendor of your cryptographic device to install and configure the device along with all the required support software. Consult device documentation to determine the exact location of unmanaged PKCS#11 library provided by the device vendor.

Create new C# console application project in Visual Studio and install Pkcs11Interop with the following command issued in the [NuGet Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console):

```
PM> Install-Package Pkcs11Interop
```

Replace contents of `Program.cs` file in your project with the following code which displays basic information about your unmanaged PKCS#11 library and all slots it can access.

**WARNING: Don't forget to replace the value of `pkcs11LibraryPath` field.**

```csharp
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;

namespace ConsoleApplication1
{
    class Program
    {
        // Defines path to unmanaged PKCS#11 library provided by the cryptographic device vendor
        static string pkcs11LibraryPath = @"c:\SoftHSM2\lib\softhsm2.dll";

        static void Main(string[] args)
        {
            // Load unmanaged PKCS#11 library
            using (Pkcs11 pkcs11 = new Pkcs11(pkcs11LibraryPath, true))
            {
                // Show general information about loaded library
                LibraryInfo libraryInfo = pkcs11.GetInfo();

                Console.WriteLine("Library" );
                Console.WriteLine("  Manufacturer:       " + libraryInfo.ManufacturerId);
                Console.WriteLine("  Description:        " + libraryInfo.LibraryDescription);
                Console.WriteLine("  Version:            " + libraryInfo.LibraryVersion);

                // Get list of all available slots
                foreach (Slot slot in pkcs11.GetSlotList(false))
                {
                    // Show basic information about slot
                    SlotInfo slotInfo = slot.GetSlotInfo();

                    Console.WriteLine();
                    Console.WriteLine("Slot");
                    Console.WriteLine("  Manufacturer:       " + slotInfo.ManufacturerId);
                    Console.WriteLine("  Description:        " + slotInfo.SlotDescription);
                    Console.WriteLine("  Token present:      " + slotInfo.SlotFlags.TokenPresent);

                    if (slotInfo.SlotFlags.TokenPresent)
                    {
                        // Show basic information about token present in the slot
                        TokenInfo tokenInfo = slot.GetTokenInfo();

                        Console.WriteLine("Token");
                        Console.WriteLine("  Manufacturer:       " + tokenInfo.ManufacturerId);
                        Console.WriteLine("  Model:              " + tokenInfo.Model);
                        Console.WriteLine("  Serial number:      " + tokenInfo.SerialNumber);
                        Console.WriteLine("  Label:              " + tokenInfo.Label);

                        // Show list of mechanisms supported by the token
                        Console.WriteLine("Supported mechanisms: ");
                        foreach (CKM mechanism in slot.GetMechanismList())
                            Console.WriteLine("  " + mechanism);
                    }
                }
            }
        }
    }
}
```

When you execute your application you should get output similar to this one:

```
PKCS#11 library
  Manufacturer:       SoftHSM
  Description:        Implementation of PKCS11
  Version:            2.0

Slot
  Manufacturer:       SoftHSM project
  Description:        SoftHSM slot 0
  Token present:      True
Token
  Manufacturer:       SoftHSM project
  Model:              SoftHSM v2
  Serial number:
  Label:
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
  CKM_SHA1_RSA_PKCS_PSS
  CKM_SHA224_RSA_PKCS_PSS
  CKM_SHA256_RSA_PKCS_PSS
  CKM_SHA384_RSA_PKCS_PSS
  CKM_SHA512_RSA_PKCS_PSS
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
  CKM_AES_KEY_GEN
  CKM_AES_ECB
  CKM_AES_CBC
  CKM_AES_CBC_PAD
  8457
  CKM_AES_ECB_ENCRYPT_DATA
  CKM_AES_CBC_ENCRYPT_DATA
  CKM_DSA_PARAMETER_GEN
  CKM_DSA_KEY_PAIR_GEN
  CKM_DSA
  CKM_DSA_SHA1
  19
  20
  21
  22
  CKM_DH_PKCS_KEY_PAIR_GEN
  CKM_DH_PKCS_PARAMETER_GEN
  CKM_DH_PKCS_DERIVE
  CKM_ECDSA_KEY_PAIR_GEN
  CKM_ECDSA
  CKM_ECDH1_DERIVE
  4624
  4625
  4608
  4609
  4610
```

That's it! You have successfully used unmanaged PKCS#11 library in your .NET application.
