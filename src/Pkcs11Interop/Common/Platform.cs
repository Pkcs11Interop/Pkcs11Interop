/*
 *  Copyright 2012-2016 The Pkcs11Interop Project
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
        /// Size of unmanaged long type
        /// </summary>
        private static int _unmanagedLongSize = 0;

        /// <summary>
        /// Size of unmanaged long type
        /// </summary>
        public static int UnmanagedLongSize
        {
            get
            {
                if (_unmanagedLongSize != 0)
                    return _unmanagedLongSize;

                if (IsLinux || IsMacOsX)
                {
                    // CK_ULONG is 4 bytes long on 32-bit Unix and 8 bytes long 64-bit Unix
                    _unmanagedLongSize = IntPtr.Size;
                }
                else
                {
                    // On Windows CK_ULONG is always 4 bytes long
                    _unmanagedLongSize = 4;
                }

                return _unmanagedLongSize;
            }
            set
            {
                if ((value != 4) && (value != 8))
                    throw new ArgumentException();

                // Automatic value detection can be overriden if needed
                _unmanagedLongSize = value;
            }
        }

        /// <summary>
        /// Controls the alignment of unmanaged struct fields
        /// </summary>
        private static int _structPackingSize = -1;

        /// <summary>
        /// Controls the alignment of unmanaged struct fields
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

#if SILVERLIGHT

            int platformId = (int)System.Environment.OSVersion.Platform;

            //   4 - Unix   - Almost everything else than Windows
            //   6 - MacOSX - Correctly detected (or not) only by newer versions of Mono
            // 128 - Unix   - Used by a few ancient versions of Mono
            if (platformId == 4 || platformId == 6 || platformId == 128)
            {
                throw new UnsupportedPlatformException("Silverlight version of Pkcs11Interop is supported only on Windows platform");
            }
            else
            {
                if (!System.Windows.Application.Current.HasElevatedPermissions)
                    throw new ElevatedPermissionsMissingException("Silverlight version of Pkcs11Interop requires elevated trust");

                _isWindows = true;
            }

#endif

#if COREFX

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                _isWindows = true;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                _isLinux = true;
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                _isMacOsX = true;
            }
            else
            {
                throw new UnsupportedPlatformException("Pkcs11Interop is not supported on this platform");
            }

#else

            int platformId = (int)System.Environment.OSVersion.Platform;

            //   4 - Unix   - Almost everything else than Windows
            //   6 - MacOSX - Correctly detected (or not) only by newer versions of Mono
            // 128 - Unix   - Used by a few ancient versions of Mono
            if (platformId == 4 || platformId == 6 || platformId == 128)
            {
                // Note: I don't like the idea of pinvoking uname() function (via mono.posix or directly)
                //       or executing uname app so let's try this slightly higher level approach
                if (File.Exists(@"/proc/sys/kernel/ostype"))
                {
                    string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
                    if (osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
                        _isLinux = true;
                    else
                        throw new UnsupportedPlatformException("Pkcs11Interop is not supported on this platform - " + osType);
                }
                else if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
                {
                    _isMacOsX = true;
                }
                else
                {
                    throw new UnsupportedPlatformException("Pkcs11Interop is not supported on this platform");
                }
            }
            else
            {
                _isWindows = true;
            }

#endif
        }
    }
}
