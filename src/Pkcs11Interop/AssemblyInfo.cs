using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Pkcs11Interop")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Pkcs11Interop")]
[assembly: AssemblyCopyright("Jaroslav Imrich")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.*")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

/*! \mainpage Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *
 * <a class="el" href="http://www.rsa.com/rsalabs/node.asp?id=2133">PKCS#11</a> is cryptography standard published by RSA Laboratories that defines ANSI C API (called cryptoki) to access smart cards and other types of cryptographic hardware.
 * 
 * <a class="el" href="http://www.pkcs11interop.net">Pkcs11interop</a> is open-source project that brings full power of PKCS#11 API to .NET environment. It uses System.Runtime.InteropServices to define platform invoke methods for accessing unmanaged cryptoki API and specifies how data is marshaled between managed and unmanaged memory.
 *
 * Pkcs11Interop API resides in Net.Pkcs11Interop namespace and is divided into two logical parts - Net.Pkcs11Interop.LowLevelAPI and Net.Pkcs11Interop.HighLevelAPI.
 *
 * \section lowlevelapi_sec LowLevelAPI
 *
 * - Provides PKCS#11 interface as defined by RSA Laboratories
 * - Gives developer a full power of PKCS#11 ANSI C API
 * - Requires C style coding with unmanaged memory management (it is quite easy with Net.Pkcs11Interop.LowLevelAPI.UnmanagedMemory class)
 *
 * Start exploring LowLevelAPI by looking at Net.Pkcs11Interop.LowLevelAPI.Pkcs11 class and <a class="el" href="examples.html">examples</a>.
 *
 * \section highlevelapi_sec HighLevelAPI
 * 
 * - Built on top of LowLevelAPI
 * - Utilizes developer friendly constructs and supports streams
 * - No unmanaged memory handling is required
 * 
 * Start exploring HighLevelAPI by looking at Net.Pkcs11Interop.HighLevelAPI.Pkcs11 class and <a class="el" href="examples.html">examples</a>.
 */