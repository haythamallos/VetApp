using System;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using Vetapp.Engine.Common;
using System.Data;

namespace Vetapp.Engine.DataAccessLayer.Data
{
	/// <summary>
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  Apikey.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	10/20/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Apikey database table.
	/// </summary>
	public class Apikey
	{
		//Attributes
		/// <summary>ApikeyID Attribute type String</summary>
		private long _lApikeyID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateExpiration Attribute type String</summary>
		private DateTime _dtDateExpiration = dtNull;
		/// <summary>IsDisabled Attribute type String</summary>
		private bool? _bIsDisabled = null;
		/// <summary>Token Attribute type String</summary>
		private string _strToken = null;
		/// <summary>Notes Attribute type String</summary>
		private string _strNotes = null;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Apikey and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Apikey"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "apikey_id"; //Table id field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_expiration Database field </summary>
		public static readonly string DB_FIELD_DATE_EXPIRATION = "date_expiration"; //Table DateExpiration field name
		/// <summary>is_disabled Database field </summary>
		public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name
		/// <summary>token Database field </summary>
		public static readonly string DB_FIELD_TOKEN = "token"; //Table Token field name
		/// <summary>notes Database field </summary>
		public static readonly string DB_FIELD_NOTES = "notes"; //Table Notes field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "ApikeyID"; //Attribute id  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateExpiration Attribute type string</summary>
		public static readonly string TAG_DATE_EXPIRATION = "DateExpiration"; //Table DateExpiration field name
		/// <summary>IsDisabled Attribute type string</summary>
		public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name
		/// <summary>Token Attribute type string</summary>
		public static readonly string TAG_TOKEN = "Token"; //Table Token field name
		/// <summary>Notes Attribute type string</summary>
		public static readonly string TAG_NOTES = "Notes"; //Table Notes field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spApikeyInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spApikeyUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spApikeyDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spApikeyLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spApikeyExist"; //Exist sp name

		//properties
		/// <summary>ApikeyID is a Property in the Apikey Class of type long</summary>
		public long ApikeyID 
		{
			get{return _lApikeyID;}
			set{_lApikeyID = value;}
		}
		/// <summary>DateCreated is a Property in the Apikey Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateExpiration is a Property in the Apikey Class of type DateTime</summary>
		public DateTime DateExpiration 
		{
			get{return _dtDateExpiration;}
			set{_dtDateExpiration = value;}
		}
		/// <summary>IsDisabled is a Property in the Apikey Class of type bool</summary>
		public bool? IsDisabled 
		{
			get{return _bIsDisabled;}
			set{_bIsDisabled = value;}
		}
		/// <summary>Token is a Property in the Apikey Class of type String</summary>
		public string Token 
		{
			get{return _strToken;}
			set{_strToken = value;}
		}
		/// <summary>Notes is a Property in the Apikey Class of type String</summary>
		public string Notes 
		{
			get{return _strNotes;}
			set{_strNotes = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Apikey and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Apikey and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Apikey empty constructor</summary>
		public Apikey()
		{
		}
		/// <summary>Apikey constructor takes ApikeyID and a SqlConnection</summary>
		public Apikey(long l, SqlConnection conn) 
		{
			ApikeyID = l;
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
		/// <summary>Apikey Constructor takes pStrData and Config</summary>
		public Apikey(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Apikey Constructor takes SqlDataReader</summary>
		public Apikey(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Apikey Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + ApikeyID.ToString() + "\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_CREATED + ":\n");
			}
			if (!dtNull.Equals(DateExpiration))
			{
				sbReturn.Append(TAG_DATE_EXPIRATION + ":  " + DateExpiration.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_EXPIRATION + ":\n");
			}
			sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
			sbReturn.Append(TAG_TOKEN + ":  " + Token + "\n");
			sbReturn.Append(TAG_NOTES + ":  " + Notes + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Apikey</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<Apikey>\n");
			sbReturn.Append("<" + TAG_ID + ">" + ApikeyID + "</" + TAG_ID + ">\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
			}
			if (!dtNull.Equals(DateExpiration))
			{
				sbReturn.Append("<" + TAG_DATE_EXPIRATION + ">" + DateExpiration.ToString() + "</" + TAG_DATE_EXPIRATION + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_EXPIRATION + "></" + TAG_DATE_EXPIRATION + ">\n");
			}
			sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
			sbReturn.Append("<" + TAG_TOKEN + ">" + Token + "</" + TAG_TOKEN + ">\n");
			sbReturn.Append("<" + TAG_NOTES + ">" + Notes + "</" + TAG_NOTES + ">\n");
			sbReturn.Append("</Apikey>" + "\n");

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
				ApikeyID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_DATE_EXPIRATION);
				DateExpiration = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
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
				xResultNode = xNode.SelectSingleNode(TAG_TOKEN);
				Token = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
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
		/// <summary>Calls sqlLoad() method which gets record from database with apikey_id equal to the current object's ApikeyID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where apikey_id equal to the current object's ApikeyID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where apikey_id equal to the current object's ApikeyID </summary>
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
						ApikeyID = long.Parse(Console.ReadLine());
					}
					catch 
					{
						ApikeyID = 0;
					}
				}
				try
				{
					Console.WriteLine(Apikey.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(Apikey.TAG_DATE_EXPIRATION + ":  ");
					DateExpiration = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateExpiration = new DateTime();
				}

				Console.WriteLine(Apikey.TAG_IS_DISABLED + ":  ");
				IsDisabled = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Apikey.TAG_TOKEN + ":  ");
				Token = Console.ReadLine();

				Console.WriteLine(Apikey.TAG_NOTES + ":  ");
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
			SqlParameter paramApikeyID = null;
			SqlParameter paramDateCreated = null;
			SqlParameter paramDateExpiration = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramToken = null;
			SqlParameter paramNotes = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters
			paramApikeyID = new SqlParameter("@" + TAG_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(DateExpiration))
			{
				paramDateExpiration = new SqlParameter("@" + TAG_DATE_EXPIRATION, DateExpiration);
			}
			else
			{
				paramDateExpiration = new SqlParameter("@" + TAG_DATE_EXPIRATION, DBNull.Value);
			}
			paramDateExpiration.DbType = DbType.DateTime;
			paramDateExpiration.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			paramToken = new SqlParameter("@" + TAG_TOKEN, Token);
			paramToken.DbType = DbType.String;
			paramToken.Size = 255;
			paramToken.Direction = ParameterDirection.Input;

			paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
			paramNotes.DbType = DbType.String;
			paramNotes.Size = 255;
			paramNotes.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramApikeyID);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramDateExpiration);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramToken);
			cmd.Parameters.Add(paramNotes);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			ApikeyID = long.Parse(strTmp);

			// cleanup to help GC
			paramApikeyID = null;
			paramDateCreated = null;
			paramDateExpiration = null;
			paramIsDisabled = null;
			paramToken = null;
			paramNotes = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramApikeyID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramApikeyID = new SqlParameter("@" + TAG_ID, ApikeyID);
			paramApikeyID.Direction = ParameterDirection.Input;
			paramApikeyID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramApikeyID);
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
			paramApikeyID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApikeyID = null;
			SqlParameter paramDateExpiration = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramToken = null;
			SqlParameter paramNotes = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramApikeyID = new SqlParameter("@" + TAG_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;



			if (!dtNull.Equals(DateExpiration))
			{
				paramDateExpiration = new SqlParameter("@" + TAG_DATE_EXPIRATION, DateExpiration);
			}
			else
			{
				paramDateExpiration = new SqlParameter("@" + TAG_DATE_EXPIRATION, DBNull.Value);
			}
			paramDateExpiration.DbType = DbType.DateTime;
			paramDateExpiration.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			paramToken = new SqlParameter("@" + TAG_TOKEN, Token);
			paramToken.DbType = DbType.String;
			paramToken.Size = 255;
			paramToken.Direction = ParameterDirection.Input;

			paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
			paramNotes.DbType = DbType.String;
			paramNotes.Size = 255;
			paramNotes.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramApikeyID);
			cmd.Parameters.Add(paramDateExpiration);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramToken);
			cmd.Parameters.Add(paramNotes);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			ApikeyID = long.Parse(s);

			// cleanup
			paramApikeyID = null;
			paramDateExpiration = null;
			paramIsDisabled = null;
			paramToken = null;
			paramNotes = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApikeyID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramApikeyID = new SqlParameter("@" + TAG_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramApikeyID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramApikeyID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApikeyID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramApikeyID = new SqlParameter("@" + TAG_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramApikeyID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramApikeyID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.ApikeyID = long.Parse(rdr[DB_FIELD_ID].ToString());
         try
			{
				this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
			}
			catch 
			{
			}
         try
			{
				this.DateExpiration = DateTime.Parse(rdr[DB_FIELD_DATE_EXPIRATION].ToString());
			}
			catch 
			{
			}
			try
			{
			this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
			}
			catch{}
			try
			{
			this.Token = rdr[DB_FIELD_TOKEN].ToString().Trim();
			}
			catch{}
			try
			{
			this.Notes = rdr[DB_FIELD_NOTES].ToString().Trim();
			}
			catch{}
		}

	}
}

//END OF Apikey CLASS FILE
