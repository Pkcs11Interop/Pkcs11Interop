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
    /// Object class
    /// </summary>
    public enum CKO : uint
    {
        /// <summary>
        /// Data object that holds information defined by an application.
        /// </summary>
        CKO_DATA = 0x00000000,

        /// <summary>
        /// Certificate object that holds public-key or attribute certificates.
        /// </summary>
        CKO_CERTIFICATE = 0x00000001,

        /// <summary>
        /// Public key object that holds public keys.
        /// </summary>
        CKO_PUBLIC_KEY = 0x00000002,

        /// <summary>
        /// Private key object that holds private keys.
        /// </summary>
        CKO_PRIVATE_KEY = 0x00000003,

        /// <summary>
        /// Secret key object that holds secret keys.
        /// </summary>
        CKO_SECRET_KEY = 0x00000004,

        /// <summary>
        /// Hardware feature object that represent features of the device.
        /// </summary>
        CKO_HW_FEATURE = 0x00000005,

        /// <summary>
        /// Domain parameter object that holds public domain parameters.
        /// </summary>
        CKO_DOMAIN_PARAMETERS = 0x00000006,

        /// <summary>
        /// Mechanism object that provides information about mechanisms supported by a device beyond that given by the CK_MECHANISM_INFO structure.
        /// </summary>
        CKO_MECHANISM = 0x00000007,

        /// <summary>
        /// OTP key object that holds secret keys used by OTP tokens.
        /// </summary>
        CKO_OTP_KEY = 0x00000008,

        /// <summary>
        /// Reserved for token vendors.
        /// </summary>
        CKO_VENDOR_DEFINED = 0x80000000
    }
}
