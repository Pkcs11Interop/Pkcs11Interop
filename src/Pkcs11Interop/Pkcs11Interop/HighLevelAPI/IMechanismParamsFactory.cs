/*
 *  Copyright 2012-2017 The Pkcs11Interop Project
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

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI.MechanismParams;

namespace Net.Pkcs11Interop.HighLevelAPI
{
    /// <summary>
    /// Factory for creation of IMechanismParams instances
    /// </summary>
    public interface IMechanismParamsFactory
    {
        // TODO - Complete this interface
        IMechanismParams CreateCkAesCbcEncryptDataParams();
        IMechanismParams CreateCkAesCtrParams();
        IMechanismParams CreateCkAriaCbcEncryptDataParams();
        IMechanismParams CreateCkCamelliaCbcEncryptDataParams();
        IMechanismParams CreateCkCamelliaCtrParams();
        IMechanismParams CreateCkCcmParams();
        IMechanismParams CreateCkCmsSigParams();
        IMechanismParams CreateCkDesCbcEncryptDataParams();
        IMechanismParams CreateCkDsaParameterGenParam();
        IMechanismParams CreateCkEcdh1DeriveParams();
        IMechanismParams CreateCkEcdh2DeriveParams();
        IMechanismParams CreateCkEcdhAesKeyWrapParams();
        IMechanismParams CreateCkEcmqvDeriveParams();
        IMechanismParams CreateCkExtractParams();
        IMechanismParams CreateCkGcmParams();
        IMechanismParams CreateCkGostR3410DeriveParams();
        IMechanismParams CreateCkGostR3410KeyWrapParams();
        IMechanismParams CreateCkKeaDeriveParams();
        IMechanismParams CreateCkKeyDerivationStringData();
        IMechanismParams CreateCkKeyWrapSetOaepParams();
        IMechanismParams CreateCkKipParams();
        IMechanismParams CreateCkMacGeneralParams();
        IMechanismParams CreateCkOtpParam();
        IMechanismParams CreateCkOtpParams();
        IMechanismParams CreateCkOtpSignatureInfo();
        IMechanismParams CreateCkPbeParams();
        IMechanismParams CreateCkPkcs5Pbkd2Params();
        IMechanismParams CreateCkPkcs5Pbkd2Params2();
        IMechanismParams CreateCkRc2CbcParams();
        IMechanismParams CreateCkRc2MacGeneralParams();
        IMechanismParams CreateCkRc2Params();
        IMechanismParams CreateCkRc5CbcParams();
        IMechanismParams CreateCkRc5MacGeneralParams();
        IMechanismParams CreateCkRc5Params();
        IMechanismParams CreateCkRsaAesKeyWrapParams();
        IMechanismParams CreateCkRsaPkcsOaepParams();
        IMechanismParams CreateCkRsaPkcsPssParams();
        IMechanismParams CreateCkSeedCbcEncryptDataParams();
        IMechanismParams CreateCkSkipjackPrivateWrapParams();
        IMechanismParams CreateCkSkipjackRelayxParams();
        IMechanismParams CreateCkSsl3KeyMatOut();
        IMechanismParams CreateCkSsl3KeyMatParams();
        IMechanismParams CreateCkSsl3MasterKeyDeriveParams();
        IMechanismParams CreateCkSsl3RandomData();
        IMechanismParams CreateCkTls12KeyMatParams();
        IMechanismParams CreateCkTls12MasterKeyDeriveParams();
        IMechanismParams CreateCkTlsKdfParams();
        IMechanismParams CreateCkTlsMacParams();
        IMechanismParams CreateCkTlsPrfParams();
        IMechanismParams CreateCkVersion();
        IMechanismParams CreateCkWtlsKeyMatOut();
        IMechanismParams CreateCkWtlsKeyMatParams();
        IMechanismParams CreateCkWtlsMasterKeyDeriveParams();
        IMechanismParams CreateCkWtlsPrfParams();
        IMechanismParams CreateCkWtlsRandomData();
        IMechanismParams CreateCkX942Dh1DeriveParams();
        IMechanismParams CreateCkX942Dh2DeriveParams();
        IMechanismParams CreateCkX942MqvDeriveParams();
    }
}
