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
        private HighLevelAPI40.ObjectHandle _objectHandle40 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI40.ObjectHandle ObjectHandle40
        {
            get
            {
                return _objectHandle40;
            }
        }

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        private HighLevelAPI41.ObjectHandle _objectHandle41 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI41.ObjectHandle ObjectHandle41
        {
            get
            {
                return _objectHandle41;
            }
        }

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        private HighLevelAPI80.ObjectHandle _objectHandle80 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI80.ObjectHandle ObjectHandle80
        {
            get
            {
                return _objectHandle80;
            }
        }

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        private HighLevelAPI81.ObjectHandle _objectHandle81 = null;

        /// <summary>
        /// Platform specific ObjectHandle
        /// </summary>
        internal HighLevelAPI81.ObjectHandle ObjectHandle81
        {
            get
            {
                return _objectHandle81;
            }
        }

        /// <summary>
        /// PKCS#11 handle of object
        /// </summary>
        public ulong ObjectId
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _objectHandle40.ObjectId : _objectHandle41.ObjectId;
                else
                    return (Platform.StructPackingSize == 0) ? _objectHandle80.ObjectId : _objectHandle81.ObjectId;
            }
        }

        /// <summary>
        /// Initializes new instance of ObjectHandle class with ObjectId set to CK_INVALID_HANDLE
        /// </summary>
        public ObjectHandle()
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _objectHandle40 = new HighLevelAPI40.ObjectHandle();
                else
                    _objectHandle41 = new HighLevelAPI41.ObjectHandle();
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _objectHandle80 = new HighLevelAPI80.ObjectHandle();
                else
                    _objectHandle81 = new HighLevelAPI81.ObjectHandle();
            }
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI40.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle40 = objectHandle;
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI41.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle41 = objectHandle;
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI80.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle80 = objectHandle;
        }

        /// <summary>
        /// Converts platform specific ObjectHandle to platfrom neutral ObjectHandle
        /// </summary>
        /// <param name="objectHandle">Platform specific ObjectHandle</param>
        internal ObjectHandle(HighLevelAPI81.ObjectHandle objectHandle)
        {
            if (objectHandle == null)
                throw new ArgumentNullException("objectHandle");

            _objectHandle81 = objectHandle;
        }

        #region Conversions

        #region HighLevelAPI40

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI40.ObjectHandle> ConvertToHighLevelAPI40List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI40.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI40.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle40);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI40List(List<HighLevelAPI40.ObjectHandle> hlaObjectHandles)
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

        #region HighLevelAPI41

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI41.ObjectHandle> ConvertToHighLevelAPI41List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI41.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI41.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle41);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI41List(List<HighLevelAPI41.ObjectHandle> hlaObjectHandles)
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

        #region HighLevelAPI80

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI80.ObjectHandle> ConvertToHighLevelAPI80List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI80.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI80.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle80);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI80List(List<HighLevelAPI80.ObjectHandle> hlaObjectHandles)
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

        #region HighLevelAPI81

        /// <summary>
        /// Converts platfrom neutral ObjectHandles to platform specific ObjectHandles
        /// </summary>
        /// <param name="objectHandles">Platfrom neutral ObjectHandles</param>
        /// <returns>Platform specific ObjectHandles</returns>
        internal static List<HighLevelAPI81.ObjectHandle> ConvertToHighLevelAPI81List(List<ObjectHandle> objectHandles)
        {
            List<HighLevelAPI81.ObjectHandle> hlaObjectHandles = null;

            if (objectHandles != null)
            {
                hlaObjectHandles = new List<HighLevelAPI81.ObjectHandle>();
                for (int i = 0; i < objectHandles.Count; i++)
                    hlaObjectHandles.Add(objectHandles[i].ObjectHandle81);
            }

            return hlaObjectHandles;
        }

        /// <summary>
        /// Converts platform specific ObjectHandles to platfrom neutral ObjectHandles
        /// </summary>
        /// <param name="hlaObjectHandles">Platform specific ObjectHandles</param>
        /// <returns>Platfrom neutral ObjectHandles</returns>
        internal static List<ObjectHandle> ConvertFromHighLevelAPI81List(List<HighLevelAPI81.ObjectHandle> hlaObjectHandles)
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

        #endregion
    }
}
