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
    /// File:  JctUserContentType.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/23/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the JctUserContentType database table.
    /// </summary>
    public class JctUserContentType
    {
        //Attributes
        /// <summary>JctUserContentTypeID Attribute type String</summary>
        private long _lJctUserContentTypeID = 0;
        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;
        /// <summary>DateModified Attribute type String</summary>
        private DateTime _dtDateModified = dtNull;
        /// <summary>UserID Attribute type String</summary>
        private long _lUserID = 0;
        /// <summary>SideID Attribute type String</summary>
        private long _lSideID = 0;
        /// <summary>ContentTypeID Attribute type String</summary>
        private long _lContentTypeID = 0;
        /// <summary>Rating Attribute type String</summary>
        private long _lRating = 0;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class JctUserContentType and is of type bool</summary>
        public static readonly string ENTITY_NAME = "JctUserContentType"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "jct_user_content_type_id"; //Table id field name
                                                                                /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
                                                                              /// <summary>date_modified Database field </summary>
        public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
                                                                                /// <summary>user_id Database field </summary>
        public static readonly string DB_FIELD_USER_ID = "user_id"; //Table UserID field name
                                                                    /// <summary>side_id Database field </summary>
        public static readonly string DB_FIELD_SIDE_ID = "side_id"; //Table SideID field name
                                                                    /// <summary>content_type_id Database field </summary>
        public static readonly string DB_FIELD_CONTENT_TYPE_ID = "content_type_id"; //Table ContentTypeID field name
                                                                                    /// <summary>rating Database field </summary>
        public static readonly string DB_FIELD_RATING = "rating"; //Table Rating field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "JctUserContentTypeID"; //Attribute id  name
                                                                       /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
                                                                        /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
                                                                          /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Table UserID field name
                                                              /// <summary>SideID Attribute type string</summary>
        public static readonly string TAG_SIDE_ID = "SideID"; //Table SideID field name
                                                              /// <summary>ContentTypeID Attribute type string</summary>
        public static readonly string TAG_CONTENT_TYPE_ID = "ContentTypeID"; //Table ContentTypeID field name
                                                                             /// <summary>Rating Attribute type string</summary>
        public static readonly string TAG_RATING = "Rating"; //Table Rating field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spJctUserContentTypeInsert"; //Insert sp name
        private static readonly string SP_UPDATE_NAME = "spJctUserContentTypeUpdate"; //Update sp name
        private static readonly string SP_DELETE_NAME = "spJctUserContentTypeDelete"; //Delete sp name
        private static readonly string SP_LOAD_NAME = "spJctUserContentTypeLoad"; //Load sp name
        private static readonly string SP_EXIST_NAME = "spJctUserContentTypeExist"; //Exist sp name

        //properties
        /// <summary>JctUserContentTypeID is a Property in the JctUserContentType Class of type long</summary>
        public long JctUserContentTypeID
        {
            get { return _lJctUserContentTypeID; }
            set { _lJctUserContentTypeID = value; }
        }
        /// <summary>DateCreated is a Property in the JctUserContentType Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }
        /// <summary>DateModified is a Property in the JctUserContentType Class of type DateTime</summary>
        public DateTime DateModified
        {
            get { return _dtDateModified; }
            set { _dtDateModified = value; }
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


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>HasError Property in class JctUserContentType and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Error Property in class JctUserContentType and is of type ErrorCode</summary>
        public ErrorCode Error
        {
            get { return _errorCode; }
        }

        //Constructors
        /// <summary>JctUserContentType empty constructor</summary>
        public JctUserContentType()
        {
        }
        /// <summary>JctUserContentType constructor takes JctUserContentTypeID and a SqlConnection</summary>
        public JctUserContentType(long l, SqlConnection conn)
        {
            JctUserContentTypeID = l;
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
        /// <summary>JctUserContentType Constructor takes pStrData and Config</summary>
        public JctUserContentType(string pStrData)
        {
            Parse(pStrData);
        }
        /// <summary>JctUserContentType Constructor takes SqlDataReader</summary>
        public JctUserContentType(SqlDataReader rd)
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
        /// <summary>ToString is overridden to display all properties of the JctUserContentType Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + JctUserContentTypeID.ToString() + "\n");
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
            sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
            sbReturn.Append(TAG_SIDE_ID + ":  " + SideID + "\n");
            sbReturn.Append(TAG_CONTENT_TYPE_ID + ":  " + ContentTypeID + "\n");
            sbReturn.Append(TAG_RATING + ":  " + Rating + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of JctUserContentType</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<JctUserContentType>\n");
            sbReturn.Append("<" + TAG_ID + ">" + JctUserContentTypeID + "</" + TAG_ID + ">\n");
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
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_SIDE_ID + ">" + SideID + "</" + TAG_SIDE_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_TYPE_ID + ">" + ContentTypeID + "</" + TAG_CONTENT_TYPE_ID + ">\n");
            sbReturn.Append("<" + TAG_RATING + ">" + Rating + "</" + TAG_RATING + ">\n");
            sbReturn.Append("</JctUserContentType>" + "\n");

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
                JctUserContentTypeID = (long)Convert.ToInt32(strTmp);
            }
            catch
            {
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
        }
        /// <summary>Calls sqlLoad() method which gets record from database with jct_user_content_type_id equal to the current object's JctUserContentTypeID </summary>
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
        /// <summary>Calls sqlUpdate() method which record record from database with current object values where jct_user_content_type_id equal to the current object's JctUserContentTypeID </summary>
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
        /// <summary>Calls sqlDelete() method which delete's the record from database where where jct_user_content_type_id equal to the current object's JctUserContentTypeID </summary>
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
                try
                {
                    Console.WriteLine(JctUserContentType.TAG_DATE_CREATED + ":  ");
                    DateCreated = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateCreated = new DateTime();
                }
                try
                {
                    Console.WriteLine(JctUserContentType.TAG_DATE_MODIFIED + ":  ");
                    DateModified = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateModified = new DateTime();
                }

                Console.WriteLine(JctUserContentType.TAG_USER_ID + ":  ");
                UserID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(JctUserContentType.TAG_SIDE_ID + ":  ");
                SideID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(JctUserContentType.TAG_CONTENT_TYPE_ID + ":  ");
                ContentTypeID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(JctUserContentType.TAG_RATING + ":  ");
                Rating = (long)Convert.ToInt32(Console.ReadLine());

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
            SqlParameter paramDateCreated = null;
            SqlParameter paramUserID = null;
            SqlParameter paramSideID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramRating = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
            paramDateCreated.DbType = DbType.DateTime;
            paramDateCreated.Direction = ParameterDirection.Input;


            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramSideID = new SqlParameter("@" + TAG_SIDE_ID, SideID);
            paramSideID.DbType = DbType.Int32;
            paramSideID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramRating = new SqlParameter("@" + TAG_RATING, Rating);
            paramRating.DbType = DbType.Int32;
            paramRating.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramSideID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramRating);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            JctUserContentTypeID = long.Parse(strTmp);

            // cleanup to help GC
            paramDateCreated = null;
            paramUserID = null;
            paramSideID = null;
            paramContentTypeID = null;
            paramRating = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Check to see if the row exists in database</summary>
        protected bool sqlExist(SqlConnection conn)
        {
            bool bExist = false;

            SqlCommand cmd = null;
            SqlParameter paramJctUserContentTypeID = null;
            SqlParameter paramCount = null;

            cmd = new SqlCommand(SP_EXIST_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            paramJctUserContentTypeID = new SqlParameter("@" + TAG_ID, JctUserContentTypeID);
            paramJctUserContentTypeID.Direction = ParameterDirection.Input;
            paramJctUserContentTypeID.DbType = DbType.Int32;

            paramCount = new SqlParameter();
            paramCount.ParameterName = "@COUNT";
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(paramJctUserContentTypeID);
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
            paramJctUserContentTypeID = null;
            paramCount = null;
            cmd = null;

            return bExist;
        }
        /// <summary>Updates row of data in database</summary>
        protected void sqlUpdate(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramJctUserContentTypeID = null;
            SqlParameter paramDateModified = null;
            SqlParameter paramUserID = null;
            SqlParameter paramSideID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramRating = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_UPDATE_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramJctUserContentTypeID = new SqlParameter("@" + TAG_ID, JctUserContentTypeID);
            paramJctUserContentTypeID.DbType = DbType.Int32;
            paramJctUserContentTypeID.Direction = ParameterDirection.Input;



            paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
            paramDateModified.DbType = DbType.DateTime;
            paramDateModified.Direction = ParameterDirection.Input;

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramSideID = new SqlParameter("@" + TAG_SIDE_ID, SideID);
            paramSideID.DbType = DbType.Int32;
            paramSideID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramRating = new SqlParameter("@" + TAG_RATING, Rating);
            paramRating.DbType = DbType.Int32;
            paramRating.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramJctUserContentTypeID);
            cmd.Parameters.Add(paramDateModified);
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramSideID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramRating);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            JctUserContentTypeID = long.Parse(s);

            // cleanup
            paramJctUserContentTypeID = null;
            paramDateModified = null;
            paramUserID = null;
            paramSideID = null;
            paramContentTypeID = null;
            paramRating = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Deletes row of data in database</summary>
        protected void sqlDelete(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramJctUserContentTypeID = null;

            cmd = new SqlCommand(SP_DELETE_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramJctUserContentTypeID = new SqlParameter("@" + TAG_ID, JctUserContentTypeID);
            paramJctUserContentTypeID.DbType = DbType.Int32;
            paramJctUserContentTypeID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramJctUserContentTypeID);
            cmd.ExecuteNonQuery();

            // cleanup to help GC
            paramJctUserContentTypeID = null;
            cmd = null;

        }
        /// <summary>Load row of data from database</summary>
        protected void sqlLoad(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramJctUserContentTypeID = null;
            SqlDataReader rdr = null;

            cmd = new SqlCommand(SP_LOAD_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramJctUserContentTypeID = new SqlParameter("@" + TAG_ID, JctUserContentTypeID);
            paramJctUserContentTypeID.DbType = DbType.Int32;
            paramJctUserContentTypeID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramJctUserContentTypeID);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sqlParseResultSet(rdr);
            }
            // cleanup
            rdr.Dispose();
            rdr = null;
            paramJctUserContentTypeID = null;
            cmd = null;
        }
        /// <summary>Parse result set</summary>
        protected void sqlParseResultSet(SqlDataReader rdr)
        {
            this.JctUserContentTypeID = long.Parse(rdr[DB_FIELD_ID].ToString());
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
                this.UserID = Convert.ToInt32(rdr[DB_FIELD_USER_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.SideID = Convert.ToInt32(rdr[DB_FIELD_SIDE_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.ContentTypeID = Convert.ToInt32(rdr[DB_FIELD_CONTENT_TYPE_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.Rating = Convert.ToInt32(rdr[DB_FIELD_RATING].ToString().Trim());
            }
            catch { }
        }

    }
}

//END OF JctUserContentType CLASS FILE