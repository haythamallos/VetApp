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
	/// File:  Dbversion.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Dbversion database table.
	/// </summary>
	public class Dbversion
	{
		//Attributes
		/// <summary>DbversionID Attribute type String</summary>
		private long _lDbversionID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>MajorNum Attribute type String</summary>
		private long _lMajorNum = 0;
		/// <summary>MinorNum Attribute type String</summary>
		private long _lMinorNum = 0;
		/// <summary>Notes Attribute type String</summary>
		private string _strNotes = null;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Dbversion and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Dbversion"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "dbversion_id"; //Table id field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>major_num Database field </summary>
		public static readonly string DB_FIELD_MAJOR_NUM = "major_num"; //Table MajorNum field name
		/// <summary>minor_num Database field </summary>
		public static readonly string DB_FIELD_MINOR_NUM = "minor_num"; //Table MinorNum field name
		/// <summary>notes Database field </summary>
		public static readonly string DB_FIELD_NOTES = "notes"; //Table Notes field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "DbversionID"; //Attribute id  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>MajorNum Attribute type string</summary>
		public static readonly string TAG_MAJOR_NUM = "MajorNum"; //Table MajorNum field name
		/// <summary>MinorNum Attribute type string</summary>
		public static readonly string TAG_MINOR_NUM = "MinorNum"; //Table MinorNum field name
		/// <summary>Notes Attribute type string</summary>
		public static readonly string TAG_NOTES = "Notes"; //Table Notes field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spDbversionInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spDbversionUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spDbversionDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spDbversionLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spDbversionExist"; //Exist sp name

		//properties
		/// <summary>DbversionID is a Property in the Dbversion Class of type long</summary>
		public long DbversionID 
		{
			get{return _lDbversionID;}
			set{_lDbversionID = value;}
		}
		/// <summary>DateCreated is a Property in the Dbversion Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>MajorNum is a Property in the Dbversion Class of type long</summary>
		public long MajorNum 
		{
			get{return _lMajorNum;}
			set{_lMajorNum = value;}
		}
		/// <summary>MinorNum is a Property in the Dbversion Class of type long</summary>
		public long MinorNum 
		{
			get{return _lMinorNum;}
			set{_lMinorNum = value;}
		}
		/// <summary>Notes is a Property in the Dbversion Class of type String</summary>
		public string Notes 
		{
			get{return _strNotes;}
			set{_strNotes = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Dbversion and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Dbversion and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Dbversion empty constructor</summary>
		public Dbversion()
		{
		}
		/// <summary>Dbversion constructor takes DbversionID and a SqlConnection</summary>
		public Dbversion(long l, SqlConnection conn) 
		{
			DbversionID = l;
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
		/// <summary>Dbversion Constructor takes pStrData and Config</summary>
		public Dbversion(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Dbversion Constructor takes SqlDataReader</summary>
		public Dbversion(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Dbversion Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + DbversionID.ToString() + "\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_CREATED + ":\n");
			}
			sbReturn.Append(TAG_MAJOR_NUM + ":  " + MajorNum + "\n");
			sbReturn.Append(TAG_MINOR_NUM + ":  " + MinorNum + "\n");
			sbReturn.Append(TAG_NOTES + ":  " + Notes + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Dbversion</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<Dbversion>\n");
			sbReturn.Append("<" + TAG_ID + ">" + DbversionID + "</" + TAG_ID + ">\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
			}
			sbReturn.Append("<" + TAG_MAJOR_NUM + ">" + MajorNum + "</" + TAG_MAJOR_NUM + ">\n");
			sbReturn.Append("<" + TAG_MINOR_NUM + ">" + MinorNum + "</" + TAG_MINOR_NUM + ">\n");
			sbReturn.Append("<" + TAG_NOTES + ">" + Notes + "</" + TAG_NOTES + ">\n");
			sbReturn.Append("</Dbversion>" + "\n");

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
				DbversionID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_MAJOR_NUM);
				MajorNum = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			MajorNum = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MINOR_NUM);
				MinorNum = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			MinorNum = 0;
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
		/// <summary>Calls sqlLoad() method which gets record from database with dbversion_id equal to the current object's DbversionID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where dbversion_id equal to the current object's DbversionID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where dbversion_id equal to the current object's DbversionID </summary>
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
						DbversionID = long.Parse(Console.ReadLine());
					}
					catch 
					{
						DbversionID = 0;
					}
				}
				try
				{
					Console.WriteLine(Dbversion.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}

				Console.WriteLine(Dbversion.TAG_MAJOR_NUM + ":  ");
				MajorNum = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Dbversion.TAG_MINOR_NUM + ":  ");
				MinorNum = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Dbversion.TAG_NOTES + ":  ");
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
			SqlParameter paramDbversionID = null;
			SqlParameter paramDateCreated = null;
			SqlParameter paramMajorNum = null;
			SqlParameter paramMinorNum = null;
			SqlParameter paramNotes = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters
			paramDbversionID = new SqlParameter("@" + TAG_ID, DbversionID);
			paramDbversionID.DbType = DbType.Int32;
			paramDbversionID.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;

			paramMajorNum = new SqlParameter("@" + TAG_MAJOR_NUM, MajorNum);
			paramMajorNum.DbType = DbType.Int32;
			paramMajorNum.Direction = ParameterDirection.Input;

			paramMinorNum = new SqlParameter("@" + TAG_MINOR_NUM, MinorNum);
			paramMinorNum.DbType = DbType.Int32;
			paramMinorNum.Direction = ParameterDirection.Input;

			paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
			paramNotes.DbType = DbType.String;
			paramNotes.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramDbversionID);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramMajorNum);
			cmd.Parameters.Add(paramMinorNum);
			cmd.Parameters.Add(paramNotes);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			DbversionID = long.Parse(strTmp);

			// cleanup to help GC
			paramDbversionID = null;
			paramDateCreated = null;
			paramMajorNum = null;
			paramMinorNum = null;
			paramNotes = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramDbversionID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramDbversionID = new SqlParameter("@" + TAG_ID, DbversionID);
			paramDbversionID.Direction = ParameterDirection.Input;
			paramDbversionID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramDbversionID);
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
			paramDbversionID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramDbversionID = null;
			SqlParameter paramMajorNum = null;
			SqlParameter paramMinorNum = null;
			SqlParameter paramNotes = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramDbversionID = new SqlParameter("@" + TAG_ID, DbversionID);
			paramDbversionID.DbType = DbType.Int32;
			paramDbversionID.Direction = ParameterDirection.Input;



			paramMajorNum = new SqlParameter("@" + TAG_MAJOR_NUM, MajorNum);
			paramMajorNum.DbType = DbType.Int32;
			paramMajorNum.Direction = ParameterDirection.Input;

			paramMinorNum = new SqlParameter("@" + TAG_MINOR_NUM, MinorNum);
			paramMinorNum.DbType = DbType.Int32;
			paramMinorNum.Direction = ParameterDirection.Input;

			paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
			paramNotes.DbType = DbType.String;
			paramNotes.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramDbversionID);
			cmd.Parameters.Add(paramMajorNum);
			cmd.Parameters.Add(paramMinorNum);
			cmd.Parameters.Add(paramNotes);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			DbversionID = long.Parse(s);

			// cleanup
			paramDbversionID = null;
			paramMajorNum = null;
			paramMinorNum = null;
			paramNotes = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramDbversionID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramDbversionID = new SqlParameter("@" + TAG_ID, DbversionID);
			paramDbversionID.DbType = DbType.Int32;
			paramDbversionID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramDbversionID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramDbversionID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramDbversionID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramDbversionID = new SqlParameter("@" + TAG_ID, DbversionID);
			paramDbversionID.DbType = DbType.Int32;
			paramDbversionID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramDbversionID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramDbversionID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.DbversionID = long.Parse(rdr[DB_FIELD_ID].ToString());
         try
			{
				this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
			}
			catch 
			{
			}
			try
			{
			this.MajorNum = Convert.ToInt32(rdr[DB_FIELD_MAJOR_NUM].ToString().Trim());
			}
			catch{}
			try
			{
			this.MinorNum = Convert.ToInt32(rdr[DB_FIELD_MINOR_NUM].ToString().Trim());
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

//END OF Dbversion CLASS FILE
