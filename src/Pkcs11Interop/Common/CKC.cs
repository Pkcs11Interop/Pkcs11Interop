/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Certificate types
    /// </summary>
    public enum CKC : uint
    {
        /// <summary>
        /// X.509 public key certificate
        /// </summary>
        CKC_X_509 = 0x00000000,

        /// <summary>
        /// X.509 attribute certificate
        /// </summary>
        CKC_X_509_ATTR_CERT = 0x00000001,

        /// <summary>
        /// WTLS public key certificate
        /// </summary>
        CKC_WTLS = 0x00000002,

        /// <summary>
        /// Permanently reserved for token vendors
        /// </summary>
        CKC_VENDOR_DEFINED = 0x80000000
    }
}
