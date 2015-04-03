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

using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI81
{
    /// <summary>
    /// Token-specific identifier for an object
    /// </summary>
    public class ObjectHandle
    {
        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        private ulong _objectId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        public ulong ObjectId
        {
            get
            {
                return _objectId;
            }
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class with ObjectId set to CK_INVALID_HANDLE
        /// </summary>
        public ObjectHandle()
        {
            _objectId = CK.CK_INVALID_HANDLE;
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class
        /// </summary>
        /// <param name="objectId">PKCS#11 handle of object</param>
        public ObjectHandle(ulong objectId)
        {
            _objectId = objectId;
        }
    }
}
