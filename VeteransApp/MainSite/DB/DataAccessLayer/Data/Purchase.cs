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
	/// File:  Purchase.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/21/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the Purchase database table.
	/// </summary>
	public class Purchase
	{
		//Attributes
		/// <summary>PurchaseID Attribute type String</summary>
		private long _lPurchaseID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateModified Attribute type String</summary>
		private DateTime _dtDateModified = dtNull;
		/// <summary>Authtoken Attribute type String</summary>
		private string _strAuthtoken = null;
		/// <summary>IsSuccess Attribute type String</summary>
		private bool? _bIsSuccess = null;
		/// <summary>IsError Attribute type String</summary>
		private bool? _bIsError = null;
		/// <summary>ErrorTrace Attribute type String</summary>
		private string _strErrorTrace = null;
		/// <summary>ResponseJson Attribute type String</summary>
		private string _strResponseJson = null;
		/// <summary>AmountInPennies Attribute type String</summary>
		private long _lAmountInPennies = 0;
		/// <summary>NumItemsInCart Attribute type String</summary>
		private long _lNumItemsInCart = 0;
		/// <summary>IsDownloaded Attribute type String</summary>
		private bool? _bIsDownloaded = null;
		/// <summary>DownloadDate Attribute type String</summary>
		private DateTime _dtDownloadDate = dtNull;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class Purchase and is of type bool</summary>
		public static readonly string ENTITY_NAME = "Purchase"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "purchase_id"; //Table id field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_modified Database field </summary>
		public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
		/// <summary>authtoken Database field </summary>
		public static readonly string DB_FIELD_AUTHTOKEN = "authtoken"; //Table Authtoken field name
		/// <summary>is_success Database field </summary>
		public static readonly string DB_FIELD_IS_SUCCESS = "is_success"; //Table IsSuccess field name
		/// <summary>is_error Database field </summary>
		public static readonly string DB_FIELD_IS_ERROR = "is_error"; //Table IsError field name
		/// <summary>error_trace Database field </summary>
		public static readonly string DB_FIELD_ERROR_TRACE = "error_trace"; //Table ErrorTrace field name
		/// <summary>response_json Database field </summary>
		public static readonly string DB_FIELD_RESPONSE_JSON = "response_json"; //Table ResponseJson field name
		/// <summary>amount_in_pennies Database field </summary>
		public static readonly string DB_FIELD_AMOUNT_IN_PENNIES = "amount_in_pennies"; //Table AmountInPennies field name
		/// <summary>num_items_in_cart Database field </summary>
		public static readonly string DB_FIELD_NUM_ITEMS_IN_CART = "num_items_in_cart"; //Table NumItemsInCart field name
		/// <summary>is_downloaded Database field </summary>
		public static readonly string DB_FIELD_IS_DOWNLOADED = "is_downloaded"; //Table IsDownloaded field name
		/// <summary>download_date Database field </summary>
		public static readonly string DB_FIELD_DOWNLOAD_DATE = "download_date"; //Table DownloadDate field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "PurchaseID"; //Attribute id  name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
		/// <summary>Authtoken Attribute type string</summary>
		public static readonly string TAG_AUTHTOKEN = "Authtoken"; //Table Authtoken field name
		/// <summary>IsSuccess Attribute type string</summary>
		public static readonly string TAG_IS_SUCCESS = "IsSuccess"; //Table IsSuccess field name
		/// <summary>IsError Attribute type string</summary>
		public static readonly string TAG_IS_ERROR = "IsError"; //Table IsError field name
		/// <summary>ErrorTrace Attribute type string</summary>
		public static readonly string TAG_ERROR_TRACE = "ErrorTrace"; //Table ErrorTrace field name
		/// <summary>ResponseJson Attribute type string</summary>
		public static readonly string TAG_RESPONSE_JSON = "ResponseJson"; //Table ResponseJson field name
		/// <summary>AmountInPennies Attribute type string</summary>
		public static readonly string TAG_AMOUNT_IN_PENNIES = "AmountInPennies"; //Table AmountInPennies field name
		/// <summary>NumItemsInCart Attribute type string</summary>
		public static readonly string TAG_NUM_ITEMS_IN_CART = "NumItemsInCart"; //Table NumItemsInCart field name
		/// <summary>IsDownloaded Attribute type string</summary>
		public static readonly string TAG_IS_DOWNLOADED = "IsDownloaded"; //Table IsDownloaded field name
		/// <summary>DownloadDate Attribute type string</summary>
		public static readonly string TAG_DOWNLOAD_DATE = "DownloadDate"; //Table DownloadDate field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spPurchaseInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spPurchaseUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spPurchaseDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spPurchaseLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spPurchaseExist"; //Exist sp name

		//properties
		/// <summary>PurchaseID is a Property in the Purchase Class of type long</summary>
		public long PurchaseID 
		{
			get{return _lPurchaseID;}
			set{_lPurchaseID = value;}
		}
		/// <summary>DateCreated is a Property in the Purchase Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the Purchase Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
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
		/// <summary>DownloadDate is a Property in the Purchase Class of type DateTime</summary>
		public DateTime DownloadDate 
		{
			get{return _dtDownloadDate;}
			set{_dtDownloadDate = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class Purchase and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class Purchase and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>Purchase empty constructor</summary>
		public Purchase()
		{
		}
		/// <summary>Purchase constructor takes PurchaseID and a SqlConnection</summary>
		public Purchase(long l, SqlConnection conn) 
		{
			PurchaseID = l;
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
		/// <summary>Purchase Constructor takes pStrData and Config</summary>
		public Purchase(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>Purchase Constructor takes SqlDataReader</summary>
		public Purchase(SqlDataReader rd)
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
		/// <summary>ToString is overridden to display all properties of the Purchase Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + PurchaseID.ToString() + "\n");
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
			sbReturn.Append(TAG_AUTHTOKEN + ":  " + Authtoken + "\n");
			sbReturn.Append(TAG_IS_SUCCESS + ":  " + IsSuccess + "\n");
			sbReturn.Append(TAG_IS_ERROR + ":  " + IsError + "\n");
			sbReturn.Append(TAG_ERROR_TRACE + ":  " + ErrorTrace + "\n");
			sbReturn.Append(TAG_RESPONSE_JSON + ":  " + ResponseJson + "\n");
			sbReturn.Append(TAG_AMOUNT_IN_PENNIES + ":  " + AmountInPennies + "\n");
			sbReturn.Append(TAG_NUM_ITEMS_IN_CART + ":  " + NumItemsInCart + "\n");
			sbReturn.Append(TAG_IS_DOWNLOADED + ":  " + IsDownloaded + "\n");
			if (!dtNull.Equals(DownloadDate))
			{
				sbReturn.Append(TAG_DOWNLOAD_DATE + ":  " + DownloadDate.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DOWNLOAD_DATE + ":\n");
			}

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of Purchase</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<Purchase>\n");
			sbReturn.Append("<" + TAG_ID + ">" + PurchaseID + "</" + TAG_ID + ">\n");
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
			sbReturn.Append("<" + TAG_AUTHTOKEN + ">" + Authtoken + "</" + TAG_AUTHTOKEN + ">\n");
			sbReturn.Append("<" + TAG_IS_SUCCESS + ">" + IsSuccess + "</" + TAG_IS_SUCCESS + ">\n");
			sbReturn.Append("<" + TAG_IS_ERROR + ">" + IsError + "</" + TAG_IS_ERROR + ">\n");
			sbReturn.Append("<" + TAG_ERROR_TRACE + ">" + ErrorTrace + "</" + TAG_ERROR_TRACE + ">\n");
			sbReturn.Append("<" + TAG_RESPONSE_JSON + ">" + ResponseJson + "</" + TAG_RESPONSE_JSON + ">\n");
			sbReturn.Append("<" + TAG_AMOUNT_IN_PENNIES + ">" + AmountInPennies + "</" + TAG_AMOUNT_IN_PENNIES + ">\n");
			sbReturn.Append("<" + TAG_NUM_ITEMS_IN_CART + ">" + NumItemsInCart + "</" + TAG_NUM_ITEMS_IN_CART + ">\n");
			sbReturn.Append("<" + TAG_IS_DOWNLOADED + ">" + IsDownloaded + "</" + TAG_IS_DOWNLOADED + ">\n");
			if (!dtNull.Equals(DownloadDate))
			{
				sbReturn.Append("<" + TAG_DOWNLOAD_DATE + ">" + DownloadDate.ToString() + "</" + TAG_DOWNLOAD_DATE + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DOWNLOAD_DATE + "></" + TAG_DOWNLOAD_DATE + ">\n");
			}
			sbReturn.Append("</Purchase>" + "\n");

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
				PurchaseID = (long) Convert.ToInt32(strTmp);
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
				xResultNode = xNode.SelectSingleNode(TAG_AUTHTOKEN);
				Authtoken = xResultNode.InnerText;
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
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_RESPONSE_JSON);
				ResponseJson = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
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
				xResultNode = xNode.SelectSingleNode(TAG_DOWNLOAD_DATE);
				DownloadDate = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}
		}
		/// <summary>Calls sqlLoad() method which gets record from database with purchase_id equal to the current object's PurchaseID </summary>
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
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where purchase_id equal to the current object's PurchaseID </summary>
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
		/// <summary>Calls sqlDelete() method which delete's the record from database where where purchase_id equal to the current object's PurchaseID </summary>
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
				try
				{
					Console.WriteLine(Purchase.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(Purchase.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(Purchase.TAG_AUTHTOKEN + ":  ");
				Authtoken = Console.ReadLine();

				Console.WriteLine(Purchase.TAG_IS_SUCCESS + ":  ");
				IsSuccess = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Purchase.TAG_IS_ERROR + ":  ");
				IsError = Convert.ToBoolean(Console.ReadLine());

				Console.WriteLine(Purchase.TAG_ERROR_TRACE + ":  ");
				ErrorTrace = Console.ReadLine();

				Console.WriteLine(Purchase.TAG_RESPONSE_JSON + ":  ");
				ResponseJson = Console.ReadLine();

				Console.WriteLine(Purchase.TAG_AMOUNT_IN_PENNIES + ":  ");
				AmountInPennies = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Purchase.TAG_NUM_ITEMS_IN_CART + ":  ");
				NumItemsInCart = (long)Convert.ToInt32(Console.ReadLine());

				Console.WriteLine(Purchase.TAG_IS_DOWNLOADED + ":  ");
				IsDownloaded = Convert.ToBoolean(Console.ReadLine());
				try
				{
					Console.WriteLine(Purchase.TAG_DOWNLOAD_DATE + ":  ");
					DownloadDate = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DownloadDate = new DateTime();
				}

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
			SqlParameter paramDateCreated = null;
			SqlParameter paramAuthtoken = null;
			SqlParameter paramIsSuccess = null;
			SqlParameter paramIsError = null;
			SqlParameter paramErrorTrace = null;
			SqlParameter paramResponseJson = null;
			SqlParameter paramAmountInPennies = null;
			SqlParameter paramNumItemsInCart = null;
			SqlParameter paramIsDownloaded = null;
			SqlParameter paramDownloadDate = null;
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


			paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, Authtoken);
			paramAuthtoken.DbType = DbType.String;
			paramAuthtoken.Size = 255;
			paramAuthtoken.Direction = ParameterDirection.Input;

			paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
			paramIsSuccess.DbType = DbType.Boolean;
			paramIsSuccess.Direction = ParameterDirection.Input;

			paramIsError = new SqlParameter("@" + TAG_IS_ERROR, IsError);
			paramIsError.DbType = DbType.Boolean;
			paramIsError.Direction = ParameterDirection.Input;

			paramErrorTrace = new SqlParameter("@" + TAG_ERROR_TRACE, ErrorTrace);
			paramErrorTrace.DbType = DbType.String;
			paramErrorTrace.Direction = ParameterDirection.Input;

			paramResponseJson = new SqlParameter("@" + TAG_RESPONSE_JSON, ResponseJson);
			paramResponseJson.DbType = DbType.String;
			paramResponseJson.Direction = ParameterDirection.Input;

			paramAmountInPennies = new SqlParameter("@" + TAG_AMOUNT_IN_PENNIES, AmountInPennies);
			paramAmountInPennies.DbType = DbType.Int32;
			paramAmountInPennies.Direction = ParameterDirection.Input;

			paramNumItemsInCart = new SqlParameter("@" + TAG_NUM_ITEMS_IN_CART, NumItemsInCart);
			paramNumItemsInCart.DbType = DbType.Int32;
			paramNumItemsInCart.Direction = ParameterDirection.Input;

			paramIsDownloaded = new SqlParameter("@" + TAG_IS_DOWNLOADED, IsDownloaded);
			paramIsDownloaded.DbType = DbType.Boolean;
			paramIsDownloaded.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(DownloadDate))
			{
				paramDownloadDate = new SqlParameter("@" + TAG_DOWNLOAD_DATE, DownloadDate);
			}
			else
			{
				paramDownloadDate = new SqlParameter("@" + TAG_DOWNLOAD_DATE, DBNull.Value);
			}
			paramDownloadDate.DbType = DbType.DateTime;
			paramDownloadDate.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramAuthtoken);
			cmd.Parameters.Add(paramIsSuccess);
			cmd.Parameters.Add(paramIsError);
			cmd.Parameters.Add(paramErrorTrace);
			cmd.Parameters.Add(paramResponseJson);
			cmd.Parameters.Add(paramAmountInPennies);
			cmd.Parameters.Add(paramNumItemsInCart);
			cmd.Parameters.Add(paramIsDownloaded);
			cmd.Parameters.Add(paramDownloadDate);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			PurchaseID = long.Parse(strTmp);

			// cleanup to help GC
			paramDateCreated = null;
			paramAuthtoken = null;
			paramIsSuccess = null;
			paramIsError = null;
			paramErrorTrace = null;
			paramResponseJson = null;
			paramAmountInPennies = null;
			paramNumItemsInCart = null;
			paramIsDownloaded = null;
			paramDownloadDate = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramPurchaseID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramPurchaseID = new SqlParameter("@" + TAG_ID, PurchaseID);
			paramPurchaseID.Direction = ParameterDirection.Input;
			paramPurchaseID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramPurchaseID);
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
			paramPurchaseID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramPurchaseID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramAuthtoken = null;
			SqlParameter paramIsSuccess = null;
			SqlParameter paramIsError = null;
			SqlParameter paramErrorTrace = null;
			SqlParameter paramResponseJson = null;
			SqlParameter paramAmountInPennies = null;
			SqlParameter paramNumItemsInCart = null;
			SqlParameter paramIsDownloaded = null;
			SqlParameter paramDownloadDate = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramPurchaseID = new SqlParameter("@" + TAG_ID, PurchaseID);
			paramPurchaseID.DbType = DbType.Int32;
			paramPurchaseID.Direction = ParameterDirection.Input;



				paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
			paramDateModified.DbType = DbType.DateTime;
			paramDateModified.Direction = ParameterDirection.Input;

			paramAuthtoken = new SqlParameter("@" + TAG_AUTHTOKEN, Authtoken);
			paramAuthtoken.DbType = DbType.String;
			paramAuthtoken.Size = 255;
			paramAuthtoken.Direction = ParameterDirection.Input;

			paramIsSuccess = new SqlParameter("@" + TAG_IS_SUCCESS, IsSuccess);
			paramIsSuccess.DbType = DbType.Boolean;
			paramIsSuccess.Direction = ParameterDirection.Input;

			paramIsError = new SqlParameter("@" + TAG_IS_ERROR, IsError);
			paramIsError.DbType = DbType.Boolean;
			paramIsError.Direction = ParameterDirection.Input;

			paramErrorTrace = new SqlParameter("@" + TAG_ERROR_TRACE, ErrorTrace);
			paramErrorTrace.DbType = DbType.String;
			paramErrorTrace.Direction = ParameterDirection.Input;

			paramResponseJson = new SqlParameter("@" + TAG_RESPONSE_JSON, ResponseJson);
			paramResponseJson.DbType = DbType.String;
			paramResponseJson.Direction = ParameterDirection.Input;

			paramAmountInPennies = new SqlParameter("@" + TAG_AMOUNT_IN_PENNIES, AmountInPennies);
			paramAmountInPennies.DbType = DbType.Int32;
			paramAmountInPennies.Direction = ParameterDirection.Input;

			paramNumItemsInCart = new SqlParameter("@" + TAG_NUM_ITEMS_IN_CART, NumItemsInCart);
			paramNumItemsInCart.DbType = DbType.Int32;
			paramNumItemsInCart.Direction = ParameterDirection.Input;

			paramIsDownloaded = new SqlParameter("@" + TAG_IS_DOWNLOADED, IsDownloaded);
			paramIsDownloaded.DbType = DbType.Boolean;
			paramIsDownloaded.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(DownloadDate))
			{
				paramDownloadDate = new SqlParameter("@" + TAG_DOWNLOAD_DATE, DownloadDate);
			}
			else
			{
				paramDownloadDate = new SqlParameter("@" + TAG_DOWNLOAD_DATE, DBNull.Value);
			}
			paramDownloadDate.DbType = DbType.DateTime;
			paramDownloadDate.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramPurchaseID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramAuthtoken);
			cmd.Parameters.Add(paramIsSuccess);
			cmd.Parameters.Add(paramIsError);
			cmd.Parameters.Add(paramErrorTrace);
			cmd.Parameters.Add(paramResponseJson);
			cmd.Parameters.Add(paramAmountInPennies);
			cmd.Parameters.Add(paramNumItemsInCart);
			cmd.Parameters.Add(paramIsDownloaded);
			cmd.Parameters.Add(paramDownloadDate);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			PurchaseID = long.Parse(s);

			// cleanup
			paramPurchaseID = null;
			paramDateModified = null;
			paramAuthtoken = null;
			paramIsSuccess = null;
			paramIsError = null;
			paramErrorTrace = null;
			paramResponseJson = null;
			paramAmountInPennies = null;
			paramNumItemsInCart = null;
			paramIsDownloaded = null;
			paramDownloadDate = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramPurchaseID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramPurchaseID = new SqlParameter("@" + TAG_ID, PurchaseID);
			paramPurchaseID.DbType = DbType.Int32;
			paramPurchaseID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramPurchaseID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramPurchaseID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramPurchaseID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramPurchaseID = new SqlParameter("@" + TAG_ID, PurchaseID);
			paramPurchaseID.DbType = DbType.Int32;
			paramPurchaseID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramPurchaseID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramPurchaseID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.PurchaseID = long.Parse(rdr[DB_FIELD_ID].ToString());
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
			this.Authtoken = rdr[DB_FIELD_AUTHTOKEN].ToString().Trim();
			}
			catch{}
			try
			{
			this.IsSuccess = Convert.ToBoolean(rdr[DB_FIELD_IS_SUCCESS].ToString().Trim());
			}
			catch{}
			try
			{
			this.IsError = Convert.ToBoolean(rdr[DB_FIELD_IS_ERROR].ToString().Trim());
			}
			catch{}
			try
			{
			       this.ErrorTrace = rdr[DB_FIELD_ERROR_TRACE].ToString().Trim();
			}
			catch{}
			try
			{
			       this.ResponseJson = rdr[DB_FIELD_RESPONSE_JSON].ToString().Trim();
			}
			catch{}
			try
			{
			this.AmountInPennies = Convert.ToInt32(rdr[DB_FIELD_AMOUNT_IN_PENNIES].ToString().Trim());
			}
			catch{}
			try
			{
			this.NumItemsInCart = Convert.ToInt32(rdr[DB_FIELD_NUM_ITEMS_IN_CART].ToString().Trim());
			}
			catch{}
			try
			{
			this.IsDownloaded = Convert.ToBoolean(rdr[DB_FIELD_IS_DOWNLOADED].ToString().Trim());
			}
			catch{}
         try
			{
				this.DownloadDate = DateTime.Parse(rdr[DB_FIELD_DOWNLOAD_DATE].ToString());
			}
			catch 
			{
			}
		}

	}
}

//END OF Purchase CLASS FILE
