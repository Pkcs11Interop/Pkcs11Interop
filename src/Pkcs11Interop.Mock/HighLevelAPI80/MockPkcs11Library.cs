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
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Logging;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.Mock.HighLevelAPI80
{
    /// <summary>
    /// High level PKCS#11 wrapper extended with vendor specific functions of PKCS11-MOCK module.
    /// </summary>
    public class MockPkcs11Library : Net.Pkcs11Interop.HighLevelAPI80.Pkcs11Library, IMockPkcs11Library
    {
        /// <summary>
        /// Logger responsible for message logging
        /// </summary>
        private Pkcs11InteropLogger _logger = Pkcs11InteropLoggerFactory.GetLogger(typeof(MockPkcs11Library));

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="factories">Factories to be used by Developer and Pkcs11Interop library</param>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="appType">Type of application that will be using PKCS#11 library</param>
        public MockPkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType)
            : base(factories, libraryPath)
        {
            _logger.Debug("MockPkcs11Library({0})::ctor1", _libraryPath);

            try
            {
                _logger.Info("Loading PKCS#11 library {0}", _libraryPath);
                _pkcs11Library = new LowLevelAPI80.MockPkcs11Library(libraryPath);
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
        public MockPkcs11Library(Pkcs11InteropFactories factories, string libraryPath, AppType appType, InitType initType)
            : base(factories, libraryPath)
        {
            _logger.Debug("MockPkcs11Library({0})::ctor2", _libraryPath);

            try
            {
                _logger.Info("Loading PKCS#11 library {0}", _libraryPath);
                _pkcs11Library = new LowLevelAPI80.MockPkcs11Library(libraryPath, (initType == InitType.WithFunctionList));
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
        /// Obtains a list of unmanaged struct sizes.
        /// </summary>
        /// <returns>List of unmanaged struct sizes</returns>
        public List<ulong> GetUnmanagedStructSizeList()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            _logger.Debug("MockPkcs11Library({0})::GetUnmanagedStructSizeList", _libraryPath);

            NativeULong sizeCount = 0;
            CKR rv = ((LowLevelAPI80.MockPkcs11Library)_pkcs11Library).C_GetUnmanagedStructSizeList(null, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            NativeULong[] sizeList = new NativeULong[sizeCount];
            rv = ((LowLevelAPI80.MockPkcs11Library)_pkcs11Library).C_GetUnmanagedStructSizeList(sizeList, ref sizeCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetUnmanagedStructSizeList", rv);

            if (sizeList.Length != ConvertUtils.UInt64ToInt32(sizeCount))
                Array.Resize(ref sizeList, ConvertUtils.UInt64ToInt32(sizeCount));

            List<ulong> ulongList = new List<ulong>();
            foreach (NativeULong size in sizeList)
                ulongList.Add(ConvertUtils.UInt64ToUInt64(size));

            return ulongList;
        }

        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected override void Dispose(bool disposing)
        {
            _logger.Debug("MockPkcs11Library({0})::Dispose", _libraryPath);

            base.Dispose(disposing);
        }
    }
}
