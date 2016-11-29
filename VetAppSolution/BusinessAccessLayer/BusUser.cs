using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.BusinessAccessLayer
{

    /// <summary>
    /// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  BusUser.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	11/28/2016	Created
    /// 
    /// ----------------------------------------------------
    /// Business Class for User objects.
    /// </summary>
    public class BusUser
    {
        private SqlConnection _conn = null;
        private bool _hasError = false;
        private bool _hasInvalid = false;

        private ArrayList _arrlstEntities = null;
        private ArrayList _arrlstColumnErrors = new ArrayList();

        private const String REGEXP_ISVALID_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_USER_ROLE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_FIRSTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_MIDDLENAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_LASTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PHONE_NUMBER = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_USERNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PASSWD = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PICTURE_URL = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PICTURE = "";
        private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;

        public string SP_ENUM_NAME = null;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>BusUser constructor takes SqlConnection object</summary>
        public BusUser()
        {
        }
        /// <summary>BusUser constructor takes SqlConnection object</summary>
        public BusUser(SqlConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         No parameters. Returns all User objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all User objects</retvalue>
        /// </summary>
        public ArrayList Get()
        {
            return (Get(0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, null, null, false));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         No parameters. Returns all User objects 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing all User objects</retvalue>
        /// </summary>
        public ArrayList Get(long lUserID)
        {
            return (Get(lUserID, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, null, null, false));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">User to be returned</param>
        ///     <retvalue>ArrayList containing User object</retvalue>
        /// </summary>
        public ArrayList Get(User o)
        {
            return (Get(o.UserID, o.UserRoleID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.Username, o.Passwd, o.PictureUrl, o.Picture, o.IsDisabled));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">User to be returned</param>
        ///     <retvalue>ArrayList containing User object</retvalue>
        /// </summary>
        public ArrayList Get(EnumUser o)
        {
            return (Get(o.UserID, o.UserRoleID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.Username, o.Passwd, o.PictureUrl, o.Picture, o.IsDisabled));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         Returns User objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing User object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngUserID, long pLngUserRoleID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrFirstname, string pStrMiddlename, string pStrLastname, string pStrPhoneNumber, string pStrUsername, string pStrPasswd, string pStrPictureUrl, byte[] pBytPicture, bool? pBolIsDisabled)
        {
            User data = null;
            _arrlstEntities = new ArrayList();
            EnumUser enumUser = new EnumUser(_conn);
            enumUser.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumUser.SP_ENUM_NAME;
            enumUser.UserID = pLngUserID;
            enumUser.UserRoleID = pLngUserRoleID;
            enumUser.BeginDateCreated = pDtBeginDateCreated;
            enumUser.EndDateCreated = pDtEndDateCreated;
            enumUser.BeginDateModified = pDtBeginDateModified;
            enumUser.EndDateModified = pDtEndDateModified;
            enumUser.Firstname = pStrFirstname;
            enumUser.Middlename = pStrMiddlename;
            enumUser.Lastname = pStrLastname;
            enumUser.PhoneNumber = pStrPhoneNumber;
            enumUser.Username = pStrUsername;
            enumUser.Passwd = pStrPasswd;
            enumUser.PictureUrl = pStrPictureUrl;
            enumUser.Picture = pBytPicture;
            enumUser.IsDisabled = pBolIsDisabled;
            enumUser.EnumData();
            while (enumUser.hasMoreElements())
            {
                data = (User)enumUser.nextElement();
                _arrlstEntities.Add(data);
            }
            enumUser = null;
            ArrayList.ReadOnly(_arrlstEntities);
            return _arrlstEntities;
        }

        /// <summary>
        ///     Saves User object to database
        ///     <param name="o">User to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(User o)
        {
            if (o != null)
            {
                o.Save(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify User object to database
        ///     <param name="o">User to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Update(User o)
        {
            if (o != null)
            {
                o.Update(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify User object to database
        ///     <param name="o">User to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Load(User o)
        {
            if (o != null)
            {
                o.Load(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Modify User object to database
        ///     <param name="o">User to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public void Delete(User o)
        {
            if (o != null)
            {
                o.Delete(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }
        }

        /// <summary>
        ///     Exist User object to database
        ///     <param name="o">User to be modified.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
        public bool Exist(User o)
        {
            bool bExist = false;
            if (o != null)
            {
                bExist = o.Exist(_conn);
                if (o.HasError)
                {
                    _hasError = true;
                }
            }

            return bExist;
        }
        /// <summary>Property as to whether or not the object has an Error</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Property as to whether or not the object has invalid columns</summary>
        public bool HasInvalid
        {
            get { return _hasInvalid; }
        }
        /// <summary>Property which returns all column error in an ArrayList</summary>
        public ArrayList ColumnErrors
        {
            get { return _arrlstColumnErrors; }
        }
        /// <summary>Property returns an ArrayList containing User objects</summary>
        public ArrayList Users
        {
            get
            {
                if (_arrlstEntities == null)
                {
                    User data = null;
                    _arrlstEntities = new ArrayList();
                    EnumUser enumUser = new EnumUser(_conn);
                    enumUser.EnumData();
                    while (enumUser.hasMoreElements())
                    {
                        data = (User)enumUser.nextElement();
                        _arrlstEntities.Add(data);
                    }
                    enumUser = null;
                    ArrayList.ReadOnly(_arrlstEntities);
                }
                return _arrlstEntities;
            }
        }

        /// <summary>
        ///     Checks to make sure all values are valid
        ///     <remarks>   
        ///         Calls "IsValid" method for each property in ocject
        ///     </remarks>   
        ///     <retvalue>true if object has valid entries, false otherwise</retvalue>
        /// </summary>
        public bool IsValid(User pRefUser)
        {
            bool isValid = true;
            bool isValidTmp = true;

            _arrlstColumnErrors = null;
            _arrlstColumnErrors = new ArrayList();

            isValidTmp = IsValidUserID(pRefUser.UserID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserRoleID(pRefUser.UserRoleID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateCreated(pRefUser.DateCreated);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateModified(pRefUser.DateModified);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidFirstname(pRefUser.Firstname);
            if (!isValidTmp && pRefUser.Firstname != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidMiddlename(pRefUser.Middlename);
            if (!isValidTmp && pRefUser.Middlename != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidLastname(pRefUser.Lastname);
            if (!isValidTmp && pRefUser.Lastname != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidPhoneNumber(pRefUser.PhoneNumber);
            if (!isValidTmp && pRefUser.PhoneNumber != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidUsername(pRefUser.Username);
            if (!isValidTmp && pRefUser.Username != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidPasswd(pRefUser.Passwd);
            if (!isValidTmp && pRefUser.Passwd != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidPictureUrl(pRefUser.PictureUrl);
            if (!isValidTmp && pRefUser.PictureUrl != null)
            {
                isValid = false;
            }
            //Cannot validate type byte[].
            isValidTmp = IsValidIsDisabled(pRefUser.IsDisabled);
            if (!isValidTmp && pRefUser.IsDisabled != null)
            {
                isValid = false;
            }

            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidUserID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_ID;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidUserRoleID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_USER_ROLE_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_USER_ROLE_ID;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidDateCreated(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_DATE_CREATED)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_DATE_CREATED;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidDateModified(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_DATE_MODIFIED)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_DATE_MODIFIED;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidFirstname(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_FIRSTNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_FIRSTNAME;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidMiddlename(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_MIDDLENAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_MIDDLENAME;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidLastname(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_LASTNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_LASTNAME;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidPhoneNumber(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PHONE_NUMBER)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PHONE_NUMBER;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidUsername(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_USERNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_USERNAME;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidPasswd(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PASSWD)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PASSWD;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidPictureUrl(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PICTURE_URL)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PICTURE_URL;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidPicture(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PICTURE)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PICTURE;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidIsDisabled(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_IS_DISABLED)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_IS_DISABLED;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE