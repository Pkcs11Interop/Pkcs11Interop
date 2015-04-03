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
        /// Platform specific Mechanism
        /// </summary>
        private HighLevelAPI41.Mechanism _mechanism4 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI41.Mechanism Mechanism4
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism4;
            }
        }

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        private HighLevelAPI81.Mechanism _mechanism8 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI81.Mechanism Mechanism8
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism8;
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

                return (Platform.UnmanagedLongSize == 4) ? _mechanism4.Type : _mechanism8.Type;
            }
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI41.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism4 = mechanism;
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI81.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism8 = mechanism;
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(ulong type)
        {
            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type));
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(CKM type)
        {
            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(type);
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(ulong type, byte[] parameter)
        {
            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type, parameter);
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(CKM type, byte[] parameter)
        {
            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(type, parameter);
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type, parameter);
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

            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type, parameter);
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

            if (Platform.UnmanagedLongSize == 4)
                _mechanism4 = new HighLevelAPI41.Mechanism(type, parameter);
            else
                _mechanism8 = new HighLevelAPI81.Mechanism(type, parameter);
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
                    if (_mechanism4 != null)
                    {
                        _mechanism4.Dispose();
                        _mechanism4 = null;
                    }
                          
                    if (_mechanism8 != null)
                    {
                        _mechanism8.Dispose();
                        _mechanism8 = null;
                    }
                }
                
                // Dispose unmanaged objects

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
