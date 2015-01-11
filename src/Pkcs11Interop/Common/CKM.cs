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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Mechanism type
    /// </summary>
    public enum CKM : uint
    {
        /// <summary>
        /// Key pair generation mechanism based on the RSA public-key cryptosystem, as defined in PKCS #1
        /// </summary>
        CKM_RSA_PKCS_KEY_PAIR_GEN = 0x00000000,

        /// <summary>
        /// Multi-purpose mechanism based on the RSA public-key cryptosystem and the block formats initially defined in PKCS #1 v1.5.
        /// </summary>
        CKM_RSA_PKCS = 0x00000001,

        /// <summary>
        /// Mechanism for single-part signatures and verification with and without message recovery based on the RSA public-key cryptosystem and the block formats defined in ISO/IEC 9796 and its annex A
        /// </summary>
        CKM_RSA_9796 = 0x00000002,

        /// <summary>
        /// Multi-purpose mechanism based on the RSA public-key cryptosystem ("raw" RSA, as assumed in X.509)
        /// </summary>
        CKM_RSA_X_509 = 0x00000003,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with MD2 mechanism
        /// </summary>
        CKM_MD2_RSA_PKCS = 0x00000004,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with MD5 mechanism
        /// </summary>
        CKM_MD5_RSA_PKCS = 0x00000005,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with SHA-1 mechanism
        /// </summary>
        CKM_SHA1_RSA_PKCS = 0x00000006,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with RIPEMD-128
        /// </summary>
        CKM_RIPEMD128_RSA_PKCS = 0x00000007,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with RIPEMD-160
        /// </summary>
        CKM_RIPEMD160_RSA_PKCS = 0x00000008,

        /// <summary>
        /// The PKCS #1 RSA OAEP mechanism based on the RSA public-key cryptosystem and the OAEP block format defined in PKCS #1
        /// </summary>
        CKM_RSA_PKCS_OAEP = 0x00000009,

        /// <summary>
        /// The X9.31 RSA key pair generation mechanism
        /// </summary>
        CKM_RSA_X9_31_KEY_PAIR_GEN = 0x0000000A,

        /// <summary>
        /// The ANSI X9.31 RSA mechanism
        /// </summary>
        CKM_RSA_X9_31 = 0x0000000B,

        /// <summary>
        /// The ANSI X9.31 RSA signature with SHA-1 mechanism
        /// </summary>
        CKM_SHA1_RSA_X9_31 = 0x0000000C,

        /// <summary>
        /// The PKCS #1 RSA PSS mechanism based on the RSA public-key cryptosystem and the PSS block format defined in PKCS#1
        /// </summary>
        CKM_RSA_PKCS_PSS = 0x0000000D,

        /// <summary>
        /// The PKCS #1 RSA PSS signature with SHA-1 mechanism
        /// </summary>
        CKM_SHA1_RSA_PKCS_PSS = 0x0000000E,

        /// <summary>
        /// The DSA key pair generation mechanism
        /// </summary>
        CKM_DSA_KEY_PAIR_GEN = 0x00000010,

        /// <summary>
        /// The DSA without hashing mechanism
        /// </summary>
        CKM_DSA = 0x00000011,

        /// <summary>
        /// The DSA with SHA-1 mechanism
        /// </summary>
        CKM_DSA_SHA1 = 0x00000012,

        /// <summary>
        /// The PKCS #3 Diffie-Hellman key pair generation mechanism
        /// </summary>
        CKM_DH_PKCS_KEY_PAIR_GEN = 0x00000020,

        /// <summary>
        /// The PKCS #3 Diffie-Hellman key derivation mechanism
        /// </summary>
        CKM_DH_PKCS_DERIVE = 0x00000021,

        /// <summary>
        /// The X9.42 Diffie-Hellman key pair generation mechanism
        /// </summary>
        CKM_X9_42_DH_KEY_PAIR_GEN = 0x00000030,

        /// <summary>
        /// The X9.42 Diffie-Hellman key derivation mechanism
        /// </summary>
        CKM_X9_42_DH_DERIVE = 0x00000031,

        /// <summary>
        /// The X9.42 Diffie-Hellman hybrid key derivation mechanism
        /// </summary>
        CKM_X9_42_DH_HYBRID_DERIVE = 0x00000032,

        /// <summary>
        /// The X9.42 Diffie-Hellman Menezes-Qu-Vanstone (MQV) key derivation mechanism
        /// </summary>
        CKM_X9_42_MQV_DERIVE = 0x00000033,

        /// <summary>
        /// PKCS #1 v1.5 RSA signature with SHA-256 mechanism
        /// </summary>
        CKM_SHA256_RSA_PKCS = 0x00000040,
        
        /// <summary>
        /// PKCS #1 v1.5 RSA signature with SHA-384 mechanism
        /// </summary>
        CKM_SHA384_RSA_PKCS = 0x00000041,

        /// <summary>
        /// PKCS #1 v1.5 RSA signature with SHA-512 mechanism
        /// </summary>
        CKM_SHA512_RSA_PKCS = 0x00000042,

        /// <summary>
        /// The PKCS #1 RSA PSS signature with SHA-256 mechanism
        /// </summary>
        CKM_SHA256_RSA_PKCS_PSS = 0x00000043,

        /// <summary>
        /// The PKCS #1 RSA PSS signature with SHA-384 mechanism
        /// </summary>
        CKM_SHA384_RSA_PKCS_PSS = 0x00000044,

        /// <summary>
        /// The PKCS #1 RSA PSS signature with SHA-512 mechanism
        /// </summary>
        CKM_SHA512_RSA_PKCS_PSS = 0x00000045,

        /// <summary>
        /// The PKCS #1 v1.5 RSA signature with SHA-224 mechanism
        /// </summary>
        CKM_SHA224_RSA_PKCS = 0x00000046,

        /// <summary>
        /// The PKCS #1 RSA PSS signature with SHA-224 mechanism
        /// </summary>
        CKM_SHA224_RSA_PKCS_PSS = 0x00000047,

        /// <summary>
        /// The RC2 key generation mechanism
        /// </summary>
        CKM_RC2_KEY_GEN = 0x00000100,

        /// <summary>
        /// RC2-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_RC2_ECB = 0x00000101,

        /// <summary>
        /// RC2-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_RC2_CBC = 0x00000102,

        /// <summary>
        /// Special case of general-length RC2-MAC mechanism
        /// </summary>
        CKM_RC2_MAC = 0x00000103,
        
        /// <summary>
        /// General-length RC2-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_RC2_MAC_GENERAL = 0x00000104,

        /// <summary>
        /// RC2-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_RC2_CBC_PAD = 0x00000105,

        /// <summary>
        /// The RC4 key generation mechanism
        /// </summary>
        CKM_RC4_KEY_GEN = 0x00000110,

        /// <summary>
        /// RC4 encryption mechanism
        /// </summary>
        CKM_RC4 = 0x00000111,

        /// <summary>
        /// Single-length DES key generation mechanism
        /// </summary>
        CKM_DES_KEY_GEN = 0x00000120,

        /// <summary>
        /// DES-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_DES_ECB = 0x00000121,

        /// <summary>
        /// DES-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_DES_CBC = 0x00000122,

        /// <summary>
        /// Special case of general-length DES-MAC mechanism
        /// </summary>
        CKM_DES_MAC = 0x00000123,

        /// <summary>
        /// General-length DES-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_DES_MAC_GENERAL = 0x00000124,
        
        /// <summary>
        /// DES-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_DES_CBC_PAD = 0x00000125,

        /// <summary>
        /// Double-length DES key generation mechanism
        /// </summary>
        CKM_DES2_KEY_GEN = 0x00000130,
        
        /// <summary>
        /// Triple-length DES key generation mechanism
        /// </summary>
        CKM_DES3_KEY_GEN = 0x00000131,

        /// <summary>
        /// DES3-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_DES3_ECB = 0x00000132,

        /// <summary>
        /// DES3-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_DES3_CBC = 0x00000133,

        /// <summary>
        /// Special case of general-length DES3-MAC mechanism
        /// </summary>
        CKM_DES3_MAC = 0x00000134,

        /// <summary>
        /// General-length DES3-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_DES3_MAC_GENERAL = 0x00000135,

        /// <summary>
        /// DES3-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_DES3_CBC_PAD = 0x00000136,

        /// <summary>
        /// Single-length CDMF key generation mechanism
        /// </summary>
        CKM_CDMF_KEY_GEN = 0x00000140,

        /// <summary>
        /// CDMF-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CDMF_ECB = 0x00000141,

        /// <summary>
        /// CDMF-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CDMF_CBC = 0x00000142,

        /// <summary>
        /// Special case of general-length CDMF-MAC mechanism
        /// </summary>
        CKM_CDMF_MAC = 0x00000143,

        /// <summary>
        /// General-length CDMF-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CDMF_MAC_GENERAL = 0x00000144,

        /// <summary>
        /// CDMF-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CDMF_CBC_PAD = 0x00000145,

        /// <summary>
        /// DES-OFB64 encryption mechanism with output feedback mode (OFB)
        /// </summary>
        CKM_DES_OFB64 = 0x00000150,

        /// <summary>
        /// DES-OFB8 encryption mechanism with output feedback mode (OFB)
        /// </summary>
        CKM_DES_OFB8 = 0x00000151,

        /// <summary>
        /// DES-CFB64 encryption mechanism with cipher feedback mode (CFB)
        /// </summary>
        CKM_DES_CFB64 = 0x00000152,

        /// <summary>
        /// DES-CFB8 encryption mechanism with cipher feedback mode (CFB)
        /// </summary>
        CKM_DES_CFB8 = 0x00000153,

        /// <summary>
        /// The MD2 digesting mechanism
        /// </summary>
        CKM_MD2 = 0x00000200,

        /// <summary>
        /// Special case of the general-length MD2-HMAC mechanism
        /// </summary>
        CKM_MD2_HMAC = 0x00000201,

        /// <summary>
        /// The general-length MD2-HMAC mechanism that uses the HMAC construction, based on the MD2 hash function
        /// </summary>
        CKM_MD2_HMAC_GENERAL = 0x00000202,

        /// <summary>
        /// The MD5 digesting mechanism
        /// </summary>
        CKM_MD5 = 0x00000210,

        /// <summary>
        /// Special case of the general-length MD5-HMAC mechanism
        /// </summary>
        CKM_MD5_HMAC = 0x00000211,

        /// <summary>
        /// The general-length MD5-HMAC mechanism that uses the HMAC construction, based on the MD5 hash function
        /// </summary>
        CKM_MD5_HMAC_GENERAL = 0x00000212,

        /// <summary>
        /// The SHA-1 digesting mechanism
        /// </summary>
        CKM_SHA_1 = 0x00000220,

        /// <summary>
        /// Special case of the general-length SHA1-HMAC mechanism
        /// </summary>
        CKM_SHA_1_HMAC = 0x00000221,

        /// <summary>
        /// The general-length SHA1-HMAC mechanism that uses the HMAC construction, based on the SHA1 hash function
        /// </summary>
        CKM_SHA_1_HMAC_GENERAL = 0x00000222,

        /// <summary>
        /// The RIPE-MD 128 digesting mechanism
        /// </summary>
        CKM_RIPEMD128 = 0x00000230,

        /// <summary>
        /// Special case of the general-length RIPE-MD 128-HMAC mechanism
        /// </summary>
        CKM_RIPEMD128_HMAC = 0x00000231,

        /// <summary>
        ///  The general-length RIPE-MD 128-HMAC mechanism that uses the HMAC construction, based on the RIPE-MD 128 hash function
        /// </summary>
        CKM_RIPEMD128_HMAC_GENERAL = 0x00000232,

        /// <summary>
        /// The RIPE-MD 160 digesting mechanism
        /// </summary>
        CKM_RIPEMD160 = 0x00000240,

        /// <summary>
        /// Special case of the general-length RIPE-MD 160-HMAC mechanism
        /// </summary>
        CKM_RIPEMD160_HMAC = 0x00000241,

        /// <summary>
        ///  The general-length RIPE-MD 160-HMAC mechanism that uses the HMAC construction, based on the RIPE-MD 160 hash function
        /// </summary>
        CKM_RIPEMD160_HMAC_GENERAL = 0x00000242,

        /// <summary>
        /// The SHA-256 digesting mechanism
        /// </summary>
        CKM_SHA256 = 0x00000250,

        /// <summary>
        /// Special case of the general-length SHA-256-HMAC mechanism
        /// </summary>
        CKM_SHA256_HMAC = 0x00000251,

        /// <summary>
        /// The general-length SHA-256-HMAC mechanism that uses the HMAC construction, based on the SHA-256 hash function
        /// </summary>
        CKM_SHA256_HMAC_GENERAL = 0x00000252,

        /// <summary>
        /// The SHA-224 digesting mechanism
        /// </summary>
        CKM_SHA224 = 0x00000255,

        /// <summary>
        /// Special case of the general-length SHA-224-HMAC mechanism
        /// </summary>
        CKM_SHA224_HMAC = 0x00000256,

        /// <summary>
        /// The general-length SHA-224-HMAC mechanism that uses the HMAC construction, based on the SHA-224 hash function
        /// </summary>
        CKM_SHA224_HMAC_GENERAL = 0x00000257,

        /// <summary>
        /// The SHA-384 digesting mechanism
        /// </summary>
        CKM_SHA384 = 0x00000260,

        /// <summary>
        /// Special case of the general-length SHA-384-HMAC mechanism
        /// </summary>
        CKM_SHA384_HMAC = 0x00000261,

        /// <summary>
        /// The general-length SHA-384-HMAC mechanism that uses the HMAC construction, based on the SHA-384 hash function
        /// </summary>
        CKM_SHA384_HMAC_GENERAL = 0x00000262,

        /// <summary>
        /// The SHA-512 digesting mechanism
        /// </summary>
        CKM_SHA512 = 0x00000270,

        /// <summary>
        /// Special case of the general-length SHA-512-HMAC mechanism
        /// </summary>
        CKM_SHA512_HMAC = 0x00000271,

        /// <summary>
        /// The general-length SHA-512-HMAC mechanism that uses the HMAC construction, based on the SHA-512 hash function
        /// </summary>
        CKM_SHA512_HMAC_GENERAL = 0x00000272,

        /// <summary>
        /// Key generation mechanism for the RSA SecurID algorithm
        /// </summary>
        CKM_SECURID_KEY_GEN = 0x00000280,

        /// <summary>
        /// Mechanism for the retrieval and verification of RSA SecurID OTP values
        /// </summary>
        CKM_SECURID = 0x00000282,

        /// <summary>
        /// Key generation mechanism for the HOTP algorithm
        /// </summary>
        CKM_HOTP_KEY_GEN = 0x00000290,

        /// <summary>
        /// Mechanism for the retrieval and verification of HOTP OTP values
        /// </summary>
        CKM_HOTP = 0x00000291,

        /// <summary>
        /// Mechanism for the retrieval and verification of ACTI OTP values
        /// </summary>
        CKM_ACTI = 0x000002A0,

        /// <summary>
        /// Key generation mechanism for the ACTI algorithm
        /// </summary>
        CKM_ACTI_KEY_GEN = 0x000002A1,

        /// <summary>
        /// CAST key generation mechanism
        /// </summary>
        CKM_CAST_KEY_GEN = 0x00000300,
        
        /// <summary>
        /// CAST-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAST_ECB = 0x00000301,

        /// <summary>
        /// CAST-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAST_CBC = 0x00000302,

        /// <summary>
        /// Special case of general-length CAST-MAC mechanism
        /// </summary>
        CKM_CAST_MAC = 0x00000303,

        /// <summary>
        /// General-length CAST-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CAST_MAC_GENERAL = 0x00000304,

        /// <summary>
        /// CAST-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CAST_CBC_PAD = 0x00000305,
        
        /// <summary>
        /// CAST3 key generation mechanism
        /// </summary>
        CKM_CAST3_KEY_GEN = 0x00000310,

        /// <summary>
        /// CAST3-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAST3_ECB = 0x00000311,

        /// <summary>
        /// CAST3-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAST3_CBC = 0x00000312,
        
        /// <summary>
        /// Special case of general-length CAST3-MAC mechanism
        /// </summary>
        CKM_CAST3_MAC = 0x00000313,

        /// <summary>
        /// General-length CAST3-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CAST3_MAC_GENERAL = 0x00000314,

        /// <summary>
        /// CAST3-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CAST3_CBC_PAD = 0x00000315,
        
        /// <summary>
        /// CAST128 key generation mechanism
        /// </summary>
        CKM_CAST5_KEY_GEN = 0x00000320,

        /// <summary>
        /// CAST128 key generation mechanism
        /// </summary>
        CKM_CAST128_KEY_GEN = 0x00000320,

        /// <summary>
        /// CAST128-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAST5_ECB = 0x00000321,

        /// <summary>
        /// CAST128-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAST128_ECB = 0x00000321,

        /// <summary>
        /// CAST128-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAST5_CBC = 0x00000322,

        /// <summary>
        /// CAST128-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAST128_CBC = 0x00000322,

        /// <summary>
        /// Special case of general-length CAST128-MAC mechanism
        /// </summary>
        CKM_CAST5_MAC = 0x00000323,

        /// <summary>
        /// Special case of general-length CAST128-MAC mechanism
        /// </summary>
        CKM_CAST128_MAC = 0x00000323,

        /// <summary>
        /// General-length CAST128-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CAST5_MAC_GENERAL = 0x00000324,

        /// <summary>
        /// General-length CAST128-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CAST128_MAC_GENERAL = 0x00000324,

        /// <summary>
        /// CAST128-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CAST5_CBC_PAD = 0x00000325,

        /// <summary>
        /// CAST128-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CAST128_CBC_PAD = 0x00000325,
        
        /// <summary>
        /// RC5 key generation mechanism
        /// </summary>
        CKM_RC5_KEY_GEN = 0x00000330,

        /// <summary>
        /// RC5-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_RC5_ECB = 0x00000331,

        /// <summary>
        /// RC5-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_RC5_CBC = 0x00000332,

        /// <summary>
        /// Special case of general-length RC5-MAC mechanism
        /// </summary>
        CKM_RC5_MAC = 0x00000333,

        /// <summary>
        /// General-length RC5-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_RC5_MAC_GENERAL = 0x00000334,

        /// <summary>
        /// RC5-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_RC5_CBC_PAD = 0x00000335,
        
        /// <summary>
        /// IDEA key generation mechanism
        /// </summary>
        CKM_IDEA_KEY_GEN = 0x00000340,

        /// <summary>
        /// IDEA-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_IDEA_ECB = 0x00000341,

        /// <summary>
        /// IDEA-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_IDEA_CBC = 0x00000342,

        /// <summary>
        /// Special case of general-length IDEA-MAC mechanism
        /// </summary>
        CKM_IDEA_MAC = 0x00000343,

        /// <summary>
        /// General-length IDEA-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_IDEA_MAC_GENERAL = 0x00000344,

        /// <summary>
        /// IDEA-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_IDEA_CBC_PAD = 0x00000345,
        
        /// <summary>
        /// The generic secret key generation mechanism
        /// </summary>
        CKM_GENERIC_SECRET_KEY_GEN = 0x00000350,

        /// <summary>
        /// Key derivation mechanism that derives a secret key from the concatenation of two existing secret keys
        /// </summary>
        CKM_CONCATENATE_BASE_AND_KEY = 0x00000360,

        /// <summary>
        /// Key derivation mechanism that derives a secret key by concatenating data onto the end of a specified secret key
        /// </summary>
        CKM_CONCATENATE_BASE_AND_DATA = 0x00000362,

        /// <summary>
        /// Key derivation mechanism that derives a secret key by prepending data to the start of a specified secret key
        /// </summary>
        CKM_CONCATENATE_DATA_AND_BASE = 0x00000363,

        /// <summary>
        /// Key derivation mechanism that 
        /// </summary>
        CKM_XOR_BASE_AND_DATA = 0x00000364,

        /// <summary>
        /// Mechanism which provides the capability of creating one secret key from the bits of another secret key
        /// </summary>
        CKM_EXTRACT_KEY_FROM_KEY = 0x00000365,

        /// <summary>
        /// Mechanism for pre_master key generation in SSL 3.0
        /// </summary>
        CKM_SSL3_PRE_MASTER_KEY_GEN = 0x00000370,
        
        /// <summary>
        /// Mechanism for master key derivation in SSL 3.0
        /// </summary>
        CKM_SSL3_MASTER_KEY_DERIVE = 0x00000371,

        /// <summary>
        /// Mechanism for key, MAC and IV derivation in SSL 3.0
        /// </summary>
        CKM_SSL3_KEY_AND_MAC_DERIVE = 0x00000372,

        /// <summary>
        /// Mechanism for master key derivation for Diffie-Hellman in SSL 3.0
        /// </summary>
        CKM_SSL3_MASTER_KEY_DERIVE_DH = 0x00000373,

        /// <summary>
        /// Mechanism for pre-master key generation in TLS 1.0,
        /// </summary>
        CKM_TLS_PRE_MASTER_KEY_GEN = 0x00000374,

        /// <summary>
        /// Mechanism for master key derivation in TLS 1.0
        /// </summary>
        CKM_TLS_MASTER_KEY_DERIVE = 0x00000375,

        /// <summary>
        /// Mechanism for key, MAC and IV derivation in TLS 1.0
        /// </summary>
        CKM_TLS_KEY_AND_MAC_DERIVE = 0x00000376,

        /// <summary>
        /// Mechanism for master key derivation for Diffie-Hellman in TLS 1.0
        /// </summary>
        CKM_TLS_MASTER_KEY_DERIVE_DH = 0x00000377,

        /// <summary>
        /// PRF (pseudo random function) in TLS
        /// </summary>
        CKM_TLS_PRF = 0x00000378,

        /// <summary>
        /// Mechanism for MD5 MACing in SSL3.0
        /// </summary>
        CKM_SSL3_MD5_MAC = 0x00000380,

        /// <summary>
        /// Mechanism for SHA-1 MACing in SSL3.0
        /// </summary>
        CKM_SSL3_SHA1_MAC = 0x00000381,

        /// <summary>
        /// MD5 key derivation mechanism
        /// </summary>
        CKM_MD5_KEY_DERIVATION = 0x00000390,

        /// <summary>
        /// MD2 key derivation mechanism
        /// </summary>
        CKM_MD2_KEY_DERIVATION = 0x00000391,

        /// <summary>
        /// SHA-1 key derivation mechanism
        /// </summary>
        CKM_SHA1_KEY_DERIVATION = 0x00000392,

        /// <summary>
        /// SHA-256 key derivation mechanism
        /// </summary>
        CKM_SHA256_KEY_DERIVATION = 0x00000393,
        
        /// <summary>
        /// SHA-384 key derivation mechanism
        /// </summary>
        CKM_SHA384_KEY_DERIVATION = 0x00000394,
        
        /// <summary>
        /// SHA-512 key derivation mechanism
        /// </summary>
        CKM_SHA512_KEY_DERIVATION = 0x00000395,
        
        /// <summary>
        /// SHA-224 key derivation mechanism
        /// </summary>
        CKM_SHA224_KEY_DERIVATION = 0x00000396,

        /// <summary>
        /// MD2-PBE for DES-CBC mechanism used for generating a DES secret key and an IV from a password and a salt value by using the MD2 digest algorithm and an iteration count. This functionality is defined in PKCS#5 as PBKDF1.
        /// </summary>
        CKM_PBE_MD2_DES_CBC = 0x000003A0,

        /// <summary>
        /// MD5-PBE for DES-CBC mechanism used for generating a DES secret key and an IV from a password and a salt value by using the MD5 digest algorithm and an iteration count. This functionality is defined in PKCS#5 as PBKDF1.
        /// </summary>
        CKM_PBE_MD5_DES_CBC = 0x000003A1,

        /// <summary>
        /// MD5-PBE for CAST-CBC mechanism used for generating a CAST secret key and an IV from a password and a salt value by using the MD5 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_MD5_CAST_CBC = 0x000003A2,

        /// <summary>
        /// MD5-PBE for CAST3-CBC mechanism used for generating a CAST3 secret key and an IV from a password and a salt value by using the MD5 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_MD5_CAST3_CBC = 0x000003A3,

        /// <summary>
        /// MD5-PBE for CAST128-CBC (CAST5-CBC) mechanism used for generating a CAST128 (CAST5) secret key and an IV from a password and a salt value by using the MD5 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_MD5_CAST5_CBC = 0x000003A4,

        /// <summary>
        /// MD5-PBE for CAST128-CBC mechanism used for generating a CAST128 secret key and an IV from a password and a salt value by using the MD5 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_MD5_CAST128_CBC = 0x000003A4,

        /// <summary>
        /// SHA-1-PBE for CAST128-CBC (CAST5-CBC) mechanism used for generating a CAST128 (CAST5) secret key and an IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_CAST5_CBC = 0x000003A5,

        /// <summary>
        /// SHA-1-PBE for CAST128-CBC mechanism used for generating a CAST128 secret key and an IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_CAST128_CBC = 0x000003A5,

        /// <summary>
        /// SHA-1-PBE for 128-bit RC4 mechanism used for generating a 128-bit RC4 secret key from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_RC4_128 = 0x000003A6,

        /// <summary>
        /// SHA-1-PBE for 40-bit RC4 mechanism used for generating a 40-bit RC4 secret key from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_RC4_40 = 0x000003A7,

        /// <summary>
        /// SHA-1-PBE for 3-key triple-DES-CBC mechanism used for generating a 3-key triple-DES secret key and IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_DES3_EDE_CBC = 0x000003A8,

        /// <summary>
        /// SHA-1-PBE for 2-key triple-DES-CBC mechanism used for generating a 2-key triple-DES secret key and IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_DES2_EDE_CBC = 0x000003A9,

        /// <summary>
        /// SHA-1-PBE for 128-bit RC2-CBC mechanism used for generating a 128-bit RC2 secret key and IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_RC2_128_CBC = 0x000003AA,

        /// <summary>
        /// SHA-1-PBE for 40-bit RC2-CBC mechanism used for generating a 40-bit RC2 secret key and IV from a password and a salt value by using the SHA-1 digest algorithm and an iteration count.
        /// </summary>
        CKM_PBE_SHA1_RC2_40_CBC = 0x000003AB,

        /// <summary>
        /// PKCS #5 PBKDF2 key generation mechanism used for generating a secret key from a password and a salt value
        /// </summary>
        CKM_PKCS5_PBKD2 = 0x000003B0,

        /// <summary>
        /// SHA-1-PBA for SHA-1-HMAC mechanism used for generating a 160-bit generic secret key from a password and a salt value by using the SHA-1 digest algorithm and an iteration count
        /// </summary>
        CKM_PBA_SHA1_WITH_SHA1_HMAC = 0x000003C0,

        /// <summary>
        /// Mechanism for pre-master secret key generation for the RSA key exchange suite in WTLS
        /// </summary>
        CKM_WTLS_PRE_MASTER_KEY_GEN = 0x000003D0,

        /// <summary>
        /// Mechanism for master secret derivation in WTLS
        /// </summary>
        CKM_WTLS_MASTER_KEY_DERIVE = 0x000003D1,

        /// <summary>
        /// Mechanism for master secret derivation for Diffie-Hellman and Elliptic Curve Cryptography in WTLS
        /// </summary>
        CKM_WTLS_MASTER_KEY_DERIVE_DH_ECC = 0x000003D2,

        /// <summary>
        /// PRF (pseudo random function) in WTLS
        /// </summary>
        CKM_WTLS_PRF = 0x000003D3,

        /// <summary>
        /// Mechanism for server key, MAC and IV derivation in WTLS
        /// </summary>
        CKM_WTLS_SERVER_KEY_AND_MAC_DERIVE = 0x000003D4,

        /// <summary>
        /// Mechanism for client key, MAC and IV derivation in WTLS
        /// </summary>
        CKM_WTLS_CLIENT_KEY_AND_MAC_DERIVE = 0x000003D5,

        /// <summary>
        /// The LYNKS key wrapping mechanism
        /// </summary>
        CKM_KEY_WRAP_LYNKS = 0x00000400,

        /// <summary>
        /// The OAEP key wrapping for SET mechanism
        /// </summary>
        CKM_KEY_WRAP_SET_OAEP = 0x00000401,

        /// <summary>
        /// The CMS mechanism
        /// </summary>
        CKM_CMS_SIG = 0x00000500,

        /// <summary>
        /// The CT-KIP key derivation mechanism
        /// </summary>
        CKM_KIP_DERIVE = 0x00000510,

        /// <summary>
        /// The CT-KIP key wrap and unwrap mechanism
        /// </summary>
        CKM_KIP_WRAP = 0x00000511,
        
        /// <summary>
        /// The CT-KIP signature (MAC) mechanism
        /// </summary>
        CKM_KIP_MAC = 0x00000512,
        
        /// <summary>
        /// The Camellia key generation mechanism
        /// </summary>
        CKM_CAMELLIA_KEY_GEN = 0x00000550,

        /// <summary>
        /// Camellia-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAMELLIA_ECB = 0x00000551,

        /// <summary>
        /// Camellia-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAMELLIA_CBC = 0x00000552,

        /// <summary>
        /// Special case of general-length Camellia-MAC mechanism
        /// </summary>
        CKM_CAMELLIA_MAC = 0x00000553,

        /// <summary>
        /// General-length Camellia-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_CAMELLIA_MAC_GENERAL = 0x00000554,

        /// <summary>
        /// Camellia-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_CAMELLIA_CBC_PAD = 0x00000555,
        
        /// <summary>
        /// Key derivation mechanism based on Camellia-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_CAMELLIA_ECB_ENCRYPT_DATA = 0x00000556,

        /// <summary>
        /// Key derivation mechanism based on Camellia-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_CAMELLIA_CBC_ENCRYPT_DATA = 0x00000557,

        /// <summary>
        /// Camellia-CTR mechanism for encryption and decryption with CAMELLIA in counter mode
        /// </summary>
        CKM_CAMELLIA_CTR = 0x00000558,
        
        /// <summary>
        /// The ARIA key generation mechanism
        /// </summary>
        CKM_ARIA_KEY_GEN = 0x00000560,

        /// <summary>
        /// ARIA-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_ARIA_ECB = 0x00000561,

        /// <summary>
        /// ARIA-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_ARIA_CBC = 0x00000562,

        /// <summary>
        /// Special case of general-length ARIA-MAC mechanism
        /// </summary>
        CKM_ARIA_MAC = 0x00000563,

        /// <summary>
        /// General-length ARIA-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_ARIA_MAC_GENERAL = 0x00000564,

        /// <summary>
        /// ARIA-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_ARIA_CBC_PAD = 0x00000565,

        /// <summary>
        /// Key derivation mechanism based on ARIA-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_ARIA_ECB_ENCRYPT_DATA = 0x00000566,

        /// <summary>
        /// Key derivation mechanism based on ARIA-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_ARIA_CBC_ENCRYPT_DATA = 0x00000567,
        
        /// <summary>
        /// The SKIPJACK key generation mechanism
        /// </summary>
        CKM_SKIPJACK_KEY_GEN = 0x00001000,

        /// <summary>
        /// SKIPJACK-ECB64 mechanism for encryption and decryption with SKIPJACK in 64-bit electronic codebook mode (ECB)
        /// </summary>
        CKM_SKIPJACK_ECB64 = 0x00001001,

        /// <summary>
        /// SKIPJACK-CBC64 mechanism for encryption and decryption with SKIPJACK in 64-bit cipher-block chaining mode (CBC)
        /// </summary>
        CKM_SKIPJACK_CBC64 = 0x00001002,

        /// <summary>
        /// SKIPJACK-OFB64 mechanism for encryption and decryption with SKIPJACK in 64-bit output feedback mode (OFB)
        /// </summary>
        CKM_SKIPJACK_OFB64 = 0x00001003,

        /// <summary>
        /// SKIPJACK-CFB64 mechanism for encryption and decryption with SKIPJACK in 64-bit cipher feedback mode (CFB)
        /// </summary>
        CKM_SKIPJACK_CFB64 = 0x00001004,

        /// <summary>
        /// SKIPJACK-CFB32 mechanism for encryption and decryption with SKIPJACK in 32-bit cipher feedback mode (CFB)
        /// </summary>
        CKM_SKIPJACK_CFB32 = 0x00001005,

        /// <summary>
        /// SKIPJACK-CFB16 mechanism for encryption and decryption with SKIPJACK in 16-bit cipher feedback mode (CFB)
        /// </summary>
        CKM_SKIPJACK_CFB16 = 0x00001006,

        /// <summary>
        /// SKIPJACK-CFB8 mechanism for encryption and decryption with SKIPJACK in 8-bit cipher feedback mode (CFB)
        /// </summary>
        CKM_SKIPJACK_CFB8 = 0x00001007,

        /// <summary>
        /// SKIPJACK mechanism for wrapping and unwrapping of secret keys (MEK)
        /// </summary>
        CKM_SKIPJACK_WRAP = 0x00001008,

        /// <summary>
        /// Mechanism for wrapping and unwrapping KEA and DSA private keys
        /// </summary>
        CKM_SKIPJACK_PRIVATE_WRAP = 0x00001009,

        /// <summary>
        /// Mechanism for "change of wrapping" on a private key which was wrapped with the SKIPJACK-PRIVATE-WRAP mechanism
        /// </summary>
        CKM_SKIPJACK_RELAYX = 0x0000100a,

        /// <summary>
        /// The KEA key pair generation mechanism
        /// </summary>
        CKM_KEA_KEY_PAIR_GEN = 0x00001010,

        /// <summary>
        /// The KEA key derivation mechanism
        /// </summary>
        CKM_KEA_KEY_DERIVE = 0x00001011,
        
        /// <summary>
        /// The FORTEZZA timestamp mechanism
        /// </summary>
        CKM_FORTEZZA_TIMESTAMP = 0x00001020,
        
        /// <summary>
        /// The BATON key generation mechanism
        /// </summary>
        CKM_BATON_KEY_GEN = 0x00001030,

        /// <summary>
        /// BATON-ECB128 mechanism for encryption and decryption with BATON in 128-bit electronic codebook mode (ECB)
        /// </summary>
        CKM_BATON_ECB128 = 0x00001031,

        /// <summary>
        /// BATON-ECB96 mechanism for encryption and decryption with BATON in 96-bit electronic codebook mode (ECB)
        /// </summary>
        CKM_BATON_ECB96 = 0x00001032,

        /// <summary>
        /// BATON-CBC128 mechanism for encryption and decryption with BATON in 128-bit cipher-block chaining mode (CBC)
        /// </summary>
        CKM_BATON_CBC128 = 0x00001033,

        /// <summary>
        /// BATON-COUNTER mechanism encryption and decryption with BATON in counter mode
        /// </summary>
        CKM_BATON_COUNTER = 0x00001034,

        /// <summary>
        /// BATON-SHUFFLE mechanism for encryption and decryption with BATON in shuffle mode
        /// </summary>
        CKM_BATON_SHUFFLE = 0x00001035,

        /// <summary>
        /// BATON mechanism for wrapping and unwrapping of secret keys (MEK)
        /// </summary>
        CKM_BATON_WRAP = 0x00001036,
        
        /// <summary>
        /// The EC (also related to ECDSA) key pair generation mechanism
        /// </summary>
        CKM_ECDSA_KEY_PAIR_GEN = 0x00001040,

        /// <summary>
        /// The EC (also related to ECDSA) key pair generation mechanism
        /// </summary>
        CKM_EC_KEY_PAIR_GEN = 0x00001040,

        /// <summary>
        /// The ECDSA without hashing mechanism
        /// </summary>
        CKM_ECDSA = 0x00001041,

        /// <summary>
        /// The ECDSA with SHA-1 mechanism
        /// </summary>
        CKM_ECDSA_SHA1 = 0x00001042,

        /// <summary>
        /// The elliptic curve Diffie-Hellman (ECDH) key derivation mechanism
        /// </summary>
        CKM_ECDH1_DERIVE = 0x00001050,

        /// <summary>
        /// The elliptic curve Diffie-Hellman (ECDH) with cofactor key derivation mechanism
        /// </summary>
        CKM_ECDH1_COFACTOR_DERIVE = 0x00001051,

        /// <summary>
        /// The elliptic curve Menezes-Qu-Vanstone (ECMQV) key derivation mechanism
        /// </summary>
        CKM_ECMQV_DERIVE = 0x00001052,

        /// <summary>
        /// The JUNIPER key generation mechanism
        /// </summary>
        CKM_JUNIPER_KEY_GEN = 0x00001060,

        /// <summary>
        /// JUNIPER-ECB128 mechanism for encryption and decryption with JUNIPER in 128-bit electronic codebook mode (ECB)
        /// </summary>
        CKM_JUNIPER_ECB128 = 0x00001061,

        /// <summary>
        /// JUNIPER-CBC128 mechanism for encryption and decryption with JUNIPER in 128-bit cipher-block chaining mode (CBC)
        /// </summary>
        CKM_JUNIPER_CBC128 = 0x00001062,

        /// <summary>
        /// JUNIPER COUNTER mechanism for encryption and decryption with JUNIPER in counter mode
        /// </summary>
        CKM_JUNIPER_COUNTER = 0x00001063,

        /// <summary>
        /// JUNIPER-SHUFFLE mechanism for encryption and decryption with JUNIPER in shuffle mode
        /// </summary>
        CKM_JUNIPER_SHUFFLE = 0x00001064,

        /// <summary>
        /// The JUNIPER wrap and unwrap mechanism used to wrap and unwrap an MEK
        /// </summary>
        CKM_JUNIPER_WRAP = 0x00001065,

        /// <summary>
        /// The FASTHASH digesting mechanism
        /// </summary>
        CKM_FASTHASH = 0x00001070,

        /// <summary>
        /// The AES key generation mechanism
        /// </summary>
        CKM_AES_KEY_GEN = 0x00001080,

        /// <summary>
        /// AES-ECB encryption mechanism with electronic codebook mode (ECB)
        /// </summary>
        CKM_AES_ECB = 0x00001081,

        /// <summary>
        /// AES-CBC encryption mechanism with cipher-block chaining mode (CBC)
        /// </summary>
        CKM_AES_CBC = 0x00001082,

        /// <summary>
        /// Special case of general-length AES-MAC mechanism
        /// </summary>
        CKM_AES_MAC = 0x00001083,

        /// <summary>
        /// General-length AES-MAC mechanism based on data authentication as defined in FIPS PUB 113
        /// </summary>
        CKM_AES_MAC_GENERAL = 0x00001084,

        /// <summary>
        /// AES-CBC encryption mechanism with cipher-block chaining mode (CBC) and PKCS#7 padding
        /// </summary>
        CKM_AES_CBC_PAD = 0x00001085,

        /// <summary>
        /// AES-CTR encryption mechanism with AES in counter mode.
        /// </summary>
        CKM_AES_CTR = 0x00001086,
        
        /// <summary>
        /// The Blowfish key generation mechanism
        /// </summary>
        CKM_BLOWFISH_KEY_GEN = 0x00001090,

        /// <summary>
        /// Blowfish-CBC mechanism for encryption and decryption; key wrapping; and key unwrapping
        /// </summary>
        CKM_BLOWFISH_CBC = 0x00001091,

        /// <summary>
        /// The Twofish key generation mechanism
        /// </summary>
        CKM_TWOFISH_KEY_GEN = 0x00001092,

        /// <summary>
        /// Twofish-CBC mechanism for encryption and decryption; key wrapping; and key unwrapping
        /// </summary>
        CKM_TWOFISH_CBC = 0x00001093,

        /// <summary>
        /// Key derivation mechanism that uses the result of an DES-ECB encryption operation as the key value
        /// </summary>
        CKM_DES_ECB_ENCRYPT_DATA = 0x00001100,

        /// <summary>
        /// Key derivation mechanism that uses the result of an DES-CBC encryption operation as the key value
        /// </summary>
        CKM_DES_CBC_ENCRYPT_DATA = 0x00001101,

        /// <summary>
        /// Key derivation mechanism that uses the result of an DES3-ECB encryption operation as the key value
        /// </summary>
        CKM_DES3_ECB_ENCRYPT_DATA = 0x00001102,

        /// <summary>
        /// Key derivation mechanism that uses the result of an DES3-CBC encryption operation as the key value
        /// </summary>
        CKM_DES3_CBC_ENCRYPT_DATA = 0x00001103,

        /// <summary>
        /// Key derivation mechanism that uses the result of an AES-ECB encryption operation as the key value
        /// </summary>
        CKM_AES_ECB_ENCRYPT_DATA = 0x00001104,

        /// <summary>
        /// Key derivation mechanism that uses the result of an AES-CBC encryption operation as the key value
        /// </summary>
        CKM_AES_CBC_ENCRYPT_DATA = 0x00001105,
        
        /// <summary>
        /// The DSA domain parameter generation mechanism
        /// </summary>
        CKM_DSA_PARAMETER_GEN = 0x00002000,

        /// <summary>
        /// The PKCS #3 Diffie-Hellman domain parameter generation mechanism
        /// </summary>
        CKM_DH_PKCS_PARAMETER_GEN = 0x00002001,

        /// <summary>
        /// The X9.42 Diffie-Hellman domain parameter generation mechanism
        /// </summary>
        CKM_X9_42_DH_PARAMETER_GEN = 0x00002002,

        /// <summary>
        /// Permanently reserved for token vendors
        /// </summary>
        CKM_VENDOR_DEFINED = 0x80000000
    }
}
