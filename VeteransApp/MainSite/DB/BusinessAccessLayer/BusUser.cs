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
    /// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  BusUser.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	5/13/2017	Created
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
        private const String REGEXP_ISVALID_FULLNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_FIRSTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_MIDDLENAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_LASTNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PHONE_NUMBER = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_USERNAME = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PASSWD = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_SSN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PICTURE_URL = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_PICTURE = "";
        private const String REGEXP_ISVALID_IS_DISABLED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_WELCOME_EMAIL_SENT = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_VALIDATIONTOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_VALIDATIONLINK = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_ISVALIDATED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_WELCOME_EMAIL_SENT_DATE = "";
        private const String REGEXP_ISVALID_LAST_LOGIN_DATE = "";
        private const String REGEXP_ISVALID_INTERNAL_NOTES = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
        private const String REGEXP_ISVALID_USER_MESSAGE = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
        private const String REGEXP_ISVALID_COOKIE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_HAS_CURRENT_RATING = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_CURRENT_RATING = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
        private const String REGEXP_ISVALID_INTERNAL_CALCULATED_RATING = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
        private const String REGEXP_ISVALID_SECURITY_QUESTION = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_SECURITY_ANSWER = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
        private const String REGEXP_ISVALID_NUMBER_OF_VISITS = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
        private const String REGEXP_ISVALID_PREVIOUS_VISIT_DATE = "";
        private const String REGEXP_ISVALID_LAST_VISIT_DATE = "";
        private const String REGEXP_ISVALID_IS_RATING_PROFILE_FINISHED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
        private const String REGEXP_ISVALID_USER_SOURCE_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
        private const String REGEXP_ISVALID_IMPERSONATE = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;

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
            return (Get(0, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, null, null, null, null, false, false, null, null, false, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, false, 0, 0, null, null, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, 0, false));
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
            return (Get(lUserID, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, null, null, null, null, null, null, null, false, false, null, null, false, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null, null, false, 0, 0, null, null, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), false, 0, false));
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
            return (Get(o.UserID, o.UserRoleID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Fullname, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.Username, o.Passwd, o.Ssn, o.PictureUrl, o.Picture, o.IsDisabled, o.WelcomeEmailSent, o.Validationtoken, o.Validationlink, o.Isvalidated, o.WelcomeEmailSentDate, o.WelcomeEmailSentDate, o.LastLoginDate, o.LastLoginDate, o.InternalNotes, o.UserMessage, o.CookieID, o.HasCurrentRating, o.CurrentRating, o.InternalCalculatedRating, o.SecurityQuestion, o.SecurityAnswer, o.NumberOfVisits, o.PreviousVisitDate, o.PreviousVisitDate, o.LastVisitDate, o.LastVisitDate, o.IsRatingProfileFinished, o.UserSourceID, o.Impersonate));
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
            return (Get(o.UserID, o.UserRoleID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Fullname, o.Firstname, o.Middlename, o.Lastname, o.PhoneNumber, o.Username, o.Passwd, o.Ssn, o.PictureUrl, o.Picture, o.IsDisabled, o.WelcomeEmailSent, o.Validationtoken, o.Validationlink, o.Isvalidated, o.BeginWelcomeEmailSentDate, o.EndWelcomeEmailSentDate, o.BeginLastLoginDate, o.EndLastLoginDate, o.InternalNotes, o.UserMessage, o.CookieID, o.HasCurrentRating, o.CurrentRating, o.InternalCalculatedRating, o.SecurityQuestion, o.SecurityAnswer, o.NumberOfVisits, o.BeginPreviousVisitDate, o.EndPreviousVisitDate, o.BeginLastVisitDate, o.EndLastVisitDate, o.IsRatingProfileFinished, o.UserSourceID, o.Impersonate));
        }

        /// <summary>
        ///     Gets all User objects
        ///     <remarks>   
        ///         Returns User objects in an array list 
        ///         using the given criteria 
        ///     </remarks>   
        ///     <retvalue>ArrayList containing User object</retvalue>
        /// </summary>
        public ArrayList Get(long pLngUserID, long pLngUserRoleID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrFullname, string pStrFirstname, string pStrMiddlename, string pStrLastname, string pStrPhoneNumber, string pStrUsername, string pStrPasswd, string pStrSsn, string pStrPictureUrl, byte[] pBytPicture, bool? pBolIsDisabled, bool? pBolWelcomeEmailSent, string pStrValidationtoken, string pStrValidationlink, bool? pBolIsvalidated, DateTime pDtBeginWelcomeEmailSentDate, DateTime pDtEndWelcomeEmailSentDate, DateTime pDtBeginLastLoginDate, DateTime pDtEndLastLoginDate, string pStrInternalNotes, string pStrUserMessage, string pStrCookieID, bool? pBolHasCurrentRating, long pLngCurrentRating, long pLngInternalCalculatedRating, string pStrSecurityQuestion, string pStrSecurityAnswer, long pLngNumberOfVisits, DateTime pDtBeginPreviousVisitDate, DateTime pDtEndPreviousVisitDate, DateTime pDtBeginLastVisitDate, DateTime pDtEndLastVisitDate, bool? pBolIsRatingProfileFinished, long pLngUserSourceID, bool? pBolImpersonate)
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
            enumUser.Fullname = pStrFullname;
            enumUser.Firstname = pStrFirstname;
            enumUser.Middlename = pStrMiddlename;
            enumUser.Lastname = pStrLastname;
            enumUser.PhoneNumber = pStrPhoneNumber;
            enumUser.Username = pStrUsername;
            enumUser.Passwd = pStrPasswd;
            enumUser.Ssn = pStrSsn;
            enumUser.PictureUrl = pStrPictureUrl;
            enumUser.Picture = pBytPicture;
            enumUser.IsDisabled = pBolIsDisabled;
            enumUser.WelcomeEmailSent = pBolWelcomeEmailSent;
            enumUser.Validationtoken = pStrValidationtoken;
            enumUser.Validationlink = pStrValidationlink;
            enumUser.Isvalidated = pBolIsvalidated;
            enumUser.BeginWelcomeEmailSentDate = pDtBeginWelcomeEmailSentDate;
            enumUser.EndWelcomeEmailSentDate = pDtEndWelcomeEmailSentDate;
            enumUser.BeginLastLoginDate = pDtBeginLastLoginDate;
            enumUser.EndLastLoginDate = pDtEndLastLoginDate;
            enumUser.InternalNotes = pStrInternalNotes;
            enumUser.UserMessage = pStrUserMessage;
            enumUser.CookieID = pStrCookieID;
            enumUser.HasCurrentRating = pBolHasCurrentRating;
            enumUser.CurrentRating = pLngCurrentRating;
            enumUser.InternalCalculatedRating = pLngInternalCalculatedRating;
            enumUser.SecurityQuestion = pStrSecurityQuestion;
            enumUser.SecurityAnswer = pStrSecurityAnswer;
            enumUser.NumberOfVisits = pLngNumberOfVisits;
            enumUser.BeginPreviousVisitDate = pDtBeginPreviousVisitDate;
            enumUser.EndPreviousVisitDate = pDtEndPreviousVisitDate;
            enumUser.BeginLastVisitDate = pDtBeginLastVisitDate;
            enumUser.EndLastVisitDate = pDtEndLastVisitDate;
            enumUser.IsRatingProfileFinished = pBolIsRatingProfileFinished;
            enumUser.UserSourceID = pLngUserSourceID;
            enumUser.Impersonate = pBolImpersonate;
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
            isValidTmp = IsValidFullname(pRefUser.Fullname);
            if (!isValidTmp && pRefUser.Fullname != null)
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
            isValidTmp = IsValidSsn(pRefUser.Ssn);
            if (!isValidTmp && pRefUser.Ssn != null)
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
            isValidTmp = IsValidWelcomeEmailSent(pRefUser.WelcomeEmailSent);
            if (!isValidTmp && pRefUser.WelcomeEmailSent != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidValidationtoken(pRefUser.Validationtoken);
            if (!isValidTmp && pRefUser.Validationtoken != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidValidationlink(pRefUser.Validationlink);
            if (!isValidTmp && pRefUser.Validationlink != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsvalidated(pRefUser.Isvalidated);
            if (!isValidTmp && pRefUser.Isvalidated != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidWelcomeEmailSentDate(pRefUser.WelcomeEmailSentDate);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidLastLoginDate(pRefUser.LastLoginDate);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidInternalNotes(pRefUser.InternalNotes);
            if (!isValidTmp && pRefUser.InternalNotes != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserMessage(pRefUser.UserMessage);
            if (!isValidTmp && pRefUser.UserMessage != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidCookieID(pRefUser.CookieID);
            if (!isValidTmp && pRefUser.CookieID != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidHasCurrentRating(pRefUser.HasCurrentRating);
            if (!isValidTmp && pRefUser.HasCurrentRating != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidCurrentRating(pRefUser.CurrentRating);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidInternalCalculatedRating(pRefUser.InternalCalculatedRating);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidSecurityQuestion(pRefUser.SecurityQuestion);
            if (!isValidTmp && pRefUser.SecurityQuestion != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidSecurityAnswer(pRefUser.SecurityAnswer);
            if (!isValidTmp && pRefUser.SecurityAnswer != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidNumberOfVisits(pRefUser.NumberOfVisits);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidPreviousVisitDate(pRefUser.PreviousVisitDate);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidLastVisitDate(pRefUser.LastVisitDate);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidIsRatingProfileFinished(pRefUser.IsRatingProfileFinished);
            if (!isValidTmp && pRefUser.IsRatingProfileFinished != null)
            {
                isValid = false;
            }
            isValidTmp = IsValidUserSourceID(pRefUser.UserSourceID);
            if (!isValidTmp)
            {
                isValid = false;
            }
            isValidTmp = IsValidImpersonate(pRefUser.Impersonate);
            if (!isValidTmp && pRefUser.Impersonate != null)
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
        public bool IsValidFullname(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_FULLNAME)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_FULLNAME;
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
        public bool IsValidSsn(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_SSN)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_SSN;
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
        /// <summary>
        ///     Checks to make sure value is valid
        ///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
        /// </summary>
        public bool IsValidWelcomeEmailSent(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_WELCOME_EMAIL_SENT)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_WELCOME_EMAIL_SENT;
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
        public bool IsValidValidationtoken(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_VALIDATIONTOKEN)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_VALIDATIONTOKEN;
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
        public bool IsValidValidationlink(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_VALIDATIONLINK)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_VALIDATIONLINK;
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
        public bool IsValidIsvalidated(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_ISVALIDATED)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_ISVALIDATED;
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
        public bool IsValidWelcomeEmailSentDate(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_WELCOME_EMAIL_SENT_DATE)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_WELCOME_EMAIL_SENT_DATE;
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
        public bool IsValidLastLoginDate(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_LAST_LOGIN_DATE)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_LAST_LOGIN_DATE;
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
        public bool IsValidInternalNotes(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_INTERNAL_NOTES)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_INTERNAL_NOTES;
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
        public bool IsValidUserMessage(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_USER_MESSAGE)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_USER_MESSAGE;
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
        public bool IsValidCookieID(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_COOKIE_ID)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_COOKIE_ID;
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
        public bool IsValidHasCurrentRating(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_HAS_CURRENT_RATING)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_HAS_CURRENT_RATING;
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
        public bool IsValidCurrentRating(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_CURRENT_RATING)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_CURRENT_RATING;
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
        public bool IsValidInternalCalculatedRating(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_INTERNAL_CALCULATED_RATING)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_INTERNAL_CALCULATED_RATING;
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
        public bool IsValidSecurityQuestion(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_SECURITY_QUESTION)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_SECURITY_QUESTION;
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
        public bool IsValidSecurityAnswer(string pStrData)
        {
            bool isValid = true;

            // do some validation
            isValid = !(new Regex(REGEXP_ISVALID_SECURITY_ANSWER)).IsMatch(pStrData);
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_SECURITY_ANSWER;
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
        public bool IsValidNumberOfVisits(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_NUMBER_OF_VISITS)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_NUMBER_OF_VISITS;
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
        public bool IsValidPreviousVisitDate(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_PREVIOUS_VISIT_DATE)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_PREVIOUS_VISIT_DATE;
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
        public bool IsValidLastVisitDate(DateTime pDtData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_LAST_VISIT_DATE)).IsMatch(pDtData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_LAST_VISIT_DATE;
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
        public bool IsValidIsRatingProfileFinished(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_IS_RATING_PROFILE_FINISHED)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_IS_RATING_PROFILE_FINISHED;
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
        public bool IsValidUserSourceID(long pLngData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_USER_SOURCE_ID)).IsMatch(pLngData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_USER_SOURCE_ID;
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
        public bool IsValidImpersonate(bool? pBolData)
        {
            bool isValid = true;

            // do some validation
            isValid = (new Regex(REGEXP_ISVALID_IMPERSONATE)).IsMatch(pBolData.ToString());
            if (!isValid)
            {
                Column clm = null;
                clm = new Column();
                clm.ColumnName = User.DB_FIELD_IMPERSONATE;
                clm.HasError = true;
                _arrlstColumnErrors.Add(clm);
                _hasInvalid = true;
            }
            return isValid;
        }
    }
}
// END OF CLASS FILE