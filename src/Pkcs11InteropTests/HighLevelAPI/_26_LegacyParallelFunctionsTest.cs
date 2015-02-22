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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// GetFunctionStatus and CancelFunction tests.
    /// </summary>
    [TestFixture()]
    public class _26_LegacyParallelFunctionsTest
    {
        /// <summary>
        /// GetFunctionStatus test.
        /// </summary>
        [Test()]
        public void _01_GetFunctionStatusTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
        public void _02_CancelFunctionTest()
        {
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath, Settings.UseOsLocking))
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
