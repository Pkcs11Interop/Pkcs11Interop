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
 */

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// Notifications
    /// </summary>
    public enum CKN : uint
    {
        /// <summary>
        /// Cryptoki is surrendering the execution of a function executing in a session so that the application may perform other operations
        /// </summary>
        CKN_SURRENDER = 0,

        /// <summary>
        /// Cryptoki is informing the application that the OTP for a key on a connected token just changed
        /// </summary>
        CKN_OTP_CHANGED = 1
    }
}
