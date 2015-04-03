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
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Token-specific identifier for an object
    /// </summary>
    public class ObjectHandle
    {
        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        private HighLevelAPI41.ObjectHandle _objectHandle4 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI41.ObjectHandle ObjectHandle4
        {
            get
            {
                return _objectHandle4;
            }
        }

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        private HighLevelAPI81.ObjectHandle _objectHandle8 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI81.ObjectHandle ObjectHandle8
        {
            get
            {
                return _objectHandle8;
            }
        }

        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        public ulong ObjectId
        {
            get
            {
                return (Platform.UnmanagedLongSize == 4) ? _objectHandle4.ObjectId : _objectHandle8.ObjectId;
            }
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class with ObjectId set to CK_INVALID_HANDLE
        /// </summary>
        public ObjectHandle()
        {
            if (Platform.UnmanagedLongSize == 4)
                _objectHandle4 = new HighLevelAPI41.ObjectHandle();
            else
                _objectHandle8 = new HighLevelAPI81.ObjectHandle();
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI41.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle4 = objectHandle;
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI81.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle8 = objectHandle;
        }

        #region Conversions

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI41.ObjectHandle> ConvertToHighLevelAPI4List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI41.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI41.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle4);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI4List(List<HighLevelAPI41.ObjectHandle> hlaObjectHandles)
        {
            List<ObjectHandle> objectHandles = null;

            if (hlaObjectHandles != null)
            {
                objectHandles = new List<ObjectHandle>();
                for (int i = 0; i < hlaObjectHandles.Count; i++)
                    objectHandles.Add(new ObjectHandle(hlaObjectHandles[i]));
            }

            return objectHandles;
        }

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI81.ObjectHandle> ConvertToHighLevelAPI8List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI81.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI81.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle8);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI8List(List<HighLevelAPI81.ObjectHandle> hlaObjectHandles)
        {
            List<ObjectHandle> objectHandles = null;

            if (hlaObjectHandles != null)
            {
                objectHandles = new List<ObjectHandle>();
                for (int i = 0; i < hlaObjectHandles.Count; i++)
                    objectHandles.Add(new ObjectHandle(hlaObjectHandles[i]));
            }

            return objectHandles;
        }

        #endregion
    }
}
