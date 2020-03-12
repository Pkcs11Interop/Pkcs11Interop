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

using System.Collections.Generic;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.HighLevelAPI.Factories;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;
using Net.Pkcs11Interop.HighLevelAPI81.MechanismParams;

// Note: Code in this file is generated automatically.

namespace Net.Pkcs11Interop.HighLevelAPI81.Factories
{
    /// <summary>
    /// Factory for creation of IMechanismParams instances
    /// </summary>
    public class MechanismParamsFactory : IMechanismParamsFactory
    {
        /// <summary>
        /// Creates parameters for the CKM_AES_CBC_ENCRYPT_DATA mechanism
        /// </summary>
        /// <param name='iv'>IV value (16 bytes)</param>
        /// <param name='data'>Data value part that must be a multiple of 16 bytes long</param>
        /// <returns>Parameters for the CKM_AES_CBC_ENCRYPT_DATA mechanism</returns>
        public ICkAesCbcEncryptDataParams CreateCkAesCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            return new CkAesCbcEncryptDataParams(iv, data);
        }

        /// <summary>
        /// Creates parameters for the CKM_AES_CTR mechanism
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        /// <returns>Parameters for the CKM_AES_CTR mechanism</returns>
        public ICkAesCtrParams CreateCkAesCtrParams(ulong counterBits, byte[] cb)
        {
            return new CkAesCtrParams(ConvertUtils.UInt64FromUInt64(counterBits), cb);
        }

        /// <summary>
        /// Creates parameters for the CKM_ARIA_CBC_ENCRYPT_DATA mechanism
        /// </summary>
        /// <param name='iv'>IV value (16 bytes)</param>
        /// <param name='data'>Data to encrypt</param>
        /// <returns>Parameters for the CKM_ARIA_CBC_ENCRYPT_DATA mechanism</returns>
        public ICkAriaCbcEncryptDataParams CreateCkAriaCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            return new CkAriaCbcEncryptDataParams(iv, data);
        }

        /// <summary>
        /// Creates parameters for the CKM_CAMELLIA_CBC_ENCRYPT_DATA mechanism
        /// </summary>
        /// <param name='iv'>IV value (16 bytes)</param>
        /// <param name='data'>Data to encrypt</param>
        /// <returns>Parameters for the CKM_CAMELLIA_CBC_ENCRYPT_DATA mechanism</returns>
        public ICkCamelliaCbcEncryptDataParams CreateCkCamelliaCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            return new CkCamelliaCbcEncryptDataParams(iv, data);
        }

        /// <summary>
        /// Creates parameters for the CKM_CAMELLIA_CTR mechanism
        /// </summary>
        /// <param name='counterBits'>The number of bits in the counter block (cb) that shall be incremented</param>
        /// <param name='cb'>Specifies the counter block (16 bytes)</param>
        /// <returns>Parameters for the CKM_CAMELLIA_CTR mechanism</returns>
        public ICkCamelliaCtrParams CreateCkCamelliaCtrParams(ulong counterBits, byte[] cb)
        {
            return new CkCamelliaCtrParams(ConvertUtils.UInt64FromUInt64(counterBits), cb);
        }

        /// <summary>
        /// Creates parameters for the CKM_AES_CCM mechanism
        /// </summary>
        /// <param name="dataLen">Length of the data</param>
        /// <param name="nonce">Nonce</param>
        /// <param name="aad">Additional authentication data</param>
        /// <param name="macLen">Length of the MAC (output following cipher text) in bytes</param>
        /// <returns>Parameters for the CKM_AES_CCM mechanism</returns>
        public ICkCcmParams CreateCkCcmParams(ulong dataLen, byte[] nonce, byte[] aad, ulong macLen)
        {
            return new CkCcmParams(ConvertUtils.UInt64FromUInt64(dataLen), nonce, aad, ConvertUtils.UInt64FromUInt64(macLen));
        }

        /// <summary>
        /// Creates parameters for the CKM_CMS_SIG mechanism
        /// </summary>
        /// <param name='certificateHandle'>Object handle for a certificate associated with the signing key</param>
        /// <param name='signingMechanism'>Mechanism to use when signing a constructed CMS SignedAttributes value</param>
        /// <param name='digestMechanism'>Mechanism to use when digesting the data</param>
        /// <param name='contentType'>String indicating complete MIME Content-type of message to be signed or null if the message is a MIME object</param>
        /// <param name='requestedAttributes'>DER-encoded list of CMS Attributes the caller requests to be included in the signed attributes</param>
        /// <param name='requiredAttributes'>DER-encoded list of CMS Attributes (with accompanying values) required to be included in the resulting signed attributes</param>
        /// <returns>Parameters for the CKM_CMS_SIG mechanism</returns>
        public ICkCmsSigParams CreateCkCmsSigParams(IObjectHandle certificateHandle, ulong? signingMechanism, ulong? digestMechanism, string contentType, byte[] requestedAttributes, byte[] requiredAttributes)
        {
            return new CkCmsSigParams(certificateHandle, ConvertUtils.UInt64FromUInt64(signingMechanism), ConvertUtils.UInt64FromUInt64(digestMechanism), contentType, requestedAttributes, requiredAttributes);
        }

        /// <summary>
        /// Creates parameters for the CKM_DES_CBC_ENCRYPT_DATA and CKM_DES3_CBC_ENCRYPT_DATA mechanisms
        /// </summary>
        /// <param name='iv'>IV value (8 bytes)</param>
        /// <param name='data'>Data to encrypt</param>
        /// <returns>Parameters for the CKM_DES_CBC_ENCRYPT_DATA and CKM_DES3_CBC_ENCRYPT_DATA mechanisms</returns>
        public ICkDesCbcEncryptDataParams CreateCkDesCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            return new CkDesCbcEncryptDataParams(iv, data);
        }

        /// <summary>
        /// Creates parameters for the CKM_DSA_PROBABLISTIC_PARAMETER_GEN, CKM_DSA_SHAWE_TAYLOR_PARAMETER_GEN a CKM_DSA_FIPS_G_GEN mechanisms
        /// </summary>
        /// <param name="hash">Mechanism value for the base hash used in PQG generation (CKM)</param>
        /// <param name="seed">Seed value used to generate PQ and G</param>
        /// <param name="index">Index value for generating G</param>
        /// <returns>Parameters for the CKM_DSA_PROBABLISTIC_PARAMETER_GEN, CKM_DSA_SHAWE_TAYLOR_PARAMETER_GEN a CKM_DSA_FIPS_G_GEN mechanisms</returns>
        public ICkDsaParameterGenParam CreateCkDsaParameterGenParam(ulong hash, byte[] seed, ulong index)
        {
            return new CkDsaParameterGenParam(ConvertUtils.UInt64FromUInt64(hash), seed, ConvertUtils.UInt64FromUInt64(index));
        }

        /// <summary>
        /// Creates parameters for the CKM_ECDH1_DERIVE and CKM_ECDH1_COFACTOR_DERIVE key derivation mechanisms
        /// </summary>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='sharedData'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's EC public key value</param>
        /// <returns>Parameters for the CKM_ECDH1_DERIVE and CKM_ECDH1_COFACTOR_DERIVE key derivation mechanisms</returns>
        public ICkEcdh1DeriveParams CreateCkEcdh1DeriveParams(ulong kdf, byte[] sharedData, byte[] publicData)
        {
            return new CkEcdh1DeriveParams(ConvertUtils.UInt64FromUInt64(kdf), sharedData, publicData);
        }

        /// <summary>
        /// Creates parameters for the CKM_ECMQV_DERIVE mechanism
        /// </summary>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='sharedData'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first EC public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second EC private key</param>
        /// <param name='privateData'>Key handle for second EC private key value</param>
        /// <param name='publicData2'>Other party's second EC public key value</param>
        /// <returns>Parameters for the CKM_ECMQV_DERIVE mechanism</returns>
        public ICkEcdh2DeriveParams CreateCkEcdh2DeriveParams(ulong kdf, byte[] sharedData, byte[] publicData, ulong privateDataLen, IObjectHandle privateData, byte[] publicData2)
        {
            return new CkEcdh2DeriveParams(ConvertUtils.UInt64FromUInt64(kdf), sharedData, publicData, ConvertUtils.UInt64FromUInt64(privateDataLen), privateData, publicData2);
        }

        /// <summary>
        /// Creates parameters for the CKM_ECDH_AES_KEY_WRAP mechanism
        /// </summary>
        /// <param name="aesKeyBits">Length of the temporary AES key in bits</param>
        /// <param name="kdf">Key derivation function used on the shared secret value to generate AES key (CKD)</param>
        /// <param name="sharedData">Data shared between the two parties</param>
        /// <returns>Parameters for the CKM_ECDH_AES_KEY_WRAP mechanism</returns>
        public ICkEcdhAesKeyWrapParams CreateCkEcdhAesKeyWrapParams(ulong aesKeyBits, ulong kdf, byte[] sharedData)
        {
            return new CkEcdhAesKeyWrapParams(ConvertUtils.UInt64FromUInt64(aesKeyBits), ConvertUtils.UInt64FromUInt64(kdf), sharedData);
        }

        /// <summary>
        /// Create parameters for the CKM_ECMQV_DERIVE mechanism
        /// </summary>>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='sharedData'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first EC public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second EC private key</param>
        /// <param name='privateData'>Key handle for second EC private key value</param>
        /// <param name='publicData2'>Other party's second EC public key value</param>
        /// <param name='publicKey'>Handle to the first party's ephemeral public key</param>
        /// <returns>Parameters for the CKM_ECMQV_DERIVE mechanism</returns>
        public ICkEcmqvDeriveParams CreateCkEcmqvDeriveParams(ulong kdf, byte[] sharedData, byte[] publicData, ulong privateDataLen, IObjectHandle privateData, byte[] publicData2, IObjectHandle publicKey)
        {
            return new CkEcmqvDeriveParams(ConvertUtils.UInt64FromUInt64(kdf), sharedData, publicData, ConvertUtils.UInt64FromUInt64(privateDataLen), privateData, publicData2, publicKey);
        }

        /// <summary>
        /// Creates parameters for the CKM_EXTRACT_KEY_FROM_KEY mechanism
        /// </summary>
        /// <param name='bit'>Specifies which bit of the base key should be used as the first bit of the derived key</param>
        /// <returns>Parameters for the CKM_EXTRACT_KEY_FROM_KEY mechanism</returns>
        public ICkExtractParams CreateCkExtractParams(ulong bit)
        {
            return new CkExtractParams(ConvertUtils.UInt64FromUInt64(bit));
        }

        /// <summary>
        /// Creates parameters for the CKM_AES_GCM mechanism
        /// </summary>
        /// <param name="iv">Initialization vector</param>
        /// <param name="ivBits">Member is defined in PKCS#11 v2.40e1 headers but the description is not present in the specification</param>
        /// <param name="aad">Additional authentication data</param>
        /// <param name="tagBits">Length of authentication tag (output following cipher text) in bits</param>
        /// <returns>Parameters for the CKM_AES_GCM mechanism</returns>
        public ICkGcmParams CreateCkGcmParams(byte[] iv, ulong ivBits, byte[] aad, ulong tagBits)
        {
            return new CkGcmParams(iv, ConvertUtils.UInt64FromUInt64(ivBits), aad, ConvertUtils.UInt64FromUInt64(tagBits));
        }

        /// <summary>
        /// Creates parameters for the CKM_GOSTR3410_DERIVE mechanism
        /// </summary>
        /// <param name="kdf">Additional key diversification algorithm (CKD)</param>
        /// <param name="publicData">Data with public key of a receiver</param>
        /// <param name="ukm">UKM data</param>
        /// <returns>Parameters for the CKM_GOSTR3410_DERIVE mechanism</returns>
        public ICkGostR3410DeriveParams CreateCkGostR3410DeriveParams(ulong kdf, byte[] publicData, byte[] ukm)
        {
            return new CkGostR3410DeriveParams(ConvertUtils.UInt64FromUInt64(kdf), publicData, ukm);
        }

        /// <summary>
        /// Creates parameters for the CKM_GOSTR3410_KEY_WRAP mechanism
        /// </summary>
        /// <param name="wrapOID">Data with DER-encoding of the object identifier indicating the data object type of GOST 28147-89</param>
        /// <param name="ukm">Data with UKM</param>
        /// <param name="key">Key handle of a sender for wrapping operation or key handle of a receiver for unwrapping operation</param>
        /// <returns>Parameters for the CKM_GOSTR3410_KEY_WRAP mechanism</returns>
        public ICkGostR3410KeyWrapParams CreateCkGostR3410KeyWrapParams(byte[] wrapOID, byte[] ukm, ulong key)
        {
            return new CkGostR3410KeyWrapParams(wrapOID, ukm, ConvertUtils.UInt64FromUInt64(key));
        }

        /// <summary>
        /// Creates parameters for the CKM_KEA_DERIVE mechanism
        /// </summary>
        /// <param name='isSender'>Option for generating the key (called a TEK). True if the sender (originator) generates the TEK, false if the recipient is regenerating the TEK.</param>
        /// <param name='randomA'>Ra data</param>
        /// <param name='randomB'>Rb data</param>
        /// <param name='publicData'>Other party's KEA public key value</param>
        /// <returns>Parameters for the CKM_KEA_DERIVE mechanism</returns>
        public ICkKeaDeriveParams CreateCkKeaDeriveParams(bool isSender, byte[] randomA, byte[] randomB, byte[] publicData)
        {
            return new CkKeaDeriveParams(isSender, randomA, randomB, publicData);
        }

        /// <summary>
        /// Creates parameters for the CKM_CONCATENATE_BASE_AND_DATA, CKM_CONCATENATE_DATA_AND_BASE and CKM_XOR_BASE_AND_DATA mechanisms
        /// </summary>
        /// <param name='data'>Byte string used as the input for derivation mechanism</param>
        /// <returns>Parameters for the CKM_CONCATENATE_BASE_AND_DATA, CKM_CONCATENATE_DATA_AND_BASE and CKM_XOR_BASE_AND_DATA mechanisms</returns>
        public ICkKeyDerivationStringData CreateCkKeyDerivationStringData(byte[] data)
        {
            return new CkKeyDerivationStringData(data);
        }

        /// <summary>
        /// Creates parameters for the CKM_KEY_WRAP_SET_OAEP mechanism
        /// </summary>
        /// <param name='bc'>Block contents byte</param>
        /// <param name='x'>Concatenation of hash of plaintext data (if present) and extra data (if present)</param>
        /// <returns>Parameters for the CKM_KEY_WRAP_SET_OAEP mechanism</returns>
        public ICkKeyWrapSetOaepParams CreateCkKeyWrapSetOaepParams(byte bc, byte[] x)
        {
            return new CkKeyWrapSetOaepParams(bc, x);
        }

        /// <summary>
        /// Creates parameters for the CKM_KIP_DERIVE, CKM_KIP_WRAP and CKM_KIP_MAC mechanisms
        /// </summary>
        /// <param name='mechanism'>Underlying cryptographic mechanism (CKM)</param>
        /// <param name='key'>Handle to a key that will contribute to the entropy of the derived key (CKM_KIP_DERIVE) or will be used in the MAC operation (CKM_KIP_MAC)</param>
        /// <param name='seed'>Input seed</param>
        /// <returns>Parameters for the CKM_KIP_DERIVE, CKM_KIP_WRAP and CKM_KIP_MAC mechanisms</returns>
        public ICkKipParams CreateCkKipParams(ulong? mechanism, IObjectHandle key, byte[] seed)
        {
            return new CkKipParams(ConvertUtils.UInt64FromUInt64(mechanism), key, seed);
        }

        /// <summary>
        /// Creates parameters for the general-length MACing mechanisms (DES, DES3, CAST, CAST3, CAST128 (CAST5), IDEA, CDMF and AES), the general length HMACing mechanisms (MD2, MD5, SHA-1, SHA-256, SHA-384, SHA-512, RIPEMD-128 and RIPEMD-160) and the two SSL 3.0 MACing mechanisms (MD5 and SHA-1)
        /// </summary>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        /// <returns>Parameters for the general-length MACing mechanisms (DES, DES3, CAST, CAST3, CAST128 (CAST5), IDEA, CDMF and AES), the general length HMACing mechanisms (MD2, MD5, SHA-1, SHA-256, SHA-384, SHA-512, RIPEMD-128 and RIPEMD-160) and the two SSL 3.0 MACing mechanisms (MD5 and SHA-1)</returns>
        public ICkMacGeneralParams CreateCkMacGeneralParams(ulong macLength)
        {
            return new CkMacGeneralParams(ConvertUtils.UInt64FromUInt64(macLength));
        }

        /// <summary>
        /// Creates type, value and length of an OTP parameter
        /// </summary>
        /// <param name='type'>Parameter type</param>
        /// <param name='value'>Value of the parameter</param>
        /// <returns>Type, value and length of an OTP parameter</returns>
        public ICkOtpParam CreateCkOtpParam(ulong type, byte[] value)
        {
            return new CkOtpParam(ConvertUtils.UInt64FromUInt64(type), value);
        }

        /// <summary>
        /// Creates parameters for OTP mechanisms in a generic fashion
        /// </summary>
        /// <param name='parameters'>List of OTP parameters</param>
        /// <returns>Parameters for OTP mechanisms in a generic fashion</returns>
        public ICkOtpParams CreateCkOtpParams(List<ICkOtpParam> parameters)
        {
            return new CkOtpParams(parameters);
        }

        /// <summary>
        /// Creates parameters returned by all OTP mechanisms in successful calls to Sign method
        /// </summary>
        /// <param name='signature'>Signature value returned by all OTP mechanisms in successful calls to Sign method</param>
        /// <returns>Parameters returned by all OTP mechanisms in successful calls to Sign method</returns>
        public ICkOtpSignatureInfo CreateCkOtpSignatureInfo(byte[] signature)
        {
            return new CkOtpSignatureInfo(signature);
        }

        /// <summary>
        /// Creates parameters for the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism
        /// </summary>
        /// <param name='initVector'>8-byte initialization vector (IV), if an IV is required</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <param name='salt'>Salt to be used in the PBE key generation</param>
        /// <param name='iteration'>Number of iterations required for the generation</param>
        /// <returns>Parameters for the CKM_PBE mechanisms and the CKM_PBA_SHA1_WITH_SHA1_HMAC mechanism</returns>
        public ICkPbeParams CreateCkPbeParams(byte[] initVector, byte[] password, byte[] salt, ulong iteration)
        {
            return new CkPbeParams(initVector, password, salt, ConvertUtils.UInt64FromUInt64(iteration));
        }

        /// <summary>
        /// Creates parameters for the CKM_PKCS5_PBKD2 mechanism
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <returns>Parameters for the CKM_PKCS5_PBKD2 mechanism</returns>
        public ICkPkcs5Pbkd2Params CreateCkPkcs5Pbkd2Params(ulong saltSource, byte[] saltSourceData, ulong iterations, ulong prf, byte[] prfData, byte[] password)
        {
            return new CkPkcs5Pbkd2Params(ConvertUtils.UInt64FromUInt64(saltSource), saltSourceData, ConvertUtils.UInt64FromUInt64(iterations), ConvertUtils.UInt64FromUInt64(prf), prfData, password);
        }

        /// <summary>
        /// Creates parameters for the CKM_PKCS5_PBKD2 mechanism
        /// </summary>
        /// <param name='saltSource'>Source of the salt value (CKZ)</param>
        /// <param name='saltSourceData'>Data used as the input for the salt source</param>
        /// <param name='iterations'>Number of iterations to perform when generating each block of random data</param>
        /// <param name='prf'>Pseudo-random function to used to generate the key (CKP)</param>
        /// <param name='prfData'>Data used as the input for PRF in addition to the salt value</param>
        /// <param name='password'>Password to be used in the PBE key generation</param>
        /// <returns>Parameters for the CKM_PKCS5_PBKD2 mechanism</returns>
        public ICkPkcs5Pbkd2Params2 CreateCkPkcs5Pbkd2Params2(ulong saltSource, byte[] saltSourceData, ulong iterations, ulong prf, byte[] prfData, byte[] password)
        {
            return new CkPkcs5Pbkd2Params2(ConvertUtils.UInt64FromUInt64(saltSource), saltSourceData, ConvertUtils.UInt64FromUInt64(iterations), ConvertUtils.UInt64FromUInt64(prf), prfData, password);
        }

        /// <summary>
        /// Creates parameters for the CKM_RC2_CBC and CKM_RC2_CBC_PAD mechanisms
        /// </summary>
        /// <param name='effectiveBits'>The effective number of bits in the RC2 search space</param>
        /// <param name='iv'>The initialization vector (IV) for cipher block chaining mode</param>
        /// <returns>Parameters for the CKM_RC2_CBC and CKM_RC2_CBC_PAD mechanisms</returns>
        public ICkRc2CbcParams CreateCkRc2CbcParams(ulong effectiveBits, byte[] iv)
        {
            return new CkRc2CbcParams(ConvertUtils.UInt64FromUInt64(effectiveBits), iv);
        }

        /// <summary>
        /// Creates parameters for the CKM_RC2_MAC_GENERAL mechanism
        /// </summary>
        /// <param name='effectiveBits'>The effective number of bits in the RC2 search space</param>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        /// <returns>Parameters for the CKM_RC2_MAC_GENERAL mechanism</returns>
        public ICkRc2MacGeneralParams CreateCkRc2MacGeneralParams(ulong effectiveBits, ulong macLength)
        {
            return new CkRc2MacGeneralParams(ConvertUtils.UInt64FromUInt64(effectiveBits), ConvertUtils.UInt64FromUInt64(macLength));
        }

        /// <summary>
        /// Creates parameters for the CKM_RC2_ECB and CKM_RC2_MAC mechanisms
        /// </summary>
        /// <param name='effectiveBits'>Effective number of bits in the RC2 search space</param>
        /// <returns>Parameters for the CKM_RC2_ECB and CKM_RC2_MAC mechanisms</returns>
        public ICkRc2Params CreateCkRc2Params(ulong effectiveBits)
        {
            return new CkRc2Params(ConvertUtils.UInt64FromUInt64(effectiveBits));
        }

        /// <summary>
        /// Creates parameters for the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <param name='iv'>Initialization vector (IV) for CBC encryption</param>
        /// <returns>Parameters for the CKM_RC5_CBC and CKM_RC5_CBC_PAD mechanisms</returns>
        public ICkRc5CbcParams CreateCkRc5CbcParams(ulong wordsize, ulong rounds, byte[] iv)
        {
            return new CkRc5CbcParams(ConvertUtils.UInt64FromUInt64(wordsize), ConvertUtils.UInt64FromUInt64(rounds), iv);
        }

        /// <summary>
        /// Creates parameters for the CKM_RC5_MAC_GENERAL mechanism
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <param name='macLength'>Length of the MAC produced, in bytes</param>
        /// <returns>Parameters for the CKM_RC5_MAC_GENERAL mechanism</returns>
        public ICkRc5MacGeneralParams CreateCkRc5MacGeneralParams(ulong wordsize, ulong rounds, ulong macLength)
        {
            return new CkRc5MacGeneralParams(ConvertUtils.UInt64FromUInt64(wordsize), ConvertUtils.UInt64FromUInt64(rounds), ConvertUtils.UInt64FromUInt64(macLength));
        }

        /// <summary>
        /// Creates parameters for the CKM_RC5_ECB and CKM_RC5_MAC mechanisms
        /// </summary>
        /// <param name='wordsize'>Wordsize of RC5 cipher in bytes</param>
        /// <param name='rounds'>Number of rounds of RC5 encipherment</param>
        /// <returns>Parameters for the CKM_RC5_ECB and CKM_RC5_MAC mechanisms</returns>
        public ICkRc5Params CreateCkRc5Params(ulong wordsize, ulong rounds)
        {
            return new CkRc5Params(ConvertUtils.UInt64FromUInt64(wordsize), ConvertUtils.UInt64FromUInt64(rounds));
        }

        /// <summary>
        /// Creates parameters for the CKM_RSA_AES_KEY_WRAP mechanism
        /// </summary>
        /// <param name='aesKeyBits'>Length of the temporary AES key in bits</param>
        /// <param name='oaepParams'>Parameters of the temporary AES key wrapping</param>
        /// <returns>Parameters for the CKM_RSA_AES_KEY_WRAP mechanism</returns>
        public ICkRsaAesKeyWrapParams CreateCkRsaAesKeyWrapParams(ulong aesKeyBits, ICkRsaPkcsOaepParams oaepParams)
        {
            return new CkRsaAesKeyWrapParams(ConvertUtils.UInt64FromUInt64(aesKeyBits), oaepParams);
        }

        /// <summary>
        /// Creates parameters for the CKM_RSA_PKCS_OAEP mechanism
        /// </summary>
        /// <param name='hashAlg'>Mechanism ID of the message digest algorithm used to calculate the digest of the encoding parameter (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='source'>Source of the encoding parameter (CKZ)</param>
        /// <param name='sourceData'>Data used as the input for the encoding parameter source</param>
        /// <returns>Parameters for the CKM_RSA_PKCS_OAEP mechanism</returns>
        public ICkRsaPkcsOaepParams CreateCkRsaPkcsOaepParams(ulong hashAlg, ulong mgf, ulong source, byte[] sourceData)
        {
            return new CkRsaPkcsOaepParams(ConvertUtils.UInt64FromUInt64(hashAlg), ConvertUtils.UInt64FromUInt64(mgf), ConvertUtils.UInt64FromUInt64(source), sourceData);
        }

        /// <summary>
        /// Creates parameters for the CKM_RSA_PKCS_PSS mechanism
        /// </summary>
        /// <param name='hashAlg'>Hash algorithm used in the PSS encoding (CKM)</param>
        /// <param name='mgf'>Mask generation function to use on the encoded block (CKG)</param>
        /// <param name='len'>Length, in bytes, of the salt value used in the PSS encoding</param>
        /// <returns>Parameters for the CKM_RSA_PKCS_PSS mechanism</returns>
        public ICkRsaPkcsPssParams CreateCkRsaPkcsPssParams(ulong hashAlg, ulong mgf, ulong len)
        {
            return new CkRsaPkcsPssParams(ConvertUtils.UInt64FromUInt64(hashAlg), ConvertUtils.UInt64FromUInt64(mgf), ConvertUtils.UInt64FromUInt64(len));
        }

        /// <summary>
        /// Creates parameters for the CKM_SEED_CBC_ENCRYPT_DATA mechanism
        /// </summary>
        /// <param name='iv'>IV value (16 bytes)</param>
        /// <param name='data'>Data value part that must be a multiple of 16 bytes long</param>
        /// <returns>Parameters for the CKM_SEED_CBC_ENCRYPT_DATA mechanism</returns>
        public ICkSeedCbcEncryptDataParams CreateCkSeedCbcEncryptDataParams(byte[] iv, byte[] data)
        {
            return new CkSeedCbcEncryptDataParams(iv, data);
        }

        /// <summary>
        /// Creates parameters for the CKM_SKIPJACK_PRIVATE_WRAP mechanism
        /// </summary>
        /// <param name='password'>User-supplied password</param>
        /// <param name='publicData'>Other party's key exchange public key value</param>
        /// <param name='randomA'>Ra data</param>
        /// <param name='primeP'>Prime, p, value</param>
        /// <param name='baseG'>Base, g, value</param>
        /// <param name='subprimeQ'>Subprime, q, value</param>
        /// <returns>Parameters for the CKM_SKIPJACK_PRIVATE_WRAP mechanism</returns>
        public ICkSkipjackPrivateWrapParams CreateCkSkipjackPrivateWrapParams(byte[] password, byte[] publicData, byte[] randomA, byte[] primeP, byte[] baseG, byte[] subprimeQ)
        {
            return new CkSkipjackPrivateWrapParams(password, publicData, randomA, primeP, baseG, subprimeQ);
        }

        /// <summary>
        /// Creates parameters for the CKM_SKIPJACK_RELAYX mechanism
        /// </summary>
        /// <param name='oldWrappedX'>Old wrapper key</param>
        /// <param name='oldPassword'>Old user-supplied password</param>
        /// <param name='oldPublicData'>Old key exchange public key value</param>
        /// <param name='oldRandomA'>Old Ra data</param>
        /// <param name='newPassword'>New user-supplied password</param>
        /// <param name='newPublicData'>New key exchange public key value</param>
        /// <param name='newRandomA'>New Ra data</param>
        /// <returns>Parameters for the CKM_SKIPJACK_RELAYX mechanism</returns>
        public ICkSkipjackRelayxParams CreateCkSkipjackRelayxParams(byte[] oldWrappedX, byte[] oldPassword, byte[] oldPublicData, byte[] oldRandomA, byte[] newPassword, byte[] newPublicData, byte[] newRandomA)
        {
            return new CkSkipjackRelayxParams(oldWrappedX, oldPassword, oldPublicData, oldRandomA, newPassword, newPublicData, newRandomA);
        }

        // Note : CkSsl3KeyMatOut does not need to be constructed here

        /// <summary>
        /// Creates parameters for the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism
        /// </summary>
        /// <param name='macSizeInBits'>The length (in bits) of the MACing keys agreed upon during the protocol handshake phase</param>
        /// <param name='keySizeInBits'>The length (in bits) of the secret keys agreed upon during the protocol handshake phase</param>
        /// <param name='ivSizeInBits'>The length (in bits) of the IV agreed upon during the protocol handshake phase or if no IV is required, the length should be set to 0</param>
        /// <param name='isExport'>Flag indicating whether the keys have to be derived for an export version of the protocol</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <returns>Parameters for the CKM_SSL3_KEY_AND_MAC_DERIVE mechanism</returns>
        public ICkSsl3KeyMatParams CreateCkSsl3KeyMatParams(ulong macSizeInBits, ulong keySizeInBits, ulong ivSizeInBits, bool isExport, ICkSsl3RandomData randomInfo)
        {
            return new CkSsl3KeyMatParams(ConvertUtils.UInt64FromUInt64(macSizeInBits), ConvertUtils.UInt64FromUInt64(keySizeInBits), ConvertUtils.UInt64FromUInt64(ivSizeInBits), isExport, randomInfo);
        }

        /// <summary>
        /// Creates parameters for the CKM_SSL3_MASTER_KEY_DERIVE and CKM_SSL3_MASTER_KEY_DERIVE_DH mechanisms
        /// </summary>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <param name='dh'>Set to false for CKM_SSL3_MASTER_KEY_DERIVE mechanism and to true for CKM_SSL3_MASTER_KEY_DERIVE_DH mechanism</param>
        /// <returns>Parameters for the CKM_SSL3_MASTER_KEY_DERIVE and CKM_SSL3_MASTER_KEY_DERIVE_DH mechanisms</returns>
        public ICkSsl3MasterKeyDeriveParams CreateCkSsl3MasterKeyDeriveParams(ICkSsl3RandomData randomInfo, bool dh)
        {
            return new CkSsl3MasterKeyDeriveParams(randomInfo, dh);
        }

        /// <summary>
        /// Creates information about the random data of a client and a server in an SSL context
        /// </summary>
        /// <param name='clientRandom'>Client's random data</param>
        /// <param name='serverRandom'>Server's random data</param>
        /// <returns>Information about the random data of a client and a server in an SSL context</returns>
        public ICkSsl3RandomData CreateCkSsl3RandomData(byte[] clientRandom, byte[] serverRandom)
        {
            return new CkSsl3RandomData(clientRandom, serverRandom);
        }

        /// <summary>
        /// Creates parameters for the CKM_TLS12_KEY_AND_MAC_DERIVE mechanism
        /// </summary>
        /// <param name="macSizeInBits">The length (in bits) of the MACing keys agreed upon during the protocol handshake phase</param>
        /// <param name="keySizeInBits">The length (in bits) of the secret keys agreed upon during the protocol handshake phase</param>
        /// <param name="ivSizeInBits">The length (in bits) of the IV agreed upon during the protocol handshake phase</param>
        /// <param name="isExport">Flag which must be set to false because export cipher suites must not be used in TLS 1.1 and later</param>
        /// <param name="randomInfo">Client's and server's random data information</param>
        /// <param name="prfHashMechanism">Base hash used in the underlying TLS1.2 PRF operation used to derive the master key (CKM)</param>
        /// <returns>Parameters for the CKM_TLS12_KEY_AND_MAC_DERIVE mechanism</returns>
        public ICkTls12KeyMatParams CreateCkTls12KeyMatParams(ulong macSizeInBits, ulong keySizeInBits, ulong ivSizeInBits, bool isExport, ICkSsl3RandomData randomInfo, ulong prfHashMechanism)
        {
            return new CkTls12KeyMatParams(ConvertUtils.UInt64FromUInt64(macSizeInBits), ConvertUtils.UInt64FromUInt64(keySizeInBits), ConvertUtils.UInt64FromUInt64(ivSizeInBits), isExport, randomInfo, ConvertUtils.UInt64FromUInt64(prfHashMechanism));
        }

        /// <summary>
        /// Creates parameters for the CKM_TLS12_MASTER_KEY_DERIVE mechanism
        /// </summary>
        /// <param name="randomInfo">Client's and server's random data information</param>
        /// <param name="prfHashMechanism">Base hash used in the underlying TLS 1.2 PRF operation used to derive the master key (CKM)</param>
        /// <returns>Parameters for the CKM_TLS12_MASTER_KEY_DERIVE mechanism</returns>
        public ICkTls12MasterKeyDeriveParams CreateCkTls12MasterKeyDeriveParams(ICkSsl3RandomData randomInfo, ulong prfHashMechanism)
        {
            return new CkTls12MasterKeyDeriveParams(randomInfo, ConvertUtils.UInt64FromUInt64(prfHashMechanism));
        }

        /// <summary>
        /// Create parameters for the CKM_TLS_KDF mechanism
        /// </summary>
        /// <param name="prfMechanism">Hash mechanism used in the TLS 1.2 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)</param>
        /// <param name="label">Label for this key derivation</param>
        /// <param name="randomInfo">Random data for the key derivation</param>
        /// <param name="contextData">Context data for this key derivation</param>
        /// <returns>Parameters for the CKM_TLS_KDF mechanism</returns>
        public ICkTlsKdfParams CreateCkTlsKdfParams(ulong prfMechanism, byte[] label, ICkSsl3RandomData randomInfo, byte[] contextData)
        {
            return new CkTlsKdfParams(ConvertUtils.UInt64FromUInt64(prfMechanism), label, randomInfo, contextData);
        }

        /// <summary>
        /// Creates parameters for the CKM_TLS_MAC mechanism
        /// </summary>
        /// <param name="prfHashMechanism">Hash mechanism used in the TLS12 PRF construct or CKM_TLS_PRF to use with the TLS 1.0 and 1.1 PRF construct (CKM)</param>
        /// <param name="macLength">Length of the MAC tag required or offered</param>
        /// <param name="serverOrClient">Should be set to "1" for "server finished" label or to "2" for "client finished" label</param>
        /// <returns>Parameters for the CKM_TLS_MAC mechanism</returns>
        public ICkTlsMacParams CreateCkTlsMacParams(ulong prfHashMechanism, ulong macLength, ulong serverOrClient)
        {
            return new CkTlsMacParams(ConvertUtils.UInt64FromUInt64(prfHashMechanism), ConvertUtils.UInt64FromUInt64(macLength), ConvertUtils.UInt64FromUInt64(serverOrClient));
        }

        /// <summary>
        /// Creates parameters for the CKM_TLS_PRF mechanism
        /// </summary>
        /// <param name='seed'>Input seed</param>
        /// <param name='label'>Identifying label</param>
        /// <param name='outputLen'>Length in bytes that the output to be created shall have</param>
        /// <returns>Parameters for the CKM_TLS_PRF mechanism</returns>
        public ICkTlsPrfParams CreateCkTlsPrfParams(byte[] seed, byte[] label, ulong outputLen)
        {
            return new CkTlsPrfParams(seed, label, ConvertUtils.UInt64FromUInt64(outputLen));
        }

        /// <summary>
        /// Creates parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism
        /// </summary>
        /// <param name='major'>Major version number (the integer portion of the version)</param>
        /// <param name='minor'>Minor version number (the hundredths portion of the version)</param>
        /// <returns>Parameters for the CKM_SSL3_PRE_MASTER_KEY_GEN mechanism</returns>
        public ICkVersion CreateCkVersion(byte major, byte minor)
        {
            return new CkVersion(major, minor);
        }

        // Note : CkWtlsKeyMatOut does not need to be constructed here

        /// <summary>
        /// Creates parameters for the CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE and the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanisms
        /// </summary>
        /// <param name='digestMechanism'>The digest mechanism to be used (CKM)</param>
        /// <param name='macSizeInBits'>The length (in bits) of the MACing key agreed upon during the protocol handshake phase</param>
        /// <param name='keySizeInBits'>The length (in bits) of the secret key agreed upon during the handshake phase</param>
        /// <param name='ivSizeInBits'>The length (in bits) of the IV agreed upon during the handshake phase or if no IV is required, the length should be set to 0</param>
        /// <param name='sequenceNumber'>The current sequence number used for records sent by the client and server respectively</param>
        /// <param name='isExport'>Flag indicating whether the keys have to be derived for an export version of the protocol</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <returns>Parameters for the CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE and the CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE mechanisms</returns>
        public ICkWtlsKeyMatParams CreateCkWtlsKeyMatParams(ulong digestMechanism, ulong macSizeInBits, ulong keySizeInBits, ulong ivSizeInBits, ulong sequenceNumber, bool isExport, ICkWtlsRandomData randomInfo)
        {
            return new CkWtlsKeyMatParams(ConvertUtils.UInt64FromUInt64(digestMechanism), ConvertUtils.UInt64FromUInt64(macSizeInBits), ConvertUtils.UInt64FromUInt64(keySizeInBits), ConvertUtils.UInt64FromUInt64(ivSizeInBits), ConvertUtils.UInt64FromUInt64(sequenceNumber), isExport, randomInfo);
        }

        /// <summary>
        /// Creates parameters for the CKM_WTLS_MASTER_KEY_DERIVE and CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC mechanisms
        /// </summary>
        /// <param name='digestMechanism'>Digest mechanism to be used (CKM)</param>
        /// <param name='randomInfo'>Client's and server's random data information</param>
        /// <param name='dh'>Set to false for CKM_WTLS_MASTER_KEY_DERIVE mechanism and to true for CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC mechanism</param>
        /// <returns>Parameters for the CKM_WTLS_MASTER_KEY_DERIVE and CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC mechanisms</returns>
        public ICkWtlsMasterKeyDeriveParams CreateCkWtlsMasterKeyDeriveParams(ulong digestMechanism, ICkWtlsRandomData randomInfo, bool dh)
        {
            return new CkWtlsMasterKeyDeriveParams(ConvertUtils.UInt64FromUInt64(digestMechanism), randomInfo, dh);
        }

        /// <summary>
        /// Creates parameters for the CKM_WTLS_PRF mechanism
        /// </summary>
        /// <param name='digestMechanism'>Digest mechanism to be used (CKM)</param>
        /// <param name='seed'>Input seed</param>
        /// <param name='label'>Identifying label</param>
        /// <param name='outputLen'>Length in bytes that the output to be created shall have</param>
        /// <returns>Parameters for the CKM_WTLS_PRF mechanism</returns>
        public ICkWtlsPrfParams CreateCkWtlsPrfParams(ulong digestMechanism, byte[] seed, byte[] label, ulong outputLen)
        {
            return new CkWtlsPrfParams(ConvertUtils.UInt64FromUInt64(digestMechanism), seed, label, ConvertUtils.UInt64FromUInt64(outputLen));
        }

        /// <summary>
        /// Creates information about the random data of a client and a server in a WTLS context
        /// </summary>
        /// <param name='clientRandom'>Client's random data</param>
        /// <param name='serverRandom'>Server's random data</param>
        /// <returns>Information about the random data of a client and a server in a WTLS context</returns>
        public ICkWtlsRandomData CreateCkWtlsRandomData(byte[] clientRandom, byte[] serverRandom)
        {
            return new CkWtlsRandomData(clientRandom, serverRandom);
        }

        /// <summary>
        /// Creates parameters for the CKM_X9_42_DH_DERIVE key derivation mechanism
        /// </summary>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='otherInfo'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's X9.42 Diffie-Hellman public key value</param>
        /// <returns>Parameters for the CKM_X9_42_DH_DERIVE key derivation mechanism</returns>
        public ICkX942Dh1DeriveParams CreateCkX942Dh1DeriveParams(ulong kdf, byte[] otherInfo, byte[] publicData)
        {
            return new CkX942Dh1DeriveParams(ConvertUtils.UInt64FromUInt64(kdf), otherInfo, publicData);
        }

        /// <summary>
        /// Creates parameters for the CKM_X9_42_DH_HYBRID_DERIVE and CKM_X9_42_MQV_DERIVE key derivation mechanisms
        /// </summary>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='otherInfo'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first X9.42 Diffie-Hellman public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second X9.42 Diffie-Hellman private key</param>
        /// <param name='privateData'>Key handle for second X9.42 Diffie-Hellman private key value</param>
        /// <param name='publicData2'>Other party's second X9.42 Diffie-Hellman public key value</param>
        /// <returns>Parameters for the CKM_X9_42_DH_HYBRID_DERIVE and CKM_X9_42_MQV_DERIVE key derivation mechanisms</returns>
        public ICkX942Dh2DeriveParams CreateCkX942Dh2DeriveParams(ulong kdf, byte[] otherInfo, byte[] publicData, ulong privateDataLen, IObjectHandle privateData, byte[] publicData2)
        {
            return new CkX942Dh2DeriveParams(ConvertUtils.UInt64FromUInt64(kdf), otherInfo, publicData, ConvertUtils.UInt64FromUInt64(privateDataLen), privateData, publicData2);
        }

        /// <summary>
        /// Creates parameters for the CKM_X9_42_MQV_DERIVE key derivation mechanism
        /// </summary>>
        /// <param name='kdf'>Key derivation function used on the shared secret value (CKD)</param>
        /// <param name='otherInfo'>Some data shared between the two parties</param>
        /// <param name='publicData'>Other party's first X9.42 Diffie-Hellman public key value</param>
        /// <param name='privateDataLen'>The length in bytes of the second X9.42 Diffie-Hellman private key</param>
        /// <param name='privateData'>Key handle for second X9.42 Diffie-Hellman private key value</param>
        /// <param name='publicData2'>Other party's second X9.42 Diffie-Hellman public key value</param>
        /// <param name='publicKey'>Handle to the first party's ephemeral public key</param>
        /// <returns>Parameters for the CKM_X9_42_MQV_DERIVE key derivation mechanism</returns>
        public ICkX942MqvDeriveParams CreateCkX942MqvDeriveParams(ulong kdf, byte[] otherInfo, byte[] publicData, ulong privateDataLen, IObjectHandle privateData, byte[] publicData2, IObjectHandle publicKey)
        {
            return new CkX942MqvDeriveParams(ConvertUtils.UInt64FromUInt64(kdf), otherInfo, publicData, ConvertUtils.UInt64FromUInt64(privateDataLen), privateData, publicData2, publicKey);
        }
    }
}
