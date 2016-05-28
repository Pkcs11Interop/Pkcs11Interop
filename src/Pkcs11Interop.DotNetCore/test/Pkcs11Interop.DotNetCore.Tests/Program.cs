using Net.Pkcs11Interop.Common;
using NUnit.Common;
using NUnitLite;
using System;
using System.Reflection;

namespace Pkcs11Interop.DotNetCore.Tests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Net.Pkcs11Interop.Common.Platform:");
            Console.WriteLine(" - IsWindows:         " + Platform.IsWindows);
            Console.WriteLine(" - IsLinux:           " + Platform.IsLinux);
            Console.WriteLine(" - IsMacOsX:          " + Platform.IsMacOsX);
            Console.WriteLine(" - UnmanagedLongSize: " + Platform.UnmanagedLongSize);
            Console.WriteLine(" - StructPackingSize: " + Platform.StructPackingSize);
            Console.WriteLine(" - Uses32BitRuntime:  " + Platform.Uses32BitRuntime);
            Console.WriteLine(" - Uses64BitRuntime:  " + Platform.Uses64BitRuntime);

            Console.WriteLine();

            return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
        }
    }
}
