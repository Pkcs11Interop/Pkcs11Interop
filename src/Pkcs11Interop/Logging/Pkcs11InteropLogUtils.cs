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

using Net.Pkcs11Interop.Common;

// Note: Code in this file is maintained manually.

namespace Net.Pkcs11Interop.Logging
{
    /// <summary>
    /// Utility class that helps with logging
    /// </summary>
    public static class Pkcs11InteropLogUtils
    {
        /// <summary>
        /// Converts CKU enum member to string that can be logged
        /// </summary>
        /// <param name="userType">CKU enum member</param>
        /// <returns>String value representing CKU enum member</returns>
        public static string ToString(CKU userType)
        {
            switch (userType)
            {
                case CKU.CKU_SO:
                    return "security officer";
                case CKU.CKU_USER:
                    return "normal user";
                case CKU.CKU_CONTEXT_SPECIFIC:
                    return "context specific user";
                default:
                    return userType.ToString();
            }
        }

        /// <summary>
        /// Converts SessionType enum member to string that can be logged
        /// </summary>
        /// <param name="sessionType">SessionType enum member</param>
        /// <returns>String value representing SessionType enum member</returns>
        public static string ToString(SessionType sessionType)
        {
            switch (sessionType)
            {
                case SessionType.ReadOnly:
                    return "read-only";
                case SessionType.ReadWrite:
                    return "read-write";
                default:
                    return sessionType.ToString();
            }
        }
    }
}
