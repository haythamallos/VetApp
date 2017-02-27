using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Vetapp.Engine.Common;

namespace Vetapp.Engine.DataAccessLayer.Data
{
    /// <summary>
    /// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  User.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	2/27/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the User database table.
    /// </summary>
    public class User
    {
        //Attributes
        /// <summary>UserID Attribute type String</summary>
        private long _lUserID = 0;
        /// <summary>UserRoleID Attribute type String</summary>
        private long _lUserRoleID = 0;
        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;
        /// <summary>DateModified Attribute type String</summary>
        private DateTime _dtDateModified = dtNull;
        /// <summary>Firstname Attribute type String</summary>
        private string _strFirstname = null;
        /// <summary>Middlename Attribute type String</summary>
        private string _strMiddlename = null;
        /// <summary>Lastname Attribute type String</summary>
        private string _strLastname = null;
        /// <summary>PhoneNumber Attribute type String</summary>
        private string _strPhoneNumber = null;
        /// <summary>Username Attribute type String</summary>
        private string _strUsername = null;
        /// <summary>Passwd Attribute type String</summary>
        private string _strPasswd = null;
        /// <summary>Ssn Attribute type String</summary>
        private string _strSsn = null;
        /// <summary>PictureUrl Attribute type String</summary>
        private string _strPictureUrl = null;
        /// <summary>Picture Attribute type String</summary>
        private byte[] _bytePicture = null;
        /// <summary>IsDisabled Attribute type String</summary>
        private bool? _bIsDisabled = null;
        /// <summary>WelcomeEmailSent Attribute type String</summary>
        private bool? _bWelcomeEmailSent = null;
        /// <summary>Validationtoken Attribute type String</summary>
        private string _strValidationtoken = null;
        /// <summary>Validationlink Attribute type String</summary>
        private string _strValidationlink = null;
        /// <summary>Isvalidated Attribute type String</summary>
        private bool? _bIsvalidated = null;
        /// <summary>WelcomeEmailSentDate Attribute type String</summary>
        private DateTime _dtWelcomeEmailSentDate = dtNull;
        /// <summary>LastLoginDate Attribute type String</summary>
        private DateTime _dtLastLoginDate = dtNull;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class User and is of type bool</summary>
        public static readonly string ENTITY_NAME = "User"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "user_id"; //Table id field name
                                                               /// <summary>user_role_id Database field </summary>
        public static readonly string DB_FIELD_USER_ROLE_ID = "user_role_id"; //Table UserRoleID field name
                                                                              /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
                                                                              /// <summary>date_modified Database field </summary>
        public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
                                                                                /// <summary>firstname Database field </summary>
        public static readonly string DB_FIELD_FIRSTNAME = "firstname"; //Table Firstname field name
                                                                        /// <summary>middlename Database field </summary>
        public static readonly string DB_FIELD_MIDDLENAME = "middlename"; //Table Middlename field name
                                                                          /// <summary>lastname Database field </summary>
        public static readonly string DB_FIELD_LASTNAME = "lastname"; //Table Lastname field name
                                                                      /// <summary>phone_number Database field </summary>
        public static readonly string DB_FIELD_PHONE_NUMBER = "phone_number"; //Table PhoneNumber field name
                                                                              /// <summary>username Database field </summary>
        public static readonly string DB_FIELD_USERNAME = "username"; //Table Username field name
                                                                      /// <summary>passwd Database field </summary>
        public static readonly string DB_FIELD_PASSWD = "passwd"; //Table Passwd field name
                                                                  /// <summary>ssn Database field </summary>
        public static readonly string DB_FIELD_SSN = "ssn"; //Table Ssn field name
                                                            /// <summary>picture_url Database field </summary>
        public static readonly string DB_FIELD_PICTURE_URL = "picture_url"; //Table PictureUrl field name
                                                                            /// <summary>picture Database field </summary>
        public static readonly string DB_FIELD_PICTURE = "picture"; //Table Picture field name
                                                                    /// <summary>is_disabled Database field </summary>
        public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name
                                                                            /// <summary>welcome_email_sent Database field </summary>
        public static readonly string DB_FIELD_WELCOME_EMAIL_SENT = "welcome_email_sent"; //Table WelcomeEmailSent field name
                                                                                          /// <summary>validationtoken Database field </summary>
        public static readonly string DB_FIELD_VALIDATIONTOKEN = "validationtoken"; //Table Validationtoken field name
                                                                                    /// <summary>validationlink Database field </summary>
        public static readonly string DB_FIELD_VALIDATIONLINK = "validationlink"; //Table Validationlink field name
                                                                                  /// <summary>isvalidated Database field </summary>
        public static readonly string DB_FIELD_ISVALIDATED = "isvalidated"; //Table Isvalidated field name
                                                                            /// <summary>welcome_email_sent_date Database field </summary>
        public static readonly string DB_FIELD_WELCOME_EMAIL_SENT_DATE = "welcome_email_sent_date"; //Table WelcomeEmailSentDate field name
                                                                                                    /// <summary>last_login_date Database field </summary>
        public static readonly string DB_FIELD_LAST_LOGIN_DATE = "last_login_date"; //Table LastLoginDate field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "UserID"; //Attribute id  name
                                                         /// <summary>UserRoleID Attribute type string</summary>
        public static readonly string TAG_USER_ROLE_ID = "UserRoleID"; //Table UserRoleID field name
                                                                       /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
                                                                        /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
                                                                          /// <summary>Firstname Attribute type string</summary>
        public static readonly string TAG_FIRSTNAME = "Firstname"; //Table Firstname field name
                                                                   /// <summary>Middlename Attribute type string</summary>
        public static readonly string TAG_MIDDLENAME = "Middlename"; //Table Middlename field name
                                                                     /// <summary>Lastname Attribute type string</summary>
        public static readonly string TAG_LASTNAME = "Lastname"; //Table Lastname field name
                                                                 /// <summary>PhoneNumber Attribute type string</summary>
        public static readonly string TAG_PHONE_NUMBER = "PhoneNumber"; //Table PhoneNumber field name
                                                                        /// <summary>Username Attribute type string</summary>
        public static readonly string TAG_USERNAME = "Username"; //Table Username field name
                                                                 /// <summary>Passwd Attribute type string</summary>
        public static readonly string TAG_PASSWD = "Passwd"; //Table Passwd field name
                                                             /// <summary>Ssn Attribute type string</summary>
        public static readonly string TAG_SSN = "Ssn"; //Table Ssn field name
                                                       /// <summary>PictureUrl Attribute type string</summary>
        public static readonly string TAG_PICTURE_URL = "PictureUrl"; //Table PictureUrl field name
                                                                      /// <summary>Picture Attribute type string</summary>
        public static readonly string TAG_PICTURE = "Picture"; //Table Picture field name
                                                               /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name
                                                                      /// <summary>WelcomeEmailSent Attribute type string</summary>
        public static readonly string TAG_WELCOME_EMAIL_SENT = "WelcomeEmailSent"; //Table WelcomeEmailSent field name
                                                                                   /// <summary>Validationtoken Attribute type string</summary>
        public static readonly string TAG_VALIDATIONTOKEN = "Validationtoken"; //Table Validationtoken field name
                                                                               /// <summary>Validationlink Attribute type string</summary>
        public static readonly string TAG_VALIDATIONLINK = "Validationlink"; //Table Validationlink field name
                                                                             /// <summary>Isvalidated Attribute type string</summary>
        public static readonly string TAG_ISVALIDATED = "Isvalidated"; //Table Isvalidated field name
                                                                       /// <summary>WelcomeEmailSentDate Attribute type string</summary>
        public static readonly string TAG_WELCOME_EMAIL_SENT_DATE = "WelcomeEmailSentDate"; //Table WelcomeEmailSentDate field name
                                                                                            /// <summary>LastLoginDate Attribute type string</summary>
        public static readonly string TAG_LAST_LOGIN_DATE = "LastLoginDate"; //Table LastLoginDate field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spUserInsert"; //Insert sp name
        private static readonly string SP_UPDATE_NAME = "spUserUpdate"; //Update sp name
        private static readonly string SP_DELETE_NAME = "spUserDelete"; //Delete sp name
        private static readonly string SP_LOAD_NAME = "spUserLoad"; //Load sp name
        private static readonly string SP_EXIST_NAME = "spUserExist"; //Exist sp name

        //properties
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
        /// <summary>DateCreated is a Property in the User Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }
        /// <summary>DateModified is a Property in the User Class of type DateTime</summary>
        public DateTime DateModified
        {
            get { return _dtDateModified; }
            set { _dtDateModified = value; }
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
        /// <summary>WelcomeEmailSentDate is a Property in the User Class of type DateTime</summary>
        public DateTime WelcomeEmailSentDate
        {
            get { return _dtWelcomeEmailSentDate; }
            set { _dtWelcomeEmailSentDate = value; }
        }
        /// <summary>LastLoginDate is a Property in the User Class of type DateTime</summary>
        public DateTime LastLoginDate
        {
            get { return _dtLastLoginDate; }
            set { _dtLastLoginDate = value; }
        }


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>HasError Property in class User and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Error Property in class User and is of type ErrorCode</summary>
        public ErrorCode Error
        {
            get { return _errorCode; }
        }

        //Constructors
        /// <summary>User empty constructor</summary>
        public User()
        {
        }
        /// <summary>User constructor takes UserID and a SqlConnection</summary>
        public User(long l, SqlConnection conn)
        {
            UserID = l;
            try
            {
                sqlLoad(conn);
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }

        }
        /// <summary>User Constructor takes pStrData and Config</summary>
        public User(string pStrData)
        {
            Parse(pStrData);
        }
        /// <summary>User Constructor takes SqlDataReader</summary>
        public User(SqlDataReader rd)
        {
            sqlParseResultSet(rd);
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

        // public methods
        /// <summary>ToString is overridden to display all properties of the User Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + UserID.ToString() + "\n");
            sbReturn.Append(TAG_USER_ROLE_ID + ":  " + UserRoleID + "\n");
            if (!dtNull.Equals(DateCreated))
            {
                sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_CREATED + ":\n");
            }
            if (!dtNull.Equals(DateModified))
            {
                sbReturn.Append(TAG_DATE_MODIFIED + ":  " + DateModified.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_MODIFIED + ":\n");
            }
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
            if (!dtNull.Equals(WelcomeEmailSentDate))
            {
                sbReturn.Append(TAG_WELCOME_EMAIL_SENT_DATE + ":  " + WelcomeEmailSentDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_WELCOME_EMAIL_SENT_DATE + ":\n");
            }
            if (!dtNull.Equals(LastLoginDate))
            {
                sbReturn.Append(TAG_LAST_LOGIN_DATE + ":  " + LastLoginDate.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_LAST_LOGIN_DATE + ":\n");
            }

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of User</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<User>\n");
            sbReturn.Append("<" + TAG_ID + ">" + UserID + "</" + TAG_ID + ">\n");
            sbReturn.Append("<" + TAG_USER_ROLE_ID + ">" + UserRoleID + "</" + TAG_USER_ROLE_ID + ">\n");
            if (!dtNull.Equals(DateCreated))
            {
                sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
            }
            if (!dtNull.Equals(DateModified))
            {
                sbReturn.Append("<" + TAG_DATE_MODIFIED + ">" + DateModified.ToString() + "</" + TAG_DATE_MODIFIED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_DATE_MODIFIED + "></" + TAG_DATE_MODIFIED + ">\n");
            }
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
            if (!dtNull.Equals(WelcomeEmailSentDate))
            {
                sbReturn.Append("<" + TAG_WELCOME_EMAIL_SENT_DATE + ">" + WelcomeEmailSentDate.ToString() + "</" + TAG_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_WELCOME_EMAIL_SENT_DATE + "></" + TAG_WELCOME_EMAIL_SENT_DATE + ">\n");
            }
            if (!dtNull.Equals(LastLoginDate))
            {
                sbReturn.Append("<" + TAG_LAST_LOGIN_DATE + ">" + LastLoginDate.ToString() + "</" + TAG_LAST_LOGIN_DATE + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_LAST_LOGIN_DATE + "></" + TAG_LAST_LOGIN_DATE + ">\n");
            }
            sbReturn.Append("</User>" + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Parse accepts a string in XML format and parses values</summary>
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
                foreach (XmlNode xNode in xNodes)
                {
                    Parse(xNode);
                }
            }
            catch (Exception e)
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
                xResultNode = xNode.SelectSingleNode(TAG_ID);
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
                xResultNode = xNode.SelectSingleNode(TAG_DATE_CREATED);
                DateCreated = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_DATE_MODIFIED);
                DateModified = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_FIRSTNAME);
                Firstname = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_MIDDLENAME);
                Middlename = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_LASTNAME);
                Lastname = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PHONE_NUMBER);
                PhoneNumber = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USERNAME);
                Username = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PASSWD);
                Passwd = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_SSN);
                Ssn = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PICTURE_URL);
                PictureUrl = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }
            //Cannot reliably convert byte[] to string.

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
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_VALIDATIONLINK);
                Validationlink = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
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
                xResultNode = xNode.SelectSingleNode(TAG_WELCOME_EMAIL_SENT_DATE);
                WelcomeEmailSentDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_LAST_LOGIN_DATE);
                LastLoginDate = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }
        }
        /// <summary>Calls sqlLoad() method which gets record from database with user_id equal to the current object's UserID </summary>
        public void Load(SqlConnection conn)
        {
            try
            {
                sqlLoad(conn);
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }

        }
        /// <summary>Calls sqlUpdate() method which record record from database with current object values where user_id equal to the current object's UserID </summary>
        public void Update(SqlConnection conn)
        {
            bool bExist = false;
            try
            {
                bExist = Exist(conn);
                if (bExist)
                {
                    sqlUpdate(conn);
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
        }
        /// <summary>Calls sqlInsert() method which inserts a record into the database with current object values</summary>
        public void Save(SqlConnection conn)
        {
            try
            {
                bool bExist = false;

                bExist = Exist(conn);
                if (!bExist)
                {
                    sqlInsert(conn);
                }
                else
                {
                    sqlUpdate(conn);
                }
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }

        }
        /// <summary>Calls sqlDelete() method which delete's the record from database where where user_id equal to the current object's UserID </summary>
        public void Delete(SqlConnection conn)
        {
            try
            {
                sqlDelete(conn);
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
        }
        /// <summary>Calls sqlExists() returns true if the record exists, false if not </summary>
        public bool Exist(SqlConnection conn)
        {
            bool bReturn = false;
            try
            {
                bReturn = sqlExist(conn);
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }

            return bReturn;
        }
        /// <summary>Prompt user to enter Property values</summary>
        public void Prompt()
        {
            try
            {

                Console.WriteLine(User.TAG_USER_ROLE_ID + ":  ");
                UserRoleID = (long)Convert.ToInt32(Console.ReadLine());
                try
                {
                    Console.WriteLine(User.TAG_DATE_CREATED + ":  ");
                    DateCreated = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateCreated = new DateTime();
                }
                try
                {
                    Console.WriteLine(User.TAG_DATE_MODIFIED + ":  ");
                    DateModified = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateModified = new DateTime();
                }

                Console.WriteLine(User.TAG_FIRSTNAME + ":  ");
                Firstname = Console.ReadLine();

                Console.WriteLine(User.TAG_MIDDLENAME + ":  ");
                Middlename = Console.ReadLine();

                Console.WriteLine(User.TAG_LASTNAME + ":  ");
                Lastname = Console.ReadLine();

                Console.WriteLine(User.TAG_PHONE_NUMBER + ":  ");
                PhoneNumber = Console.ReadLine();

                Console.WriteLine(User.TAG_USERNAME + ":  ");
                Username = Console.ReadLine();

                Console.WriteLine(User.TAG_PASSWD + ":  ");
                Passwd = Console.ReadLine();

                Console.WriteLine(User.TAG_SSN + ":  ");
                Ssn = Console.ReadLine();

                Console.WriteLine(User.TAG_PICTURE_URL + ":  ");
                PictureUrl = Console.ReadLine();
                //Cannot reliably convert byte[] to string.

                Console.WriteLine(User.TAG_IS_DISABLED + ":  ");
                IsDisabled = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine(User.TAG_WELCOME_EMAIL_SENT + ":  ");
                WelcomeEmailSent = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine(User.TAG_VALIDATIONTOKEN + ":  ");
                Validationtoken = Console.ReadLine();

                Console.WriteLine(User.TAG_VALIDATIONLINK + ":  ");
                Validationlink = Console.ReadLine();

                Console.WriteLine(User.TAG_ISVALIDATED + ":  ");
                Isvalidated = Convert.ToBoolean(Console.ReadLine());
                try
                {
                    Console.WriteLine(User.TAG_WELCOME_EMAIL_SENT_DATE + ":  ");
                    WelcomeEmailSentDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    WelcomeEmailSentDate = new DateTime();
                }
                try
                {
                    Console.WriteLine(User.TAG_LAST_LOGIN_DATE + ":  ");
                    LastLoginDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    LastLoginDate = new DateTime();
                }

            }
            catch (Exception e)
            {
                _hasError = true;
                _errorCode = new ErrorCode();
            }
        }

        //protected
        /// <summary>Inserts row of data into the database</summary>
        protected void sqlInsert(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramUserRoleID = null;
            SqlParameter paramDateCreated = null;
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
            SqlParameter paramWelcomeEmailSentDate = null;
            SqlParameter paramLastLoginDate = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
            paramUserRoleID.DbType = DbType.Int32;
            paramUserRoleID.Direction = ParameterDirection.Input;

            paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
            paramDateCreated.DbType = DbType.DateTime;
            paramDateCreated.Direction = ParameterDirection.Input;


            paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, Firstname);
            paramFirstname.DbType = DbType.String;
            paramFirstname.Size = 255;
            paramFirstname.Direction = ParameterDirection.Input;

            paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, Middlename);
            paramMiddlename.DbType = DbType.String;
            paramMiddlename.Size = 255;
            paramMiddlename.Direction = ParameterDirection.Input;

            paramLastname = new SqlParameter("@" + TAG_LASTNAME, Lastname);
            paramLastname.DbType = DbType.String;
            paramLastname.Size = 255;
            paramLastname.Direction = ParameterDirection.Input;

            paramPhoneNumber = new SqlParameter("@" + TAG_PHONE_NUMBER, PhoneNumber);
            paramPhoneNumber.DbType = DbType.String;
            paramPhoneNumber.Size = 255;
            paramPhoneNumber.Direction = ParameterDirection.Input;

            paramUsername = new SqlParameter("@" + TAG_USERNAME, Username);
            paramUsername.DbType = DbType.String;
            paramUsername.Size = 255;
            paramUsername.Direction = ParameterDirection.Input;

            paramPasswd = new SqlParameter("@" + TAG_PASSWD, Passwd);
            paramPasswd.DbType = DbType.String;
            paramPasswd.Size = 255;
            paramPasswd.Direction = ParameterDirection.Input;

            paramSsn = new SqlParameter("@" + TAG_SSN, Ssn);
            paramSsn.DbType = DbType.String;
            paramSsn.Size = 255;
            paramSsn.Direction = ParameterDirection.Input;

            paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, PictureUrl);
            paramPictureUrl.DbType = DbType.String;
            paramPictureUrl.Size = 255;
            paramPictureUrl.Direction = ParameterDirection.Input;

            paramPicture = new SqlParameter("@" + TAG_PICTURE, Picture);
            paramPicture.DbType = DbType.Binary;
            paramPicture.Size = 2147483647;
            paramPicture.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramWelcomeEmailSent = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT, WelcomeEmailSent);
            paramWelcomeEmailSent.DbType = DbType.Boolean;
            paramWelcomeEmailSent.Direction = ParameterDirection.Input;

            paramValidationtoken = new SqlParameter("@" + TAG_VALIDATIONTOKEN, Validationtoken);
            paramValidationtoken.DbType = DbType.String;
            paramValidationtoken.Size = 255;
            paramValidationtoken.Direction = ParameterDirection.Input;

            paramValidationlink = new SqlParameter("@" + TAG_VALIDATIONLINK, Validationlink);
            paramValidationlink.DbType = DbType.String;
            paramValidationlink.Size = 255;
            paramValidationlink.Direction = ParameterDirection.Input;

            paramIsvalidated = new SqlParameter("@" + TAG_ISVALIDATED, Isvalidated);
            paramIsvalidated.DbType = DbType.Boolean;
            paramIsvalidated.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(WelcomeEmailSentDate))
            {
                paramWelcomeEmailSentDate = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT_DATE, WelcomeEmailSentDate);
            }
            else
            {
                paramWelcomeEmailSentDate = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT_DATE, DBNull.Value);
            }
            paramWelcomeEmailSentDate.DbType = DbType.DateTime;
            paramWelcomeEmailSentDate.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(LastLoginDate))
            {
                paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, LastLoginDate);
            }
            else
            {
                paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, DBNull.Value);
            }
            paramLastLoginDate.DbType = DbType.DateTime;
            paramLastLoginDate.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramUserRoleID);
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramFirstname);
            cmd.Parameters.Add(paramMiddlename);
            cmd.Parameters.Add(paramLastname);
            cmd.Parameters.Add(paramPhoneNumber);
            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramPasswd);
            cmd.Parameters.Add(paramSsn);
            cmd.Parameters.Add(paramPictureUrl);
            cmd.Parameters.Add(paramPicture);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramWelcomeEmailSent);
            cmd.Parameters.Add(paramValidationtoken);
            cmd.Parameters.Add(paramValidationlink);
            cmd.Parameters.Add(paramIsvalidated);
            cmd.Parameters.Add(paramWelcomeEmailSentDate);
            cmd.Parameters.Add(paramLastLoginDate);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            UserID = long.Parse(strTmp);

            // cleanup to help GC
            paramUserRoleID = null;
            paramDateCreated = null;
            paramFirstname = null;
            paramMiddlename = null;
            paramLastname = null;
            paramPhoneNumber = null;
            paramUsername = null;
            paramPasswd = null;
            paramSsn = null;
            paramPictureUrl = null;
            paramPicture = null;
            paramIsDisabled = null;
            paramWelcomeEmailSent = null;
            paramValidationtoken = null;
            paramValidationlink = null;
            paramIsvalidated = null;
            paramWelcomeEmailSentDate = null;
            paramLastLoginDate = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Check to see if the row exists in database</summary>
        protected bool sqlExist(SqlConnection conn)
        {
            bool bExist = false;

            SqlCommand cmd = null;
            SqlParameter paramUserID = null;
            SqlParameter paramCount = null;

            cmd = new SqlCommand(SP_EXIST_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            paramUserID = new SqlParameter("@" + TAG_ID, UserID);
            paramUserID.Direction = ParameterDirection.Input;
            paramUserID.DbType = DbType.Int32;

            paramCount = new SqlParameter();
            paramCount.ParameterName = "@COUNT";
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramCount);
            cmd.ExecuteNonQuery();

            string strTmp;
            int nCount = 0;
            strTmp = cmd.Parameters["@COUNT"].Value.ToString();
            nCount = int.Parse(strTmp);
            if (nCount > 0)
            {
                bExist = true;
            }

            // cleanup
            paramUserID = null;
            paramCount = null;
            cmd = null;

            return bExist;
        }
        /// <summary>Updates row of data in database</summary>
        protected void sqlUpdate(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramUserID = null;
            SqlParameter paramUserRoleID = null;
            SqlParameter paramDateModified = null;
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
            SqlParameter paramWelcomeEmailSentDate = null;
            SqlParameter paramLastLoginDate = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_UPDATE_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramUserID = new SqlParameter("@" + TAG_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;


            paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
            paramUserRoleID.DbType = DbType.Int32;
            paramUserRoleID.Direction = ParameterDirection.Input;


            paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
            paramDateModified.DbType = DbType.DateTime;
            paramDateModified.Direction = ParameterDirection.Input;

            paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, Firstname);
            paramFirstname.DbType = DbType.String;
            paramFirstname.Size = 255;
            paramFirstname.Direction = ParameterDirection.Input;

            paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, Middlename);
            paramMiddlename.DbType = DbType.String;
            paramMiddlename.Size = 255;
            paramMiddlename.Direction = ParameterDirection.Input;

            paramLastname = new SqlParameter("@" + TAG_LASTNAME, Lastname);
            paramLastname.DbType = DbType.String;
            paramLastname.Size = 255;
            paramLastname.Direction = ParameterDirection.Input;

            paramPhoneNumber = new SqlParameter("@" + TAG_PHONE_NUMBER, PhoneNumber);
            paramPhoneNumber.DbType = DbType.String;
            paramPhoneNumber.Size = 255;
            paramPhoneNumber.Direction = ParameterDirection.Input;

            paramUsername = new SqlParameter("@" + TAG_USERNAME, Username);
            paramUsername.DbType = DbType.String;
            paramUsername.Size = 255;
            paramUsername.Direction = ParameterDirection.Input;

            paramPasswd = new SqlParameter("@" + TAG_PASSWD, Passwd);
            paramPasswd.DbType = DbType.String;
            paramPasswd.Size = 255;
            paramPasswd.Direction = ParameterDirection.Input;

            paramSsn = new SqlParameter("@" + TAG_SSN, Ssn);
            paramSsn.DbType = DbType.String;
            paramSsn.Size = 255;
            paramSsn.Direction = ParameterDirection.Input;

            paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, PictureUrl);
            paramPictureUrl.DbType = DbType.String;
            paramPictureUrl.Size = 255;
            paramPictureUrl.Direction = ParameterDirection.Input;

            paramPicture = new SqlParameter("@" + TAG_PICTURE, Picture);
            paramPicture.DbType = DbType.Binary;
            paramPicture.Size = 2147483647;
            paramPicture.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramWelcomeEmailSent = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT, WelcomeEmailSent);
            paramWelcomeEmailSent.DbType = DbType.Boolean;
            paramWelcomeEmailSent.Direction = ParameterDirection.Input;

            paramValidationtoken = new SqlParameter("@" + TAG_VALIDATIONTOKEN, Validationtoken);
            paramValidationtoken.DbType = DbType.String;
            paramValidationtoken.Size = 255;
            paramValidationtoken.Direction = ParameterDirection.Input;

            paramValidationlink = new SqlParameter("@" + TAG_VALIDATIONLINK, Validationlink);
            paramValidationlink.DbType = DbType.String;
            paramValidationlink.Size = 255;
            paramValidationlink.Direction = ParameterDirection.Input;

            paramIsvalidated = new SqlParameter("@" + TAG_ISVALIDATED, Isvalidated);
            paramIsvalidated.DbType = DbType.Boolean;
            paramIsvalidated.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(WelcomeEmailSentDate))
            {
                paramWelcomeEmailSentDate = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT_DATE, WelcomeEmailSentDate);
            }
            else
            {
                paramWelcomeEmailSentDate = new SqlParameter("@" + TAG_WELCOME_EMAIL_SENT_DATE, DBNull.Value);
            }
            paramWelcomeEmailSentDate.DbType = DbType.DateTime;
            paramWelcomeEmailSentDate.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(LastLoginDate))
            {
                paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, LastLoginDate);
            }
            else
            {
                paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, DBNull.Value);
            }
            paramLastLoginDate.DbType = DbType.DateTime;
            paramLastLoginDate.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramUserRoleID);
            cmd.Parameters.Add(paramDateModified);
            cmd.Parameters.Add(paramFirstname);
            cmd.Parameters.Add(paramMiddlename);
            cmd.Parameters.Add(paramLastname);
            cmd.Parameters.Add(paramPhoneNumber);
            cmd.Parameters.Add(paramUsername);
            cmd.Parameters.Add(paramPasswd);
            cmd.Parameters.Add(paramSsn);
            cmd.Parameters.Add(paramPictureUrl);
            cmd.Parameters.Add(paramPicture);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramWelcomeEmailSent);
            cmd.Parameters.Add(paramValidationtoken);
            cmd.Parameters.Add(paramValidationlink);
            cmd.Parameters.Add(paramIsvalidated);
            cmd.Parameters.Add(paramWelcomeEmailSentDate);
            cmd.Parameters.Add(paramLastLoginDate);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            UserID = long.Parse(s);

            // cleanup
            paramUserID = null;
            paramUserRoleID = null;
            paramDateModified = null;
            paramFirstname = null;
            paramMiddlename = null;
            paramLastname = null;
            paramPhoneNumber = null;
            paramUsername = null;
            paramPasswd = null;
            paramSsn = null;
            paramPictureUrl = null;
            paramPicture = null;
            paramIsDisabled = null;
            paramWelcomeEmailSent = null;
            paramValidationtoken = null;
            paramValidationlink = null;
            paramIsvalidated = null;
            paramWelcomeEmailSentDate = null;
            paramLastLoginDate = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Deletes row of data in database</summary>
        protected void sqlDelete(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramUserID = null;

            cmd = new SqlCommand(SP_DELETE_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramUserID = new SqlParameter("@" + TAG_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramUserID);
            cmd.ExecuteNonQuery();

            // cleanup to help GC
            paramUserID = null;
            cmd = null;

        }
        /// <summary>Load row of data from database</summary>
        protected void sqlLoad(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramUserID = null;
            SqlDataReader rdr = null;

            cmd = new SqlCommand(SP_LOAD_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramUserID = new SqlParameter("@" + TAG_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramUserID);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sqlParseResultSet(rdr);
            }
            // cleanup
            rdr.Dispose();
            rdr = null;
            paramUserID = null;
            cmd = null;
        }
        /// <summary>Parse result set</summary>
        protected void sqlParseResultSet(SqlDataReader rdr)
        {
            this.UserID = long.Parse(rdr[DB_FIELD_ID].ToString());
            try
            {
                this.UserRoleID = Convert.ToInt32(rdr[DB_FIELD_USER_ROLE_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
            }
            catch
            {
            }
            try
            {
                this.DateModified = DateTime.Parse(rdr[DB_FIELD_DATE_MODIFIED].ToString());
            }
            catch
            {
            }
            try
            {
                this.Firstname = rdr[DB_FIELD_FIRSTNAME].ToString().Trim();
            }
            catch { }
            try
            {
                this.Middlename = rdr[DB_FIELD_MIDDLENAME].ToString().Trim();
            }
            catch { }
            try
            {
                this.Lastname = rdr[DB_FIELD_LASTNAME].ToString().Trim();
            }
            catch { }
            try
            {
                this.PhoneNumber = rdr[DB_FIELD_PHONE_NUMBER].ToString().Trim();
            }
            catch { }
            try
            {
                this.Username = rdr[DB_FIELD_USERNAME].ToString().Trim();
            }
            catch { }
            try
            {
                this.Passwd = rdr[DB_FIELD_PASSWD].ToString().Trim();
            }
            catch { }
            try
            {
                this.Ssn = rdr[DB_FIELD_SSN].ToString().Trim();
            }
            catch { }
            try
            {
                this.PictureUrl = rdr[DB_FIELD_PICTURE_URL].ToString().Trim();
            }
            catch { }
            try
            {
                if (rdr[rdr.GetOrdinal(DB_FIELD_PICTURE)] != DBNull.Value)
                {
                    this.Picture = (byte[])rdr[rdr.GetOrdinal(DB_FIELD_PICTURE)];
                }
            }
            catch { }
            try
            {
                this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
            }
            catch { }
            try
            {
                this.WelcomeEmailSent = Convert.ToBoolean(rdr[DB_FIELD_WELCOME_EMAIL_SENT].ToString().Trim());
            }
            catch { }
            try
            {
                this.Validationtoken = rdr[DB_FIELD_VALIDATIONTOKEN].ToString().Trim();
            }
            catch { }
            try
            {
                this.Validationlink = rdr[DB_FIELD_VALIDATIONLINK].ToString().Trim();
            }
            catch { }
            try
            {
                this.Isvalidated = Convert.ToBoolean(rdr[DB_FIELD_ISVALIDATED].ToString().Trim());
            }
            catch { }
            try
            {
                this.WelcomeEmailSentDate = DateTime.Parse(rdr[DB_FIELD_WELCOME_EMAIL_SENT_DATE].ToString());
            }
            catch
            {
            }
            try
            {
                this.LastLoginDate = DateTime.Parse(rdr[DB_FIELD_LAST_LOGIN_DATE].ToString());
            }
            catch
            {
            }
        }

    }
}

//END OF User CLASS FILE