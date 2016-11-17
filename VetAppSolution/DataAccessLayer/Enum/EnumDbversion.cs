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
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  EnumDbversion.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/16/2016	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumDbversion
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
		public static readonly string ENTITY_NAME = "EnumDbversion"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lDbversionID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private long _lMajorNum = 0;
		private long _lMinorNum = 0;
		private string _strNotes = null;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "dbversion_id"; //Table id field name
		/// <summary>DbversionID Attribute type string</summary>
		public static readonly string TAG_DBVERSION_ID = "DbversionID"; //Attribute DbversionID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>MajorNum Attribute type string</summary>
		public static readonly string TAG_MAJOR_NUM = "MajorNum"; //Attribute MajorNum  name
		/// <summary>MinorNum Attribute type string</summary>
		public static readonly string TAG_MINOR_NUM = "MinorNum"; //Attribute MinorNum  name
		/// <summary>Notes Attribute type string</summary>
		public static readonly string TAG_NOTES = "Notes"; //Attribute Notes  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spDbversionEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Dbversion Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>DbversionID is a Property in the Dbversion Class of type long</summary>
		public long DbversionID 
		{
			get{return _lDbversionID;}
			set{_lDbversionID = value;}
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
		public EnumDbversion()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumDbversion(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type Dbversion. Returns the next Dbversion in the list</summary>
		private Dbversion _nextTransaction
		{
			get
			{
				Dbversion o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Dbversion(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the Dbversion Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_DBVERSION_ID + ":  " + DbversionID.ToString() + "\n");
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
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_DBVERSION_ID + ">" + DbversionID + "</" + TAG_DBVERSION_ID + ">\n");
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
			sbReturn.Append("<" + TAG_MAJOR_NUM + ">" + MajorNum + "</" + TAG_MAJOR_NUM + ">\n");
			sbReturn.Append("<" + TAG_MINOR_NUM + ">" + MinorNum + "</" + TAG_MINOR_NUM + ">\n");
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
				xResultNode = xNode.SelectSingleNode(TAG_DBVERSION_ID);
				strTmp = xResultNode.InnerText;
				DbversionID = (long) Convert.ToInt32(strTmp);
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

				Console.WriteLine(TAG_MAJOR_NUM + ":  ");
				try
				{
					MajorNum = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					MajorNum = 0;
				}

				Console.WriteLine(TAG_MINOR_NUM + ":  ");
				try
				{
					MinorNum = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					MinorNum = 0;
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
			SqlParameter paramDbversionID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramMajorNum = null;
			SqlParameter paramMinorNum = null;
			SqlParameter paramNotes = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramDbversionID = new SqlParameter("@" + TAG_DBVERSION_ID, DbversionID);
				sbLog.Append(TAG_DBVERSION_ID + "=" + DbversionID + "\n");
				paramDbversionID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramDbversionID);

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

				paramMajorNum = new SqlParameter("@" + TAG_MAJOR_NUM, MajorNum);
				sbLog.Append(TAG_MAJOR_NUM + "=" + MajorNum + "\n");
				paramMajorNum.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramMajorNum);

				paramMinorNum = new SqlParameter("@" + TAG_MINOR_NUM, MinorNum);
				sbLog.Append(TAG_MINOR_NUM + "=" + MinorNum + "\n");
				paramMinorNum.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramMinorNum);

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

