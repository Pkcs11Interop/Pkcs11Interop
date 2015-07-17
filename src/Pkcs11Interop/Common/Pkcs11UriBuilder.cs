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

using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// PKCS#11 URI builder
    /// 
    /// Implementation notes:
    /// - As recommended by PKCS#11 URI specification Pkcs11UriBuilder class 
    ///   percent-encodes the whole value of the "id" attribute which is supposed 
    ///   to be handled as arbitrary binary data. Therefore it is not possible to 
    ///   construct URIs with arbitrary string value of the "id" attribute.
    /// - Validation of each individual attribute value is performed by the setter 
    ///   of corresponding Pkcs11UriBuilder class property with the exception to 
    ///   UnknownPathAttributes and UnknownQueryAttributes properties whose values 
    ///   are validated when ToString() or ToPkcs11Uri() method is called.
    /// </summary>
    public class Pkcs11UriBuilder
    {
        #region Constructors

        /// <summary>
        /// Intializes new instance of Pkcs11UriBuilder class that checks max lengths of path attribute values
        /// </summary>
        public Pkcs11UriBuilder()
            : this(true)
        {

        }

        /// <summary>
        /// Intializes new instance of Pkcs11UriBuilder class
        /// </summary>
        /// <param name="checkLengths">Flag indicating whether max lengths of path attribute values should be checked</param>
        public Pkcs11UriBuilder(bool checkLengths)
        {
            _checkLengths = checkLengths;
        }

        /// <summary>
        /// Intializes new instance of Pkcs11UriBuilder class with specified PKCS#11 URI whose ChecksLengths property specifies whether max lengths of path attribute values should be checked
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI with default values</param>
        public Pkcs11UriBuilder(Pkcs11Uri pkcs11Uri)
        {
            if (pkcs11Uri == null)
                throw new ArgumentException("pkcs11Uri");

            ConstructFromPkcs11Uri(pkcs11Uri, pkcs11Uri.ChecksLengths);
        }

        /// <summary>
        /// Intializes new instance of Pkcs11UriBuilder class with specified PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI with default values</param>
        /// <param name="checkLengths">Flag indicating whether max lengths of path attribute values should be checked</param>
        public Pkcs11UriBuilder(Pkcs11Uri pkcs11Uri, bool checkLengths)
        {
            if (pkcs11Uri == null)
                throw new ArgumentException("pkcs11Uri");

            ConstructFromPkcs11Uri(pkcs11Uri, checkLengths);
        }

        /// <summary>
        /// Sets properties of Pkcs11UriBuilder class with default values specified by PKCS#11 URI
        /// </summary>
        /// <param name="pkcs11Uri">PKCS#11 URI with default values</param>
        /// <param name="checkLengths">Flag indicating whether max lengths of path attribute values should be checked</param>
        private void ConstructFromPkcs11Uri(Pkcs11Uri pkcs11Uri, bool checkLengths)
        {
            if (pkcs11Uri == null)
                throw new ArgumentException("pkcs11Uri");

            _checkLengths = checkLengths;

            Token = pkcs11Uri.Token;
            Manufacturer = pkcs11Uri.Manufacturer;
            Serial = pkcs11Uri.Serial;
            Model = pkcs11Uri.Model;
            LibraryManufacturer = pkcs11Uri.LibraryManufacturer;
            LibraryDescription = pkcs11Uri.LibraryDescription;
            LibraryVersion = pkcs11Uri.LibraryVersion;
            Object = pkcs11Uri.Object;
            Type = pkcs11Uri.Type;
            Id = pkcs11Uri.Id;
            SlotManufacturer = pkcs11Uri.SlotManufacturer;
            SlotDescription = pkcs11Uri.SlotDescription;
            SlotId = pkcs11Uri.SlotId;
            UnknownPathAttributes = pkcs11Uri.UnknownPathAttributes;

            PinSource = pkcs11Uri.PinSource;
            PinValue = pkcs11Uri.PinValue;
            ModuleName = pkcs11Uri.ModuleName;
            ModulePath = pkcs11Uri.ModulePath;
            UnknownQueryAttributes = pkcs11Uri.UnknownQueryAttributes;
        }

        #endregion

        #region Properties and variables

        /// <summary>
        /// Flag indicating whether max lengths of path attribute values are checked
        /// </summary>
        private bool _checkLengths = true;

        /// <summary>
        /// Flag indicating whether max lengths of path attribute values are checked
        /// </summary>
        public bool ChecksLengths
        {
            get
            {
                return _checkLengths;
            }
        }

        #region Path attributes

        /// <summary>
        /// Value of path attribute "token" encoded for PKCS#11 URI
        /// </summary>
        private string _tokenEncoded = null;

        /// <summary>
        /// Value of path attribute "token" that corresponds to the "label" member of the CK_TOKEN_INFO structure
        /// </summary>
        private string _token = null;

        /// <summary>
        /// Value of path attribute "token" that corresponds to the "label" member of the CK_TOKEN_INFO structure
        /// </summary>
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _token = value;
                    _tokenEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Token;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11TokenMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _tokenEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _token = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "manufacturer" encoded for PKCS#11 URI
        /// </summary>
        private string _manufacturerEncoded = null;

        /// <summary>
        /// Value of path attribute "manufacturer" that corresponds to the "manufacturerID" member of CK_TOKEN_INFO structure
        /// </summary>
        private string _manufacturer = null;

        /// <summary>
        /// Value of path attribute "manufacturer" that corresponds to the "manufacturerID" member of CK_TOKEN_INFO structure
        /// </summary>
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _manufacturer = value;
                    _manufacturerEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Manuf;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11ManufMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _manufacturerEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _manufacturer = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "serial" encoded for PKCS#11 URI
        /// </summary>
        private string _serialEncoded = null;

        /// <summary>
        /// Value of path attribute "serial" that corresponds to the "serialNumber" member of CK_TOKEN_INFO structure
        /// </summary>
        private string _serial = null;

        /// <summary>
        /// Value of path attribute "serial" that corresponds to the "serialNumber" member of CK_TOKEN_INFO structure
        /// </summary>
        public string Serial
        {
            get
            {
                return _serial;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _serial = value;
                    _serialEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Serial;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11SerialMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _serialEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _serial = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "model" encoded for PKCS#11 URI
        /// </summary>
        private string _modelEncoded = null;

        /// <summary>
        /// Value of path attribute "model" that corresponds to the "model" member of CK_TOKEN_INFO structure
        /// </summary>
        private string _model = null;

        /// <summary>
        /// Value of path attribute "model" that corresponds to the "model" member of CK_TOKEN_INFO structure
        /// </summary>
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _model = value;
                    _modelEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Model;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11ModelMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _modelEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _model = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "library-manufacturer" encoded for PKCS#11 URI
        /// </summary>
        private string _libraryManufacturerEncoded = null;

        /// <summary>
        /// Value of path attribute "library-manufacturer" that corresponds to the "manufacturerID" member of CK_INFO structure
        /// </summary>
        private string _libraryManufacturer = null;

        /// <summary>
        /// Value of path attribute "library-manufacturer" that corresponds to the "manufacturerID" member of CK_INFO structure
        /// </summary>
        public string LibraryManufacturer
        {
            get
            {
                return _libraryManufacturer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _libraryManufacturer = value;
                    _libraryManufacturerEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11LibManuf;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11LibManufMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _libraryManufacturerEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _libraryManufacturer = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "library-description" encoded for PKCS#11 URI
        /// </summary>
        private string _libraryDescriptionEncoded = null;

        /// <summary>
        /// Value of path attribute "library-description" that corresponds to the "libraryDescription" member of CK_INFO structure
        /// </summary>
        private string _libraryDescription = null;

        /// <summary>
        /// Value of path attribute "library-description" that corresponds to the "libraryDescription" member of CK_INFO structure
        /// </summary>
        public string LibraryDescription
        {
            get
            {
                return _libraryDescription;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _libraryDescription = value;
                    _libraryDescriptionEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11LibDesc;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11LibDescMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _libraryDescriptionEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _libraryDescription = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "library-version" encoded for PKCS#11 URI
        /// </summary>
        private string _libraryVersionEncoded = null;

        /// <summary>
        /// Value of path attribute "library-version" that corresponds to the "libraryVersion" member of CK_INFO structure
        /// </summary>
        private string _libraryVersion = null;

        /// <summary>
        /// Value of path attribute "library-version" that corresponds to the "libraryVersion" member of CK_INFO structure
        /// </summary>
        public string LibraryVersion
        {
            get
            {
                return _libraryVersion;
            }
            set
            {
                if (value == null)
                {
                    _libraryVersion = value;
                    _libraryVersionEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11LibVer;

                    if (value == string.Empty)
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute cannot be empty");

                    int major = 0;
                    int minor = 0;

                    string[] parts = value.Split(new char[] { '.' }, StringSplitOptions.None);
                    if (parts.Length == 1)
                    {
                        major = Convert.ToInt32(parts[0]);
                    }
                    else if (parts.Length == 2)
                    {
                        if (string.IsNullOrEmpty(parts[0]))
                            throw new Pkcs11UriException("Attribute " + attributeName + " does not specify major version");

                        if (string.IsNullOrEmpty(parts[1]))
                            throw new Pkcs11UriException("Attribute " + attributeName + " does not specify minor version");

                        try
                        {
                            major = Convert.ToInt32(parts[0]);
                        }
                        catch (Exception ex)
                        {
                            throw new Pkcs11UriException("Attribute " + attributeName + " contains major version that cannot be converted to integer", ex);
                        }

                        try
                        {
                            minor = Convert.ToInt32(parts[1]);
                        }
                        catch (Exception ex)
                        {
                            throw new Pkcs11UriException("Attribute " + attributeName + " contains minor version that cannot be converted to integer", ex);
                        }
                    }
                    else
                    {
                        throw new Pkcs11UriException("Invalid value of " + attributeName + " attribute");
                    }

                    if ((_checkLengths == true) && ((major > 0xff) || (minor > 0xff)))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");

                    _libraryVersion = value;
                    _libraryVersionEncoded = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "object" encoded for PKCS#11 URI
        /// </summary>
        private string _objectEncoded = null;

        /// <summary>
        /// Value of path attribute "object" that corresponds to the "CKA_LABEL" object attribute
        /// </summary>
        private string _object = null;

        /// <summary>
        /// Value of path attribute "object" that corresponds to the "CKA_LABEL" object attribute
        /// </summary>
        public string Object
        {
            get
            {
                return _object;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _object = value;
                    _objectEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Object;
                    _objectEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _object = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "type" encoded for PKCS#11 URI
        /// </summary>
        private string _typeEncoded = null;

        /// <summary>
        /// Value of path attribute "type" that corresponds to the "CKA_CLASS" object attribute
        /// </summary>
        private CKO? _type = null;

        /// <summary>
        /// Value of path attribute "type" that corresponds to the "CKA_CLASS" object attribute
        /// </summary>
        public CKO? Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value == null)
                {
                    _type = value;
                    _typeEncoded = null;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11Type;

                    switch (value)
                    {
                        case CKO.CKO_PUBLIC_KEY:
                            _type = value;
                            _typeEncoded = Pkcs11UriSpec.Pk11TypePublic;
                            break;
                        case CKO.CKO_PRIVATE_KEY:
                            _type = value;
                            _typeEncoded = Pkcs11UriSpec.Pk11TypePrivate;
                            break;
                        case CKO.CKO_CERTIFICATE:
                            _type = value;
                            _typeEncoded = Pkcs11UriSpec.Pk11TypeCert;
                            break;
                        case CKO.CKO_SECRET_KEY:
                            _type = value;
                            _typeEncoded = Pkcs11UriSpec.Pk11TypeSecretKey;
                            break;
                        case CKO.CKO_DATA:
                            _type = value;
                            _typeEncoded = Pkcs11UriSpec.Pk11TypeData;
                            break;
                        default:
                            throw new Pkcs11UriException("Invalid value of " + attributeName + " attribute");
                    }
                }
            }
        }

        /// <summary>
        /// Value of path attribute "id" encoded for PKCS#11 URI
        /// </summary>
        private string _idEncoded = null;

        /// <summary>
        /// Value of path attribute "id" that corresponds to the "CKA_ID" object attribute
        /// </summary>
        private byte[] _id = null;

        /// <summary>
        /// Value of path attribute "id" that corresponds to the "CKA_ID" object attribute
        /// </summary>
        public byte[] Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                _idEncoded = PctEncodeByteArray(value);
            }
        }

        /// <summary>
        /// Value of path attribute "slot-manufacturer" encoded for PKCS#11 URI
        /// </summary>
        private string _slotManufacturerEncoded = null;

        /// <summary>
        /// Value of path attribute "slot-manufacturer" that corresponds to the "manufacturerID" member of CK_SLOT_INFO structure
        /// </summary>
        private string _slotManufacturer = null;

        /// <summary>
        /// Value of path attribute "slot-manufacturer" that corresponds to the "manufacturerID" member of CK_SLOT_INFO structure
        /// </summary>
        public string SlotManufacturer
        {
            get
            {
                return _slotManufacturer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _slotManufacturer = value;
                    _slotManufacturerEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11SlotManuf;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11SlotManufMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _slotManufacturerEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _slotManufacturer = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "slot-description" encoded for PKCS#11 URI
        /// </summary>
        private string _slotDescriptionEncoded = null;

        /// <summary>
        /// Value of path attribute "slot-description" that corresponds to the "slotDescription" member of CK_SLOT_INFO structure
        /// </summary>
        private string _slotDescription = null;

        /// <summary>
        /// Value of path attribute "slot-description" that corresponds to the "slotDescription" member of CK_SLOT_INFO structure
        /// </summary>
        public string SlotDescription
        {
            get
            {
                return _slotDescription;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _slotDescription = value;
                    _slotDescriptionEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11SlotDesc;
                    byte[] attributeValue = ConvertUtils.Utf8StringToBytes(value);
                    if ((_checkLengths == true) && (attributeValue.Length > Pkcs11UriSpec.Pk11SlotDescMaxLength))
                        throw new ArgumentOutOfRangeException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                    _slotDescriptionEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    _slotDescription = value;
                }
            }
        }

        /// <summary>
        /// Value of path attribute "slot-id" encoded for PKCS#11 URI
        /// </summary>
        private string _slotIdEncoded = null;

        /// <summary>
        /// Value of path attribute "slot-id" that corresponds to the decimal number of "CK_SLOT_ID" type
        /// </summary>
        private ulong? _slotId = null;

        /// <summary>
        /// Value of path attribute "slot-id" that corresponds to the decimal number of "CK_SLOT_ID" type
        /// </summary>
        public ulong? SlotId
        {
            get
            {
                return _slotId;
            }
            set
            {
                if (value == null)
                {
                    _slotId = value;
                    _slotIdEncoded = null;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11SlotId;

                    try
                    {
                        _slotId = Convert.ToUInt64(value);
                        _slotIdEncoded = Convert.ToString(_slotId);
                    }
                    catch (Exception ex)
                    {
                        throw new Pkcs11UriException("Invalid value of " + attributeName + " attribute", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Collection of unknown vendor specific path attributes that is validated when ToString() or ToPkcs11Uri() method is called
        /// </summary>
        private Dictionary<string, string> _unknownPathAttributes = new Dictionary<string,string>();

        /// <summary>
        /// Collection of unknown vendor specific path attributes that is validated when ToString() or ToPkcs11Uri() method is called
        /// </summary>
        public Dictionary<string, string> UnknownPathAttributes
        {
            get
            {
                return _unknownPathAttributes;
            }
            set
            {
                _unknownPathAttributes = value;
            }
        }

        /// <summary>
        /// Encodes collection of unknown vendor specific path attributes for PKCS#11 URI
        /// </summary>
        /// <returns>List of unknown vendor specific path attributes encoded for PKCS#11 URI</returns>
        private List<string> EncodeUnknownPathAttributes()
        {
            if (_unknownPathAttributes == null)
                return null;

            List<string> attributes = new List<string>();

            foreach (KeyValuePair<string, string> attribute in _unknownPathAttributes)
            {
                string attributeName = attribute.Key;
                string attributeValue = attribute.Value;

                // Validate attribute name
                if (string.IsNullOrEmpty(attributeName))
                    throw new Pkcs11UriException("Attribute name cannot be null or empty");

                attributeName = EncodePk11String(null, attributeName, Pkcs11UriSpec.Pk11VendorAttrNameChars, false);

                // Validate attribute value
                if (string.IsNullOrEmpty(attributeValue))
                    attributeValue = string.Empty;
                else
                    attributeValue = EncodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);

                attributes.Add(attributeName + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + attributeValue);
            }

            return attributes;
        }

        #endregion

        #region Query attributes

        /// <summary>
        /// Value of query attribute "pin-source" encoded for PKCS#11 URI
        /// </summary>
        private string _pinSourceEncoded = null;

        /// <summary>
        /// Value of query attribute "pin-source" that specifies where token PIN can be obtained
        /// </summary>
        private string _pinSource = null;

        /// <summary>
        /// Value of query attribute "pin-source" that specifies where token PIN can be obtained
        /// </summary>
        public string PinSource
        {
            get
            {
                return _pinSource;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _pinSource = value;
                    _pinSourceEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11PinSource;
                    _pinSourceEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                    _pinSource = value;
                }
            }
        }

        /// <summary>
        /// Value of query attribute "pin-value" encoded for PKCS#11 URI
        /// </summary>
        private string _pinValueEncoded = null;

        /// <summary>
        /// Value of query attribute "pin-value" that contains token PIN
        /// </summary>
        private string _pinValue = null;

        /// <summary>
        /// Value of query attribute "pin-value" that contains token PIN
        /// </summary>
        public string PinValue
        {
            get
            {
                return _pinValue;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _pinValue = value;
                    _pinValueEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11PinValue;
                    _pinValueEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                    _pinValue = value;
                }
            }
        }

        /// <summary>
        /// Value of query attribute "module-name" encoded for PKCS#11 URI
        /// </summary>
        private string _moduleNameEncoded = null;

        /// <summary>
        /// Value of query attribute "module-name" that specifies name of the PKCS#11 library
        /// </summary>
        private string _moduleName = null;

        /// <summary>
        /// Value of query attribute "module-name" that specifies name of the PKCS#11 library
        /// </summary>
        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _moduleName = value;
                    _moduleNameEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11ModuleName;
                    _moduleNameEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                    _moduleName = value;
                }
            }
        }

        /// <summary>
        /// Value of query attribute "module-path" encoded for PKCS#11 URI
        /// </summary>
        private string _modulePathEncoded = null;

        /// <summary>
        /// Value of query attribute "module-path" that specifies path to the PKCS#11 library
        /// </summary>
        private string _modulePath = null;

        /// <summary>
        /// Value of query attribute "module-path" that specifies path to the PKCS#11 library
        /// </summary>
        public string ModulePath
        {
            get
            {
                return _modulePath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _modulePath = value;
                    _modulePathEncoded = value;
                }
                else
                {
                    string attributeName = Pkcs11UriSpec.Pk11ModulePath;
                    _modulePathEncoded = EncodePk11String(attributeName, value, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                    _modulePath = value;
                }
            }
        }

        /// <summary>
        /// Collection of unknown vendor specific query attributes that is validated when ToString() or ToPkcs11Uri() method is called
        /// </summary>
        private Dictionary<string, List<string>> _unknownQueryAttributes = new Dictionary<string,List<string>>();

        /// <summary>
        /// Collection of unknown vendor specific query attributes that is validated when ToString() or ToPkcs11Uri() method is called
        /// </summary>
        public Dictionary<string, List<string>> UnknownQueryAttributes
        {
            get
            {
                return _unknownQueryAttributes;
            }
            set
            {
                _unknownQueryAttributes = value;
            }
        }

        /// <summary>
        /// Encodes collection of unknown vendor specific query attributes for PKCS#11 URI
        /// </summary>
        /// <returns>List of unknown vendor specific query attributes encoded for PKCS#11 URI</returns>
        private List<string> EncodeUnknownQueryAttributes()
        {
            if (_unknownQueryAttributes == null)
                return null;

            List<string> attributes = new List<string>();

            foreach (KeyValuePair<string, List<string>> attribute in _unknownQueryAttributes)
            {
                string attributeName = attribute.Key;
                List<string> attributeValues = attribute.Value;

                // Validate attribute name
                if (string.IsNullOrEmpty(attributeName))
                    throw new Pkcs11UriException("Attribute name cannot be null or empty");

                attributeName = EncodePk11String(null, attributeName, Pkcs11UriSpec.Pk11VendorAttrNameChars, false);

                // Validate attribute values
                if ((attributeValues == null) || (attributeValues.Count == 0))
                {
                    string value = string.Empty;
                    attributes.Add(attributeName + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + value);
                }
                else
                {
                    foreach (string attributeValue in attributeValues)
                    {
                        string value = string.Empty;

                        if (!string.IsNullOrEmpty(attributeValue))
                            value = EncodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);

                        attributes.Add(attributeName + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + value);
                    }
                }
            }

            return attributes;
        }

        #endregion

        #endregion

        /// <summary>
        /// Generates PKCS#11 URI representing contents of Pkcs11UriBuilder instance
        /// </summary>
        /// <returns>PKCS#11 URI representing contents of Pkcs11UriBuilder instance</returns>
        public override string ToString()
        {
            List<string> pathAttributes = new List<string>();
            // Library definition
            if (_libraryManufacturerEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11LibManuf + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _libraryManufacturerEncoded);
            if (_libraryDescriptionEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11LibDesc + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _libraryDescriptionEncoded);
            if (_libraryVersionEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11LibVer + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _libraryVersionEncoded);
            // Slot definition
            if (_slotManufacturerEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11SlotManuf + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _slotManufacturerEncoded);
            if (_slotDescriptionEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11SlotDesc + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _slotDescriptionEncoded);
            if (_slotIdEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11SlotId + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _slotIdEncoded);
            // Token definition
            if (_manufacturerEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Manuf + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _manufacturerEncoded);
            if (_modelEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Model + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _modelEncoded);
            if (_serialEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Serial + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _serialEncoded);
            if (_tokenEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Token + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _tokenEncoded);
            // Object definition
            if (_typeEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Type + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _typeEncoded);
            if (_objectEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Object + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _objectEncoded);
            if (_idEncoded != null)
                pathAttributes.Add(Pkcs11UriSpec.Pk11Id + Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator + _idEncoded);
            // Vendor specific attributes
            if (_unknownPathAttributes != null && _unknownPathAttributes.Count > 0)
                pathAttributes.AddRange(EncodeUnknownPathAttributes());

            List<string> queryAttributes = new List<string>();
            // Library definition
            if (_modulePath != null)
                queryAttributes.Add(Pkcs11UriSpec.Pk11ModulePath + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + _modulePathEncoded);
            if (_moduleName != null)
                queryAttributes.Add(Pkcs11UriSpec.Pk11ModuleName + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + _moduleNameEncoded);
            // PIN handling definition
            if (_pinValueEncoded != null)
                queryAttributes.Add(Pkcs11UriSpec.Pk11PinValue + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + _pinValueEncoded);
            if (_pinSourceEncoded != null)
                queryAttributes.Add(Pkcs11UriSpec.Pk11PinSource + Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator + _pinSourceEncoded);
            // Vendor specific attributes
            if (_unknownQueryAttributes != null && _unknownQueryAttributes.Count > 0)
                queryAttributes.AddRange(EncodeUnknownQueryAttributes());

            string path = string.Join(Pkcs11UriSpec.Pk11PathAttributesSeparator, pathAttributes.ToArray());
            string query = string.Join(Pkcs11UriSpec.Pk11QueryAttributesSeparator, queryAttributes.ToArray());
            
            string uri = Pkcs11UriSpec.Pk11UriSchemeName + Pkcs11UriSpec.Pk11UriAndPathSeparator;
            uri += (string.IsNullOrEmpty(path)) ? string.Empty : path;
            uri += (string.IsNullOrEmpty(query)) ? string.Empty : Pkcs11UriSpec.Pk11PathAndQuerySeparator + query;

            return uri;
        }

        /// <summary>
        /// Converts Pkcs11UriBuilder instance to Pkcs11Uri instance
        /// </summary>
        /// <returns>Pkcs11Uri instance representing contents of Pkcs11UriBuilder instance</returns>
        public Pkcs11Uri ToPkcs11Uri()
        {
            return new Pkcs11Uri(ToString(), _checkLengths);
        }

        #region Private methods

        /// <summary>
        /// Percent encodes provided byte array
        /// </summary>
        /// <param name="byteArray">Byte array that should be encoded</param>
        /// <returns>Percent encoded byte array</returns>
        private string PctEncodeByteArray(byte[] byteArray)
        {
            if (byteArray == null)
                return null;

            StringBuilder stringBuilder = new StringBuilder(byteArray.Length * 3);

            for (int i = 0; i < byteArray.Length; i++)
            {
                stringBuilder.Append('%');
                stringBuilder.Append(BitConverter.ToString(new byte[] { byteArray[i] }));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Percent encodes provided character
        /// </summary>
        /// <param name="character">Character that should be encoded</param>
        /// <returns>Percent encoded character</returns>
        private string PctEncodeCharacter(char character)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(new char[] { character });
            return PctEncodeByteArray(bytes);
        }

        /// <summary>
        /// Checks whether Pk11String contains invalid characters and optionaly percent encodes invalid characters
        /// </summary>
        /// <param name="attributeName">Name of attribute whose value is being encoded</param>
        /// <param name="pk11String">Pk11String that should be encoded</param>
        /// <param name="allowedChars">Characters allowed to be present unencoded in Pk11String</param>
        /// <param name="usePctEncoding">Flag indicating whether invalid characters should be percent encoded</param>
        /// <returns>Encoded Pk11String</returns>
        private string EncodePk11String(string attributeName, string pk11String, char[] allowedChars, bool usePctEncoding)
        {
            if (string.IsNullOrEmpty(pk11String))
                return pk11String;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < pk11String.Length; i++)
            {
                bool allowedChar = false;

                for (int j = 0; j < allowedChars.Length; j++)
                {
                    if (pk11String[i] == allowedChars[j])
                    {
                        allowedChar = true;
                        break;
                    }
                }

                if (allowedChar)
                {
                    stringBuilder.Append(pk11String[i]);
                }
                else
                {
                    if (usePctEncoding == true)
                    {
                        stringBuilder.Append(PctEncodeCharacter(pk11String[i]));
                    }
                    else
                    {
                        if (attributeName != null)
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute contains invalid character");
                        else
                            throw new Pkcs11UriException("Attribute name contains invalid character");
                    }
                }
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}
