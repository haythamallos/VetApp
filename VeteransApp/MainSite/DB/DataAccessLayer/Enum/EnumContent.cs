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
    /// File:  EnumContent.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/21/2017	Created
    /// 
    /// ----------------------------------------------------
    /// </summary>
    public class EnumContent
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
        public static readonly string ENTITY_NAME = "EnumContent"; //Table name to abstract
        private static DateTime dtNull = new DateTime();
        private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

        private long _lContentID = 0;
        private long _lUserID = 0;
        private long _lPurchaseID = 0;
        private long _lContentStateID = 0;
        private long _lContentTypeID = 0;
        private DateTime _dtBeginDateCreated = new DateTime();
        private DateTime _dtEndDateCreated = new DateTime();
        private DateTime _dtBeginDateModified = new DateTime();
        private DateTime _dtEndDateModified = new DateTime();
        private string _strContentUrl = null;
        private byte[] _byteContentData = null;
        private string _strContentMeta = null;
        private bool? _bIsDisabled = null;
        private string _strNotes = null;
        private string _strAuthtoken = null;
        private long _lErrorPurchaseID = 0;
        //		private string _strOrderByEnum = "ASC";
        private string _strOrderByField = DB_FIELD_ID;

        /// <summary>DB_FIELD_ID Attribute type string</summary>
        public static readonly string DB_FIELD_ID = "content_id"; //Table id field name
                                                                  /// <summary>ContentID Attribute type string</summary>
        public static readonly string TAG_CONTENT_ID = "ContentID"; //Attribute ContentID  name
                                                                    /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Attribute UserID  name
                                                              /// <summary>PurchaseID Attribute type string</summary>
        public static readonly string TAG_PURCHASE_ID = "PurchaseID"; //Attribute PurchaseID  name
                                                                      /// <summary>ContentStateID Attribute type string</summary>
        public static readonly string TAG_CONTENT_STATE_ID = "ContentStateID"; //Attribute ContentStateID  name
                                                                               /// <summary>ContentTypeID Attribute type string</summary>
        public static readonly string TAG_CONTENT_TYPE_ID = "ContentTypeID"; //Attribute ContentTypeID  name
                                                                             /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
                                                                                   /// <summary>EndDateCreated Attribute type string</summary>
        public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
                                                                               /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
                                                                                     /// <summary>EndDateModified Attribute type string</summary>
        public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
                                                                                 /// <summary>ContentUrl Attribute type string</summary>
        public static readonly string TAG_CONTENT_URL = "ContentUrl"; //Attribute ContentUrl  name
                                                                      /// <summary>ContentData Attribute type string</summary>
        public static readonly string TAG_CONTENT_DATA = "ContentData"; //Attribute ContentData  name
                                                                        /// <summary>ContentMeta Attribute type string</summary>
        public static readonly string TAG_CONTENT_META = "ContentMeta"; //Attribute ContentMeta  name
                                                                        /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Attribute IsDisabled  name
                                                                      /// <summary>Notes Attribute type string</summary>
        public static readonly string TAG_NOTES = "Notes"; //Attribute Notes  name
                                                           /// <summary>Authtoken Attribute type string</summary>
        public static readonly string TAG_AUTHTOKEN = "Authtoken"; //Attribute Authtoken  name
                                                                   /// <summary>ErrorPurchaseID Attribute type string</summary>
        public static readonly string TAG_ERROR_PURCHASE_ID = "ErrorPurchaseID"; //Attribute ErrorPurchaseID  name
                                                                                 // Stored procedure name
        public string SP_ENUM_NAME = "spContentEnum"; //Enum sp name

        /// <summary>HasError is a Property in the Content Class of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        /// <summary>ContentID is a Property in the Content Class of type long</summary>
        public long ContentID
        {
            get { return _lContentID; }
            set { _lContentID = value; }
        }
        /// <summary>UserID is a Property in the Content Class of type long</summary>
        public long UserID
        {
            get { return _lUserID; }
            set { _lUserID = value; }
        }
        /// <summary>PurchaseID is a Property in the Content Class of type long</summary>
        public long PurchaseID
        {
            get { return _lPurchaseID; }
            set { _lPurchaseID = value; }
        }
        /// <summary>ContentStateID is a Property in the Content Class of type long</summary>
        public long ContentStateID
        {
            get { return _lContentStateID; }
            set { _lContentStateID = value; }
        }
        /// <summary>ContentTypeID is a Property in the Content Class of type long</summary>
        public long ContentTypeID
        {
            get { return _lContentTypeID; }
            set { _lContentTypeID = value; }
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
        /// <summary>ContentUrl is a Property in the Content Class of type String</summary>
        public string ContentUrl
        {
            get { return _strContentUrl; }
            set { _strContentUrl = value; }
        }
        /// <summary>ContentData is a Property in the Content Class of type byte[]</summary>
        public byte[] ContentData
        {
            get { return _byteContentData; }
            set { _byteContentData = value; }
        }
        /// <summary>ContentMeta is a Property in the Content Class of type String</summary>
        public string ContentMeta
        {
            get { return _strContentMeta; }
            set { _strContentMeta = value; }
        }
        /// <summary>IsDisabled is a Property in the Content Class of type bool</summary>
        public bool? IsDisabled
        {
            get { return _bIsDisabled; }
            set { _bIsDisabled = value; }
        }
        /// <summary>Notes is a Property in the Content Class of type String</summary>
        public string Notes
        {
            get { return _strNotes; }
            set { _strNotes = value; }
        }
        /// <summary>Authtoken is a Property in the Content Class of type String</summary>
        public string Authtoken
        {
            get { return _strAuthtoken; }
            set { _strAuthtoken = value; }
        }
        /// <summary>ErrorPurchaseID is a Property in the Content Class of type long</summary>
        public long ErrorPurchaseID
        {
            get { return _lErrorPurchaseID; }
            set { _lErrorPurchaseID = value; }
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
        public EnumContent()
        {
        }
        /// <summary>Contructor takes 1 parameter: SqlConnection</summary>
        public EnumContent(SqlConnection conn)
        {
            _conn = conn;
        }


        // Implementation of IEnumerator
        /// <summary>Property of type Content. Returns the next Content in the list</summary>
        private Content _nextTransaction
        {
            get
            {
                Content o = null;

                if (!_bSetup)
                {
                    EnumData();
                }
                if (_hasMore)
                {
                    o = new Content(_rdr);
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

        /// <summary>ToString is overridden to display all properties of the Content Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_CONTENT_ID + ":  " + ContentID.ToString() + "\n");
            sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
            sbReturn.Append(TAG_PURCHASE_ID + ":  " + PurchaseID + "\n");
            sbReturn.Append(TAG_CONTENT_STATE_ID + ":  " + ContentStateID + "\n");
            sbReturn.Append(TAG_CONTENT_TYPE_ID + ":  " + ContentTypeID + "\n");
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
            sbReturn.Append(TAG_CONTENT_URL + ":  " + ContentUrl + "\n");
            sbReturn.Append(TAG_CONTENT_DATA + ":  " + ContentData + "\n");
            sbReturn.Append(TAG_CONTENT_META + ":  " + ContentMeta + "\n");
            sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
            sbReturn.Append(TAG_NOTES + ":  " + Notes + "\n");
            sbReturn.Append(TAG_AUTHTOKEN + ":  " + Authtoken + "\n");
            sbReturn.Append(TAG_ERROR_PURCHASE_ID + ":  " + ErrorPurchaseID + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of Content</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<" + ENTITY_NAME + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_ID + ">" + ContentID + "</" + TAG_CONTENT_ID + ">\n");
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_PURCHASE_ID + ">" + PurchaseID + "</" + TAG_PURCHASE_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_STATE_ID + ">" + ContentStateID + "</" + TAG_CONTENT_STATE_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_TYPE_ID + ">" + ContentTypeID + "</" + TAG_CONTENT_TYPE_ID + ">\n");
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
            sbReturn.Append("<" + TAG_CONTENT_URL + ">" + ContentUrl + "</" + TAG_CONTENT_URL + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_DATA + ">" + ContentData + "</" + TAG_CONTENT_DATA + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_META + ">" + ContentMeta + "</" + TAG_CONTENT_META + ">\n");
            sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
            sbReturn.Append("<" + TAG_NOTES + ">" + Notes + "</" + TAG_NOTES + ">\n");
            sbReturn.Append("<" + TAG_AUTHTOKEN + ">" + Authtoken + "</" + TAG_AUTHTOKEN + ">\n");
            sbReturn.Append("<" + TAG_ERROR_PURCHASE_ID + ">" + ErrorPurchaseID + "</" + TAG_ERROR_PURCHASE_ID + ">\n");
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
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_ID);
                strTmp = xResultNode.InnerText;
                ContentID = (long)Convert.ToInt32(strTmp);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_USER_ID);
                UserID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                UserID = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_PURCHASE_ID);
                PurchaseID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                PurchaseID = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_STATE_ID);
                ContentStateID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                ContentStateID = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_TYPE_ID);
                ContentTypeID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                ContentTypeID = 0;
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
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_URL);
                ContentUrl = xResultNode.InnerText;
                if (ContentUrl.Trim().Length == 0)
                    ContentUrl = null;
            }
            catch
            {
                ContentUrl = null;
            }
            // Cannot reliably convert a byte[] to a string.

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_META);
                ContentMeta = xResultNode.InnerText;
                if (ContentMeta.Trim().Length == 0)
                    ContentMeta = null;
            }
            catch
            {
                ContentMeta = null;
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
                xResultNode = xNode.SelectSingleNode(TAG_NOTES);
                Notes = xResultNode.InnerText;
                if (Notes.Trim().Length == 0)
                    Notes = null;
            }
            catch
            {
                Notes = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_AUTHTOKEN);
                Authtoken = xResultNode.InnerText;
                if (Authtoken.Trim().Length == 0)
                    Authtoken = null;
            }
            catch
            {
                Authtoken = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_ERROR_PURCHASE_ID);
                ErrorPurchaseID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                ErrorPurchaseID = 0;
            }
        }
        /// <summary>Prompt for values</summary>
        public void Prompt()
        {
            try
            {
                Console.WriteLine(TAG_USER_ID + ":  ");
                try
                {
                    UserID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UserID = 0;
                }

                Console.WriteLine(TAG_PURCHASE_ID + ":  ");
                try
                {
                    PurchaseID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    PurchaseID = 0;
                }

                Console.WriteLine(TAG_CONTENT_STATE_ID + ":  ");
                try
                {
                    ContentStateID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ContentStateID = 0;
                }

                Console.WriteLine(TAG_CONTENT_TYPE_ID + ":  ");
                try
                {
                    ContentTypeID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ContentTypeID = 0;
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


                Console.WriteLine(TAG_CONTENT_URL + ":  ");
                ContentUrl = Console.ReadLine();
                if (ContentUrl.Length == 0)
                {
                    ContentUrl = null;
                }
                // Cannot reliably convert a byte[] to string.

                Console.WriteLine(TAG_CONTENT_META + ":  ");
                ContentMeta = Console.ReadLine();
                if (ContentMeta.Length == 0)
                {
                    ContentMeta = null;
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


                Console.WriteLine(TAG_NOTES + ":  ");
                Notes = Console.ReadLine();
                if (Notes.Length == 0)
                {
                    Notes = null;
                }

                Console.WriteLine(TAG_AUTHTOKEN + ":  ");
                Authtoken = Console.ReadLine();
                if (Authtoken.Length == 0)
                {
                    Authtoken = null;
                }
                Console.WriteLine(TAG_ERROR_PURCHASE_ID + ":  ");
                try
                {
                    ErrorPurchaseID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ErrorPurchaseID = 0;
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
            SqlParameter paramContentID = null;
            SqlParameter paramUserID = null;
            SqlParameter paramPurchaseID = null;
            SqlParameter paramContentStateID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramBeginDateCreated = null;
            SqlParameter paramEndDateCreated = null;
            SqlParameter paramBeginDateModified = null;
            SqlParameter paramEndDateModified = null;
            SqlParameter paramContentUrl = null;
            SqlParameter paramContentData = null;
            SqlParameter paramContentMeta = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramNotes = null;
            SqlParameter paramAuthtoken = null;
            SqlParameter paramErrorPurchaseID = null;
            DateTime dtNull = new DateTime();

            sbLog = new System.Text.StringBuilder();
            paramContentID = new SqlParameter("@" + TAG_CONTENT_ID, ContentID);
            sbLog.Append(TAG_CONTENT_ID + "=" + ContentID + "\n");
            paramContentID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentID);

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            sbLog.Append(TAG_USER_ID + "=" + UserID + "\n");
            paramUserID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserID);
            paramPurchaseID = new SqlParameter("@" + TAG_PURCHASE_ID, PurchaseID);
            sbLog.Append(TAG_PURCHASE_ID + "=" + PurchaseID + "\n");
            paramPurchaseID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramPurchaseID);
            paramContentStateID = new SqlParameter("@" + TAG_CONTENT_STATE_ID, ContentStateID);
            sbLog.Append(TAG_CONTENT_STATE_ID + "=" + ContentStateID + "\n");
            paramContentStateID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentStateID);
            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            sbLog.Append(TAG_CONTENT_TYPE_ID + "=" + ContentTypeID + "\n");
            paramContentTypeID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentTypeID);
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

            // Setup the content url text param
            if (ContentUrl != null)
            {
                paramContentUrl = new SqlParameter("@" + TAG_CONTENT_URL, ContentUrl);
                sbLog.Append(TAG_CONTENT_URL + "=" + ContentUrl + "\n");
            }
            else
            {
                paramContentUrl = new SqlParameter("@" + TAG_CONTENT_URL, DBNull.Value);
            }
            paramContentUrl.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentUrl);

            // Setup the content data text param
            //if ( ContentData != null )
            //{
            //	paramContentData = new SqlParameter("@" + TAG_CONTENT_DATA, ContentData);
            //	sbLog.Append(TAG_CONTENT_DATA + "=" + ContentData + "\n");
            //}
            //else
            //{
            //	paramContentData = new SqlParameter("@" + TAG_CONTENT_DATA, DBNull.Value);
            //}
            //paramContentData.Direction = ParameterDirection.Input;
            //_cmd.Parameters.Add(paramContentData);

            // Setup the content meta text param
            if (ContentMeta != null)
            {
                paramContentMeta = new SqlParameter("@" + TAG_CONTENT_META, ContentMeta);
                sbLog.Append(TAG_CONTENT_META + "=" + ContentMeta + "\n");
            }
            else
            {
                paramContentMeta = new SqlParameter("@" + TAG_CONTENT_META, DBNull.Value);
            }
            paramContentMeta.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentMeta);

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            sbLog.Append(TAG_IS_DISABLED + "=" + IsDisabled + "\n");
            paramIsDisabled.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramIsDisabled);
            // Setup the notes text param
            if (Notes != null)
            {
                paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
                sbLog.Append(TAG_NOTES + "=" + Notes + "\n");
            }
            else
            {
                paramNotes = new SqlParameter("@" + TAG_NOTES, DBNull.Value);
            }
            paramNotes.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramNotes);

            // Setup the authtoken text param
            if (Authtoken != null)
            {
                paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, Authtoken);
                sbLog.Append(TAG_AUTHTOKEN + "=" + Authtoken + "\n");
            }
            else
            {
                paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, DBNull.Value);
            }
            paramAuthtoken.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramAuthtoken);

            paramErrorPurchaseID = new SqlParameter("@" + TAG_ERROR_PURCHASE_ID, ErrorPurchaseID);
            sbLog.Append(TAG_ERROR_PURCHASE_ID + "=" + ErrorPurchaseID + "\n");
            paramErrorPurchaseID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramErrorPurchaseID);
        }

    }
}
