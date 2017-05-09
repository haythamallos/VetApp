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
	/// File:  JctUserUser.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	5/9/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the JctUserUser database table.
	/// </summary>
	public class JctUserUser
	{
		//Attributes
		/// <summary>JctUserUserID Attribute type String</summary>
		private long _lJctUserUserID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateModified Attribute type String</summary>
		private DateTime _dtDateModified = dtNull;
		/// <summary>UserSourceID Attribute type String</summary>
		private long _lUserSourceID = 0;
		/// <summary>UserMemberID Attribute type String</summary>
		private long _lUserMemberID = 0;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class JctUserUser and is of type bool</summary>
		public static readonly string ENTITY_NAME = "JctUserUser"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "jct_user_user_id"; //Table id field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_modified Database field </summary>
		public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
		/// <summary>user_source_id Database field </summary>
		public static readonly string DB_FIELD_USER_SOURCE_ID = "user_source_id"; //Table UserSourceID field name
		/// <summary>user_member_id Database field </summary>
		public static readonly string DB_FIELD_USER_MEMBER_ID = "user_member_id"; //Table UserMemberID field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "JctUserUserID"; //Attribute id  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
		/// <summary>UserSourceID Attribute type string</summary>
		public static readonly string TAG_USER_SOURCE_ID = "UserSourceID"; //Table UserSourceID field name
		/// <summary>UserMemberID Attribute type string</summary>
		public static readonly string TAG_USER_MEMBER_ID = "UserMemberID"; //Table UserMemberID field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spJctUserUserInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spJctUserUserUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spJctUserUserDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spJctUserUserLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spJctUserUserExist"; //Exist sp name

		//properties
		/// <summary>JctUserUserID is a Property in the JctUserUser Class of type long</summary>
		public long JctUserUserID 
		{
			get{return _lJctUserUserID;}
			set{_lJctUserUserID = value;}
		}
		/// <summary>DateCreated is a Property in the JctUserUser Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the JctUserUser Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
		}
		/// <summary>UserSourceID is a Property in the JctUserUser Class of type long</summary>
		public long UserSourceID 
		{
			get{return _lUserSourceID;}
			set{_lUserSourceID = value;}
		}
		/// <summary>UserMemberID is a Property in the JctUserUser Class of type long</summary>
		public long UserMemberID 
		{
			get{return _lUserMemberID;}
			set{_lUserMemberID = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class JctUserUser and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class JctUserUser and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>JctUserUser empty constructor</summary>
		public JctUserUser()
		{
		}
		/// <summary>JctUserUser constructor takes JctUserUserID and a SqlConnection</summary>
		public JctUserUser(long l, SqlConnection conn) 
		{
			JctUserUserID = l;
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
		/// <summary>JctUserUser Constructor takes pStrData and Config</summary>
		public JctUserUser(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>JctUserUser Constructor takes SqlDataReader</summary>
		public JctUserUser(SqlDataReader rd)
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
			if (! disposing)
				return; // we're being collected, so let the GC take care of this object
		}

		// public methods
		/// <summary>ToString is overridden to display all properties of the JctUserUser Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + JctUserUserID.ToString() + "\n");
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
			sbReturn.Append(TAG_USER_SOURCE_ID + ":  " + UserSourceID + "\n");
			sbReturn.Append(TAG_USER_MEMBER_ID + ":  " + UserMemberID + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of JctUserUser</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<JctUserUser>\n");
			sbReturn.Append("<" + TAG_ID + ">" + JctUserUserID + "</" + TAG_ID + ">\n");
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
			sbReturn.Append("<" + TAG_USER_SOURCE_ID + ">" + UserSourceID + "</" + TAG_USER_SOURCE_ID + ">\n");
			sbReturn.Append("<" + TAG_USER_MEMBER_ID + ">" + UserMemberID + "</" + TAG_USER_MEMBER_ID + ">\n");
			sbReturn.Append("</JctUserUser>" + "\n");

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
				JctUserUserID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_USER_SOURCE_ID);
				UserSourceID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserSourceID = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USER_MEMBER_ID);
				UserMemberID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserMemberID = 0;
			}
		}
		/// <summary>Calls sqlLoad() method which gets record from database with jct_user_user_id equal to the current object's JctUserUserID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where jct_user_user_id equal to the current object's JctUserUserID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where jct_user_user_id equal to the current object's JctUserUserID </summary>
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
					Console.WriteLine(JctUserUser.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(JctUserUser.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(JctUserUser.TAG_USER_SOURCE_ID + ":  ");
				UserSourceID = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(JctUserUser.TAG_USER_MEMBER_ID + ":  ");
				UserMemberID = (long)Convert.ToInt32(Console.ReadLine());

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
			SqlParameter paramUserSourceID = null;
			SqlParameter paramUserMemberID = null;
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


			paramUserSourceID = new SqlParameter("@" + TAG_USER_SOURCE_ID, UserSourceID);
			paramUserSourceID.DbType = DbType.Int32;
			paramUserSourceID.Direction = ParameterDirection.Input;

			paramUserMemberID = new SqlParameter("@" + TAG_USER_MEMBER_ID, UserMemberID);
			paramUserMemberID.DbType = DbType.Int32;
			paramUserMemberID.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramUserSourceID);
			cmd.Parameters.Add(paramUserMemberID);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			JctUserUserID = long.Parse(strTmp);

			// cleanup to help GC
			paramDateCreated = null;
			paramUserSourceID = null;
			paramUserMemberID = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramJctUserUserID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramJctUserUserID = new SqlParameter("@" + TAG_ID, JctUserUserID);
			paramJctUserUserID.Direction = ParameterDirection.Input;
			paramJctUserUserID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramJctUserUserID);
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
			paramJctUserUserID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramJctUserUserID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramUserSourceID = null;
			SqlParameter paramUserMemberID = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramJctUserUserID = new SqlParameter("@" + TAG_ID, JctUserUserID);
			paramJctUserUserID.DbType = DbType.Int32;
			paramJctUserUserID.Direction = ParameterDirection.Input;



				paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
			paramDateModified.DbType = DbType.DateTime;
			paramDateModified.Direction = ParameterDirection.Input;

			paramUserSourceID = new SqlParameter("@" + TAG_USER_SOURCE_ID, UserSourceID);
			paramUserSourceID.DbType = DbType.Int32;
			paramUserSourceID.Direction = ParameterDirection.Input;

			paramUserMemberID = new SqlParameter("@" + TAG_USER_MEMBER_ID, UserMemberID);
			paramUserMemberID.DbType = DbType.Int32;
			paramUserMemberID.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramJctUserUserID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramUserSourceID);
			cmd.Parameters.Add(paramUserMemberID);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			JctUserUserID = long.Parse(s);

			// cleanup
			paramJctUserUserID = null;
			paramDateModified = null;
			paramUserSourceID = null;
			paramUserMemberID = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramJctUserUserID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramJctUserUserID = new SqlParameter("@" + TAG_ID, JctUserUserID);
			paramJctUserUserID.DbType = DbType.Int32;
			paramJctUserUserID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramJctUserUserID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramJctUserUserID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramJctUserUserID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramJctUserUserID = new SqlParameter("@" + TAG_ID, JctUserUserID);
			paramJctUserUserID.DbType = DbType.Int32;
			paramJctUserUserID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramJctUserUserID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramJctUserUserID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.JctUserUserID = long.Parse(rdr[DB_FIELD_ID].ToString());
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
			this.UserSourceID = Convert.ToInt32(rdr[DB_FIELD_USER_SOURCE_ID].ToString().Trim());
			}
			catch{}
			try
			{
			this.UserMemberID = Convert.ToInt32(rdr[DB_FIELD_USER_MEMBER_ID].ToString().Trim());
			}
			catch{}
		}

	}
}

//END OF JctUserUser CLASS FILE
