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
    /// Session States
    /// </summary>
    public enum CKS : uint
    {
        /// <summary>
        /// The application has opened a read-only session. The application has read-only access to public token objects and read/write access to public session objects.
        /// </summary>
        CKS_RO_PUBLIC_SESSION = 0,
        
        /// <summary>
        /// The normal user has been authenticated to the token. The application has read-only access to all token objects (public or private) and read/write access to all session objects (public or private).
        /// </summary>
        CKS_RO_USER_FUNCTIONS = 1,
        
        /// <summary>
        /// The application has opened a read/write session. The application has read/write access to all public objects.
        /// </summary>
        CKS_RW_PUBLIC_SESSION = 2,
        
        /// <summary>
        /// The normal user has been authenticated to the token. The application has read/write access to all objects.
        /// </summary>
        CKS_RW_USER_FUNCTIONS = 3,
        
        /// <summary>
        /// The Security Officer has been authenticated to the token. The application has read/write access only to public objects on the token, not to private objects. The SO can set the normal user's PIN.
        /// </summary>
        CKS_RW_SO_FUNCTIONS = 4
    }
}
