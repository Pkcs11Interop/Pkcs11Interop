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
        private HighLevelAPI40.MechanismFlags _mechanismFlags40 = null;

        /// <summary>
        /// Platform specific MechanismFlags
        /// </summary>
        private HighLevelAPI41.MechanismFlags _mechanismFlags41 = null;

        /// <summary>
        /// Platform specific MechanismFlags
        /// </summary>
        private HighLevelAPI80.MechanismFlags _mechanismFlags80 = null;

        /// <summary>
        /// Platform specific MechanismFlags
        /// </summary>
        private HighLevelAPI81.MechanismFlags _mechanismFlags81 = null;

        /// <summary>
        /// Bits flags specifying mechanism capabilities
        /// </summary>
        public ulong Flags
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Flags : _mechanismFlags41.Flags;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Flags : _mechanismFlags81.Flags;
            }
        }

        /// <summary>
        /// True if the mechanism is performed by the device; false if the mechanism is performed in software
        /// </summary>
        public bool Hw
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Hw : _mechanismFlags41.Hw;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Hw : _mechanismFlags81.Hw;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_EncryptInit
        /// </summary>
        public bool Encrypt
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Encrypt : _mechanismFlags41.Encrypt;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Encrypt : _mechanismFlags81.Encrypt;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DecryptInit
        /// </summary>
        public bool Decrypt
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Decrypt : _mechanismFlags41.Decrypt;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Decrypt : _mechanismFlags81.Decrypt;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DigestInit
        /// </summary>
        public bool Digest
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Digest : _mechanismFlags41.Digest;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Digest : _mechanismFlags81.Digest;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignInit
        /// </summary>
        public bool Sign
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Sign : _mechanismFlags41.Sign;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Sign : _mechanismFlags81.Sign;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_SignRecoverInit
        /// </summary>
        public bool SignRecover
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.SignRecover : _mechanismFlags41.SignRecover;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.SignRecover : _mechanismFlags81.SignRecover;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyInit
        /// </summary>
        public bool Verify
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Verify : _mechanismFlags41.Verify;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Verify : _mechanismFlags81.Verify;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_VerifyRecoverInit
        /// </summary>
        public bool VerifyRecover
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.VerifyRecover : _mechanismFlags41.VerifyRecover;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.VerifyRecover : _mechanismFlags81.VerifyRecover;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKey
        /// </summary>
        public bool Generate
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Generate : _mechanismFlags41.Generate;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Generate : _mechanismFlags81.Generate;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_GenerateKeyPair
        /// </summary>
        public bool GenerateKeyPair
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.GenerateKeyPair : _mechanismFlags41.GenerateKeyPair;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.GenerateKeyPair : _mechanismFlags81.GenerateKeyPair;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_WrapKey
        /// </summary>
        public bool Wrap
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Wrap : _mechanismFlags41.Wrap;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Wrap : _mechanismFlags81.Wrap;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_UnwrapKey
        /// </summary>
        public bool Unwrap
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Unwrap : _mechanismFlags41.Unwrap;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Unwrap : _mechanismFlags81.Unwrap;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with C_DeriveKey
        /// </summary>
        public bool Derive
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Derive : _mechanismFlags41.Derive;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Derive : _mechanismFlags81.Derive;
            }
        }

        /// <summary>
        /// True if there is an extension to the flags; false if no extensions.
        /// </summary>
        public bool Extension
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.Extension : _mechanismFlags41.Extension;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.Extension : _mechanismFlags81.Extension;
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
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcFp : _mechanismFlags41.EcFp;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcFp : _mechanismFlags81.EcFp;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters over F2m
        /// </summary>
        public bool EcF2m
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcF2m : _mechanismFlags41.EcF2m;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcF2m : _mechanismFlags81.EcF2m;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice ecParameters
        /// </summary>
        public bool EcEcParameters
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcEcParameters : _mechanismFlags41.EcEcParameters;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcEcParameters : _mechanismFlags81.EcEcParameters;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with EC domain parameters of the choice namedCurve
        /// </summary>
        public bool EcNamedCurve
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcNamedCurve : _mechanismFlags41.EcNamedCurve;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcNamedCurve : _mechanismFlags81.EcNamedCurve;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point uncompressed
        /// </summary>
        public bool EcUncompress
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcUncompress : _mechanismFlags41.EcUncompress;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcUncompress : _mechanismFlags81.EcUncompress;
            }
        }

        /// <summary>
        /// True if the mechanism can be used with elliptic curve point compressed
        /// </summary>
        public bool EcCompress
        {
            get
            {
                if (Platform.UnmanagedLongSize == 4)
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags40.EcCompress : _mechanismFlags41.EcCompress;
                else
                    return (Platform.StructPackingSize == 0) ? _mechanismFlags80.EcCompress : _mechanismFlags81.EcCompress;
            }
        }

        #endregion

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI40.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags40 = mechanismFlags;
        }

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI41.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags41 = mechanismFlags;
        }

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI80.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags80 = mechanismFlags;
        }

        /// <summary>
        /// Converts platform specific MechanismFlags to platfrom neutral MechanismFlags
        /// </summary>
        /// <param name="mechanismFlags">Platform specific MechanismFlags</param>
        internal MechanismFlags(HighLevelAPI81.MechanismFlags mechanismFlags)
        {
            if (mechanismFlags == null)
                throw new ArgumentNullException("mechanismFlags");

            _mechanismFlags81 = mechanismFlags;
        }
    }
}
