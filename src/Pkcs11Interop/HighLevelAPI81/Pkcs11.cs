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
using Net.Pkcs11Interop.LowLevelAPI81;

namespace Net.Pkcs11Interop.HighLevelAPI81
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
        /// Low level PKCS#11 wrapper
        /// </summary>
        private LowLevelAPI81.Pkcs11 _p11 = null;

        /// <summary>
        /// Low level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public LowLevelAPI81.Pkcs11 LowLevelPkcs11
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _p11;
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        public Pkcs11(string libraryPath, bool useOsLocking)
        {
            _p11 = new LowLevelAPI81.Pkcs11(libraryPath);

            try
            {
                CK_C_INITIALIZE_ARGS initArgs = null;
                if (useOsLocking)
                {
                    initArgs = new CK_C_INITIALIZE_ARGS();
                    initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
                }

                CKR rv = _p11.C_Initialize(initArgs);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    throw new Pkcs11Exception("C_Initialize", rv);
            }
            catch
            {
                _p11.Dispose();
                _p11 = null;
                throw;
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
            _p11 = new LowLevelAPI81.Pkcs11(libraryPath, useGetFunctionList);

            try
            {
                CK_C_INITIALIZE_ARGS initArgs = null;
                if (useOsLocking)
                {
                    initArgs = new CK_C_INITIALIZE_ARGS();
                    initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
                }

                CKR rv = _p11.C_Initialize(initArgs);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    throw new Pkcs11Exception("C_Initialize", rv);
            }
            catch
            {
                _p11.Dispose();
                _p11 = null;
                throw;
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

            CK_INFO info = new CK_INFO();
            CKR rv = _p11.C_GetInfo(ref info);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetInfo", rv);

            return new LibraryInfo(info);
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

            ulong slotCount = 0;
            CKR rv = _p11.C_GetSlotList(tokenPresent, null, ref slotCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotList", rv);

            if (slotCount == 0)
            {
                return new List<Slot>();
            }
            else
            {
                ulong[] slotList = new ulong[slotCount];
                rv = _p11.C_GetSlotList(tokenPresent, slotList, ref slotCount);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_GetSlotList", rv);

                if (slotList.Length != Convert.ToInt32(slotCount))
                    Array.Resize(ref slotList, Convert.ToInt32(slotCount));

                List<Slot> list = new List<Slot>();
                foreach (ulong slot in slotList)
                    list.Add(new Slot(_p11, slot));

                return list;
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

            ulong flags = (dontBlock) ? CKF.CKF_DONT_BLOCK : 0;

            ulong slotId_ = 0;
            CKR rv = _p11.C_WaitForSlotEvent(flags, ref slotId_, IntPtr.Zero);
            if (dontBlock)
            {
                if (rv == CKR.CKR_OK)
                {
                    eventOccured = true;
                    slotId = slotId_;
                }
                else if (rv == CKR.CKR_NO_EVENT)
                {
                    eventOccured = false;
                    slotId = slotId_;
                }
                else
                {
                    throw new Pkcs11Exception("C_WaitForSlotEvent", rv);
                }
            }
            else
            {
                if (rv == CKR.CKR_OK)
                {
                    eventOccured = true;
                    slotId = slotId_;
                }
                else
                {
                    throw new Pkcs11Exception("C_WaitForSlotEvent", rv);
                }
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
                    if (_p11 != null)
                    {
                        _p11.C_Finalize(IntPtr.Zero);
                        _p11.Dispose();
                        _p11 = null;
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
