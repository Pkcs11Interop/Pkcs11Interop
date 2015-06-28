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
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        public Pkcs11(string libraryPath, bool useOsLocking)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _p11_40 = new HighLevelAPI40.Pkcs11(libraryPath, useOsLocking);
                else
                    _p11_41 = new HighLevelAPI41.Pkcs11(libraryPath, useOsLocking);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80 = new HighLevelAPI80.Pkcs11(libraryPath, useOsLocking);
                else
                    _p11_81 = new HighLevelAPI81.Pkcs11(libraryPath, useOsLocking);
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        public Pkcs11(string libraryPath, bool useOsLocking, bool useGetFunctionList)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _p11_40 = new HighLevelAPI40.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
                else
                    _p11_41 = new HighLevelAPI41.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80 = new HighLevelAPI80.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
                else
                    _p11_81 = new HighLevelAPI81.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
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
        /// <param name="tokenPresent">Flag indicating whether the list obtained includes only those slots with a token present (true), or all slots (false)</param>
        /// <returns>List of available slots</returns>
        public List<Slot> GetSlotList(bool tokenPresent)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                List<Slot> slotList = new List<Slot>();

                if (Platform.StructPackingSize == 0)
                {
                    List<HighLevelAPI40.Slot> hlaSlotList = _p11_40.GetSlotList(tokenPresent);
                    foreach (HighLevelAPI40.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }
                else
                {
                    List<HighLevelAPI41.Slot> hlaSlotList = _p11_41.GetSlotList(tokenPresent);
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
                    List<HighLevelAPI80.Slot> hlaSlotList = _p11_80.GetSlotList(tokenPresent);
                    foreach (HighLevelAPI80.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }
                else
                {
                    List<HighLevelAPI81.Slot> hlaSlotList = _p11_81.GetSlotList(tokenPresent);
                    foreach (HighLevelAPI81.Slot hlaSlot in hlaSlotList)
                        slotList.Add(new Slot(hlaSlot));
                }

                return slotList;
            }
        }

        /// <summary>
        /// Waits for a slot event, such as token insertion or token removal, to occur
        /// </summary>
        /// <param name="dontBlock">Flag indicating that method should not block until an event occurs - it should return immediately instead. See PKCS#11 standard for full explanation.</param>
        /// <param name="eventOccured">Flag indicating whether event occured</param>
        /// <param name="slotId">PKCS#11 handle of slot that the event occurred in</param>
        public void WaitForSlotEvent(bool dontBlock, out bool eventOccured, out ulong slotId)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (Platform.UnmanagedLongSize == 4)
            {
                uint uintSlotId = CK.CK_INVALID_HANDLE;

                if (Platform.StructPackingSize == 0)
                    _p11_40.WaitForSlotEvent(dontBlock, out eventOccured, out uintSlotId);
                else
                    _p11_41.WaitForSlotEvent(dontBlock, out eventOccured, out uintSlotId);
                
                slotId = uintSlotId;
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _p11_80.WaitForSlotEvent(dontBlock, out eventOccured, out slotId);
                else
                    _p11_81.WaitForSlotEvent(dontBlock, out eventOccured, out slotId);
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
