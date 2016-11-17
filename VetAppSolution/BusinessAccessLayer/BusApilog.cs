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
	/// Copyright (c) 2016 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  BusApilog.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	11/16/2016	Created
	/// 
	/// ----------------------------------------------------
	/// Business Class for Apilog objects.
	/// </summary>
	public class BusApilog
	{
		private SqlConnection _conn = null;
		private bool _hasError = false;
		private bool _hasInvalid = false;

		private ArrayList _arrlstEntities = null;
		private ArrayList _arrlstColumnErrors = new ArrayList();

		private const String REGEXP_ISVALID_ID= BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_APIKEY_ID = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_REF_NUM = BusValidationExpressions.REGEX_TYPE_PATTERN_NUMERIC10;
		private const String REGEXP_ISVALID_DATE_CREATED = "";
		private const String REGEXP_ISVALID_MSGSOURCE = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_TRACE = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_IS_SUCCESS = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_IN_PROGRESS = BusValidationExpressions.REGEX_TYPE_PATTERN_BIT;
		private const String REGEXP_ISVALID_HTTP_STATUS_STR = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_HTTP_STATUS_NUM = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_MSGTXT = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
		private const String REGEXP_ISVALID_REQTXT = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
		private const String REGEXP_ISVALID_RESPTXT = BusValidationExpressions.REGEX_TYPE_PATTERN_TEXT;
		private const String REGEXP_ISVALID_DURATION_IN_MS = BusValidationExpressions.REGEX_TYPE_PATTERN_INT;
		private const String REGEXP_ISVALID_CALL_START_TIME = "";
		private const String REGEXP_ISVALID_CALL_END_TIME = "";
		private const String REGEXP_ISVALID_SEARCHTEXT = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;
		private const String REGEXP_ISVALID_AUTHUSERID = BusValidationExpressions.REGEX_TYPE_PATTERN_NVARCHAR255;

		public string SP_ENUM_NAME = null;


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>BusApilog constructor takes SqlConnection object</summary>
		public BusApilog()
		{
		}
		/// <summary>BusApilog constructor takes SqlConnection object</summary>
		public BusApilog(SqlConnection conn)
		{
			_conn = conn;
		}

	 /// <summary>
	///     Gets all Apilog objects
	///     <remarks>   
	///         No parameters. Returns all Apilog objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Apilog objects</retvalue>
	/// </summary>
	public ArrayList Get()
	{
		return (Get(0, 0, 0, new DateTime(), new DateTime(), null, null, false, false, null, 0, null, null, null, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null));
	}

	 /// <summary>
	///     Gets all Apilog objects
	///     <remarks>   
	///         No parameters. Returns all Apilog objects 
	///     </remarks>   
	///     <retvalue>ArrayList containing all Apilog objects</retvalue>
	/// </summary>
	public ArrayList Get(long lApilogID)
	{
		return (Get(lApilogID , 0, 0, new DateTime(), new DateTime(), null, null, false, false, null, 0, null, null, null, 0, new DateTime(), new DateTime(), new DateTime(), new DateTime(), null, null));
	}

        /// <summary>
        ///     Gets all Apilog objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Apilog to be returned</param>
        ///     <retvalue>ArrayList containing Apilog object</retvalue>
        /// </summary>
	public ArrayList Get(Apilog o)
	{	
		return (Get( o.ApilogID, o.ApikeyID, o.RefNum, o.DateCreated, o.DateCreated, o.Msgsource, o.Trace, o.IsSuccess, o.InProgress, o.HttpStatusStr, o.HttpStatusNum, o.Msgtxt, o.Reqtxt, o.Resptxt, o.DurationInMs, o.CallStartTime, o.CallStartTime, o.CallEndTime, o.CallEndTime, o.Searchtext, o.Authuserid	));
	}

        /// <summary>
        ///     Gets all Apilog objects
        ///     <remarks>   
        ///         Returns ArrayList containing object passed in 
        ///     </remarks>   
        ///     <param name="o">Apilog to be returned</param>
        ///     <retvalue>ArrayList containing Apilog object</retvalue>
        /// </summary>
	public ArrayList Get(EnumApilog o)
	{	
		return (Get( o.ApilogID, o.ApikeyID, o.RefNum, o.BeginDateCreated, o.EndDateCreated, o.Msgsource, o.Trace, o.IsSuccess, o.InProgress, o.HttpStatusStr, o.HttpStatusNum, o.Msgtxt, o.Reqtxt, o.Resptxt, o.DurationInMs, o.BeginCallStartTime, o.EndCallStartTime, o.BeginCallEndTime, o.EndCallEndTime, o.Searchtext, o.Authuserid	));
	}

		/// <summary>
		///     Gets all Apilog objects
		///     <remarks>   
		///         Returns Apilog objects in an array list 
		///         using the given criteria 
		///     </remarks>   
		///     <retvalue>ArrayList containing Apilog object</retvalue>
		/// </summary>
		public ArrayList Get( long pLngApilogID, long pLngApikeyID, long pLngRefNum, DateTime pDtBeginDateCreated, DateTime pDtEndDateCreated, string pStrMsgsource, string pStrTrace, bool? pBolIsSuccess, bool? pBolInProgress, string pStrHttpStatusStr, long pLngHttpStatusNum, string pStrMsgtxt, string pStrReqtxt, string pStrResptxt, long pLngDurationInMs, DateTime pDtBeginCallStartTime, DateTime pDtEndCallStartTime, DateTime pDtBeginCallEndTime, DateTime pDtEndCallEndTime, string pStrSearchtext, string pStrAuthuserid)
		{
			Apilog data = null;
			_arrlstEntities = new ArrayList();
			EnumApilog enumApilog = new EnumApilog(_conn);
			 enumApilog.SP_ENUM_NAME = (!string.IsNullOrEmpty(SP_ENUM_NAME)) ? SP_ENUM_NAME : enumApilog.SP_ENUM_NAME;
			enumApilog.ApilogID = pLngApilogID;
			enumApilog.ApikeyID = pLngApikeyID;
			enumApilog.RefNum = pLngRefNum;
			enumApilog.BeginDateCreated = pDtBeginDateCreated;
			enumApilog.EndDateCreated = pDtEndDateCreated;
			enumApilog.Msgsource = pStrMsgsource;
			enumApilog.Trace = pStrTrace;
			enumApilog.IsSuccess = pBolIsSuccess;
			enumApilog.InProgress = pBolInProgress;
			enumApilog.HttpStatusStr = pStrHttpStatusStr;
			enumApilog.HttpStatusNum = pLngHttpStatusNum;
			enumApilog.Msgtxt = pStrMsgtxt;
			enumApilog.Reqtxt = pStrReqtxt;
			enumApilog.Resptxt = pStrResptxt;
			enumApilog.DurationInMs = pLngDurationInMs;
			enumApilog.BeginCallStartTime = pDtBeginCallStartTime;
			enumApilog.EndCallStartTime = pDtEndCallStartTime;
			enumApilog.BeginCallEndTime = pDtBeginCallEndTime;
			enumApilog.EndCallEndTime = pDtEndCallEndTime;
			enumApilog.Searchtext = pStrSearchtext;
			enumApilog.Authuserid = pStrAuthuserid;
			enumApilog.EnumData();
			while (enumApilog.hasMoreElements())
			{
				data = (Apilog) enumApilog.nextElement();
				_arrlstEntities.Add(data);
			}
			enumApilog = null;
			ArrayList.ReadOnly(_arrlstEntities);
			return _arrlstEntities;
		}

        /// <summary>
        ///     Saves Apilog object to database
        ///     <param name="o">Apilog to be saved.</param>
        ///     <retvalue>void</retvalue>
        /// </summary>
		public void Save(Apilog o)
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
		///     Modify Apilog object to database
		///     <param name="o">Apilog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Update(Apilog o)
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
		///     Modify Apilog object to database
		///     <param name="o">Apilog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Load(Apilog o)
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
		///     Modify Apilog object to database
		///     <param name="o">Apilog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public void Delete(Apilog o)
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
		///     Exist Apilog object to database
		///     <param name="o">Apilog to be modified.</param>
		///     <retvalue>void</retvalue>
		/// </summary>
		public bool Exist(Apilog o)
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
		/// <summary>Property returns an ArrayList containing Apilog objects</summary>
		public ArrayList Apilogs 
		{
			get
			{
				if ( _arrlstEntities == null )
				{
					Apilog data = null;
					_arrlstEntities = new ArrayList();
					EnumApilog enumApilog = new EnumApilog(_conn);
					enumApilog.EnumData();
					while (enumApilog.hasMoreElements())
					{
						data = (Apilog) enumApilog.nextElement();
						_arrlstEntities.Add(data);
					}
					enumApilog = null;
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
		public bool IsValid(Apilog pRefApilog)
		{
			bool isValid = true;
			bool isValidTmp = true;
            
			_arrlstColumnErrors = null;
			_arrlstColumnErrors = new ArrayList();

			isValidTmp = IsValidApilogID(pRefApilog.ApilogID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidApikeyID(pRefApilog.ApikeyID);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidRefNum(pRefApilog.RefNum);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidDateCreated(pRefApilog.DateCreated);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidMsgsource(pRefApilog.Msgsource);
			if (!isValidTmp && pRefApilog.Msgsource != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidTrace(pRefApilog.Trace);
			if (!isValidTmp && pRefApilog.Trace != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidIsSuccess(pRefApilog.IsSuccess);
			if (!isValidTmp && pRefApilog.IsSuccess != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidInProgress(pRefApilog.InProgress);
			if (!isValidTmp && pRefApilog.InProgress != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidHttpStatusStr(pRefApilog.HttpStatusStr);
			if (!isValidTmp && pRefApilog.HttpStatusStr != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidHttpStatusNum(pRefApilog.HttpStatusNum);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidMsgtxt(pRefApilog.Msgtxt);
			if (!isValidTmp && pRefApilog.Msgtxt != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidReqtxt(pRefApilog.Reqtxt);
			if (!isValidTmp && pRefApilog.Reqtxt != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidResptxt(pRefApilog.Resptxt);
			if (!isValidTmp && pRefApilog.Resptxt != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidDurationInMs(pRefApilog.DurationInMs);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCallStartTime(pRefApilog.CallStartTime);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidCallEndTime(pRefApilog.CallEndTime);
			if (!isValidTmp)
			{
				isValid = false;
			}
			isValidTmp = IsValidSearchtext(pRefApilog.Searchtext);
			if (!isValidTmp && pRefApilog.Searchtext != null)
			{
				isValid = false;
			}
			isValidTmp = IsValidAuthuserid(pRefApilog.Authuserid);
			if (!isValidTmp && pRefApilog.Authuserid != null)
			{
				isValid = false;
			}

			return isValid;
		}
		/// <summary>
		///     Checks to make sure value is valid
		///     <retvalue>true if object has a valid entry, false otherwise</retvalue>
		/// </summary>
		public bool IsValidApilogID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_ID;
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
		public bool IsValidApikeyID(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_APIKEY_ID)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_APIKEY_ID;
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
		public bool IsValidRefNum(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_REF_NUM)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_REF_NUM;
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
				clm.ColumnName = Apilog.DB_FIELD_DATE_CREATED;
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
		public bool IsValidMsgsource(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_MSGSOURCE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_MSGSOURCE;
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
		public bool IsValidTrace(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_TRACE)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_TRACE;
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
				clm.ColumnName = Apilog.DB_FIELD_IS_SUCCESS;
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
		public bool IsValidInProgress(bool? pBolData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_IN_PROGRESS)).IsMatch(pBolData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_IN_PROGRESS;
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
		public bool IsValidHttpStatusStr(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_HTTP_STATUS_STR)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_HTTP_STATUS_STR;
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
		public bool IsValidHttpStatusNum(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_HTTP_STATUS_NUM)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_HTTP_STATUS_NUM;
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
		public bool IsValidMsgtxt(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_MSGTXT)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_MSGTXT;
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
		public bool IsValidReqtxt(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_REQTXT)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_REQTXT;
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
		public bool IsValidResptxt(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_RESPTXT)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_RESPTXT;
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
		public bool IsValidDurationInMs(long pLngData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_DURATION_IN_MS)).IsMatch(pLngData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_DURATION_IN_MS;
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
		public bool IsValidCallStartTime(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_CALL_START_TIME)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_CALL_START_TIME;
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
		public bool IsValidCallEndTime(DateTime pDtData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = (new Regex(REGEXP_ISVALID_CALL_END_TIME)).IsMatch(pDtData.ToString());
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_CALL_END_TIME;
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
		public bool IsValidSearchtext(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_SEARCHTEXT)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_SEARCHTEXT;
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
		public bool IsValidAuthuserid(string pStrData)
		{
			bool isValid = true;
            
			// do some validation
			isValid = !(new Regex(REGEXP_ISVALID_AUTHUSERID)).IsMatch(pStrData);
			if ( !isValid )
			{
				Column clm = null;
				clm = new Column();
				clm.ColumnName = Apilog.DB_FIELD_AUTHUSERID;
				clm.HasError = true;
				_arrlstColumnErrors.Add(clm);
				_hasInvalid = true;
			}
			return isValid;
		}
	}
}
 // END OF CLASS FILE
