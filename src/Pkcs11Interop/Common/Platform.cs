/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using System.IO;
#if NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class for runtime platform detection
    /// </summary>
    public static class Platform
    {
        /// <summary>
        /// True if 64-bit runtime is used
        /// </summary>
        public static bool Uses64BitRuntime
        {
            get
            {
                return (IntPtr.Size == 8);
            }
        }

        /// <summary>
        /// True if 32-bit runtime is used
        /// </summary>
        public static bool Uses32BitRuntime
        {
            get
            {
                return (IntPtr.Size == 4);
            }
        }

        /// <summary>
        /// True if runtime platform is Windows
        /// </summary>
        private static bool _isWindows = false;

        /// <summary>
        /// True if runtime platform is Windows
        /// </summary>
        public static bool IsWindows
        {
            get
            {
                DetectPlatform();
                return _isWindows;
            }
        }

        /// <summary>
        /// True if runtime platform is Linux
        /// </summary>
        private static bool _isLinux = false;

        /// <summary>
        /// True if runtime platform is Linux
        /// </summary>
        public static bool IsLinux
        {
            get
            {
                DetectPlatform();
                return _isLinux;
            }
        }

        /// <summary>
        /// True if runtime platform is Mac OS X
        /// </summary>
        private static bool _isMacOsX = false;

        /// <summary>
        /// True if runtime platform is Mac OS X
        /// </summary>
        public static bool IsMacOsX
        {
            get
            {
                DetectPlatform();
                return _isMacOsX;
            }
        }

        /// <summary>
        /// Size of native (unmanaged) long type
        /// </summary>
        private static int _nativeULongSize = 0;

        /// <summary>
        /// Size of native (unmanaged) long type.
        /// This property is used by HighLevelAPI to choose correct set of LowLevelAPIs.
        /// Value of this property can be changed if needed.
        /// </summary>
        public static int NativeULongSize
        {
            get
            {
                if (_nativeULongSize != 0)
                    return _nativeULongSize;

                if (IsLinux || IsMacOsX)
                {
                    // CK_ULONG is 4 bytes long on 32-bit Unix and 8 bytes long 64-bit Unix
                    _nativeULongSize = IntPtr.Size;
                }
                else
                {
                    // On Windows CK_ULONG is always 4 bytes long
                    _nativeULongSize = 4;
                }

                return _nativeULongSize;
            }
            set
            {
                if ((value != 4) && (value != 8))
                    throw new ArgumentException();

                // Automatic value detection can be overriden if needed
                _nativeULongSize = value;
            }
        }

        /// <summary>
        /// Controls the alignment of unmanaged struct fields
        /// </summary>
        private static int _structPackingSize = -1;

        /// <summary>
        /// Controls the alignment of unmanaged struct fields.
        /// This property is used by HighLevelAPI to choose correct set of LowLevelAPIs.
        /// Value of this property can be changed if needed.
        /// </summary>
        public static int StructPackingSize
        {
            get
            {
                if (_structPackingSize != -1)
                    return _structPackingSize;

                if (IsLinux || IsMacOsX)
                {
                    // On UNIX platforms CRYPTOKI structs are usually packed with the default byte alignment
                    _structPackingSize = 0;
                }
                else
                {
                    // On Windows platforms CRYPTOKI structs are usually packed with 1-byte alignment
                    _structPackingSize = 1;
                }

                return _structPackingSize;
            }
            set
            {
                if ((value != 0) && (value != 1))
                    throw new ArgumentException();

                // Automatic value detection can be overriden if needed
                _structPackingSize = value;
            }
        }

        /// <summary>
        /// Performs platform detection
        /// </summary>
        private static void DetectPlatform()
        {
            // Supported platform has already been detected
            if (_isWindows || _isLinux || _isMacOsX)
                return;

#if NETSTANDARD2_0

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _isWindows = true;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Note: Android gets here too
                _isLinux = true;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // Note: iOS gets here too
                _isMacOsX = true;
            }
            else
            {
                throw new UnsupportedPlatformException("Pkcs11Interop is not supported on this platform");
            }

#else

            // Detect platform
            //
            // System.Environment.OSVersion.Platform is not used because:
            // - Mac OS X detection almost never works under Mono
            // - it is not implemented by .NET Core
            //
            // System.Runtime.InteropServices.RuntimeInformation is not used because:
            // - it is not implemented by full .NET and Mono
            // - it does not perform platform detection in runtime but uses hardcoded information instead
            //   See https://github.com/dotnet/corefx/issues/3032 for more info
            //
            // Pinvoking of platform specific unmanaged functions is not used because:
            // - it may cause segmentation fault on unknown platforms
            //
            // Following code may look silly but:
            // - it is 100% managed code
            // - it works under .NET, Mono and .NET Core
            // - it works like a charm so far

            string windir = Environment.GetEnvironmentVariable("windir");
            if (!string.IsNullOrEmpty(windir) && windir.Contains(@"\") && Directory.Exists(windir))
            {
                _isWindows = true;
            }
            else if (File.Exists(@"/proc/sys/kernel/ostype"))
            {
                string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
                if (osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
                {
                    // Note: Android gets here too
                    _isLinux = true;
                }
                else
                {
                    throw new UnsupportedPlatformException(string.Format("Pkcs11Interop is not supported on \"{0}\" platform", osType));
                }
            }
            else if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                // Note: iOS gets here too
                _isMacOsX = true;
            }
            else
            {
                throw new UnsupportedPlatformException("Pkcs11Interop is not supported on this platform");
            }

#endif
        }
    }
}
