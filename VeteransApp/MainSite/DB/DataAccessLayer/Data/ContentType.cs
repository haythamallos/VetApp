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
    /// File:  ContentType.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/16/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the ContentType database table.
    /// </summary>
    public class ContentType
    {
        //Attributes
        /// <summary>ContentTypeID Attribute type String</summary>
        private long _lContentTypeID = 0;
        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;
        /// <summary>Code Attribute type String</summary>
        private string _strCode = null;
        /// <summary>Description Attribute type String</summary>
        private string _strDescription = null;
        /// <summary>VisibleCode Attribute type String</summary>
        private string _strVisibleCode = null;
        /// <summary>MaxRating Attribute type String</summary>
        private long _lMaxRating = 0;
        /// <summary>HasSides Attribute type String</summary>
        private bool? _bHasSides = null;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class ContentType and is of type bool</summary>
        public static readonly string ENTITY_NAME = "ContentType"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "content_type_id"; //Table id field name
                                                                       /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
                                                                              /// <summary>code Database field </summary>
        public static readonly string DB_FIELD_CODE = "code"; //Table Code field name
                                                              /// <summary>description Database field </summary>
        public static readonly string DB_FIELD_DESCRIPTION = "description"; //Table Description field name
                                                                            /// <summary>visible_code Database field </summary>
        public static readonly string DB_FIELD_VISIBLE_CODE = "visible_code"; //Table VisibleCode field name
                                                                              /// <summary>max_rating Database field </summary>
        public static readonly string DB_FIELD_MAX_RATING = "max_rating"; //Table MaxRating field name
                                                                          /// <summary>has_sides Database field </summary>
        public static readonly string DB_FIELD_HAS_SIDES = "has_sides"; //Table HasSides field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "ContentTypeID"; //Attribute id  name
                                                                /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
                                                                        /// <summary>Code Attribute type string</summary>
        public static readonly string TAG_CODE = "Code"; //Table Code field name
                                                         /// <summary>Description Attribute type string</summary>
        public static readonly string TAG_DESCRIPTION = "Description"; //Table Description field name
                                                                       /// <summary>VisibleCode Attribute type string</summary>
        public static readonly string TAG_VISIBLE_CODE = "VisibleCode"; //Table VisibleCode field name
                                                                        /// <summary>MaxRating Attribute type string</summary>
        public static readonly string TAG_MAX_RATING = "MaxRating"; //Table MaxRating field name
                                                                    /// <summary>HasSides Attribute type string</summary>
        public static readonly string TAG_HAS_SIDES = "HasSides"; //Table HasSides field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spContentTypeInsert"; //Insert sp name
        private static readonly string SP_UPDATE_NAME = "spContentTypeUpdate"; //Update sp name
        private static readonly string SP_DELETE_NAME = "spContentTypeDelete"; //Delete sp name
        private static readonly string SP_LOAD_NAME = "spContentTypeLoad"; //Load sp name
        private static readonly string SP_EXIST_NAME = "spContentTypeExist"; //Exist sp name

        //properties
        /// <summary>ContentTypeID is a Property in the ContentType Class of type long</summary>
        public long ContentTypeID
        {
            get { return _lContentTypeID; }
            set { _lContentTypeID = value; }
        }
        /// <summary>DateCreated is a Property in the ContentType Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }
        /// <summary>Code is a Property in the ContentType Class of type String</summary>
        public string Code
        {
            get { return _strCode; }
            set { _strCode = value; }
        }
        /// <summary>Description is a Property in the ContentType Class of type String</summary>
        public string Description
        {
            get { return _strDescription; }
            set { _strDescription = value; }
        }
        /// <summary>VisibleCode is a Property in the ContentType Class of type String</summary>
        public string VisibleCode
        {
            get { return _strVisibleCode; }
            set { _strVisibleCode = value; }
        }
        /// <summary>MaxRating is a Property in the ContentType Class of type long</summary>
        public long MaxRating
        {
            get { return _lMaxRating; }
            set { _lMaxRating = value; }
        }
        /// <summary>HasSides is a Property in the ContentType Class of type bool</summary>
        public bool? HasSides
        {
            get { return _bHasSides; }
            set { _bHasSides = value; }
        }


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>HasError Property in class ContentType and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Error Property in class ContentType and is of type ErrorCode</summary>
        public ErrorCode Error
        {
            get { return _errorCode; }
        }

        //Constructors
        /// <summary>ContentType empty constructor</summary>
        public ContentType()
        {
        }
        /// <summary>ContentType constructor takes ContentTypeID and a SqlConnection</summary>
        public ContentType(long l, SqlConnection conn)
        {
            ContentTypeID = l;
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
        /// <summary>ContentType Constructor takes pStrData and Config</summary>
        public ContentType(string pStrData)
        {
            Parse(pStrData);
        }
        /// <summary>ContentType Constructor takes SqlDataReader</summary>
        public ContentType(SqlDataReader rd)
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
        /// <summary>ToString is overridden to display all properties of the ContentType Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + ContentTypeID.ToString() + "\n");
            if (!dtNull.Equals(DateCreated))
            {
                sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
            }
            else
            {
                sbReturn.Append(TAG_DATE_CREATED + ":\n");
            }
            sbReturn.Append(TAG_CODE + ":  " + Code + "\n");
            sbReturn.Append(TAG_DESCRIPTION + ":  " + Description + "\n");
            sbReturn.Append(TAG_VISIBLE_CODE + ":  " + VisibleCode + "\n");
            sbReturn.Append(TAG_MAX_RATING + ":  " + MaxRating + "\n");
            sbReturn.Append(TAG_HAS_SIDES + ":  " + HasSides + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of ContentType</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<ContentType>\n");
            sbReturn.Append("<" + TAG_ID + ">" + ContentTypeID + "</" + TAG_ID + ">\n");
            if (!dtNull.Equals(DateCreated))
            {
                sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
            }
            else
            {
                sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
            }
            sbReturn.Append("<" + TAG_CODE + ">" + Code + "</" + TAG_CODE + ">\n");
            sbReturn.Append("<" + TAG_DESCRIPTION + ">" + Description + "</" + TAG_DESCRIPTION + ">\n");
            sbReturn.Append("<" + TAG_VISIBLE_CODE + ">" + VisibleCode + "</" + TAG_VISIBLE_CODE + ">\n");
            sbReturn.Append("<" + TAG_MAX_RATING + ">" + MaxRating + "</" + TAG_MAX_RATING + ">\n");
            sbReturn.Append("<" + TAG_HAS_SIDES + ">" + HasSides + "</" + TAG_HAS_SIDES + ">\n");
            sbReturn.Append("</ContentType>" + "\n");

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
                ContentTypeID = (long)Convert.ToInt32(strTmp);
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
                xResultNode = xNode.SelectSingleNode(TAG_CODE);
                Code = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_DESCRIPTION);
                Description = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_VISIBLE_CODE);
                VisibleCode = xResultNode.InnerText;
            }
            catch
            {
                xResultNode = null;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_MAX_RATING);
                MaxRating = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                MaxRating = 0;
            }

            try
            {
                xResultNode = xNode.SelectSingleNode(TAG_HAS_SIDES);
                HasSides = Convert.ToBoolean(xResultNode.InnerText);
            }
            catch
            {
                HasSides = false;
            }
        }
        /// <summary>Calls sqlLoad() method which gets record from database with content_type_id equal to the current object's ContentTypeID </summary>
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
        /// <summary>Calls sqlUpdate() method which record record from database with current object values where content_type_id equal to the current object's ContentTypeID </summary>
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
        /// <summary>Calls sqlDelete() method which delete's the record from database where where content_type_id equal to the current object's ContentTypeID </summary>
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
                {
                    Console.WriteLine(TAG_ID + ":  ");
                    try
                    {
                        ContentTypeID = long.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        ContentTypeID = 0;
                    }
                }
                try
                {
                    Console.WriteLine(ContentType.TAG_DATE_CREATED + ":  ");
                    DateCreated = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateCreated = new DateTime();
                }

                Console.WriteLine(ContentType.TAG_CODE + ":  ");
                Code = Console.ReadLine();

                Console.WriteLine(ContentType.TAG_DESCRIPTION + ":  ");
                Description = Console.ReadLine();

                Console.WriteLine(ContentType.TAG_VISIBLE_CODE + ":  ");
                VisibleCode = Console.ReadLine();

                Console.WriteLine(ContentType.TAG_MAX_RATING + ":  ");
                MaxRating = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(ContentType.TAG_HAS_SIDES + ":  ");
                HasSides = Convert.ToBoolean(Console.ReadLine());

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
            SqlParameter paramContentTypeID = null;
            SqlParameter paramDateCreated = null;
            SqlParameter paramCode = null;
            SqlParameter paramDescription = null;
            SqlParameter paramVisibleCode = null;
            SqlParameter paramMaxRating = null;
            SqlParameter paramHasSides = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_INSERT_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            paramContentTypeID = new SqlParameter("@" + TAG_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
            paramDateCreated.DbType = DbType.DateTime;
            paramDateCreated.Direction = ParameterDirection.Input;

            paramCode = new SqlParameter("@" + TAG_CODE, Code);
            paramCode.DbType = DbType.String;
            paramCode.Size = 255;
            paramCode.Direction = ParameterDirection.Input;

            paramDescription = new SqlParameter("@" + TAG_DESCRIPTION, Description);
            paramDescription.DbType = DbType.String;
            paramDescription.Size = 255;
            paramDescription.Direction = ParameterDirection.Input;

            paramVisibleCode = new SqlParameter("@" + TAG_VISIBLE_CODE, VisibleCode);
            paramVisibleCode.DbType = DbType.String;
            paramVisibleCode.Size = 255;
            paramVisibleCode.Direction = ParameterDirection.Input;

            paramMaxRating = new SqlParameter("@" + TAG_MAX_RATING, MaxRating);
            paramMaxRating.DbType = DbType.Int32;
            paramMaxRating.Direction = ParameterDirection.Input;

            paramHasSides = new SqlParameter("@" + TAG_HAS_SIDES, HasSides);
            paramHasSides.DbType = DbType.Boolean;
            paramHasSides.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramCode);
            cmd.Parameters.Add(paramDescription);
            cmd.Parameters.Add(paramVisibleCode);
            cmd.Parameters.Add(paramMaxRating);
            cmd.Parameters.Add(paramHasSides);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            ContentTypeID = long.Parse(strTmp);

            // cleanup to help GC
            paramContentTypeID = null;
            paramDateCreated = null;
            paramCode = null;
            paramDescription = null;
            paramVisibleCode = null;
            paramMaxRating = null;
            paramHasSides = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Check to see if the row exists in database</summary>
        protected bool sqlExist(SqlConnection conn)
        {
            bool bExist = false;

            SqlCommand cmd = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramCount = null;

            cmd = new SqlCommand(SP_EXIST_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            paramContentTypeID = new SqlParameter("@" + TAG_ID, ContentTypeID);
            paramContentTypeID.Direction = ParameterDirection.Input;
            paramContentTypeID.DbType = DbType.Int32;

            paramCount = new SqlParameter();
            paramCount.ParameterName = "@COUNT";
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(paramContentTypeID);
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
            paramContentTypeID = null;
            paramCount = null;
            cmd = null;

            return bExist;
        }
        /// <summary>Updates row of data in database</summary>
        protected void sqlUpdate(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramCode = null;
            SqlParameter paramDescription = null;
            SqlParameter paramVisibleCode = null;
            SqlParameter paramMaxRating = null;
            SqlParameter paramHasSides = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_UPDATE_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramContentTypeID = new SqlParameter("@" + TAG_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;



            paramCode = new SqlParameter("@" + TAG_CODE, Code);
            paramCode.DbType = DbType.String;
            paramCode.Size = 255;
            paramCode.Direction = ParameterDirection.Input;

            paramDescription = new SqlParameter("@" + TAG_DESCRIPTION, Description);
            paramDescription.DbType = DbType.String;
            paramDescription.Size = 255;
            paramDescription.Direction = ParameterDirection.Input;

            paramVisibleCode = new SqlParameter("@" + TAG_VISIBLE_CODE, VisibleCode);
            paramVisibleCode.DbType = DbType.String;
            paramVisibleCode.Size = 255;
            paramVisibleCode.Direction = ParameterDirection.Input;

            paramMaxRating = new SqlParameter("@" + TAG_MAX_RATING, MaxRating);
            paramMaxRating.DbType = DbType.Int32;
            paramMaxRating.Direction = ParameterDirection.Input;

            paramHasSides = new SqlParameter("@" + TAG_HAS_SIDES, HasSides);
            paramHasSides.DbType = DbType.Boolean;
            paramHasSides.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramCode);
            cmd.Parameters.Add(paramDescription);
            cmd.Parameters.Add(paramVisibleCode);
            cmd.Parameters.Add(paramMaxRating);
            cmd.Parameters.Add(paramHasSides);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            ContentTypeID = long.Parse(s);

            // cleanup
            paramContentTypeID = null;
            paramCode = null;
            paramDescription = null;
            paramVisibleCode = null;
            paramMaxRating = null;
            paramHasSides = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Deletes row of data in database</summary>
        protected void sqlDelete(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentTypeID = null;

            cmd = new SqlCommand(SP_DELETE_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramContentTypeID = new SqlParameter("@" + TAG_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramContentTypeID);
            cmd.ExecuteNonQuery();

            // cleanup to help GC
            paramContentTypeID = null;
            cmd = null;

        }
        /// <summary>Load row of data from database</summary>
        protected void sqlLoad(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramContentTypeID = null;
            SqlDataReader rdr = null;

            cmd = new SqlCommand(SP_LOAD_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramContentTypeID = new SqlParameter("@" + TAG_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramContentTypeID);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sqlParseResultSet(rdr);
            }
            // cleanup
            rdr.Dispose();
            rdr = null;
            paramContentTypeID = null;
            cmd = null;
        }
        /// <summary>Parse result set</summary>
        protected void sqlParseResultSet(SqlDataReader rdr)
        {
            this.ContentTypeID = long.Parse(rdr[DB_FIELD_ID].ToString());
            try
            {
                this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
            }
            catch
            {
            }
            try
            {
                this.Code = rdr[DB_FIELD_CODE].ToString().Trim();
            }
            catch { }
            try
            {
                this.Description = rdr[DB_FIELD_DESCRIPTION].ToString().Trim();
            }
            catch { }
            try
            {
                this.VisibleCode = rdr[DB_FIELD_VISIBLE_CODE].ToString().Trim();
            }
            catch { }
            try
            {
                this.MaxRating = Convert.ToInt32(rdr[DB_FIELD_MAX_RATING].ToString().Trim());
            }
            catch { }
            try
            {
                this.HasSides = Convert.ToBoolean(rdr[DB_FIELD_HAS_SIDES].ToString().Trim());
            }
            catch { }
        }

    }
}

//END OF ContentType CLASS FILE