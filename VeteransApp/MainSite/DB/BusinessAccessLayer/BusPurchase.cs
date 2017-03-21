using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Vetapp.Engine.Common;
using Vetapp.Engine.DataAccessLayer.Enumeration;
using Vetapp.Engine.DataAccessLayer.Data;

namespace Vetapp.Engine.BusinessAccessLayer
{

	/// <summary>
	/// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  BusPurchase.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	3/21/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Purchase objects.
	/// </summary>
	public class BusPurchase
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_DATE_MODIFIED = "";
		private const String REGEXP_ISVALID_AUTHTOKEN = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_IS_SUCCESS = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_IS_ERROR = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_ERROR_TRACE = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
		private const String REGEXP_ISVALID_RESPONSE_JSON = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
		private const String REGEXP_ISVALID_AMOUNT_IN_PENNIES = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_NUM_ITEMS_IN_CART = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_IS_DOWNLOADED = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_DOWNLOAD_DATE = "";

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusPurchase constructor takes SqlConnection object</summary>
		public BusPurchase()
		{
		}
		/// <summary>BusPurchase constructor takes SqlConnection object</summary>
		public BusPurchase(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Purchase objects
	///     <remarks>   
	///         No parameters. Returns all Purchase objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Purchase objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, false, false, null, null, 0, 0, false, new DateTime(), new DateTime()));
	}

	 /// <summary>
	///     Gets all Purchase objects
	///     <remarks>   
	///         No parameters. Returns all Purchase objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Purchase objects</retvalue>
	/// </summary>
	public ArrayList Get(long lPurchaseID)
	{
		return (Get(lPurchaseID , new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, false, false, null, null, 0, 0, false, new DateTime(), new DateTime()));
	}

        /// <summary>
        ///     Gets all Purchase objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Purchase to be returned</param>
        ///     <retvalue>ArrayList containing Purchase object</retvalue>
        /// </summary>
	public ArrayList Get(Purchase o)
	{	
		return (Get( o.PurchaseID, o.DateCreated, o.DateCreated, o.DateModified, o.DateModified, o.Authtoken, o.IsSuccess, o.IsError, o.ErrorTrace, o.ResponseJson, o.AmountInPennies, o.NumItemsInCart, o.IsDownloaded, o.DownloadDate, o.DownloadDate	));
	}

        /// <summary>
        ///     Gets all Purchase objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Purchase to be returned</param>
        ///     <retvalue>ArrayList containing Purchase object</retvalue>
        /// </summary>
	public ArrayList Get(EnumPurchase o)
	{	
		return (Get( o.PurchaseID, o.BeginDateCreated, o.EndDateCreated, o.BeginDateModified, o.EndDateModified, o.Authtoken, o.IsSuccess, o.IsError, o.ErrorTrace, o.ResponseJson, o.AmountInPennies, o.NumItemsInCart, o.IsDownloaded, o.BeginDownloadDate, o.EndDownloadDate	));
	}

		/// <summary>
		///     Gets all Purchase objects
		///     <remarks>   
		///         Returns Purchase objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Purchase object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngPurchaseID, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, DateTime pDtBeginDateModified, DateTime pDtEndDateModified, string pStrAuthtoken, bool? pBolIsSuccess, bool? pBolIsError, string pStrErrorTrace, string pStrResponseJson, long pLngAmountInPennies, long pLngNumItemsInCart, bool? pBolIsDownloaded, DateTime pDtBeginDownloadDate, DateTime pDtEndDownloadDate)
		{
			Purchase data = null;
			_arrlstEntities = new ArrayList();
			EnumPurchase enumPurchase = new EnumPurchase(_conn);
			 enumPurchase.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumPurchase.SP_ENUM_NAME;
			enumPurchase.PurchaseID = pLngPurchaseID;
			enumPurchase.BeginDateCreated = pDtBeginDateCreated;
			enumPurchase.EndDateCreated = pDtEndDateCreated;
			enumPurchase.BeginDateModified = pDtBeginDateModified;
			enumPurchase.EndDateModified = pDtEndDateModified;
			enumPurchase.Authtoken = pStrAuthtoken;
			enumPurchase.IsSuccess = pBolIsSuccess;
			enumPurchase.IsError = pBolIsError;
			enumPurchase.ErrorTrace = pStrErrorTrace;
			enumPurchase.ResponseJson = pStrResponseJson;
			enumPurchase.AmountInPennies = pLngAmountInPennies;
			enumPurchase.NumItemsInCart = pLngNumItemsInCart;
			enumPurchase.IsDownloaded = pBolIsDownloaded;
			enumPurchase.BeginDownloadDate = pDtBeginDownloadDate;
			enumPurchase.EndDownloadDate = pDtEndDownloadDate;
			enumPurchase.EnumData();
			while (enumPurchase.hasMoreElements())
			{
				data = (Purchase) enumPurchase.nextElement();
				_arrlstEntities.Add(data);
			}
			enumPurchase = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Purchase object to database
        ///     <param name="o">Purchase to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Purchase o)
		{
			if ( o != null )
			{
				o.Save(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify Purchase object to database
		///     <param name="o">Purchase to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Purchase o)
		{
			if ( o != null )
			{
				o.Update(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify Purchase object to database
		///     <param name="o">Purchase to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Purchase o)
		{
			if ( o != null )
			{
				o.Load(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Modify Purchase object to database
		///     <param name="o">Purchase to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Purchase o)
		{
			if ( o != null )
			{
				o.Delete(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}
		}

		/// <summary>
		///     Exist Purchase object to database
		///     <param name="o">Purchase to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Purchase o)
		{
			bool bExist = false;
			if ( o != null )
			{
				bExist = o.Exist(_conn);
				if ( o.HasError )
				{
					_hasError = true;
				}
			}

			return bExist;
		}
		/// <summary>Property as to whether or not the object has an Error</summary>
		public bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Property as to whether or not the object has invalid columns</summary>
		public bool HasInvalid 
		{
			get{return _hasInvalid;}
		}
		/// <summary>Property which returns all column error in an ArrayList</summary>
		public ArrayList ColumnErrors
		{
			get{return _arrlstColumnErrors;}
		}
		/// <summary>Property returns an ArrayList containing Purchase objects</summary>
		public ArrayList Purchases 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Purchase data = null;
					_arrlstEntities = new ArrayList();
					EnumPurchase enumPurchase = new EnumPurchase(_conn);
					enumPurchase.EnumData();
					while (enumPurchase.hasMoreElements())
					{
						data = (Purchase) enumPurchase.nextElement();
						_arrlstEntities.Add(data);
					}
					enumPurchase = null;
					ArrayList.ReadOnly(_arrlstEntities);
				}
				return _arrlstEntities;
			}
		}

		/// <summary>
		///     Checks to make sure all values are valid
		///     <remarks>   
		///         Calls "IsValid" method for each property in ocject
		///     </remarks>   
		///     <retvalue>true if object has valid entries, false otherwise</retvalue>
		/// </summary>
		public bool IsValid(Purchase pRefPurchase)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidPurchaseID(pRefPurchase.PurchaseID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefPurchase.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateModified(pRefPurchase.DateModified);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidAuthtoken(pRefPurchase.Authtoken);
			if (!isValidTmp && pRefPurchase.Authtoken != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsSuccess(pRefPurchase.IsSuccess);
			if (!isValidTmp && pRefPurchase.IsSuccess != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsError(pRefPurchase.IsError);
			if (!isValidTmp && pRefPurchase.IsError != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidErrorTrace(pRefPurchase.ErrorTrace);
			if (!isValidTmp && pRefPurchase.ErrorTrace != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidResponseJson(pRefPurchase.ResponseJson);
			if (!isValidTmp && pRefPurchase.ResponseJson != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidAmountInPennies(pRefPurchase.AmountInPennies);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidNumItemsInCart(pRefPurchase.NumItemsInCart);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsDownloaded(pRefPurchase.IsDownloaded);
			if (!isValidTmp && pRefPurchase.IsDownloaded != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDownloadDate(pRefPurchase.DownloadDate);
			if (!isValidTmp)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidPurchaseID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_ID;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDateCreated(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DATE_CREATED)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_DATE_CREATED;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDateModified(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DATE_MODIFIED)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_DATE_MODIFIED;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidAuthtoken(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_AUTHTOKEN)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_AUTHTOKEN;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidIsSuccess(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IS_SUCCESS)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_IS_SUCCESS;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidIsError(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IS_ERROR)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_IS_ERROR;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidErrorTrace(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_ERROR_TRACE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_ERROR_TRACE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidResponseJson(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_RESPONSE_JSON)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_RESPONSE_JSON;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidAmountInPennies(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_AMOUNT_IN_PENNIES)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_AMOUNT_IN_PENNIES;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidNumItemsInCart(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_NUM_ITEMS_IN_CART)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_NUM_ITEMS_IN_CART;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidIsDownloaded(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IS_DOWNLOADED)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_IS_DOWNLOADED;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidDownloadDate(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DOWNLOAD_DATE)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Purchase.DB_FIELD_DOWNLOAD_DATE;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
