/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Utility class that provides size of unmanaged long type
    /// </summary>
    public static class UnmanagedLong
    {
        /// <summary>
        /// Size of unmanaged long type
        /// </summary>
        private static int _size = 0;

        /// <summary>
        /// Size of unmanaged long type
        /// </summary>
        public static int Size
        {
            get
            {
                if (_size != 0)
                    return _size;

                if (Platform.IsLinux || Platform.IsMacOsX)
                {
                    // CK_ULONG is 4 bytes long on 32-bit Unix and 8 bytes long 64-bit Unix
                    _size = IntPtr.Size;
                }
                else
                {
                    // On Windows CK_ULONG is always 4 bytes long
                    _size = 4;
                }

                return _size;
            }
            set
            {
                if ((value != 4) && (value != 8))
                    throw new ArgumentException();

                // Automatic platform detection can be overriden if needed
                _size = value;
            }
        }
    }
}
