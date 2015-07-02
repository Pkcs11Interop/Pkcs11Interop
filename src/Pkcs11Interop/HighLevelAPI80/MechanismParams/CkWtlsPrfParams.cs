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
using Net.Pkcs11Interop.LowLevelAPI80.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI80.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_WTLS_PRF mechanism
    /// </summary>
    public class CkWtlsPrfParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;
        
        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_WTLS_PRF_PARAMS _lowLevelStruct = new CK_WTLS_PRF_PARAMS();
        
        /// <summary>
        /// Output of the operation
        /// </summary>
        public byte[] Output
        {
            get
            {
                if (this._disposed)
                    throw new ObjectDisposedException(this.GetType().FullName);

                int ulongSize = UnmanagedMemory.SizeOf(typeof(ulong));
                byte[] outputLenBytes = UnmanagedMemory.Read(_lowLevelStruct.OutputLen, ulongSize);
                ulong outputLen = ConvertUtils.BytesToULong(outputLenBytes);
                return UnmanagedMemory.Read(_lowLevelStruct.Output, Convert.ToInt32(outputLen));
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the CkWtlsPrfParams class.
        /// </summary>
        /// <param name='digestMechanism'>Digest mechanism to be used (CKM)</param>
        /// <param name='seed'>Input seed</param>
        /// <param name='label'>Identifying label</param>
        /// <param name='outputLen'>Length in bytes that the output to be created shall have</param>
        public CkWtlsPrfParams(ulong digestMechanism, byte[] seed, byte[] label, ulong outputLen)
        {
            _lowLevelStruct.DigestMechanism = 0;
            _lowLevelStruct.Seed = IntPtr.Zero;
            _lowLevelStruct.SeedLen = 0;
            _lowLevelStruct.Label = IntPtr.Zero;
            _lowLevelStruct.LabelLen = 0;
            _lowLevelStruct.Output = IntPtr.Zero;
            _lowLevelStruct.OutputLen = IntPtr.Zero;

            _lowLevelStruct.DigestMechanism = digestMechanism;

            if (seed != null)
            {
                _lowLevelStruct.Seed = UnmanagedMemory.Allocate(seed.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Seed, seed);
                _lowLevelStruct.SeedLen = Convert.ToUInt64(seed.Length);
            }
            
            if (label != null)
            {
                _lowLevelStruct.Label = UnmanagedMemory.Allocate(label.Length);
                UnmanagedMemory.Write(_lowLevelStruct.Label, label);
                _lowLevelStruct.LabelLen = Convert.ToUInt64(label.Length);
            }
            
            if (outputLen < 1)
                throw new ArgumentException("Value has to be positive number", "outputLen");
            
            _lowLevelStruct.Output = UnmanagedMemory.Allocate(Convert.ToInt32(outputLen));
            
            byte[] outputLenBytes = ConvertUtils.ULongToBytes(outputLen);
            _lowLevelStruct.OutputLen = UnmanagedMemory.Allocate(outputLenBytes.Length);
            UnmanagedMemory.Write(_lowLevelStruct.OutputLen, outputLenBytes);
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            return _lowLevelStruct;
        }
        
        #endregion
        
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
                UnmanagedMemory.Free(ref _lowLevelStruct.Seed);
                _lowLevelStruct.SeedLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Label);
                _lowLevelStruct.LabelLen = 0;
                UnmanagedMemory.Free(ref _lowLevelStruct.Output);
                UnmanagedMemory.Free(ref _lowLevelStruct.OutputLen);
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkWtlsPrfParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
