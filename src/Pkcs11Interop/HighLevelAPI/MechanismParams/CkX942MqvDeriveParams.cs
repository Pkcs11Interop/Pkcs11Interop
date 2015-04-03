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

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_X9_42_MQV_DERIVE key derivation mechanism
    /// </summary>
    public class CkX942MqvDeriveParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkX942MqvDeriveParams
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkX942MqvDeriveParams _params4 = null;

        /// <summary>
        /// Platform specific CkX942MqvDeriveParams
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkX942MqvDeriveParams _params8 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkX942MqvDeriveParams class.
        /// </summary>>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='otherInfo'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first X9.42 Diffie-Hellman public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second X9.42 Diffie-Hellman private key</param>
        /// <param name='privateData'>Key handle for second X9.42 Diffie-Hellman private key value</param>
        /// <param name='publicData2'>Other party's second X9.42 Diffie-Hellman public key value</param>
        /// <param name='publicKey'>Handle to the first party's ephemeral public key</param>
        public CkX942MqvDeriveParams(ulong kdf, byte[] otherInfo, byte[] publicData, ulong privateDataLen, ObjectHandle privateData, byte[] publicData2, ObjectHandle publicKey)
        {
            if (Platform.UnmanagedLongSize == 4)
                _params4 = new HighLevelAPI4.MechanismParams.CkX942MqvDeriveParams(Convert.ToUInt32(kdf), otherInfo, publicData, Convert.ToUInt32(privateDataLen), privateData.ObjectHandle4, publicData2, publicKey.ObjectHandle4);
            else
                _params8 = new HighLevelAPI8.MechanismParams.CkX942MqvDeriveParams(kdf, otherInfo, publicData, privateDataLen, privateData.ObjectHandle8, publicData2, publicKey.ObjectHandle8);
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

            if (Platform.UnmanagedLongSize == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
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
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects
                
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkX942MqvDeriveParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
