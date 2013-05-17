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
using System.Collections.Generic;
using System.Text;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Mechanism and its parameters (CK_MECHANISM alternative)
    /// </summary>
    public class Mechanism : IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism structure
        /// </summary>
        private LowLevelAPI.CK_MECHANISM _ckMechanism;

        /// <summary>
        /// Low level mechanism structure
        /// </summary>
        internal LowLevelAPI.CK_MECHANISM CkMechanism
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _ckMechanism;
            }
        }

        /// <summary>
        /// The type of mechanism
        /// </summary>
        public uint Type
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _ckMechanism.Mechanism;
            }
        }

        /// <summary>
        /// High level object with mechanism parameters
        /// </summary>
        private IMechanismParams _mechanismParams = null;

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(uint type)
        {
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(CKM type)
        {
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(uint type, byte[] parameter)
        {
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(CKM type, byte[] parameter)
        {
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(uint type, IMechanismParams parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            // Keep reference to parameter so GC will not free it while mechanism exists
            _mechanismParams = parameter;

            object lowLevelParams = _mechanismParams.ToLowLevelParams();
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type, lowLevelParams);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(CKM type, IMechanismParams parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            // Keep reference to parameter so GC will not free it while mechanism exists
            _mechanismParams = parameter;

            object lowLevelParams = _mechanismParams.ToLowLevelParams();
            _ckMechanism = LowLevelAPI.CkmUtils.CreateMechanism(type, lowLevelParams);
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
                }
                
                // Dispose unmanaged objects
                LowLevelAPI.UnmanagedMemory.Free(ref _ckMechanism.Parameter);
                _ckMechanism.ParameterLen = 0;
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~Mechanism()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
