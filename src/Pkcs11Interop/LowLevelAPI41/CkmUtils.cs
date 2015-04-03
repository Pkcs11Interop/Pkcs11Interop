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
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI41
{
    /// <summary>
    /// Utility class that helps to manage CK_MECHANISM structure
    /// </summary>
    public static class CkmUtils
    {
        #region Mechanism with no parameter

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <returns>Mechanism of given type with no parameter</returns>
        public static CK_MECHANISM CreateMechanism(CKM mechanism)
        {
            return CreateMechanism((uint)mechanism);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <returns>Mechanism of given type with no parameter</returns>
        public static CK_MECHANISM CreateMechanism(uint mechanism)
        {
            return _CreateMechanism(mechanism, null);
        }

        #endregion

        #region Mechanism with byte array parameter

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism of given type with byte array parameter</returns>
        public static CK_MECHANISM CreateMechanism(CKM mechanism, byte[] parameter)
        {
            return CreateMechanism((uint)mechanism, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism of given type with byte array parameter</returns>
        public static CK_MECHANISM CreateMechanism(uint mechanism, byte[] parameter)
        {
            return _CreateMechanism(mechanism, parameter);
        }

        #endregion

        #region Mechanism with structure as parameter

        /// <summary>
        /// Creates mechanism of given type with structure as parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameterStructure">Structure with mechanism parameters</param>
        /// <returns>Mechanism of given type with structure as parameter</returns>
        public static CK_MECHANISM CreateMechanism(CKM mechanism, object parameterStructure)
        {
            if (parameterStructure == null)
                throw new ArgumentNullException("parameterStructure");
            
            return CreateMechanism((uint)mechanism, parameterStructure);
        }
        
        /// <summary>
        /// Creates mechanism of given type with structure as parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameterStructure">Structure with mechanism parameters</param>
        /// <returns>Mechanism of given type with structure as parameter</returns>
        public static CK_MECHANISM CreateMechanism(uint mechanism, object parameterStructure)
        {
            if (parameterStructure == null)
                throw new ArgumentNullException("parameterStructure");

            CK_MECHANISM ckMechanism = new CK_MECHANISM();
            ckMechanism.Mechanism = mechanism;
            ckMechanism.ParameterLen = Convert.ToUInt32(UnmanagedMemory.SizeOf(parameterStructure.GetType()));
            ckMechanism.Parameter = UnmanagedMemory.Allocate(Convert.ToInt32(ckMechanism.ParameterLen));
            UnmanagedMemory.Write(ckMechanism.Parameter, parameterStructure);

            return ckMechanism;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates mechanism of given type with parameter copied from managed byte array to the newly allocated unmanaged memory
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism of given type with specified parameter</returns>
        private static CK_MECHANISM _CreateMechanism(uint mechanism, byte[] parameter)
        {
            CK_MECHANISM mech = new CK_MECHANISM();
            mech.Mechanism = mechanism;
            if ((parameter != null) && (parameter.Length > 0))
            {
                mech.Parameter = UnmanagedMemory.Allocate(parameter.Length);
                UnmanagedMemory.Write(mech.Parameter, parameter);
                mech.ParameterLen = Convert.ToUInt32(parameter.Length);
            }
            else
            {
                mech.Parameter = IntPtr.Zero;
                mech.ParameterLen = 0;
            }

            return mech;
        }

        #endregion
    }
}
