/*
 *  Pkcs11Interop - Open-source .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o.
 *  Author: Jaroslav Imrich
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License version 3
 *  as published by the Free Software Foundation.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program. If not, see <http://www.gnu.org/licenses/>.
 * 
 *  You can be released from the requirements of the license by purchasing
 *  a commercial license. Buying such a license is mandatory as soon as you
 *  develop commercial activities involving the Pkcs11Interop software without
 *  disclosing the source code of your own applications.
 * 
 *  For more information, please contact JWC s.r.o. at info@pkcs11interop.net
 */

using NUnit.Framework;
using System;
using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.LowLevelAPI;
using System.Text;
using System.IO;

namespace Net.Pkcs11Interop.Tests.LowLevelAPI
{
    /// <summary>
    /// C_DigestEncryptUpdate and C_DecryptDigestUpdate tests.
    /// </summary>
    [TestFixture()]
    public class DigestEncryptAndDecryptDigestTest
    {
        /// <summary>
        /// Basic C_DigestEncryptUpdate and C_DecryptDigestUpdate test.
        /// </summary>
        [Test()]
        public void BasicDigestEncryptAndDecryptDigestTest()
        {
            CKR rv = CKR.CKR_OK;
            
            using (Pkcs11 pkcs11 = new Pkcs11(Settings.Pkcs11LibraryPath))
            {
                rv = pkcs11.C_Initialize(null);
                if ((rv != CKR.CKR_OK) && (rv != CKR.CKR_CRYPTOKI_ALREADY_INITIALIZED))
                    Assert.Fail(rv.ToString());
                
                // Find first slot with token present
                uint slotId = Helpers.GetUsableSlot(pkcs11);
                
                uint session = CK.CK_INVALID_HANDLE;
                rv = pkcs11.C_OpenSession(slotId, (CKF.CKF_SERIAL_SESSION | CKF.CKF_RW_SESSION), IntPtr.Zero, IntPtr.Zero, ref session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Login as normal user
                rv = pkcs11.C_Login(session, CKU.CKU_USER, Settings.NormalUserPinArray, (uint)Settings.NormalUserPinArray.Length);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate symetric key
                uint keyId = CK.CK_INVALID_HANDLE;
                rv = Helpers.GenerateKey(pkcs11, session, ref keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                // Generate random initialization vector
                byte[] iv = new byte[8];
                rv = pkcs11.C_GenerateRandom(session, iv, (uint)iv.Length);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());

                // Specify encryption mechanism with initialization vector as parameter.
                // Note that CkmUtils.CreateMechanism() automaticaly copies iv into newly allocated unmanaged memory.
                CK_MECHANISM encryptionMechanism = CkmUtils.CreateMechanism(CKM.CKM_DES3_CBC, iv);

                // Specify digesting mechanism (needs no parameter => no unamanaged memory is needed)
                CK_MECHANISM digestingMechanism = CkmUtils.CreateMechanism(CKM.CKM_SHA_1);
                
                byte[] sourceData = ConvertUtils.Utf8StringToBytes("Our new password");
                byte[] encryptedData = null;
                byte[] digest1 = null;
                byte[] decryptedData = null;
                byte[] digest2 = null;
                
                // Multipart digesting and encryption function C_DigestEncryptUpdate can be used i.e. for digesting and encryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(sourceData), outputStream = new MemoryStream())
                {
                    // Initialize digesting operation
                    rv = pkcs11.C_DigestInit(session, ref digestingMechanism);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Initialize encryption operation
                    rv = pkcs11.C_EncryptInit(session, ref encryptionMechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for source data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    
                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    uint encryptedPartLen = (uint)encryptedPart.Length;
                    
                    // Read input stream with source data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(part, 0, part.Length)) > 0)
                    {
                        // Process each individual source data part
                        encryptedPartLen = (uint)encryptedPart.Length;
                        rv = pkcs11.C_DigestEncryptUpdate(session, part, (uint)bytesRead, encryptedPart, ref encryptedPartLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append encrypted data part to the output stream
                        outputStream.Write(encryptedPart, 0, (int)encryptedPartLen);
                    }

                    // Get length of digest value in first call
                    uint digestLen = 0;
                    rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    ;
                    
                    Assert.IsTrue(digestLen > 0);
                    
                    // Allocate array for digest value
                    digest1 = new byte[digestLen];
                    
                    // Get digest value in second call
                    rv = pkcs11.C_DigestFinal(session, digest1, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Get the length of last encrypted data part in first call
                    byte[] lastEncryptedPart = null;
                    uint lastEncryptedPartLen = 0;
                    rv = pkcs11.C_EncryptFinal(session, null, ref lastEncryptedPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Allocate array for the last encrypted data part
                    lastEncryptedPart = new byte[lastEncryptedPartLen];
                    
                    // Get the last encrypted data part in second call
                    rv = pkcs11.C_EncryptFinal(session, lastEncryptedPart, ref lastEncryptedPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Append the last encrypted data part to the output stream
                    outputStream.Write(lastEncryptedPart, 0, (int)lastEncryptedPartLen);
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    encryptedData = outputStream.ToArray();
                }
                
                // Do something interesting with encrypted data and digest
                
                // Multipart decryption and digesting function C_DecryptDigestUpdate can be used i.e. for digesting and decryption of streamed data
                using (MemoryStream inputStream = new MemoryStream(encryptedData), outputStream = new MemoryStream())
                {
                    // Initialize decryption operation
                    rv = pkcs11.C_DecryptInit(session, ref encryptionMechanism, keyId);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Initialize digesting operation
                    rv = pkcs11.C_DigestInit(session, ref digestingMechanism);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());

                    // Prepare buffer for encrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] encryptedPart = new byte[8];
                    
                    // Prepare buffer for decrypted data part
                    // Note that in real world application we would rather use bigger buffer i.e. 4096 bytes long
                    byte[] part = new byte[8];
                    uint partLen = (uint)part.Length;
                    
                    // Read input stream with encrypted data
                    int bytesRead = 0;
                    while ((bytesRead = inputStream.Read(encryptedPart, 0, encryptedPart.Length)) > 0)
                    {
                        // Process each individual encrypted data part
                        partLen = (uint)part.Length;
                        rv = pkcs11.C_DecryptDigestUpdate(session, encryptedPart, (uint)bytesRead, part, ref partLen);
                        if (rv != CKR.CKR_OK)
                            Assert.Fail(rv.ToString());
                        
                        // Append decrypted data part to the output stream
                        outputStream.Write(part, 0, (int)partLen);
                    }
                    
                    // Get the length of last decrypted data part in first call
                    byte[] lastPart = null;
                    uint lastPartLen = 0;
                    rv = pkcs11.C_DecryptFinal(session, null, ref lastPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Allocate array for the last decrypted data part
                    lastPart = new byte[lastPartLen];
                    
                    // Get the last decrypted data part in second call
                    rv = pkcs11.C_DecryptFinal(session, lastPart, ref lastPartLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    
                    // Append the last decrypted data part to the output stream
                    outputStream.Write(lastPart, 0, (int)lastPartLen);
                    
                    // Read whole output stream to the byte array so we can compare results more easily
                    decryptedData = outputStream.ToArray();

                    // Get length of digest value in first call
                    uint digestLen = 0;
                    rv = pkcs11.C_DigestFinal(session, null, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                    ;
                    
                    Assert.IsTrue(digestLen > 0);
                    
                    // Allocate array for digest value
                    digest2 = new byte[digestLen];
                    
                    // Get digest value in second call
                    rv = pkcs11.C_DigestFinal(session, digest2, ref digestLen);
                    if (rv != CKR.CKR_OK)
                        Assert.Fail(rv.ToString());
                }
                
                // Do something interesting with decrypted data and digest
                Assert.IsTrue(Convert.ToBase64String(sourceData) == Convert.ToBase64String(decryptedData));
                Assert.IsTrue(Convert.ToBase64String(digest1) == Convert.ToBase64String(digest2));
                
                // In LowLevelAPI we have to free unmanaged memory taken by mechanism parameter (iv in this case)
                UnmanagedMemory.Free(ref encryptionMechanism.Parameter);
                encryptionMechanism.ParameterLen = 0;
                
                rv = pkcs11.C_DestroyObject(session, keyId);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Logout(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_CloseSession(session);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
                
                rv = pkcs11.C_Finalize(IntPtr.Zero);
                if (rv != CKR.CKR_OK)
                    Assert.Fail(rv.ToString());
            }
        }
    }
}

