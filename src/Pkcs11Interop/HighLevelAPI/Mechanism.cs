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
        private HighLevelAPI40.Mechanism _mechanism40 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI40.Mechanism Mechanism40
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism40;
            }
        }

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        private HighLevelAPI41.Mechanism _mechanism41 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI41.Mechanism Mechanism41
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism41;
            }
        }

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        private HighLevelAPI80.Mechanism _mechanism80 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI80.Mechanism Mechanism80
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism80;
            }
        }

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        private HighLevelAPI81.Mechanism _mechanism81 = null;

        /// <summary>
        /// Platform specific Mechanism
        /// </summary>
        internal HighLevelAPI81.Mechanism Mechanism81
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                return _mechanism81;
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

                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanism40.Type : _mechanism41.Type;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanism80.Type : _mechanism81.Type;
            }
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI40.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism40 = mechanism;
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI41.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism41 = mechanism;
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI80.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism80 = mechanism;
        }

        /// <summary>
        /// Converts platform specific Mechanism to platfrom neutral Mechanism
        /// </summary>
        /// <param name="mechanism">Platform specific Mechanism</param>
        internal Mechanism(HighLevelAPI81.Mechanism mechanism)
        {
            if (mechanism == null)
                throw new ArgumentNullException("mechanism");

            _mechanism81 = mechanism;
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(ulong type)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type));
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type);
            }
        }

        /// <summary>
        /// Creates mechanism of given type with no parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        public Mechanism(CKM type)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type));
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type));
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type);
            }
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(ulong type, byte[] parameter)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type), parameter);
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type, parameter);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type, parameter);
            }
        }

        /// <summary>
        /// Creates mechanism of given type with byte array parameter
        /// </summary>
        /// <param name="type">Mechanism type</param>
        /// <param name="parameter">Mechanism parameter</param>
        public Mechanism(CKM type, byte[] parameter)
        {
            if (Platform.UnmanagedLongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type), parameter);
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type, parameter);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type, parameter);
            }
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
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type), parameter);
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type, parameter);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type, parameter);
            }
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
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism40 = new HighLevelAPI40.Mechanism(Convert.ToUInt32(type), parameter);
                else
                    _mechanism41 = new HighLevelAPI41.Mechanism(Convert.ToUInt32(type), parameter);
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                    _mechanism80 = new HighLevelAPI80.Mechanism(type, parameter);
                else
                    _mechanism81 = new HighLevelAPI81.Mechanism(type, parameter);
            }
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
                    if (_mechanism40 != null)
                    {
                        _mechanism40.Dispose();
                        _mechanism40 = null;
                    }

                    if (_mechanism41 != null)
                    {
                        _mechanism41.Dispose();
                        _mechanism41 = null;
                    }

                    if (_mechanism80 != null)
                    {
                        _mechanism80.Dispose();
                        _mechanism80 = null;
                    }

                    if (_mechanism81 != null)
                    {
                        _mechanism81.Dispose();
                        _mechanism81 = null;
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
