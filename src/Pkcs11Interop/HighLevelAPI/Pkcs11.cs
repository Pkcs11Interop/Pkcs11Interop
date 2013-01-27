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

using System;
using System.Collections.Generic;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /**
     * \example HighLevelAPI/InitializeTest.cs
     * \example HighLevelAPI/GetInfoTest.cs
     * \example HighLevelAPI/SlotListInfoAndEventTest.cs
     * \example HighLevelAPI/TokenInfoTest.cs
     * \example HighLevelAPI/MechanismListAndInfoTest.cs
     * \example HighLevelAPI/SessionTest.cs
     * \example HighLevelAPI/OperationStateTest.cs
     * \example HighLevelAPI/LoginTest.cs
     * \example HighLevelAPI/InitTokenAndPinTest.cs
     * \example HighLevelAPI/SetPinTest.cs
     * \example HighLevelAPI/SeedAndGenerateRandomTest.cs
     * \example HighLevelAPI/DigestTest.cs
     * \example HighLevelAPI/ObjectAttributeTest.cs
     * \example HighLevelAPI/MechanismTest.cs
     * \example HighLevelAPI/CreateCopyDestroyObjectTest.cs
     * \example HighLevelAPI/GetAndSetAttributeValueTest.cs
     * \example HighLevelAPI/ObjectFindingTest.cs
     * \example HighLevelAPI/GenerateKeyAndKeyPairTest.cs
     * \example HighLevelAPI/EncryptAndDecryptTest.cs
     * \example HighLevelAPI/SignAndVerifyTest.cs
     * \example HighLevelAPI/SignAndVerifyRecoverTest.cs
     * \example HighLevelAPI/DigestEncryptAndDecryptDigestTest.cs
     * \example HighLevelAPI/SignEncryptAndDecryptVerifyTest.cs
     * \example HighLevelAPI/WrapAndUnwrapKeyTest.cs
     * \example HighLevelAPI/DeriveKeyTest.cs
     * \example HighLevelAPI/LegacyParallelFunctionsTest.cs
     * \example HighLevelAPI/Helpers.cs
     */

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
        /// Low level PKCS#11 wrapper
        /// </summary>
        private LowLevelAPI.Pkcs11 _p11 = null;

        /// <summary>
        /// Loads and initializes PCKS#11 library
        /// </summary>
        /// <param name="libraryPath">Library name or path</param>
        /// <param name="useOsLocking">Flag indicating whether PKCS#11 library can use the native operation system threading model for locking. Should be set to true in all multithreaded applications.</param>
        public Pkcs11(string libraryPath, bool useOsLocking)
        {
            if (libraryPath == null)
                throw new ArgumentNullException("libraryPath");

            _p11 = new LowLevelAPI.Pkcs11(libraryPath);

            LowLevelAPI.CK_C_INITIALIZE_ARGS initArgs = null;
            if (useOsLocking)
            {
                initArgs = new LowLevelAPI.CK_C_INITIALIZE_ARGS();
                initArgs.Flags = CKF.CKF_OS_LOCKING_OK;
            }

            CKR rv = _p11.C_Initialize(initArgs);
            if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                throw new Pkcs11Exception("C_Initialize", rv);
        }

        /// <summary>
        /// Gets general information about loaded PKCS#11 library
        /// </summary>
        /// <returns>General information about loaded PKCS#11 library</returns>
        public LibraryInfo GetInfo()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            LowLevelAPI.CK_INFO info = new LowLevelAPI.CK_INFO();
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

            uint slotCount = 0;
            CKR rv = _p11.C_GetSlotList(tokenPresent, null, ref slotCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotList", rv);

            uint[] slotList = new uint[slotCount];
            rv = _p11.C_GetSlotList(tokenPresent, slotList, ref slotCount);
            if (rv != CKR.CKR_OK)
                throw new Pkcs11Exception("C_GetSlotList", rv);

            if (slotList.Length != slotCount)
                Array.Resize(ref slotList, (int)slotCount);

            List<Slot> list = new List<Slot>();
            foreach (uint slot in slotList)
                list.Add(new Slot(_p11, slot));

            return list;
        }

        /// <summary>
        /// Waits for a slot event, such as token insertion or token removal, to occur
        /// </summary>
        /// <param name="dontBlock">Flag indicating that method should not block until an event occurs - it should return immediately instead. See PKCS#11 standard for full explanation.</param>
        /// <returns>PKCS#11 handle of slot that the event occurred in</returns>
        public uint WaitForSlotEvent(bool dontBlock)
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            uint flags = (dontBlock) ? CKF.CKF_DONT_BLOCK : 0;

            uint slotId = CK.CK_INVALID_HANDLE;
            CKR rv = _p11.C_WaitForSlotEvent(flags, ref slotId, IntPtr.Zero);
            if (dontBlock)
            {
                if (rv == CKR.CKR_NO_EVENT)
                    slotId = CK.CK_INVALID_HANDLE;
                else if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_WaitForSlotEvent", rv);
            }
            else
            {
                if (rv != CKR.CKR_OK)
                    throw new Pkcs11Exception("C_WaitForSlotEvent", rv);
            }

            return slotId;
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
