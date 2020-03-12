/*
 *  Copyright 2012-2020 The Pkcs11Interop Project
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
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.LowLevelAPI80;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI80
{
    /// <summary>
    /// High level PKCS#11 wrapper
    /// </summary>
    public class Pkcs11Library : IPkcs11Library
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        protected bool _disposed = false;

        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(Pkcs11Library));

        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        protected Pkcs11InteropFactories _factories = null;

        /// <summary>
        /// Factories to be used by Developer and Pkcs11Interop library
        /// </summary>
        public Pkcs11InteropFactories Factories
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _factories;
            }
        }

        /// <summary>
        /// Library name or path
        /// </summary>
        protected string _libraryPath = null;

        /// <summary>
        /// Low level PKCS#11 wrapper
        /// </summary>
        protected LowLevelAPI80.Pkcs11Library _pkcs11Library = null;

        /// <summary>
        /// Initializes new instance of Pkcs11Library class
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        protected Pkcs11Library(Pkcs11InteropFactories factories, string libraryPath)
        {
            _logger.Debug("Pkcs11Library({0})::ctor1", libraryPath);

            if (factories == null)
                throw new ArgumentNullException("factories");

            _factories = factories;
            _libraryPath = libraryPath;
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        public Pkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType)
            : this(factories, libraryPath)
        {
            _logger.Debug("Pkcs11Library({0})::ctor2", _libraryPath);

            try
            {
                _logger.Info("Loading PKCS#11 library {0}", _libraryPath);
                _pkcs11Library = new LowLevelAPI80.Pkcs11Library(_libraryPath);
                Initialize(appType);
            }
            catch
            {
                if (_pkcs11Library != null)
                {
                    _logger.Info("Unloading PKCS#11 library {0}", _libraryPath);
                    _pkcs11Library.Dispose();
                    _pkcs11Library = null;
                }

                throw;
            }
        }

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        /// <param name="initType">Source of PKCS#11 function pointers</param>
        public Pkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType, InitType initType)
            : this(factories, libraryPath)
        {
            _logger.Debug("Pkcs11Library({0})::ctor3", _libraryPath);

            try
            {
                _logger.Info("Loading PKCS#11 library {0}", _libraryPath);
                _pkcs11Library = new LowLevelAPI80.Pkcs11Library(_libraryPath, (initType == InitType.WithFunctionList));
                Initialize(appType);
            }
            catch
            {
                if (_pkcs11Library != null)
                {
                    _logger.Info("Unloading PKCS#11 library {0}", _libraryPath);
                    _pkcs11Library.Dispose();
                    _pkcs11Library = null;
                }

                throw;
            }
        }

        /// <summary>
        /// Initializes PCKS#11 library
        /// </summary>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        protected void Initialize(AppType appType)
        {
            _logger.Debug("Pkcs11Library({0})::Initialize", _libraryPath);

            CK_C_INITIALIZE_ARGS initArgs = null;
            if (appType == AppType.MultiThreaded)
            {
                initArgs = new CK_C_INITIALIZE_ARGS();
                initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
            }

            CKR rv = _pkcs11Library.C_Initialize(initArgs);
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                throw new Pkcs11Exception("C_Initialize", rv);
        }

        /// <summary>
        /// Gets general information about loaded PKCS#11 library
        /// </summary>
        /// <returns>General information about loaded PKCS#11 library</returns>
        public ILibraryInfo GetInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("Pkcs11Library({0})::GetInfo", _libraryPath);

            CK_INFO info = new CK_INFO();
            CKR rv = _pkcs11Library.C_GetInfo(ref info);
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

            _logger.Debug("Pkcs11Library({0})::GetSlotList", _libraryPath);

            NativeULong slotCount = 0;
            CKR rv = _pkcs11Library.C_GetSlotList((slotsType == SlotsType.WithTokenPresent), null, ref slotCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotList", rv);

            if (slotCount == 0)
            {
                return new List<ISlot>();
            }
            else
            {
                NativeULong[] slotList = new NativeULong[slotCount];
                rv = _pkcs11Library.C_GetSlotList((slotsType == SlotsType.WithTokenPresent), slotList, ref slotCount);
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_GetSlotList", rv);

                if (slotList.Length != ConvertUtils.UInt64ToInt32(slotCount))
                    Array.Resize(ref slotList, ConvertUtils.UInt64ToInt32(slotCount));

                List<ISlot> list = new List<ISlot>();
                foreach (NativeULong slot in slotList)
                    list.Add(_factories.SlotFactory.Create(_factories, _pkcs11Library, slot));

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

            _logger.Debug("Pkcs11Library({0})::WaitForSlotEvent", _libraryPath);

            NativeULong flags = (waitType == WaitType.NonBlocking) ? CKF.CKF_DONT_BLOCK : 0;

            NativeULong slotId_ = 0;
            CKR rv = _pkcs11Library.C_WaitForSlotEvent(flags, ref slotId_, IntPtr.Zero);
            if (waitType == WaitType.NonBlocking)
            {
                if (rv == CKR.CKR_OK)
                {
                    eventOccured = true;
                    slotId = ConvertUtils.UInt64ToUInt64(slotId_);
                }
                else if (rv == CKR.CKR_NO_EVENT)
                {
                    eventOccured = false;
                    slotId = ConvertUtils.UInt64ToUInt64(slotId_);
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
                    slotId = ConvertUtils.UInt64ToUInt64(slotId_);
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
            _logger.Debug("Pkcs11Library({0})::Dispose1", _libraryPath);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            _logger.Debug("Pkcs11Library({0})::Dispose2", _libraryPath);

            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                    if (_pkcs11Library != null)
                    {
                        _pkcs11Library.C_Finalize(IntPtr.Zero);

                        _logger.Info("Unloading PKCS#11 library {0}", _libraryPath);
                        _pkcs11Library.Dispose();
                        _pkcs11Library = null;
                    }
                }

                // Dispose unmanaged objects
                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~Pkcs11Library()
        {
            Dispose(false);
        }

        #endregion
    }
}
