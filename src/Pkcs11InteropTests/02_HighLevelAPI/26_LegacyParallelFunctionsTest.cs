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
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetFunctionStatus and CancelFunction tests.
    /// </summary>
    [TestFixture()]
    public class LegacyParallelFunctionsTest
    {
        /// <summary>
        /// GetFunctionStatus test.
        /// </summary>
        [Test()]
        public void GetFunctionStatusTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
                
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                    try
                    {
                        session.GetFunctionStatus();
                    }
                    catch (Pkcs11Exception ex)
                    {
                        if (ex.RV != CKR.CKR_FUNCTION_NOT_PARALLEL)
                            throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// CancelFunction test.
        /// </summary>
        [Test()]
        public void CancelFunctionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, false))
            {
                // Find first slot with token present
                Slot slot = Helpers.GetUsableSlot(pkcs11);
            
                // Open RO (read-only) session
                using (Session session = slot.OpenSession(true))
                {
                    // Legacy functions should always return CKR_FUNCTION_NOT_PARALLEL
                    try
                    {
                        session.CancelFunction();
                    } catch (Pkcs11Exception ex)
                    {
                        if (ex.RV != CKR.CKR_FUNCTION_NOT_PARALLEL)
                            throw;
                    }
                }
            }
        }
    }
}
