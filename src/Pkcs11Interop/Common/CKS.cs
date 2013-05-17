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
