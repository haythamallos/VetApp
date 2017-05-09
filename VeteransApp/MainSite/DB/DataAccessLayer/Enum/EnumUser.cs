using System;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Data;

using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.DataAccessLayer.Enumeration
{

    /// <summary>
    /// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  EnumUser.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	5/9/2017	Created
    /// 
    /// ----------------------------------------------------
    /// </summary>
    public class EnumUser
    {
        private bool _hasAny = false;
        private bool _hasMore = false;
        private bool _bSetup = false;

        private SqlCommand _cmd = null;
        private SqlDataReader _rdr = null;
        private SqlConnection _conn = null;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private int _nCount = 0;


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>Attribute of type string</summary>
        public static readonly string ENTITY_NAME = "EnumUser"; //Table name to abstract
        private static DateTime dtNull = new DateTime();
        private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

        private long _lUserID = 0;
        private long _lUserRoleID = 0;
        private DateTime _dtBeginDateCreated = new DateTime();
        private DateTime _dtEndDateCreated = new DateTime();
        private DateTime _dtBeginDateModified = new DateTime();
        private DateTime _dtEndDateModified = new DateTime();
        private string _strFullname = null;
        private string _strFirstname = null;
        private string _strMiddlename = null;
        private string _strLastname = null;
        private string _strPhoneNumber = null;
        private string _strUsername = null;
        private string _strPasswd = null;
        private string _strSsn = null;
        private string _strPictureUrl = null;
        private byte[] _bytePicture = null;
        private bool? _bIsDisabled = null;
        private bool? _bWelcomeEmailSent = null;
        private string _strValidationtoken = null;
        private string _strValidationlink = null;
        private bool? _bIsvalidated = null;
        private DateTime _dtBeginWelcomeEmailSentDate = new DateTime();
        private DateTime _dtEndWelcomeEmailSentDate = new DateTime();
        private DateTime _dtBeginLastLoginDate = new DateTime();
        private DateTime _dtEndLastLoginDate = new DateTime();
        private string _strInternalNotes = null;
        private string _strUserMessage = null;
        private string _strCookieID = null;
        private bool? _bHasCurrentRating = null;
        private long _lCurrentRating = 0;
        private long _lInternalCalculatedRating = 0;
        private string _strSecurityQuestion = null;
        private string _strSecurityAnswer = null;
        private long _lNumberOfVisits = 0;
        private DateTime _dtBeginPreviousVisitDate = new DateTime();
        private DateTime _dtEndPreviousVisitDate = new DateTime();
        private DateTime _dtBeginLastVisitDate = new DateTime();
        private DateTime _dtEndLastVisitDate = new DateTime();
        private bool? _bIsRatingProfileFinished = null;
        private long _lUserSourceID = 0;
        //		private string _strOrderByEnum = "ASC";
        private string _strOrderByField = DB_FIELD_ID;

        /// <summary>DB_FIELD_ID Attribute type string</summary>
        public static readonly string DB_FIELD_ID = "user_id"; //Table id field name
                                                               /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Attribute UserID  name
                                                              /// <summary>UserRoleID Attribute type string</summary>
        public static readonly string TAG_USER_ROLE_ID = "UserRoleID"; //Attribute UserRoleID  name
                                                                       /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
                                                                                   /// <summary>EndDateCreated Attribute type string</summary>
        public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
                                                                               /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
                                                                                     /// <summary>EndDateModified Attribute type string</summary>
        public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
                                                                                 /// <summary>Fullname Attribute type string</summary>
        public static readonly string TAG_FULLNAME = "Fullname"; //Attribute Fullname  name
                                                                 /// <summary>Firstname Attribute type string</summary>
        public static readonly string TAG_FIRSTNAME = "Firstname"; //Attribute Firstname  name
                                                                   /// <summary>Middlename Attribute type string</summary>
        public static readonly string TAG_MIDDLENAME = "Middlename"; //Attribute Middlename  name
                                                                     /// <summary>Lastname Attribute type string</summary>
        public static readonly string TAG_LASTNAME = "Lastname"; //Attribute Lastname  name
                                                                 /// <summary>PhoneNumber Attribute type string</summary>
        public static readonly string TAG_PHONE_NUMBER = "PhoneNumber"; //Attribute PhoneNumber  name
                                                                        /// <summary>Username Attribute type string</summary>
        public static readonly string TAG_USERNAME = "Username"; //Attribute Username  name
                                                                 /// <summary>Passwd Attribute type string</summary>
        public static readonly string TAG_PASSWD = "Passwd"; //Attribute Passwd  name
                                                             /// <summary>Ssn Attribute type string</summary>
        public static readonly string TAG_SSN = "Ssn"; //Attribute Ssn  name
                                                       /// <summary>PictureUrl Attribute type string</summary>
        public static readonly string TAG_PICTURE_URL = "PictureUrl"; //Attribute PictureUrl  name
                                                                      /// <summary>Picture Attribute type string</summary>
        public static readonly string TAG_PICTURE = "Picture"; //Attribute Picture  name
                                                               /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Attribute IsDisabled  name
                                                                      /// <summary>WelcomeEmailSent Attribute type string</summary>
        public static readonly string TAG_WELCOME_EMAIL_SENT = "WelcomeEmailSent"; //Attribute WelcomeEmailSent  name
                                                                                   /// <summary>Validationtoken Attribute type string</summary>
        public static readonly string TAG_VALIDATIONTOKEN = "Validationtoken"; //Attribute Validationtoken  name
                                                                               /// <summary>Validationlink Attribute type string</summary>
        public static readonly string TAG_VALIDATIONLINK = "Validationlink"; //Attribute Validationlink  name
                                                                             /// <summary>Isvalidated Attribute type string</summary>
        public static readonly string TAG_ISVALIDATED = "Isvalidated"; //Attribute Isvalidated  name
                                                                       /// <summary>WelcomeEmailSentDate Attribute type string</summary>
        public static readonly string TAG_BEGIN_WELCOME_EMAIL_SENT_DATE = "BeginWelcomeEmailSentDate"; //Attribute WelcomeEmailSentDate  name
                                                                                                       /// <summary>EndWelcomeEmailSentDate Attribute type string</summary>
        public static readonly string TAG_END_WELCOME_EMAIL_SENT_DATE = "EndWelcomeEmailSentDate"; //Attribute WelcomeEmailSentDate  name
                                                                                                   /// <summary>LastLoginDate Attribute type string</summary>
        public static readonly string TAG_BEGIN_LAST_LOGIN_DATE = "BeginLastLoginDate"; //Attribute LastLoginDate  name
                                                                                        /// <summary>EndLastLoginDate Attribute type string</summary>
        public static readonly string TAG_END_LAST_LOGIN_DATE = "EndLastLoginDate"; //Attribute LastLoginDate  name
                                                                                    /// <summary>InternalNotes Attribute type string</summary>
        public static readonly string TAG_INTERNAL_NOTES = "InternalNotes"; //Attribute InternalNotes  name
                                                                            /// <summary>UserMessage Attribute type string</summary>
        public static readonly string TAG_USER_MESSAGE = "UserMessage"; //Attribute UserMessage  name
                                                                        /// <summary>CookieID Attribute type string</summary>
        public static readonly string TAG_COOKIE_ID = "CookieID"; //Attribute CookieID  name
                                                                  /// <summary>HasCurrentRating Attribute type string</summary>
        public static readonly string TAG_HAS_CURRENT_RATING = "HasCurrentRating"; //Attribute HasCurrentRating  name
                                                                                   /// <summary>CurrentRating Attribute type string</summary>
        public static readonly string TAG_CURRENT_RATING = "CurrentRating"; //Attribute CurrentRating  name
                                                                            /// <summary>InternalCalculatedRating Attribute type string</summary>
        public static readonly string TAG_INTERNAL_CALCULATED_RATING = "InternalCalculatedRating"; //Attribute InternalCalculatedRating  name
                                                                                                   /// <summary>SecurityQuestion Attribute type string</summary>
        public static readonly string TAG_SECURITY_QUESTION = "SecurityQuestion"; //Attribute SecurityQuestion  name
                                                                                  /// <summary>SecurityAnswer Attribute type string</summary>
        public static readonly string TAG_SECURITY_ANSWER = "SecurityAnswer"; //Attribute SecurityAnswer  name
                                                                              /// <summary>NumberOfVisits Attribute type string</summary>
        public static readonly string TAG_NUMBER_OF_VISITS = "NumberOfVisits"; //Attribute NumberOfVisits  name
                                                                               /// <summary>PreviousVisitDate Attribute type string</summary>
        public static readonly string TAG_BEGIN_PREVIOUS_VISIT_DATE = "BeginPreviousVisitDate"; //Attribute PreviousVisitDate  name
                                                                                                /// <summary>EndPreviousVisitDate Attribute type string</summary>
        public static readonly string TAG_END_PREVIOUS_VISIT_DATE = "EndPreviousVisitDate"; //Attribute PreviousVisitDate  name
                                                                                            /// <summary>LastVisitDate Attribute type string</summary>
        public static readonly string TAG_BEGIN_LAST_VISIT_DATE = "BeginLastVisitDate"; //Attribute LastVisitDate  name
                                                                                        /// <summary>EndLastVisitDate Attribute type string</summary>
        public static readonly string TAG_END_LAST_VISIT_DATE = "EndLastVisitDate"; //Attribute LastVisitDate  name
                                                                                    /// <summary>IsRatingProfileFinished Attribute type string</summary>
        public static readonly string TAG_IS_RATING_PROFILE_FINISHED = "IsRatingProfileFinished"; //Attribute IsRatingProfileFinished  name
                                                                                                  /// <summary>UserSourceID Attribute type string</summary>
        public static readonly string TAG_USER_SOURCE_ID = "UserSourceID"; //Attribute UserSourceID  name
                                                                           // Stored procedure name
        public string SP_ENUM_NAME = "spUserEnum"; //Enum sp name

        /// <summary>HasError is a Property in the User Class of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        /// <summary>UserID is a Property in the User Class of type long</summary>
        public long UserID
        {
            get { return _lUserID; }
            set { _lUserID = value; }
        }
        /// <summary>UserRoleID is a Property in the User Class of type long</summary>
        public long UserRoleID
        {
            get { return _lUserRoleID; }
            set { _lUserRoleID = value; }
        }
        /// <summary>Property DateCreated. Type: DateTime</summary>
        public DateTime BeginDateCreated
        {
            get { return _dtBeginDateCreated; }
            set { _dtBeginDateCreated = value; }
        }
        /// <summary>Property DateCreated. Type: DateTime</summary>
        public DateTime EndDateCreated
        {
            get { return _dtEndDateCreated; }
            set { _dtEndDateCreated = value; }
        }
        /// <summary>Property DateModified. Type: DateTime</summary>
        public DateTime BeginDateModified
        {
            get { return _dtBeginDateModified; }
            set { _dtBeginDateModified = value; }
        }
        /// <summary>Property DateModified. Type: DateTime</summary>
        public DateTime EndDateModified
        {
            get { return _dtEndDateModified; }
            set { _dtEndDateModified = value; }
        }
        /// <summary>Fullname is a Property in the User Class of type String</summary>
        public string Fullname
        {
            get { return _strFullname; }
            set { _strFullname = value; }
        }
        /// <summary>Firstname is a Property in the User Class of type String</summary>
        public string Firstname
        {
            get { return _strFirstname; }
            set { _strFirstname = value; }
        }
        /// <summary>Middlename is a Property in the User Class of type String</summary>
        public string Middlename
        {
            get { return _strMiddlename; }
            set { _strMiddlename = value; }
        }
        /// <summary>Lastname is a Property in the User Class of type String</summary>
        public string Lastname
        {
            get { return _strLastname; }
            set { _strLastname = value; }
        }
        /// <summary>PhoneNumber is a Property in the User Class of type String</summary>
        public string PhoneNumber
        {
            get { return _strPhoneNumber; }
            set { _strPhoneNumber = value; }
        }
        /// <summary>Username is a Property in the User Class of type String</summary>
        public string Username
        {
            get { return _strUsername; }
            set { _strUsername = value; }
        }
        /// <summary>Passwd is a Property in the User Class of type String</summary>
        public string Passwd
        {
            get { return _strPasswd; }
            set { _strPasswd = value; }
        }
        /// <summary>Ssn is a Property in the User Class of type String</summary>
        public string Ssn
        {
            get { return _strSsn; }
            set { _strSsn = value; }
        }
        /// <summary>PictureUrl is a Property in the User Class of type String</summary>
        public string PictureUrl
        {
            get { return _strPictureUrl; }
            set { _strPictureUrl = value; }
        }
        /// <summary>Picture is a Property in the User Class of type byte[]</summary>
        public byte[] Picture
        {
            get { return _bytePicture; }
            set { _bytePicture = value; }
        }
        /// <summary>IsDisabled is a Property in the User Class of type bool</summary>
        public bool? IsDisabled
        {
            get { return _bIsDisabled; }
            set { _bIsDisabled = value; }
        }
        /// <summary>WelcomeEmailSent is a Property in the User Class of type bool</summary>
        public bool? WelcomeEmailSent
        {
            get { return _bWelcomeEmailSent; }
            set { _bWelcomeEmailSent = value; }
        }
        /// <summary>Validationtoken is a Property in the User Class of type String</summary>
        public string Validationtoken
        {
            get { return _strValidationtoken; }
            set { _strValidationtoken = value; }
        }
        /// <summary>Validationlink is a Property in the User Class of type String</summary>
        public string Validationlink
        {
            get { return _strValidationlink; }
            set { _strValidationlink = value; }
        }
        /// <summary>Isvalidated is a Property in the User Class of type bool</summary>
        public bool? Isvalidated
        {
            get { return _bIsvalidated; }
            set { _bIsvalidated = value; }
        }
        /// <summary>Property WelcomeEmailSentDate. Type: DateTime</summary>
        public DateTime BeginWelcomeEmailSentDate
        {
            get { return _dtBeginWelcomeEmailSentDate; }
            set { _dtBeginWelcomeEmailSentDate = value; }
        }
        /// <summary>Property WelcomeEmailSentDate. Type: DateTime</summary>
        public DateTime EndWelcomeEmailSentDate
        {
            get { return _dtEndWelcomeEmailSentDate; }
            set { _dtEndWelcomeEmailSentDate = value; }
        }
        /// <summary>Property LastLoginDate. Type: DateTime</summary>
        public DateTime BeginLastLoginDate
        {
            get { return _dtBeginLastLoginDate; }
            set { _dtBeginLastLoginDate = value; }
        }
        /// <summary>Property LastLoginDate. Type: DateTime</summary>
        public DateTime EndLastLoginDate
        {
            get { return _dtEndLastLoginDate; }
            set { _dtEndLastLoginDate = value; }
        }
        /// <summary>InternalNotes is a Property in the User Class of type String</summary>
        public string InternalNotes
        {
            get { return _strInternalNotes; }
            set { _strInternalNotes = value; }
        }
        /// <summary>UserMessage is a Property in the User Class of type String</summary>
        public string UserMessage
        {
            get { return _strUserMessage; }
            set { _strUserMessage = value; }
        }
        /// <summary>CookieID is a Property in the User Class of type String</summary>
        public string CookieID
        {
            get { return _strCookieID; }
            set { _strCookieID = value; }
        }
        /// <summary>HasCurrentRating is a Property in the User Class of type bool</summary>
        public bool? HasCurrentRating
        {
            get { return _bHasCurrentRating; }
            set { _bHasCurrentRating = value; }
        }
        /// <summary>CurrentRating is a Property in the User Class of type long</summary>
        public long CurrentRating
        {
            get { return _lCurrentRating; }
            set { _lCurrentRating = value; }
        }
        /// <summary>InternalCalculatedRating is a Property in the User Class of type long</summary>
        public long InternalCalculatedRating
        {
            get { return _lInternalCalculatedRating; }
            set { _lInternalCalculatedRating = value; }
        }
        /// <summary>SecurityQuestion is a Property in the User Class of type String</summary>
        public string SecurityQuestion
        {
            get { return _strSecurityQuestion; }
            set { _strSecurityQuestion = value; }
        }
        /// <summary>SecurityAnswer is a Property in the User Class of type String</summary>
        public string SecurityAnswer
        {
            get { return _strSecurityAnswer; }
            set { _strSecurityAnswer = value; }
        }
        /// <summary>NumberOfVisits is a Property in the User Class of type long</summary>
        public long NumberOfVisits
        {
            get { return _lNumberOfVisits; }
            set { _lNumberOfVisits = value; }
        }
        /// <summary>Property PreviousVisitDate. Type: DateTime</summary>
        public DateTime BeginPreviousVisitDate
        {
            get { return _dtBeginPreviousVisitDate; }
            set { _dtBeginPreviousVisitDate = value; }
        }
        /// <summary>Property PreviousVisitDate. Type: DateTime</summary>
        public DateTime EndPreviousVisitDate
        {
            get { return _dtEndPreviousVisitDate; }
            set { _dtEndPreviousVisitDate = value; }
        }
        /// <summary>Property LastVisitDate. Type: DateTime</summary>
        public DateTime BeginLastVisitDate
        {
            get { return _dtBeginLastVisitDate; }
            set { _dtBeginLastVisitDate = value; }
        }
        /// <summary>Property LastVisitDate. Type: DateTime</summary>
        public DateTime EndLastVisitDate
        {
            get { return _dtEndLastVisitDate; }
            set { _dtEndLastVisitDate = value; }
        }
        /// <summary>IsRatingProfileFinished is a Property in the User Class of type bool</summary>
        public bool? IsRatingProfileFinished
        {
            get { return _bIsRatingProfileFinished; }
            set { _bIsRatingProfileFinished = value; }
        }
        /// <summary>UserSourceID is a Property in the User Class of type long</summary>
        public long UserSourceID
        {
            get { return _lUserSourceID; }
            set { _lUserSourceID = value; }
        }

        /// <summary>Count Property. Type: int</summary>
        public int Count
        {
            get
            {
                _bSetup = true;
                // if necessary, close the old reader
                if ((_cmd != null) || (_rdr != null))
                {
                    Close();
                }
                _cmd = new SqlCommand(SP_ENUM_NAME, _conn);
                _cmd.CommandType = CommandType.StoredProcedure;
                _setupEnumParams();
                _setupCountParams();
                _cmd.Connection = _conn;
                _cmd.ExecuteNonQuery();
                try
                {
                    string strTmp;
                    strTmp = _cmd.Parameters[PARAM_COUNT].Value.ToString();
                    _nCount = int.Parse(strTmp);
                }
                catch
                {
                    _nCount = 0;
                }
                return _nCount;
            }
        }

        /// <summary>Contructor takes 1 parameter: SqlConnection</summary>
        public EnumUser()
        {
        }
        /// <summary>Contructor takes 1 parameter: SqlConnection</summary>
        public EnumUser(SqlConnection conn)
        {
            _conn = conn;
        }


        // Implementation of IEnumerator
        /// <summary>Property of type User. Returns the next User in the list</summary>
        private User _nextTransaction
        {
            get
            {
                User o = null;

                if (!_bSetup)
                {
                    EnumData();
                }
                if (_hasMore)
                {
                    o = new User(_rdr);
                    _hasMore = _rdr.Read();
                    if (!_hasMore)
                    {
                        Close();
                    }
                }
                return o;
            }
        }

        /// <summary>Enumerates the Data</summary>
        public void EnumData()
        {
            if (!_bSetup)
            {
                _bSetup = true;
                // if necessary, close the old reader
                if ((_cmd != null) || (_rdr != null))
                {
                    Close();
                }
                _cmd = new SqlCommand(SP_ENUM_NAME, _conn);
                _cmd.CommandType = CommandType.StoredProcedure;
                _setupEnumParams();
                _cmd.Connection = _conn;
                _rdr = _cmd.ExecuteReader();
                _hasAny = _rdr.Read();
                _hasMore = _hasAny;
            }
        }


        /// <summary>returns the next element in the enumeration</summary>
        public object nextElement()
        {
            try
            {
                return _nextTransaction;
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
                return null;
            }
        }

        /// <summary>Returns whether or not more elements exist</summary>
        public bool hasMoreElements()
        {
            try
            {
                if (_bSetup)
                {
                    EnumData();
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }

            return _hasMore;
        }

        /// <summary>Closes the datareader</summary>
        public void Close()
        {
            try
            {
                if (_rdr != null)
                {
                    _rdr.Dispose();
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
            _rdr = null;
            _cmd = null;
        }

        /// <summary>ToString is overridden to display all properties of the User Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_USER_ID + ":  " + UserID.ToString() + "\n");
            sbReturn.Append(TAG_USER_ROLE_ID + ":  " + UserRoleID + "\n");
            if (!dtNull.Equals(BeginDateCreated))
            {
                sbReturn.Append(TAG_BEGIN_DATE_CREATED + ":  " + BeginDateCreated.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_DATE_CREATED + ":\n");
            }
            if (!dtNull.Equals(EndDateCreated))
            {
                sbReturn.Append(TAG_END_DATE_CREATED + ":  " + EndDateCreated.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_DATE_CREATED + ":\n");
            }
            if (!dtNull.Equals(BeginDateModified))
            {
                sbReturn.Append(TAG_BEGIN_DATE_MODIFIED + ":  " + BeginDateModified.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_DATE_MODIFIED + ":\n");
            }
            if (!dtNull.Equals(EndDateModified))
            {
                sbReturn.Append(TAG_END_DATE_MODIFIED + ":  " + EndDateModified.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_DATE_MODIFIED + ":\n");
            }
            sbReturn.Append(TAG_FULLNAME + ":  " + Fullname + "\n");
            sbReturn.Append(TAG_FIRSTNAME + ":  " + Firstname + "\n");
            sbReturn.Append(TAG_MIDDLENAME + ":  " + Middlename + "\n");
            sbReturn.Append(TAG_LASTNAME + ":  " + Lastname + "\n");
            sbReturn.Append(TAG_PHONE_NUMBER + ":  " + PhoneNumber + "\n");
            sbReturn.Append(TAG_USERNAME + ":  " + Username + "\n");
            sbReturn.Append(TAG_PASSWD + ":  " + Passwd + "\n");
            sbReturn.Append(TAG_SSN + ":  " + Ssn + "\n");
            sbReturn.Append(TAG_PICTURE_URL + ":  " + PictureUrl + "\n");
            sbReturn.Append(TAG_PICTURE + ":  " + Picture + "\n");
            sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
            sbReturn.Append(TAG_WELCOME_EMAIL_SENT + ":  " + WelcomeEmailSent + "\n");
            sbReturn.Append(TAG_VALIDATIONTOKEN + ":  " + Validationtoken + "\n");
            sbReturn.Append(TAG_VALIDATIONLINK + ":  " + Validationlink + "\n");
            sbReturn.Append(TAG_ISVALIDATED + ":  " + Isvalidated + "\n");
            if (!dtNull.Equals(BeginWelcomeEmailSentDate))
            {
                sbReturn.Append(TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ":  " + BeginWelcomeEmailSentDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ":\n");
            }
            if (!dtNull.Equals(EndWelcomeEmailSentDate))
            {
                sbReturn.Append(TAG_END_WELCOME_EMAIL_SENT_DATE + ":  " + EndWelcomeEmailSentDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_WELCOME_EMAIL_SENT_DATE + ":\n");
            }
            if (!dtNull.Equals(BeginLastLoginDate))
            {
                sbReturn.Append(TAG_BEGIN_LAST_LOGIN_DATE + ":  " + BeginLastLoginDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_LAST_LOGIN_DATE + ":\n");
            }
            if (!dtNull.Equals(EndLastLoginDate))
            {
                sbReturn.Append(TAG_END_LAST_LOGIN_DATE + ":  " + EndLastLoginDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_LAST_LOGIN_DATE + ":\n");
            }
            sbReturn.Append(TAG_INTERNAL_NOTES + ":  " + InternalNotes + "\n");
            sbReturn.Append(TAG_USER_MESSAGE + ":  " + UserMessage + "\n");
            sbReturn.Append(TAG_COOKIE_ID + ":  " + CookieID + "\n");
            sbReturn.Append(TAG_HAS_CURRENT_RATING + ":  " + HasCurrentRating + "\n");
            sbReturn.Append(TAG_CURRENT_RATING + ":  " + CurrentRating + "\n");
            sbReturn.Append(TAG_INTERNAL_CALCULATED_RATING + ":  " + InternalCalculatedRating + "\n");
            sbReturn.Append(TAG_SECURITY_QUESTION + ":  " + SecurityQuestion + "\n");
            sbReturn.Append(TAG_SECURITY_ANSWER + ":  " + SecurityAnswer + "\n");
            sbReturn.Append(TAG_NUMBER_OF_VISITS + ":  " + NumberOfVisits + "\n");
            if (!dtNull.Equals(BeginPreviousVisitDate))
            {
                sbReturn.Append(TAG_BEGIN_PREVIOUS_VISIT_DATE + ":  " + BeginPreviousVisitDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_PREVIOUS_VISIT_DATE + ":\n");
            }
            if (!dtNull.Equals(EndPreviousVisitDate))
            {
                sbReturn.Append(TAG_END_PREVIOUS_VISIT_DATE + ":  " + EndPreviousVisitDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_PREVIOUS_VISIT_DATE + ":\n");
            }
            if (!dtNull.Equals(BeginLastVisitDate))
            {
                sbReturn.Append(TAG_BEGIN_LAST_VISIT_DATE + ":  " + BeginLastVisitDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_LAST_VISIT_DATE + ":\n");
            }
            if (!dtNull.Equals(EndLastVisitDate))
            {
                sbReturn.Append(TAG_END_LAST_VISIT_DATE + ":  " + EndLastVisitDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_LAST_VISIT_DATE + ":\n");
            }
            sbReturn.Append(TAG_IS_RATING_PROFILE_FINISHED + ":  " + IsRatingProfileFinished + "\n");
            sbReturn.Append(TAG_USER_SOURCE_ID + ":  " + UserSourceID + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of User</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<" + ENTITY_NAME + ">\n");
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_USER_ROLE_ID + ">" + UserRoleID + "</" + TAG_USER_ROLE_ID + ">\n");
            if (!dtNull.Equals(BeginDateCreated))
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_CREATED + ">" + BeginDateCreated.ToString() + "</" + TAG_BEGIN_DATE_CREATED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_CREATED + "></" + TAG_BEGIN_DATE_CREATED + ">\n");
            }
            if (!dtNull.Equals(EndDateCreated))
            {
                sbReturn.Append("<" + TAG_END_DATE_CREATED + ">" + EndDateCreated.ToString() + "</" + TAG_END_DATE_CREATED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_DATE_CREATED + "></" + TAG_END_DATE_CREATED + ">\n");
            }
            if (!dtNull.Equals(BeginDateModified))
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_MODIFIED + ">" + BeginDateModified.ToString() + "</" + TAG_BEGIN_DATE_MODIFIED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_MODIFIED + "></" + TAG_BEGIN_DATE_MODIFIED + ">\n");
            }
            if (!dtNull.Equals(EndDateModified))
            {
                sbReturn.Append("<" + TAG_END_DATE_MODIFIED + ">" + EndDateModified.ToString() + "</" + TAG_END_DATE_MODIFIED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_DATE_MODIFIED + "></" + TAG_END_DATE_MODIFIED + ">\n");
            }
            sbReturn.Append("<" + TAG_FULLNAME + ">" + Fullname + "</" + TAG_FULLNAME + ">\n");
            sbReturn.Append("<" + TAG_FIRSTNAME + ">" + Firstname + "</" + TAG_FIRSTNAME + ">\n");
            sbReturn.Append("<" + TAG_MIDDLENAME + ">" + Middlename + "</" + TAG_MIDDLENAME + ">\n");
            sbReturn.Append("<" + TAG_LASTNAME + ">" + Lastname + "</" + TAG_LASTNAME + ">\n");
            sbReturn.Append("<" + TAG_PHONE_NUMBER + ">" + PhoneNumber + "</" + TAG_PHONE_NUMBER + ">\n");
            sbReturn.Append("<" + TAG_USERNAME + ">" + Username + "</" + TAG_USERNAME + ">\n");
            sbReturn.Append("<" + TAG_PASSWD + ">" + Passwd + "</" + TAG_PASSWD + ">\n");
            sbReturn.Append("<" + TAG_SSN + ">" + Ssn + "</" + TAG_SSN + ">\n");
            sbReturn.Append("<" + TAG_PICTURE_URL + ">" + PictureUrl + "</" + TAG_PICTURE_URL + ">\n");
            sbReturn.Append("<" + TAG_PICTURE + ">" + Picture + "</" + TAG_PICTURE + ">\n");
            sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
            sbReturn.Append("<" + TAG_WELCOME_EMAIL_SENT + ">" + WelcomeEmailSent + "</" + TAG_WELCOME_EMAIL_SENT + ">\n");
            sbReturn.Append("<" + TAG_VALIDATIONTOKEN + ">" + Validationtoken + "</" + TAG_VALIDATIONTOKEN + ">\n");
            sbReturn.Append("<" + TAG_VALIDATIONLINK + ">" + Validationlink + "</" + TAG_VALIDATIONLINK + ">\n");
            sbReturn.Append("<" + TAG_ISVALIDATED + ">" + Isvalidated + "</" + TAG_ISVALIDATED + ">\n");
            if (!dtNull.Equals(BeginWelcomeEmailSentDate))
            {
                sbReturn.Append("<" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ">" + BeginWelcomeEmailSentDate.ToString() + "</" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + "></" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            if (!dtNull.Equals(EndWelcomeEmailSentDate))
            {
                sbReturn.Append("<" + TAG_END_WELCOME_EMAIL_SENT_DATE + ">" + EndWelcomeEmailSentDate.ToString() + "</" + TAG_END_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_WELCOME_EMAIL_SENT_DATE + "></" + TAG_END_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            if (!dtNull.Equals(BeginLastLoginDate))
            {
                sbReturn.Append("<" + TAG_BEGIN_LAST_LOGIN_DATE + ">" + BeginLastLoginDate.ToString() + "</" + TAG_BEGIN_LAST_LOGIN_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_LAST_LOGIN_DATE + "></" + TAG_BEGIN_LAST_LOGIN_DATE + ">\n");
            }
            if (!dtNull.Equals(EndLastLoginDate))
            {
                sbReturn.Append("<" + TAG_END_LAST_LOGIN_DATE + ">" + EndLastLoginDate.ToString() + "</" + TAG_END_LAST_LOGIN_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_LAST_LOGIN_DATE + "></" + TAG_END_LAST_LOGIN_DATE + ">\n");
            }
            sbReturn.Append("<" + TAG_INTERNAL_NOTES + ">" + InternalNotes + "</" + TAG_INTERNAL_NOTES + ">\n");
            sbReturn.Append("<" + TAG_USER_MESSAGE + ">" + UserMessage + "</" + TAG_USER_MESSAGE + ">\n");
            sbReturn.Append("<" + TAG_COOKIE_ID + ">" + CookieID + "</" + TAG_COOKIE_ID + ">\n");
            sbReturn.Append("<" + TAG_HAS_CURRENT_RATING + ">" + HasCurrentRating + "</" + TAG_HAS_CURRENT_RATING + ">\n");
            sbReturn.Append("<" + TAG_CURRENT_RATING + ">" + CurrentRating + "</" + TAG_CURRENT_RATING + ">\n");
            sbReturn.Append("<" + TAG_INTERNAL_CALCULATED_RATING + ">" + InternalCalculatedRating + "</" + TAG_INTERNAL_CALCULATED_RATING + ">\n");
            sbReturn.Append("<" + TAG_SECURITY_QUESTION + ">" + SecurityQuestion + "</" + TAG_SECURITY_QUESTION + ">\n");
            sbReturn.Append("<" + TAG_SECURITY_ANSWER + ">" + SecurityAnswer + "</" + TAG_SECURITY_ANSWER + ">\n");
            sbReturn.Append("<" + TAG_NUMBER_OF_VISITS + ">" + NumberOfVisits + "</" + TAG_NUMBER_OF_VISITS + ">\n");
            if (!dtNull.Equals(BeginPreviousVisitDate))
            {
                sbReturn.Append("<" + TAG_BEGIN_PREVIOUS_VISIT_DATE + ">" + BeginPreviousVisitDate.ToString() + "</" + TAG_BEGIN_PREVIOUS_VISIT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_PREVIOUS_VISIT_DATE + "></" + TAG_BEGIN_PREVIOUS_VISIT_DATE + ">\n");
            }
            if (!dtNull.Equals(EndPreviousVisitDate))
            {
                sbReturn.Append("<" + TAG_END_PREVIOUS_VISIT_DATE + ">" + EndPreviousVisitDate.ToString() + "</" + TAG_END_PREVIOUS_VISIT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_PREVIOUS_VISIT_DATE + "></" + TAG_END_PREVIOUS_VISIT_DATE + ">\n");
            }
            if (!dtNull.Equals(BeginLastVisitDate))
            {
                sbReturn.Append("<" + TAG_BEGIN_LAST_VISIT_DATE + ">" + BeginLastVisitDate.ToString() + "</" + TAG_BEGIN_LAST_VISIT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_LAST_VISIT_DATE + "></" + TAG_BEGIN_LAST_VISIT_DATE + ">\n");
            }
            if (!dtNull.Equals(EndLastVisitDate))
            {
                sbReturn.Append("<" + TAG_END_LAST_VISIT_DATE + ">" + EndLastVisitDate.ToString() + "</" + TAG_END_LAST_VISIT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_LAST_VISIT_DATE + "></" + TAG_END_LAST_VISIT_DATE + ">\n");
            }
            sbReturn.Append("<" + TAG_IS_RATING_PROFILE_FINISHED + ">" + IsRatingProfileFinished + "</" + TAG_IS_RATING_PROFILE_FINISHED + ">\n");
            sbReturn.Append("<" + TAG_USER_SOURCE_ID + ">" + UserSourceID + "</" + TAG_USER_SOURCE_ID + ">\n");
            sbReturn.Append("</" + ENTITY_NAME + ">" + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Parse XML string and assign values to object</summary>
        public void Parse(string pStrXml)
        {
            try
            {
                XmlDocument xmlDoc = null;
                string strXPath = null;
                XmlNodeList xNodes = null;

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(pStrXml);

                // get the element
                strXPath = "//" + ENTITY_NAME;
                xNodes = xmlDoc.SelectNodes(strXPath);
                if (xNodes.Count > 0)
                {
                    Parse(xNodes.Item(0));
                }
            }
            catch
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
        }
        /// <summary>Parse accepts an XmlNode and parses values</summary>
        public void Parse(XmlNode xNode)
        {
            XmlNode xResultNode = null;
            string strTmp = null;

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USER_ID);
                strTmp = xResultNode.InnerText;
                UserID = (long)Convert.ToInt32(strTmp);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USER_ROLE_ID);
                UserRoleID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                UserRoleID = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_CREATED);
                BeginDateCreated = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_DATE_CREATED);
                EndDateCreated = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_MODIFIED);
                BeginDateModified = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_DATE_MODIFIED);
                EndDateModified = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_FULLNAME);
                Fullname = xResultNode.InnerText;
                if (Fullname.Trim().Length == 0)
                    Fullname = null;
            }
            catch
            {
                Fullname = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_FIRSTNAME);
                Firstname = xResultNode.InnerText;
                if (Firstname.Trim().Length == 0)
                    Firstname = null;
            }
            catch
            {
                Firstname = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_MIDDLENAME);
                Middlename = xResultNode.InnerText;
                if (Middlename.Trim().Length == 0)
                    Middlename = null;
            }
            catch
            {
                Middlename = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_LASTNAME);
                Lastname = xResultNode.InnerText;
                if (Lastname.Trim().Length == 0)
                    Lastname = null;
            }
            catch
            {
                Lastname = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PHONE_NUMBER);
                PhoneNumber = xResultNode.InnerText;
                if (PhoneNumber.Trim().Length == 0)
                    PhoneNumber = null;
            }
            catch
            {
                PhoneNumber = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USERNAME);
                Username = xResultNode.InnerText;
                if (Username.Trim().Length == 0)
                    Username = null;
            }
            catch
            {
                Username = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PASSWD);
                Passwd = xResultNode.InnerText;
                if (Passwd.Trim().Length == 0)
                    Passwd = null;
            }
            catch
            {
                Passwd = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_SSN);
                Ssn = xResultNode.InnerText;
                if (Ssn.Trim().Length == 0)
                    Ssn = null;
            }
            catch
            {
                Ssn = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PICTURE_URL);
                PictureUrl = xResultNode.InnerText;
                if (PictureUrl.Trim().Length == 0)
                    PictureUrl = null;
            }
            catch
            {
                PictureUrl = null;
            }
            // Cannot reliably convert a byte[] to a string.

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_IS_DISABLED);
                IsDisabled = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                IsDisabled = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_WELCOME_EMAIL_SENT);
                WelcomeEmailSent = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                WelcomeEmailSent = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_VALIDATIONTOKEN);
                Validationtoken = xResultNode.InnerText;
                if (Validationtoken.Trim().Length == 0)
                    Validationtoken = null;
            }
            catch
            {
                Validationtoken = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_VALIDATIONLINK);
                Validationlink = xResultNode.InnerText;
                if (Validationlink.Trim().Length == 0)
                    Validationlink = null;
            }
            catch
            {
                Validationlink = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_ISVALIDATED);
                Isvalidated = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                Isvalidated = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_WELCOME_EMAIL_SENT_DATE);
                BeginWelcomeEmailSentDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_WELCOME_EMAIL_SENT_DATE);
                EndWelcomeEmailSentDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_LAST_LOGIN_DATE);
                BeginLastLoginDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_LAST_LOGIN_DATE);
                EndLastLoginDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_INTERNAL_NOTES);
                InternalNotes = xResultNode.InnerText;
                if (InternalNotes.Trim().Length == 0)
                    InternalNotes = null;
            }
            catch
            {
                InternalNotes = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USER_MESSAGE);
                UserMessage = xResultNode.InnerText;
                if (UserMessage.Trim().Length == 0)
                    UserMessage = null;
            }
            catch
            {
                UserMessage = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_COOKIE_ID);
                CookieID = xResultNode.InnerText;
                if (CookieID.Trim().Length == 0)
                    CookieID = null;
            }
            catch
            {
                CookieID = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_HAS_CURRENT_RATING);
                HasCurrentRating = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                HasCurrentRating = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_CURRENT_RATING);
                CurrentRating = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                CurrentRating = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_INTERNAL_CALCULATED_RATING);
                InternalCalculatedRating = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                InternalCalculatedRating = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_SECURITY_QUESTION);
                SecurityQuestion = xResultNode.InnerText;
                if (SecurityQuestion.Trim().Length == 0)
                    SecurityQuestion = null;
            }
            catch
            {
                SecurityQuestion = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_SECURITY_ANSWER);
                SecurityAnswer = xResultNode.InnerText;
                if (SecurityAnswer.Trim().Length == 0)
                    SecurityAnswer = null;
            }
            catch
            {
                SecurityAnswer = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_NUMBER_OF_VISITS);
                NumberOfVisits = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                NumberOfVisits = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_PREVIOUS_VISIT_DATE);
                BeginPreviousVisitDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_PREVIOUS_VISIT_DATE);
                EndPreviousVisitDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_LAST_VISIT_DATE);
                BeginLastVisitDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_LAST_VISIT_DATE);
                EndLastVisitDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_IS_RATING_PROFILE_FINISHED);
                IsRatingProfileFinished = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                IsRatingProfileFinished = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USER_SOURCE_ID);
                UserSourceID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                UserSourceID = 0;
            }
        }
        /// <summary>Prompt for values</summary>
        public void Prompt()
        {
            try
            {
                Console.WriteLine(TAG_USER_ROLE_ID + ":  ");
                try
                {
                    UserRoleID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UserRoleID = 0;
                }

                Console.WriteLine(TAG_BEGIN_DATE_CREATED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginDateCreated = DateTime.Parse(s);
                }
                catch
                {
                    BeginDateCreated = new DateTime();
                }

                Console.WriteLine(TAG_END_DATE_CREATED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndDateCreated = DateTime.Parse(s);
                }
                catch
                {
                    EndDateCreated = new DateTime();
                }

                Console.WriteLine(TAG_BEGIN_DATE_MODIFIED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginDateModified = DateTime.Parse(s);
                }
                catch
                {
                    BeginDateModified = new DateTime();
                }

                Console.WriteLine(TAG_END_DATE_MODIFIED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndDateModified = DateTime.Parse(s);
                }
                catch
                {
                    EndDateModified = new DateTime();
                }


                Console.WriteLine(TAG_FULLNAME + ":  ");
                Fullname = Console.ReadLine();
                if (Fullname.Length == 0)
                {
                    Fullname = null;
                }

                Console.WriteLine(TAG_FIRSTNAME + ":  ");
                Firstname = Console.ReadLine();
                if (Firstname.Length == 0)
                {
                    Firstname = null;
                }

                Console.WriteLine(TAG_MIDDLENAME + ":  ");
                Middlename = Console.ReadLine();
                if (Middlename.Length == 0)
                {
                    Middlename = null;
                }

                Console.WriteLine(TAG_LASTNAME + ":  ");
                Lastname = Console.ReadLine();
                if (Lastname.Length == 0)
                {
                    Lastname = null;
                }

                Console.WriteLine(TAG_PHONE_NUMBER + ":  ");
                PhoneNumber = Console.ReadLine();
                if (PhoneNumber.Length == 0)
                {
                    PhoneNumber = null;
                }

                Console.WriteLine(TAG_USERNAME + ":  ");
                Username = Console.ReadLine();
                if (Username.Length == 0)
                {
                    Username = null;
                }

                Console.WriteLine(TAG_PASSWD + ":  ");
                Passwd = Console.ReadLine();
                if (Passwd.Length == 0)
                {
                    Passwd = null;
                }

                Console.WriteLine(TAG_SSN + ":  ");
                Ssn = Console.ReadLine();
                if (Ssn.Length == 0)
                {
                    Ssn = null;
                }

                Console.WriteLine(TAG_PICTURE_URL + ":  ");
                PictureUrl = Console.ReadLine();
                if (PictureUrl.Length == 0)
                {
                    PictureUrl = null;
                }
                // Cannot reliably convert a byte[] to string.
                Console.WriteLine(TAG_IS_DISABLED + ":  ");
                try
                {
                    IsDisabled = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    IsDisabled = false;
                }

                Console.WriteLine(TAG_WELCOME_EMAIL_SENT + ":  ");
                try
                {
                    WelcomeEmailSent = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    WelcomeEmailSent = false;
                }


                Console.WriteLine(TAG_VALIDATIONTOKEN + ":  ");
                Validationtoken = Console.ReadLine();
                if (Validationtoken.Length == 0)
                {
                    Validationtoken = null;
                }

                Console.WriteLine(TAG_VALIDATIONLINK + ":  ");
                Validationlink = Console.ReadLine();
                if (Validationlink.Length == 0)
                {
                    Validationlink = null;
                }
                Console.WriteLine(TAG_ISVALIDATED + ":  ");
                try
                {
                    Isvalidated = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    Isvalidated = false;
                }

                Console.WriteLine(TAG_BEGIN_WELCOME_EMAIL_SENT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginWelcomeEmailSentDate = DateTime.Parse(s);
                }
                catch
                {
                    BeginWelcomeEmailSentDate = new DateTime();
                }

                Console.WriteLine(TAG_END_WELCOME_EMAIL_SENT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndWelcomeEmailSentDate = DateTime.Parse(s);
                }
                catch
                {
                    EndWelcomeEmailSentDate = new DateTime();
                }

                Console.WriteLine(TAG_BEGIN_LAST_LOGIN_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginLastLoginDate = DateTime.Parse(s);
                }
                catch
                {
                    BeginLastLoginDate = new DateTime();
                }

                Console.WriteLine(TAG_END_LAST_LOGIN_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndLastLoginDate = DateTime.Parse(s);
                }
                catch
                {
                    EndLastLoginDate = new DateTime();
                }


                Console.WriteLine(TAG_INTERNAL_NOTES + ":  ");
                InternalNotes = Console.ReadLine();
                if (InternalNotes.Length == 0)
                {
                    InternalNotes = null;
                }

                Console.WriteLine(TAG_USER_MESSAGE + ":  ");
                UserMessage = Console.ReadLine();
                if (UserMessage.Length == 0)
                {
                    UserMessage = null;
                }

                Console.WriteLine(TAG_COOKIE_ID + ":  ");
                CookieID = Console.ReadLine();
                if (CookieID.Length == 0)
                {
                    CookieID = null;
                }
                Console.WriteLine(TAG_HAS_CURRENT_RATING + ":  ");
                try
                {
                    HasCurrentRating = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    HasCurrentRating = false;
                }

                Console.WriteLine(TAG_CURRENT_RATING + ":  ");
                try
                {
                    CurrentRating = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    CurrentRating = 0;
                }

                Console.WriteLine(TAG_INTERNAL_CALCULATED_RATING + ":  ");
                try
                {
                    InternalCalculatedRating = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    InternalCalculatedRating = 0;
                }


                Console.WriteLine(TAG_SECURITY_QUESTION + ":  ");
                SecurityQuestion = Console.ReadLine();
                if (SecurityQuestion.Length == 0)
                {
                    SecurityQuestion = null;
                }

                Console.WriteLine(TAG_SECURITY_ANSWER + ":  ");
                SecurityAnswer = Console.ReadLine();
                if (SecurityAnswer.Length == 0)
                {
                    SecurityAnswer = null;
                }
                Console.WriteLine(TAG_NUMBER_OF_VISITS + ":  ");
                try
                {
                    NumberOfVisits = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    NumberOfVisits = 0;
                }

                Console.WriteLine(TAG_BEGIN_PREVIOUS_VISIT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginPreviousVisitDate = DateTime.Parse(s);
                }
                catch
                {
                    BeginPreviousVisitDate = new DateTime();
                }

                Console.WriteLine(TAG_END_PREVIOUS_VISIT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndPreviousVisitDate = DateTime.Parse(s);
                }
                catch
                {
                    EndPreviousVisitDate = new DateTime();
                }

                Console.WriteLine(TAG_BEGIN_LAST_VISIT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginLastVisitDate = DateTime.Parse(s);
                }
                catch
                {
                    BeginLastVisitDate = new DateTime();
                }

                Console.WriteLine(TAG_END_LAST_VISIT_DATE + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndLastVisitDate = DateTime.Parse(s);
                }
                catch
                {
                    EndLastVisitDate = new DateTime();
                }

                Console.WriteLine(TAG_IS_RATING_PROFILE_FINISHED + ":  ");
                try
                {
                    IsRatingProfileFinished = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    IsRatingProfileFinished = false;
                }

                Console.WriteLine(TAG_USER_SOURCE_ID + ":  ");
                try
                {
                    UserSourceID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UserSourceID = 0;
                }


            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
        }

        /// <summary>
        ///     Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }
        /// <summary>
        ///		Free the instance variables of this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return; // we're being collected, so let the GC take care of this object
        }
        private void _setupCountParams()
        {
            SqlParameter paramCount = null;
            paramCount = new SqlParameter();
            paramCount.ParameterName = PARAM_COUNT;
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            _cmd.Parameters.Add(paramCount);
        }
        private void _setupEnumParams()
        {
            System.Text.StringBuilder sbLog = null;
            SqlParameter paramUserID = null;
            SqlParameter paramUserRoleID = null;
            SqlParameter paramBeginDateCreated = null;
            SqlParameter paramEndDateCreated = null;
            SqlParameter paramBeginDateModified = null;
            SqlParameter paramEndDateModified = null;
            SqlParameter paramFullname = null;
            SqlParameter paramFirstname = null;
            SqlParameter paramMiddlename = null;
            SqlParameter paramLastname = null;
            SqlParameter paramPhoneNumber = null;
            SqlParameter paramUsername = null;
            SqlParameter paramPasswd = null;
            SqlParameter paramSsn = null;
            SqlParameter paramPictureUrl = null;
            SqlParameter paramPicture = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramWelcomeEmailSent = null;
            SqlParameter paramValidationtoken = null;
            SqlParameter paramValidationlink = null;
            SqlParameter paramIsvalidated = null;
            SqlParameter paramBeginWelcomeEmailSentDate = null;
            SqlParameter paramEndWelcomeEmailSentDate = null;
            SqlParameter paramBeginLastLoginDate = null;
            SqlParameter paramEndLastLoginDate = null;
            SqlParameter paramInternalNotes = null;
            SqlParameter paramUserMessage = null;
            SqlParameter paramCookieID = null;
            SqlParameter paramHasCurrentRating = null;
            SqlParameter paramCurrentRating = null;
            SqlParameter paramInternalCalculatedRating = null;
            SqlParameter paramSecurityQuestion = null;
            SqlParameter paramSecurityAnswer = null;
            SqlParameter paramNumberOfVisits = null;
            SqlParameter paramBeginPreviousVisitDate = null;
            SqlParameter paramEndPreviousVisitDate = null;
            SqlParameter paramBeginLastVisitDate = null;
            SqlParameter paramEndLastVisitDate = null;
            SqlParameter paramIsRatingProfileFinished = null;
            SqlParameter paramUserSourceID = null;
            DateTime dtNull = new DateTime();

            sbLog = new System.Text.StringBuilder();
            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            sbLog.Append(TAG_USER_ID + "=" + UserID + "\n");
            paramUserID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserID);

            paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
            sbLog.Append(TAG_USER_ROLE_ID + "=" + UserRoleID + "\n");
            paramUserRoleID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserRoleID);
            // Setup the date created param
            if (!dtNull.Equals(BeginDateCreated))
            {
                paramBeginDateCreated = new SqlParameter("@" + TAG_BEGIN_DATE_CREATED, BeginDateCreated);
            }
            else
            {
                paramBeginDateCreated = new SqlParameter("@" + TAG_BEGIN_DATE_CREATED, DBNull.Value);
            }
            paramBeginDateCreated.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginDateCreated);

            if (!dtNull.Equals(EndDateCreated))
            {
                paramEndDateCreated = new SqlParameter("@" + TAG_END_DATE_CREATED, EndDateCreated);
            }
            else
            {
                paramEndDateCreated = new SqlParameter("@" + TAG_END_DATE_CREATED, DBNull.Value);
            }
            paramEndDateCreated.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndDateCreated);

            // Setup the date modified param
            if (!dtNull.Equals(BeginDateModified))
            {
                paramBeginDateModified = new SqlParameter("@" + TAG_BEGIN_DATE_MODIFIED, BeginDateModified);
            }
            else
            {
                paramBeginDateModified = new SqlParameter("@" + TAG_BEGIN_DATE_MODIFIED, DBNull.Value);
            }
            paramBeginDateModified.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginDateModified);

            if (!dtNull.Equals(EndDateModified))
            {
                paramEndDateModified = new SqlParameter("@" + TAG_END_DATE_MODIFIED, EndDateModified);
            }
            else
            {
                paramEndDateModified = new SqlParameter("@" + TAG_END_DATE_MODIFIED, DBNull.Value);
            }
            paramEndDateModified.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndDateModified);

            // Setup the fullname text param
            if (Fullname != null)
            {
                paramFullname = new SqlParameter("@" + TAG_FULLNAME, Fullname);
                sbLog.Append(TAG_FULLNAME + "=" + Fullname + "\n");
            }
            else
            {
                paramFullname = new SqlParameter("@" + TAG_FULLNAME, DBNull.Value);
            }
            paramFullname.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramFullname);

            // Setup the firstname text param
            if (Firstname != null)
            {
                paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, Firstname);
                sbLog.Append(TAG_FIRSTNAME + "=" + Firstname + "\n");
            }
            else
            {
                paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, DBNull.Value);
            }
            paramFirstname.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramFirstname);

            // Setup the middlename text param
            if (Middlename != null)
            {
                paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, Middlename);
                sbLog.Append(TAG_MIDDLENAME + "=" + Middlename + "\n");
            }
            else
            {
                paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, DBNull.Value);
            }
            paramMiddlename.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramMiddlename);

            // Setup the lastname text param
            if (Lastname != null)
            {
                paramLastname = new SqlParameter("@" + TAG_LASTNAME, Lastname);
                sbLog.Append(TAG_LASTNAME + "=" + Lastname + "\n");
            }
            else
            {
                paramLastname = new SqlParameter("@" + TAG_LASTNAME, DBNull.Value);
            }
            paramLastname.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramLastname);

            // Setup the phone number text param
            if (PhoneNumber != null)
            {
                paramPhoneNumber = new SqlParameter("@" + TAG_PHONE_NUMBER, PhoneNumber);
                sbLog.Append(TAG_PHONE_NUMBER + "=" + PhoneNumber + "\n");
            }
            else
            {
                paramPhoneNumber = new SqlParameter("@" + TAG_PHONE_NUMBER, DBNull.Value);
            }
            paramPhoneNumber.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramPhoneNumber);

            // Setup the username text param
            if (Username != null)
            {
                paramUsername = new SqlParameter("@" + TAG_USERNAME, Username);
                sbLog.Append(TAG_USERNAME + "=" + Username + "\n");
            }
            else
            {
                paramUsername = new SqlParameter("@" + TAG_USERNAME, DBNull.Value);
            }
            paramUsername.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUsername);

            // Setup the passwd text param
            if (Passwd != null)
            {
                paramPasswd = new SqlParameter("@" + TAG_PASSWD, Passwd);
                sbLog.Append(TAG_PASSWD + "=" + Passwd + "\n");
            }
            else
            {
                paramPasswd = new SqlParameter("@" + TAG_PASSWD, DBNull.Value);
            }
            paramPasswd.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramPasswd);

            // Setup the ssn text param
            if (Ssn != null)
            {
                paramSsn = new SqlParameter("@" + TAG_SSN, Ssn);
                sbLog.Append(TAG_SSN + "=" + Ssn + "\n");
            }
            else
            {
                paramSsn = new SqlParameter("@" + TAG_SSN, DBNull.Value);
            }
            paramSsn.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramSsn);

            // Setup the picture url text param
            if (PictureUrl != null)
            {
                paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, PictureUrl);
                sbLog.Append(TAG_PICTURE_URL + "=" + PictureUrl + "\n");
            }
            else
            {
                paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, DBNull.Value);
            }
            paramPictureUrl.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramPictureUrl);

            // Setup the picture text param
            //if ( Picture != null )
            //{
            //	paramPicture = new SqlParameter("@" + TAG_PICTURE, Picture);
            //	sbLog.Append(TAG_PICTURE + "=" + Picture + "\n");
            //}
            //else
            //{
            //	paramPicture = new SqlParameter("@" + TAG_PICTURE, DBNull.Value);
            //}
            //paramPicture.Direction = ParameterDirection.Input;
            //_cmd.Parameters.Add(paramPicture);

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            sbLog.Append(TAG_IS_DISABLED + "=" + IsDisabled + "\n");
            paramIsDisabled.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramIsDisabled);
            paramWelcomeEmailSent = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT, WelcomeEmailSent);
            sbLog.Append(TAG_WELCOME_EMAIL_SENT + "=" + WelcomeEmailSent + "\n");
            paramWelcomeEmailSent.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramWelcomeEmailSent);
            // Setup the validationtoken text param
            if (Validationtoken != null)
            {
                paramValidationtoken = new SqlParameter("@" + TAG_VALIDATIONTOKEN, Validationtoken);
                sbLog.Append(TAG_VALIDATIONTOKEN + "=" + Validationtoken + "\n");
            }
            else
            {
                paramValidationtoken = new SqlParameter("@" + TAG_VALIDATIONTOKEN, DBNull.Value);
            }
            paramValidationtoken.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramValidationtoken);

            // Setup the validationlink text param
            if (Validationlink != null)
            {
                paramValidationlink = new SqlParameter("@" + TAG_VALIDATIONLINK, Validationlink);
                sbLog.Append(TAG_VALIDATIONLINK + "=" + Validationlink + "\n");
            }
            else
            {
                paramValidationlink = new SqlParameter("@" + TAG_VALIDATIONLINK, DBNull.Value);
            }
            paramValidationlink.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramValidationlink);

            paramIsvalidated = new SqlParameter("@" + TAG_ISVALIDATED, Isvalidated);
            sbLog.Append(TAG_ISVALIDATED + "=" + Isvalidated + "\n");
            paramIsvalidated.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramIsvalidated);
            // Setup the welcome email sent date param
            if (!dtNull.Equals(BeginWelcomeEmailSentDate))
            {
                paramBeginWelcomeEmailSentDate = new SqlParameter("@" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE, BeginWelcomeEmailSentDate);
            }
            else
            {
                paramBeginWelcomeEmailSentDate = new SqlParameter("@" + TAG_BEGIN_WELCOME_EMAIL_SENT_DATE, DBNull.Value);
            }
            paramBeginWelcomeEmailSentDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginWelcomeEmailSentDate);

            if (!dtNull.Equals(EndWelcomeEmailSentDate))
            {
                paramEndWelcomeEmailSentDate = new SqlParameter("@" + TAG_END_WELCOME_EMAIL_SENT_DATE, EndWelcomeEmailSentDate);
            }
            else
            {
                paramEndWelcomeEmailSentDate = new SqlParameter("@" + TAG_END_WELCOME_EMAIL_SENT_DATE, DBNull.Value);
            }
            paramEndWelcomeEmailSentDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndWelcomeEmailSentDate);

            // Setup the last login date param
            if (!dtNull.Equals(BeginLastLoginDate))
            {
                paramBeginLastLoginDate = new SqlParameter("@" + TAG_BEGIN_LAST_LOGIN_DATE, BeginLastLoginDate);
            }
            else
            {
                paramBeginLastLoginDate = new SqlParameter("@" + TAG_BEGIN_LAST_LOGIN_DATE, DBNull.Value);
            }
            paramBeginLastLoginDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginLastLoginDate);

            if (!dtNull.Equals(EndLastLoginDate))
            {
                paramEndLastLoginDate = new SqlParameter("@" + TAG_END_LAST_LOGIN_DATE, EndLastLoginDate);
            }
            else
            {
                paramEndLastLoginDate = new SqlParameter("@" + TAG_END_LAST_LOGIN_DATE, DBNull.Value);
            }
            paramEndLastLoginDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndLastLoginDate);

            // Setup the internal notes text param
            if (InternalNotes != null)
            {
                paramInternalNotes = new SqlParameter("@" + TAG_INTERNAL_NOTES, InternalNotes);
                sbLog.Append(TAG_INTERNAL_NOTES + "=" + InternalNotes + "\n");
            }
            else
            {
                paramInternalNotes = new SqlParameter("@" + TAG_INTERNAL_NOTES, DBNull.Value);
            }
            paramInternalNotes.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramInternalNotes);

            // Setup the user message text param
            if (UserMessage != null)
            {
                paramUserMessage = new SqlParameter("@" + TAG_USER_MESSAGE, UserMessage);
                sbLog.Append(TAG_USER_MESSAGE + "=" + UserMessage + "\n");
            }
            else
            {
                paramUserMessage = new SqlParameter("@" + TAG_USER_MESSAGE, DBNull.Value);
            }
            paramUserMessage.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserMessage);

            // Setup the cookie id text param
            if (CookieID != null)
            {
                paramCookieID = new SqlParameter("@" + TAG_COOKIE_ID, CookieID);
                sbLog.Append(TAG_COOKIE_ID + "=" + CookieID + "\n");
            }
            else
            {
                paramCookieID = new SqlParameter("@" + TAG_COOKIE_ID, DBNull.Value);
            }
            paramCookieID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramCookieID);

            paramHasCurrentRating = new SqlParameter("@" + TAG_HAS_CURRENT_RATING, HasCurrentRating);
            sbLog.Append(TAG_HAS_CURRENT_RATING + "=" + HasCurrentRating + "\n");
            paramHasCurrentRating.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramHasCurrentRating);
            paramCurrentRating = new SqlParameter("@" + TAG_CURRENT_RATING, CurrentRating);
            sbLog.Append(TAG_CURRENT_RATING + "=" + CurrentRating + "\n");
            paramCurrentRating.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramCurrentRating);

            paramInternalCalculatedRating = new SqlParameter("@" + TAG_INTERNAL_CALCULATED_RATING, InternalCalculatedRating);
            sbLog.Append(TAG_INTERNAL_CALCULATED_RATING + "=" + InternalCalculatedRating + "\n");
            paramInternalCalculatedRating.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramInternalCalculatedRating);

            // Setup the security question text param
            if (SecurityQuestion != null)
            {
                paramSecurityQuestion = new SqlParameter("@" + TAG_SECURITY_QUESTION, SecurityQuestion);
                sbLog.Append(TAG_SECURITY_QUESTION + "=" + SecurityQuestion + "\n");
            }
            else
            {
                paramSecurityQuestion = new SqlParameter("@" + TAG_SECURITY_QUESTION, DBNull.Value);
            }
            paramSecurityQuestion.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramSecurityQuestion);

            // Setup the security answer text param
            if (SecurityAnswer != null)
            {
                paramSecurityAnswer = new SqlParameter("@" + TAG_SECURITY_ANSWER, SecurityAnswer);
                sbLog.Append(TAG_SECURITY_ANSWER + "=" + SecurityAnswer + "\n");
            }
            else
            {
                paramSecurityAnswer = new SqlParameter("@" + TAG_SECURITY_ANSWER, DBNull.Value);
            }
            paramSecurityAnswer.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramSecurityAnswer);

            paramNumberOfVisits = new SqlParameter("@" + TAG_NUMBER_OF_VISITS, NumberOfVisits);
            sbLog.Append(TAG_NUMBER_OF_VISITS + "=" + NumberOfVisits + "\n");
            paramNumberOfVisits.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramNumberOfVisits);

            // Setup the previous visit date param
            if (!dtNull.Equals(BeginPreviousVisitDate))
            {
                paramBeginPreviousVisitDate = new SqlParameter("@" + TAG_BEGIN_PREVIOUS_VISIT_DATE, BeginPreviousVisitDate);
            }
            else
            {
                paramBeginPreviousVisitDate = new SqlParameter("@" + TAG_BEGIN_PREVIOUS_VISIT_DATE, DBNull.Value);
            }
            paramBeginPreviousVisitDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginPreviousVisitDate);

            if (!dtNull.Equals(EndPreviousVisitDate))
            {
                paramEndPreviousVisitDate = new SqlParameter("@" + TAG_END_PREVIOUS_VISIT_DATE, EndPreviousVisitDate);
            }
            else
            {
                paramEndPreviousVisitDate = new SqlParameter("@" + TAG_END_PREVIOUS_VISIT_DATE, DBNull.Value);
            }
            paramEndPreviousVisitDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndPreviousVisitDate);

            // Setup the last visit date param
            if (!dtNull.Equals(BeginLastVisitDate))
            {
                paramBeginLastVisitDate = new SqlParameter("@" + TAG_BEGIN_LAST_VISIT_DATE, BeginLastVisitDate);
            }
            else
            {
                paramBeginLastVisitDate = new SqlParameter("@" + TAG_BEGIN_LAST_VISIT_DATE, DBNull.Value);
            }
            paramBeginLastVisitDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginLastVisitDate);

            if (!dtNull.Equals(EndLastVisitDate))
            {
                paramEndLastVisitDate = new SqlParameter("@" + TAG_END_LAST_VISIT_DATE, EndLastVisitDate);
            }
            else
            {
                paramEndLastVisitDate = new SqlParameter("@" + TAG_END_LAST_VISIT_DATE, DBNull.Value);
            }
            paramEndLastVisitDate.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndLastVisitDate);

            paramIsRatingProfileFinished = new SqlParameter("@" + TAG_IS_RATING_PROFILE_FINISHED, IsRatingProfileFinished);
            sbLog.Append(TAG_IS_RATING_PROFILE_FINISHED + "=" + IsRatingProfileFinished + "\n");
            paramIsRatingProfileFinished.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramIsRatingProfileFinished);
            paramUserSourceID = new SqlParameter("@" + TAG_USER_SOURCE_ID, UserSourceID);
            sbLog.Append(TAG_USER_SOURCE_ID + "=" + UserSourceID + "\n");
            paramUserSourceID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserSourceID);
        }

    }
}
