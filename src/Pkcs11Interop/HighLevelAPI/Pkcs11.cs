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
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI4.Pkcs11 _p11_4 = null;

        /// <summary>
        /// Platform specific high level PKCS#11 wrapper
        /// </summary>
        private HighLevelAPI8.Pkcs11 _p11_8 = null;

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        public Pkcs11(string libraryPath, bool useOsLocking)
        {
            if (UnmanagedLong.Size == 4)
                _p11_4 = new HighLevelAPI4.Pkcs11(libraryPath, useOsLocking);
            else
                _p11_8 = new HighLevelAPI8.Pkcs11(libraryPath, useOsLocking);
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        /// <param name="useGetFunctionList">Flag indicating whether cryptoki function pointers should be acquired via C_GetFunctionList (true) or via platform native function (false)</param>
        public Pkcs11(string libraryPath, bool useOsLocking, bool useGetFunctionList)
        {
            if (UnmanagedLong.Size == 4)
                _p11_4 = new HighLevelAPI4.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
            else
                _p11_8 = new HighLevelAPI8.Pkcs11(libraryPath, useOsLocking, useGetFunctionList);
        }

        /// <summary>
        /// Gets general information about loaded PKCS#11 library
        /// </summary>
        /// <returns>General information about loaded PKCS#11 library</returns>
        public LibraryInfo GetInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
            {
                HighLevelAPI4.LibraryInfo hlaLibraryInfo = _p11_4.GetInfo();
                return new LibraryInfo(hlaLibraryInfo);
            }
            else
            {
                HighLevelAPI8.LibraryInfo hlaLibraryInfo = _p11_8.GetInfo();
                return new LibraryInfo(hlaLibraryInfo);
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

            if (UnmanagedLong.Size == 4)
            {
                List<HighLevelAPI4.Slot> hlaSlotList = _p11_4.GetSlotList(tokenPresent);
                List<Slot> slotList = new List<Slot>();
                foreach (HighLevelAPI4.Slot hlaSlot in hlaSlotList)
                    slotList.Add(new Slot(hlaSlot));
                return slotList;
            }
            else
            {
                List<HighLevelAPI8.Slot> hlaSlotList = _p11_8.GetSlotList(tokenPresent);
                List<Slot> slotList = new List<Slot>();
                foreach (HighLevelAPI8.Slot hlaSlot in hlaSlotList)
                    slotList.Add(new Slot(hlaSlot));
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

            if (UnmanagedLong.Size == 4)
            {
                uint uintSlotId = CK.CK_INVALID_HANDLE;
                _p11_4.WaitForSlotEvent(dontBlock, out eventOccured, out uintSlotId);
                slotId = uintSlotId;
            }
            else
            {
                _p11_8.WaitForSlotEvent(dontBlock, out eventOccured, out slotId);
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
                    if (_p11_4 != null)
                    {
                        _p11_4.Dispose();
                        _p11_4 = null;
                    }

                    if (_p11_8 != null)
                    {
                        _p11_8.Dispose();
                        _p11_8 = null;
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
