/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using System;
using System.Runtime.InteropServices;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.LowLevelAPI
{
    /// <summary>
    /// Utility class that helps to manage CK_MECHANISM structure
    /// </summary>
    public class CkmUtils
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
            ckMechanism.ParameterLen = (uint)UnmanagedMemory.SizeOf(parameterStructure.GetType());
            ckMechanism.Parameter = UnmanagedMemory.Allocate((int)ckMechanism.ParameterLen);
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
                mech.ParameterLen = (uint)parameter.Length;
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
