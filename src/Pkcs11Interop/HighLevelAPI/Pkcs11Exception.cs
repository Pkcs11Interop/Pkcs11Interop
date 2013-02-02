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

namespace Net.Pkcs11Interop.HighLevelAPI
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
