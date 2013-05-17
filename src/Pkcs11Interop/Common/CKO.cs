/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
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
