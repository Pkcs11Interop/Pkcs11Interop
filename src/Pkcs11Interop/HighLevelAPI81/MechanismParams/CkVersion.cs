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
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.LowLevelAPI81;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism
    /// </summary>
    public class CkVersion : ICkVersion
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Low level mechanism parameters
        /// </summary>
        private CK_VERSION _lowLevelStruct = new CK_VERSION();

        /// <summary>
        /// Major version number (the integer portion of the version)
        /// </summary>
        public byte Major
        {
            get
            {
                return _lowLevelStruct.Major[0];
            }
        }

        /// <summary>
        /// Minor version number (the hundredths portion of the version)
        /// </summary>
        public byte Minor
        {
            get
            {
                return _lowLevelStruct.Minor[0];
            }
        }

        /// <summary>
        /// Initializes a new instance of the CkVersion class.
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        public CkVersion(byte major, byte minor)
        {
            _lowLevelStruct.Major = new byte[] { major };
            _lowLevelStruct.Minor = new byte[] { minor };
        }
        
        #region IMechanismParams
        
        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            return _lowLevelStruct;
        }
        
        #endregion

        /// <summary>
        /// Returns a string that represents the current CkVersion object.
        /// </summary>
        /// <returns>String that represents the current CkVersion object.</returns>
        public override string ToString()
        {
            return _lowLevelStruct.ToString();
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

                _disposed = true;
            }
        }

        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkVersion()
        {
            Dispose(false);
        }

        #endregion
    }
}
