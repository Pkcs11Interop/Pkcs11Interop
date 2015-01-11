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
