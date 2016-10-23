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
    /// 001	HA	10/23/2016	Created
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
        private const String REGEXP_ISVALID_AUTHUSERID = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_AUTHCONNECTION = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_AUTHPROVIDER = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_AUTHACCESSTOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_AUTHIDTOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_DATE_CREATED = "";
        private const String REGEXP_ISVALID_DATE_MODIFIED = "";
        private const String REGEXP_ISVALID_FIRSTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_MIDDLENAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_LASTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PHONE_NUMBER = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_EMAIL_ADDRESS = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PROFILEIMAGEURL = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_CAN_TEXT_MSG = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_DATE_TEXT_MSG_APPROVED = "";

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
            return (Get(0, null, null, null, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, false, false, new DateTime(), new DateTime()));
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
            return (Get(lUserID, null, null, null, null, null, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, false, false, new DateTime(), new DateTime()));
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
            return (Get(o.UserID, o.AuthUserid, o.AuthConnection, o.AuthProvider, o.AuthAccessToken, o.AuthIdToken, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.EmailAddress, o.Profileimageurl, o.IsDisabled, o.CanTextMsg, o.DateTextMsgApproved, o.DateTextMsgApproved));
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
            return (Get(o.UserID, o.AuthUserid, o.AuthConnection, o.AuthProvider, o.AuthAccessToken, o.AuthIdToken, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.EmailAddress, o.Profileimageurl, o.IsDisabled, o.CanTextMsg, o.BeginDateTextMsgApproved, o.EndDateTextMsgApproved));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         Returns User objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing User object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngUserID, string pStrAuthUserid, string pStrAuthConnection, string pStrAuthProvider, string pStrAuthAccessToken, string pStrAuthIdToken, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrFirstname, string pStrMiddlename, string pStrLastname, string pStrPhoneNumber, string pStrEmailAddress, string pStrProfileimageurl, bool? pBolIsDisabled, bool? pBolCanTextMsg, DateTime pDtBeginDateTextMsgApproved, DateTime pDtEndDateTextMsgApproved)
        {
            User data = null;
            _arrlstEntities = new ArrayList();
            EnumUser enumUser = new EnumUser(_conn);
            enumUser.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumUser.SP_ENUM_NAME;
            enumUser.UserID = pLngUserID;
            enumUser.AuthUserid = pStrAuthUserid;
            enumUser.AuthConnection = pStrAuthConnection;
            enumUser.AuthProvider = pStrAuthProvider;
            enumUser.AuthAccessToken = pStrAuthAccessToken;
            enumUser.AuthIdToken = pStrAuthIdToken;
            enumUser.BeginDateCreated = pDtBeginDateCreated;
            enumUser.EndDateCreated = pDtEndDateCreated;
            enumUser.BeginDateModified = pDtBeginDateModified;
            enumUser.EndDateModified = pDtEndDateModified;
            enumUser.Firstname = pStrFirstname;
            enumUser.Middlename = pStrMiddlename;
            enumUser.Lastname = pStrLastname;
            enumUser.PhoneNumber = pStrPhoneNumber;
            enumUser.EmailAddress = pStrEmailAddress;
            enumUser.Profileimageurl = pStrProfileimageurl;
            enumUser.IsDisabled = pBolIsDisabled;
            enumUser.CanTextMsg = pBolCanTextMsg;
            enumUser.BeginDateTextMsgApproved = pDtBeginDateTextMsgApproved;
            enumUser.EndDateTextMsgApproved = pDtEndDateTextMsgApproved;
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
            isValidTmp = IsValidAuthUserid(pRefUser.AuthUserid);
            if (!isValidTmp && pRefUser.AuthUserid != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidAuthConnection(pRefUser.AuthConnection);
            if (!isValidTmp && pRefUser.AuthConnection != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidAuthProvider(pRefUser.AuthProvider);
            if (!isValidTmp && pRefUser.AuthProvider != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidAuthAccessToken(pRefUser.AuthAccessToken);
            if (!isValidTmp && pRefUser.AuthAccessToken != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidAuthIdToken(pRefUser.AuthIdToken);
            if (!isValidTmp && pRefUser.AuthIdToken != null)
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
            isValidTmp = IsValidEmailAddress(pRefUser.EmailAddress);
            if (!isValidTmp && pRefUser.EmailAddress != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidProfileimageurl(pRefUser.Profileimageurl);
            if (!isValidTmp && pRefUser.Profileimageurl != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsDisabled(pRefUser.IsDisabled);
            if (!isValidTmp && pRefUser.IsDisabled != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidCanTextMsg(pRefUser.CanTextMsg);
            if (!isValidTmp && pRefUser.CanTextMsg != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidDateTextMsgApproved(pRefUser.DateTextMsgApproved);
            if (!isValidTmp)
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
        public bool IsValidAuthUserid(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHUSERID)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_AUTHUSERID;
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
        public bool IsValidAuthConnection(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHCONNECTION)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_AUTHCONNECTION;
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
        public bool IsValidAuthProvider(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHPROVIDER)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_AUTHPROVIDER;
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
        public bool IsValidAuthAccessToken(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHACCESSTOKEN)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_AUTHACCESSTOKEN;
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
        public bool IsValidAuthIdToken(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_AUTHIDTOKEN)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_AUTHIDTOKEN;
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
        public bool IsValidEmailAddress(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_EMAIL_ADDRESS)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_EMAIL_ADDRESS;
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
        public bool IsValidProfileimageurl(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_PROFILEIMAGEURL)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PROFILEIMAGEURL;
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
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidCanTextMsg(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_CAN_TEXT_MSG)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_CAN_TEXT_MSG;
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
        public bool IsValidDateTextMsgApproved(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_DATE_TEXT_MSG_APPROVED)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_DATE_TEXT_MSG_APPROVED;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE