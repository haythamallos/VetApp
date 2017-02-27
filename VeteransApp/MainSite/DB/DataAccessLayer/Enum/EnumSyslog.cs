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
	/// File:  EnumSyslog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	2/27/2017	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumSyslog
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
		public static readonly string ENTITY_NAME = "EnumSyslog"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lSyslogID = 0;
		private long _lInteractionID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private DateTime _dtBeginDateModified = new DateTime();
		private DateTime _dtEndDateModified = new DateTime();
		private string _strMsgsource = null;
		private string _strMsgaction = null;
		private string _strMsgtxt = null;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "syslog_id"; //Table id field name
		/// <summary>SyslogID Attribute type string</summary>
		public static readonly string TAG_SYSLOG_ID = "SyslogID"; //Attribute SyslogID  name
		/// <summary>InteractionID Attribute type string</summary>
		public static readonly string TAG_INTERACTION_ID = "InteractionID"; //Attribute InteractionID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
		/// <summary>EndDateModified Attribute type string</summary>
		public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
		/// <summary>Msgsource Attribute type string</summary>
		public static readonly string TAG_MSGSOURCE = "Msgsource"; //Attribute Msgsource  name
		/// <summary>Msgaction Attribute type string</summary>
		public static readonly string TAG_MSGACTION = "Msgaction"; //Attribute Msgaction  name
		/// <summary>Msgtxt Attribute type string</summary>
		public static readonly string TAG_MSGTXT = "Msgtxt"; //Attribute Msgtxt  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spSyslogEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Syslog Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
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
		/// <summary>Property DateCreated. Type: DateTime</summary>
		public DateTime BeginDateCreated
		{
			get{return _dtBeginDateCreated;}
			set{_dtBeginDateCreated = value;}
		}
		/// <summary>Property DateCreated. Type: DateTime</summary>
		public DateTime EndDateCreated
		{
			get{return _dtEndDateCreated;}
			set{_dtEndDateCreated = value;}
		}
		/// <summary>Property DateModified. Type: DateTime</summary>
		public DateTime BeginDateModified
		{
			get{return _dtBeginDateModified;}
			set{_dtBeginDateModified = value;}
		}
		/// <summary>Property DateModified. Type: DateTime</summary>
		public DateTime EndDateModified
		{
			get{return _dtEndDateModified;}
			set{_dtEndDateModified = value;}
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

		/// <summary>Count Property. Type: int</summary>
		public int Count 
		{
			get
			{
				_bSetup = true;
				// if necessary, close the old reader
				if ( (_cmd != null) || (_rdr != null) )
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
				return _nCount;			}
		}

		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumSyslog()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumSyslog(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type Syslog. Returns the next Syslog in the list</summary>
		private Syslog _nextTransaction
		{
			get
			{
				Syslog o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Syslog(_rdr);
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
				if ( (_cmd != null) || (_rdr != null) )
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
				if ( _rdr != null )
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

		/// <summary>ToString is overridden to display all properties of the Syslog Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_SYSLOG_ID + ":  " + SyslogID.ToString() + "\n");
			sbReturn.Append(TAG_INTERACTION_ID + ":  " + InteractionID + "\n");
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
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_SYSLOG_ID + ">" + SyslogID + "</" + TAG_SYSLOG_ID + ">\n");
			sbReturn.Append("<" + TAG_INTERACTION_ID + ">" + InteractionID + "</" + TAG_INTERACTION_ID + ">\n");
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
			sbReturn.Append("<" + TAG_MSGSOURCE + ">" + Msgsource + "</" + TAG_MSGSOURCE + ">\n");
			sbReturn.Append("<" + TAG_MSGACTION + ">" + Msgaction + "</" + TAG_MSGACTION + ">\n");
			sbReturn.Append("<" + TAG_MSGTXT + ">" + Msgtxt + "</" + TAG_MSGTXT + ">\n");
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
				if ( xNodes.Count > 0 )
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
				xResultNode = xNode.SelectSingleNode(TAG_SYSLOG_ID);
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
				xResultNode = xNode.SelectSingleNode(TAG_MSGSOURCE);
				Msgsource = xResultNode.InnerText;
				if (Msgsource.Trim().Length == 0)
					Msgsource = null;
			}
			catch  
			{
				Msgsource = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MSGACTION);
				Msgaction = xResultNode.InnerText;
				if (Msgaction.Trim().Length == 0)
					Msgaction = null;
			}
			catch  
			{
				Msgaction = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MSGTXT);
				Msgtxt = xResultNode.InnerText;
				if (Msgtxt.Trim().Length == 0)
					Msgtxt = null;
			}
			catch  
			{
				Msgtxt = null;
			}
		}
		/// <summary>Prompt for values</summary>
		public void Prompt()
		{
			try 
			{
				Console.WriteLine(TAG_INTERACTION_ID + ":  ");
				try
				{
					InteractionID = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					InteractionID = 0;
				}

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


				Console.WriteLine(TAG_MSGSOURCE + ":  ");
				Msgsource = Console.ReadLine();
				if (Msgsource.Length == 0)
				{
					Msgsource = null;
				}

				Console.WriteLine(TAG_MSGACTION + ":  ");
				Msgaction = Console.ReadLine();
				if (Msgaction.Length == 0)
				{
					Msgaction = null;
				}

				Console.WriteLine(TAG_MSGTXT + ":  ");
				Msgtxt = Console.ReadLine();
				if (Msgtxt.Length == 0)
				{
					Msgtxt = null;
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
			if (! disposing)
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
			SqlParameter paramSyslogID = null;
			SqlParameter paramInteractionID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramBeginDateModified = null;
			SqlParameter paramEndDateModified = null;
			SqlParameter paramMsgsource = null;
			SqlParameter paramMsgaction = null;
			SqlParameter paramMsgtxt = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramSyslogID = new SqlParameter("@" + TAG_SYSLOG_ID, SyslogID);
				sbLog.Append(TAG_SYSLOG_ID + "=" + SyslogID + "\n");
				paramSyslogID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramSyslogID);

				paramInteractionID = new SqlParameter("@" + TAG_INTERACTION_ID, InteractionID);
				sbLog.Append(TAG_INTERACTION_ID + "=" + InteractionID + "\n");
				paramInteractionID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramInteractionID);
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

			// Setup the msgsource text param
			if ( Msgsource != null )
			{
				paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
				sbLog.Append(TAG_MSGSOURCE + "=" + Msgsource + "\n");
			}
			else
			{
				paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, DBNull.Value);
			}
			paramMsgsource.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramMsgsource);

			// Setup the msgaction text param
			if ( Msgaction != null )
			{
				paramMsgaction = new SqlParameter("@" + TAG_MSGACTION, Msgaction);
				sbLog.Append(TAG_MSGACTION + "=" + Msgaction + "\n");
			}
			else
			{
				paramMsgaction = new SqlParameter("@" + TAG_MSGACTION, DBNull.Value);
			}
			paramMsgaction.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramMsgaction);

			// Setup the msgtxt text param
			if ( Msgtxt != null )
			{
				paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
				sbLog.Append(TAG_MSGTXT + "=" + Msgtxt + "\n");
			}
			else
			{
				paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, DBNull.Value);
			}
			paramMsgtxt.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramMsgtxt);

		}

	}
}

