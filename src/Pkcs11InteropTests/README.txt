Converting NUnit test project to Visual Studio UnitTests
********************************************************

1.  Open "Pkcs11Interop.sln" solution in Visual Studio
2.  Add new "Test project" named "TestProject1" to the solution
3.  Delete automatically created file "UnitTest1.cs" from "TestProject1"
4.  Righ click "Pkcs11InteropTests" project and add reference 
    to "Pkcs11Interop" project
5.  Drag all files (excluding AssemblyInfo.cs) from "Pkcs11InteropTests" 
    project and drop them into "TestProject1"
6.  Right click "Pkcs11InteropTests" project and choose "Unload project" 
    to unload it from solution
7.  Mass replace (CTRL + SHIFT + H) in entire solution:
    using NUnit.Framework;
    to
    using Microsoft.VisualStudio.TestTools.UnitTesting;
8.  Mass replace (CTRL + SHIFT + H) in entire solution:
    [TestFixture()]
    to
    [TestClass]
9.  Mass replace (CTRL + SHIFT + H) in entire solution:
    [Test()]
    to
    [TestMethod]
10. Rebuild solution
