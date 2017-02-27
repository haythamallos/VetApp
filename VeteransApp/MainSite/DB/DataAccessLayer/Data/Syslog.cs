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
	/// File:  Syslog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Syslog database table.
	/// </summary>
	public class Syslog
	{
		//Attributes
		/// <summary>SyslogID Attribute type String</summary>
		private long _lSyslogID = 0;
		/// <summary>InteractionID Attribute type String</summary>
		private long _lInteractionID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateModified Attribute type String</summary>
		private DateTime _dtDateModified = dtNull;
		/// <summary>Msgsource Attribute type String</summary>
		private string _strMsgsource = null;
		/// <summary>Msgaction Attribute type String</summary>
		private string _strMsgaction = null;
		/// <summary>Msgtxt Attribute type String</summary>
		private string _strMsgtxt = null;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Syslog and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Syslog"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "syslog_id"; //Table id field name
		/// <summary>interaction_id Database field </summary>
		public static readonly string DB_FIELD_INTERACTION_ID = "interaction_id"; //Table InteractionID field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_modified Database field </summary>
		public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
		/// <summary>msgsource Database field </summary>
		public static readonly string DB_FIELD_MSGSOURCE = "msgsource"; //Table Msgsource field name
		/// <summary>msgaction Database field </summary>
		public static readonly string DB_FIELD_MSGACTION = "msgaction"; //Table Msgaction field name
		/// <summary>msgtxt Database field </summary>
		public static readonly string DB_FIELD_MSGTXT = "msgtxt"; //Table Msgtxt field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "SyslogID"; //Attribute id  name
		/// <summary>InteractionID Attribute type string</summary>
		public static readonly string TAG_INTERACTION_ID = "InteractionID"; //Table InteractionID field name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
		/// <summary>Msgsource Attribute type string</summary>
		public static readonly string TAG_MSGSOURCE = "Msgsource"; //Table Msgsource field name
		/// <summary>Msgaction Attribute type string</summary>
		public static readonly string TAG_MSGACTION = "Msgaction"; //Table Msgaction field name
		/// <summary>Msgtxt Attribute type string</summary>
		public static readonly string TAG_MSGTXT = "Msgtxt"; //Table Msgtxt field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spSyslogInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spSyslogUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spSyslogDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spSyslogLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spSyslogExist"; //Exist sp name

		//properties
		/// <summary>SyslogID is a Property in the Syslog Class of type long</summary>
		public long SyslogID 
		{
			get{return _lSyslogID;}
			set{_lSyslogID = value;}
		}
		/// <summary>InteractionID is a Property in the Syslog Class of type long</summary>
		public long InteractionID 
		{
			get{return _lInteractionID;}
			set{_lInteractionID = value;}
		}
		/// <summary>DateCreated is a Property in the Syslog Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the Syslog Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
		}
		/// <summary>Msgsource is a Property in the Syslog Class of type String</summary>
		public string Msgsource 
		{
			get{return _strMsgsource;}
			set{_strMsgsource = value;}
		}
		/// <summary>Msgaction is a Property in the Syslog Class of type String</summary>
		public string Msgaction 
		{
			get{return _strMsgaction;}
			set{_strMsgaction = value;}
		}
		/// <summary>Msgtxt is a Property in the Syslog Class of type String</summary>
		public string Msgtxt 
		{
			get{return _strMsgtxt;}
			set{_strMsgtxt = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Syslog and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Syslog and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Syslog empty constructor</summary>
		public Syslog()
		{
		}
		/// <summary>Syslog constructor takes SyslogID and a SqlConnection</summary>
		public Syslog(long l, SqlConnection conn) 
		{
			SyslogID = l;
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
		/// <summary>Syslog Constructor takes pStrData and Config</summary>
		public Syslog(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Syslog Constructor takes SqlDataReader</summary>
		public Syslog(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Syslog Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + SyslogID.ToString() + "\n");
			sbReturn.Append(TAG_INTERACTION_ID + ":  " + InteractionID + "\n");
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
			sbReturn.Append(TAG_MSGSOURCE + ":  " + Msgsource + "\n");
			sbReturn.Append(TAG_MSGACTION + ":  " + Msgaction + "\n");
			sbReturn.Append(TAG_MSGTXT + ":  " + Msgtxt + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Syslog</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<Syslog>\n");
			sbReturn.Append("<" + TAG_ID + ">" + SyslogID + "</" + TAG_ID + ">\n");
			sbReturn.Append("<" + TAG_INTERACTION_ID + ">" + InteractionID + "</" + TAG_INTERACTION_ID + ">\n");
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
			sbReturn.Append("<" + TAG_MSGSOURCE + ">" + Msgsource + "</" + TAG_MSGSOURCE + ">\n");
			sbReturn.Append("<" + TAG_MSGACTION + ">" + Msgaction + "</" + TAG_MSGACTION + ">\n");
			sbReturn.Append("<" + TAG_MSGTXT + ">" + Msgtxt + "</" + TAG_MSGTXT + ">\n");
			sbReturn.Append("</Syslog>" + "\n");

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
				SyslogID = (long) Convert.ToInt32(strTmp);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_INTERACTION_ID);
				InteractionID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			InteractionID = 0;
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
				xResultNode = xNode.SelectSingleNode(TAG_MSGSOURCE);
				Msgsource = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MSGACTION);
				Msgaction = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MSGTXT);
				Msgtxt = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}
		}
		/// <summary>Calls sqlLoad() method which gets record from database with syslog_id equal to the current object's SyslogID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where syslog_id equal to the current object's SyslogID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where syslog_id equal to the current object's SyslogID </summary>
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

				Console.WriteLine(Syslog.TAG_INTERACTION_ID + ":  ");
				InteractionID = (long)Convert.ToInt32(Console.ReadLine());
				try
				{
					Console.WriteLine(Syslog.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(Syslog.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(Syslog.TAG_MSGSOURCE + ":  ");
				Msgsource = Console.ReadLine();

				Console.WriteLine(Syslog.TAG_MSGACTION + ":  ");
				Msgaction = Console.ReadLine();

				Console.WriteLine(Syslog.TAG_MSGTXT + ":  ");
				Msgtxt = Console.ReadLine();

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
			SqlParameter paramInteractionID = null;
			SqlParameter paramDateCreated = null;
			SqlParameter paramMsgsource = null;
			SqlParameter paramMsgaction = null;
			SqlParameter paramMsgtxt = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramInteractionID = new SqlParameter("@" + TAG_INTERACTION_ID, InteractionID);
			paramInteractionID.DbType = DbType.Int32;
			paramInteractionID.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;


			paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
			paramMsgsource.DbType = DbType.String;
			paramMsgsource.Size = 255;
			paramMsgsource.Direction = ParameterDirection.Input;

			paramMsgaction = new SqlParameter("@" + TAG_MSGACTION, Msgaction);
			paramMsgaction.DbType = DbType.String;
			paramMsgaction.Size = 255;
			paramMsgaction.Direction = ParameterDirection.Input;

			paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
			paramMsgtxt.DbType = DbType.String;
			paramMsgtxt.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramInteractionID);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramMsgsource);
			cmd.Parameters.Add(paramMsgaction);
			cmd.Parameters.Add(paramMsgtxt);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			SyslogID = long.Parse(strTmp);

			// cleanup to help GC
			paramInteractionID = null;
			paramDateCreated = null;
			paramMsgsource = null;
			paramMsgaction = null;
			paramMsgtxt = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramSyslogID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramSyslogID = new SqlParameter("@" + TAG_ID, SyslogID);
			paramSyslogID.Direction = ParameterDirection.Input;
			paramSyslogID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramSyslogID);
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
			paramSyslogID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramSyslogID = null;
			SqlParameter paramInteractionID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramMsgsource = null;
			SqlParameter paramMsgaction = null;
			SqlParameter paramMsgtxt = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramSyslogID = new SqlParameter("@" + TAG_ID, SyslogID);
			paramSyslogID.DbType = DbType.Int32;
			paramSyslogID.Direction = ParameterDirection.Input;


			paramInteractionID = new SqlParameter("@" + TAG_INTERACTION_ID, InteractionID);
			paramInteractionID.DbType = DbType.Int32;
			paramInteractionID.Direction = ParameterDirection.Input;


				paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
			paramDateModified.DbType = DbType.DateTime;
			paramDateModified.Direction = ParameterDirection.Input;

			paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
			paramMsgsource.DbType = DbType.String;
			paramMsgsource.Size = 255;
			paramMsgsource.Direction = ParameterDirection.Input;

			paramMsgaction = new SqlParameter("@" + TAG_MSGACTION, Msgaction);
			paramMsgaction.DbType = DbType.String;
			paramMsgaction.Size = 255;
			paramMsgaction.Direction = ParameterDirection.Input;

			paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
			paramMsgtxt.DbType = DbType.String;
			paramMsgtxt.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramSyslogID);
			cmd.Parameters.Add(paramInteractionID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramMsgsource);
			cmd.Parameters.Add(paramMsgaction);
			cmd.Parameters.Add(paramMsgtxt);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			SyslogID = long.Parse(s);

			// cleanup
			paramSyslogID = null;
			paramInteractionID = null;
			paramDateModified = null;
			paramMsgsource = null;
			paramMsgaction = null;
			paramMsgtxt = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramSyslogID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramSyslogID = new SqlParameter("@" + TAG_ID, SyslogID);
			paramSyslogID.DbType = DbType.Int32;
			paramSyslogID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramSyslogID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramSyslogID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramSyslogID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramSyslogID = new SqlParameter("@" + TAG_ID, SyslogID);
			paramSyslogID.DbType = DbType.Int32;
			paramSyslogID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramSyslogID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramSyslogID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.SyslogID = long.Parse(rdr[DB_FIELD_ID].ToString());
			try
			{
			this.InteractionID = Convert.ToInt32(rdr[DB_FIELD_INTERACTION_ID].ToString().Trim());
			}
			catch{}
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
			this.Msgsource = rdr[DB_FIELD_MSGSOURCE].ToString().Trim();
			}
			catch{}
			try
			{
			this.Msgaction = rdr[DB_FIELD_MSGACTION].ToString().Trim();
			}
			catch{}
			try
			{
			       this.Msgtxt = rdr[DB_FIELD_MSGTXT].ToString().Trim();
			}
			catch{}
		}

	}
}

//END OF Syslog CLASS FILE
