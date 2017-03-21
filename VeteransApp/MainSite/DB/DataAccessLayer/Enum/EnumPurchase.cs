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
	/// File:  EnumPurchase.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/21/2017	Created
	/// 
	/// ----------------------------------------------------
	/// </summary>
	public class EnumPurchase
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
		public static readonly string ENTITY_NAME = "EnumPurchase"; //Table name to abstract
		private static DateTime dtNull = new DateTime();
		private static readonly string PARAM_COUNT = "@COUNT"; //Sp count parameter

		private long _lPurchaseID = 0;
		private DateTime _dtBeginDateCreated = new DateTime();
		private DateTime _dtEndDateCreated = new DateTime();
		private DateTime _dtBeginDateModified = new DateTime();
		private DateTime _dtEndDateModified = new DateTime();
		private string _strAuthtoken = null;
		private bool? _bIsSuccess = null;
		private bool? _bIsError = null;
		private string _strErrorTrace = null;
		private string _strResponseJson = null;
		private long _lAmountInPennies = 0;
		private long _lNumItemsInCart = 0;
		private bool? _bIsDownloaded = null;
		private DateTime _dtBeginDownloadDate = new DateTime();
		private DateTime _dtEndDownloadDate = new DateTime();
//		private string _strOrderByEnum = "ASC";
		private string _strOrderByField = DB_FIELD_ID;

		/// <summary>DB_FIELD_ID Attribute type string</summary>
		public static readonly string DB_FIELD_ID = "purchase_id"; //Table id field name
		/// <summary>PurchaseID Attribute type string</summary>
		public static readonly string TAG_PURCHASE_ID = "PurchaseID"; //Attribute PurchaseID  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_CREATED = "BeginDateCreated"; //Attribute DateCreated  name
		/// <summary>EndDateCreated Attribute type string</summary>
		public static readonly string TAG_END_DATE_CREATED = "EndDateCreated"; //Attribute DateCreated  name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_BEGIN_DATE_MODIFIED = "BeginDateModified"; //Attribute DateModified  name
		/// <summary>EndDateModified Attribute type string</summary>
		public static readonly string TAG_END_DATE_MODIFIED = "EndDateModified"; //Attribute DateModified  name
		/// <summary>Authtoken Attribute type string</summary>
		public static readonly string TAG_AUTHTOKEN = "Authtoken"; //Attribute Authtoken  name
		/// <summary>IsSuccess Attribute type string</summary>
		public static readonly string TAG_IS_SUCCESS = "IsSuccess"; //Attribute IsSuccess  name
		/// <summary>IsError Attribute type string</summary>
		public static readonly string TAG_IS_ERROR = "IsError"; //Attribute IsError  name
		/// <summary>ErrorTrace Attribute type string</summary>
		public static readonly string TAG_ERROR_TRACE = "ErrorTrace"; //Attribute ErrorTrace  name
		/// <summary>ResponseJson Attribute type string</summary>
		public static readonly string TAG_RESPONSE_JSON = "ResponseJson"; //Attribute ResponseJson  name
		/// <summary>AmountInPennies Attribute type string</summary>
		public static readonly string TAG_AMOUNT_IN_PENNIES = "AmountInPennies"; //Attribute AmountInPennies  name
		/// <summary>NumItemsInCart Attribute type string</summary>
		public static readonly string TAG_NUM_ITEMS_IN_CART = "NumItemsInCart"; //Attribute NumItemsInCart  name
		/// <summary>IsDownloaded Attribute type string</summary>
		public static readonly string TAG_IS_DOWNLOADED = "IsDownloaded"; //Attribute IsDownloaded  name
		/// <summary>DownloadDate Attribute type string</summary>
		public static readonly string TAG_BEGIN_DOWNLOAD_DATE = "BeginDownloadDate"; //Attribute DownloadDate  name
		/// <summary>EndDownloadDate Attribute type string</summary>
		public static readonly string TAG_END_DOWNLOAD_DATE = "EndDownloadDate"; //Attribute DownloadDate  name
		// Stored procedure name
		public string SP_ENUM_NAME = "spPurchaseEnum"; //Enum sp name

		/// <summary>HasError is a Property in the Purchase Class of type bool</summary>
		public bool HasError 
		{
			get{return _hasError;}
			set{_hasError = value;}
		}
		/// <summary>PurchaseID is a Property in the Purchase Class of type long</summary>
		public long PurchaseID 
		{
			get{return _lPurchaseID;}
			set{_lPurchaseID = value;}
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
		/// <summary>Authtoken is a Property in the Purchase Class of type String</summary>
		public string Authtoken 
		{
			get{return _strAuthtoken;}
			set{_strAuthtoken = value;}
		}
		/// <summary>IsSuccess is a Property in the Purchase Class of type bool</summary>
		public bool? IsSuccess 
		{
			get{return _bIsSuccess;}
			set{_bIsSuccess = value;}
		}
		/// <summary>IsError is a Property in the Purchase Class of type bool</summary>
		public bool? IsError 
		{
			get{return _bIsError;}
			set{_bIsError = value;}
		}
		/// <summary>ErrorTrace is a Property in the Purchase Class of type String</summary>
		public string ErrorTrace 
		{
			get{return _strErrorTrace;}
			set{_strErrorTrace = value;}
		}
		/// <summary>ResponseJson is a Property in the Purchase Class of type String</summary>
		public string ResponseJson 
		{
			get{return _strResponseJson;}
			set{_strResponseJson = value;}
		}
		/// <summary>AmountInPennies is a Property in the Purchase Class of type long</summary>
		public long AmountInPennies 
		{
			get{return _lAmountInPennies;}
			set{_lAmountInPennies = value;}
		}
		/// <summary>NumItemsInCart is a Property in the Purchase Class of type long</summary>
		public long NumItemsInCart 
		{
			get{return _lNumItemsInCart;}
			set{_lNumItemsInCart = value;}
		}
		/// <summary>IsDownloaded is a Property in the Purchase Class of type bool</summary>
		public bool? IsDownloaded 
		{
			get{return _bIsDownloaded;}
			set{_bIsDownloaded = value;}
		}
		/// <summary>Property DownloadDate. Type: DateTime</summary>
		public DateTime BeginDownloadDate
		{
			get{return _dtBeginDownloadDate;}
			set{_dtBeginDownloadDate = value;}
		}
		/// <summary>Property DownloadDate. Type: DateTime</summary>
		public DateTime EndDownloadDate
		{
			get{return _dtEndDownloadDate;}
			set{_dtEndDownloadDate = value;}
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
		public EnumPurchase()
		{
		}
		/// <summary>Contructor takes 1 parameter: SqlConnection</summary>
		public EnumPurchase(SqlConnection conn)
		{
			_conn = conn;
		}


		// Implementation of IEnumerator
		/// <summary>Property of type Purchase. Returns the next Purchase in the list</summary>
		private Purchase _nextTransaction
		{
			get
			{
				Purchase o = null;
				
				if (!_bSetup)
				{
					EnumData();
				}
				if (_hasMore)
				{
					o = new Purchase(_rdr);
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

		/// <summary>ToString is overridden to display all properties of the Purchase Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_PURCHASE_ID + ":  " + PurchaseID.ToString() + "\n");
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
			sbReturn.Append(TAG_AUTHTOKEN + ":  " + Authtoken + "\n");
			sbReturn.Append(TAG_IS_SUCCESS + ":  " + IsSuccess + "\n");
			sbReturn.Append(TAG_IS_ERROR + ":  " + IsError + "\n");
			sbReturn.Append(TAG_ERROR_TRACE + ":  " + ErrorTrace + "\n");
			sbReturn.Append(TAG_RESPONSE_JSON + ":  " + ResponseJson + "\n");
			sbReturn.Append(TAG_AMOUNT_IN_PENNIES + ":  " + AmountInPennies + "\n");
			sbReturn.Append(TAG_NUM_ITEMS_IN_CART + ":  " + NumItemsInCart + "\n");
			sbReturn.Append(TAG_IS_DOWNLOADED + ":  " + IsDownloaded + "\n");
			if (!dtNull.Equals(BeginDownloadDate))
			{
				sbReturn.Append(TAG_BEGIN_DOWNLOAD_DATE + ":  " + BeginDownloadDate.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_BEGIN_DOWNLOAD_DATE + ":\n");
			}
			if (!dtNull.Equals(EndDownloadDate))
			{
				sbReturn.Append(TAG_END_DOWNLOAD_DATE + ":  " + EndDownloadDate.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_END_DOWNLOAD_DATE + ":\n");
			}

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Purchase</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<" + ENTITY_NAME + ">\n");
			sbReturn.Append("<" + TAG_PURCHASE_ID + ">" + PurchaseID + "</" + TAG_PURCHASE_ID + ">\n");
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
			sbReturn.Append("<" + TAG_AUTHTOKEN + ">" + Authtoken + "</" + TAG_AUTHTOKEN + ">\n");
			sbReturn.Append("<" + TAG_IS_SUCCESS + ">" + IsSuccess + "</" + TAG_IS_SUCCESS + ">\n");
			sbReturn.Append("<" + TAG_IS_ERROR + ">" + IsError + "</" + TAG_IS_ERROR + ">\n");
			sbReturn.Append("<" + TAG_ERROR_TRACE + ">" + ErrorTrace + "</" + TAG_ERROR_TRACE + ">\n");
			sbReturn.Append("<" + TAG_RESPONSE_JSON + ">" + ResponseJson + "</" + TAG_RESPONSE_JSON + ">\n");
			sbReturn.Append("<" + TAG_AMOUNT_IN_PENNIES + ">" + AmountInPennies + "</" + TAG_AMOUNT_IN_PENNIES + ">\n");
			sbReturn.Append("<" + TAG_NUM_ITEMS_IN_CART + ">" + NumItemsInCart + "</" + TAG_NUM_ITEMS_IN_CART + ">\n");
			sbReturn.Append("<" + TAG_IS_DOWNLOADED + ">" + IsDownloaded + "</" + TAG_IS_DOWNLOADED + ">\n");
			if (!dtNull.Equals(BeginDownloadDate))
			{
				sbReturn.Append("<" + TAG_BEGIN_DOWNLOAD_DATE + ">" + BeginDownloadDate.ToString() + "</" + TAG_BEGIN_DOWNLOAD_DATE + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_BEGIN_DOWNLOAD_DATE + "></" + TAG_BEGIN_DOWNLOAD_DATE + ">\n");
			}
			if (!dtNull.Equals(EndDownloadDate))
			{
				sbReturn.Append("<" + TAG_END_DOWNLOAD_DATE + ">" + EndDownloadDate.ToString() + "</" + TAG_END_DOWNLOAD_DATE + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_END_DOWNLOAD_DATE + "></" + TAG_END_DOWNLOAD_DATE + ">\n");
			}
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
				xResultNode = xNode.SelectSingleNode(TAG_PURCHASE_ID);
				strTmp = xResultNode.InnerText;
				PurchaseID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_AUTHTOKEN);
				Authtoken = xResultNode.InnerText;
				if (Authtoken.Trim().Length == 0)
					Authtoken = null;
			}
			catch  
			{
				Authtoken = null;
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
				xResultNode = xNode.SelectSingleNode(TAG_IS_ERROR);
				IsError = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			IsError = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_ERROR_TRACE);
				ErrorTrace = xResultNode.InnerText;
				if (ErrorTrace.Trim().Length == 0)
					ErrorTrace = null;
			}
			catch  
			{
				ErrorTrace = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_RESPONSE_JSON);
				ResponseJson = xResultNode.InnerText;
				if (ResponseJson.Trim().Length == 0)
					ResponseJson = null;
			}
			catch  
			{
				ResponseJson = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_AMOUNT_IN_PENNIES);
				AmountInPennies = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			AmountInPennies = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_NUM_ITEMS_IN_CART);
				NumItemsInCart = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			NumItemsInCart = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_IS_DOWNLOADED);
				IsDownloaded = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			IsDownloaded = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_BEGIN_DOWNLOAD_DATE);
				BeginDownloadDate = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_END_DOWNLOAD_DATE);
				EndDownloadDate = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
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


				Console.WriteLine(TAG_AUTHTOKEN + ":  ");
				Authtoken = Console.ReadLine();
				if (Authtoken.Length == 0)
				{
					Authtoken = null;
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

				Console.WriteLine(TAG_IS_ERROR + ":  ");
				try
				{
					IsError = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					IsError = false;
				}


				Console.WriteLine(TAG_ERROR_TRACE + ":  ");
				ErrorTrace = Console.ReadLine();
				if (ErrorTrace.Length == 0)
				{
					ErrorTrace = null;
				}

				Console.WriteLine(TAG_RESPONSE_JSON + ":  ");
				ResponseJson = Console.ReadLine();
				if (ResponseJson.Length == 0)
				{
					ResponseJson = null;
				}
				Console.WriteLine(TAG_AMOUNT_IN_PENNIES + ":  ");
				try
				{
					AmountInPennies = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					AmountInPennies = 0;
				}

				Console.WriteLine(TAG_NUM_ITEMS_IN_CART + ":  ");
				try
				{
					NumItemsInCart = (long)Convert.ToInt32(Console.ReadLine());
				}
				catch 
				{
					NumItemsInCart = 0;
				}

				Console.WriteLine(TAG_IS_DOWNLOADED + ":  ");
				try
				{
					IsDownloaded = Convert.ToBoolean(Console.ReadLine());
				}
				catch 
				{
					IsDownloaded = false;
				}

				Console.WriteLine(TAG_BEGIN_DOWNLOAD_DATE + ":  ");
				try
				{
					string s = Console.ReadLine();
					BeginDownloadDate = DateTime.Parse(s);
				}
				catch 
				{
					BeginDownloadDate = new DateTime();
				}

				Console.WriteLine(TAG_END_DOWNLOAD_DATE + ":  ");
				try
				{
					string s = Console.ReadLine();
					EndDownloadDate = DateTime.Parse(s);
				}
				catch  
				{
					EndDownloadDate = new DateTime();
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
			SqlParameter paramPurchaseID = null;
			SqlParameter paramBeginDateCreated = null;
			SqlParameter paramEndDateCreated = null;
			SqlParameter paramBeginDateModified = null;
			SqlParameter paramEndDateModified = null;
			SqlParameter paramAuthtoken = null;
			SqlParameter paramIsSuccess = null;
			SqlParameter paramIsError = null;
			SqlParameter paramErrorTrace = null;
			SqlParameter paramResponseJson = null;
			SqlParameter paramAmountInPennies = null;
			SqlParameter paramNumItemsInCart = null;
			SqlParameter paramIsDownloaded = null;
			SqlParameter paramBeginDownloadDate = null;
			SqlParameter paramEndDownloadDate = null;
			DateTime dtNull = new DateTime();

			sbLog = new System.Text.StringBuilder();
				paramPurchaseID = new SqlParameter("@" + TAG_PURCHASE_ID, PurchaseID);
				sbLog.Append(TAG_PURCHASE_ID + "=" + PurchaseID + "\n");
				paramPurchaseID.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramPurchaseID);

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

			// Setup the authtoken text param
			if ( Authtoken != null )
			{
				paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, Authtoken);
				sbLog.Append(TAG_AUTHTOKEN + "=" + Authtoken + "\n");
			}
			else
			{
				paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, DBNull.Value);
			}
			paramAuthtoken.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramAuthtoken);

				paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
				sbLog.Append(TAG_IS_SUCCESS + "=" + IsSuccess + "\n");
				paramIsSuccess.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsSuccess);
				paramIsError = new SqlParameter("@" + TAG_IS_ERROR, IsError);
				sbLog.Append(TAG_IS_ERROR + "=" + IsError + "\n");
				paramIsError.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsError);
			// Setup the error trace text param
			if ( ErrorTrace != null )
			{
				paramErrorTrace = new SqlParameter("@" + TAG_ERROR_TRACE, ErrorTrace);
				sbLog.Append(TAG_ERROR_TRACE + "=" + ErrorTrace + "\n");
			}
			else
			{
				paramErrorTrace = new SqlParameter("@" + TAG_ERROR_TRACE, DBNull.Value);
			}
			paramErrorTrace.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramErrorTrace);

			// Setup the response json text param
			if ( ResponseJson != null )
			{
				paramResponseJson = new SqlParameter("@" + TAG_RESPONSE_JSON, ResponseJson);
				sbLog.Append(TAG_RESPONSE_JSON + "=" + ResponseJson + "\n");
			}
			else
			{
				paramResponseJson = new SqlParameter("@" + TAG_RESPONSE_JSON, DBNull.Value);
			}
			paramResponseJson.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramResponseJson);

				paramAmountInPennies = new SqlParameter("@" + TAG_AMOUNT_IN_PENNIES, AmountInPennies);
				sbLog.Append(TAG_AMOUNT_IN_PENNIES + "=" + AmountInPennies + "\n");
				paramAmountInPennies.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramAmountInPennies);

				paramNumItemsInCart = new SqlParameter("@" + TAG_NUM_ITEMS_IN_CART, NumItemsInCart);
				sbLog.Append(TAG_NUM_ITEMS_IN_CART + "=" + NumItemsInCart + "\n");
				paramNumItemsInCart.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramNumItemsInCart);

				paramIsDownloaded = new SqlParameter("@" + TAG_IS_DOWNLOADED, IsDownloaded);
				sbLog.Append(TAG_IS_DOWNLOADED + "=" + IsDownloaded + "\n");
				paramIsDownloaded.Direction = ParameterDirection.Input;
				_cmd.Parameters.Add(paramIsDownloaded);
			// Setup the download date param
			if (!dtNull.Equals(BeginDownloadDate))
			{
				paramBeginDownloadDate = new SqlParameter("@" + TAG_BEGIN_DOWNLOAD_DATE, BeginDownloadDate);
			}
			else
			{
				paramBeginDownloadDate = new SqlParameter("@" + TAG_BEGIN_DOWNLOAD_DATE, DBNull.Value);
			}
			paramBeginDownloadDate.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramBeginDownloadDate);

			if (!dtNull.Equals(EndDownloadDate))
			{
				paramEndDownloadDate = new SqlParameter("@" + TAG_END_DOWNLOAD_DATE, EndDownloadDate);
			}
			else
			{
				paramEndDownloadDate = new SqlParameter("@" + TAG_END_DOWNLOAD_DATE, DBNull.Value);
			}
			paramEndDownloadDate.Direction = ParameterDirection.Input;
			_cmd.Parameters.Add(paramEndDownloadDate);

		}

	}
}

