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
using Net.Pkcs11Interop.LowLevelAPI80;

namespace Net.Pkcs11Interop.HighLevelAPI80
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
        private CK_MECHANISM _ckMechanism;

        /// <summary>
        /// Low level mechanism structure
        /// </summary>
        internal CK_MECHANISM CkMechanism
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
        public ulong Type
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
        public Mechanism(ulong type)
        {
            _ckMechanism = CkmUtils.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(CKM type)
        {
            _ckMechanism = CkmUtils.CreateMechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(ulong type, byte[] parameter)
        {
            _ckMechanism = CkmUtils.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(CKM type, byte[] parameter)
        {
            _ckMechanism = CkmUtils.CreateMechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with object parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(ulong type, IMechanismParams parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            // Keep reference to parameter so GC will not free it while mechanism exists
            _mechanismParams = parameter;

            object lowLevelParams = _mechanismParams.ToMarshalableStructure();
            _ckMechanism = CkmUtils.CreateMechanism(type, lowLevelParams);
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

            object lowLevelParams = _mechanismParams.ToMarshalableStructure();
            _ckMechanism = CkmUtils.CreateMechanism(type, lowLevelParams);
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
                Common.UnmanagedMemory.Free(ref _ckMechanism.Parameter);
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
