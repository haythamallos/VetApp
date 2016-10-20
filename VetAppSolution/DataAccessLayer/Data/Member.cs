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
	/// File:  Member.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	10/20/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Member database table.
	/// </summary>
	public class Member
	{
		//Attributes
		/// <summary>MemberID Attribute type String</summary>
		private long _lMemberID = 0;
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
		/// <summary>Profileimageurl Attribute type String</summary>
		private string _strProfileimageurl = null;
		/// <summary>IsDisabled Attribute type String</summary>
		private bool? _bIsDisabled = null;

		private Config _config = null;
		private ErrorCode _errorCode = null;
		private Logger _oLog = null;
		private string _strLognameText = "DataAccessLayer-Data-Member";
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Member and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Member"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "member_id"; //Table id field name
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
		/// <summary>profileimageurl Database field </summary>
		public static readonly string DB_FIELD_PROFILEIMAGEURL = "profileimageurl"; //Table Profileimageurl field name
		/// <summary>is_disabled Database field </summary>
		public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "MemberID"; //Attribute id  name
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
		/// <summary>Profileimageurl Attribute type string</summary>
		public static readonly string TAG_PROFILEIMAGEURL = "Profileimageurl"; //Table Profileimageurl field name
		/// <summary>IsDisabled Attribute type string</summary>
		public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spMemberInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spMemberUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spMemberDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spMemberLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spMemberExist"; //Exist sp name

		//properties
		/// <summary>MemberID is a Property in the Member Class of type long</summary>
		public long MemberID 
		{
			get{return _lMemberID;}
			set{_lMemberID = value;}
		}
		/// <summary>DateCreated is a Property in the Member Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the Member Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
		}
		/// <summary>Firstname is a Property in the Member Class of type String</summary>
		public string Firstname 
		{
			get{return _strFirstname;}
			set{_strFirstname = value;}
		}
		/// <summary>Middlename is a Property in the Member Class of type String</summary>
		public string Middlename 
		{
			get{return _strMiddlename;}
			set{_strMiddlename = value;}
		}
		/// <summary>Lastname is a Property in the Member Class of type String</summary>
		public string Lastname 
		{
			get{return _strLastname;}
			set{_strLastname = value;}
		}
		/// <summary>Profileimageurl is a Property in the Member Class of type String</summary>
		public string Profileimageurl 
		{
			get{return _strProfileimageurl;}
			set{_strProfileimageurl = value;}
		}
		/// <summary>IsDisabled is a Property in the Member Class of type bool</summary>
		public bool? IsDisabled 
		{
			get{return _bIsDisabled;}
			set{_bIsDisabled = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Member and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Member and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Member empty constructor</summary>
		public Member()
		{
		}
		/// <summary>Member constructor takes a Config</summary>
		public Member(Config pConfig)
		{
			_config = pConfig;
			_oLog = new Logger(_strLognameText);
		}
		/// <summary>Member constructor takes MemberID and a SqlConnection</summary>
		public Member(long l, SqlConnection conn) 
		{
			MemberID = l;
			try
			{
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Member Constructor takes pStrData and Config</summary>
		public Member(string pStrData, Config pConfig)
		{
			_config = pConfig;
			_oLog = new Logger(_strLognameText);
			Parse(pStrData);
		}
		/// <summary>Member Constructor takes SqlDataReader</summary>
		public Member(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Member Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + MemberID.ToString() + "\n");
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
			sbReturn.Append(TAG_PROFILEIMAGEURL + ":  " + Profileimageurl + "\n");
			sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Member</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<Member>\n");
			sbReturn.Append("<" + TAG_ID + ">" + MemberID + "</" + TAG_ID + ">\n");
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
			sbReturn.Append("<" + TAG_PROFILEIMAGEURL + ">" + Profileimageurl + "</" + TAG_PROFILEIMAGEURL + ">\n");
			sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
			sbReturn.Append("</Member>" + "\n");

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
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
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
				MemberID = (long) Convert.ToInt32(strTmp);
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
		}
		/// <summary>Calls sqlLoad() method which gets record from database with member_id equal to the current object's MemberID </summary>
		public void Load(SqlConnection conn)
		{
			try
			{
				_log("LOAD", ToString());
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where member_id equal to the current object's MemberID </summary>
		public void Update(SqlConnection conn)
		{
			bool bExist = false;
			try
			{
				_log("UPDATE", ToString());
				bExist = Exist(conn);
				if (bExist)
				{
					sqlUpdate(conn);
				}
				else
				{
				_log("NOT_EXIST", ToString());
				}
			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
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

				_log("INSERT", ToString());
				bExist = Exist(conn);
				if (!bExist)
				{
					sqlInsert(conn);
				}
				else
				{
				_log("ALREADY_EXISTS", ToString());
					sqlUpdate(conn);
				}
			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlDelete() method which delete's the record from database where where member_id equal to the current object's MemberID </summary>
		public void Delete(SqlConnection conn)
		{
			try
			{
				_log("DELETE", ToString());
				sqlDelete(conn);
			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
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
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
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
					Console.WriteLine(Member.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(Member.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(Member.TAG_FIRSTNAME + ":  ");
				Firstname = Console.ReadLine();

				Console.WriteLine(Member.TAG_MIDDLENAME + ":  ");
				Middlename = Console.ReadLine();

				Console.WriteLine(Member.TAG_LASTNAME + ":  ");
				Lastname = Console.ReadLine();

				Console.WriteLine(Member.TAG_PROFILEIMAGEURL + ":  ");
				Profileimageurl = Console.ReadLine();

				Console.WriteLine(Member.TAG_IS_DISABLED + ":  ");
				IsDisabled = Convert.ToBoolean(Console.ReadLine());

			}
			catch (Exception e) 
			{
				 _log("ERROR", e.ToString() + e.StackTrace.ToString());
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
			SqlParameter paramFirstname = null;
			SqlParameter paramMiddlename = null;
			SqlParameter paramLastname = null;
			SqlParameter paramProfileimageurl = null;
			SqlParameter paramIsDisabled = null;
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

			paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, Profileimageurl);
			paramProfileimageurl.DbType = DbType.String;
			paramProfileimageurl.Size = 255;
			paramProfileimageurl.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramFirstname);
			cmd.Parameters.Add(paramMiddlename);
			cmd.Parameters.Add(paramLastname);
			cmd.Parameters.Add(paramProfileimageurl);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			MemberID = long.Parse(strTmp);

			// cleanup to help GC
			paramDateCreated = null;
			paramFirstname = null;
			paramMiddlename = null;
			paramLastname = null;
			paramProfileimageurl = null;
			paramIsDisabled = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramMemberID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramMemberID = new SqlParameter("@" + TAG_ID, MemberID);
			paramMemberID.Direction = ParameterDirection.Input;
			paramMemberID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramMemberID);
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
			paramMemberID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramMemberID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramFirstname = null;
			SqlParameter paramMiddlename = null;
			SqlParameter paramLastname = null;
			SqlParameter paramProfileimageurl = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramMemberID = new SqlParameter("@" + TAG_ID, MemberID);
			paramMemberID.DbType = DbType.Int32;
			paramMemberID.Direction = ParameterDirection.Input;



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

			paramProfileimageurl = new SqlParameter("@" + TAG_PROFILEIMAGEURL, Profileimageurl);
			paramProfileimageurl.DbType = DbType.String;
			paramProfileimageurl.Size = 255;
			paramProfileimageurl.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramMemberID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramFirstname);
			cmd.Parameters.Add(paramMiddlename);
			cmd.Parameters.Add(paramLastname);
			cmd.Parameters.Add(paramProfileimageurl);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			MemberID = long.Parse(s);

			// cleanup
			paramMemberID = null;
			paramDateModified = null;
			paramFirstname = null;
			paramMiddlename = null;
			paramLastname = null;
			paramProfileimageurl = null;
			paramIsDisabled = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramMemberID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramMemberID = new SqlParameter("@" + TAG_ID, MemberID);
			paramMemberID.DbType = DbType.Int32;
			paramMemberID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramMemberID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramMemberID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramMemberID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramMemberID = new SqlParameter("@" + TAG_ID, MemberID);
			paramMemberID.DbType = DbType.Int32;
			paramMemberID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramMemberID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Close();
			rdr = null;
			paramMemberID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.MemberID = long.Parse(rdr[DB_FIELD_ID].ToString());
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
			catch{}
			try
			{
			this.Middlename = rdr[DB_FIELD_MIDDLENAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Lastname = rdr[DB_FIELD_LASTNAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Profileimageurl = rdr[DB_FIELD_PROFILEIMAGEURL].ToString().Trim();
			}
			catch{}
			try
			{
			this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
			}
			catch{}
		}

		//private
		/// <summary>Log errors</summary>
		private void _log(string pStrAction, string pStrMsgText) 
		{
			if (_config != null )
			{
				if (_config.DoLogInfo)
				{
						_oLog.Log(pStrAction, pStrMsgText);
				}
			}

		}

		/// <summary>Unit Testing: Save, Delete, Update, Exist, Load and ToXml</summary>
		public void Test(SqlConnection conn)
		{
			try 
			{
				Console.WriteLine("What would you like to do?");
				Console.WriteLine("1.  Save.");
				Console.WriteLine("2.  Delete.");
				Console.WriteLine("3.  Update.");
				Console.WriteLine("4.  Exist.");
				Console.WriteLine("5.  Load.");
				Console.WriteLine("6.  ToXml.");
				Console.WriteLine("q.  Quit.");
				
				string strAns = "";

				strAns = Console.ReadLine();
				if (strAns != "q")
				{	
					int nAns = 0;
					nAns = int.Parse(strAns);
					switch(nAns)
					{
						case 1:
							// insert
							Console.WriteLine("Save:  ");
							Prompt();
							Save(conn);
							Console.WriteLine(ToString());
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						case 2:
							Console.WriteLine("Delete " + TAG_ID + ":  ");
							strAns = Console.ReadLine();
							MemberID = long.Parse(strAns);
							Delete(conn);
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						case 3:
							Console.WriteLine("Update:  ");
							Prompt();
							Update(conn);
							Console.WriteLine(ToString());
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						case 4:
							Console.WriteLine("Exist " + TAG_ID + ":  ");
							strAns = Console.ReadLine();
							MemberID = long.Parse(strAns);
							bool bExist = false;
							bExist = Exist(conn);
							Console.WriteLine("Record id " + MemberID + " exist:  " + bExist.ToString() );
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						case 5:
							Console.WriteLine("Load " + TAG_ID + ":  ");
							strAns = Console.ReadLine();
							MemberID = long.Parse(strAns);
							Load(conn);
							Console.WriteLine(ToString());
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						case 6:
							Console.WriteLine("ToXml " + TAG_ID + ":  ");
							strAns = Console.ReadLine();
							MemberID = long.Parse(strAns);
							Load(conn);
							Console.WriteLine(ToXml());
							Console.WriteLine(" ");
							Console.WriteLine("Press ENTER to continue...");
							Console.ReadLine();
							break;
						default:
							Console.WriteLine("Undefined option.");
							break;
					}
				}
			}
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine(e.StackTrace);
				Console.ReadLine();
			}

		}		
	}
}

//END OF Member CLASS FILE
