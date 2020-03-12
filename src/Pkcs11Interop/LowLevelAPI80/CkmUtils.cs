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
using Net.Pkcs11Interop.Common;
using NativeULong = System.UInt64;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.LowLevelAPI80
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
            return CreateMechanism(ConvertUtils.UInt64FromCKM(mechanism));
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <returns>Mechanism of given type with no parameter</returns>
        public static CK_MECHANISM CreateMechanism(NativeULong mechanism)
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
            return CreateMechanism(ConvertUtils.UInt64FromCKM(mechanism), parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        /// <returns>Mechanism of given type with byte array parameter</returns>
        public static CK_MECHANISM CreateMechanism(NativeULong mechanism, byte[] parameter)
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
            
            return CreateMechanism(ConvertUtils.UInt64FromCKM(mechanism), parameterStructure);
        }
        
        /// <summary>
        /// Creates mechanism of given type with structure as parameter
        /// </summary>
        /// <param name="mechanism">Mechanism type</param>
        /// <param name="parameterStructure">Structure with mechanism parameters</param>
        /// <returns>Mechanism of given type with structure as parameter</returns>
        public static CK_MECHANISM CreateMechanism(NativeULong mechanism, object parameterStructure)
        {
            if (parameterStructure == null)
                throw new ArgumentNullException("parameterStructure");

            CK_MECHANISM ckMechanism = new CK_MECHANISM();
            ckMechanism.Mechanism = mechanism;
            ckMechanism.ParameterLen = ConvertUtils.UInt64FromInt32(UnmanagedMemory.SizeOf(parameterStructure.GetType()));
            ckMechanism.Parameter = UnmanagedMemory.Allocate(ConvertUtils.UInt64ToInt32(ckMechanism.ParameterLen));
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
        private static CK_MECHANISM _CreateMechanism(NativeULong mechanism, byte[] parameter)
        {
            CK_MECHANISM mech = new CK_MECHANISM();
            mech.Mechanism = mechanism;
            if ((parameter != null) && (parameter.Length > 0))
            {
                mech.Parameter = UnmanagedMemory.Allocate(parameter.Length);
                UnmanagedMemory.Write(mech.Parameter, parameter);
                mech.ParameterLen = ConvertUtils.UInt64FromInt32(parameter.Length);
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
