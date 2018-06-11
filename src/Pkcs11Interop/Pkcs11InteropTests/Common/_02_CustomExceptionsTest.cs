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

#if (!NETCOREAPP1_0)

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Net.Pkcs11Interop.Common;
using NUnit.Framework;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Tests.Common
{
    /// <summary>
    /// Exception serialization tests.
    /// </summary>
    [TestFixture()]
    public class _02_CustomExceptionsTest
    {
        /// <summary>
        /// Serialization test for Pkcs11Exception class
        /// </summary>
        [Test()]
        public void _01_Pkcs11ExceptionSerializationTest()
        {
            Pkcs11Exception exception1 = new Pkcs11Exception("Hello world!", CKR.CKR_GENERAL_ERROR);
            Assert.IsTrue(exception1.Method == "Hello world!");
            Assert.IsTrue(exception1.RV == CKR.CKR_GENERAL_ERROR);

            Pkcs11Exception exception2 = SerializeAndDeserializeException<Pkcs11Exception>(exception1);
            Assert.IsTrue(exception2.Message == exception1.Message);
            Assert.IsTrue(exception2.Method == exception1.Method);
            Assert.IsTrue(exception2.RV == exception1.RV);
            Assert.IsTrue(exception2.ToString() == exception1.ToString());
        }

        /// <summary>
        /// Serialization test for Pkcs11UriException class
        /// </summary>
        [Test()]
        public void _02_Pkcs11UriExceptionSerializationTest()
        {
            Pkcs11UriException exception1 = new Pkcs11UriException("Hello world!");
            Assert.IsTrue(exception1.Message == "Hello world!");

            Pkcs11UriException exception2 = SerializeAndDeserializeException<Pkcs11UriException>(exception1);
            Assert.IsTrue(exception2.Message == exception1.Message);
            Assert.IsTrue(exception2.ToString() == exception1.ToString());
        }

        /// <summary>
        /// Serialization test for UnmanagedException class with ErrorCode set
        /// </summary>
        [Test()]
        public void _03_UnmanagedExceptionWithErrorCodeSerializationTest()
        {
            UnmanagedException exception1 = new UnmanagedException("Hello world!", 1);
            Assert.IsTrue(exception1.Message == "Hello world!");
            Assert.IsTrue(exception1.ErrorCode == 1);

            UnmanagedException exception2 = SerializeAndDeserializeException<UnmanagedException>(exception1);
            Assert.IsTrue(exception2.Message == exception1.Message);
            Assert.IsTrue(exception2.ErrorCode == exception1.ErrorCode);
        }

        /// <summary>
        /// Serialization test for UnmanagedException class with ErrorCode not set
        /// </summary>
        [Test()]
        public void _04_UnmanagedExceptionWithoutErrorCodeSerializationTest()
        {
            UnmanagedException exception1 = new UnmanagedException("Hello world!");
            Assert.IsTrue(exception1.Message == "Hello world!");
            Assert.IsTrue(exception1.ErrorCode == null);

            UnmanagedException exception2 = SerializeAndDeserializeException<UnmanagedException>(exception1);
            Assert.IsTrue(exception2.Message == exception1.Message);
            Assert.IsTrue(exception2.ErrorCode == exception1.ErrorCode);
        }

        /// <summary>
        /// Serialization test for UnsupportedPlatformException class
        /// </summary>
        [Test()]
        public void _05_UnsupportedPlatformExceptionSerializationTest()
        {
            UnsupportedPlatformException exception1 = new UnsupportedPlatformException("Hello world!");
            Assert.IsTrue(exception1.Message == "Hello world!");

            UnsupportedPlatformException exception2 = SerializeAndDeserializeException<UnsupportedPlatformException>(exception1);
            Assert.IsTrue(exception2.Message == exception1.Message);
            Assert.IsTrue(exception2.ToString() == exception1.ToString());
        }

        /// <summary>
        /// Serializes and deserializes exception
        /// </summary>
        /// <typeparam name="T">Exception type</typeparam>
        /// <param name="o">Exception to be serialized</param>
        /// <returns>Deserialized exception</returns>
        private T SerializeAndDeserializeException<T>(object o)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, o);
                ms.Seek(0, 0);
                return (T)bf.Deserialize(ms);
            }
        }
    }
}

#endif
