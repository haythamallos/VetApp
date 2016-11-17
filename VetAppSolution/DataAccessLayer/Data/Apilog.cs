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
	/// File:  Apilog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/16/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Apilog database table.
	/// </summary>
	public class Apilog
	{
		//Attributes
		/// <summary>ApilogID Attribute type String</summary>
		private long _lApilogID = 0;
		/// <summary>ApikeyID Attribute type String</summary>
		private long _lApikeyID = 0;
		/// <summary>RefNum Attribute type String</summary>
		private long _lRefNum = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>Msgsource Attribute type String</summary>
		private string _strMsgsource = null;
		/// <summary>Trace Attribute type String</summary>
		private string _strTrace = null;
		/// <summary>IsSuccess Attribute type String</summary>
		private bool? _bIsSuccess = null;
		/// <summary>InProgress Attribute type String</summary>
		private bool? _bInProgress = null;
		/// <summary>HttpStatusStr Attribute type String</summary>
		private string _strHttpStatusStr = null;
		/// <summary>HttpStatusNum Attribute type String</summary>
		private long _lHttpStatusNum = 0;
		/// <summary>Msgtxt Attribute type String</summary>
		private string _strMsgtxt = null;
		/// <summary>Reqtxt Attribute type String</summary>
		private string _strReqtxt = null;
		/// <summary>Resptxt Attribute type String</summary>
		private string _strResptxt = null;
		/// <summary>DurationInMs Attribute type String</summary>
		private long _lDurationInMs = 0;
		/// <summary>CallStartTime Attribute type String</summary>
		private DateTime _dtCallStartTime = dtNull;
		/// <summary>CallEndTime Attribute type String</summary>
		private DateTime _dtCallEndTime = dtNull;
		/// <summary>Searchtext Attribute type String</summary>
		private string _strSearchtext = null;
		/// <summary>Authuserid Attribute type String</summary>
		private string _strAuthuserid = null;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Apilog and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Apilog"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "apilog_id"; //Table id field name
		/// <summary>apikey_id Database field </summary>
		public static readonly string DB_FIELD_APIKEY_ID = "apikey_id"; //Table ApikeyID field name
		/// <summary>ref_num Database field </summary>
		public static readonly string DB_FIELD_REF_NUM = "ref_num"; //Table RefNum field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>msgsource Database field </summary>
		public static readonly string DB_FIELD_MSGSOURCE = "msgsource"; //Table Msgsource field name
		/// <summary>trace Database field </summary>
		public static readonly string DB_FIELD_TRACE = "trace"; //Table Trace field name
		/// <summary>is_success Database field </summary>
		public static readonly string DB_FIELD_IS_SUCCESS = "is_success"; //Table IsSuccess field name
		/// <summary>in_progress Database field </summary>
		public static readonly string DB_FIELD_IN_PROGRESS = "in_progress"; //Table InProgress field name
		/// <summary>http_status_str Database field </summary>
		public static readonly string DB_FIELD_HTTP_STATUS_STR = "http_status_str"; //Table HttpStatusStr field name
		/// <summary>http_status_num Database field </summary>
		public static readonly string DB_FIELD_HTTP_STATUS_NUM = "http_status_num"; //Table HttpStatusNum field name
		/// <summary>msgtxt Database field </summary>
		public static readonly string DB_FIELD_MSGTXT = "msgtxt"; //Table Msgtxt field name
		/// <summary>reqtxt Database field </summary>
		public static readonly string DB_FIELD_REQTXT = "reqtxt"; //Table Reqtxt field name
		/// <summary>resptxt Database field </summary>
		public static readonly string DB_FIELD_RESPTXT = "resptxt"; //Table Resptxt field name
		/// <summary>duration_in_ms Database field </summary>
		public static readonly string DB_FIELD_DURATION_IN_MS = "duration_in_ms"; //Table DurationInMs field name
		/// <summary>call_start_time Database field </summary>
		public static readonly string DB_FIELD_CALL_START_TIME = "call_start_time"; //Table CallStartTime field name
		/// <summary>call_end_time Database field </summary>
		public static readonly string DB_FIELD_CALL_END_TIME = "call_end_time"; //Table CallEndTime field name
		/// <summary>searchtext Database field </summary>
		public static readonly string DB_FIELD_SEARCHTEXT = "searchtext"; //Table Searchtext field name
		/// <summary>authuserid Database field </summary>
		public static readonly string DB_FIELD_AUTHUSERID = "authuserid"; //Table Authuserid field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "ApilogID"; //Attribute id  name
		/// <summary>ApikeyID Attribute type string</summary>
		public static readonly string TAG_APIKEY_ID = "ApikeyID"; //Table ApikeyID field name
		/// <summary>RefNum Attribute type string</summary>
		public static readonly string TAG_REF_NUM = "RefNum"; //Table RefNum field name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>Msgsource Attribute type string</summary>
		public static readonly string TAG_MSGSOURCE = "Msgsource"; //Table Msgsource field name
		/// <summary>Trace Attribute type string</summary>
		public static readonly string TAG_TRACE = "Trace"; //Table Trace field name
		/// <summary>IsSuccess Attribute type string</summary>
		public static readonly string TAG_IS_SUCCESS = "IsSuccess"; //Table IsSuccess field name
		/// <summary>InProgress Attribute type string</summary>
		public static readonly string TAG_IN_PROGRESS = "InProgress"; //Table InProgress field name
		/// <summary>HttpStatusStr Attribute type string</summary>
		public static readonly string TAG_HTTP_STATUS_STR = "HttpStatusStr"; //Table HttpStatusStr field name
		/// <summary>HttpStatusNum Attribute type string</summary>
		public static readonly string TAG_HTTP_STATUS_NUM = "HttpStatusNum"; //Table HttpStatusNum field name
		/// <summary>Msgtxt Attribute type string</summary>
		public static readonly string TAG_MSGTXT = "Msgtxt"; //Table Msgtxt field name
		/// <summary>Reqtxt Attribute type string</summary>
		public static readonly string TAG_REQTXT = "Reqtxt"; //Table Reqtxt field name
		/// <summary>Resptxt Attribute type string</summary>
		public static readonly string TAG_RESPTXT = "Resptxt"; //Table Resptxt field name
		/// <summary>DurationInMs Attribute type string</summary>
		public static readonly string TAG_DURATION_IN_MS = "DurationInMs"; //Table DurationInMs field name
		/// <summary>CallStartTime Attribute type string</summary>
		public static readonly string TAG_CALL_START_TIME = "CallStartTime"; //Table CallStartTime field name
		/// <summary>CallEndTime Attribute type string</summary>
		public static readonly string TAG_CALL_END_TIME = "CallEndTime"; //Table CallEndTime field name
		/// <summary>Searchtext Attribute type string</summary>
		public static readonly string TAG_SEARCHTEXT = "Searchtext"; //Table Searchtext field name
		/// <summary>Authuserid Attribute type string</summary>
		public static readonly string TAG_AUTHUSERID = "Authuserid"; //Table Authuserid field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spApilogInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spApilogUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spApilogDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spApilogLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spApilogExist"; //Exist sp name

		//properties
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
		/// <summary>DateCreated is a Property in the Apilog Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
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
		/// <summary>CallStartTime is a Property in the Apilog Class of type DateTime</summary>
		public DateTime CallStartTime 
		{
			get{return _dtCallStartTime;}
			set{_dtCallStartTime = value;}
		}
		/// <summary>CallEndTime is a Property in the Apilog Class of type DateTime</summary>
		public DateTime CallEndTime 
		{
			get{return _dtCallEndTime;}
			set{_dtCallEndTime = value;}
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


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Apilog and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Apilog and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Apilog empty constructor</summary>
		public Apilog()
		{
		}
		/// <summary>Apilog constructor takes ApilogID and a SqlConnection</summary>
		public Apilog(long l, SqlConnection conn) 
		{
			ApilogID = l;
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
		/// <summary>Apilog Constructor takes pStrData and Config</summary>
		public Apilog(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Apilog Constructor takes SqlDataReader</summary>
		public Apilog(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Apilog Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + ApilogID.ToString() + "\n");
			sbReturn.Append(TAG_APIKEY_ID + ":  " + ApikeyID + "\n");
			sbReturn.Append(TAG_REF_NUM + ":  " + RefNum + "\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_CREATED + ":\n");
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
			if (!dtNull.Equals(CallStartTime))
			{
				sbReturn.Append(TAG_CALL_START_TIME + ":  " + CallStartTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_CALL_START_TIME + ":\n");
			}
			if (!dtNull.Equals(CallEndTime))
			{
				sbReturn.Append(TAG_CALL_END_TIME + ":  " + CallEndTime.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_CALL_END_TIME + ":\n");
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
			sbReturn.Append("<Apilog>\n");
			sbReturn.Append("<" + TAG_ID + ">" + ApilogID + "</" + TAG_ID + ">\n");
			sbReturn.Append("<" + TAG_APIKEY_ID + ">" + ApikeyID + "</" + TAG_APIKEY_ID + ">\n");
			sbReturn.Append("<" + TAG_REF_NUM + ">" + RefNum + "</" + TAG_REF_NUM + ">\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
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
			if (!dtNull.Equals(CallStartTime))
			{
				sbReturn.Append("<" + TAG_CALL_START_TIME + ">" + CallStartTime.ToString() + "</" + TAG_CALL_START_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_CALL_START_TIME + "></" + TAG_CALL_START_TIME + ">\n");
			}
			if (!dtNull.Equals(CallEndTime))
			{
				sbReturn.Append("<" + TAG_CALL_END_TIME + ">" + CallEndTime.ToString() + "</" + TAG_CALL_END_TIME + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_CALL_END_TIME + "></" + TAG_CALL_END_TIME + ">\n");
			}
			sbReturn.Append("<" + TAG_SEARCHTEXT + ">" + Searchtext + "</" + TAG_SEARCHTEXT + ">\n");
			sbReturn.Append("<" + TAG_AUTHUSERID + ">" + Authuserid + "</" + TAG_AUTHUSERID + ">\n");
			sbReturn.Append("</Apilog>" + "\n");

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
				xResultNode = xNode.SelectSingleNode(TAG_DATE_CREATED);
				DateCreated = DateTime.Parse(xResultNode.InnerText);
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
				xResultNode = xNode.SelectSingleNode(TAG_TRACE);
				Trace = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
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
			}
			catch  
			{
				xResultNode = null;
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
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_REQTXT);
				Reqtxt = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_RESPTXT);
				Resptxt = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
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
				xResultNode = xNode.SelectSingleNode(TAG_CALL_START_TIME);
				CallStartTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_CALL_END_TIME);
				CallEndTime = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_SEARCHTEXT);
				Searchtext = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_AUTHUSERID);
				Authuserid = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}
		}
		/// <summary>Calls sqlLoad() method which gets record from database with apilog_id equal to the current object's ApilogID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where apilog_id equal to the current object's ApilogID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where apilog_id equal to the current object's ApilogID </summary>
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

				Console.WriteLine(Apilog.TAG_APIKEY_ID + ":  ");
				ApikeyID = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Apilog.TAG_REF_NUM + ":  ");
				RefNum = (long)Convert.ToInt32(Console.ReadLine());
				try
				{
					Console.WriteLine(Apilog.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}

				Console.WriteLine(Apilog.TAG_MSGSOURCE + ":  ");
				Msgsource = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_TRACE + ":  ");
				Trace = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_IS_SUCCESS + ":  ");
				IsSuccess = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Apilog.TAG_IN_PROGRESS + ":  ");
				InProgress = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Apilog.TAG_HTTP_STATUS_STR + ":  ");
				HttpStatusStr = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_HTTP_STATUS_NUM + ":  ");
				HttpStatusNum = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Apilog.TAG_MSGTXT + ":  ");
				Msgtxt = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_REQTXT + ":  ");
				Reqtxt = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_RESPTXT + ":  ");
				Resptxt = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_DURATION_IN_MS + ":  ");
				DurationInMs = (long)Convert.ToInt32(Console.ReadLine());
				try
				{
					Console.WriteLine(Apilog.TAG_CALL_START_TIME + ":  ");
					CallStartTime = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					CallStartTime = new DateTime();
				}
				try
				{
					Console.WriteLine(Apilog.TAG_CALL_END_TIME + ":  ");
					CallEndTime = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					CallEndTime = new DateTime();
				}

				Console.WriteLine(Apilog.TAG_SEARCHTEXT + ":  ");
				Searchtext = Console.ReadLine();

				Console.WriteLine(Apilog.TAG_AUTHUSERID + ":  ");
				Authuserid = Console.ReadLine();

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
			SqlParameter paramRefNum = null;
			SqlParameter paramDateCreated = null;
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
			SqlParameter paramCallStartTime = null;
			SqlParameter paramCallEndTime = null;
			SqlParameter paramSearchtext = null;
			SqlParameter paramAuthuserid = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramApikeyID = new SqlParameter("@" + TAG_APIKEY_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;

			paramRefNum = new SqlParameter("@" + TAG_REF_NUM, RefNum);
			paramRefNum.DbType = DbType.Int32;
			paramRefNum.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;

			paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
			paramMsgsource.DbType = DbType.String;
			paramMsgsource.Size = 255;
			paramMsgsource.Direction = ParameterDirection.Input;

			paramTrace = new SqlParameter("@" + TAG_TRACE, Trace);
			paramTrace.DbType = DbType.String;
			paramTrace.Size = 255;
			paramTrace.Direction = ParameterDirection.Input;

			paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
			paramIsSuccess.DbType = DbType.Boolean;
			paramIsSuccess.Direction = ParameterDirection.Input;

			paramInProgress = new SqlParameter("@" + TAG_IN_PROGRESS, InProgress);
			paramInProgress.DbType = DbType.Boolean;
			paramInProgress.Direction = ParameterDirection.Input;

			paramHttpStatusStr = new SqlParameter("@" + TAG_HTTP_STATUS_STR, HttpStatusStr);
			paramHttpStatusStr.DbType = DbType.String;
			paramHttpStatusStr.Size = 255;
			paramHttpStatusStr.Direction = ParameterDirection.Input;

			paramHttpStatusNum = new SqlParameter("@" + TAG_HTTP_STATUS_NUM, HttpStatusNum);
			paramHttpStatusNum.DbType = DbType.Int32;
			paramHttpStatusNum.Direction = ParameterDirection.Input;

			paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
			paramMsgtxt.DbType = DbType.String;
			paramMsgtxt.Direction = ParameterDirection.Input;

			paramReqtxt = new SqlParameter("@" + TAG_REQTXT, Reqtxt);
			paramReqtxt.DbType = DbType.String;
			paramReqtxt.Direction = ParameterDirection.Input;

			paramResptxt = new SqlParameter("@" + TAG_RESPTXT, Resptxt);
			paramResptxt.DbType = DbType.String;
			paramResptxt.Direction = ParameterDirection.Input;

			paramDurationInMs = new SqlParameter("@" + TAG_DURATION_IN_MS, DurationInMs);
			paramDurationInMs.DbType = DbType.Int32;
			paramDurationInMs.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(CallStartTime))
			{
				paramCallStartTime = new SqlParameter("@" + TAG_CALL_START_TIME, CallStartTime);
			}
			else
			{
				paramCallStartTime = new SqlParameter("@" + TAG_CALL_START_TIME, DBNull.Value);
			}
			paramCallStartTime.DbType = DbType.DateTime;
			paramCallStartTime.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(CallEndTime))
			{
				paramCallEndTime = new SqlParameter("@" + TAG_CALL_END_TIME, CallEndTime);
			}
			else
			{
				paramCallEndTime = new SqlParameter("@" + TAG_CALL_END_TIME, DBNull.Value);
			}
			paramCallEndTime.DbType = DbType.DateTime;
			paramCallEndTime.Direction = ParameterDirection.Input;

			paramSearchtext = new SqlParameter("@" + TAG_SEARCHTEXT, Searchtext);
			paramSearchtext.DbType = DbType.String;
			paramSearchtext.Size = 255;
			paramSearchtext.Direction = ParameterDirection.Input;

			paramAuthuserid = new SqlParameter("@" + TAG_AUTHUSERID, Authuserid);
			paramAuthuserid.DbType = DbType.String;
			paramAuthuserid.Size = 255;
			paramAuthuserid.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramApikeyID);
			cmd.Parameters.Add(paramRefNum);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramMsgsource);
			cmd.Parameters.Add(paramTrace);
			cmd.Parameters.Add(paramIsSuccess);
			cmd.Parameters.Add(paramInProgress);
			cmd.Parameters.Add(paramHttpStatusStr);
			cmd.Parameters.Add(paramHttpStatusNum);
			cmd.Parameters.Add(paramMsgtxt);
			cmd.Parameters.Add(paramReqtxt);
			cmd.Parameters.Add(paramResptxt);
			cmd.Parameters.Add(paramDurationInMs);
			cmd.Parameters.Add(paramCallStartTime);
			cmd.Parameters.Add(paramCallEndTime);
			cmd.Parameters.Add(paramSearchtext);
			cmd.Parameters.Add(paramAuthuserid);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			ApilogID = long.Parse(strTmp);

			// cleanup to help GC
			paramApikeyID = null;
			paramRefNum = null;
			paramDateCreated = null;
			paramMsgsource = null;
			paramTrace = null;
			paramIsSuccess = null;
			paramInProgress = null;
			paramHttpStatusStr = null;
			paramHttpStatusNum = null;
			paramMsgtxt = null;
			paramReqtxt = null;
			paramResptxt = null;
			paramDurationInMs = null;
			paramCallStartTime = null;
			paramCallEndTime = null;
			paramSearchtext = null;
			paramAuthuserid = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramApilogID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramApilogID = new SqlParameter("@" + TAG_ID, ApilogID);
			paramApilogID.Direction = ParameterDirection.Input;
			paramApilogID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramApilogID);
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
			paramApilogID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApilogID = null;
			SqlParameter paramApikeyID = null;
			SqlParameter paramRefNum = null;
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
			SqlParameter paramCallStartTime = null;
			SqlParameter paramCallEndTime = null;
			SqlParameter paramSearchtext = null;
			SqlParameter paramAuthuserid = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramApilogID = new SqlParameter("@" + TAG_ID, ApilogID);
			paramApilogID.DbType = DbType.Int32;
			paramApilogID.Direction = ParameterDirection.Input;


			paramApikeyID = new SqlParameter("@" + TAG_APIKEY_ID, ApikeyID);
			paramApikeyID.DbType = DbType.Int32;
			paramApikeyID.Direction = ParameterDirection.Input;

			paramRefNum = new SqlParameter("@" + TAG_REF_NUM, RefNum);
			paramRefNum.DbType = DbType.Int32;
			paramRefNum.Direction = ParameterDirection.Input;


			paramMsgsource = new SqlParameter("@" + TAG_MSGSOURCE, Msgsource);
			paramMsgsource.DbType = DbType.String;
			paramMsgsource.Size = 255;
			paramMsgsource.Direction = ParameterDirection.Input;

			paramTrace = new SqlParameter("@" + TAG_TRACE, Trace);
			paramTrace.DbType = DbType.String;
			paramTrace.Size = 255;
			paramTrace.Direction = ParameterDirection.Input;

			paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
			paramIsSuccess.DbType = DbType.Boolean;
			paramIsSuccess.Direction = ParameterDirection.Input;

			paramInProgress = new SqlParameter("@" + TAG_IN_PROGRESS, InProgress);
			paramInProgress.DbType = DbType.Boolean;
			paramInProgress.Direction = ParameterDirection.Input;

			paramHttpStatusStr = new SqlParameter("@" + TAG_HTTP_STATUS_STR, HttpStatusStr);
			paramHttpStatusStr.DbType = DbType.String;
			paramHttpStatusStr.Size = 255;
			paramHttpStatusStr.Direction = ParameterDirection.Input;

			paramHttpStatusNum = new SqlParameter("@" + TAG_HTTP_STATUS_NUM, HttpStatusNum);
			paramHttpStatusNum.DbType = DbType.Int32;
			paramHttpStatusNum.Direction = ParameterDirection.Input;

			paramMsgtxt = new SqlParameter("@" + TAG_MSGTXT, Msgtxt);
			paramMsgtxt.DbType = DbType.String;
			paramMsgtxt.Direction = ParameterDirection.Input;

			paramReqtxt = new SqlParameter("@" + TAG_REQTXT, Reqtxt);
			paramReqtxt.DbType = DbType.String;
			paramReqtxt.Direction = ParameterDirection.Input;

			paramResptxt = new SqlParameter("@" + TAG_RESPTXT, Resptxt);
			paramResptxt.DbType = DbType.String;
			paramResptxt.Direction = ParameterDirection.Input;

			paramDurationInMs = new SqlParameter("@" + TAG_DURATION_IN_MS, DurationInMs);
			paramDurationInMs.DbType = DbType.Int32;
			paramDurationInMs.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(CallStartTime))
			{
				paramCallStartTime = new SqlParameter("@" + TAG_CALL_START_TIME, CallStartTime);
			}
			else
			{
				paramCallStartTime = new SqlParameter("@" + TAG_CALL_START_TIME, DBNull.Value);
			}
			paramCallStartTime.DbType = DbType.DateTime;
			paramCallStartTime.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(CallEndTime))
			{
				paramCallEndTime = new SqlParameter("@" + TAG_CALL_END_TIME, CallEndTime);
			}
			else
			{
				paramCallEndTime = new SqlParameter("@" + TAG_CALL_END_TIME, DBNull.Value);
			}
			paramCallEndTime.DbType = DbType.DateTime;
			paramCallEndTime.Direction = ParameterDirection.Input;

			paramSearchtext = new SqlParameter("@" + TAG_SEARCHTEXT, Searchtext);
			paramSearchtext.DbType = DbType.String;
			paramSearchtext.Size = 255;
			paramSearchtext.Direction = ParameterDirection.Input;

			paramAuthuserid = new SqlParameter("@" + TAG_AUTHUSERID, Authuserid);
			paramAuthuserid.DbType = DbType.String;
			paramAuthuserid.Size = 255;
			paramAuthuserid.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramApilogID);
			cmd.Parameters.Add(paramApikeyID);
			cmd.Parameters.Add(paramRefNum);
			cmd.Parameters.Add(paramMsgsource);
			cmd.Parameters.Add(paramTrace);
			cmd.Parameters.Add(paramIsSuccess);
			cmd.Parameters.Add(paramInProgress);
			cmd.Parameters.Add(paramHttpStatusStr);
			cmd.Parameters.Add(paramHttpStatusNum);
			cmd.Parameters.Add(paramMsgtxt);
			cmd.Parameters.Add(paramReqtxt);
			cmd.Parameters.Add(paramResptxt);
			cmd.Parameters.Add(paramDurationInMs);
			cmd.Parameters.Add(paramCallStartTime);
			cmd.Parameters.Add(paramCallEndTime);
			cmd.Parameters.Add(paramSearchtext);
			cmd.Parameters.Add(paramAuthuserid);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			ApilogID = long.Parse(s);

			// cleanup
			paramApilogID = null;
			paramApikeyID = null;
			paramRefNum = null;
			paramMsgsource = null;
			paramTrace = null;
			paramIsSuccess = null;
			paramInProgress = null;
			paramHttpStatusStr = null;
			paramHttpStatusNum = null;
			paramMsgtxt = null;
			paramReqtxt = null;
			paramResptxt = null;
			paramDurationInMs = null;
			paramCallStartTime = null;
			paramCallEndTime = null;
			paramSearchtext = null;
			paramAuthuserid = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApilogID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramApilogID = new SqlParameter("@" + TAG_ID, ApilogID);
			paramApilogID.DbType = DbType.Int32;
			paramApilogID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramApilogID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramApilogID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramApilogID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramApilogID = new SqlParameter("@" + TAG_ID, ApilogID);
			paramApilogID.DbType = DbType.Int32;
			paramApilogID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramApilogID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramApilogID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.ApilogID = long.Parse(rdr[DB_FIELD_ID].ToString());
			try
			{
			this.ApikeyID = Convert.ToInt32(rdr[DB_FIELD_APIKEY_ID].ToString().Trim());
			}
			catch{}
			try
			{
			this.RefNum = Convert.ToInt32(rdr[DB_FIELD_REF_NUM].ToString().Trim());
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
			this.Msgsource = rdr[DB_FIELD_MSGSOURCE].ToString().Trim();
			}
			catch{}
			try
			{
			this.Trace = rdr[DB_FIELD_TRACE].ToString().Trim();
			}
			catch{}
			try
			{
			this.IsSuccess = Convert.ToBoolean(rdr[DB_FIELD_IS_SUCCESS].ToString().Trim());
			}
			catch{}
			try
			{
			this.InProgress = Convert.ToBoolean(rdr[DB_FIELD_IN_PROGRESS].ToString().Trim());
			}
			catch{}
			try
			{
			this.HttpStatusStr = rdr[DB_FIELD_HTTP_STATUS_STR].ToString().Trim();
			}
			catch{}
			try
			{
			this.HttpStatusNum = Convert.ToInt32(rdr[DB_FIELD_HTTP_STATUS_NUM].ToString().Trim());
			}
			catch{}
			try
			{
			       this.Msgtxt = rdr[DB_FIELD_MSGTXT].ToString().Trim();
			}
			catch{}
			try
			{
			       this.Reqtxt = rdr[DB_FIELD_REQTXT].ToString().Trim();
			}
			catch{}
			try
			{
			       this.Resptxt = rdr[DB_FIELD_RESPTXT].ToString().Trim();
			}
			catch{}
			try
			{
			this.DurationInMs = Convert.ToInt32(rdr[DB_FIELD_DURATION_IN_MS].ToString().Trim());
			}
			catch{}
         try
			{
				this.CallStartTime = DateTime.Parse(rdr[DB_FIELD_CALL_START_TIME].ToString());
			}
			catch 
			{
			}
         try
			{
				this.CallEndTime = DateTime.Parse(rdr[DB_FIELD_CALL_END_TIME].ToString());
			}
			catch 
			{
			}
			try
			{
			this.Searchtext = rdr[DB_FIELD_SEARCHTEXT].ToString().Trim();
			}
			catch{}
			try
			{
			this.Authuserid = rdr[DB_FIELD_AUTHUSERID].ToString().Trim();
			}
			catch{}
		}

	}
}

//END OF Apilog CLASS FILE
