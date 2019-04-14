/*
 *  Copyright 2012-2019 The Pkcs11Interop Project
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
using System.Collections.Generic;
using System.Reflection;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Net.Pkcs11Interop.Mock.HighLevelAPI;
using NUnit.Framework;
using LLA40 = Net.Pkcs11Interop.LowLevelAPI40;
using LLA41 = Net.Pkcs11Interop.LowLevelAPI41;
using LLA80 = Net.Pkcs11Interop.LowLevelAPI80;
using LLA81 = Net.Pkcs11Interop.LowLevelAPI81;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.HighLevelAPI
{
    /// <summary>
    /// Tests the ability to extend classes with a vendor specific functions
    /// </summary>
    [TestFixture()]
    public class _28_VendorExtensionsTest
    {
        /// <summary>
        /// Factories from Pkcs11Interop.Mock library
        /// </summary>
        private MockPkcs11InteropFactories _mockFactories = new MockPkcs11InteropFactories();

        /// <summary>
        /// C_GetUnmanagedStructSizeList vendor specific function test
        /// </summary>
        [Test()]
        public void _01_StructSizeListTest()
        {
            using (IPkcs11Library pkcs11 = _mockFactories.Pkcs11LibraryFactory.CreatePkcs11Library(_mockFactories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                ILibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Obtain a list of unmanaged struct sizes via vendor specific function C_GetUnmanagedStructSizeList
                List<ulong> unmanagedSizes = ((IMockPkcs11Library)pkcs11).GetUnmanagedStructSizeList();

                // Obtain a list of managed struct sizes
                List<ulong> managedSizes = GetManagedStructSizeList();

                // Compare sizes of unmanaged and managed structs
                Assert.IsTrue(unmanagedSizes.Count == managedSizes.Count);
                for (int i = 0; i < unmanagedSizes.Count; i++)
                    Assert.IsTrue(unmanagedSizes[i] == managedSizes[i]);
            }
        }

        /// <summary>
        /// C_EjectToken vendor specific function test
        /// </summary>
        [Test()]
        public void _02_EjectTokenTest()
        {
            using (IPkcs11Library pkcs11 = _mockFactories.Pkcs11LibraryFactory.CreatePkcs11Library(_mockFactories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                ILibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);

                // Eject token via vendor specific function C_EjectToken
                ((IMockSlot)slot).EjectToken();
            }
        }

        /// <summary>
        /// C_InteractiveLogin vendor specific function test
        /// </summary>
        [Test()]
        public void _03_InteractiveLoginTest()
        {
            using (IPkcs11Library pkcs11 = _mockFactories.Pkcs11LibraryFactory.CreatePkcs11Library(_mockFactories, Settings.Pkcs11LibraryPath, Settings.AppType))
            {
                ILibraryInfo libraryInfo = pkcs11.GetInfo();
                if (libraryInfo.LibraryDescription != "Mock module" && libraryInfo.ManufacturerId != "Pkcs11Interop Project")
                    Assert.Inconclusive("Test cannot be executed with this PKCS#11 library");

                // Find first slot with token present
                ISlot slot = Helpers.GetUsableSlot(pkcs11);

                // Open RO session
                using (ISession session = slot.OpenSession(SessionType.ReadOnly))
                {
                    // Login interactively via vendor specific function C_EjectToken
                    ((IMockSession)session).InteractiveLogin();
                }
            }
        }

        /// <summary>
        /// Obtains a list of managed struct sizes.
        /// </summary>
        /// <returns>List of managed struct sizes</returns>
        private List<ulong> GetManagedStructSizeList()
        {
            List<ulong> sizeList = new List<ulong>();

            if (Platform.NativeULongSize == 4)
            {
                if (Platform.StructPackingSize == 0)
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_C_INITIALIZE_ARGS))));
#if NETCOREAPP1_0
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_INFO).GetTypeInfo().Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI40.CK_FUNCTION_LIST"))));
#else
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI40.CK_FUNCTION_LIST"))));
#endif
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA40.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
                else
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_C_INITIALIZE_ARGS))));
#if NETCOREAPP1_0
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_INFO).GetTypeInfo().Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI41.CK_FUNCTION_LIST"))));
#else
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI41.CK_FUNCTION_LIST"))));
#endif
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA41.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
            }
            else
            {
                if (Platform.StructPackingSize == 0)
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_C_INITIALIZE_ARGS))));
#if NETCOREAPP1_0
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_INFO).GetTypeInfo().Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI80.CK_FUNCTION_LIST"))));
#else
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI80.CK_FUNCTION_LIST"))));
#endif
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA80.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
                else
                {
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_ATTRIBUTE))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_C_INITIALIZE_ARGS))));
#if NETCOREAPP1_0
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_INFO).GetTypeInfo().Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI81.CK_FUNCTION_LIST"))));
#else
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_INFO).Assembly.GetType("Net.Pkcs11Interop.LowLevelAPI81.CK_FUNCTION_LIST"))));
#endif
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_MECHANISM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_MECHANISM_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_SESSION_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_SLOT_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_TOKEN_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.CK_VERSION))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_AES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_AES_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ARIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CAMELLIA_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CAMELLIA_CTR_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_CMS_SIG_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_DES_CBC_ENCRYPT_DATA_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECDH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECDH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_ECMQV_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_EXTRACT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEA_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEY_DERIVATION_STRING_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KEY_WRAP_SET_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_KIP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_PARAM))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_OTP_SIGNATURE_INFO))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_PBE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_PKCS5_PBKD2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC2_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_CBC_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_MAC_GENERAL_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RC5_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RSA_PKCS_OAEP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_RSA_PKCS_PSS_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SKIPJACK_PRIVATE_WRAP_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SKIPJACK_RELAYX_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_SSL3_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_TLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_KEY_MAT_OUT))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_KEY_MAT_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_MASTER_KEY_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_PRF_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_WTLS_RANDOM_DATA))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_DH1_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_DH2_DERIVE_PARAMS))));
                    sizeList.Add(Convert.ToUInt64(UnmanagedMemory.SizeOf(typeof(LLA81.MechanismParams.CK_X9_42_MQV_DERIVE_PARAMS))));
                }
            }

            return sizeList;
        }
    }
}
