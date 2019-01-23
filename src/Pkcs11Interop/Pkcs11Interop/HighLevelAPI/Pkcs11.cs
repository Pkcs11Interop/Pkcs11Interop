/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

/*
 *  Written for the Pkcs11Interop project by:
 *  Jaroslav IMRICH <jimrich@jimrich.sk>
 */

using System;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// High level PKCS#11 wrapper
    /// </summary>
    public class Pkcs11 : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        public bool Disposed
        {
            get
            {
                return _disposed;
            }
        }

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI40.Pkcs11 _p11_40 = null;

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public HighLevelAPI40.Pkcs11 HLA40Pkcs11
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _p11_40;
            }
        }

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI41.Pkcs11 _p11_41 = null;

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public HighLevelAPI41.Pkcs11 HLA41Pkcs11
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _p11_41;
            }
        }

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI80.Pkcs11 _p11_80 = null;

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public HighLevelAPI80.Pkcs11 HLA80Pkcs11
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _p11_80;
            }
        }

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI81.Pkcs11 _p11_81 = null;

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public HighLevelAPI81.Pkcs11 HLA81Pkcs11
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _p11_81;
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        public Pkcs11(string libraryPath, AppType appType)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _p11_40 = new HighLevelAPI40.Pkcs11(libraryPath, appType);
                else
                    _p11_41 = new HighLevelAPI41.Pkcs11(libraryPath, appType);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80 = new HighLevelAPI80.Pkcs11(libraryPath, appType);
                else
                    _p11_81 = new HighLevelAPI81.Pkcs11(libraryPath, appType);
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <param name="initType">Source of PKCS#11 function pointers</param>
        public Pkcs11(string libraryPath, AppType appType, InitType initType)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _p11_40 = new HighLevelAPI40.Pkcs11(libraryPath, appType, initType);
                else
                    _p11_41 = new HighLevelAPI41.Pkcs11(libraryPath, appType, initType);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80 = new HighLevelAPI80.Pkcs11(libraryPath, appType, initType);
                else
                    _p11_81 = new HighLevelAPI81.Pkcs11(libraryPath, appType, initType);
            }
        }

        /// <summary>
        /// Gets general information about loaded PKCS#11 library
        /// </summary>
        /// <returns>General information about loaded PKCS#11 library</returns>
        public LibraryInfo GetInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    return new LibraryInfo(_p11_40.GetInfo());
                else
                    return new LibraryInfo(_p11_41.GetInfo());
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    return new LibraryInfo(_p11_80.GetInfo());
                else
                    return new LibraryInfo(_p11_81.GetInfo());
            }
        }

        /// <summary>
        /// Obtains a list of slots in the system
        /// </summary>
        /// <param name="slotsType">Type of slots to be obtained</param>
        /// <returns>List of available slots</returns>
        public List<Slot> GetSlotList(SlotsType slotsType)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                List<Slot> slotList = new List<Slot>();

                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.Slot> hlaSlotList = _p11_40.GetSlotList(slotsType);
                    foreach (HighLevelAPI40.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }
                else
                {
                    List<HighLevelAPI41.Slot> hlaSlotList = _p11_41.GetSlotList(slotsType);
                    foreach (HighLevelAPI41.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }

                return slotList;
            }
            else
            {
                List<Slot> slotList = new List<Slot>();

                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI80.Slot> hlaSlotList = _p11_80.GetSlotList(slotsType);
                    foreach (HighLevelAPI80.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }
                else
                {
                    List<HighLevelAPI81.Slot> hlaSlotList = _p11_81.GetSlotList(slotsType);
                    foreach (HighLevelAPI81.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }

                return slotList;
            }
        }

        /// <summary>
        /// Waits for a slot event, such as token insertion or token removal, to occur
        /// </summary>
        /// <param name="waitType">Type of waiting for a slot event</param>
        /// <param name="eventOccured">Flag indicating whether event occured</param>
        /// <param name="slotId">PKCS#11 handle of slot that the event occurred in</param>
        public void WaitForSlotEvent(WaitType waitType, out bool eventOccured, out ulong slotId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                uint uintSlotId = CK.CK_INVALID_HANDLE;

                if (Platform.StructPackingSize == 0)
                    _p11_40.WaitForSlotEvent(waitType, out eventOccured, out uintSlotId);
                else
                    _p11_41.WaitForSlotEvent(waitType, out eventOccured, out uintSlotId);
                
                slotId = uintSlotId;
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80.WaitForSlotEvent(waitType, out eventOccured, out slotId);
                else
                    _p11_81.WaitForSlotEvent(waitType, out eventOccured, out slotId);
            }
        }

        #region IDisposable

        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                    if (_p11_40 != null)
                    {
                        _p11_40.Dispose();
                        _p11_40 = null;
                    }

                    if (_p11_41 != null)
                    {
                        _p11_41.Dispose();
                        _p11_41 = null;
                    }

                    if (_p11_80 != null)
                    {
                        _p11_80.Dispose();
                        _p11_80 = null;
                    }

                    if (_p11_81 != null)
                    {
                        _p11_81.Dispose();
                        _p11_81 = null;
                    }
                }

                // Dispose unmanaged objects
                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~Pkcs11()
        {
            Dispose(false);
        }

        #endregion
    }
}
