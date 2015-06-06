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
using System.IO;
using System.Text;

namespace Net.Pkcs11Interop.Common
{
    /// <summary>
    /// PKCS#11 URI parser
    /// </summary>
    public class Pkcs11Uri
    {
        #region Constructors

        /// <summary>
        /// Intializes new instance of Pkcs11Uri class that parses provided PKCS#11 URI and checks max lengths of path attribute values
        /// </summary>
        /// <param name="uri">PKCS#11 URI to be parsed</param>
        public Pkcs11Uri(string uri)
            : this(uri, true)
        {

        }

        /// <summary>
        /// Intializes new instance of Pkcs11Uri class that parses provided PKCS#11 URI
        /// </summary>
        /// <param name="uri">PKCS#11 URI to be parsed</param>
        /// <param name="checkLengths">Flag indicating whether max lengths of path attribute values should be checked</param>
        public Pkcs11Uri(string uri, bool checkLengths)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri");

            _checkLengths = checkLengths;

            Parse(Extract(uri));
        }

        #endregion

        #region Properties and variables

        /// <summary>
        /// Flag indicating whether max lengths of path attribute values were checked
        /// </summary>
        private bool _checkLengths = true;

        /// <summary>
        /// Flag indicating whether max lengths of path attribute values were checked
        /// </summary>
        public bool ChecksLengths
        {
            get
            {
                return _checkLengths;
            }
        }

        #region Flags
        
        /// <summary>
        /// Flag indicating whether PKCS#11 URI path attributes define specific PKCS#11 library
        /// </summary>
        public bool DefinesLibrary
        {
            get
            {
                return (LibraryManufacturer != null ||
                        LibraryDescription != null ||
                        LibraryVersion != null);
            }
        }

        /// <summary>
        /// Flag indicating whether PKCS#11 URI path attributes define specific slot
        /// </summary>
        public bool DefinesSlot
        {
            get
            {
                return (SlotManufacturer != null ||
                        SlotDescription != null ||
                        SlotId != null);
            }
        }

        /// <summary>
        /// Flag indicating whether PKCS#11 URI path attributes define specific token
        /// </summary>
        public bool DefinesToken
        {
            get
            {
                return (Token != null ||
                        Manufacturer != null ||
                        Serial != null ||
                        Model != null);
            }
        }

        /// <summary>
        /// Flag indicating whether PKCS#11 URI path attributes define specific object
        /// </summary>
        public bool DefinesObject
        {
            get
            {
                return (Object != null ||
                        Type != null ||
                        Id != null);
            }
        }

        #endregion

        #region Path attributes

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

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
        }

        /// <summary>
        /// Collection of unknown vendor specific path attributes
        /// </summary>
        private Dictionary<string, string> _unknownPathAttributes = new Dictionary<string,string>();

        /// <summary>
        /// Collection of unknown vendor specific path attributes
        /// </summary>
        public Dictionary<string, string> UnknownPathAttributes
        {
            get
            {
                return _unknownPathAttributes;
            }
        }

        #endregion

        #region Query attributes

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
        }

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
        }

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
        }

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
        }

        /// <summary>
        /// Collection of unknown vendor specific query attributes
        /// </summary>
        private Dictionary<string, List<string>> _unknownQueryAttributes = new Dictionary<string,List<string>>();

        /// <summary>
        /// Collection of unknown vendor specific query attributes
        /// </summary>
        public Dictionary<string, List<string>> UnknownQueryAttributes
        {
            get
            {
                return _unknownQueryAttributes;
            }
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Extracts PKCS#11 URI from text and removes all whitespaces
        /// </summary>
        /// <param name="text">Text that contains PKCS#11 URI</param>
        /// <returns>PKCS#11 URI without whitespaces</returns>
        private string Extract(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            StringBuilder stringBuilder = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
                if (!char.IsWhiteSpace(text[i]))
                    stringBuilder.Append(text[i]);

            string uri = stringBuilder.ToString();

            int firstCharPosition = 0;
            int lastCharPosition = 0;

            firstCharPosition = uri.IndexOf(Pkcs11UriSpec.Pk11UriSchemeName + Pkcs11UriSpec.Pk11UriAndPathSeparator, StringComparison.Ordinal);
            if (firstCharPosition > 0)
            {
                if (uri[firstCharPosition - 1] == '"')
                {
                    uri = uri.Remove(0, firstCharPosition);
                    lastCharPosition = uri.IndexOf('"');
                }
                else if (uri[firstCharPosition - 1] == '<')
                {
                    uri = uri.Remove(0, firstCharPosition);
                    lastCharPosition = uri.IndexOf('>');
                }
                else
                    throw new Pkcs11UriException("URI is not delimited within double quotes or angle brackets");

                if (lastCharPosition < 0)
                    throw new Pkcs11UriException("URI is not correctly delimited within double quotes or angle brackets");

                uri = uri.Substring(0, lastCharPosition);
            }

            return uri;
        }

        /// <summary>
        /// Parses PKCS#11 URI
        /// </summary>
        /// <param name="uri">PKCS#11 URI that should be parsed</param>
        private void Parse(string uri)
        {
            if (string.IsNullOrEmpty(uri))
                throw new ArgumentNullException("uri");

            // Check URI scheme
            if (!uri.StartsWith(Pkcs11UriSpec.Pk11UriSchemeName + Pkcs11UriSpec.Pk11UriAndPathSeparator, StringComparison.Ordinal))
                throw new Pkcs11UriException("Unknown URI scheme name");

            // Remove URI prefix
            uri = uri.Remove(0, Pkcs11UriSpec.Pk11UriSchemeName.Length + Pkcs11UriSpec.Pk11UriAndPathSeparator.Length);

            // Empty PKCS#11 URI is also valid
            if (uri == string.Empty)
                return;

            // Extract path and query parts
            string path = null;
            string query = null;
            int separatorIndex = uri.IndexOf(Pkcs11UriSpec.Pk11PathAndQuerySeparator);

            if (separatorIndex == -1)
            {
                path = uri;
                query = string.Empty;
            }
            else
            {
                path = uri.Substring(0, separatorIndex);
                query = uri.Substring(separatorIndex + 1);

                if (string.IsNullOrEmpty(query))
                    throw new Pkcs11UriException("Question mark is present in the URI but query component is missing");
            }

            // Parse path attributes
            if (!string.IsNullOrEmpty(path))
            {
                string[] pathAttributes = path.Split(new string[] { Pkcs11UriSpec.Pk11PathAttributesSeparator }, StringSplitOptions.None);
                foreach (string pathAttribute in pathAttributes)
                    ParsePathAttribute(pathAttribute);
            }

            // Parse query attributes
            if (!string.IsNullOrEmpty(query))
            {
                string[] queryAttributes = query.Split(new string[] { Pkcs11UriSpec.Pk11QueryAttributesSeparator }, StringSplitOptions.None);
                foreach (string queryAttribute in queryAttributes)
                    ParseQueryAttribute(queryAttribute);
            }
        }

        /// <summary>
        /// Parses path attribute
        /// </summary>
        /// <param name="attribute">Path attribute that should be parsed</param>
        private void ParsePathAttribute(string attribute)
        {
            int separatorIndex = attribute.IndexOf(Pkcs11UriSpec.Pk11PathAttributeNameAndValueSeparator);
            if (separatorIndex == -1)
                throw new Pkcs11UriException("Attribute name and value are not separated");

            string attributeName = attribute.Substring(0, separatorIndex);
            string attributeValue = attribute.Substring(separatorIndex + 1);

            switch (attributeName)
            {
                case Pkcs11UriSpec.Pk11Token:

                    if (_token != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11TokenMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _token = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _token = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11Manuf:

                    if (_manufacturer != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11ManufMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _manufacturer = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _manufacturer = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11Serial:

                    if (_serial != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11SerialMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _serial = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _serial = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11Model:

                    if (_model != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11ModelMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _model = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _model = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11LibManuf:

                    if (_libraryManufacturer != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11LibManufMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _libraryManufacturer = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _libraryManufacturer = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11LibDesc:

                    if (_libraryDescription != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11LibDescMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _libraryDescription = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _libraryDescription = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11LibVer:

                    if (_libraryVersion != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue == string.Empty)
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute cannot be empty");

                    int major = 0;
                    int minor = 0;

                    string[] parts = attributeValue.Split(new char[] { '.' }, StringSplitOptions.None);
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
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");

                    _libraryVersion = string.Format("{0}.{1}", major, minor);

                    break;

                case Pkcs11UriSpec.Pk11Object:

                    if (_object != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        _object = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _object = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11Type:

                    if (_type != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue == string.Empty)
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute cannot be empty");

                    switch (attributeValue)
                    {
                        case Pkcs11UriSpec.Pk11TypePublic:
                            _type = CKO.CKO_PUBLIC_KEY;
                            break;
                        case Pkcs11UriSpec.Pk11TypePrivate:
                            _type = CKO.CKO_PRIVATE_KEY;
                            break;
                        case Pkcs11UriSpec.Pk11TypeCert:
                            _type = CKO.CKO_CERTIFICATE;
                            break;
                        case Pkcs11UriSpec.Pk11TypeSecretKey:
                            _type = CKO.CKO_SECRET_KEY;
                            break;
                        case Pkcs11UriSpec.Pk11TypeData:
                            _type = CKO.CKO_DATA;
                            break;
                        default:
                            throw new Pkcs11UriException("Invalid value of " + attributeName + " attribute");
                    }

                    break;

                case Pkcs11UriSpec.Pk11Id:

                    if (_id != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        _id = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                    }
                    else
                    {
                        _id = new byte[0];
                    }

                    break;

                case Pkcs11UriSpec.Pk11SlotManuf:

                    if (_slotManufacturer != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11SlotManufMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _slotManufacturer = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _slotManufacturer = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11SlotDesc:

                    if (_slotDescription != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        if ((_checkLengths == true) && (bytes.Length > Pkcs11UriSpec.Pk11SlotDescMaxLength))
                            throw new Pkcs11UriException("Value of " + attributeName + " attribute exceeds the maximum allowed length");
                        _slotDescription = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _slotDescription = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11SlotId:

                    if (_slotId != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");

                    if (attributeValue == string.Empty)
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute cannot be empty");

                    try
                    {
                        _slotId = Convert.ToUInt64(attributeValue);
                    }
                    catch (Exception ex)
                    {
                        throw new Pkcs11UriException("Invalid value of " + attributeName + " attribute", ex);
                    }

                    break;

                default:

                    if (string.IsNullOrEmpty(attributeName))
                        throw new Pkcs11UriException("Attribute without name found in the path component");

                    byte[] vendorAttrName = DecodePk11String(null, attributeName, Pkcs11UriSpec.Pk11VendorAttrNameChars, false);
                    attributeName = ConvertUtils.BytesToUtf8String(vendorAttrName);

                    if (attributeValue != string.Empty)
                    {
                        byte[] vendorAttrValue = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11PathAttrValueChars, true);
                        attributeValue = ConvertUtils.BytesToUtf8String(vendorAttrValue);
                    }

                    if (_unknownPathAttributes.ContainsKey(attributeName))
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the path component");
                    else
                        _unknownPathAttributes.Add(attributeName, attributeValue);

                    break;
            }
        }

        /// <summary>
        /// Parses query attribute
        /// </summary>
        /// <param name="attribute">Query attribute that should be parsed</param>
        private void ParseQueryAttribute(string attribute)
        {
            int separatorIndex = attribute.IndexOf(Pkcs11UriSpec.Pk11QueryAttributeNameAndValueSeparator);
            if (separatorIndex == -1)
                throw new Pkcs11UriException("Attribute name and value are not separated");

            string attributeName = attribute.Substring(0, separatorIndex);
            string attributeValue = attribute.Substring(separatorIndex + 1);

            switch (attributeName)
            {
                case Pkcs11UriSpec.Pk11PinSource:

                    if (_pinSource != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the query component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                        _pinSource = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _pinSource = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11PinValue:

                    if (_pinValue != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the query component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                        _pinValue = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _pinValue = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11ModuleName:

                    if (_moduleName != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the query component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                        _moduleName = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _moduleName = string.Empty;
                    }

                    break;

                case Pkcs11UriSpec.Pk11ModulePath:

                    if (_modulePath != null)
                        throw new Pkcs11UriException("Duplicate attribute " + attributeName + " found in the query component");

                    if (attributeValue != string.Empty)
                    {
                        byte[] bytes = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                        _modulePath = ConvertUtils.BytesToUtf8String(bytes);
                    }
                    else
                    {
                        _modulePath = string.Empty;
                    }

                    break;

                default:

                    if (string.IsNullOrEmpty(attributeName))
                        throw new Pkcs11UriException("Attribute without name found in the query component");

                    byte[] vendorAttrName = DecodePk11String(null, attributeName, Pkcs11UriSpec.Pk11VendorAttrNameChars, false);
                    attributeName = ConvertUtils.BytesToUtf8String(vendorAttrName);

                    if (attributeValue != string.Empty)
                    {
                        byte[] vendorAttrValue = DecodePk11String(attributeName, attributeValue, Pkcs11UriSpec.Pk11QueryAttrValueChars, true);
                        attributeValue = ConvertUtils.BytesToUtf8String(vendorAttrValue);
                    }

                    if (_unknownQueryAttributes.ContainsKey(attributeName))
                        _unknownQueryAttributes[attributeName].Add(attributeValue);
                    else
                        _unknownQueryAttributes.Add(attributeName, new List<string>() { attributeValue });

                    break;
            }
        }

        /// <summary>
        /// Checks whether Pk11String contains invalid characters and optionaly decodes percent encoded characters
        /// </summary>
        /// <param name="attributeName">Name of attribute whose value is being decoded</param>
        /// <param name="pk11String">Pk11String that should be decoded</param>
        /// <param name="allowedChars">Characters allowed to be present unencoded in Pk11String</param>
        /// <param name="decodePctEncodedChars">Flag indicating whether percent encoded characters should be decoded</param>
        /// <returns>Decoded Pk11String</returns>
        private byte[] DecodePk11String(string attributeName, string pk11String, char[] allowedChars, bool decodePctEncodedChars)
        {
            if (string.IsNullOrEmpty(pk11String))
                return null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                int i = 0;

                while (i < pk11String.Length)
                {
                    if (decodePctEncodedChars)
                    {
                        if (pk11String[i] == '%')
                        {
                            if ((i + 2) > pk11String.Length)
                            {
                                if (attributeName != null)
                                    throw new Pkcs11UriException("Value of " + attributeName + " attribute contains invalid application of percent-encoding");
                                else
                                    throw new Pkcs11UriException("URI contains invalid application of percent-encoding");
                            }

                            if (!IsHexDigit(pk11String[i + 1]) || !IsHexDigit(pk11String[i + 2]))
                            {
                                if (attributeName != null)
                                    throw new Pkcs11UriException("Value of " + attributeName + " attribute contains invalid application of percent-encoding");
                                else
                                    throw new Pkcs11UriException("URI contains invalid application of percent-encoding");
                            }

                            memoryStream.WriteByte(Convert.ToByte(pk11String.Substring(i + 1, 2), 16));
                            i = i + 3;
                            continue;
                        }
                    }

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
                        memoryStream.WriteByte(Convert.ToByte(pk11String[i]));
                        i++;
                        continue;
                    }

                    if (attributeName != null)
                        throw new Pkcs11UriException("Value of " + attributeName + " attribute contains invalid character");
                    else
                        throw new Pkcs11UriException("URI contains invalid character");
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Checks whether character is hex digit
        /// </summary>
        /// <param name="c">Character that should be checked</param>
        /// <returns>True if character is hex digit false otherwise</returns>
        private bool IsHexDigit(char c)
        {
            return (((c >= 0x30) && (c <= 0x39)) || // 0-9
                    ((c >= 0x41) && (c <= 0x46)) || // A-F
                    ((c >= 0x61) && (c <= 0x66)));  // a-f
        }

        #endregion
    }
}
