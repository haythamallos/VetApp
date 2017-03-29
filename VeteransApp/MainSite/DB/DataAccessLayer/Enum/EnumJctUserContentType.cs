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
    /// File:  EnumJctUserContentType.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/24/2017	Created
    /// 
    /// ----------------------------------------------------
    /// </summary>
    public class EnumJctUserContentType
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
        public static readonly string ENTITY_NAME = "EnumJctUserContentType"; //Table name to abstract
        private static DateTime dtNull = new DateTime();
        private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

        private long _lJctUserContentTypeID = 0;
        private DateTime _dtBeginDateCreated = new DateTime();
        private DateTime _dtEndDateCreated = new DateTime();
        private DateTime _dtBeginDateModified = new DateTime();
        private DateTime _dtEndDateModified = new DateTime();
        private long _lUserID = 0;
        private long _lSideID = 0;
        private long _lContentTypeID = 0;
        private long _lRating = 0;
        private long _lRatingLeft = 0;
        private long _lRatingRight = 0;
        //		private string _strOrderByEnum = "ASC";
        private string _strOrderByField = DB_FIELD_ID;

        /// <summary>DB_FIELD_ID Attribute type string</summary>
        public static readonly string DB_FIELD_ID = "jct_user_content_type_id"; //Table id field name
                                                                                /// <summary>JctUserContentTypeID Attribute type string</summary>
        public static readonly string TAG_JCT_USER_CONTENT_TYPE_ID = "JctUserContentTypeID"; //Attribute JctUserContentTypeID  name
                                                                                             /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
                                                                                   /// <summary>EndDateCreated Attribute type string</summary>
        public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
                                                                               /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
                                                                                     /// <summary>EndDateModified Attribute type string</summary>
        public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
                                                                                 /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Attribute UserID  name
                                                              /// <summary>SideID Attribute type string</summary>
        public static readonly string TAG_SIDE_ID = "SideID"; //Attribute SideID  name
                                                              /// <summary>ContentTypeID Attribute type string</summary>
        public static readonly string TAG_CONTENT_TYPE_ID = "ContentTypeID"; //Attribute ContentTypeID  name
                                                                             /// <summary>Rating Attribute type string</summary>
        public static readonly string TAG_RATING = "Rating"; //Attribute Rating  name
                                                             /// <summary>RatingLeft Attribute type string</summary>
        public static readonly string TAG_RATINGLEFT = "RatingLeft"; //Attribute RatingLeft  name
                                                                     /// <summary>RatingRight Attribute type string</summary>
        public static readonly string TAG_RATINGRIGHT = "RatingRight"; //Attribute RatingRight  name
                                                                       // Stored procedure name
        public string SP_ENUM_NAME = "spJctUserContentTypeEnum"; //Enum sp name

        /// <summary>HasError is a Property in the JctUserContentType Class of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        /// <summary>JctUserContentTypeID is a Property in the JctUserContentType Class of type long</summary>
        public long JctUserContentTypeID
        {
            get { return _lJctUserContentTypeID; }
            set { _lJctUserContentTypeID = value; }
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
        /// <summary>UserID is a Property in the JctUserContentType Class of type long</summary>
        public long UserID
        {
            get { return _lUserID; }
            set { _lUserID = value; }
        }
        /// <summary>SideID is a Property in the JctUserContentType Class of type long</summary>
        public long SideID
        {
            get { return _lSideID; }
            set { _lSideID = value; }
        }
        /// <summary>ContentTypeID is a Property in the JctUserContentType Class of type long</summary>
        public long ContentTypeID
        {
            get { return _lContentTypeID; }
            set { _lContentTypeID = value; }
        }
        /// <summary>Rating is a Property in the JctUserContentType Class of type long</summary>
        public long Rating
        {
            get { return _lRating; }
            set { _lRating = value; }
        }
        /// <summary>RatingLeft is a Property in the JctUserContentType Class of type long</summary>
        public long RatingLeft
        {
            get { return _lRatingLeft; }
            set { _lRatingLeft = value; }
        }
        /// <summary>RatingRight is a Property in the JctUserContentType Class of type long</summary>
        public long RatingRight
        {
            get { return _lRatingRight; }
            set { _lRatingRight = value; }
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
        public EnumJctUserContentType()
        {
        }
        /// <summary>Contructor takes 1 parameter: SqlConnection</summary>
        public EnumJctUserContentType(SqlConnection conn)
        {
            _conn = conn;
        }


        // Implementation of IEnumerator
        /// <summary>Property of type JctUserContentType. Returns the next JctUserContentType in the list</summary>
        private JctUserContentType _nextTransaction
        {
            get
            {
                JctUserContentType o = null;

                if (!_bSetup)
                {
                    EnumData();
                }
                if (_hasMore)
                {
                    o = new JctUserContentType(_rdr);
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

        /// <summary>ToString is overridden to display all properties of the JctUserContentType Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_JCT_USER_CONTENT_TYPE_ID + ":  " + JctUserContentTypeID.ToString() + "\n");
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
            sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
            sbReturn.Append(TAG_SIDE_ID + ":  " + SideID + "\n");
            sbReturn.Append(TAG_CONTENT_TYPE_ID + ":  " + ContentTypeID + "\n");
            sbReturn.Append(TAG_RATING + ":  " + Rating + "\n");
            sbReturn.Append(TAG_RATINGLEFT + ":  " + RatingLeft + "\n");
            sbReturn.Append(TAG_RATINGRIGHT + ":  " + RatingRight + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of JctUserContentType</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<" + ENTITY_NAME + ">\n");
            sbReturn.Append("<" + TAG_JCT_USER_CONTENT_TYPE_ID + ">" + JctUserContentTypeID + "</" + TAG_JCT_USER_CONTENT_TYPE_ID + ">\n");
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
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_SIDE_ID + ">" + SideID + "</" + TAG_SIDE_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_TYPE_ID + ">" + ContentTypeID + "</" + TAG_CONTENT_TYPE_ID + ">\n");
            sbReturn.Append("<" + TAG_RATING + ">" + Rating + "</" + TAG_RATING + ">\n");
            sbReturn.Append("<" + TAG_RATINGLEFT + ">" + RatingLeft + "</" + TAG_RATINGLEFT + ">\n");
            sbReturn.Append("<" + TAG_RATINGRIGHT + ">" + RatingRight + "</" + TAG_RATINGRIGHT + ">\n");
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
                xResultNode = xNode.SelectSingleNode(TAG_JCT_USER_CONTENT_TYPE_ID);
                strTmp = xResultNode.InnerText;
                JctUserContentTypeID = (long)Convert.ToInt32(strTmp);
            }
            catch
            {
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
                xResultNode = xNode.SelectSingleNode(TAG_USER_ID);
                UserID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                UserID = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_SIDE_ID);
                SideID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                SideID = 0;
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
                xResultNode = xNode.SelectSingleNode(TAG_RATING);
                Rating = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                Rating = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_RATINGLEFT);
                RatingLeft = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                RatingLeft = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_RATINGRIGHT);
                RatingRight = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                RatingRight = 0;
            }
        }
        /// <summary>Prompt for values</summary>
        public void Prompt()
        {
            try
            {
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

                Console.WriteLine(TAG_USER_ID + ":  ");
                try
                {
                    UserID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UserID = 0;
                }

                Console.WriteLine(TAG_SIDE_ID + ":  ");
                try
                {
                    SideID = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    SideID = 0;
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

                Console.WriteLine(TAG_RATING + ":  ");
                try
                {
                    Rating = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Rating = 0;
                }

                Console.WriteLine(TAG_RATINGLEFT + ":  ");
                try
                {
                    RatingLeft = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    RatingLeft = 0;
                }

                Console.WriteLine(TAG_RATINGRIGHT + ":  ");
                try
                {
                    RatingRight = (long)Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    RatingRight = 0;
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
            SqlParameter paramJctUserContentTypeID = null;
            SqlParameter paramBeginDateCreated = null;
            SqlParameter paramEndDateCreated = null;
            SqlParameter paramBeginDateModified = null;
            SqlParameter paramEndDateModified = null;
            SqlParameter paramUserID = null;
            SqlParameter paramSideID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramRating = null;
            SqlParameter paramRatingLeft = null;
            SqlParameter paramRatingRight = null;
            DateTime dtNull = new DateTime();

            sbLog = new System.Text.StringBuilder();
            paramJctUserContentTypeID = new SqlParameter("@" + TAG_JCT_USER_CONTENT_TYPE_ID, JctUserContentTypeID);
            sbLog.Append(TAG_JCT_USER_CONTENT_TYPE_ID + "=" + JctUserContentTypeID + "\n");
            paramJctUserContentTypeID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramJctUserContentTypeID);

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

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            sbLog.Append(TAG_USER_ID + "=" + UserID + "\n");
            paramUserID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramUserID);
            paramSideID = new SqlParameter("@" + TAG_SIDE_ID, SideID);
            sbLog.Append(TAG_SIDE_ID + "=" + SideID + "\n");
            paramSideID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramSideID);
            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            sbLog.Append(TAG_CONTENT_TYPE_ID + "=" + ContentTypeID + "\n");
            paramContentTypeID.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramContentTypeID);
            paramRating = new SqlParameter("@" + TAG_RATING, Rating);
            sbLog.Append(TAG_RATING + "=" + Rating + "\n");
            paramRating.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramRating);

            paramRatingLeft = new SqlParameter("@" + TAG_RATINGLEFT, RatingLeft);
            sbLog.Append(TAG_RATINGLEFT + "=" + RatingLeft + "\n");
            paramRatingLeft.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramRatingLeft);

            paramRatingRight = new SqlParameter("@" + TAG_RATINGRIGHT, RatingRight);
            sbLog.Append(TAG_RATINGRIGHT + "=" + RatingRight + "\n");
            paramRatingRight.Direction = ParameterDirection.Input;
            _cmd.Parameters.Add(paramRatingRight);

        }

    }
}
