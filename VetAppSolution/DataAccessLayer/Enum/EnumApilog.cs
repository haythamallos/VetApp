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
	/// File:  EnumApilog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/16/2016	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumApilog
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
		public static readonly string ENTITY_NAME = "EnumApilog"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lApilogID = 0;
		private long _lApikeyID = 0;
		private long _lRefNum = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private string _strMsgsource = null;
		private string _strTrace = null;
		private bool? _bIsSuccess = null;
		private bool? _bInProgress = null;
		private string _strHttpStatusStr = null;
		private long _lHttpStatusNum = 0;
		private string _strMsgtxt = null;
		private string _strReqtxt = null;
		private string _strResptxt = null;
		private long _lDurationInMs = 0;
		private DateTime _dtBeginCallStartTime = new DateTime();
		private DateTime _dtEndCallStartTime = new DateTime();
		private DateTime _dtBeginCallEndTime = new DateTime();
		private DateTime _dtEndCallEndTime = new DateTime();
		private string _strSearchtext = null;
		private string _strAuthuserid = null;
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "apilog_id"; //Table id field name
		/// <summary>ApilogID Attribute type string</summary>
		public static readonly string TAG_APILOG_ID = "ApilogID"; //Attribute ApilogID  name
		/// <summary>ApikeyID Attribute type string</summary>
		public static readonly string TAG_APIKEY_ID = "ApikeyID"; //Attribute ApikeyID  name
		/// <summary>RefNum Attribute type string</summary>
		public static readonly string TAG_REF_NUM = "RefNum"; //Attribute RefNum  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>Msgsource Attribute type string</summary>
		public static readonly string TAG_MSGSOURCE = "Msgsource"; //Attribute Msgsource  name
		/// <summary>Trace Attribute type string</summary>
		public static readonly string TAG_TRACE = "Trace"; //Attribute Trace  name
		/// <summary>IsSuccess Attribute type string</summary>
		public static readonly string TAG_IS_SUCCESS = "IsSuccess"; //Attribute IsSuccess  name
		/// <summary>InProgress Attribute type string</summary>
		public static readonly string TAG_IN_PROGRESS = "InProgress"; //Attribute InProgress  name
		/// <summary>HttpStatusStr Attribute type string</summary>
		public static readonly string TAG_HTTP_STATUS_STR = "HttpStatusStr"; //Attribute HttpStatusStr  name
		/// <summary>HttpStatusNum Attribute type string</summary>
		public static readonly string TAG_HTTP_STATUS_NUM = "HttpStatusNum"; //Attribute HttpStatusNum  name
		/// <summary>Msgtxt Attribute type string</summary>
		public static readonly string TAG_MSGTXT = "Msgtxt"; //Attribute Msgtxt  name
		/// <summary>Reqtxt Attribute type string</summary>
		public static readonly string TAG_REQTXT = "Reqtxt"; //Attribute Reqtxt  name
		/// <summary>Resptxt Attribute type string</summary>
		public static readonly string TAG_RESPTXT = "Resptxt"; //Attribute Resptxt  name
		/// <summary>DurationInMs Attribute type string</summary>
		public static readonly string TAG_DURATION_IN_MS = "DurationInMs"; //Attribute DurationInMs  name
		/// <summary>CallStartTime Attribute type string</summary>
		public static readonly string TAG_BEGIN_CALL_START_TIME = "BeginCallStartTime"; //Attribute CallStartTime  name
		/// <summary>EndCallStartTime Attribute type string</summary>
		public static readonly string TAG_END_CALL_START_TIME = "EndCallStartTime"; //Attribute CallStartTime  name
		/// <summary>CallEndTime Attribute type string</summary>
		public static readonly string TAG_BEGIN_CALL_END_TIME = "BeginCallEndTime"; //Attribute CallEndTime  name
		/// <summary>EndCallEndTime Attribute type string</summary>
		public static readonly string TAG_END_CALL_END_TIME = "EndCallEndTime"; //Attribute CallEndTime  name
		/// <summary>Searchtext Attribute type string</summary>
		public static readonly string TAG_SEARCHTEXT = "Searchtext"; //Attribute Searchtext  name
		/// <summary>Authuserid Attribute type string</summary>
		public static readonly string TAG_AUTHUSERID = "Authuserid"; //Attribute Authuserid  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spApilogEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Apilog Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>ApilogID is a Property in the Apilog Class of type long</summary>
		public long ApilogID 
		{
			get{return _lApilogID;}
			set{_lApilogID = value;}
		}
		/// <summary>ApikeyID is a Property in the Apilog Class of type long</summary>
		public long ApikeyID 
		{
			get{return _lApikeyID;}
			set{_lApikeyID = value;}
		}
		/// <summary>RefNum is a Property in the Apilog Class of type long</summary>
		public long RefNum 
		{
			get{return _lRefNum;}
			set{_lRefNum = value;}
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
		/// <summary>Msgsource is a Property in the Apilog Class of type String</summary>
		public string Msgsource 
		{
			get{return _strMsgsource;}
			set{_strMsgsource = value;}
		}
		/// <summary>Trace is a Property in the Apilog Class of type String</summary>
		public string Trace 
		{
			get{return _strTrace;}
			set{_strTrace = value;}
		}
		/// <summary>IsSuccess is a Property in the Apilog Class of type bool</summary>
		public bool? IsSuccess 
		{
			get{return _bIsSuccess;}
			set{_bIsSuccess = value;}
		}
		/// <summary>InProgress is a Property in the Apilog Class of type bool</summary>
		public bool? InProgress 
		{
			get{return _bInProgress;}
			set{_bInProgress = value;}
		}
		/// <summary>HttpStatusStr is a Property in the Apilog Class of type String</summary>
		public string HttpStatusStr 
		{
			get{return _strHttpStatusStr;}
			set{_strHttpStatusStr = value;}
		}
		/// <summary>HttpStatusNum is a Property in the Apilog Class of type long</summary>
		public long HttpStatusNum 
		{
			get{return _lHttpStatusNum;}
			set{_lHttpStatusNum = value;}
		}
		/// <summary>Msgtxt is a Property in the Apilog Class of type String</summary>
		public string Msgtxt 
		{
			get{return _strMsgtxt;}
			set{_strMsgtxt = value;}
		}
		/// <summary>Reqtxt is a Property in the Apilog Class of type String</summary>
		public string Reqtxt 
		{
			get{return _strReqtxt;}
			set{_strReqtxt = value;}
		}
		/// <summary>Resptxt is a Property in the Apilog Class of type String</summary>
		public string Resptxt 
		{
			get{return _strResptxt;}
			set{_strResptxt = value;}
		}
		/// <summary>DurationInMs is a Property in the Apilog Class of type long</summary>
		public long DurationInMs 
		{
			get{return _lDurationInMs;}
			set{_lDurationInMs = value;}
		}
		/// <summary>Property CallStartTime. Type: DateTime</summary>
		public DateTime BeginCallStartTime
		{
			get{return _dtBeginCallStartTime;}
			set{_dtBeginCallStartTime = value;}
		}
		/// <summary>Property CallStartTime. Type: DateTime</summary>
		public DateTime EndCallStartTime
		{
			get{return _dtEndCallStartTime;}
			set{_dtEndCallStartTime = value;}
		}
		/// <summary>Property CallEndTime. Type: DateTime</summary>
		public DateTime BeginCallEndTime
		{
			get{return _dtBeginCallEndTime;}
			set{_dtBeginCallEndTime = value;}
		}
		/// <summary>Property CallEndTime. Type: DateTime</summary>
		public DateTime EndCallEndTime
		{
			get{return _dtEndCallEndTime;}
			set{_dtEndCallEndTime = value;}
		}
		/// <summary>Searchtext is a Property in the Apilog Class of type String</summary>
		public string Searchtext 
		{
			get{return _strSearchtext;}
			set{_strSearchtext = value;}
		}
		/// <summary>Authuserid is a Property in the Apilog Class of type String</summary>
		public string Authuserid 
		{
			get{return _strAuthuserid;}
			set{_strAuthuserid = value;}
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
		public EnumApilog()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumApilog(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type Apilog. Returns the next Apilog in the list</summary>
		private Apilog _nextTransaction
		{
			get
			{
				Apilog o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Apilog(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the Apilog Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_APILOG_ID + ":  " + ApilogID.ToString() + "\n");
			sbReturn.Append(TAG_APIKEY_ID + ":  " + ApikeyID + "\n");
			sbReturn.Append(TAG_REF_NUM + ":  " + RefNum + "\n");
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
			sbReturn.Append(TAG_MSGSOURCE + ":  " + Msgsource + "\n");
			sbReturn.Append(TAG_TRACE + ":  " + Trace + "\n");
			sbReturn.Append(TAG_IS_SUCCESS + ":  " + IsSuccess + "\n");
			sbReturn.Append(TAG_IN_PROGRESS + ":  " + InProgress + "\n");
			sbReturn.Append(TAG_HTTP_STATUS_STR + ":  " + HttpStatusStr + "\n");
			sbReturn.Append(TAG_HTTP_STATUS_NUM + ":  " + HttpStatusNum + "\n");
			sbReturn.Append(TAG_MSGTXT + ":  " + Msgtxt + "\n");
			sbReturn.Append(TAG_REQTXT + ":  " + Reqtxt + "\n");
			sbReturn.Append(TAG_RESPTXT + ":  " + Resptxt + "\n");
			sbReturn.Append(TAG_DURATION_IN_MS + ":  " + DurationInMs + "\n");
			if (!dtNull.Equals(BeginCallStartTime))
			{
				sbReturn.Append(TAG_BEGIN_CALL_START_TIME + ":  " + BeginCallStartTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_CALL_START_TIME + ":\n");
			}
			if (!dtNull.Equals(EndCallStartTime))
			{
				sbReturn.Append(TAG_END_CALL_START_TIME + ":  " + EndCallStartTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_CALL_START_TIME + ":\n");
			}
			if (!dtNull.Equals(BeginCallEndTime))
			{
				sbReturn.Append(TAG_BEGIN_CALL_END_TIME + ":  " + BeginCallEndTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_CALL_END_TIME + ":\n");
			}
			if (!dtNull.Equals(EndCallEndTime))
			{
				sbReturn.Append(TAG_END_CALL_END_TIME + ":  " + EndCallEndTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_CALL_END_TIME + ":\n");
			}
			sbReturn.Append(TAG_SEARCHTEXT + ":  " + Searchtext + "\n");
			sbReturn.Append(TAG_AUTHUSERID + ":  " + Authuserid + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Apilog</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_APILOG_ID + ">" + ApilogID + "</" + TAG_APILOG_ID + ">\n");
			sbReturn.Append("<" + TAG_APIKEY_ID + ">" + ApikeyID + "</" + TAG_APIKEY_ID + ">\n");
			sbReturn.Append("<" + TAG_REF_NUM + ">" + RefNum + "</" + TAG_REF_NUM + ">\n");
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
			sbReturn.Append("<" + TAG_MSGSOURCE + ">" + Msgsource + "</" + TAG_MSGSOURCE + ">\n");
			sbReturn.Append("<" + TAG_TRACE + ">" + Trace + "</" + TAG_TRACE + ">\n");
			sbReturn.Append("<" + TAG_IS_SUCCESS + ">" + IsSuccess + "</" + TAG_IS_SUCCESS + ">\n");
			sbReturn.Append("<" + TAG_IN_PROGRESS + ">" + InProgress + "</" + TAG_IN_PROGRESS + ">\n");
			sbReturn.Append("<" + TAG_HTTP_STATUS_STR + ">" + HttpStatusStr + "</" + TAG_HTTP_STATUS_STR + ">\n");
			sbReturn.Append("<" + TAG_HTTP_STATUS_NUM + ">" + HttpStatusNum + "</" + TAG_HTTP_STATUS_NUM + ">\n");
			sbReturn.Append("<" + TAG_MSGTXT + ">" + Msgtxt + "</" + TAG_MSGTXT + ">\n");
			sbReturn.Append("<" + TAG_REQTXT + ">" + Reqtxt + "</" + TAG_REQTXT + ">\n");
			sbReturn.Append("<" + TAG_RESPTXT + ">" + Resptxt + "</" + TAG_RESPTXT + ">\n");
			sbReturn.Append("<" + TAG_DURATION_IN_MS + ">" + DurationInMs + "</" + TAG_DURATION_IN_MS + ">\n");
			if (!dtNull.Equals(BeginCallStartTime))
			{
				sbReturn.Append("<" + TAG_BEGIN_CALL_START_TIME + ">" + BeginCallStartTime.ToString() + "</" + TAG_BEGIN_CALL_START_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_CALL_START_TIME + "></" + TAG_BEGIN_CALL_START_TIME + ">\n");
			}
			if (!dtNull.Equals(EndCallStartTime))
			{
				sbReturn.Append("<" + TAG_END_CALL_START_TIME + ">" + EndCallStartTime.ToString() + "</" + TAG_END_CALL_START_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_CALL_START_TIME + "></" + TAG_END_CALL_START_TIME + ">\n");
			}
			if (!dtNull.Equals(BeginCallEndTime))
			{
				sbReturn.Append("<" + TAG_BEGIN_CALL_END_TIME + ">" + BeginCallEndTime.ToString() + "</" + TAG_BEGIN_CALL_END_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_CALL_END_TIME + "></" + TAG_BEGIN_CALL_END_TIME + ">\n");
			}
			if (!dtNull.Equals(EndCallEndTime))
			{
				sbReturn.Append("<" + TAG_END_CALL_END_TIME + ">" + EndCallEndTime.ToString() + "</" + TAG_END_CALL_END_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_CALL_END_TIME + "></" + TAG_END_CALL_END_TIME + ">\n");
			}
			sbReturn.Append("<" + TAG_SEARCHTEXT + ">" + Searchtext + "</" + TAG_SEARCHTEXT + ">\n");
			sbReturn.Append("<" + TAG_AUTHUSERID + ">" + Authuserid + "</" + TAG_AUTHUSERID + ">\n");
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
				xResultNode = xNode.SelectSingleNode(TAG_APILOG_ID);
				strTmp = xResultNode.InnerText;
				ApilogID = (long) Convert.ToInt32(strTmp);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_APIKEY_ID);
				ApikeyID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			ApikeyID = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_REF_NUM);
				RefNum = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			RefNum = 0;
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
				xResultNode = xNode.SelectSingleNode(TAG_TRACE);
				Trace = xResultNode.InnerText;
				if (Trace.Trim().Length == 0)
					Trace = null;
			}
			catch  
			{
				Trace = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_IS_SUCCESS);
				IsSuccess = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			IsSuccess = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_IN_PROGRESS);
				InProgress = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			InProgress = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_HTTP_STATUS_STR);
				HttpStatusStr = xResultNode.InnerText;
				if (HttpStatusStr.Trim().Length == 0)
					HttpStatusStr = null;
			}
			catch  
			{
				HttpStatusStr = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_HTTP_STATUS_NUM);
				HttpStatusNum = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			HttpStatusNum = 0;
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

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_REQTXT);
				Reqtxt = xResultNode.InnerText;
				if (Reqtxt.Trim().Length == 0)
					Reqtxt = null;
			}
			catch  
			{
				Reqtxt = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_RESPTXT);
				Resptxt = xResultNode.InnerText;
				if (Resptxt.Trim().Length == 0)
					Resptxt = null;
			}
			catch  
			{
				Resptxt = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_DURATION_IN_MS);
				DurationInMs = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			DurationInMs = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_CALL_START_TIME);
				BeginCallStartTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_CALL_START_TIME);
				EndCallStartTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_CALL_END_TIME);
				BeginCallEndTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_CALL_END_TIME);
				EndCallEndTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_SEARCHTEXT);
				Searchtext = xResultNode.InnerText;
				if (Searchtext.Trim().Length == 0)
					Searchtext = null;
			}
			catch  
			{
				Searchtext = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_AUTHUSERID);
				Authuserid = xResultNode.InnerText;
				if (Authuserid.Trim().Length == 0)
					Authuserid = null;
			}
			catch  
			{
				Authuserid = null;
			}
		}
		/// <summary>Prompt for values</summary>
		public void Prompt()
		{
			try 
			{
				Console.WriteLine(TAG_APIKEY_ID + ":  ");
				try
				{
					ApikeyID = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					ApikeyID = 0;
				}

				Console.WriteLine(TAG_REF_NUM + ":  ");
				try
				{
					RefNum = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					RefNum = 0;
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


				Console.WriteLine(TAG_MSGSOURCE + ":  ");
				Msgsource = Console.ReadLine();
				if (Msgsource.Length == 0)
				{
					Msgsource = null;
				}

				Console.WriteLine(TAG_TRACE + ":  ");
				Trace = Console.ReadLine();
				if (Trace.Length == 0)
				{
					Trace = null;
				}
				Console.WriteLine(TAG_IS_SUCCESS + ":  ");
				try
				{
					IsSuccess = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					IsSuccess = false;
				}

				Console.WriteLine(TAG_IN_PROGRESS + ":  ");
				try
				{
					InProgress = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					InProgress = false;
				}


				Console.WriteLine(TAG_HTTP_STATUS_STR + ":  ");
				HttpStatusStr = Console.ReadLine();
				if (HttpStatusStr.Length == 0)
				{
					HttpStatusStr = null;
				}
				Console.WriteLine(TAG_HTTP_STATUS_NUM + ":  ");
				try
				{
					HttpStatusNum = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					HttpStatusNum = 0;
				}


				Console.WriteLine(TAG_MSGTXT + ":  ");
				Msgtxt = Console.ReadLine();
				if (Msgtxt.Length == 0)
				{
					Msgtxt = null;
				}

				Console.WriteLine(TAG_REQTXT + ":  ");
				Reqtxt = Console.ReadLine();
				if (Reqtxt.Length == 0)
				{
					Reqtxt = null;
				}

				Console.WriteLine(TAG_RESPTXT + ":  ");
				Resptxt = Console.ReadLine();
				if (Resptxt.Length == 0)
				{
					Resptxt = null;
				}
				Console.WriteLine(TAG_DURATION_IN_MS + ":  ");
				try
				{
					DurationInMs = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					DurationInMs = 0;
				}

				Console.WriteLine(TAG_BEGIN_CALL_START_TIME + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginCallStartTime = DateTime.Parse(s);
				}
				catch 
				{
					BeginCallStartTime = new DateTime();
				}

				Console.WriteLine(TAG_END_CALL_START_TIME + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndCallStartTime = DateTime.Parse(s);
				}
				catch  
				{
					EndCallStartTime = new DateTime();
				}

				Console.WriteLine(TAG_BEGIN_CALL_END_TIME + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginCallEndTime = DateTime.Parse(s);
				}
				catch 
				{
					BeginCallEndTime = new DateTime();
				}

				Console.WriteLine(TAG_END_CALL_END_TIME + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndCallEndTime = DateTime.Parse(s);
				}
				catch  
				{
					EndCallEndTime = new DateTime();
				}


				Console.WriteLine(TAG_SEARCHTEXT + ":  ");
				Searchtext = Console.ReadLine();
				if (Searchtext.Length == 0)
				{
					Searchtext = null;
				}

				Console.WriteLine(TAG_AUTHUSERID + ":  ");
				Authuserid = Console.ReadLine();
				if (Authuserid.Length == 0)
				{
					Authuserid = null;
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
			SqlParameter paramApilogID = null;
			SqlParameter paramApikeyID = null;
			SqlParameter paramRefNum = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramMsgsource = null;
			SqlParameter paramTrace = null;
			SqlParameter paramIsSuccess = null;
			SqlParameter paramInProgress = null;
			SqlParameter paramHttpStatusStr = null;
			SqlParameter paramHttpStatusNum = null;
			SqlParameter paramMsgtxt = null;
			SqlParameter paramReqtxt = null;
			SqlParameter paramResptxt = null;
			SqlParameter paramDurationInMs = null;
			SqlParameter paramBeginCallStartTime = null;
			SqlParameter paramEndCallStartTime = null;
			SqlParameter paramBeginCallEndTime = null;
			SqlParameter paramEndCallEndTime = null;
			SqlParameter paramSearchtext = null;
			SqlParameter paramAuthuserid = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramApilogID = new SqlParameter("@" + TAG_APILOG_ID, ApilogID);
				sbLog.Append(TAG_APILOG_ID + "=" + ApilogID + "\n");
				paramApilogID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramApilogID);

				paramApikeyID = new SqlParameter("@" + TAG_APIKEY_ID, ApikeyID);
				sbLog.Append(TAG_APIKEY_ID + "=" + ApikeyID + "\n");
				paramApikeyID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramApikeyID);
				paramRefNum = new SqlParameter("@" + TAG_REF_NUM, RefNum);
				sbLog.Append(TAG_REF_NUM + "=" + RefNum + "\n");
				paramRefNum.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramRefNum);

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

			// Setup the trace text param
			if ( Trace != null )
			{
				paramTrace = new SqlParameter("@" + TAG_TRACE, Trace);
				sbLog.Append(TAG_TRACE + "=" + Trace + "\n");
			}
			else
			{
				paramTrace = new SqlParameter("@" + TAG_TRACE, DBNull.Value);
			}
			paramTrace.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramTrace);

				paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
				sbLog.Append(TAG_IS_SUCCESS + "=" + IsSuccess + "\n");
				paramIsSuccess.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsSuccess);
				paramInProgress = new SqlParameter("@" + TAG_IN_PROGRESS, InProgress);
				sbLog.Append(TAG_IN_PROGRESS + "=" + InProgress + "\n");
				paramInProgress.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramInProgress);
			// Setup the http status str text param
			if ( HttpStatusStr != null )
			{
				paramHttpStatusStr = new SqlParameter("@" + TAG_HTTP_STATUS_STR, HttpStatusStr);
				sbLog.Append(TAG_HTTP_STATUS_STR + "=" + HttpStatusStr + "\n");
			}
			else
			{
				paramHttpStatusStr = new SqlParameter("@" + TAG_HTTP_STATUS_STR, DBNull.Value);
			}
			paramHttpStatusStr.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramHttpStatusStr);

				paramHttpStatusNum = new SqlParameter("@" + TAG_HTTP_STATUS_NUM, HttpStatusNum);
				sbLog.Append(TAG_HTTP_STATUS_NUM + "=" + HttpStatusNum + "\n");
				paramHttpStatusNum.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramHttpStatusNum);

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

			// Setup the reqtxt text param
			if ( Reqtxt != null )
			{
				paramReqtxt = new SqlParameter("@" + TAG_REQTXT, Reqtxt);
				sbLog.Append(TAG_REQTXT + "=" + Reqtxt + "\n");
			}
			else
			{
				paramReqtxt = new SqlParameter("@" + TAG_REQTXT, DBNull.Value);
			}
			paramReqtxt.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramReqtxt);

			// Setup the resptxt text param
			if ( Resptxt != null )
			{
				paramResptxt = new SqlParameter("@" + TAG_RESPTXT, Resptxt);
				sbLog.Append(TAG_RESPTXT + "=" + Resptxt + "\n");
			}
			else
			{
				paramResptxt = new SqlParameter("@" + TAG_RESPTXT, DBNull.Value);
			}
			paramResptxt.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramResptxt);

				paramDurationInMs = new SqlParameter("@" + TAG_DURATION_IN_MS, DurationInMs);
				sbLog.Append(TAG_DURATION_IN_MS + "=" + DurationInMs + "\n");
				paramDurationInMs.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramDurationInMs);

			// Setup the call start time param
			if (!dtNull.Equals(BeginCallStartTime))
			{
				paramBeginCallStartTime = new SqlParameter("@" + TAG_BEGIN_CALL_START_TIME, BeginCallStartTime);
			}
			else
			{
				paramBeginCallStartTime = new SqlParameter("@" + TAG_BEGIN_CALL_START_TIME, DBNull.Value);
			}
			paramBeginCallStartTime.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginCallStartTime);

			if (!dtNull.Equals(EndCallStartTime))
			{
				paramEndCallStartTime = new SqlParameter("@" + TAG_END_CALL_START_TIME, EndCallStartTime);
			}
			else
			{
				paramEndCallStartTime = new SqlParameter("@" + TAG_END_CALL_START_TIME, DBNull.Value);
			}
			paramEndCallStartTime.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndCallStartTime);

			// Setup the call end time param
			if (!dtNull.Equals(BeginCallEndTime))
			{
				paramBeginCallEndTime = new SqlParameter("@" + TAG_BEGIN_CALL_END_TIME, BeginCallEndTime);
			}
			else
			{
				paramBeginCallEndTime = new SqlParameter("@" + TAG_BEGIN_CALL_END_TIME, DBNull.Value);
			}
			paramBeginCallEndTime.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginCallEndTime);

			if (!dtNull.Equals(EndCallEndTime))
			{
				paramEndCallEndTime = new SqlParameter("@" + TAG_END_CALL_END_TIME, EndCallEndTime);
			}
			else
			{
				paramEndCallEndTime = new SqlParameter("@" + TAG_END_CALL_END_TIME, DBNull.Value);
			}
			paramEndCallEndTime.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndCallEndTime);

			// Setup the searchtext text param
			if ( Searchtext != null )
			{
				paramSearchtext = new SqlParameter("@" + TAG_SEARCHTEXT, Searchtext);
				sbLog.Append(TAG_SEARCHTEXT + "=" + Searchtext + "\n");
			}
			else
			{
				paramSearchtext = new SqlParameter("@" + TAG_SEARCHTEXT, DBNull.Value);
			}
			paramSearchtext.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramSearchtext);

			// Setup the authuserid text param
			if ( Authuserid != null )
			{
				paramAuthuserid = new SqlParameter("@" + TAG_AUTHUSERID, Authuserid);
				sbLog.Append(TAG_AUTHUSERID + "=" + Authuserid + "\n");
			}
			else
			{
				paramAuthuserid = new SqlParameter("@" + TAG_AUTHUSERID, DBNull.Value);
			}
			paramAuthuserid.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramAuthuserid);

		}

	}
}

