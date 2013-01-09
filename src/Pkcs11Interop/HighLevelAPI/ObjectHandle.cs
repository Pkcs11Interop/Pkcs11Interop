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

using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Token-specific identifier for an object
    /// </summary>
    public class ObjectHandle
    {
        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        private uint _objectId = CK.CK_INVALID_HANDLE;

        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        public uint ObjectId
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
        public ObjectHandle(uint objectId)
        {
            _objectId = objectId;
        }
    }
}
