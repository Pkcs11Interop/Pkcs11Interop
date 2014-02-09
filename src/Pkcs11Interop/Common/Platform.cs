/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2014 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
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
        /// Performs platform detection
        /// </summary>
        private static void DetectPlatform()
        {
            // Supported platform has already been detected
            if (_isWindows || _isLinux || _isMacOsX)
                return;

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
                    if (osType.StartsWith("Linux", StringComparison.InvariantCultureIgnoreCase))
                        _isLinux = true;
                    else
                        throw new Exception("This platform (" + osType + ") is not supported");
                }
                else if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
                {
                    _isMacOsX = true;
                }
                else
                {
                    throw new Exception("This platform is not supported");
                }
            }
            else
            {
                _isWindows = true;
            }
        }
    }
}
