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
    /// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
    /// All Rights Reserved
    /// 
    /// File:  EnumUser.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	10/23/2016	Created
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
        private string _strAuthUserid = null;
        private string _strAuthConnection = null;
        private string _strAuthProvider = null;
        private string _strAuthAccessToken = null;
        private string _strAuthIdToken = null;
        private DateTime _dtBeginDateCreated = new DateTime();
        private DateTime _dtEndDateCreated = new DateTime();
        private DateTime _dtBeginDateModified = new DateTime();
        private DateTime _dtEndDateModified = new DateTime();
        private string _strFirstname = null;
        private string _strMiddlename = null;
        private string _strLastname = null;
        private string _strPhoneNumber = null;
        private string _strEmailAddress = null;
        private string _strProfileimageurl = null;
        private bool? _bIsDisabled = null;
        private bool? _bCanTextMsg = null;
        private DateTime _dtBeginDateTextMsgApproved = new DateTime();
        private DateTime _dtEndDateTextMsgApproved = new DateTime();
        //		private string _strOrderByEnum = "ASC";
        private string _strOrderByField = DB_FIELD_ID;

        /// <summary>DB_FIELD_ID Attribute type string</summary>
        public static readonly string DB_FIELD_ID = "user_id"; //Table id field name
                                                               /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Attribute UserID  name
                                                              /// <summary>AuthUserid Attribute type string</summary>
        public static readonly string TAG_AUTHUSERID = "AuthUserid"; //Attribute AuthUserid  name
                                                                     /// <summary>AuthConnection Attribute type string</summary>
        public static readonly string TAG_AUTHCONNECTION = "AuthConnection"; //Attribute AuthConnection  name
                                                                             /// <summary>AuthProvider Attribute type string</summary>
        public static readonly string TAG_AUTHPROVIDER = "AuthProvider"; //Attribute AuthProvider  name
                                                                         /// <summary>AuthAccessToken Attribute type string</summary>
        public static readonly string TAG_AUTHACCESSTOKEN = "AuthAccessToken"; //Attribute AuthAccessToken  name
                                                                               /// <summary>AuthIdToken Attribute type string</summary>
        public static readonly string TAG_AUTHIDTOKEN = "AuthIdToken"; //Attribute AuthIdToken  name
                                                                       /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
                                                                                   /// <summary>EndDateCreated Attribute type string</summary>
        public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
                                                                               /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
                                                                                     /// <summary>EndDateModified Attribute type string</summary>
        public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
                                                                                 /// <summary>Firstname Attribute type string</summary>
        public static readonly string TAG_FIRSTNAME = "Firstname"; //Attribute Firstname  name
                                                                   /// <summary>Middlename Attribute type string</summary>
        public static readonly string TAG_MIDDLENAME = "Middlename"; //Attribute Middlename  name
                                                                     /// <summary>Lastname Attribute type string</summary>
        public static readonly string TAG_LASTNAME = "Lastname"; //Attribute Lastname  name
                                                                 /// <summary>PhoneNumber Attribute type string</summary>
        public static readonly string TAG_PHONE_NUMBER = "PhoneNumber"; //Attribute PhoneNumber  name
                                                                        /// <summary>EmailAddress Attribute type string</summary>
        public static readonly string TAG_EMAIL_ADDRESS = "EmailAddress"; //Attribute EmailAddress  name
                                                                          /// <summary>Profileimageurl Attribute type string</summary>
        public static readonly string TAG_PROFILEIMAGEURL = "Profileimageurl"; //Attribute Profileimageurl  name
                                                                               /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Attribute IsDisabled  name
                                                                      /// <summary>CanTextMsg Attribute type string</summary>
        public static readonly string TAG_CAN_TEXT_MSG = "CanTextMsg"; //Attribute CanTextMsg  name
                                                                       /// <summary>DateTextMsgApproved Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_TEXT_MSG_APPROVED = "BeginDateTextMsgApproved"; //Attribute DateTextMsgApproved  name
                                                                                                     /// <summary>EndDateTextMsgApproved Attribute type string</summary>
        public static readonly string TAG_END_DATE_TEXT_MSG_APPROVED = "EndDateTextMsgApproved"; //Attribute DateTextMsgApproved  name
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
        /// <summary>Property DateTextMsgApproved. Type: DateTime</summary>
        public DateTime BeginDateTextMsgApproved
        {
            get { return _dtBeginDateTextMsgApproved; }
            set { _dtBeginDateTextMsgApproved = value; }
        }
        /// <summary>Property DateTextMsgApproved. Type: DateTime</summary>
        public DateTime EndDateTextMsgApproved
        {
            get { return _dtEndDateTextMsgApproved; }
            set { _dtEndDateTextMsgApproved = value; }
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
            sbReturn.Append(TAG_AUTHUSERID + ":  " + AuthUserid + "\n");
            sbReturn.Append(TAG_AUTHCONNECTION + ":  " + AuthConnection + "\n");
            sbReturn.Append(TAG_AUTHPROVIDER + ":  " + AuthProvider + "\n");
            sbReturn.Append(TAG_AUTHACCESSTOKEN + ":  " + AuthAccessToken + "\n");
            sbReturn.Append(TAG_AUTHIDTOKEN + ":  " + AuthIdToken + "\n");
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
            sbReturn.Append(TAG_FIRSTNAME + ":  " + Firstname + "\n");
            sbReturn.Append(TAG_MIDDLENAME + ":  " + Middlename + "\n");
            sbReturn.Append(TAG_LASTNAME + ":  " + Lastname + "\n");
            sbReturn.Append(TAG_PHONE_NUMBER + ":  " + PhoneNumber + "\n");
            sbReturn.Append(TAG_EMAIL_ADDRESS + ":  " + EmailAddress + "\n");
            sbReturn.Append(TAG_PROFILEIMAGEURL + ":  " + Profileimageurl + "\n");
            sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
            sbReturn.Append(TAG_CAN_TEXT_MSG + ":  " + CanTextMsg + "\n");
            if (!dtNull.Equals(BeginDateTextMsgApproved))
            {
                sbReturn.Append(TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ":  " + BeginDateTextMsgApproved.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ":\n");
            }
            if (!dtNull.Equals(EndDateTextMsgApproved))
            {
                sbReturn.Append(TAG_END_DATE_TEXT_MSG_APPROVED + ":  " + EndDateTextMsgApproved.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_END_DATE_TEXT_MSG_APPROVED + ":\n");
            }

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of User</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<" + ENTITY_NAME + ">\n");
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_AUTHUSERID + ">" + AuthUserid + "</" + TAG_AUTHUSERID + ">\n");
            sbReturn.Append("<" + TAG_AUTHCONNECTION + ">" + AuthConnection + "</" + TAG_AUTHCONNECTION + ">\n");
            sbReturn.Append("<" + TAG_AUTHPROVIDER + ">" + AuthProvider + "</" + TAG_AUTHPROVIDER + ">\n");
            sbReturn.Append("<" + TAG_AUTHACCESSTOKEN + ">" + AuthAccessToken + "</" + TAG_AUTHACCESSTOKEN + ">\n");
            sbReturn.Append("<" + TAG_AUTHIDTOKEN + ">" + AuthIdToken + "</" + TAG_AUTHIDTOKEN + ">\n");
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
            sbReturn.Append("<" + TAG_FIRSTNAME + ">" + Firstname + "</" + TAG_FIRSTNAME + ">\n");
            sbReturn.Append("<" + TAG_MIDDLENAME + ">" + Middlename + "</" + TAG_MIDDLENAME + ">\n");
            sbReturn.Append("<" + TAG_LASTNAME + ">" + Lastname + "</" + TAG_LASTNAME + ">\n");
            sbReturn.Append("<" + TAG_PHONE_NUMBER + ">" + PhoneNumber + "</" + TAG_PHONE_NUMBER + ">\n");
            sbReturn.Append("<" + TAG_EMAIL_ADDRESS + ">" + EmailAddress + "</" + TAG_EMAIL_ADDRESS + ">\n");
            sbReturn.Append("<" + TAG_PROFILEIMAGEURL + ">" + Profileimageurl + "</" + TAG_PROFILEIMAGEURL + ">\n");
            sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
            sbReturn.Append("<" + TAG_CAN_TEXT_MSG + ">" + CanTextMsg + "</" + TAG_CAN_TEXT_MSG + ">\n");
            if (!dtNull.Equals(BeginDateTextMsgApproved))
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ">" + BeginDateTextMsgApproved.ToString() + "</" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED + "></" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ">\n");
            }
            if (!dtNull.Equals(EndDateTextMsgApproved))
            {
                sbReturn.Append("<" + TAG_END_DATE_TEXT_MSG_APPROVED + ">" + EndDateTextMsgApproved.ToString() + "</" + TAG_END_DATE_TEXT_MSG_APPROVED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_END_DATE_TEXT_MSG_APPROVED + "></" + TAG_END_DATE_TEXT_MSG_APPROVED + ">\n");
            }
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
                xResultNode = xNode.SelectSingleNode(TAG_AUTHUSERID);
                AuthUserid = xResultNode.InnerText;
                if (AuthUserid.Trim().Length == 0)
                    AuthUserid = null;
            }
            catch
            {
                AuthUserid = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHCONNECTION);
                AuthConnection = xResultNode.InnerText;
                if (AuthConnection.Trim().Length == 0)
                    AuthConnection = null;
            }
            catch
            {
                AuthConnection = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHPROVIDER);
                AuthProvider = xResultNode.InnerText;
                if (AuthProvider.Trim().Length == 0)
                    AuthProvider = null;
            }
            catch
            {
                AuthProvider = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHACCESSTOKEN);
                AuthAccessToken = xResultNode.InnerText;
                if (AuthAccessToken.Trim().Length == 0)
                    AuthAccessToken = null;
            }
            catch
            {
                AuthAccessToken = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHIDTOKEN);
                AuthIdToken = xResultNode.InnerText;
                if (AuthIdToken.Trim().Length == 0)
                    AuthIdToken = null;
            }
            catch
            {
                AuthIdToken = null;
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
                xResultNode = xNode.SelectSingleNode(TAG_EMAIL_ADDRESS);
                EmailAddress = xResultNode.InnerText;
                if (EmailAddress.Trim().Length == 0)
                    EmailAddress = null;
            }
            catch
            {
                EmailAddress = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PROFILEIMAGEURL);
                Profileimageurl = xResultNode.InnerText;
                if (Profileimageurl.Trim().Length == 0)
                    Profileimageurl = null;
            }
            catch
            {
                Profileimageurl = null;
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
                xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_TEXT_MSG_APPROVED);
                BeginDateTextMsgApproved = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_END_DATE_TEXT_MSG_APPROVED);
                EndDateTextMsgApproved = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }
        }
        /// <summary>Prompt for values</summary>
        public void Prompt()
        {
            try
            {

                Console.WriteLine(TAG_AUTHUSERID + ":  ");
                AuthUserid = Console.ReadLine();
                if (AuthUserid.Length == 0)
                {
                    AuthUserid = null;
                }

                Console.WriteLine(TAG_AUTHCONNECTION + ":  ");
                AuthConnection = Console.ReadLine();
                if (AuthConnection.Length == 0)
                {
                    AuthConnection = null;
                }

                Console.WriteLine(TAG_AUTHPROVIDER + ":  ");
                AuthProvider = Console.ReadLine();
                if (AuthProvider.Length == 0)
                {
                    AuthProvider = null;
                }

                Console.WriteLine(TAG_AUTHACCESSTOKEN + ":  ");
                AuthAccessToken = Console.ReadLine();
                if (AuthAccessToken.Length == 0)
                {
                    AuthAccessToken = null;
                }

                Console.WriteLine(TAG_AUTHIDTOKEN + ":  ");
                AuthIdToken = Console.ReadLine();
                if (AuthIdToken.Length == 0)
                {
                    AuthIdToken = null;
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

                Console.WriteLine(TAG_EMAIL_ADDRESS + ":  ");
                EmailAddress = Console.ReadLine();
                if (EmailAddress.Length == 0)
                {
                    EmailAddress = null;
                }

                Console.WriteLine(TAG_PROFILEIMAGEURL + ":  ");
                Profileimageurl = Console.ReadLine();
                if (Profileimageurl.Length == 0)
                {
                    Profileimageurl = null;
                }
                Console.WriteLine(TAG_IS_DISABLED + ":  ");
                try
                {
                    IsDisabled = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    IsDisabled = false;
                }

                Console.WriteLine(TAG_CAN_TEXT_MSG + ":  ");
                try
                {
                    CanTextMsg = Convert.ToBoolean(Console.ReadLine());
                }
                catch
                {
                    CanTextMsg = false;
                }

                Console.WriteLine(TAG_BEGIN_DATE_TEXT_MSG_APPROVED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    BeginDateTextMsgApproved = DateTime.Parse(s);
                }
                catch
                {
                    BeginDateTextMsgApproved = new DateTime();
                }

                Console.WriteLine(TAG_END_DATE_TEXT_MSG_APPROVED + ":  ");
                try
                {
                    string s = Console.ReadLine();
                    EndDateTextMsgApproved = DateTime.Parse(s);
                }
                catch
                {
                    EndDateTextMsgApproved = new DateTime();
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
            SqlParameter paramAuthUserid = null;
            SqlParameter paramAuthConnection = null;
            SqlParameter paramAuthProvider = null;
            SqlParameter paramAuthAccessToken = null;
            SqlParameter paramAuthIdToken = null;
            SqlParameter paramBeginDateCreated = null;
            SqlParameter paramEndDateCreated = null;
            SqlParameter paramBeginDateModified = null;
            SqlParameter paramEndDateModified = null;
            SqlParameter paramFirstname = null;
            SqlParameter paramMiddlename = null;
            SqlParameter paramLastname = null;
            SqlParameter paramPhoneNumber = null;
            SqlParameter paramEmailAddress = null;
            SqlParameter paramProfileimageurl = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramCanTextMsg = null;
            SqlParameter paramBeginDateTextMsgApproved = null;
            SqlParameter paramEndDateTextMsgApproved = null;
            DateTime dtNull = new DateTime();

            sbLog = new System.Text.StringBuilder();
            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            sbLog.Append(TAG_USER_ID + "=" + UserID + "\n");
            paramUserID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserID);

            // Setup the AuthUserid text param
            if (AuthUserid != null)
            {
                paramAuthUserid = new SqlParameter("@" + TAG_AUTHUSERID, AuthUserid);
                sbLog.Append(TAG_AUTHUSERID + "=" + AuthUserid + "\n");
            }
            else
            {
                paramAuthUserid = new SqlParameter("@" + TAG_AUTHUSERID, DBNull.Value);
            }
            paramAuthUserid.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthUserid);

            // Setup the AuthConnection text param
            if (AuthConnection != null)
            {
                paramAuthConnection = new SqlParameter("@" + TAG_AUTHCONNECTION, AuthConnection);
                sbLog.Append(TAG_AUTHCONNECTION + "=" + AuthConnection + "\n");
            }
            else
            {
                paramAuthConnection = new SqlParameter("@" + TAG_AUTHCONNECTION, DBNull.Value);
            }
            paramAuthConnection.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthConnection);

            // Setup the AuthProvider text param
            if (AuthProvider != null)
            {
                paramAuthProvider = new SqlParameter("@" + TAG_AUTHPROVIDER, AuthProvider);
                sbLog.Append(TAG_AUTHPROVIDER + "=" + AuthProvider + "\n");
            }
            else
            {
                paramAuthProvider = new SqlParameter("@" + TAG_AUTHPROVIDER, DBNull.Value);
            }
            paramAuthProvider.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthProvider);

            // Setup the AuthAccessToken text param
            if (AuthAccessToken != null)
            {
                paramAuthAccessToken = new SqlParameter("@" + TAG_AUTHACCESSTOKEN, AuthAccessToken);
                sbLog.Append(TAG_AUTHACCESSTOKEN + "=" + AuthAccessToken + "\n");
            }
            else
            {
                paramAuthAccessToken = new SqlParameter("@" + TAG_AUTHACCESSTOKEN, DBNull.Value);
            }
            paramAuthAccessToken.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthAccessToken);

            // Setup the AuthIdToken text param
            if (AuthIdToken != null)
            {
                paramAuthIdToken = new SqlParameter("@" + TAG_AUTHIDTOKEN, AuthIdToken);
                sbLog.Append(TAG_AUTHIDTOKEN + "=" + AuthIdToken + "\n");
            }
            else
            {
                paramAuthIdToken = new SqlParameter("@" + TAG_AUTHIDTOKEN, DBNull.Value);
            }
            paramAuthIdToken.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthIdToken);

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

            // Setup the email address text param
            if (EmailAddress != null)
            {
                paramEmailAddress = new SqlParameter("@" + TAG_EMAIL_ADDRESS, EmailAddress);
                sbLog.Append(TAG_EMAIL_ADDRESS + "=" + EmailAddress + "\n");
            }
            else
            {
                paramEmailAddress = new SqlParameter("@" + TAG_EMAIL_ADDRESS, DBNull.Value);
            }
            paramEmailAddress.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEmailAddress);

            // Setup the profileimageurl text param
            if (Profileimageurl != null)
            {
                paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, Profileimageurl);
                sbLog.Append(TAG_PROFILEIMAGEURL + "=" + Profileimageurl + "\n");
            }
            else
            {
                paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, DBNull.Value);
            }
            paramProfileimageurl.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramProfileimageurl);

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            sbLog.Append(TAG_IS_DISABLED + "=" + IsDisabled + "\n");
            paramIsDisabled.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramIsDisabled);
            paramCanTextMsg = new SqlParameter("@" + TAG_CAN_TEXT_MSG, CanTextMsg);
            sbLog.Append(TAG_CAN_TEXT_MSG + "=" + CanTextMsg + "\n");
            paramCanTextMsg.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramCanTextMsg);
            // Setup the date text msg approved param
            if (!dtNull.Equals(BeginDateTextMsgApproved))
            {
                paramBeginDateTextMsgApproved = new SqlParameter("@" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED, BeginDateTextMsgApproved);
            }
            else
            {
                paramBeginDateTextMsgApproved = new SqlParameter("@" + TAG_BEGIN_DATE_TEXT_MSG_APPROVED, DBNull.Value);
            }
            paramBeginDateTextMsgApproved.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramBeginDateTextMsgApproved);

            if (!dtNull.Equals(EndDateTextMsgApproved))
            {
                paramEndDateTextMsgApproved = new SqlParameter("@" + TAG_END_DATE_TEXT_MSG_APPROVED, EndDateTextMsgApproved);
            }
            else
            {
                paramEndDateTextMsgApproved = new SqlParameter("@" + TAG_END_DATE_TEXT_MSG_APPROVED, DBNull.Value);
            }
            paramEndDateTextMsgApproved.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramEndDateTextMsgApproved);

        }

    }
}
