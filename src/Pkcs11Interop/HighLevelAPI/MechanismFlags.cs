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
    /// Flags specifying mechanism capabilities
    /// </summary>
    public class MechanismFlags
    {
        /// <summary>
        /// Platform specific MechanismFlags
        /// </summary>
        private HighLevelAPI4.MechanismFlags _mechanismFlags4 = null;

        /// <summary>
        /// Platform specific MechanismFlags
        /// </summary>
        private HighLevelAPI8.MechanismFlags _mechanismFlags8 = null;

        /// <summary>
        /// Bits flags specifying mechanism capabilities
        /// </summary>
        public ulong Flags
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Flags : _mechanismFlags8.Flags;
            }
        }

        /// <summary>
        /// True if the mechanism is performed by the device; false if the mechanism is performed in software
        /// </summary>
        public bool Hw
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Hw : _mechanismFlags8.Hw;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_EncryptInit
        /// </summary>
        public bool Encrypt
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Encrypt : _mechanismFlags8.Encrypt;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DecryptInit
        /// </summary>
        public bool Decrypt
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Decrypt : _mechanismFlags8.Decrypt;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DigestInit
        /// </summary>
        public bool Digest
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Digest : _mechanismFlags8.Digest;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignInit
        /// </summary>
        public bool Sign
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Sign : _mechanismFlags8.Sign;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignRecoverInit
        /// </summary>
        public bool SignRecover
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.SignRecover : _mechanismFlags8.SignRecover;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyInit
        /// </summary>
        public bool Verify
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Verify : _mechanismFlags8.Verify;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyRecoverInit
        /// </summary>
        public bool VerifyRecover
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.VerifyRecover : _mechanismFlags8.VerifyRecover;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKey
        /// </summary>
        public bool Generate
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Generate : _mechanismFlags8.Generate;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKeyPair
        /// </summary>
        public bool GenerateKeyPair
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.GenerateKeyPair : _mechanismFlags8.GenerateKeyPair;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_WrapKey
        /// </summary>
        public bool Wrap
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Wrap : _mechanismFlags8.Wrap;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_UnwrapKey
        /// </summary>
        public bool Unwrap
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Unwrap : _mechanismFlags8.Unwrap;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DeriveKey
        /// </summary>
        public bool Derive
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Derive : _mechanismFlags8.Derive;
            }
        }

        /// <summary>
        /// True if there is an extension to the flags; false if no extensions.
        /// </summary>
        public bool Extension
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.Extension : _mechanismFlags8.Extension;
            }
        }

        #region Elliptic Curve

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over Fp
        /// </summary>
        public bool EcFp
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcFp : _mechanismFlags8.EcFp;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over F2m
        /// </summary>
        public bool EcF2m
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcF2m : _mechanismFlags8.EcF2m;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice ecParameters
        /// </summary>
        public bool EcEcParameters
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcEcParameters : _mechanismFlags8.EcEcParameters;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice namedCurve
        /// </summary>
        public bool EcNamedCurve
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcNamedCurve : _mechanismFlags8.EcNamedCurve;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point uncompressed
        /// </summary>
        public bool EcUncompress
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcUncompress : _mechanismFlags8.EcUncompress;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point compressed
        /// </summary>
        public bool EcCompress
        {
            get
            {
                return (UnmanagedLong.Size == 4) ? _mechanismFlags4.EcCompress : _mechanismFlags8.EcCompress;
            }
        }

        #endregion

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI4.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags4 = mechanismFlags;
        }

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI8.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags8 = mechanismFlags;
        }
    }
}
