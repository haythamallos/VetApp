using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Vetapp.Engine.Common;

namespace Vetapp.Engine.DataAccessLayer.Data
{
    /// <summary>
    /// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  User.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	10/23/2016	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the User database table.
    /// </summary>
    public class User
    {
        //Attributes
        /// <summary>UserID Attribute type String</summary>
        private long _lUserID = 0;
        /// <summary>AuthUserid Attribute type String</summary>
        private string _strAuthUserid = null;
        /// <summary>AuthConnection Attribute type String</summary>
        private string _strAuthConnection = null;
        /// <summary>AuthProvider Attribute type String</summary>
        private string _strAuthProvider = null;
        /// <summary>AuthAccessToken Attribute type String</summary>
        private string _strAuthAccessToken = null;
        /// <summary>AuthIdToken Attribute type String</summary>
        private string _strAuthIdToken = null;
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
        /// <summary>EmailAddress Attribute type String</summary>
        private string _strEmailAddress = null;
        /// <summary>Profileimageurl Attribute type String</summary>
        private string _strProfileimageurl = null;
        /// <summary>IsDisabled Attribute type String</summary>
        private bool? _bIsDisabled = null;
        /// <summary>CanTextMsg Attribute type String</summary>
        private bool? _bCanTextMsg = null;
        /// <summary>DateTextMsgApproved Attribute type String</summary>
        private DateTime _dtDateTextMsgApproved = dtNull;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class User and is of type bool</summary>
        public static readonly string ENTITY_NAME = "User"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "user_id"; //Table id field name
                                                               /// <summary>AuthUserid Database field </summary>
        public static readonly string DB_FIELD_AUTHUSERID = "AuthUserid"; //Table AuthUserid field name
                                                                          /// <summary>AuthConnection Database field </summary>
        public static readonly string DB_FIELD_AUTHCONNECTION = "AuthConnection"; //Table AuthConnection field name
                                                                                  /// <summary>AuthProvider Database field </summary>
        public static readonly string DB_FIELD_AUTHPROVIDER = "AuthProvider"; //Table AuthProvider field name
                                                                              /// <summary>AuthAccessToken Database field </summary>
        public static readonly string DB_FIELD_AUTHACCESSTOKEN = "AuthAccessToken"; //Table AuthAccessToken field name
                                                                                    /// <summary>AuthIdToken Database field </summary>
        public static readonly string DB_FIELD_AUTHIDTOKEN = "AuthIdToken"; //Table AuthIdToken field name
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
                                                                              /// <summary>email_address Database field </summary>
        public static readonly string DB_FIELD_EMAIL_ADDRESS = "email_address"; //Table EmailAddress field name
                                                                                /// <summary>profileimageurl Database field </summary>
        public static readonly string DB_FIELD_PROFILEIMAGEURL = "profileimageurl"; //Table Profileimageurl field name
                                                                                    /// <summary>is_disabled Database field </summary>
        public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name
                                                                            /// <summary>can_text_msg Database field </summary>
        public static readonly string DB_FIELD_CAN_TEXT_MSG = "can_text_msg"; //Table CanTextMsg field name
                                                                              /// <summary>date_text_msg_approved Database field </summary>
        public static readonly string DB_FIELD_DATE_TEXT_MSG_APPROVED = "date_text_msg_approved"; //Table DateTextMsgApproved field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "UserID"; //Attribute id  name
                                                         /// <summary>AuthUserid Attribute type string</summary>
        public static readonly string TAG_AUTHUSERID = "AuthUserid"; //Table AuthUserid field name
                                                                     /// <summary>AuthConnection Attribute type string</summary>
        public static readonly string TAG_AUTHCONNECTION = "AuthConnection"; //Table AuthConnection field name
                                                                             /// <summary>AuthProvider Attribute type string</summary>
        public static readonly string TAG_AUTHPROVIDER = "AuthProvider"; //Table AuthProvider field name
                                                                         /// <summary>AuthAccessToken Attribute type string</summary>
        public static readonly string TAG_AUTHACCESSTOKEN = "AuthAccessToken"; //Table AuthAccessToken field name
                                                                               /// <summary>AuthIdToken Attribute type string</summary>
        public static readonly string TAG_AUTHIDTOKEN = "AuthIdToken"; //Table AuthIdToken field name
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
                                                                        /// <summary>EmailAddress Attribute type string</summary>
        public static readonly string TAG_EMAIL_ADDRESS = "EmailAddress"; //Table EmailAddress field name
                                                                          /// <summary>Profileimageurl Attribute type string</summary>
        public static readonly string TAG_PROFILEIMAGEURL = "Profileimageurl"; //Table Profileimageurl field name
                                                                               /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name
                                                                      /// <summary>CanTextMsg Attribute type string</summary>
        public static readonly string TAG_CAN_TEXT_MSG = "CanTextMsg"; //Table CanTextMsg field name
                                                                       /// <summary>DateTextMsgApproved Attribute type string</summary>
        public static readonly string TAG_DATE_TEXT_MSG_APPROVED = "DateTextMsgApproved"; //Table DateTextMsgApproved field name

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
        /// <summary>AuthUserid is a Property in the User Class of type String</summary>
        public string AuthUserid
        {
            get { return _strAuthUserid; }
            set { _strAuthUserid = value; }
        }
        /// <summary>AuthConnection is a Property in the User Class of type String</summary>
        public string AuthConnection
        {
            get { return _strAuthConnection; }
            set { _strAuthConnection = value; }
        }
        /// <summary>AuthProvider is a Property in the User Class of type String</summary>
        public string AuthProvider
        {
            get { return _strAuthProvider; }
            set { _strAuthProvider = value; }
        }
        /// <summary>AuthAccessToken is a Property in the User Class of type String</summary>
        public string AuthAccessToken
        {
            get { return _strAuthAccessToken; }
            set { _strAuthAccessToken = value; }
        }
        /// <summary>AuthIdToken is a Property in the User Class of type String</summary>
        public string AuthIdToken
        {
            get { return _strAuthIdToken; }
            set { _strAuthIdToken = value; }
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
        /// <summary>EmailAddress is a Property in the User Class of type String</summary>
        public string EmailAddress
        {
            get { return _strEmailAddress; }
            set { _strEmailAddress = value; }
        }
        /// <summary>Profileimageurl is a Property in the User Class of type String</summary>
        public string Profileimageurl
        {
            get { return _strProfileimageurl; }
            set { _strProfileimageurl = value; }
        }
        /// <summary>IsDisabled is a Property in the User Class of type bool</summary>
        public bool? IsDisabled
        {
            get { return _bIsDisabled; }
            set { _bIsDisabled = value; }
        }
        /// <summary>CanTextMsg is a Property in the User Class of type bool</summary>
        public bool? CanTextMsg
        {
            get { return _bCanTextMsg; }
            set { _bCanTextMsg = value; }
        }
        /// <summary>DateTextMsgApproved is a Property in the User Class of type DateTime</summary>
        public DateTime DateTextMsgApproved
        {
            get { return _dtDateTextMsgApproved; }
            set { _dtDateTextMsgApproved = value; }
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
            sbReturn.Append(TAG_AUTHUSERID + ":  " + AuthUserid + "\n");
            sbReturn.Append(TAG_AUTHCONNECTION + ":  " + AuthConnection + "\n");
            sbReturn.Append(TAG_AUTHPROVIDER + ":  " + AuthProvider + "\n");
            sbReturn.Append(TAG_AUTHACCESSTOKEN + ":  " + AuthAccessToken + "\n");
            sbReturn.Append(TAG_AUTHIDTOKEN + ":  " + AuthIdToken + "\n");
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
            sbReturn.Append(TAG_EMAIL_ADDRESS + ":  " + EmailAddress + "\n");
            sbReturn.Append(TAG_PROFILEIMAGEURL + ":  " + Profileimageurl + "\n");
            sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
            sbReturn.Append(TAG_CAN_TEXT_MSG + ":  " + CanTextMsg + "\n");
            if (!dtNull.Equals(DateTextMsgApproved))
            {
                sbReturn.Append(TAG_DATE_TEXT_MSG_APPROVED + ":  " + DateTextMsgApproved.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_TEXT_MSG_APPROVED + ":\n");
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
            sbReturn.Append("<" + TAG_AUTHUSERID + ">" + AuthUserid + "</" + TAG_AUTHUSERID + ">\n");
            sbReturn.Append("<" + TAG_AUTHCONNECTION + ">" + AuthConnection + "</" + TAG_AUTHCONNECTION + ">\n");
            sbReturn.Append("<" + TAG_AUTHPROVIDER + ">" + AuthProvider + "</" + TAG_AUTHPROVIDER + ">\n");
            sbReturn.Append("<" + TAG_AUTHACCESSTOKEN + ">" + AuthAccessToken + "</" + TAG_AUTHACCESSTOKEN + ">\n");
            sbReturn.Append("<" + TAG_AUTHIDTOKEN + ">" + AuthIdToken + "</" + TAG_AUTHIDTOKEN + ">\n");
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
            sbReturn.Append("<" + TAG_EMAIL_ADDRESS + ">" + EmailAddress + "</" + TAG_EMAIL_ADDRESS + ">\n");
            sbReturn.Append("<" + TAG_PROFILEIMAGEURL + ">" + Profileimageurl + "</" + TAG_PROFILEIMAGEURL + ">\n");
            sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
            sbReturn.Append("<" + TAG_CAN_TEXT_MSG + ">" + CanTextMsg + "</" + TAG_CAN_TEXT_MSG + ">\n");
            if (!dtNull.Equals(DateTextMsgApproved))
            {
                sbReturn.Append("<" + TAG_DATE_TEXT_MSG_APPROVED + ">" + DateTextMsgApproved.ToString() + "</" + TAG_DATE_TEXT_MSG_APPROVED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_DATE_TEXT_MSG_APPROVED + "></" + TAG_DATE_TEXT_MSG_APPROVED + ">\n");
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
                xResultNode = xNode.SelectSingleNode(TAG_AUTHUSERID);
                AuthUserid = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHCONNECTION);
                AuthConnection = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHPROVIDER);
                AuthProvider = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHACCESSTOKEN);
                AuthAccessToken = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHIDTOKEN);
                AuthIdToken = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
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
                xResultNode = xNode.SelectSingleNode(TAG_EMAIL_ADDRESS);
                EmailAddress = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PROFILEIMAGEURL);
                Profileimageurl = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

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
                xResultNode = xNode.SelectSingleNode(TAG_CAN_TEXT_MSG);
                CanTextMsg = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                CanTextMsg = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_DATE_TEXT_MSG_APPROVED);
                DateTextMsgApproved = DateTime.Parse(xResultNode.InnerText);
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

                Console.WriteLine(User.TAG_AUTHUSERID + ":  ");
                AuthUserid = Console.ReadLine();

                Console.WriteLine(User.TAG_AUTHCONNECTION + ":  ");
                AuthConnection = Console.ReadLine();

                Console.WriteLine(User.TAG_AUTHPROVIDER + ":  ");
                AuthProvider = Console.ReadLine();

                Console.WriteLine(User.TAG_AUTHACCESSTOKEN + ":  ");
                AuthAccessToken = Console.ReadLine();

                Console.WriteLine(User.TAG_AUTHIDTOKEN + ":  ");
                AuthIdToken = Console.ReadLine();
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

                Console.WriteLine(User.TAG_EMAIL_ADDRESS + ":  ");
                EmailAddress = Console.ReadLine();

                Console.WriteLine(User.TAG_PROFILEIMAGEURL + ":  ");
                Profileimageurl = Console.ReadLine();

                Console.WriteLine(User.TAG_IS_DISABLED + ":  ");
                IsDisabled = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine(User.TAG_CAN_TEXT_MSG + ":  ");
                CanTextMsg = Convert.ToBoolean(Console.ReadLine());
                try
                {
                    Console.WriteLine(User.TAG_DATE_TEXT_MSG_APPROVED + ":  ");
                    DateTextMsgApproved = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateTextMsgApproved = new DateTime();
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
            SqlParameter paramAuthUserid = null;
            SqlParameter paramAuthConnection = null;
            SqlParameter paramAuthProvider = null;
            SqlParameter paramAuthAccessToken = null;
            SqlParameter paramAuthIdToken = null;
            SqlParameter paramDateCreated = null;
            SqlParameter paramFirstname = null;
            SqlParameter paramMiddlename = null;
            SqlParameter paramLastname = null;
            SqlParameter paramPhoneNumber = null;
            SqlParameter paramEmailAddress = null;
            SqlParameter paramProfileimageurl = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramCanTextMsg = null;
            SqlParameter paramDateTextMsgApproved = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramAuthUserid = new SqlParameter("@" + TAG_AUTHUSERID, AuthUserid);
            paramAuthUserid.DbType = DbType.String;
            paramAuthUserid.Size = 255;
            paramAuthUserid.Direction = ParameterDirection.Input;

            paramAuthConnection = new SqlParameter("@" + TAG_AUTHCONNECTION, AuthConnection);
            paramAuthConnection.DbType = DbType.String;
            paramAuthConnection.Size = 255;
            paramAuthConnection.Direction = ParameterDirection.Input;

            paramAuthProvider = new SqlParameter("@" + TAG_AUTHPROVIDER, AuthProvider);
            paramAuthProvider.DbType = DbType.String;
            paramAuthProvider.Size = 255;
            paramAuthProvider.Direction = ParameterDirection.Input;

            paramAuthAccessToken = new SqlParameter("@" + TAG_AUTHACCESSTOKEN, AuthAccessToken);
            paramAuthAccessToken.DbType = DbType.String;
            paramAuthAccessToken.Size = 255;
            paramAuthAccessToken.Direction = ParameterDirection.Input;

            paramAuthIdToken = new SqlParameter("@" + TAG_AUTHIDTOKEN, AuthIdToken);
            paramAuthIdToken.DbType = DbType.String;
            paramAuthIdToken.Size = 1024;
            paramAuthIdToken.Direction = ParameterDirection.Input;

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

            paramEmailAddress = new SqlParameter("@" + TAG_EMAIL_ADDRESS, EmailAddress);
            paramEmailAddress.DbType = DbType.String;
            paramEmailAddress.Size = 255;
            paramEmailAddress.Direction = ParameterDirection.Input;

            paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, Profileimageurl);
            paramProfileimageurl.DbType = DbType.String;
            paramProfileimageurl.Size = 255;
            paramProfileimageurl.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramCanTextMsg = new SqlParameter("@" + TAG_CAN_TEXT_MSG, CanTextMsg);
            paramCanTextMsg.DbType = DbType.Boolean;
            paramCanTextMsg.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(DateTextMsgApproved))
            {
                paramDateTextMsgApproved = new SqlParameter("@" + TAG_DATE_TEXT_MSG_APPROVED, DateTextMsgApproved);
            }
            else
            {
                paramDateTextMsgApproved = new SqlParameter("@" + TAG_DATE_TEXT_MSG_APPROVED, DBNull.Value);
            }
            paramDateTextMsgApproved.DbType = DbType.DateTime;
            paramDateTextMsgApproved.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramAuthUserid);
            cmd.Parameters.Add(paramAuthConnection);
            cmd.Parameters.Add(paramAuthProvider);
            cmd.Parameters.Add(paramAuthAccessToken);
            cmd.Parameters.Add(paramAuthIdToken);
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramFirstname);
            cmd.Parameters.Add(paramMiddlename);
            cmd.Parameters.Add(paramLastname);
            cmd.Parameters.Add(paramPhoneNumber);
            cmd.Parameters.Add(paramEmailAddress);
            cmd.Parameters.Add(paramProfileimageurl);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramCanTextMsg);
            cmd.Parameters.Add(paramDateTextMsgApproved);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            UserID = long.Parse(strTmp);

            // cleanup to help GC
            paramAuthUserid = null;
            paramAuthConnection = null;
            paramAuthProvider = null;
            paramAuthAccessToken = null;
            paramAuthIdToken = null;
            paramDateCreated = null;
            paramFirstname = null;
            paramMiddlename = null;
            paramLastname = null;
            paramPhoneNumber = null;
            paramEmailAddress = null;
            paramProfileimageurl = null;
            paramIsDisabled = null;
            paramCanTextMsg = null;
            paramDateTextMsgApproved = null;
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
            SqlParameter paramAuthUserid = null;
            SqlParameter paramAuthConnection = null;
            SqlParameter paramAuthProvider = null;
            SqlParameter paramAuthAccessToken = null;
            SqlParameter paramAuthIdToken = null;
            SqlParameter paramDateModified = null;
            SqlParameter paramFirstname = null;
            SqlParameter paramMiddlename = null;
            SqlParameter paramLastname = null;
            SqlParameter paramPhoneNumber = null;
            SqlParameter paramEmailAddress = null;
            SqlParameter paramProfileimageurl = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramCanTextMsg = null;
            SqlParameter paramDateTextMsgApproved = null;
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


            paramAuthUserid = new SqlParameter("@" + TAG_AUTHUSERID, AuthUserid);
            paramAuthUserid.DbType = DbType.String;
            paramAuthUserid.Size = 255;
            paramAuthUserid.Direction = ParameterDirection.Input;

            paramAuthConnection = new SqlParameter("@" + TAG_AUTHCONNECTION, AuthConnection);
            paramAuthConnection.DbType = DbType.String;
            paramAuthConnection.Size = 255;
            paramAuthConnection.Direction = ParameterDirection.Input;

            paramAuthProvider = new SqlParameter("@" + TAG_AUTHPROVIDER, AuthProvider);
            paramAuthProvider.DbType = DbType.String;
            paramAuthProvider.Size = 255;
            paramAuthProvider.Direction = ParameterDirection.Input;

            paramAuthAccessToken = new SqlParameter("@" + TAG_AUTHACCESSTOKEN, AuthAccessToken);
            paramAuthAccessToken.DbType = DbType.String;
            paramAuthAccessToken.Size = 255;
            paramAuthAccessToken.Direction = ParameterDirection.Input;

            paramAuthIdToken = new SqlParameter("@" + TAG_AUTHIDTOKEN, AuthIdToken);
            paramAuthIdToken.DbType = DbType.String;
            paramAuthIdToken.Size = 1024;
            paramAuthIdToken.Direction = ParameterDirection.Input;


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

            paramEmailAddress = new SqlParameter("@" + TAG_EMAIL_ADDRESS, EmailAddress);
            paramEmailAddress.DbType = DbType.String;
            paramEmailAddress.Size = 255;
            paramEmailAddress.Direction = ParameterDirection.Input;

            paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, Profileimageurl);
            paramProfileimageurl.DbType = DbType.String;
            paramProfileimageurl.Size = 255;
            paramProfileimageurl.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramCanTextMsg = new SqlParameter("@" + TAG_CAN_TEXT_MSG, CanTextMsg);
            paramCanTextMsg.DbType = DbType.Boolean;
            paramCanTextMsg.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(DateTextMsgApproved))
            {
                paramDateTextMsgApproved = new SqlParameter("@" + TAG_DATE_TEXT_MSG_APPROVED, DateTextMsgApproved);
            }
            else
            {
                paramDateTextMsgApproved = new SqlParameter("@" + TAG_DATE_TEXT_MSG_APPROVED, DBNull.Value);
            }
            paramDateTextMsgApproved.DbType = DbType.DateTime;
            paramDateTextMsgApproved.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramAuthUserid);
            cmd.Parameters.Add(paramAuthConnection);
            cmd.Parameters.Add(paramAuthProvider);
            cmd.Parameters.Add(paramAuthAccessToken);
            cmd.Parameters.Add(paramAuthIdToken);
            cmd.Parameters.Add(paramDateModified);
            cmd.Parameters.Add(paramFirstname);
            cmd.Parameters.Add(paramMiddlename);
            cmd.Parameters.Add(paramLastname);
            cmd.Parameters.Add(paramPhoneNumber);
            cmd.Parameters.Add(paramEmailAddress);
            cmd.Parameters.Add(paramProfileimageurl);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramCanTextMsg);
            cmd.Parameters.Add(paramDateTextMsgApproved);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            UserID = long.Parse(s);

            // cleanup
            paramUserID = null;
            paramAuthUserid = null;
            paramAuthConnection = null;
            paramAuthProvider = null;
            paramAuthAccessToken = null;
            paramAuthIdToken = null;
            paramDateModified = null;
            paramFirstname = null;
            paramMiddlename = null;
            paramLastname = null;
            paramPhoneNumber = null;
            paramEmailAddress = null;
            paramProfileimageurl = null;
            paramIsDisabled = null;
            paramCanTextMsg = null;
            paramDateTextMsgApproved = null;
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
                this.AuthUserid = rdr[DB_FIELD_AUTHUSERID].ToString().Trim();
            }
            catch { }
            try
            {
                this.AuthConnection = rdr[DB_FIELD_AUTHCONNECTION].ToString().Trim();
            }
            catch { }
            try
            {
                this.AuthProvider = rdr[DB_FIELD_AUTHPROVIDER].ToString().Trim();
            }
            catch { }
            try
            {
                this.AuthAccessToken = rdr[DB_FIELD_AUTHACCESSTOKEN].ToString().Trim();
            }
            catch { }
            try
            {
                this.AuthIdToken = rdr[DB_FIELD_AUTHIDTOKEN].ToString().Trim();
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
                this.EmailAddress = rdr[DB_FIELD_EMAIL_ADDRESS].ToString().Trim();
            }
            catch { }
            try
            {
                this.Profileimageurl = rdr[DB_FIELD_PROFILEIMAGEURL].ToString().Trim();
            }
            catch { }
            try
            {
                this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
            }
            catch { }
            try
            {
                this.CanTextMsg = Convert.ToBoolean(rdr[DB_FIELD_CAN_TEXT_MSG].ToString().Trim());
            }
            catch { }
            try
            {
                this.DateTextMsgApproved = DateTime.Parse(rdr[DB_FIELD_DATE_TEXT_MSG_APPROVED].ToString());
            }
            catch
            {
            }
        }

    }
}

//END OF User CLASS FILE