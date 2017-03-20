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
    /// File:  CartItem.cs
    /// History
    /// ----------------------------------------------------
    /// 001	HA	3/20/2017	Created
    /// 
    /// ----------------------------------------------------
    /// Abstracts the CartItem database table.
    /// </summary>
    public class CartItem
    {
        //Attributes
        /// <summary>CartItemID Attribute type String</summary>
        private long _lCartItemID = 0;
        /// <summary>DateCreated Attribute type String</summary>
        private DateTime _dtDateCreated = dtNull;
        /// <summary>DateModified Attribute type String</summary>
        private DateTime _dtDateModified = dtNull;
        /// <summary>PurchaseID Attribute type String</summary>
        private long _lPurchaseID = 0;
        /// <summary>UserID Attribute type String</summary>
        private long _lUserID = 0;
        /// <summary>ContentID Attribute type String</summary>
        private long _lContentID = 0;
        /// <summary>ContentTypeID Attribute type String</summary>
        private long _lContentTypeID = 0;

        private ErrorCode _errorCode = null;
        private bool _hasError = false;
        private static DateTime dtNull = new DateTime();

        /// <summary>HasError Property in class CartItem and is of type bool</summary>
        public static readonly string ENTITY_NAME = "CartItem"; //Table name to abstract

        // DB Field names
        /// <summary>ID Database field</summary>
        public static readonly string DB_FIELD_ID = "cart_item_id"; //Table id field name
                                                                    /// <summary>date_created Database field </summary>
        public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
                                                                              /// <summary>date_modified Database field </summary>
        public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
                                                                                /// <summary>purchase_id Database field </summary>
        public static readonly string DB_FIELD_PURCHASE_ID = "purchase_id"; //Table PurchaseID field name
                                                                            /// <summary>user_id Database field </summary>
        public static readonly string DB_FIELD_USER_ID = "user_id"; //Table UserID field name
                                                                    /// <summary>content_id Database field </summary>
        public static readonly string DB_FIELD_CONTENT_ID = "content_id"; //Table ContentID field name
                                                                          /// <summary>content_type_id Database field </summary>
        public static readonly string DB_FIELD_CONTENT_TYPE_ID = "content_type_id"; //Table ContentTypeID field name

        // Attribute variables
        /// <summary>TAG_ID Attribute type string</summary>
        public static readonly string TAG_ID = "CartItemID"; //Attribute id  name
                                                             /// <summary>DateCreated Attribute type string</summary>
        public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
                                                                        /// <summary>DateModified Attribute type string</summary>
        public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
                                                                          /// <summary>PurchaseID Attribute type string</summary>
        public static readonly string TAG_PURCHASE_ID = "PurchaseID"; //Table PurchaseID field name
                                                                      /// <summary>UserID Attribute type string</summary>
        public static readonly string TAG_USER_ID = "UserID"; //Table UserID field name
                                                              /// <summary>ContentID Attribute type string</summary>
        public static readonly string TAG_CONTENT_ID = "ContentID"; //Table ContentID field name
                                                                    /// <summary>ContentTypeID Attribute type string</summary>
        public static readonly string TAG_CONTENT_TYPE_ID = "ContentTypeID"; //Table ContentTypeID field name

        // Stored procedure names
        private static readonly string SP_INSERT_NAME = "spCartItemInsert"; //Insert sp name
        private static readonly string SP_UPDATE_NAME = "spCartItemUpdate"; //Update sp name
        private static readonly string SP_DELETE_NAME = "spCartItemDelete"; //Delete sp name
        private static readonly string SP_LOAD_NAME = "spCartItemLoad"; //Load sp name
        private static readonly string SP_EXIST_NAME = "spCartItemExist"; //Exist sp name

        //properties
        /// <summary>CartItemID is a Property in the CartItem Class of type long</summary>
        public long CartItemID
        {
            get { return _lCartItemID; }
            set { _lCartItemID = value; }
        }
        /// <summary>DateCreated is a Property in the CartItem Class of type DateTime</summary>
        public DateTime DateCreated
        {
            get { return _dtDateCreated; }
            set { _dtDateCreated = value; }
        }
        /// <summary>DateModified is a Property in the CartItem Class of type DateTime</summary>
        public DateTime DateModified
        {
            get { return _dtDateModified; }
            set { _dtDateModified = value; }
        }
        /// <summary>PurchaseID is a Property in the CartItem Class of type long</summary>
        public long PurchaseID
        {
            get { return _lPurchaseID; }
            set { _lPurchaseID = value; }
        }
        /// <summary>UserID is a Property in the CartItem Class of type long</summary>
        public long UserID
        {
            get { return _lUserID; }
            set { _lUserID = value; }
        }
        /// <summary>ContentID is a Property in the CartItem Class of type long</summary>
        public long ContentID
        {
            get { return _lContentID; }
            set { _lContentID = value; }
        }
        /// <summary>ContentTypeID is a Property in the CartItem Class of type long</summary>
        public long ContentTypeID
        {
            get { return _lContentTypeID; }
            set { _lContentTypeID = value; }
        }


        /*********************** CUSTOM NON-META BEGIN *********************/

        /*********************** CUSTOM NON-META END *********************/


        /// <summary>HasError Property in class CartItem and is of type bool</summary>
        public bool HasError
        {
            get { return _hasError; }
        }
        /// <summary>Error Property in class CartItem and is of type ErrorCode</summary>
        public ErrorCode Error
        {
            get { return _errorCode; }
        }

        //Constructors
        /// <summary>CartItem empty constructor</summary>
        public CartItem()
        {
        }
        /// <summary>CartItem constructor takes CartItemID and a SqlConnection</summary>
        public CartItem(long l, SqlConnection conn)
        {
            CartItemID = l;
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
        /// <summary>CartItem Constructor takes pStrData and Config</summary>
        public CartItem(string pStrData)
        {
            Parse(pStrData);
        }
        /// <summary>CartItem Constructor takes SqlDataReader</summary>
        public CartItem(SqlDataReader rd)
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
        /// <summary>ToString is overridden to display all properties of the CartItem Class</summary>
        public override string ToString()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append(TAG_ID + ":  " + CartItemID.ToString() + "\n");
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
            sbReturn.Append(TAG_PURCHASE_ID + ":  " + PurchaseID + "\n");
            sbReturn.Append(TAG_USER_ID + ":  " + UserID + "\n");
            sbReturn.Append(TAG_CONTENT_ID + ":  " + ContentID + "\n");
            sbReturn.Append(TAG_CONTENT_TYPE_ID + ":  " + ContentTypeID + "\n");

            return sbReturn.ToString();
        }
        /// <summary>Creates well formatted XML - includes all properties of CartItem</summary>
        public string ToXml()
        {
            StringBuilder sbReturn = null;

            sbReturn = new StringBuilder();
            sbReturn.Append("<CartItem>\n");
            sbReturn.Append("<" + TAG_ID + ">" + CartItemID + "</" + TAG_ID + ">\n");
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
            sbReturn.Append("<" + TAG_PURCHASE_ID + ">" + PurchaseID + "</" + TAG_PURCHASE_ID + ">\n");
            sbReturn.Append("<" + TAG_USER_ID + ">" + UserID + "</" + TAG_USER_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_ID + ">" + ContentID + "</" + TAG_CONTENT_ID + ">\n");
            sbReturn.Append("<" + TAG_CONTENT_TYPE_ID + ">" + ContentTypeID + "</" + TAG_CONTENT_TYPE_ID + ">\n");
            sbReturn.Append("</CartItem>" + "\n");

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
                CartItemID = (long)Convert.ToInt32(strTmp);
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
                xResultNode = xNode.SelectSingleNode(TAG_PURCHASE_ID);
                PurchaseID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                PurchaseID = 0;
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
                xResultNode = xNode.SelectSingleNode(TAG_CONTENT_ID);
                ContentID = (long)Convert.ToInt32(xResultNode.InnerText);
            }
            catch
            {
                ContentID = 0;
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
        }
        /// <summary>Calls sqlLoad() method which gets record from database with cart_item_id equal to the current object's CartItemID </summary>
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
        /// <summary>Calls sqlUpdate() method which record record from database with current object values where cart_item_id equal to the current object's CartItemID </summary>
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
        /// <summary>Calls sqlDelete() method which delete's the record from database where where cart_item_id equal to the current object's CartItemID </summary>
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
                    Console.WriteLine(CartItem.TAG_DATE_CREATED + ":  ");
                    DateCreated = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateCreated = new DateTime();
                }
                try
                {
                    Console.WriteLine(CartItem.TAG_DATE_MODIFIED + ":  ");
                    DateModified = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    DateModified = new DateTime();
                }

                Console.WriteLine(CartItem.TAG_PURCHASE_ID + ":  ");
                PurchaseID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(CartItem.TAG_USER_ID + ":  ");
                UserID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(CartItem.TAG_CONTENT_ID + ":  ");
                ContentID = (long)Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(CartItem.TAG_CONTENT_TYPE_ID + ":  ");
                ContentTypeID = (long)Convert.ToInt32(Console.ReadLine());

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
            SqlParameter paramPurchaseID = null;
            SqlParameter paramUserID = null;
            SqlParameter paramContentID = null;
            SqlParameter paramContentTypeID = null;
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


            paramPurchaseID = new SqlParameter("@" + TAG_PURCHASE_ID, PurchaseID);
            paramPurchaseID.DbType = DbType.Int32;
            paramPurchaseID.Direction = ParameterDirection.Input;

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramContentID = new SqlParameter("@" + TAG_CONTENT_ID, ContentID);
            paramContentID.DbType = DbType.Int32;
            paramContentID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramDateCreated);
            cmd.Parameters.Add(paramPurchaseID);
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramContentID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            // assign the primary kiey
            string strTmp;
            strTmp = cmd.Parameters["@PKID"].Value.ToString();
            CartItemID = long.Parse(strTmp);

            // cleanup to help GC
            paramDateCreated = null;
            paramPurchaseID = null;
            paramUserID = null;
            paramContentID = null;
            paramContentTypeID = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Check to see if the row exists in database</summary>
        protected bool sqlExist(SqlConnection conn)
        {
            bool bExist = false;

            SqlCommand cmd = null;
            SqlParameter paramCartItemID = null;
            SqlParameter paramCount = null;

            cmd = new SqlCommand(SP_EXIST_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            paramCartItemID = new SqlParameter("@" + TAG_ID, CartItemID);
            paramCartItemID.Direction = ParameterDirection.Input;
            paramCartItemID.DbType = DbType.Int32;

            paramCount = new SqlParameter();
            paramCount.ParameterName = "@COUNT";
            paramCount.DbType = DbType.Int32;
            paramCount.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(paramCartItemID);
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
            paramCartItemID = null;
            paramCount = null;
            cmd = null;

            return bExist;
        }
        /// <summary>Updates row of data in database</summary>
        protected void sqlUpdate(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramCartItemID = null;
            SqlParameter paramDateModified = null;
            SqlParameter paramPurchaseID = null;
            SqlParameter paramUserID = null;
            SqlParameter paramContentID = null;
            SqlParameter paramContentTypeID = null;
            SqlParameter paramPKID = null;

            //Create a command object identifying
            //the stored procedure	
            cmd = new SqlCommand(SP_UPDATE_NAME, conn);

            //Set the command object so it knows
            //to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters

            paramCartItemID = new SqlParameter("@" + TAG_ID, CartItemID);
            paramCartItemID.DbType = DbType.Int32;
            paramCartItemID.Direction = ParameterDirection.Input;



            paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
            paramDateModified.DbType = DbType.DateTime;
            paramDateModified.Direction = ParameterDirection.Input;

            paramPurchaseID = new SqlParameter("@" + TAG_PURCHASE_ID, PurchaseID);
            paramPurchaseID.DbType = DbType.Int32;
            paramPurchaseID.Direction = ParameterDirection.Input;

            paramUserID = new SqlParameter("@" + TAG_USER_ID, UserID);
            paramUserID.DbType = DbType.Int32;
            paramUserID.Direction = ParameterDirection.Input;

            paramContentID = new SqlParameter("@" + TAG_CONTENT_ID, ContentID);
            paramContentID.DbType = DbType.Int32;
            paramContentID.Direction = ParameterDirection.Input;

            paramContentTypeID = new SqlParameter("@" + TAG_CONTENT_TYPE_ID, ContentTypeID);
            paramContentTypeID.DbType = DbType.Int32;
            paramContentTypeID.Direction = ParameterDirection.Input;

            paramPKID = new SqlParameter();
            paramPKID.ParameterName = "@PKID";
            paramPKID.DbType = DbType.Int32;
            paramPKID.Direction = ParameterDirection.Output;

            //Add parameters to command, which
            //will be passed to the stored procedure
            cmd.Parameters.Add(paramCartItemID);
            cmd.Parameters.Add(paramDateModified);
            cmd.Parameters.Add(paramPurchaseID);
            cmd.Parameters.Add(paramUserID);
            cmd.Parameters.Add(paramContentID);
            cmd.Parameters.Add(paramContentTypeID);
            cmd.Parameters.Add(paramPKID);

            // execute the command
            cmd.ExecuteNonQuery();
            string s;
            s = cmd.Parameters["@PKID"].Value.ToString();
            CartItemID = long.Parse(s);

            // cleanup
            paramCartItemID = null;
            paramDateModified = null;
            paramPurchaseID = null;
            paramUserID = null;
            paramContentID = null;
            paramContentTypeID = null;
            paramPKID = null;
            cmd = null;
        }
        /// <summary>Deletes row of data in database</summary>
        protected void sqlDelete(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramCartItemID = null;

            cmd = new SqlCommand(SP_DELETE_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramCartItemID = new SqlParameter("@" + TAG_ID, CartItemID);
            paramCartItemID.DbType = DbType.Int32;
            paramCartItemID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramCartItemID);
            cmd.ExecuteNonQuery();

            // cleanup to help GC
            paramCartItemID = null;
            cmd = null;

        }
        /// <summary>Load row of data from database</summary>
        protected void sqlLoad(SqlConnection conn)
        {
            SqlCommand cmd = null;
            SqlParameter paramCartItemID = null;
            SqlDataReader rdr = null;

            cmd = new SqlCommand(SP_LOAD_NAME, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            paramCartItemID = new SqlParameter("@" + TAG_ID, CartItemID);
            paramCartItemID.DbType = DbType.Int32;
            paramCartItemID.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(paramCartItemID);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                sqlParseResultSet(rdr);
            }
            // cleanup
            rdr.Dispose();
            rdr = null;
            paramCartItemID = null;
            cmd = null;
        }
        /// <summary>Parse result set</summary>
        protected void sqlParseResultSet(SqlDataReader rdr)
        {
            this.CartItemID = long.Parse(rdr[DB_FIELD_ID].ToString());
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
                this.PurchaseID = Convert.ToInt32(rdr[DB_FIELD_PURCHASE_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.UserID = Convert.ToInt32(rdr[DB_FIELD_USER_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.ContentID = Convert.ToInt32(rdr[DB_FIELD_CONTENT_ID].ToString().Trim());
            }
            catch { }
            try
            {
                this.ContentTypeID = Convert.ToInt32(rdr[DB_FIELD_CONTENT_TYPE_ID].ToString().Trim());
            }
            catch { }
        }

    }
}

//END OF CartItem CLASS FILE