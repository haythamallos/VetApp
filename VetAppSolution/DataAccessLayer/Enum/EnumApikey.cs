using System;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using Vetapp.Engine.Common;

using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.DataAccessLayer.Enumeration
{

	/// <summary>
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  EnumApikey.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	10/20/2016	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumApikey
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
		public static readonly string ENTITY_NAME = "EnumApikey"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lApikeyID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private DateTime _dtBeginDateExpiration = new DateTime();
		private DateTime _dtEndDateExpiration = new DateTime();
		private bool? _bIsDisabled = null;
		private string _strToken = null;
		private string _strNotes = null;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "apikey_id"; //Table id field name
		/// <summary>ApikeyID Attribute type string</summary>
		public static readonly string TAG_APIKEY_ID = "ApikeyID"; //Attribute ApikeyID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>DateExpiration Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_EXPIRATION = "BeginDateExpiration"; //Attribute DateExpiration  name
		/// <summary>EndDateExpiration Attribute type string</summary>
		public static readonly string TAG_END_DATE_EXPIRATION = "EndDateExpiration"; //Attribute DateExpiration  name
		/// <summary>IsDisabled Attribute type string</summary>
		public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Attribute IsDisabled  name
		/// <summary>Token Attribute type string</summary>
		public static readonly string TAG_TOKEN = "Token"; //Attribute Token  name
		/// <summary>Notes Attribute type string</summary>
		public static readonly string TAG_NOTES = "Notes"; //Attribute Notes  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spApikeyEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Apikey Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>ApikeyID is a Property in the Apikey Class of type long</summary>
		public long ApikeyID 
		{
			get{return _lApikeyID;}
			set{_lApikeyID = value;}
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
		/// <summary>Property DateExpiration. Type: DateTime</summary>
		public DateTime BeginDateExpiration
		{
			get{return _dtBeginDateExpiration;}
			set{_dtBeginDateExpiration = value;}
		}
		/// <summary>Property DateExpiration. Type: DateTime</summary>
		public DateTime EndDateExpiration
		{
			get{return _dtEndDateExpiration;}
			set{_dtEndDateExpiration = value;}
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
				_cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
		public EnumApikey()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumApikey(SqlConnection conn)
		{
			_conn = conn;
		}

		// Implementation of IEnumerator
		/// <summary>Property of type Apikey. Returns the next Apikey in the list</summary>
		private Apikey _nextTransaction
		{
			get
			{
				Apikey o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Apikey(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the Apikey Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_APIKEY_ID + ":  " + ApikeyID.ToString() + "\n");
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
			if (!dtNull.Equals(BeginDateExpiration))
			{
				sbReturn.Append(TAG_BEGIN_DATE_EXPIRATION + ":  " + BeginDateExpiration.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_DATE_EXPIRATION + ":\n");
			}
			if (!dtNull.Equals(EndDateExpiration))
			{
				sbReturn.Append(TAG_END_DATE_EXPIRATION + ":  " + EndDateExpiration.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_DATE_EXPIRATION + ":\n");
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
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_APIKEY_ID + ">" + ApikeyID + "</" + TAG_APIKEY_ID + ">\n");
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
			if (!dtNull.Equals(BeginDateExpiration))
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_EXPIRATION + ">" + BeginDateExpiration.ToString() + "</" + TAG_BEGIN_DATE_EXPIRATION + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_DATE_EXPIRATION + "></" + TAG_BEGIN_DATE_EXPIRATION + ">\n");
			}
			if (!dtNull.Equals(EndDateExpiration))
			{
				sbReturn.Append("<" + TAG_END_DATE_EXPIRATION + ">" + EndDateExpiration.ToString() + "</" + TAG_END_DATE_EXPIRATION + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_DATE_EXPIRATION + "></" + TAG_END_DATE_EXPIRATION + ">\n");
			}
			sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
			sbReturn.Append("<" + TAG_TOKEN + ">" + Token + "</" + TAG_TOKEN + ">\n");
			sbReturn.Append("<" + TAG_NOTES + ">" + Notes + "</" + TAG_NOTES + ">\n");
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
				xResultNode = xNode.SelectSingleNode(TAG_APIKEY_ID);
				strTmp = xResultNode.InnerText;
				ApikeyID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DATE_EXPIRATION);
				BeginDateExpiration = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_DATE_EXPIRATION);
				EndDateExpiration = DateTime.Parse(xResultNode.InnerText);
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
				if (Token.Trim().Length == 0)
					Token = null;
			}
			catch  
			{
				Token = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_NOTES);
				Notes = xResultNode.InnerText;
				if (Notes.Trim().Length == 0)
					Notes = null;
			}
			catch  
			{
				Notes = null;
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

				Console.WriteLine(TAG_BEGIN_DATE_EXPIRATION + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginDateExpiration = DateTime.Parse(s);
				}
				catch 
				{
					BeginDateExpiration = new DateTime();
				}

				Console.WriteLine(TAG_END_DATE_EXPIRATION + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndDateExpiration = DateTime.Parse(s);
				}
				catch  
				{
					EndDateExpiration = new DateTime();
				}

				Console.WriteLine(TAG_IS_DISABLED + ":  ");
				try
				{
					IsDisabled = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					IsDisabled = false;
				}


				Console.WriteLine(TAG_TOKEN + ":  ");
				Token = Console.ReadLine();
				if (Token.Length == 0)
				{
					Token = null;
				}

				Console.WriteLine(TAG_NOTES + ":  ");
				Notes = Console.ReadLine();
				if (Notes.Length == 0)
				{
					Notes = null;
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
			SqlParameter paramApikeyID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramBeginDateExpiration = null;
			SqlParameter paramEndDateExpiration = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramToken = null;
			SqlParameter paramNotes = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramApikeyID = new SqlParameter("@" + TAG_APIKEY_ID, ApikeyID);
				sbLog.Append(TAG_APIKEY_ID + "=" + ApikeyID + "\n");
				paramApikeyID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramApikeyID);

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

			// Setup the date expiration param
			if (!dtNull.Equals(BeginDateExpiration))
			{
				paramBeginDateExpiration = new SqlParameter("@" + TAG_BEGIN_DATE_EXPIRATION, BeginDateExpiration);
			}
			else
			{
				paramBeginDateExpiration = new SqlParameter("@" + TAG_BEGIN_DATE_EXPIRATION, DBNull.Value);
			}
			paramBeginDateExpiration.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginDateExpiration);

			if (!dtNull.Equals(EndDateExpiration))
			{
				paramEndDateExpiration = new SqlParameter("@" + TAG_END_DATE_EXPIRATION, EndDateExpiration);
			}
			else
			{
				paramEndDateExpiration = new SqlParameter("@" + TAG_END_DATE_EXPIRATION, DBNull.Value);
			}
			paramEndDateExpiration.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndDateExpiration);

				paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
				sbLog.Append(TAG_IS_DISABLED + "=" + IsDisabled + "\n");
				paramIsDisabled.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsDisabled);
			// Setup the token text param
			if ( Token != null )
			{
				paramToken = new SqlParameter("@" + TAG_TOKEN, Token);
				sbLog.Append(TAG_TOKEN + "=" + Token + "\n");
			}
			else
			{
				paramToken = new SqlParameter("@" + TAG_TOKEN, DBNull.Value);
			}
			paramToken.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramToken);

			// Setup the notes text param
			if ( Notes != null )
			{
				paramNotes = new SqlParameter("@" + TAG_NOTES, Notes);
				sbLog.Append(TAG_NOTES + "=" + Notes + "\n");
			}
			else
			{
				paramNotes = new SqlParameter("@" + TAG_NOTES, DBNull.Value);
			}
			paramNotes.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramNotes);

		}

	}
}

