/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.LowLevelAPI41;
using NativeULong = System.UInt32;

namespace Net.Pkcs11Interop.HighLevelAPI41
{
    /// <summary>
    /// High level PKCS#11 wrapper
    /// </summary>
    public class Pkcs11 : IPkcs11
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
        /// Factories used by Pkcs11Interop library
        /// </summary>
        private Pkcs11Factories _factories = null;

        /// <summary>
        /// Factories used by Pkcs11Interop library
        /// </summary>
        public Pkcs11Factories Factories
        {
            get
            {
                return _factories;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Factories");

                _factories = value;
            }
        }

        /// <summary>
        /// Low level PKCS#11 wrapper
        /// </summary>
        private LowLevelAPI41.Pkcs11 _p11 = null;

        /// <summary>
        /// Low level PKCS#11 wrapper. Use with caution!
        /// </summary>
        public LowLevelAPI41.Pkcs11 LowLevelPkcs11
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
        /// <param name="factories">Factories used by Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        public Pkcs11(Pkcs11Factories factories, string libraryPath, AppType appType)
        {
            if (factories == null)
                throw new ArgumentNullException("factories");

            _factories = factories;
            _p11 = new LowLevelAPI41.Pkcs11(libraryPath);

            try
            {
                CK_C_INITIALIZE_ARGS initArgs = null;
                if (appType == AppType.MultiThreaded)
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
        /// <param name="factories">Factories used by Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <param name="initType">Source of PKCS#11 function pointers</param>
        public Pkcs11(Pkcs11Factories factories, string libraryPath, AppType appType, InitType initType)
        {
            if (factories == null)
                throw new ArgumentNullException("factories");

            _factories = factories;
            _p11 = new LowLevelAPI41.Pkcs11(libraryPath, (initType == InitType.WithFunctionList));

            try
            {
                CK_C_INITIALIZE_ARGS initArgs = null;
                if (appType == AppType.MultiThreaded)
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
        public ILibraryInfo GetInfo()
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
        /// <param name="slotsType">Type of slots to be obtained</param>
        /// <returns>List of available slots</returns>
        public List<ISlot> GetSlotList(SlotsType slotsType)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            NativeULong slotCount = 0;
            CKR rv = _p11.C_GetSlotList((slotsType == SlotsType.WithTokenPresent), null, ref slotCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotList", rv);

            if (slotCount == 0)
            {
                return new List<ISlot>();
            }
            else
            {
                NativeULong[] slotList = new NativeULong[slotCount];
                rv = _p11.C_GetSlotList((slotsType == SlotsType.WithTokenPresent), slotList, ref slotCount);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_GetSlotList", rv);

                if (slotList.Length != NativeLongUtils.ConvertToInt32(slotCount))
                    Array.Resize(ref slotList, NativeLongUtils.ConvertToInt32(slotCount));

                List<ISlot> list = new List<ISlot>();
                foreach (NativeULong slot in slotList)
                    list.Add(_factories.SlotFactory.CreateSlot(_factories, _p11, slot));

                return list;
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

            NativeULong flags = (waitType == WaitType.NonBlocking) ? CKF.CKF_DONT_BLOCK : 0;

            NativeULong slotId_ = 0;
            CKR rv = _p11.C_WaitForSlotEvent(flags, ref slotId_, IntPtr.Zero);
            if (waitType == WaitType.NonBlocking)
            {
                if (rv == CKR.CKR_OK)
                {
                    eventOccured = true;
                    slotId = NativeLongUtils.ConvertToUInt64(slotId_);
                }
                else if (rv == CKR.CKR_NO_EVENT)
                {
                    eventOccured = false;
                    slotId = NativeLongUtils.ConvertToUInt64(slotId_);
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
                    slotId = NativeLongUtils.ConvertToUInt64(slotId_);
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
