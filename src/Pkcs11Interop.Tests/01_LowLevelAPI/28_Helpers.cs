/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (C) 2012 Jaroslav Imrich <jimrich(at)jimrich(dot)sk>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  If this license does not suit your needs you can purchase a commercial
 *  license from Pkcs11Interop author.
 */

using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;
using NUnit.Framework;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// Helper methods for LowLevelAPI tests.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Finds first slot with token present
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <returns>First slot with token present</returns>
        public static uint GetUsableSlot(Pkcs11 pkcs11)
        {
            CKR rv = CKR.CKR_OK;
            
            // Get number of slots in first call
            uint slotCount = 0;
            rv = pkcs11.C_GetSlotList(true, null, ref slotCount);
            if (rv != CKR.CKR_OK)
                Assert.Fail(rv.ToString());
            
            Assert.IsTrue(slotCount > 0);
            
            // Allocate array for slot IDs
            uint[] slotList = new uint[slotCount];
            
            // Get slot IDs in second call
            rv = pkcs11.C_GetSlotList(true, slotList, ref slotCount);
            if (rv != CKR.CKR_OK)
                Assert.Fail(rv.ToString());
            
            // Let's use first slot with token present
            return slotList[0];
        }

        /// <summary>
        /// Creates the data object.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='objectId'>Output parameter for data object handle</param>
        /// <returns>Return value of C_CreateObject</returns>
        public static CKR CreateDataObject(Pkcs11 pkcs11, uint session, ref uint objectId)
        {
            CKR rv = CKR.CKR_OK;

            // Prepare attribute template of new data object
            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[5];
            template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_DATA);
            template[1] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            template[2] = CkaUtils.CreateAttribute(CKA.CKA_APPLICATION, Settings.ApplicationName);
            template[3] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            template[4] = CkaUtils.CreateAttribute(CKA.CKA_VALUE, "Data object content");
            
            // Create object
            rv = pkcs11.C_CreateObject(session, template, (uint)template.Length, ref objectId);

            // In LowLevelAPI caller has to free unmanaged memory taken by attributes
            for (int i = 0; i < template.Length; i++)
            {
                UnmanagedMemory.Free(ref template[i].value);
                template[i].valueLen = 0;
            }

            return rv;
        }

        /// <summary>
        /// Generates symetric key.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='keyId'>Output parameter for key object handle</param>
        /// <returns>Return value of C_GenerateKey</returns>
        public static CKR GenerateKey(Pkcs11 pkcs11, uint session, ref uint keyId)
        {
            CKR rv = CKR.CKR_OK;

            // Prepare attribute template of new key
            CK_ATTRIBUTE[] template = new CK_ATTRIBUTE[4];
            template[0] = CkaUtils.CreateAttribute(CKA.CKA_CLASS, (uint)CKO.CKO_SECRET_KEY);
            template[1] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
            template[2] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
            template[3] = CkaUtils.CreateAttribute(CKA.CKA_DERIVE, true);
            
            // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_KEY_GEN);
            
            // Generate key
            rv = pkcs11.C_GenerateKey(session, ref mechanism, template, (uint)template.Length, ref keyId);

            // In LowLevelAPI we have to free unmanaged memory taken by attributes
            for (int i = 0; i < template.Length; i++)
            {
                UnmanagedMemory.Free(ref template[i].value);
                template[i].valueLen = 0;
            }

            return rv;
        }

        /// <summary>
        /// Generates asymetric key pair.
        /// </summary>
        /// <param name='pkcs11'>Initialized PKCS11 wrapper</param>
        /// <param name='session'>Read-write session with user logged in</param>
        /// <param name='pubKeyId'>Output parameter for public key object handle</param>
        /// <param name='privKeyId'>Output parameter for private key object handle</param>
        /// <returns>Return value of C_GenerateKeyPair</returns>
        public static CKR GenerateKeyPair(Pkcs11 pkcs11, uint session, ref uint pubKeyId, ref uint privKeyId)
        {
            CKR rv = CKR.CKR_OK;

            // The CKA_ID attribute is intended as a means of distinguishing multiple key pairs held by the same subject
            byte[] ckaId = new byte[20];
            rv = pkcs11.C_GenerateRandom(session, ckaId, (uint)ckaId.Length);
            if (rv != CKR.CKR_OK)
                return rv;
            
            // Prepare attribute template of new public key
            CK_ATTRIBUTE[] pubKeyTemplate = new CK_ATTRIBUTE[10];
            pubKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            pubKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, false);
            pubKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            pubKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
            pubKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_ENCRYPT, true);
            pubKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY, true);
            pubKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_VERIFY_RECOVER, true);
            pubKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_WRAP, true);
            pubKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_MODULUS_BITS, 1024);
            pubKeyTemplate[9] = CkaUtils.CreateAttribute(CKA.CKA_PUBLIC_EXPONENT, new byte[] { 0x01, 0x00, 0x01 });
            
            // Prepare attribute template of new private key
            CK_ATTRIBUTE[] privKeyTemplate = new CK_ATTRIBUTE[9];
            privKeyTemplate[0] = CkaUtils.CreateAttribute(CKA.CKA_TOKEN, true);
            privKeyTemplate[1] = CkaUtils.CreateAttribute(CKA.CKA_PRIVATE, true);
            privKeyTemplate[2] = CkaUtils.CreateAttribute(CKA.CKA_LABEL, Settings.ApplicationName);
            privKeyTemplate[3] = CkaUtils.CreateAttribute(CKA.CKA_ID, ckaId);
            privKeyTemplate[4] = CkaUtils.CreateAttribute(CKA.CKA_SENSITIVE, true);
            privKeyTemplate[5] = CkaUtils.CreateAttribute(CKA.CKA_DECRYPT, true);
            privKeyTemplate[6] = CkaUtils.CreateAttribute(CKA.CKA_SIGN, true);
            privKeyTemplate[7] = CkaUtils.CreateAttribute(CKA.CKA_SIGN_RECOVER, true);
            privKeyTemplate[8] = CkaUtils.CreateAttribute(CKA.CKA_UNWRAP, true);
            
            // Specify key generation mechanism (needs no parameter => no unamanaged memory is needed)
            CK_MECHANISM mechanism = CkmUtils.CreateMechanism(CKM.CKM_RSA_PKCS_KEY_PAIR_GEN);
            
            // Generate key pair
            rv = pkcs11.C_GenerateKeyPair(session, ref mechanism, pubKeyTemplate, (uint)pubKeyTemplate.Length, privKeyTemplate, (uint)privKeyTemplate.Length, ref pubKeyId, ref privKeyId);

            // In LowLevelAPI we have to free unmanaged memory taken by attributes
            for (int i = 0; i < privKeyTemplate.Length; i++)
            {
                UnmanagedMemory.Free(ref privKeyTemplate[i].value);
                privKeyTemplate[i].valueLen = 0;
            }
            
            for (int i = 0; i < pubKeyTemplate.Length; i++)
            {
                UnmanagedMemory.Free(ref pubKeyTemplate[i].value);
                pubKeyTemplate[i].valueLen = 0;
            }

            return rv;
        }
    }
}

