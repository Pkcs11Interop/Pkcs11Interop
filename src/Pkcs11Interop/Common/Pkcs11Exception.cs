/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2013 JWC s.r.o. <http://www.jwc.sk>
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

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Exception with the name of PKCS#11 method that failed and its return value
    /// </summary>
    public class Pkcs11Exception : Exception
    {
        /// <summary>
        /// Name of method that caused exception
        /// </summary>
        private string _method = null;

        /// <summary>
        /// Name of method that caused exception
        /// </summary>
        public string Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Return value of method that caused exception
        /// </summary>
        private CKR _rv = CKR.CKR_OK;

        /// <summary>
        /// Return value of method that caused exception
        /// </summary>
        public CKR RV
        {
            get
            {
                return _rv;
            }
        }

        /// <summary>
        /// Initializes new instance of Pkcs11Exception class
        /// </summary>
        /// <param name="method">Name of method that caused exception</param>
        /// <param name="rv">Return value of method that caused exception</param>
        public Pkcs11Exception(string method, CKR rv)
            : base(string.Format("Method {0} returned {1}", method, rv.ToString()))
        {
            _method = method;
            _rv = rv;
        }
    }
}
