using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("Pkcs11Interop")]
[assembly: AssemblyDescription("Open-source .NET wrapper for unmanaged PKCS#11 libraries")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("JWC s.r.o.")]
[assembly: AssemblyProduct("Pkcs11Interop")]
[assembly: AssemblyCopyright("Copyright (c) 2012-2013 JWC s.r.o. All Rights Reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.2")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

/*! \mainpage Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *
 * <a class="el" href="http://www.rsa.com/rsalabs/node.asp?id=2133">PKCS#11</a> is cryptography standard published by RSA Laboratories that defines ANSI C API (called cryptoki) to access smart cards and other types of cryptographic hardware.
 * 
 * <a class="el" href="http://www.pkcs11interop.net">Pkcs11interop</a> is open-source project written in C# that brings full power of PKCS#11 API to .NET environment. It uses System.Runtime.InteropServices to define platform invoke methods for accessing unmanaged cryptoki API and specifies how data is marshaled between managed and unmanaged memory.
 *
 * You should first get familiar with basic concepts of <a class="el" href="http://www.rsa.com/rsalabs/node.asp?id=2133">PKCS#11 v2.20</a> and then start exploring Pkcs11Interop API which resides in Net.Pkcs11Interop namespace and is divided into two logical parts - Net.Pkcs11Interop.LowLevelAPI and Net.Pkcs11Interop.HighLevelAPI.
 *
 * \section lowlevelapi_sec LowLevelAPI
 *
 * - Provides exact PKCS#11 interface as defined by RSA Laboratories
 * - Gives developer a full power of PKCS#11 ANSI C API
 * - Requires C style coding with unmanaged memory management (it is actually quite easy with Net.Pkcs11Interop.LowLevelAPI.UnmanagedMemory class)
 *
 * Start your exploration of LowLevelAPI by looking at Net.Pkcs11Interop.LowLevelAPI.Pkcs11 class and <a class="el" href="examples.html">examples</a>.
 *
 * \section highlevelapi_sec HighLevelAPI
 * 
 * - It is built on top of LowLevelAPI
 * - Utilizes developer friendly constructs and supports streams
 * - No unmanaged memory handling is required because garbage collector takes care of everything
 * 
 * Start your exploration of HighLevelAPI by looking at Net.Pkcs11Interop.HighLevelAPI.Pkcs11 class and <a class="el" href="examples.html">examples</a>.
 * 
 * Please visit project website - <a class="el" href="http://www.pkcs11interop.net">pkcs11interop.net</a> - for more information regarding updates, licensing, support etc.
 */