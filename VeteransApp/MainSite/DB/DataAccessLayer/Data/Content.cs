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
    /// File:  Content.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/1/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the Content database table.
    /// </summary>
    public class Content
    {
        //Attributes
        /// <summary>ContentID Attribute type String</summary>
        private long _lContentID = 0;
        /// <summary>UserID Attribute type String</summary>
        private long _lUserID = 0;
        /// <summary>ContentTypeID Attribute type String</summary>
        private long _lContentTypeID = 0;
        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;
        /// <summary>DateModified Attribute type String</summary>
        private DateTime _dtDateModified = dtNull;
        /// <summary>ContentUrl Attribute type String</summary>
        private string _strContentUrl = null;
        /// <summary>ContentData Attribute type String</summary>
        private byte[] _byteContentData = null;
        /// <summary>ContentMeta Attribute type String</summary>
        private string _strContentMeta = null;
        /// <summary>IsSubmitted Attribute type String</summary>
        private bool? _bIsSubmitted = null;
        /// <summary>IsDisabled Attribute type String</summary>
        private bool? _bIsDisabled = null;
        /// <summary>IsDraft Attribute type String</summary>
        private bool? _bIsDraft = null;
        /// <summary>DateSubmitted Attribute type String</summary>
        private DateTime _dtDateSubmitted = dtNull;
        /// <summary>Notes Attribute type String</summary>
        private string _strNotes = null;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class Content and is of type bool</summary>
        public static readonly string ENTITY_NAME = "Content"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "content_id"; //Table id field name
                                                                  /// <summary>user_id Database field </summary>
        public static readonly string DB_FIELD_USER_ID = "user_id"; //Table UserID field name
                                                                    /// <summary>content_type_id Database field </summary>
        public static readonly string DB_FIELD_CONTENT_TYPE_ID = "content_type_id"; //Table ContentTypeID field name
                                                                                    /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
                                                                              /// <summary>date_modified Database field </summary>
        public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
                                                                                /// <summary>content_url Database field </summary>
        public static readonly string DB_FIELD_CONTENT_URL = "content_url"; //Table ContentUrl field name
                                                                            /// <summary>content_data Database field </summary>
        public static readonly string DB_FIELD_CONTENT_DATA = "content_data"; //Table ContentData field name
                                                                              /// <summary>content_meta Database field </summary>
        public static readonly string DB_FIELD_CONTENT_META = "content_meta"; //Table ContentMeta field name
                                                                              /// <summary>is_submitted Database field </summary>
        public static readonly string DB_FIELD_IS_SUBMITTED = "is_submitted"; //Table IsSubmitted field name
                                                                              /// <summary>is_disabled Database field </summary>
        public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name
                                                                            /// <summary>is_draft Database field </summary>
        public static readonly string DB_FIELD_IS_DRAFT = "is_draft"; //Table IsDraft field name
                                                                      /// <summary>date_submitted Database field </summary>
        public static readonly string DB_FIELD_DATE_SUBMITTED = "date_submitted"; //Table DateSubmitted field name
                                                                                  /// <summary>notes Database field </summary>
        public static readonly string DB_FIELD_NOTES = "notes"; //Table Notes field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "ContentID"; //Attribute id  name
                                                            /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Table UserID field name
                                                              /// <summary>ContentTypeID Attribute type string</summary>
        public static readonly string TAG_CONTENT_TYPE_ID = "ContentTypeID"; //Table ContentTypeID field name
                                                                             /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
                                                                        /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
                                                                          /// <summary>ContentUrl Attribute type string</summary>
        public static readonly string TAG_CONTENT_URL = "ContentUrl"; //Table ContentUrl field name
                                                                      /// <summary>ContentData Attribute type string</summary>
        public static readonly string TAG_CONTENT_DATA = "ContentData"; //Table ContentData field name
                                                                        /// <summary>ContentMeta Attribute type string</summary>
        public static readonly string TAG_CONTENT_META = "ContentMeta"; //Table ContentMeta field name
                                                                        /// <summary>IsSubmitted Attribute type string</summary>
        public static readonly string TAG_IS_SUBMITTED = "IsSubmitted"; //Table IsSubmitted field name
                                                                        /// <summary>IsDisabled Attribute type string</summary>
        public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name
                                                                      /// <summary>IsDraft Attribute type string</summary>
        public static readonly string TAG_IS_DRAFT = "IsDraft"; //Table IsDraft field name
                                                                /// <summary>DateSubmitted Attribute type string</summary>
        public static readonly string TAG_DATE_SUBMITTED = "DateSubmitted"; //Table DateSubmitted field name
                                                                            /// <summary>Notes Attribute type string</summary>
        public static readonly string TAG_NOTES = "Notes"; //Table Notes field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spContentInsert"; //Insert sp name
        private static readonly string SP_UPDATE_NAME = "spContentUpdate"; //Update sp name
        private static readonly string SP_DELETE_NAME = "spContentDelete"; //Delete sp name
        private static readonly string SP_LOAD_NAME = "spContentLoad"; //Load sp name
        private static readonly string SP_EXIST_NAME = "spContentExist"; //Exist sp name

        //properties
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
        /// <summary>ContentTypeID is a Property in the Content Class of type long</summary>
        public long ContentTypeID
        {
            get { return _lContentTypeID; }
            set { _lContentTypeID = value; }
        }
        /// <summary>DateCreated is a Property in the Content Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }
        /// <summary>DateModified is a Property in the Content Class of type DateTime</summary>
        public DateTime DateModified
        {
            get { return _dtDateModified; }
            set { _dtDateModified = value; }
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
        /// <summary>IsSubmitted is a Property in the Content Class of type bool</summary>
        public bool? IsSubmitted
        {
            get { return _bIsSubmitted; }
            set { _bIsSubmitted = value; }
        }
        /// <summary>IsDisabled is a Property in the Content Class of type bool</summary>
        public bool? IsDisabled
        {
            get { return _bIsDisabled; }
            set { _bIsDisabled = value; }
        }
        /// <summary>IsDraft is a Property in the Content Class of type bool</summary>
        public bool? IsDraft
        {
            get { return _bIsDraft; }
            set { _bIsDraft = value; }
        }
        /// <summary>DateSubmitted is a Property in the Content Class of type DateTime</summary>
        public DateTime DateSubmitted
        {
            get { return _dtDateSubmitted; }
            set { _dtDateSubmitted = value; }
        }
        /// <summary>Notes is a Property in the Content Class of type String</summary>
        public string Notes
        {
            get { return _strNotes; }
            set { _strNotes = value; }
        }


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>HasError Property in class Content and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Error Property in class Content and is of type ErrorCode</summary>
        public ErrorCode Error
        {
            get { return _errorCode; }
        }

        //Constructors
        /// <summary>Content empty constructor</summary>
        public Content()
        {
        }
        /// <summary>Content constructor takes ContentID and a SqlConnection</summary>
        public Content(long l, SqlConnection conn)
        {
            ContentID = l;
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
        /// <summary>Content Constructor takes pStrData and Config</summary>
        public Content(string pStrData)
        {
            Parse(pStrData);
        }
        /// <summary>Content Constructor takes SqlDataReader</summary>
        public Content(SqlDataReader rd)
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
        /// <summary>ToString is overridden to display all properties of the Content Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + ContentID.ToString() + "\n");
            sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
            sbReturn.Append(TAG_CONTENT_TYPE_ID + ":  " + ContentTypeID + "\n");
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
            sbReturn.Append(TAG_CONTENT_URL + ":  " + ContentUrl + "\n");
            sbReturn.Append(TAG_CONTENT_DATA + ":  " + ContentData + "\n");
            sbReturn.Append(TAG_CONTENT_META + ":  " + ContentMeta + "\n");
            sbReturn.Append(TAG_IS_SUBMITTED + ":  " + IsSubmitted + "\n");
            sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
            sbReturn.Append(TAG_IS_DRAFT + ":  " + IsDraft + "\n");
            if (!dtNull.Equals(DateSubmitted))
            {
                sbReturn.Append(TAG_DATE_SUBMITTED + ":  " + DateSubmitted.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_SUBMITTED + ":\n");
            }
            sbReturn.Append(TAG_NOTES + ":  " + Notes + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of Content</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<Content>\n");
            sbReturn.Append("<" + TAG_ID + ">" + ContentID + "</" + TAG_ID + ">\n");
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_TYPE_ID + ">" + ContentTypeID + "</" + TAG_CONTENT_TYPE_ID + ">\n");
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
            sbReturn.Append("<" + TAG_CONTENT_URL + ">" + ContentUrl + "</" + TAG_CONTENT_URL + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_DATA + ">" + ContentData + "</" + TAG_CONTENT_DATA + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_META + ">" + ContentMeta + "</" + TAG_CONTENT_META + ">\n");
            sbReturn.Append("<" + TAG_IS_SUBMITTED + ">" + IsSubmitted + "</" + TAG_IS_SUBMITTED + ">\n");
            sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
            sbReturn.Append("<" + TAG_IS_DRAFT + ">" + IsDraft + "</" + TAG_IS_DRAFT + ">\n");
            if (!dtNull.Equals(DateSubmitted))
            {
                sbReturn.Append("<" + TAG_DATE_SUBMITTED + ">" + DateSubmitted.ToString() + "</" + TAG_DATE_SUBMITTED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_DATE_SUBMITTED + "></" + TAG_DATE_SUBMITTED + ">\n");
            }
            sbReturn.Append("<" + TAG_NOTES + ">" + Notes + "</" + TAG_NOTES + ">\n");
            sbReturn.Append("</Content>" + "\n");

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
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_TYPE_ID);
                ContentTypeID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                ContentTypeID = 0;
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
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_URL);
                ContentUrl = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }
            //Cannot reliably convert byte[] to string.

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_META);
                ContentMeta = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_IS_SUBMITTED);
                IsSubmitted = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                IsSubmitted = false;
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
                xResultNode = xNode.SelectSingleNode(TAG_IS_DRAFT);
                IsDraft = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                IsDraft = false;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_DATE_SUBMITTED);
                DateSubmitted = DateTime.Parse(xResultNode.InnerText);
            }
            catch
            {
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_NOTES);
                Notes = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }
        }
        /// <summary>Calls sqlLoad() method which gets record from database with content_id equal to the current object's ContentID </summary>
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
        /// <summary>Calls sqlUpdate() method which record record from database with current object values where content_id equal to the current object's ContentID </summary>
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
        /// <summary>Calls sqlDelete() method which delete's the record from database where where content_id equal to the current object's ContentID </summary>
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

                Console.WriteLine(Content.TAG_USER_ID + ":  ");
                UserID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(Content.TAG_CONTENT_TYPE_ID + ":  ");
                ContentTypeID = (long)Convert.ToInt32(Console.ReadLine());
                try
                {
                    Console.WriteLine(Content.TAG_DATE_CREATED + ":  ");
                    DateCreated = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateCreated = new DateTime();
                }
                try
                {
                    Console.WriteLine(Content.TAG_DATE_MODIFIED + ":  ");
                    DateModified = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateModified = new DateTime();
                }

                Console.WriteLine(Content.TAG_CONTENT_URL + ":  ");
                ContentUrl = Console.ReadLine();
                //Cannot reliably convert byte[] to string.

                Console.WriteLine(Content.TAG_CONTENT_META + ":  ");
                ContentMeta = Console.ReadLine();

                Console.WriteLine(Content.TAG_IS_SUBMITTED + ":  ");
                IsSubmitted = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine(Content.TAG_IS_DISABLED + ":  ");
                IsDisabled = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine(Content.TAG_IS_DRAFT + ":  ");
                IsDraft = Convert.ToBoolean(Console.ReadLine());
                try
                {
                    Console.WriteLine(Content.TAG_DATE_SUBMITTED + ":  ");
                    DateSubmitted = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateSubmitted = new DateTime();
                }

                Console.WriteLine(Content.TAG_NOTES + ":  ");
                Notes = Console.ReadLine();

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
            SqlParameter paramUserID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramDateCreated = null;
            SqlParameter paramContentUrl = null;
            SqlParameter paramContentData = null;
            SqlParameter paramContentMeta = null;
            SqlParameter paramIsSubmitted = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramIsDraft = null;
            SqlParameter paramDateSubmitted = null;
            SqlParameter paramNotes = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
            paramDateCreated.DbType = DbType.DateTime;
            paramDateCreated.Direction = ParameterDirection.Input;


            paramContentUrl = new SqlParameter("@" + TAG_CONTENT_URL, ContentUrl);
            paramContentUrl.DbType = DbType.String;
            paramContentUrl.Size = 255;
            paramContentUrl.Direction = ParameterDirection.Input;

            paramContentData = new SqlParameter("@" + TAG_CONTENT_DATA, ContentData);
            paramContentData.DbType = DbType.Binary;
            paramContentData.Size = 2147483647;
            paramContentData.Direction = ParameterDirection.Input;

            paramContentMeta = new SqlParameter("@" + TAG_CONTENT_META, ContentMeta);
            paramContentMeta.DbType = DbType.String;
            paramContentMeta.Direction = ParameterDirection.Input;

            paramIsSubmitted = new SqlParameter("@" + TAG_IS_SUBMITTED, IsSubmitted);
            paramIsSubmitted.DbType = DbType.Boolean;
            paramIsSubmitted.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramIsDraft = new SqlParameter("@" + TAG_IS_DRAFT, IsDraft);
            paramIsDraft.DbType = DbType.Boolean;
            paramIsDraft.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(DateSubmitted))
            {
                paramDateSubmitted = new SqlParameter("@" + TAG_DATE_SUBMITTED, DateSubmitted);
            }
            else
            {
                paramDateSubmitted = new SqlParameter("@" + TAG_DATE_SUBMITTED, DBNull.Value);
            }
            paramDateSubmitted.DbType = DbType.DateTime;
            paramDateSubmitted.Direction = ParameterDirection.Input;

            paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
            paramNotes.DbType = DbType.String;
            paramNotes.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramContentUrl);
            cmd.Parameters.Add(paramContentData);
            cmd.Parameters.Add(paramContentMeta);
            cmd.Parameters.Add(paramIsSubmitted);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramIsDraft);
            cmd.Parameters.Add(paramDateSubmitted);
            cmd.Parameters.Add(paramNotes);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            ContentID = long.Parse(strTmp);

            // cleanup to help GC
            paramUserID = null;
            paramContentTypeID = null;
            paramDateCreated = null;
            paramContentUrl = null;
            paramContentData = null;
            paramContentMeta = null;
            paramIsSubmitted = null;
            paramIsDisabled = null;
            paramIsDraft = null;
            paramDateSubmitted = null;
            paramNotes = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Check to see if the row exists in database</summary>
        protected bool sqlExist(SqlConnection conn)
        {
            bool bExist = false;

            SqlCommand cmd = null;
            SqlParameter paramContentID = null;
            SqlParameter paramCount = null;

            cmd = new SqlCommand(SP_EXIST_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            paramContentID = new SqlParameter("@" + TAG_ID, ContentID);
            paramContentID.Direction = ParameterDirection.Input;
            paramContentID.DbType = DbType.Int32;

            paramCount = new SqlParameter();
            paramCount.ParameterName = "@COUNT";
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(paramContentID);
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
            paramContentID = null;
            paramCount = null;
            cmd = null;

            return bExist;
        }
        /// <summary>Updates row of data in database</summary>
        protected void sqlUpdate(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentID = null;
            SqlParameter paramUserID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramDateModified = null;
            SqlParameter paramContentUrl = null;
            SqlParameter paramContentData = null;
            SqlParameter paramContentMeta = null;
            SqlParameter paramIsSubmitted = null;
            SqlParameter paramIsDisabled = null;
            SqlParameter paramIsDraft = null;
            SqlParameter paramDateSubmitted = null;
            SqlParameter paramNotes = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_UPDATE_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramContentID = new SqlParameter("@" + TAG_ID, ContentID);
            paramContentID.DbType = DbType.Int32;
            paramContentID.Direction = ParameterDirection.Input;


            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;


            paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
            paramDateModified.DbType = DbType.DateTime;
            paramDateModified.Direction = ParameterDirection.Input;

            paramContentUrl = new SqlParameter("@" + TAG_CONTENT_URL, ContentUrl);
            paramContentUrl.DbType = DbType.String;
            paramContentUrl.Size = 255;
            paramContentUrl.Direction = ParameterDirection.Input;

            paramContentData = new SqlParameter("@" + TAG_CONTENT_DATA, ContentData);
            paramContentData.DbType = DbType.Binary;
            paramContentData.Size = 2147483647;
            paramContentData.Direction = ParameterDirection.Input;

            paramContentMeta = new SqlParameter("@" + TAG_CONTENT_META, ContentMeta);
            paramContentMeta.DbType = DbType.String;
            paramContentMeta.Direction = ParameterDirection.Input;

            paramIsSubmitted = new SqlParameter("@" + TAG_IS_SUBMITTED, IsSubmitted);
            paramIsSubmitted.DbType = DbType.Boolean;
            paramIsSubmitted.Direction = ParameterDirection.Input;

            paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
            paramIsDisabled.DbType = DbType.Boolean;
            paramIsDisabled.Direction = ParameterDirection.Input;

            paramIsDraft = new SqlParameter("@" + TAG_IS_DRAFT, IsDraft);
            paramIsDraft.DbType = DbType.Boolean;
            paramIsDraft.Direction = ParameterDirection.Input;

            if (!dtNull.Equals(DateSubmitted))
            {
                paramDateSubmitted = new SqlParameter("@" + TAG_DATE_SUBMITTED, DateSubmitted);
            }
            else
            {
                paramDateSubmitted = new SqlParameter("@" + TAG_DATE_SUBMITTED, DBNull.Value);
            }
            paramDateSubmitted.DbType = DbType.DateTime;
            paramDateSubmitted.Direction = ParameterDirection.Input;

            paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
            paramNotes.DbType = DbType.String;
            paramNotes.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramContentID);
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramDateModified);
            cmd.Parameters.Add(paramContentUrl);
            cmd.Parameters.Add(paramContentData);
            cmd.Parameters.Add(paramContentMeta);
            cmd.Parameters.Add(paramIsSubmitted);
            cmd.Parameters.Add(paramIsDisabled);
            cmd.Parameters.Add(paramIsDraft);
            cmd.Parameters.Add(paramDateSubmitted);
            cmd.Parameters.Add(paramNotes);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            ContentID = long.Parse(s);

            // cleanup
            paramContentID = null;
            paramUserID = null;
            paramContentTypeID = null;
            paramDateModified = null;
            paramContentUrl = null;
            paramContentData = null;
            paramContentMeta = null;
            paramIsSubmitted = null;
            paramIsDisabled = null;
            paramIsDraft = null;
            paramDateSubmitted = null;
            paramNotes = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Deletes row of data in database</summary>
        protected void sqlDelete(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentID = null;

            cmd = new SqlCommand(SP_DELETE_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramContentID = new SqlParameter("@" + TAG_ID, ContentID);
            paramContentID.DbType = DbType.Int32;
            paramContentID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramContentID);
            cmd.ExecuteNonQuery();

            // cleanup to help GC
            paramContentID = null;
            cmd = null;

        }
        /// <summary>Load row of data from database</summary>
        protected void sqlLoad(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentID = null;
            SqlDataReader rdr = null;

            cmd = new SqlCommand(SP_LOAD_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramContentID = new SqlParameter("@" + TAG_ID, ContentID);
            paramContentID.DbType = DbType.Int32;
            paramContentID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramContentID);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sqlParseResultSet(rdr);
            }
            // cleanup
            rdr.Dispose();
            rdr = null;
            paramContentID = null;
            cmd = null;
        }
        /// <summary>Parse result set</summary>
        protected void sqlParseResultSet(SqlDataReader rdr)
        {
            this.ContentID = long.Parse(rdr[DB_FIELD_ID].ToString());
            try
            {
                this.UserID = Convert.ToInt32(rdr[DB_FIELD_USER_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.ContentTypeID = Convert.ToInt32(rdr[DB_FIELD_CONTENT_TYPE_ID].ToString().Trim());
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
                this.ContentUrl = rdr[DB_FIELD_CONTENT_URL].ToString().Trim();
            }
            catch { }
            try
            {
                if (rdr[rdr.GetOrdinal(DB_FIELD_CONTENT_DATA)] != DBNull.Value)
                {
                    this.ContentData = (byte[])rdr[rdr.GetOrdinal(DB_FIELD_CONTENT_DATA)];
                }
            }
            catch { }
            try
            {
                this.ContentMeta = rdr[DB_FIELD_CONTENT_META].ToString().Trim();
            }
            catch { }
            try
            {
                this.IsSubmitted = Convert.ToBoolean(rdr[DB_FIELD_IS_SUBMITTED].ToString().Trim());
            }
            catch { }
            try
            {
                this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
            }
            catch { }
            try
            {
                this.IsDraft = Convert.ToBoolean(rdr[DB_FIELD_IS_DRAFT].ToString().Trim());
            }
            catch { }
            try
            {
                this.DateSubmitted = DateTime.Parse(rdr[DB_FIELD_DATE_SUBMITTED].ToString());
            }
            catch
            {
            }
            try
            {
                this.Notes = rdr[DB_FIELD_NOTES].ToString().Trim();
            }
            catch { }
        }

    }
}

//END OF Content CLASS FILE